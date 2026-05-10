# AdvancedLazy

`AdvancedLazy<T>` is a small helper for caching a value that is computed on-demand, with the extra ability to clear the cached value and recompute it later.

## When To Use It

- You want `Lazy<T>`-style value initialization, but you also need to invalidate the cached value (`ClearValue()`).
- You want a simple "cache once" primitive without bringing in a full caching abstraction.

## Basic Usage

```csharp
using Garyon.Objects.Advanced;

var lazy = new AdvancedLazy<int>(() => ExpensiveCalculation());

// Triggers factory on first access:
var value1 = lazy.Value;

// Reuses cached value:
var value2 = lazy.Value;

lazy.ClearValue();

// Factory runs again after invalidation:
 var value3 = lazy.Value;
 ```

## Async Values

`AdvancedLazy<T>` is intentionally synchronous: if your factory is synchronous, `AdvancedLazy<T>` is a good fit.

If the value must be created asynchronously, use `AsyncLazy<T>` instead.

## API Reference

- [AdvancedLazy](../api/Garyon.Objects.Advanced.AdvancedLazy-1.yml)
- [AsyncLazy](../api/Garyon.Objects.Advanced.AsyncLazy-1.yml)
