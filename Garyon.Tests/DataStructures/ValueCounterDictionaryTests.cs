using Garyon.DataStructures;
using System.Threading.Tasks;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;

namespace Garyon.Tests.DataStructures;

public class ValueCounterDictionaryTests
{
    private static readonly ValueCounterDictionary<char> testDictionary = InitializeTestDictionary();

    private static ValueCounterDictionary<char> InitializeTestDictionary()
    {
        var dictionary = new ValueCounterDictionary<char>();
        for (int i = 0; i < 5; i++)
            dictionary.Add((char)('a' + i));
        return dictionary;
    }

    [Test]
    public async Task EnumerableInitializationTest()
    {
        var d = new ValueCounterDictionary<char>("CHARACTER COUNTER TEST");

        await Assert.That(d[' ']).IsEqualTo(2);
        await Assert.That(d['T']).IsEqualTo(4);
        await Assert.That(d.Count).IsEqualTo(11);
    }
    [Test]
    public async Task AddTest()
    {
        var d = new ValueCounterDictionary<char>(testDictionary);
        d.Add('a', 4);
        await Assert.That(d['a']).IsEqualTo(5);
    }
    [Test]
    public async Task AdjustCountersTest()
    {
        var d = new ValueCounterDictionary<char>(testDictionary);
        d.Add('a', 4);
        d.AdjustCounters('a', 'b', 3);
        await Assert.That(d['a']).IsEqualTo(2);
        await Assert.That(d['b']).IsEqualTo(4);
    }
}