# Singleton & SharedInstance

Garyon provides simplified patterns for implementing singleton and shared instance patterns in C#.

## Singleton Pattern

The `Singleton<T>` class provides a simple, thread-safe singleton implementation.

### Basic Usage

```csharp
using Garyon.Objects;

public class Configuration
{
    public static Configuration Instance => Singleton<Configuration>.Instance;

    // Private constructor prevents external instantiation
    private Configuration()
    {
        // Initialize
    }

    public string Setting { get; set; }
    public int MaxConnections { get; set; } = 10;
}

// Usage
var config = Configuration.Instance;
config.Setting = "Production";
```

### How It Works

`Singleton<T>` uses a static readonly field initialized at class load time:

```csharp
public sealed class Singleton<T> where T : new()
{
    public static readonly T Instance = new();
}
```

This provides:
- **Thread safety**: Initialized by CLR before any access
- **Lazy initialization**: Only created when first accessed
- **Simple API**: No boilerplate code needed

### Multiple Singleton Styles

```csharp
using Garyon.Objects;

// Style 1: Property delegation
public class Logger
{
    public static Logger Instance => Singleton<Logger>.Instance;
    private Logger() { }

    public void Log(string message) => Console.WriteLine(message);
}

// Style 2: Direct field
public class Cache
{
    public static readonly Cache Instance = Singleton<Cache>.Instance;
    private Cache() { }

    private Dictionary<string, object> _data = new();
}

// Style 3: Lazy<T> wrapper
public class DatabaseConnection
{
    private static readonly Lazy<DatabaseConnection> _instance =
        new(() => Singleton<DatabaseConnection>.Instance);

    public static DatabaseConnection Instance => _instance.Value;

    private DatabaseConnection() { }
}
```

## SharedInstance Pattern

The `ISharedInstance` interface combined with `SharedInstanceExtensions` provides a convention-based shared instance pattern.

> [!VERSION]
> **C# 14 required:** the `.Shared` extension uses C# 14 extension members syntax.

### Basic Usage

```csharp
using Garyon.Objects;

public class Settings : ISharedInstance
{
    public string AppName { get; set; }
    public bool DebugMode { get; set; }
}

// Access via extension
var settings = Settings.Shared;
settings.AppName = "MyApp";

// Same instance everywhere
var sameSettings = Settings.Shared;
Debug.Assert(ReferenceEquals(settings, sameSettings));
```

### Implementation

The `SharedInstanceExtensions` provides the `.Shared` property:

```csharp
public static class SharedInstanceExtensions
{
    extension<T>(T type) where T : ISharedInstance, new()
    {
        public static T Shared => Singleton<T>.Instance;
    }
}
```

### Marker Interface

`ISharedInstance` is a marker interface:

```csharp
public interface ISharedInstance
{
    // Empty - just marks types as having a shared instance
}
```

## Examples

### Application Settings

```csharp
using Garyon.Objects;

public class AppSettings : ISharedInstance
{
    private AppSettings()
    {
        // Load from configuration file
        LoadSettings();
    }

    public string DatabaseConnection { get; set; }
    public int MaxRetries { get; set; }
    public TimeSpan Timeout { get; set; }

    private void LoadSettings()
    {
        // Load from app.config, appsettings.json, etc.
    }

    public void Save()
    {
        // Persist settings
    }
}

// Usage throughout application
public class DataService
{
    public void Connect()
    {
        var settings = AppSettings.Shared;
        ConnectToDatabase(settings.DatabaseConnection);
    }
}

public class RetryHandler
{
    public void Process()
    {
        var maxRetries = AppSettings.Shared.MaxRetries;
        // Use setting
    }
}
```

### Logger

```csharp
using Garyon.Objects;

public class Logger : ISharedInstance
{
    private readonly object _lock = new();
    private readonly List<string> _logs = new();

    private Logger() { }

    public void Log(string message)
    {
        var timestamped = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";

        lock (_lock)
        {
            _logs.Add(timestamped);
            Console.WriteLine(timestamped);
        }
    }

    public void LogError(string message)
    {
        Log($"ERROR: {message}");
    }

    public void LogWarning(string message)
    {
        Log($"WARNING: {message}");
    }

    public IReadOnlyList<string> GetLogs()
    {
        lock (_lock)
        {
            return _logs.ToList();
        }
    }
}

// Usage
Logger.Shared.Log("Application started");
Logger.Shared.LogError("Connection failed");
```

### Service Locator

```csharp
using Garyon.Objects;

public class ServiceLocator : ISharedInstance
{
    private readonly Dictionary<Type, object> _services = new();
    private readonly object _lock = new();

    private ServiceLocator() { }

    public void Register<T>(T service) where T : class
    {
        lock (_lock)
        {
            _services[typeof(T)] = service;
        }
    }

    public T Resolve<T>() where T : class
    {
        lock (_lock)
        {
            if (_services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }

            throw new InvalidOperationException($"Service {typeof(T).Name} not registered");
        }
    }

    public bool TryResolve<T>(out T service) where T : class
    {
        lock (_lock)
        {
            if (_services.TryGetValue(typeof(T), out var obj))
            {
                service = (T)obj;
                return true;
            }

            service = null;
            return false;
        }
    }
}

// Usage
ServiceLocator.Shared.Register<IDataService>(new DataService());
var dataService = ServiceLocator.Shared.Resolve<IDataService>();
```

### Cache Manager

