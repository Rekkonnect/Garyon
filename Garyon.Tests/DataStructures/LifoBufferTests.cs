using Garyon.DataStructures;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.DataStructures;

public class LifoBufferTests
{
    [Test]
    public async Task EmptyBufferTest()
    {
        var buffer = new LifoBuffer<int>(3);

        buffer.GetOrderedBuffer(out var newest, out var oldest);
        var bufferLength = buffer.GetBuffer().Length;
        var newestLength = newest.Length;
        var oldestLength = oldest.Length;

        await Assert.That(buffer.Appends).IsEqualTo(0);
        await Assert.That(bufferLength).IsEqualTo(0);
        await Assert.That(newestLength).IsEqualTo(0);
        await Assert.That(oldestLength).IsEqualTo(0);
    }

    [Test]
    public async Task AppendAndGetBufferTest()
    {
        var buffer = new LifoBuffer<int>(3);
        buffer.Append(1);
        buffer.Append(2);
        buffer.Append(3);
        buffer.Append(4);

        await Assert.That(buffer.Capacity).IsEqualTo(3);
        await Assert.That(buffer.Appends).IsEqualTo(4);
        await Assert.That(buffer.GetBuffer().ToArray().SequenceEqual([4, 2, 3])).IsTrue();
    }

    [Test]
    public async Task GetOrderedBufferWithoutWraparoundTest()
    {
        var buffer = new LifoBuffer<int>(3);
        buffer.Append(1);
        buffer.Append(2);

        buffer.GetOrderedBuffer(out var newest, out var oldest);
        var newestArray = newest.ToArray();
        var oldestArray = oldest.ToArray();

        await Assert.That(oldestArray.Length).IsEqualTo(0);
        await Assert.That(newestArray.SequenceEqual([1, 2])).IsTrue();
    }

    [Test]
    public async Task GetOrderedBufferTest()
    {
        var buffer = new LifoBuffer<int>(3);
        buffer.Append(1);
        buffer.Append(2);
        buffer.Append(3);
        buffer.Append(4);

        buffer.GetOrderedBuffer(out var newest, out var oldest);
        var newestArray = newest.ToArray();
        var oldestArray = oldest.ToArray();

        await Assert.That(newestArray.SequenceEqual([4])).IsTrue();
        await Assert.That(oldestArray.SequenceEqual([2, 3])).IsTrue();
        await Assert.That(oldestArray.Concat(newestArray).SequenceEqual([2, 3, 4])).IsTrue();
    }
}
