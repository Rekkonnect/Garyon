using Garyon.DataStructures;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.DataStructures;

public class FlexibleDictionaryTests
{
    private static FlexDictionary<int, char> testDictionary = new(defaultValue: default);

    [Before(HookType.Class)]
    public static void InitializeTestDictionary()
    {
        for (int i = 0; i < 5; i++)
            testDictionary.Add(i, (char)('a' + i));
    }

    [Test]
    public async Task InitializationTest()
    {
        var d = new FlexDictionary<int, string>(defaultValue: default)
        {
            [1] = "a"
        };
        await Assert.That(d[1]).IsEqualTo("a");
        await Assert.That(d.Count).IsEqualTo(1);
    }
    [Test]
    public async Task EnumerableInitializationTest()
    {
        var values = new[] { 1, 4, 1, 6, 2, 2 };
        var uniqueValues = new[] { 1, 2, 4, 6 };
        var d = new FlexDictionary<int, string>(values, initialValue: default);
        foreach (var v in uniqueValues)
            await Assert.That(d[v]).IsNull();
        await Assert.That(d.Count).IsEqualTo(4);
    }
    [Test]
    public async Task CloneTest()
    {
        var cloned = testDictionary.Clone();
        for (int i = 0; i < 5; i++)
            await Assert.That(cloned[i]).IsEqualTo((char)('a' + i));
        await Assert.That(cloned.Count).IsEqualTo(testDictionary.Count);

        cloned.Add(10, 'f');
        await Assert.That(testDictionary.ContainsKey(10)).IsFalse();
        await Assert.That(cloned.ContainsKey(10)).IsTrue();
    }
    [Test]
    public async Task ClearTest()
    {
        var cloned = testDictionary.Clone();
        cloned.Clear();
        foreach (var kvp in testDictionary)
            await Assert.That(cloned.ContainsKey(kvp.Key)).IsFalse();
        await Assert.That(cloned).IsEmpty();
    }
    [Test]
    public async Task RemoveTest()
    {
        var cloned = testDictionary.Clone();
        cloned.Remove(1);
        foreach (var kvp in testDictionary)
            await Assert.That(cloned.ContainsKey(kvp.Key)).IsEqualTo(kvp.Key != 1);
        await Assert.That(cloned.Count).IsEqualTo(testDictionary.Count - 1);
    }
    [Test]
    public async Task RemoveKeysTest()
    {
        var cloned = testDictionary.Clone();
        int removed = cloned.RemoveKeys(1, 2, 43);

        await Assert.That(removed).IsEqualTo(2);
        await Assert.That(cloned.ContainsKey(0)).IsTrue();
        await Assert.That(cloned.ContainsKey(1)).IsFalse();
        await Assert.That(cloned.ContainsKey(2)).IsFalse();
        await Assert.That(cloned.ContainsKey(3)).IsTrue();
        await Assert.That(cloned.ContainsKey(4)).IsTrue();
        await Assert.That(cloned.Count).IsEqualTo(testDictionary.Count - removed);
    }

    [Test]
    public async Task TryGetValueTest()
    {
        var d = new FlexDictionary<string, int>(defaultValue: default)
        {
            ["a"] = 2,
        };

        bool found = d.TryGetValue("fsad", out int value);
        await Assert.That(found).IsFalse();
        await Assert.That(value).IsZero();

        found = d.TryGetValue("a", out value);
        await Assert.That(found).IsTrue();
        await Assert.That(value).IsEqualTo(2);
    }

    [Test]
    public async Task AccessorTest()
    {
        var d = new FlexDictionary<string, int>(defaultValue: default);
        int value = d[""];
        await Assert.That(value).IsZero();
        d["a"] = 5;
        await Assert.That(d["a"]).IsEqualTo(5);
        d[""] = 3;
        await Assert.That(d[""]).IsEqualTo(3);
    }
}