```csharp
using Garyon.Objects;

public class CacheManager : ISharedInstance
{
    private readonly Dictionary<string, CacheEntry> _cache = new();
    private readonly object _lock = new();

    private CacheManager()
    {
        // Start cleanup task
        _ = Task.Run(CleanupExpiredEntries);
    }

    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        var entry = new CacheEntry
        {
            Value = value,
            ExpiresAt = DateTime.UtcNow + expiration
        };

        lock (_lock)
        {
            _cache[key] = entry;
        }
    }

    public bool TryGet<T>(string key, out T value)
    {
        lock (_lock)
        {
            if (_cache.TryGetValue(key, out var entry))
            {
                if (DateTime.UtcNow < entry.ExpiresAt)
                {
                    value = (T)entry.Value;
                    return true;
                }

                _cache.Remove(key);
            }

            value = default;
            return false;
        }
    }

    private async Task CleanupExpiredEntries()
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromMinutes(5));

            lock (_lock)
            {
                var expired = _cache
                    .Where(kvp => DateTime.UtcNow >= kvp.Value.ExpiresAt)
                    .Select(kvp => kvp.Key)
                    .ToList();

                foreach (var key in expired)
                {
                    _cache.Remove(key);
                }
            }
        }
    }

    private class CacheEntry
    {
        public object Value { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}

// Usage
CacheManager.Shared.Set("user:123", userData, TimeSpan.FromMinutes(30));

if (CacheManager.Shared.TryGet<UserData>("user:123", out var cached))
{
    return cached;
}
```

### Feature Flags

```csharp
using Garyon.Objects;

public class FeatureFlags : ISharedInstance
{
    private readonly Dictionary<string, bool> _flags = new();

    private FeatureFlags()
    {
        LoadFlags();
    }

    public bool IsEnabled(string featureName)
    {
        return _flags.TryGetValue(featureName, out var enabled) && enabled;
    }

    public void Enable(string featureName)
    {
        _flags[featureName] = true;
    }

    public void Disable(string featureName)
    {
        _flags[featureName] = false;
    }

    private void LoadFlags()
    {
        // Load from configuration
        _flags["NewUI"] = false;
        _flags["BetaFeatures"] = false;
        _flags["AdvancedSearch"] = true;
    }
}

// Usage
if (FeatureFlags.Shared.IsEnabled("NewUI"))
{
    RenderNewUI();
}
else
{
    RenderLegacyUI();
}
```

## Singleton vs SharedInstance

### When to Use Singleton<T>

Use `Singleton<T>` when:
- You want explicit singleton pattern
- The type is not publicly accessible

```csharp
public class InternalService
{
    public static InternalService Instance => Singleton<InternalService>.Instance;
    private InternalService() { }
}
```

### When to Use ISharedInstance

Use `ISharedInstance` when:
- You want convention-based pattern
- The type can be publicly exposed for instantiation

```csharp
public class TestableService : ISharedInstance
{
    public TestableService() { }  // Public for testing
}

// Production
var service = TestableService.Shared;

// Testing
var testService = new TestableService();  // Create test instance
```

## Thread Safety

Both patterns are thread-safe for initialization:

```csharp
// Multiple threads accessing simultaneously
Parallel.For(0, 1000, i =>
{
    var instance = MySingleton.Instance;
    // Always the same instance, no race conditions
});
```

However, the singleton's members are not automatically thread-safe:

```csharp
public class ThreadSafeCounter : ISharedInstance
{
    private int _count;
    private readonly object _lock = new();

    public void Increment()
    {
        lock (_lock)
        {
            _count++;
        }
    }

    public int GetCount()
    {
        lock (_lock)
        {
            return _count;
        }
    }
}
```

## Testing Considerations

### Mocking Singletons

Singletons can be hard to test. Consider:

```csharp
// Instead of direct singleton usage
public class Service
{
    public void DoWork()
    {
        Logger.Shared.Log("Working");  // Hard to test
    }
}

// Use dependency injection
public class Service
{
    private readonly Logger _logger;

    public Service(Logger logger = null)
    {
        _logger = logger ?? Logger.Shared;
    }

    public void DoWork()
    {
        _logger.Log("Working");  // Can inject test logger
    }
}
```

### Resetting State

For testing, you might need to reset singleton state:

```csharp
public class TestableCache : ISharedInstance
{
    private Dictionary<string, object> _data = new();

    public void Set(string key, object value) => _data[key] = value;
    public object Get(string key) => _data[key];

    // For testing
    internal void Reset()
    {
        _data.Clear();
    }
}

// In tests
[TestCleanup]
public void Cleanup()
{
    // Reset singleton state between tests
    TestableCache.Shared.Reset();
}
```

## Best Practices

1. **Use sparingly**: Singletons introduce global state
2. **Thread safety**: Protect mutable state with locks
3. **Lazy initialization**: Use when appropriate
4. **Testability**: Consider dependency injection for better testability
5. **Documentation**: Document that type is a singleton
6. **Disposal**: If needed, implement IDisposable

## Performance

Both patterns have minimal overhead:
- **Singleton<T>**: Single field access
- **ISharedInstance**: Single property access via extension

```csharp
// Both compile to similar IL
var s1 = Singleton<MyType>.Instance;    // Field access
var s2 = MyType.Shared;                 // Extension property -> field access
```

## Compiler Requirements

> [!VERSION]
> **C# 14 required:** set your project `LangVersion` to C# 14 (or later) to use `.Shared`.

```xml
<PropertyGroup>
  <LangVersion>14.0</LangVersion>
</PropertyGroup>
```

## API Reference

See the following API references:
- [Singleton](../api/Garyon.Objects.Singleton-1.yml)
- [ISharedInstance](../api/Garyon.Objects.ISharedInstance.yml)
- [SharedInstanceExtensions](../api/Garyon.Objects.SharedInstanceExtensions.yml)

## Related

- [AppDomainCache](appdomain-cache.md)
- [Message Channels & Yielding](messaging-yielding.md)
- [Quick Start Guide](quick-start.md)
