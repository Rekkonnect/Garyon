using Garyon.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class StackExtensionsTests
{
    [Test]
    public async Task PushRangeTest()
    {
        var stack = new Stack<int>();
        stack.PushRange([1, 2, 3]);

        await Assert.That(stack.SequenceEqual([3, 2, 1])).IsTrue();
    }

    [Test]
    public async Task PopRangeTest()
    {
        var stack = new Stack<int>([1, 2, 3, 4]);
        var popped = stack.PopRange(2).ToArray();

        await Assert.That(popped.SequenceEqual([4, 3])).IsTrue();
        await Assert.That(stack.SequenceEqual([2, 1])).IsTrue();
    }

    [Test]
    public async Task PopAllTest()
    {
        var stack = new Stack<int>([1, 2, 3]);
        var popped = stack.PopAll().ToArray();

        await Assert.That(popped.SequenceEqual([3, 2, 1])).IsTrue();
        await Assert.That(stack.Count).IsEqualTo(0);
    }

    [Test]
    public async Task InsertPushesWhenIndexIsOutsideRangeTest()
    {
        var stack = new Stack<int>([1, 2, 3]);
        stack.Insert(4, 10);

        await Assert.That(stack.Peek()).IsEqualTo(4);
    }

    [Test]
    public async Task InsertWithinRangeTest()
    {
        var stack = new Stack<int>([1, 2, 3, 4]);
        stack.Insert(9, 1);

        await Assert.That(stack.SequenceEqual([3, 4, 9, 2, 1])).IsTrue();
    }
}
