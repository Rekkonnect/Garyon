using Garyon.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class QueueExtensionsTests
{
    [Test]
    public async Task EnqueueRangeTest()
    {
        var queue = new Queue<int>();
        queue.EnqueueRange([1, 2, 3]);

        await Assert.That(queue.SequenceEqual([1, 2, 3])).IsTrue();
    }

    [Test]
    public async Task DequeueRangeTest()
    {
        var queue = new Queue<int>([1, 2, 3, 4]);
        var dequeued = queue.DequeueRange(2).ToArray();

        await Assert.That(dequeued.SequenceEqual([1, 2])).IsTrue();
        await Assert.That(queue.SequenceEqual([3, 4])).IsTrue();
    }

    [Test]
    public async Task DequeueAllTest()
    {
        var queue = new Queue<int>([1, 2, 3]);
        var dequeued = queue.DequeueAll().ToArray();

        await Assert.That(dequeued.SequenceEqual([1, 2, 3])).IsTrue();
        await Assert.That(queue.Count).IsEqualTo(0);
    }

    [Test]
    public async Task InsertAppendsWhenIndexIsOutsideRangeTest()
    {
        var queue = new Queue<int>([1, 2, 3]);
        queue.Insert(4, 10);

        await Assert.That(queue.SequenceEqual([1, 2, 3, 4])).IsTrue();
    }

    [Test]
    public async Task InsertWithinRangeTest()
    {
        var queue = new Queue<int>([1, 2, 3, 4]);
        queue.Insert(9, 1);

        await Assert.That(queue.SequenceEqual([3, 4, 9, 1, 2])).IsTrue();
    }
}
