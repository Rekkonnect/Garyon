using Garyon.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class IListExtensionsTests
{
    [Test]
    public async Task RemoveLastTest()
    {
        var iList = (IList<int>)new List<int> { 1, 2, 3 };
        iList.RemoveLast();
        await Assert.That(iList.SequenceEqual([1, 2])).IsTrue();
    }

    [Test]
    public async Task SwapTest()
    {
        var iList = (IList<int>)new List<int> { 1, 2 };
        iList.Swap(0, 1);
        await Assert.That(iList.SequenceEqual([2, 1])).IsTrue();
    }

    [Test]
    public async Task RemoveAtDecrementTest()
    {
        var iList = (IList<int>)new List<int> { 2, 1 };
        int index = 1;
        iList.RemoveAtDecrement(ref index);
        await Assert.That(iList.SequenceEqual([2])).IsTrue();
        await Assert.That(index).IsEqualTo(0);
    }

    [Test]
    public async Task PopTest()
    {
        var iList = (IList<int>)new List<int> { 2 };
        var popped = iList.Pop();
        await Assert.That(popped).IsEqualTo(2);
        await Assert.That(iList.Count).IsEqualTo(0);
    }

    [Test]
    public async Task AtIndexOrDefaultReadOnlyTest()
    {
        IReadOnlyList<int> readOnly = [7, 8];
        await Assert.That(readOnly.AtIndexOrDefaultReadOnly(1)).IsEqualTo(8);
        await Assert.That(readOnly.AtIndexOrDefaultReadOnly(5)).IsEqualTo(0);
    }

    [Test]
    public async Task TryGetAtIndexReadOnlyTest()
    {
        IReadOnlyList<int> readOnly = [7, 8];
        await Assert.That(readOnly.TryGetAtIndexReadOnly(0, out var roValue)).IsTrue();
        await Assert.That(roValue).IsEqualTo(7);
        await Assert.That(readOnly.TryGetAtIndexReadOnly(10, out _)).IsFalse();
    }

    [Test]
    public async Task AtIndexOrDefaultTest()
    {
        var mutable = (IList<int>)new List<int> { 9 };
        await Assert.That(mutable.AtIndexOrDefault(0)).IsEqualTo(9);
        await Assert.That(mutable.AtIndexOrDefault(1)).IsEqualTo(0);
    }

    [Test]
    public async Task TryGetAtIndexTest()
    {
        var mutable = (IList<int>)new List<int> { 9 };
        await Assert.That(mutable.TryGetAtIndex(0, out var listValue)).IsTrue();
        await Assert.That(listValue).IsEqualTo(9);
        await Assert.That(mutable.TryGetAtIndex(2, out _)).IsFalse();
    }

    [Test]
    public async Task SingleOrDefaultReadOnlyTest()
    {
        await Assert.That(new[] { 5 }.SingleOrDefaultReadOnly()).IsEqualTo(5);
        await Assert.That(new[] { 5, 6 }.SingleOrDefaultReadOnly()).IsEqualTo(0);
    }

    [Test]
    public async Task SingleOrDefaultSafeTest()
    {
        await Assert.That(((IList<int>)new List<int> { 5 }).SingleOrDefaultSafe()).IsEqualTo(5);
        await Assert.That(((IList<int>)new List<int> { 5, 6 }).SingleOrDefaultSafe()).IsEqualTo(0);
    }

    [Test]
    public async Task ClearSetRangeTest()
    {
        var nonGeneric = (IList)new ArrayList { 1, 2 };
        nonGeneric.ClearSetRange(new[] { 3, 4, 5 });
        await Assert.That(nonGeneric.Count).IsEqualTo(1);
        await Assert.That(nonGeneric[0]).IsTypeOf<int[]>();
    }

    [Test]
    public async Task AddRangeTest()
    {
        var nonGeneric = (IList)new ArrayList { 1 };
        nonGeneric.AddRange(new[] { 6, 7 });
        await Assert.That(nonGeneric.Count).IsEqualTo(3);
    }
}
