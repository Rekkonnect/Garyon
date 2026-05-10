# Message Channels & Yielding

Garyon provides utilities for message-based communication and efficient value generation through `MessageRequestChannel`, `Yielder`, and related types.

## MessageRequestChannel

A message request channel allows signaling requests for a single operation using channels.

> **Framework Support**: Available only when `System.Threading.Channels` is supported (typically .NET Core 3.1+).

### Basic Usage

```csharp
using Garyon.Objects;
using System.Threading;
using System.Threading.Channels;

var channelOptions = new UnboundedChannelOptions
{
    SingleWriter = true,
    SingleReader = true
};

var channel = MessageRequestChannel.Create(channelOptions);

// Producer: Request an operation
await channel.WriteOne(CancellationToken.None);

// Consumer: Check and consume requests
if (channel.ConsumeAllRequests())
{
    // At least one request was pending
    await PerformRequestedOperation();
}
```

### Request Pattern

The channel is designed for scenarios where:
- Multiple requests should be coalesced into one
- Only the presence of a request matters, not the count
- Consumers process requests in batches

```csharp
// Drop mode: Additional writes while processing are dropped
var channel = MessageRequestChannel.Create(new UnboundedChannelOptions
{
    SingleWriter = false,
    SingleReader = true
});

// Multiple concurrent requests
await Task.WhenAll(
    channel.WriteOne(),
    channel.WriteOne(),
    channel.WriteOne()
);

// ConsumeAllRequests returns true if ANY request was pending
bool hadRequests = channel.ConsumeAllRequests();
// All requests consumed in one call
```

### Example: Refresh Trigger

```csharp
using Garyon.Objects;
using System.Threading;
using System.Threading.Tasks;

public class DataRefresher
{
    private readonly MessageRequestChannel _refreshChannel;
    private readonly CancellationTokenSource _cts = new();

    public DataRefresher()
    {
        _refreshChannel = MessageRequestChannel.Create(
            new UnboundedChannelOptions
            {
                SingleWriter = false,
                SingleReader = true
            }
        );

        _ = Task.Run(RefreshLoop);
    }

    public async Task RequestRefreshAsync()
    {
        await _refreshChannel.WriteOne(_cts.Token);
    }

    private async Task RefreshLoop()
    {
        while (!_cts.Token.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(1), _cts.Token);

            if (_refreshChannel.ConsumeAllRequests())
            {
                await PerformRefresh();
            }
        }
    }

    private async Task PerformRefresh()
    {
        Console.WriteLine("Refreshing data...");
        await Task.Delay(100); // Simulate work
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }
}
```

## Yielder

The `Yielder<T>` pattern provides mechanisms for convenient value generation from a factory function.

### Basic Usage

```csharp
using Garyon.Objects;

var random = new Random();
var yielder = Yielding.For(() => random.Next(100));

// Generate array
int[] numbers = yielder.YieldArray(10);

// Generate list
List<int> list = yielder.YieldList(20);

// Generate set (duplicates automatically handled)
HashSet<int> set = yielder.YieldSet(15);
```

### Factory Function

The factory is called for each value:

```csharp
int counter = 0;
var yielder = Yielding.For(() => counter++);

int[] sequence = yielder.YieldArray(5);
// sequence: [0, 1, 2, 3, 4]
```

### Collection Types

Generate different collection types:

```csharp
using Garyon.Objects;

var guidFactory = Yielding.For(() => Guid.NewGuid());

// Array
Guid[] guids = guidFactory.YieldArray(10);

// List
List<Guid> guidList = guidFactory.YieldList(10);

// HashSet
HashSet<Guid> guidSet = guidFactory.YieldSet(10);

// SortedSet
SortedSet<int> sortedNumbers = Yielding.For(() => random.Next(100))
    .YieldSortedSet(20);

// ImmutableArray (when available)
#if HAS_IMMUTABLE
ImmutableArray<int> immutable = yielder.YieldImmutableArray(10);
#endif
```

### Yielding Into Existing Collections

Add generated values to existing collections:

```csharp
var yielder = Yielding.For(() => random.Next());

var existingList = new List<int> { 1, 2, 3 };
yielder.YieldInto(5, existingList);
// existingList now has 8 elements (3 original + 5 generated)

var existingSet = new HashSet<int>();
yielder.YieldInto(10, existingSet);
// existingSet has up to 10 elements (duplicates ignored)
```

### Lazy Enumeration

Generate values without materializing:

```csharp
var yielder = Yielding.For(() => random.NextDouble());

// Lazy enumeration
IEnumerable<double> values = yielder.Yield(1000);

// Only generates values as enumerated
foreach (var value in values.Take(5))
{
    Console.WriteLine(value);  // Only 5 values generated
}
```

## SpanYielder

Similar to `Yielder` but works with spans for zero-allocation scenarios.

### Basic Usage

```csharp
using Garyon.Objects;

var yielder = new SpanYielder<int>(() => random.Next(100));

// Yield into a span
Span<int> buffer = stackalloc int[10];
yielder.YieldIntoSpan(buffer);
```

### Ref Struct Factory

For maximum performance with ref structs:

```csharp
var yielder = new SpanYielder<int>(static (ref int value) =>
{
    value = Random.Shared.Next(100);
});

Span<int> numbers = stackalloc int[20];
yielder.YieldIntoSpan(numbers);
```

## Examples

### Test Data Generation

