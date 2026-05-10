# Dictionary & Collection Helpers

Garyon provides powerful extensions and helpers for dictionaries, collections, lists, and other data structures, making common operations more convenient and efficient.

## Dictionary Extensions

### IncrementOrAddKeyValue

Increment a counter in a dictionary, adding the key if it doesn't exist:

```csharp
using Garyon.Extensions;

var wordCount = new Dictionary<string, int>();

wordCount.IncrementOrAddKeyValue("hello");
wordCount.IncrementOrAddKeyValue("world");
wordCount.IncrementOrAddKeyValue("hello");

// wordCount: { "hello": 2, "world": 1 }
```

### ValueOrDefault

Get a value or return a default if the key doesn't exist:

```csharp
using Garyon.Extensions;

var config = new Dictionary<string, string>
{
    ["host"] = "localhost",
    ["port"] = "8080"
};

string host = config.ValueOrDefault("host");           // "localhost"
string timeout = config.ValueOrDefault("timeout");     // null
string maxRetries = config.ValueOrDefault("retries", "3");  // "3" (default)
```

### AddRange

Add multiple key-value pairs at once:

```csharp
using Garyon.Extensions;

var dict = new Dictionary<string, int>();

var newEntries = new Dictionary<string, int>
{
    ["one"] = 1,
    ["two"] = 2,
    ["three"] = 3
};

dict.AddRange(newEntries);
```

## List Extensions

### Swap Elements

```csharp
using Garyon.Extensions;

var list = new List<string> { "A", "B", "C", "D" };

list.Swap(0, 2);
// list: ["C", "B", "A", "D"]

list.Swap(^1, ^2);
// list: ["C", "B", "D", "A"]
```

## Collection Extensions

### IsEmpty / IsNotEmpty

```csharp
using Garyon.Extensions;

var numbers = new List<int>();

if (numbers.IsEmpty())
{
    Console.WriteLine("No numbers");
}

numbers.Add(42);

if (numbers.IsNotEmpty())
{
    Console.WriteLine($"Has {numbers.Count} numbers");
}
```

### AddIfNotNull

```csharp
using Garyon.Extensions;

var items = new List<string>();

string value1 = "hello";
string value2 = null;

items.AddIfNotNull(value1);  // Added
items.AddIfNotNull(value2);  // Not added

// items: ["hello"]
```

### Cloning

Clone collections efficiently:

```csharp
using Garyon.Extensions;

var original = new List<int> { 1, 2, 3 };
var clone = original.CloneList();

clone.Add(4);
// original: [1, 2, 3]
// clone: [1, 2, 3, 4]
```

## Specialized Collections

### FlexDictionary

A dictionary that creates values on first access:

```csharp
using Garyon.DataStructures;

// Auto-create lists when accessing new keys
var groups = new FlexDictionary<string, List<int>>(() => new List<int>(20));

groups["evens"].Add(2);
groups["evens"].Add(4);
groups["odds"].Add(1);
groups["odds"].Add(3);

// No need to check ContainsKey or initialize lists manually
```

### FlexInitDictionary

A dictionary that creates values on first access, using the parameterless
constructor if available:

```csharp
using Garyon.DataStructures;

// Auto-create lists when accessing new keys
var groups = new FlexInitDictionary<string, List<int>>();

groups["evens"].Add(2);
groups["evens"].Add(4);
groups["odds"].Add(1);
groups["odds"].Add(3);

// No need to check ContainsKey or initialize lists manually
```

### ValueCounterDictionary

Automatically count occurrences:

```csharp
using Garyon.Objects;

var counter = new ValueCounterDictionary<string>();

counter.Add("apple");
counter.Add("banana");
counter.Add("apple");
counter.Add("apple");

int appleCount = counter["apple"];  // 3
int bananaCount = counter["banana"]; // 1
```

### InterlinkedDictionary

Bidirectional key-value mapping:

```csharp
using Garyon.DataStructures;

var interlinked = new InterlinkedDictionary<int, string>();

interlinked.Add(1, "one");
interlinked.Add(2, "two");

string value = interlinked[1];           // "one"
int key = interlinked.GetKey("two");     // 2

// Both directions maintained automatically
```

### ConcurrentSet

Thread-safe set implementation:

```csharp
using Garyon.DataStructures;
using System.Threading.Tasks;

var set = new ConcurrentSet<int>();

Parallel.For(0, 1000, i =>
{
    set.Add(i % 100);  // Thread-safe
});

int uniqueCount = set.Count;  // Will be 100
```

