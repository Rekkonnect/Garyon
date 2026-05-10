using Garyon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Objects;

public class YieldingTests
{
    [Test]
    public async Task YieldingHelpersPopulateCollectionsTest()
    {
        int current = 0;
        var array = Yielding.YieldArray(3, () => ++current);
        var list = new List<int> { 10 }.YieldOnto(2, () => ++current);
        var set = Yielding.YieldSet(3, () => 1);
        var sortedSet = Yielding.YieldSortedSet(3, () => --current);

        await Assert.That(array.SequenceEqual([1, 2, 3])).IsTrue();
        await Assert.That(list.SequenceEqual([10, 4, 5])).IsTrue();
        await Assert.That(set.Count).IsEqualTo(1);
        await Assert.That(sortedSet.SequenceEqual([2, 3, 4])).IsTrue();
    }

    [Test]
    public async Task YielderFillHelpersPopulateArrayTest()
    {
        int current = 0;
        var yielder = Yielding.For(() => ++current);
        var array = new int[5];
        var memory = new int[3].AsMemory();

        yielder.Fill(array);
        yielder.Fill(memory);

        await Assert.That(array.SequenceEqual([1, 2, 3, 4, 5])).IsTrue();
        await Assert.That(memory.ToArray().SequenceEqual([6, 7, 8])).IsTrue();
    }

    [Test]
    public async Task YieldingEnumerableAndIntoHelpersTest()
    {
        int current = 0;
        var enumerable = Yielding.Yield(3, () => ++current).ToArray();
        var list = Yielding.YieldInto(2, () => ++current, new List<int> { 10 });

        await Assert.That(enumerable.SequenceEqual([1, 2, 3])).IsTrue();
        await Assert.That(list.SequenceEqual([10, 4, 5])).IsTrue();
    }

    [Test]
    public async Task SpanYieldingHelpersPopulateSpanTest()
    {
        var array = new int[5];
        int current = 0;
        int nextIndex;

        {
            var yielder = SpanYielding.YieldOntoSpan(array.AsSpan(), 2, () => ++current);
            yielder.Yield(1, () => current += 10);
            yielder.FillRest();
            nextIndex = yielder.NextSpanIndex;
        }

        await Assert.That(array.SequenceEqual([1, 2, 12, 22, 32])).IsTrue();
        await Assert.That(nextIndex).IsEqualTo(array.Length);
    }

    [Test]
    public async Task SpanYielderResetNextIndexTest()
    {
        var array = new int[3];
        int current = 0;

        {
            var yielder = SpanYielding.For(array.AsSpan(), () => ++current, 1);
            yielder.Yield(2);
            yielder.ResetNextIndex();
            yielder.Yield(1, () => 9);
        }

        await Assert.That(array.SequenceEqual([9, 1, 2])).IsTrue();
    }
}
