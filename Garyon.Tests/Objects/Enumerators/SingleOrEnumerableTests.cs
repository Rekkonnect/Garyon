using Garyon.Objects.Enumerators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Objects.Enumerators;

public class SingleOrEnumerableTests
{
    [Test]
    public async Task TestSingle()
    {
        const int value = 3;
        var instance = new SingleOrEnumerable<int>(value);
        await TestSingle(value, instance);
    }
    [Test]
    public async Task TestEnumerableAsync()
    {
        int[] enumerable = [1, 2, 5, 67, 348];
        var instance = new SingleOrEnumerable<int>(enumerable);
        await TestEnumerable(enumerable, instance);
    }

    [Test]
    public async Task TestAssignAsync()
    {
        const int value = 3;
        int[] enumerable = [1, 2, 5, 67, 348];

        var instance = new SingleOrEnumerable<int>(value);
        await TestSingle(value, instance);

        instance.Assign(enumerable);
        await TestEnumerable(enumerable, instance);

        instance.Assign(value);
        await TestSingle(value, instance);
    }

    private async Task TestSingle<T>(T value, SingleOrEnumerable<T> instance)
    {
        await Assert.That(() => _ = instance.Enumerable).ThrowsException();
        await Assert.That(instance.Single).IsEqualTo(value);

        var enumeratedArray = instance.ToArray();
        await Assert.That(enumeratedArray).IsEquivalentTo([value]);
    }
    private async Task TestEnumerable<T>(IEnumerable<T> enumerable, SingleOrEnumerable<T> instance)
    {
        await Assert.That(() => _ = instance.Single).ThrowsException();
        await Assert.That(instance.Enumerable).IsEquivalentTo(enumerable);

        var enumeratedArray = instance.ToArray();
        await Assert.That(enumeratedArray).IsEquivalentTo(enumerable);
    }
}