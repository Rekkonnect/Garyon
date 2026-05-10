# AppDomainCache

`AppDomainCache` provides cached reflection information about `AppDomain` instances, significantly improving performance when repeatedly querying types within an application domain.

## Overview

Reflection operations are expensive, especially when scanning all types in an `AppDomain`. `AppDomainCache` provides a caching layer that:
- Scans types once and caches results
- Provides filtered views (classes, interfaces, structs, etc.)
- Improves performance for repeated type queries
- Manages cache lifecycle

> **Performance Warning**: Initial type scanning is expensive both computationally and memory-wise. Cache the instance and reuse it.

## Basic Usage

### Accessing Current AppDomain Cache

```csharp
using Garyon.Reflection;

// Get cache for current AppDomain
var cache = AppDomainCache.Current;

// Get all types (expensive first time, cached afterward)
var allTypes = cache.GetAllTypes();
```

### Creating Cache for Specific AppDomain

```csharp
using System;
using Garyon.Reflection;

var domain = AppDomain.CreateDomain("MyDomain");
var cache = new AppDomainCache(domain);
```

## Type Queries

### Get All Types

```csharp
using Garyon.Reflection;

var cache = AppDomainCache.Current;

// Get TypeListCache containing all types
var allTypes = cache.GetAllTypes();

// TypeListCache provides filtered access
foreach (var type in allTypes.AllTypes)
{
    Console.WriteLine(type.FullName);
}
```

### Get Specific Type Categories

```csharp
using Garyon.Reflection;

var cache = AppDomainCache.Current;

// Get all classes
var classes = cache.GetAllClasses();

// Get all abstract classes
var abstractClasses = cache.GetAllAbstractClasses();

// Get all non-abstract (concrete) classes
var concreteClasses = cache.GetAllNonAbstractClasses();

// Get all interfaces
var interfaces = cache.GetAllInterfaces();

// Get all structs
var structs = cache.GetAllStructs();

// Get all static classes
var staticClasses = cache.GetAllStaticClasses();
```

## Performance Optimization

### Lazy Loading

Types are loaded lazily—the scan doesn't occur until you first call a `Get*` method:

```csharp
var cache = AppDomainCache.Current; // No scanning yet

// First call triggers the scan (expensive)
var types = cache.GetAllTypes();

// Subsequent calls use cached data (fast)
var classes = cache.GetAllClasses();
var interfaces = cache.GetAllInterfaces();
```

### Cache Management

Clear the cache when needed:

```csharp
var cache = AppDomainCache.Current;

// Use cached types
var types1 = cache.GetAllTypes();

// Clear cache (e.g., after assembly load)
cache.EmptyAllTypesCache();

// Next call will re-scan
var types2 = cache.GetAllTypes();
```

## Examples

### Finding Types by Criteria

```csharp
using Garyon.Reflection;
using System.Linq;

public class TypeFinder
{
    private readonly AppDomainCache _cache;

    public TypeFinder()
    {
        _cache = AppDomainCache.Current;
    }

    public IEnumerable<Type> FindClassesInNamespace(string namespaceName)
    {
        return _cache.GetAllClasses()
            .Where(t => t.Namespace == namespaceName);
    }

    public IEnumerable<Type> FindTypesImplementing<TInterface>()
    {
        var interfaceType = typeof(TInterface);
        return _cache.GetAllTypes()
            .AllTypes
            .Where(t => interfaceType.IsAssignableFrom(t) && t != interfaceType);
    }

    public IEnumerable<Type> FindTypesByAttribute<TAttribute>() 
        where TAttribute : Attribute
    {
        return _cache.GetAllTypes()
            .AllTypes
            .Where(t => t.GetCustomAttributes(typeof(TAttribute), inherit: true).Any());
    }
}
```

### Plugin Discovery

```csharp
using Garyon.Reflection;
using System.Linq;

public interface IPlugin
{
    string Name { get; }
    void Initialize();
}

public class PluginLoader
{
    public List<IPlugin> DiscoverPlugins()
    {
        var cache = AppDomainCache.Current;

        // Find all non-abstract classes implementing IPlugin
        var pluginTypes = cache.GetAllNonAbstractClasses()
            .Where(t => typeof(IPlugin).IsAssignableFrom(t));

        // Instantiate plugins
        var plugins = new List<IPlugin>();
        foreach (var type in pluginTypes)
        {
            if (Activator.CreateInstance(type) is IPlugin plugin)
            {
                plugins.Add(plugin);
            }
        }

        return plugins;
    }
}
```

### Service Registration

