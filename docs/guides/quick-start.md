# Quick Start Guide

This guide will help you get started with Garyon's most commonly used features.

## Basic Usage

### Collection Extensions

Work with collections more efficiently:

For details, see: [Enumerable Extensions](enumerable-extensions.md) and [Dictionary & Collection Helpers](collection-helpers.md)

```csharp
using Garyon.Extensions;

var numbers = new[] { 1, 2, 3, 4, 5 };

// Get min and max in one operation
var (min, max) = numbers.MinMax();

// Check if collection has exactly N elements
bool hasThree = numbers.CountExactly(3); // false
bool hasFive = numbers.CountExactly(5);  // true

// Incremental dictionary operations
var counter = new Dictionary<string, int>();
counter.IncrementOrAddKeyValue("hello");
counter.IncrementOrAddKeyValue("hello");
// counter["hello"] is now 2
```

### Async Task Handling

Use the `.NoContext` extension for cleaner async code:

> [!VERSION]
> **C# 14 required:** `.NoContext` uses C# 14 extension members syntax.

For details, see: [Task Handling & .NoContext](task-handling.md)

```csharp
using Garyon.Extensions;

async Task DoWorkAsync()
{
    // Instead of: await task.ConfigureAwait(false);
    await SomeAsyncOperation().NoContext;

    // Works with Task<T> too
    var result = await GetResultAsync().NoContext;
}
```

### Singleton Pattern

Easily create singleton instances:

```csharp
using Garyon.Objects;

public class MyService
{
    // Traditional singleton pattern, simplified
    public static MyService Instance => Singleton<MyService>.Instance;

    private MyService() { }
}

// Or use SharedInstance pattern
public class Configuration : ISharedInstance
{
    public string Setting { get; set; }
}

// Access via extension
var config = Configuration.Shared; // Uses SharedInstanceExtensions
```

### Comparison Patterns

Use the fluent BeginCompare() pattern for multi-property comparisons:

For details, see: [Comparison Patterns](comparison-patterns.md)

```csharp
using Garyon.Extensions.Comparison;

public class Person : IComparable<Person>
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int Age { get; set; }

    public int CompareTo(Person other)
    {
        return this.BeginCompare(other)
            .By(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .ThenBy(p => p.Age)
            .Result;
    }
}
```

### Yielder Pattern

Generate collections efficiently:

For details, see: [Message Channels & Yielding](messaging-yielding.md)

```csharp
using Garyon.Objects;

var random = new Random();
var yielder = new Yielder<int>(() => random.Next(100));

// Generate array of 10 random numbers
int[] numbers = yielder.YieldArray(10);

// Generate list
List<int> list = yielder.YieldList(5);

// Generate HashSet (duplicates handled automatically)
HashSet<int> set = yielder.YieldSet(20);
```

## Next Steps

Explore detailed guides for specific features:

- [Task Handling & .NoContext](task-handling.md)
- [Enumerable Extensions](enumerable-extensions.md)
- [Math Utilities](math.md)
