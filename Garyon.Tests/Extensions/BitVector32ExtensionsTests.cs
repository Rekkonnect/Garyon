using Garyon.Extensions;
using System.Collections.Specialized;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class BitVector32ExtensionsTests
{
    [Test]
    public async Task BitVectorHelpersTest()
    {
        var vector = new BitVector32(0);
        vector.Set(1, true);
        vector.Set(3, true);

        await Assert.That(vector.UInt).IsEqualTo((uint)10);
        await Assert.That(vector.Get(1)).IsTrue();
        await Assert.That(vector.Get(2)).IsFalse();

        var other = new BitVector32(12);
        await Assert.That(vector.And(other).Data).IsEqualTo(8);
        await Assert.That(vector.Or(other).Data).IsEqualTo(14);
        await Assert.That(vector.Xor(other).Data).IsEqualTo(6);
        await Assert.That(vector.PopCount()).IsEqualTo(2);
        await Assert.That(vector.FirstBitIndex()).IsEqualTo(1);
        await Assert.That(vector.LastBitIndex()).IsEqualTo(3);
    }
}
