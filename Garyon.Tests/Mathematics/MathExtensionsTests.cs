using Garyon.Mathematics;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Mathematics;

public class MathExtensionsTests
{
    [Test]
    public async Task NumberSquareAndMathWrapperHelpersTest()
    {
        await Assert.That(5.Square()).IsEqualTo(25);
        await Assert.That(5U.Square()).IsEqualTo(25U);
        await Assert.That(5L.Square()).IsEqualTo(25L);
        await Assert.That(5UL.Square()).IsEqualTo(25UL);
        await Assert.That(2.5F.Square()).IsEqualTo(6.25F);
        await Assert.That(2.5D.Square()).IsEqualTo(6.25D);
        await Assert.That(2.5M.Square()).IsEqualTo(6.25M);
        await Assert.That(Math.Square(6)).IsEqualTo(36);
        await Assert.That(Math.Square(6U)).IsEqualTo(36U);
        await Assert.That(Math.Square(6L)).IsEqualTo(36L);
        await Assert.That(Math.Square(6UL)).IsEqualTo(36UL);
        await Assert.That(Math.Square(1.5F)).IsEqualTo(2.25F);
        await Assert.That(Math.Square(1.5D)).IsEqualTo(2.25D);
        await Assert.That(Math.Square(1.5M)).IsEqualTo(2.25M);
    }

    [Test]
    public async Task FloatingMathHelpersTest()
    {
        await Assert.That(2D.Pow(3)).IsEqualTo(8D);
        await Assert.That(9D.Sqrt()).IsEqualTo(3D);
        await Assert.That(9F.Sqrt()).IsEqualTo(3F);
        await Assert.That(Math.E.Log()).IsEqualTo(1D).Within(0.0000000001D);
        await Assert.That(8D.Log(2D)).IsEqualTo(3D);
        await Assert.That(100D.Log10()).IsEqualTo(2D);
        await Assert.That(8D.Log2()).IsEqualTo(3D);
    }

    [Test]
    public async Task BinaryFlagAndHalveHelpersTest()
    {
        const int value = 0b1110;

        await Assert.That(value.HasFlag(0b0110)).IsTrue();
        await Assert.That(value.HasFlag(0b0001)).IsFalse();
        await Assert.That(value.RemoveFlag(0b0100)).IsEqualTo(0b1010);
        await Assert.That(9.Halve()).IsEqualTo(4);
        await Assert.That((-9).Halve()).IsEqualTo(-4);
    }
}
