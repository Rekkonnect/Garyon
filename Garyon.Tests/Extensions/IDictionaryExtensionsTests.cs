using Garyon.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class IDictionaryExtensionsTests
{
    [Test]
    public async Task IncrementOrAddKeyValueTest()
    {
        var dictionary = new Dictionary<string, int>
        {
            ["a"] = 1,
        };

        dictionary.IncrementOrAddKeyValue("a");
        dictionary.IncrementOrAddKeyValue("b");

        await Assert.That(dictionary["a"]).IsEqualTo(2);
        await Assert.That(dictionary["b"]).IsEqualTo(1);
    }

    [Test]
    public async Task ValueOrDefaultTest()
    {
        var dictionary = new Dictionary<string, int>
        {
            ["a"] = 5,
        };

        await Assert.That(dictionary.ValueOrDefault("a")).IsEqualTo(5);
        await Assert.That(dictionary.ValueOrDefault("b")).IsEqualTo(0);
        await Assert.That(dictionary.ValueOrDefault(null, 7)).IsEqualTo(7);
        await Assert.That(dictionary.ValueOrDefault("b", 7)).IsEqualTo(7);
    }

    [Test]
    public async Task AddOrSetTest()
    {
        var dictionary = new Dictionary<string, int>
        {
            ["a"] = 1,
        };

        var added = dictionary.AddOrSet("b", 2);
        var unchanged = dictionary.AddOrSet("a", 1);
        var overwritten = dictionary.AddOrSet("a", 3);

        await Assert.That(added).IsFalse();
        await Assert.That(unchanged).IsFalse();
        await Assert.That(overwritten).IsTrue();
        await Assert.That(dictionary["a"]).IsEqualTo(3);
        await Assert.That(dictionary["b"]).IsEqualTo(2);
    }

    [Test]
    public async Task TryAddPreserveTest()
    {
        var dictionary = new Dictionary<string, int>
        {
            ["a"] = 1,
        };

        var added = dictionary.TryAddPreserve("b", 2, out var missingExisting);
        var preservedEqual = dictionary.TryAddPreserve("a", 1, out var equalExisting);
        var preservedDifferent = dictionary.TryAddPreserve("a", 3, out var differentExisting);

        await Assert.That(added).IsTrue();
        await Assert.That(preservedEqual).IsTrue();
        await Assert.That(preservedDifferent).IsFalse();
        await Assert.That(missingExisting).IsEqualTo(0);
        await Assert.That(equalExisting).IsEqualTo(1);
        await Assert.That(differentExisting).IsEqualTo(1);
        await Assert.That(dictionary["a"]).IsEqualTo(1);
        await Assert.That(dictionary["b"]).IsEqualTo(2);
    }

    [Test]
    public async Task RangeMutationHelpersTest()
    {
        var dictionary = new Dictionary<string, int>
        {
            ["a"] = 1,
        };

        dictionary.AddRange(new Dictionary<string, int>
        {
            ["b"] = 2,
        });

        var overwritten = dictionary.AddOrSetRange(new Dictionary<string, int>
        {
            ["a"] = 3,
            ["c"] = 4,
        });

        var preserved = dictionary.TryAddPreserveRange(new Dictionary<string, int>
        {
            ["a"] = 9,
            ["d"] = 5,
        });

        await Assert.That(overwritten).IsTrue();
        await Assert.That(preserved).IsFalse();
        await Assert.That(dictionary["a"]).IsEqualTo(3);
        await Assert.That(dictionary["b"]).IsEqualTo(2);
        await Assert.That(dictionary["c"]).IsEqualTo(4);
        await Assert.That(dictionary["d"]).IsEqualTo(5);
    }

    [Test]
    public async Task GetKeyValuePairAndTryAddKvpTest()
    {
        var dictionary = new Dictionary<string, int>();
        var firstAdded = dictionary.TryAdd(new KeyValuePair<string, int>("a", 1));
        var secondAdded = dictionary.TryAdd(new KeyValuePair<string, int>("a", 2));
        var pair = dictionary.GetKeyValuePair("a");

        await Assert.That(firstAdded).IsTrue();
        await Assert.That(secondAdded).IsFalse();
        await Assert.That(pair.Key).IsEqualTo("a");
        await Assert.That(pair.Value).IsEqualTo(1);
    }

    [Test]
    public async Task TransformHelpersTest()
    {
        IReadOnlyDictionary<string, int> dictionary = new Dictionary<string, int>
        {
            ["ab"] = 1,
            ["cd"] = 2,
        };

        var transformedKeys = dictionary.TransformKeys(k => k.Length);
        var transformedValues = dictionary.TransformValues(v => $"#{v}");

        await Assert.That(transformedKeys[2]).IsEqualTo(2);
        await Assert.That(transformedValues["ab"]).IsEqualTo("#1");
        await Assert.That(transformedValues["cd"]).IsEqualTo("#2");
        await Assert.That(transformedValues.Keys.SequenceEqual(["ab", "cd"])).IsTrue();
    }
}
