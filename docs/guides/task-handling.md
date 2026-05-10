# Task Handling & .NoContext Extensions

Garyon provides convenient extensions for async/await patterns, most notably the `.NoContext` extension that simplifies the common `ConfigureAwait(false)` pattern.

> [!VERSION]
> **C# 14 required:** these extensions use C# 14 extension members syntax to add properties directly to `Task` and `ValueTask` types.

## Overview

When writing library code or performance-critical async code, it's a best practice to use `ConfigureAwait(false)` to avoid capturing the synchronization context. Garyon makes this pattern more concise and readable.

## The .NoContext Extension

### Basic Usage

Instead of writing:
```csharp
await task.ConfigureAwait(false);
```

You can write:
```csharp
using Garyon.Extensions;

await task.NoContext;
```

### Supported Types

The `.NoContext` extension works with all common task types:

```csharp
using Garyon.Extensions;
using System.Threading.Tasks;

// Task
async Task DoWorkAsync(Task operation)
{
    await operation.NoContext;
}

// Task<T>
async Task<int> GetResultAsync(Task<int> value)
{
    return await value.NoContext;
}

// ValueTask
async ValueTask ProcessAsync(ValueTask process)
{
    await process.NoContext;
}

// ValueTask<T>
async ValueTask<string> GetDataAsync(ValueTask<string> fetchData)
{
    return await fetchData.NoContext;
}
```

## Using .NoContext

Using .NoContext follows the same guidelines as using `.ConfigureAwait(false)`,
often abbreviated as CAF. More information about CAF and when to use it
[here](https://devblogs.microsoft.com/dotnet/configureawait-faq/).

In short, here is a summary of the guidelines:

### Best Practices

Use `.NoContext` in:
- **Library code**: Libraries should not capture synchronization contexts
- **ASP.NET Core**: Modern ASP.NET Core doesn't use SynchronizationContext
- **Performance-critical paths**: Reduce overhead in hot paths
- **Background processing**: When UI context is not needed

### When NOT to Use

Avoid `.NoContext` when:
- You need to return to the original context (e.g., updating UI from UI thread)
- Working with legacy WinForms or WPF applications without proper async patterns
- The calling code expects synchronization context preservation

## Examples

### Library Method Pattern

```csharp
using Garyon.Extensions;
using System.Net.Http;
using System.Threading.Tasks;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetDataAsync(string url)
    {
        // Library methods should use .NoContext
        var response = await _httpClient.GetAsync(url).NoContext;
        response.EnsureSuccessStatusCode();

        string content = await response.Content.ReadAsStringAsync().NoContext;
        return content;
    }

    public async Task<T> GetJsonAsync<T>(string url)
    {
        string json = await GetDataAsync(url).NoContext;
        return System.Text.Json.JsonSerializer.Deserialize<T>(json);
    }
}
```

### Chaining Async Operations

```csharp
using Garyon.Extensions;
using System.Threading.Tasks;

public class DataProcessor
{
    public async Task<ProcessedData> ProcessAsync(string input)
    {
        // Chain multiple async operations with .NoContext
        var validated = await ValidateAsync(input).NoContext;
        var transformed = await TransformAsync(validated).NoContext;
        var enriched = await EnrichAsync(transformed).NoContext;
        var result = await FinalizeAsync(enriched).NoContext;

        return result;
    }

    private async Task<string> ValidateAsync(string input)
    {
        await Task.Delay(10).NoContext;
        return input;
    }

    private async Task<string> TransformAsync(string input)
    {
        await Task.Delay(10).NoContext;
        return input.ToUpper();
    }

    private async Task<string> EnrichAsync(string input)
    {
        await Task.Delay(10).NoContext;
        return $"[{input}]";
    }

    private async Task<ProcessedData> FinalizeAsync(string input)
    {
        await Task.Delay(10).NoContext;
        return new ProcessedData { Value = input };
    }
}

public class ProcessedData
{
    public string Value { get; set; }
}
```

### ValueTask Usage

```csharp
using Garyon.Extensions;
using System.Threading.Tasks;

public class CachingService<T>
{
    private readonly Dictionary<string, T> _cache = new();

    public ValueTask<T> GetOrCreateAsync(string key, Func<ValueTask<T>> factory)
    {
        // Hot path: return cached value synchronously
        if (_cache.TryGetValue(key, out var cached))
        {
            return new ValueTask<T>(cached);
        }

        // Cold path: create and cache
        return CreateAndCacheAsync(key, factory);
    }

    private async ValueTask<T> CreateAndCacheAsync(string key, Func<ValueTask<T>> factory)
    {
        var value = await factory().NoContext;
        _cache[key] = value;
        return value;
    }
}
```

> [!WARNING]
> Notice that in the above example, the SDK-provided Roslyn analyzers may report
> [CA2012](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca2012).
> This diagnostic is a false positive, since `NoContext` is not a known API that
> is safe to use in this context. Replacing `NoContext` with `ConfigureAwait(false)`
> gets the diagnostic away, and as such `NoContext` is safe to use as it's equivalent.

## Implementation Details

The `.NoContext` extensions are defined as extension members (C# 14):

```csharp
public static class ConfigureAwaitFalseExtensions
{
    extension(Task task)
    {
        public ConfiguredTaskAwaitable NoContext => task.ConfigureAwait(false);
    }

    extension<T>(Task<T> task)
    {
        public ConfiguredTaskAwaitable<T> NoContext => task.ConfigureAwait(false);
    }

    extension(ValueTask task)
    {
        public ConfiguredValueTaskAwaitable NoContext => task.ConfigureAwait(false);
    }

    extension<T>(ValueTask<T> task)
    {
        public ConfiguredValueTaskAwaitable<T> NoContext => task.ConfigureAwait(false);
    }
}
```

This syntax allows the property to appear as if it's a member of the `Task` type itself, providing a more intuitive API.

## Compiler Requirements

> [!VERSION]
> **C# 14 required:** set your project `LangVersion` to C# 14 (or later) to use `.NoContext`.

```xml
<PropertyGroup>
  <LangVersion>14.0</LangVersion>
</PropertyGroup>
```

## Performance Comparison

```csharp
// Traditional approach
await task.ConfigureAwait(false);  // Overhead: method call

// .NoContext approach  
await task.NoContext;              // Overhead: property access (optimized by compiler)
```

Both approaches compile to identical IL code, so there's no performance difference—only readability improvement.

## Best Practices

1. **Use consistently**: Apply `.NoContext` to all awaits in a method or none
2. **Library code**: Always use in library/framework code
3. **Document behavior**: Make it clear when methods preserve/don't preserve context
4. **Combine with cancellation**: Works seamlessly with `CancellationToken`

```csharp
public async Task<Result> ProcessAsync(CancellationToken cancellationToken)
{
    await Task.Delay(1000, cancellationToken).NoContext;
    return new Result();
}
```

## Related Extensions

### TaskAwaiting

Garyon also provides other task-related extensions in `TaskAwaiting`:

- Additional task utilities
- Task combination helpers
- Timeout extensions

## API Reference

See the [ConfigureAwaitFalseExtensions API Reference](../api/Garyon.Extensions.ConfigureAwaitFalseExtensions.yml) for complete documentation.

## Related

- [Process Extensions](process-extensions.md)
- [Enumerable Extensions](enumerable-extensions.md)
- [Quick Start Guide](quick-start.md)
