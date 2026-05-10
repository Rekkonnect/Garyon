using Garyon.Extensions.ArrayExtensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class GenericArrayExtensionsTests
{
    [Test]
    public async Task CopyMoveReverseSwapAndSegmentHelpersTest()
    {
        int[] original = [1, 2, 3, 4];
        var copy = original.CopyArray();
        var movedForward = original.CopyArray().MoveElement(1, 3);
        var movedBackward = original.CopyArray().MoveElement(3, 1);
        var movedToEnd = original.CopyArray().MoveElementToEnd(0);
        var movedToStart = original.CopyArray().MoveElementToStart(3);
        var reversed = Garyon.Extensions.ArrayExtensions.GenericArrayExtensions.Reverse<int>(original.CopyArray());
        var swapped = original.CopyArray().Swap(0, 2);
        var fullSegment = original.Segment();
        var partialSegment = original.Segment(1, 2);
        var boundedSegment = original.SegmentFromBounds(1, 3);

        copy[0] = 9;

        await Assert.That(original.SequenceEqual([1, 2, 3, 4])).IsTrue();
        await Assert.That(copy.SequenceEqual([9, 2, 3, 4])).IsTrue();
        await Assert.That(movedForward.SequenceEqual([1, 3, 4, 2])).IsTrue();
        await Assert.That(movedBackward.SequenceEqual([1, 4, 2, 3])).IsTrue();
        await Assert.That(movedToEnd.SequenceEqual([2, 3, 4, 1])).IsTrue();
        await Assert.That(movedToStart.SequenceEqual([4, 1, 2, 3])).IsTrue();
        await Assert.That(reversed.SequenceEqual([4, 3, 2, 1])).IsTrue();
        await Assert.That(swapped.SequenceEqual([3, 2, 1, 4])).IsTrue();
        await Assert.That(fullSegment.Array).IsSameReferenceAs(original);
        await Assert.That(fullSegment.Count).IsEqualTo(4);
        await Assert.That(partialSegment.SequenceEqual([2, 3])).IsTrue();
        await Assert.That(boundedSegment.SequenceEqual([2, 3])).IsTrue();
    }

    [Test]
    public async Task ArrayTypeAndDimensionHelpersTest()
    {
        Array rectangular = new int[2, 3];
        Array jagged = new string[1][];
        var lengths = rectangular.GetDimensionLengths();

        await Assert.That(rectangular.IsArrayOfType<int>()).IsTrue();
        await Assert.That(rectangular.IsArrayOfType<string>()).IsFalse();
        await Assert.That(jagged.IsArrayOfType<string>(2)).IsTrue();
        await Assert.That(lengths.SequenceEqual([2, 3])).IsTrue();
    }

    [Test]
    public async Task ClearAndFillTest()
    {
        int[] values = [1, 2, 3];
        values.Fill(7);
        await Assert.That(values.SequenceEqual([7, 7, 7])).IsTrue();

        ((Array)values).Clear();
        await Assert.That(values.SequenceEqual([0, 0, 0])).IsTrue();
    }

    [Test]
    public async Task InvalidMoveAndSwapIndexesThrowTest()
    {
        int[] values = [1, 2, 3];

        Assert.Throws<ArgumentException>(() => values.CopyArray().MoveElement(-1, 0));
        Assert.Throws<ArgumentException>(() => values.CopyArray().MoveElement(0, 3));
        Assert.Throws<ArgumentException>(() => values.CopyArray().Swap(0, 3));

        await Assert.That(values.SequenceEqual([1, 2, 3])).IsTrue();
    }
}
