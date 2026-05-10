using Garyon.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class ListExtensionsTests
{
    [Test]
    public async Task CloneTest()
    {
        var nested = new List<List<int>>
        {
            new() { 1, 2 },
            new() { 3 },
        };
        var nestedClone = nested.Clone();
        nested[0][0] = 100;
        await Assert.That(nestedClone[0][0]).IsEqualTo(1);
    }

    [Test]
    public async Task RemoveRangeTest()
    {
        var removed = new List<int> { 1, 2, 3, 2, 4 }.RemoveRange([2, 4], out int removedElements);
        await Assert.That(removed.SequenceEqual([1, 3])).IsTrue();
        await Assert.That(removedElements).IsEqualTo(3);
    }

    [Test]
    public async Task InsertAtStartNullListTest()
    {
        List<int>? nullList = null;
        var insertedNew = nullList.InsertAtStart(5);
        await Assert.That(insertedNew.SequenceEqual([5])).IsTrue();
    }

    [Test]
    public async Task InsertAtStartExistingListTest()
    {
        var insertedExisting = new List<int> { 2, 3 }.InsertAtStart(1);
        await Assert.That(insertedExisting.SequenceEqual([1, 2, 3])).IsTrue();
    }

    [Test]
    public async Task MoveElementForwardTest()
    {
        var movedForward = new List<int> { 1, 2, 3, 4 }.MoveElement(1, 3);
        await Assert.That(movedForward.SequenceEqual([1, 3, 2, 4])).IsTrue();
    }

    [Test]
    public async Task MoveElementBackwardTest()
    {
        var movedBackward = new List<int> { 1, 2, 3, 4 }.MoveElement(3, 1);
        await Assert.That(movedBackward.SequenceEqual([1, 4, 2, 3])).IsTrue();
    }

    [Test]
    public async Task MoveElementToEndTest()
    {
        var movedToEnd = new List<int> { 1, 2, 3 }.MoveElementToEnd(0);
        await Assert.That(movedToEnd.SequenceEqual([2, 3, 1])).IsTrue();
    }

    [Test]
    public async Task MoveElementToStartTest()
    {
        var movedToStart = new List<int> { 1, 2, 3 }.MoveElementToStart(2);
        await Assert.That(movedToStart.SequenceEqual([3, 1, 2])).IsTrue();
    }

    [Test]
    public async Task SetEnsureCapacityTest()
    {
        var ensuredCapacity = new List<string>();
        ensuredCapacity.SetEnsureCapacity(2, "x");
        await Assert.That(ensuredCapacity.Count).IsEqualTo(3);
        await Assert.That(ensuredCapacity[2]).IsEqualTo("x");
        await Assert.That(ensuredCapacity[0]).IsNull();
    }
}