```csharp
using Garyon.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

public class ServiceRegistrar
{
    public void RegisterServicesWithAttribute(IServiceCollection services)
    {
        var cache = AppDomainCache.Current;

        // Find all classes marked with [Service] attribute
        var serviceTypes = cache.GetAllNonAbstractClasses()
            .Where(t => t.GetCustomAttributes(typeof(ServiceAttribute), false).Any());

        foreach (var type in serviceTypes)
        {
            // Register with appropriate lifetime
            var attr = type.GetCustomAttribute<ServiceAttribute>();
            switch (attr.Lifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(type);
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped(type);
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient(type);
                    break;
            }
        }
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class ServiceAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; }

    public ServiceAttribute(ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        Lifetime = lifetime;
    }
}
```

### Type Statistics

```csharp
using Garyon.Reflection;

public class TypeStatistics
{
    public void PrintStatistics()
    {
        var cache = AppDomainCache.Current;

        var allTypes = cache.GetAllTypes();

        Console.WriteLine("Type Statistics:");
        Console.WriteLine($"Total Types: {allTypes.AllTypes.Count()}");
        Console.WriteLine($"Classes: {cache.GetAllClasses().Count()}");
        Console.WriteLine($"  Abstract: {cache.GetAllAbstractClasses().Count()}");
        Console.WriteLine($"  Concrete: {cache.GetAllNonAbstractClasses().Count()}");
        Console.WriteLine($"  Static: {cache.GetAllStaticClasses().Count()}");
        Console.WriteLine($"Interfaces: {cache.GetAllInterfaces().Count()}");
        Console.WriteLine($"Structs: {cache.GetAllStructs().Count()}");
    }
}
```

### Dynamic Assembly Loading

```csharp
using Garyon.Reflection;
using System.Reflection;

public class AssemblyManager
{
    private readonly AppDomainCache _cache;

    public AssemblyManager()
    {
        _cache = AppDomainCache.Current;
    }

    public void LoadAssemblyAndRefreshCache(string assemblyPath)
    {
        // Load new assembly
        Assembly.LoadFrom(assemblyPath);

        // Clear cache to include new types
        _cache.EmptyAllTypesCache();

        // Next query will include types from new assembly
        var updatedTypes = _cache.GetAllTypes();

        Console.WriteLine($"Total types after loading: {updatedTypes.AllTypes.Count()}");
    }
}
```

## TypeListCache

The `TypeListCache` class provides the filtered type collections:

```csharp
public class TypeListCache
{
    public IEnumerable<Type> AllTypes { get; }

    public IEnumerable<Type> GetClasses();
    public IEnumerable<Type> GetAbstractClasses();
    public IEnumerable<Type> GetNonAbstractClasses();
    public IEnumerable<Type> GetInterfaces();
    public IEnumerable<Type> GetStructs();
    public IEnumerable<Type> GetStaticClasses();
}
```

## Performance Characteristics

### Initial Scan
- **Time**: O(n) where n = total types in AppDomain
- **Space**: O(n) for cached type list
- **Warning**: Can take significant time and memory for large applications

### Subsequent Queries
- **Time**: O(1) to O(k) where k = types matching filter
- **Space**: No additional allocation beyond enumeration

### Best Practices

1. **Reuse Cache Instance**: Don't create multiple `AppDomainCache` instances
2. **Use Singleton Pattern**: Cache is perfect for singleton usage
3. **Clear Thoughtfully**: Only clear cache when assemblies are loaded/unloaded
4. **Filter Early**: Use specific `Get*` methods rather than filtering `AllTypes`

## Common Patterns

### Singleton Cache

```csharp
public class TypeRegistry
{
    private static readonly Lazy<AppDomainCache> _cacheInstance =
        new(() => AppDomainCache.Current);

    public static AppDomainCache Cache => _cacheInstance.Value;
}
```

### Filtered Type Provider

```csharp
public class TypeProvider
{
    private readonly AppDomainCache _cache = AppDomainCache.Current;

    public IEnumerable<Type> GetPublicClasses()
    {
        return _cache.GetAllClasses()
            .Where(t => t.IsPublic);
    }

    public IEnumerable<Type> GetTypesInAssembly(Assembly assembly)
    {
        return _cache.GetAllTypes()
            .AllTypes
            .Where(t => t.Assembly == assembly);
    }
}
```

## Related Utilities

Garyon provides additional reflection utilities:
- `TypeExtensions` - Extensions for `Type`
- `AssemblyExtensions` - Extensions for `Assembly`
- `TypePredicates` - Common type predicate functions
- `AppDomainHelpers` - AppDomain utility functions

## API Reference

See the [AppDomainCache API Reference](../api/Garyon.Reflection.AppDomainCache.yml) for complete documentation.

## Related

- [Singleton & SharedInstance](singleton-pattern.md)
- [Enumerable Extensions](enumerable-extensions.md)
- [Quick Start Guide](quick-start.md)