```csharp
using Garyon.Objects;

public class TestDataGenerator
{
    private static readonly Random _random = new();

    public static List<User> GenerateUsers(int count)
    {
        var userYielder = Yielding.For(() => new User
        {
            Id = Guid.NewGuid(),
            Name = GenerateRandomName(),
            Age = _random.Next(18, 80),
            Email = $"user{_random.Next(10000)}@example.com"
        });

        return userYielder.YieldList(count);
    }

    private static string GenerateRandomName()
    {
        var names = new[] { "Alice", "Bob", "Charlie", "Diana", "Eve" };
        return names[_random.Next(names.Length)];
    }
}
```

### Unique ID Generation

```csharp
using Garyon.Objects;

public class IdGenerator
{
    private long _nextId = 1;
    private readonly object _lock = new();

    public List<long> GenerateIds(int count)
    {
        var yielder = Yielding.For(() =>
        {
            lock (_lock)
            {
                return _nextId++;
            }
        });

        return yielder.YieldList(count);
    }
}
```

### Random Sample Generator

```csharp
using Garyon.Objects;

public class SampleGenerator<T>
{
    private readonly List<T> _population;
    private readonly Random _random;

    public SampleGenerator(List<T> population)
    {
        _population = population;
        _random = new Random();
    }

    public HashSet<T> GenerateUniqueSample(int sampleSize)
    {
        if (sampleSize > _population.Count)
            throw new ArgumentException("Sample size exceeds population");

        var yielder = Yielding.For(() => 
            _population[_random.Next(_population.Count)]);

        // HashSet automatically handles uniqueness
        var sample = yielder.YieldSet(sampleSize * 2); // Generate extra to account for duplicates

        // Trim to desired size
        return sample.Take(sampleSize).ToHashSet();
    }
}
```

### Batch Processing with MessageRequestChannel

```csharp
using Garyon.Objects;
using System.Threading.Channels;

public class BatchProcessor<T>
{
    private readonly MessageRequestChannel _processChannel;
    private readonly Queue<T> _queue = new();
    private readonly object _queueLock = new();
    private readonly CancellationTokenSource _cts = new();

    public BatchProcessor()
    {
        _processChannel = MessageRequestChannel.Create(
            new UnboundedChannelOptions
            {
                SingleWriter = false,
                SingleReader = true
            }
        );

        _ = Task.Run(ProcessLoop);
    }

    public async Task AddItemAsync(T item)
    {
        lock (_queueLock)
        {
            _queue.Enqueue(item);
        }

        await _processChannel.WriteOne(_cts.Token);
    }

    private async Task ProcessLoop()
    {
        while (!_cts.Token.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(1), _cts.Token);

            if (_processChannel.ConsumeAllRequests())
            {
                List<T> batch;
                lock (_queueLock)
                {
                    batch = new List<T>(_queue);
                    _queue.Clear();
                }

                if (batch.Count > 0)
                {
                    await ProcessBatch(batch);
                }
            }
        }
    }

    private async Task ProcessBatch(List<T> items)
    {
        Console.WriteLine($"Processing batch of {items.Count} items");
        await Task.Delay(100); // Simulate work
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }
}
```

### Performance Test Data

```csharp
using Garyon.Objects;

public class PerformanceTestHelper
{
    public static List<int> GenerateSortedData(int count)
    {
        var yielder = Yielding.For(() => Random.Shared.Next(1000000));
        var data = yielder.YieldList(count);
        data.Sort();
        return data;
    }

    public static int[] GenerateRandomArray(int count, int min, int max)
    {
        var yielder = Yielding.For(() => Random.Shared.Next(min, max));
        return yielder.YieldArray(count);
    }

    public static HashSet<string> GenerateUniqueStrings(int count, int length = 10)
    {
        var yielder = Yielding.For(() => GenerateRandomString(length));
        return yielder.YieldSet(count * 2); // Generate extra for uniqueness
    }

    private static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var buffer = new char[length];
        for (int i = 0; i < length; i++)
        {
            buffer[i] = chars[Random.Shared.Next(chars.Length)];
        }
        return new string(buffer);
    }
}
```

## Best Practices

### Yielder

1. **Factory purity**: Keep factory functions simple and side-effect free when possible
2. **Set generation**: When using `YieldSet`, generate extra to account for duplicates
3. **Lazy vs. Materialized**: Use `Yield()` for lazy, or `YieldArray/List` for materialized
4. **Reuse yielder**: Create once, use multiple times with different counts

### MessageRequestChannel

1. **Single purpose**: Use for coalescing multiple requests into one operation
2. **Channel options**: Configure for your specific read/write patterns
3. **Consume pattern**: Always use `ConsumeAllRequests()` to clear all pending
4. **Cleanup**: Properly dispose of cancellation tokens

## Performance

### Yielder Performance

```csharp
// ❌ Traditional: Verbose and primitive
var list = new List<int>();
for (int i = 0; i < 1000; i++)
{
    list.Add(factory());
}

// ✅ Yielder: Convenient, concise and frictionless
var yielder = Yielding.For(factory);
var list = yielder.YieldList(1000);
```

### SpanYielder for Zero Allocation

```csharp
// ✅ Zero heap allocation
Span<int> buffer = stackalloc int[100];
var yielder = new SpanYielder<int>(() => Random.Shared.Next());
yielder.YieldIntoSpan(buffer);
```

## API Reference

See the following API references:
- [MessageRequestChannel](../api/Garyon.Objects.MessageRequestChannel.yml)
- [Yielder](../api/Garyon.Objects.Yielder-1.yml)
- [SpanYielder](../api/Garyon.Objects.SpanYielder-1.yml)

## Related

- [Enumerable Extensions](enumerable-extensions.md)
- [Collection Helpers](collection-helpers.md)
- [Task Handling](task-handling.md)
