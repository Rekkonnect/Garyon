# AsyncLazy

`AsyncLazy<T>` is a small helper for caching a value that is computed on-demand **asynchronously**, with the extra ability to clear the cached value and recompute it later.

## When To Use It

- You want to lazily create a value via an async factory (`Func<Task<T>>`).
- You want a simple "compute once, then cache" primitive for async values.
- You want to invalidate the cached value and recompute it later (`ClearValue()`).

## Basic Usage

```csharp
using Garyon.Objects.Advanced;

var lazy = new AsyncLazy<int>(async () =>
{
    await Task.Delay(10);
    return 42;
});

// Triggers factory on first call:
var value1 = await lazy.GetValueAsync();

// Reuses cached task/value:
var value2 = await lazy.GetValueAsync();

lazy.ClearValue();

// Factory runs again after invalidation:
var value3 = await lazy.GetValueAsync();
```

## Notes

- `IsValueCreated` indicates whether the factory has been invoked.
- `ValueTaskOrDefault` exposes the cached task (or `null` before first use).

## API Reference

- [AsyncLazy](../api/Garyon.Objects.Advanced.AsyncLazy-1.yml)