## Queue and Stack Extensions

### Queue Extensions

```csharp
using Garyon.Extensions;

var queue = new Queue<int>();
queue.EnqueueRange(new[] { 1, 2, 3, 4, 5 });

// Dequeue multiple
var items = queue.DequeueRange(3);  // [1, 2, 3]
```

### Stack Extensions

```csharp
using Garyon.Extensions;

var stack = new Stack<string>();
stack.PushRange(new[] { "A", "B", "C" });

// Pop multiple
var items = stack.PopRange(2);  // ["C", "B"]
```

### StackSet and QueueSet

Sets with stack/queue ordering:

```csharp
using Garyon.DataStructures;

// Stack with unique elements
var stackSet = new StackSet<int>();
stackSet.Push(1);
stackSet.Push(2);
stackSet.Push(1);  // Ignored, already exists

// Queue with unique elements
var queueSet = new QueueSet<int>();
queueSet.Enqueue(1);
queueSet.Enqueue(2);
queueSet.Enqueue(1);  // Ignored
```

## Examples

### Word Frequency Counter

```csharp
using Garyon.Extensions;
using System.Text.RegularExpressions;

public class WordFrequency
{
    public static ValueCounterDictionary<string> CountWords(string text)
    {
        var frequency = new ValueCounterDictionary<string>();

        var words = Regex.Split(text.ToLower(), @"\W+")
            .Where(w => !string.IsNullOrWhiteSpace(w));

        foreach (var word in words)
        {
            frequency.Add(word);
        }

        return frequency;
    }

    public static void PrintTopWords(string text, int top = 10)
    {
        var frequency = CountWords(text);

        var topWords = frequency
            .OrderByDescending(kvp => kvp.Value)
            .Take(top);

        foreach (var (word, count) in topWords)
        {
            Console.WriteLine($"{word}: {count}");
        }
    }
}
```

### Grouping Helper

```csharp
using Garyon.Extensions;
using Garyon.DataStructures;

public class DataGrouper<T>
{
    private readonly FlexDictionary<string, List<T>> _groups;

    public DataGrouper()
    {
        _groups = new FlexDictionary<string, List<T>>(() => new List<T>());
    }

    public void Add(string group, T item)
    {
        _groups[group].Add(item);  // Auto-creates list if needed
    }

    public void AddRange(string group, IEnumerable<T> items)
    {
        _groups[group].AddRange(items);
    }

    public List<T> GetGroup(string group)
    {
        return _groups.ValueOrDefault(group) ?? new List<T>();
    }

    public Dictionary<string, int> GetGroupSizes()
    {
        return _groups.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.Count
        );
    }
}
```

## Performance Tips

1. **Pre-size collections**: Use capacity constructors when size is known
2. **Use appropriate collection**: Choose based on access patterns
3. **Avoid repeated lookups**: Cache dictionary lookups
4. **Batch operations**: Use AddRange instead of multiple Add calls
5. **Consider concurrent collections**: For multi-threaded scenarios

### Efficient Dictionary Usage

```csharp
// ❌ Multiple lookups
if (dict.ContainsKey(key))
{
    var value = dict[key];
    Process(value);
}

// ✅ Single lookup
if (dict.TryGetValue(key, out var value))
{
    Process(value);
}

// ✅ With Garyon extension
var value = dict.ValueOrDefault(key);
if (value != null)
{
    Process(value);
}
```

## Best Practices

1. **Use IncrementOrAddKeyValue**: For counting scenarios
2. **ValueOrDefault**: Safer than direct indexing
3. **Null checks**: Use AddIfNotNull for safety
4. **Thread safety**: Use ConcurrentSet/ConcurrentDictionary for parallel access
5. **Specialized collections**: Use FlexDictionary, ValueCounterDictionary for specific patterns

## API Reference

See the following API references:
- [IDictionaryExtensions](../api/Garyon.Extensions.IDictionaryExtensions.yml)
- [ICollectionExtensions](../api/Garyon.Extensions.ICollectionExtensions.yml)
- [IListExtensions](../api/Garyon.Extensions.IListExtensions.yml)
- [Data Structures](../api/Garyon.DataStructures.yml)

## Related

- [Enumerable Extensions](enumerable-extensions.md)
- [Math Utilities](math.md)
- [Message Channels & Yielding](messaging-yielding.md)
