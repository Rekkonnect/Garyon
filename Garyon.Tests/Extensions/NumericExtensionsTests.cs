using Garyon.Extensions;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class NumericExtensionsTests
{
    [Test]
    public async Task IsPowerOfTwoTest()
    {
        await Assert.That(((byte)1).IsPowerOfTwo()).IsTrue();
        await Assert.That(((byte)3).IsPowerOfTwo()).IsFalse();
        await Assert.That(((short)16).IsPowerOfTwo()).IsTrue();
        await Assert.That(((short)(-2)).IsPowerOfTwo()).IsFalse();
        await Assert.That(8.IsPowerOfTwo()).IsTrue();
        await Assert.That((-8).IsPowerOfTwo()).IsFalse();
        await Assert.That(64L.IsPowerOfTwo()).IsTrue();
        await Assert.That((-64L).IsPowerOfTwo()).IsFalse();
        await Assert.That(((sbyte)4).IsPowerOfTwo()).IsTrue();
        await Assert.That(((sbyte)(-4)).IsPowerOfTwo()).IsFalse();
        await Assert.That(((ushort)32).IsPowerOfTwo()).IsTrue();
        await Assert.That(((ushort)0).IsPowerOfTwo()).IsFalse();
        await Assert.That(128u.IsPowerOfTwo()).IsTrue();
        await Assert.That(0u.IsPowerOfTwo()).IsFalse();
        await Assert.That(256ul.IsPowerOfTwo()).IsTrue();
        await Assert.That(3ul.IsPowerOfTwo()).IsFalse();
    }

    [Test]
    public async Task OneOrGreaterTest()
    {
        await Assert.That(((byte)0).OneOrGreater()).IsEqualTo((byte)1);
        await Assert.That(((short)2).OneOrGreater()).IsEqualTo((short)2);
        await Assert.That(0.OneOrGreater()).IsEqualTo(1);
        await Assert.That(5L.OneOrGreater()).IsEqualTo(5L);
        await Assert.That(((sbyte)0).OneOrGreater()).IsEqualTo((sbyte)1);
        await Assert.That(((ushort)0).OneOrGreater()).IsEqualTo((ushort)1);
        await Assert.That(0u.OneOrGreater()).IsEqualTo(1u);
        await Assert.That(2ul.OneOrGreater()).IsEqualTo(2ul);
        await Assert.That(0f.OneOrGreater()).IsEqualTo(1f);
        await Assert.That(2d.OneOrGreater()).IsEqualTo(2d);
        await Assert.That(0m.OneOrGreater()).IsEqualTo(1m);
    }

    [Test]
    public async Task ZeroOrGreaterTest()
    {
        await Assert.That(((byte)0).ZeroOrGreater()).IsEqualTo((byte)0);
        await Assert.That(((short)(-1)).ZeroOrGreater()).IsEqualTo((short)0);
        await Assert.That((-3).ZeroOrGreater()).IsEqualTo(0);
        await Assert.That(4L.ZeroOrGreater()).IsEqualTo(4L);
        await Assert.That(((sbyte)(-2)).ZeroOrGreater()).IsEqualTo((sbyte)0);
        await Assert.That(((ushort)3).ZeroOrGreater()).IsEqualTo((ushort)3);
        await Assert.That(4u.ZeroOrGreater()).IsEqualTo(4u);
        await Assert.That(5ul.ZeroOrGreater()).IsEqualTo(5ul);
        await Assert.That((-1.5f).ZeroOrGreater()).IsEqualTo(0f);
        await Assert.That(2.5d.ZeroOrGreater()).IsEqualTo(2.5d);
        await Assert.That((-1m).ZeroOrGreater()).IsEqualTo(0m);
    }
}
