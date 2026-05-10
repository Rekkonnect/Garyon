# NullGuards

`NullGuards` provides compact helpers for checking nullability across multiple values.

## AnyNull / NoneNull

Use `AnyNull(...)` when you want to short-circuit on a missing value, and `NoneNull(...)` when you prefer expressing the positive case.

```csharp
using Garyon.Functions;

if (NullGuards.AnyNull(firstName, lastName, email))
    return;

// Here, all values are known to be non-null.
SendWelcomeEmail(firstName, lastName, email);
```

`AnyNull(...)` is annotated so that when it returns `false`, the arguments are treated as not-null by the compiler. Likewise, `NoneNull(...)` is annotated so that when it returns `true`, the arguments are treated as not-null.

## AnyNonNull / SingleNull

Use `AnyNonNull(...)` when you only need at least one value present, and `SingleNull(...)` when you want to check for "exactly one missing".

```csharp
using Garyon.Functions;

bool hasAnyValue = NullGuards.AnyNonNull(a, b, c);
bool hasExactlyOneNull = NullGuards.SingleNull(left, right);
```

## API Reference

- [NullGuards](../api/Garyon.Functions.NullGuards.yml)

