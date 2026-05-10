# Enumerable & Enumerator Extensions

Garyon provides extensive extensions for working with `IEnumerable<T>`, `IEnumerator<T>`, and related collection interfaces, offering powerful LINQ-like operations and advanced enumeration patterns.

## Overview

The enumerable extensions include:
- **IEnumerableExtensions** - Core enumerable operations
- **IEnumeratorExtensions** - Enumerator manipulation
- **Specialized enumerators** - Custom enumeration patterns
- **Performance-optimized helpers** - Count operations, MinMax, etc.
- **Collection transformation** - Flattening, cartesian products, etc.

## MinMax Operations

Get both minimum and maximum values in a single pass:

```csharp
using Garyon.Extensions;

var numbers = new[] { 5, 2, 8, 1, 9, 3 };

// Get both min and max efficiently
var (min, max) = numbers.MinMax();
// min = 1, max = 9

// Works with custom selectors
var people = new[]
{
    new Person { Name = "Alice", Age = 30 },
    new Person { Name = "Bob", Age = 25 },
    new Person { Name = "Charlie", Age = 35 }
};

var (youngest, oldest) = people.MinMax(p => p.Age);
// youngest = 25, oldest = 35
```

### INumber Support

For numeric types (and more generally for types participating in .NET's generic math), Garyon provides a `MinMax()` overload that leverages generic math constraints when available:

```csharp
using Garyon.Extensions;

var values = new[] { 1, 2, 3, 4, 5 };
var (min, max) = values.MinMax();
```

## Count Operations

These helpers are designed for count-related checks that can short-circuit without fully enumerating sequences.

```csharp
using Garyon.Extensions;

var numbers = new[] { 1, 2, 3, 4, 5 };

bool atLeastThree = numbers.CountAtLeast(3);          // true
bool atMostTen = numbers.CountAtMost(10);             // true
bool exactlyFive = numbers.CountExactly(5);           // true
bool betweenTwoAndFour = numbers.CountBetween(2, 4);  // false

bool atLeastTwoEvens = numbers.CountAtLeast(n => n % 2 == 0, 2);        // true
bool oneOrTwoEvens = numbers.CountBetween(n => n % 2 == 0, 1, 2);       // true
```

## Nested Enumerables

### Flattening

Flatten nested collections:

```csharp
using Garyon.Extensions;

// 2D flattening
var nested2D = new[]
{
    new[] { 1, 2, 3 },
    new[] { 4, 5, 6 },
    new[] { 7, 8, 9 }
};

var flattened = nested2D.Flatten();
// Result: [1, 2, 3, 4, 5, 6, 7, 8, 9]

// 3D flattening
var nested3D = new[]
{
    new[] 
    { 
        new[] { 1, 2 }, 
        new[] { 3, 4 } 
    },
    new[] 
    { 
        new[] { 5, 6 }, 
        new[] { 7, 8 } 
    }
};

var flattened3D = nested3D.Flatten3D();
// Result: [1, 2, 3, 4, 5, 6, 7, 8]
```

## Cartesian Products

Generate cartesian products of collections:

```csharp
using Garyon.Extensions;

var colors = new[] { "Red", "Blue" };
var sizes = new[] { "S", "M", "L" };

var products = colors.CartesianProduct(sizes);
// Result: [(Red, S), (Red, M), (Red, L), (Blue, S), (Blue, M), (Blue, L)]

// Works with multiple collections
var styles = new[] { "Solid", "Striped" };
var allCombinations = colors.CartesianProduct(sizes, styles);
```

## Iterative Operations

### ForEach with Index

```csharp
using Garyon.Extensions;

var items = new[] { "apple", "banana", "cherry" };

items.ForEach((item, index) =>
{
    Console.WriteLine($"{index}: {item}");
});
// Output:
// 0: apple
// 1: banana
// 2: cherry
```

### Indexed Enumeration

```csharp
using Garyon.Extensions;

var items = new[] { "A", "B", "C" };

foreach (var (item, index) in items.Indexed())
{
    Console.WriteLine($"[{index}] = {item}");
}
```

## Advanced Enumerators

### ParallellyEnumerable

Enumerate multiple collections in parallel:

```csharp
using Garyon.Objects.Enumerators;

var list1 = new[] { 1, 2, 3 };
var list2 = new[] { "A", "B", "C" };

foreach (var (num, letter) in (list1, list2).AsParallellyEnumerable())
{
    Console.WriteLine($"{num}: {letter}");
}
// Output:
// 1: A
// 2: B
// 3: C
```

### SingleOrEnumerable

Handle both single values and collections uniformly:

```csharp
using Garyon.Objects.Enumerators;

// Can hold a single value
var single = SingleOrEnumerable<int>.Single(42);

// Or an enumerable
var multiple = SingleOrEnumerable<int>.Enumerable(new[] { 1, 2, 3 });

// Enumerate either
foreach (var item in single)
{
    Console.WriteLine(item);  // 42
}

foreach (var item in multiple)
{
    Console.WriteLine(item);  // 1, 2, 3
}
```

## LINQ-Like Extensions

### DistinctBy (for older frameworks)

```csharp
using Garyon.Extensions;

var people = new[]
{
    new Person { Name = "Alice", Age = 30 },
    new Person { Name = "Bob", Age = 30 },
    new Person { Name = "Charlie", Age = 25 }
};

var distinctAges = people.DistinctBy(p => p.Age);
// Returns: Alice and Charlie (distinct ages)
```

### WhereNot

```csharp
using Garyon.Extensions;

var numbers = new[] { 1, 2, 3, 4, 5, 6 };

var notEven = numbers.WhereNot(n => n % 2 == 0);
// Result: [1, 3, 5]
```

### Enumerable with Count Caching

```csharp
using Garyon.Extensions;

object[] mixed = { 1, "hello", 2, "world", 3 };

var strings = mixed.OfType<string>().WithCountCaching();
int count = strings.ForceCount();
```

## Examples

### Early-Terminating Enumeration

Use `CutAt` to stop enumerating once a predicate becomes false (similar to `TakeWhile`, with an option to include the first element that breaks the predicate):

```csharp
using Garyon.Extensions;

var values = Enumerable.Range(1, 100);

var small = values.CutAt(i => i >= 10).ToArray(); // [1..9]
```

## Performance Considerations

### Early Termination

Many extensions support early termination and unnecessary enumeration:

```csharp
using Garyon.Extensions;

var hasEactlyTenPrimes = Enumerable.Range(1, 1000000)
    .Where(IsPrime)
    .CountExactly(10); // Enumerates 11 elements before returning false

bool IsPrime()
{
    // Complex function that evaluates primality
}
```

## Best Practices

1. **Use MinMax**: Get both min and max in one pass
2. **Short-circuit counting**: Use `CountAtLeast` / `CountAtMost` / `CountExactly` instead of `.Count() == n`
3. **Cache wisely**: Use `WithCountCaching` to avoid repeated enumerations for count
4. **Avoid multiple enumeration**: Materialize with `ToList()` if needed
5. **Choose right tool**: Use appropriate extension for the job

## API Reference

See the following API references:
- [IEnumerableExtensions](../api/Garyon.Extensions.IEnumerableExtensions.yml)
- [IEnumeratorExtensions](../api/Garyon.Extensions.IEnumeratorExtensions.yml)
- [Enumerator Helpers](../api/Garyon.Objects.Enumerators.yml)

## Related

- [Collection Helpers](collection-helpers.md)
- [Math Utilities](math.md)
- [Message Channels & Yielding](messaging-yielding.md)
