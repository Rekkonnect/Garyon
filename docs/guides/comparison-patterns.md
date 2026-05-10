# Comparison Patterns

Garyon exposes a small comparison surface that covers two distinct jobs:

- fluent multi-key ordering with `BeginCompare()`
- direct comparison and extremum helpers on `IComparable` values

## Fluent Comparison Chains

`BeginCompare()` is the entry point for multi-property comparisons. Each step stops as soon as a non-equal result is found, so later selectors are only evaluated when they are needed.

```csharp
using Garyon.Extensions.Comparison;

public sealed class Person : IComparable<Person>
{
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public int Age { get; set; }

    public int CompareTo(Person? other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return this.BeginCompare(other)
            .By(person => person.LastName)
            .ThenBy(person => person.FirstName)
            .ThenBy(person => person.Age)
            .Result;
    }
}
```

### Descending Steps

When a later key should be ordered descendingly, use `ByDesc()` or `ThenByDesc()` instead of swapping operands manually.

```csharp
using Garyon.Extensions.Comparison;

public sealed class Employee : IComparable<Employee>
{
    public string Department { get; set; } = string.Empty;
    public int Level { get; set; }
    public decimal Salary { get; set; }
    public string Name { get; set; } = string.Empty;

    public int CompareTo(Employee? other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return this.BeginCompare(other)
            .By(employee => employee.Department)
            .ThenByDesc(employee => employee.Level)
            .ThenByDesc(employee => employee.Salary)
            .ThenBy(employee => employee.Name)
            .Result;
    }
}
```

### Async Steps (`ByAsync`, `ThenByAsync`, …)

`ComparisonSource<T>` and `ComparisonInfo<T>` provide async selector support in two styles:

- **Overloads on `By` / `ThenBy` / `ByDesc` / `ThenByDesc`**: if your selector returns `Task<TResult>` or `ValueTask<TResult>`, the chain step returns `ValueTask<ComparisonInfo<T>>`.
- **Explicit `ByAsync` / `ThenByAsync` / `ByDescAsync` / `ThenByDescAsync`**: these keep the fluent chain intact by returning an awaitable `AsyncComparisonInfo<T>`.

The explicit `*Async` methods are useful when you want to mix sync and async steps and only `await` once at the end:

```csharp
using Garyon.Extensions.Comparison;

public static async ValueTask<int> CompareAsync(Person left, Person right)
{
    return await left.BeginCompare(right)
        .By(person => person.LastName)
        .ThenByAsync(person => FetchRankingAsync(person)) // Task<int> or ValueTask<int>
        .ThenBy(person => person.FirstName);
}
```

If you prefer working with the raw `ValueTask<ComparisonInfo<T>>` (for example to inspect intermediate state), you can still use the overload-based approach:

```csharp
using Garyon.Extensions.Comparison;

public static async ValueTask<int> CompareAsync(Person left, Person right)
{
    var comparison = left.BeginCompare(right)
        .By(person => person.LastName);

    comparison = await comparison.ThenBy(person => FetchRankingAsync(person));

    return comparison
        .ThenBy(person => person.FirstName)
        .Result;
}
```

> [!WARNING]
> When introducing async comparison steps in the chain, ensure the following are met:
> - An async-retrieved value must be never recomputed in batch comparisons, such as ordering an array
> - Use batch-loading wherever possible to avoid having to compare against async-provided values anyway, by having stored them before the comparison.
> In most real-world cases, there must be a way to retrieve all those values in parallel/bulk without invoking async methods once per item and property

### Inspecting Intermediate State

Each chain step returns `ComparisonInfo<T>`, which exposes the current result together with convenience flags.

```csharp
var comparison = left.BeginCompare(right)
    .By(value => value.LastName);

if (comparison.AreEqual)
{
    comparison = comparison.ThenBy(value => value.FirstName);
}

if (comparison.AreDifferent)
{
    return comparison.Result;
}

return comparison.ThenByDesc(value => value.Age).Result;
```

## Working With `ComparisonSource<T>` Directly

`BeginCompare()` is usually the right entry point, but `ComparisonSource<T>` is available when you already have the left and right values and want to compare either the values themselves or a selector projection.

```csharp
using Garyon.Extensions.Comparison;

var source = new ComparisonSource<int>(5, 10);

var selfComparison = source.Self();
var descendingComparison = source.ByDesc(value => value);

Console.WriteLine(selfComparison.Result);       // -1
Console.WriteLine(descendingComparison.Result); // 1
```

## `IComparable` Helpers

Garyon also exposes direct helpers on `IComparable` and `IComparable<T>` so call sites do not need to work with raw `CompareTo()` values.

```csharp
using Garyon.Extensions;

int left = 5;
int right = 10;

bool isLess = left.LessThan(right);
bool isLessOrEqual = left.LessThanOrEqual(right);
bool isGreater = left.GreaterThan(right);
bool isEqual = left.EqualTo(right);
ComparisonResult result = left.GetComparisonResult(right);
```

When comparison behavior is driven by data, use `ComparisonKinds` or match against a concrete `ComparisonResult`.

```csharp
using Garyon.Extensions;
using Garyon.Objects;

bool accepted = score.SatisfiesComparison(70, ComparisonKinds.GreaterOrEqual);
bool exactMatch = score.MatchesComparisonResult(100, ComparisonResult.Equal);
```

## Tracking Extremes While Iterating

Garyon uses assignment-oriented extremum helpers. The intended APIs are `AssignMin()`, `AssignMax()`, and `AssignExtremum()`.

```csharp
using Garyon.Extensions;

var values = new[] { 12, 4, 19, 7, 3 };

int min = values[0];
int max = values[0];

foreach (var value in values[1..])
{
    min.AssignMin(value);
    max.AssignMax(value);
}

Console.WriteLine($"{min}, {max}");
```

Use `AssignExtremum()` when the target comparison is decided elsewhere.

```csharp
using Garyon.Extensions;
using Garyon.Objects;

int extremum = 10;
extremum.AssignExtremum(20, ComparisonResult.Greater);
```

## Notes

- `BeginCompare()` is the normal choice inside `IComparable<T>.CompareTo` implementations.
- `ByDesc()` and `ThenByDesc()` keep descending keys inside the same fluent chain instead of forcing operand swaps.
- `AssignMin()` and `AssignMax()` are the concrete min/max helpers; there are no separate `AssignIfLess` or `AssignIfGreater` APIs.

## API Reference

- [ComparisonExtensions](../api/Garyon.Extensions.Comparison.ComparisonExtensions.yml)
- [ComparisonSource](../api/Garyon.Extensions.Comparison.ComparisonSource-1.yml)
- [ComparisonInfo](../api/Garyon.Extensions.Comparison.ComparisonInfo-1.yml)
- [IComparableExtensions](../api/Garyon.Extensions.IComparableExtensions.yml)

## Related

- [Enumerable Extensions](enumerable-extensions.md)
- [Collection Helpers](collection-helpers.md)
- [Quick Start Guide](quick-start.md)
