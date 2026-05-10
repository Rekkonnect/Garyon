# DelegateHelpers

`DelegateHelpers` provides small helpers for invoking delegates while handling exceptions in a predictable way.

## Try(Action)

`Try(Action)` executes an action and returns whether it completed successfully. All exceptions are caught and suppressed.

```csharp
using Garyon.Functions;

if (!DelegateHelpers.Try(() => DoRiskyThing()))
{
    // Handle failure (logging, fallback, etc.)
}
```

## Try(Func<T>, defaultValue)

`Try<T>(Func<T>, T? defaultValue = default)` returns the function result, or `defaultValue` when an exception is thrown.

```csharp
using Garyon.Functions;

int value = DelegateHelpers.Try(() => int.Parse(userInput), defaultValue: -1) ?? -1;
```

## Notes

- These helpers suppress *all* exceptions. Prefer targeted handling when you can, and use these only where it is intentional to treat failures as "best effort".

## API Reference

- [DelegateHelpers](../api/Garyon.Functions.DelegateHelpers.yml)

