using Garyon.Functions.UnmanagedHelpers;
using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerBitwiseOperations;

namespace Garyon.Tests.Extensions.ArrayBitwiseOperations;

public class ArrayXORVector128 : ArrayManipulationHelpersTestsBase
{
    protected unsafe void XORArray<TOrigin>()
        where TOrigin : unmanaged
    {
        PerformManipulationArray<TOrigin>(XORArrayVector128);
    }

    protected override unsafe object GetExpectedResult<TOrigin, TTarget>(TOrigin* origin, int index) => ValueManipulation.XORRescaleMask(*(TTarget*)(origin + index), Mask);

    [Test]
    public unsafe void XORByteArray()
    {
        XORArray<byte>();
    }
    [Test]
    public unsafe void XORInt16Array()
    {
        XORArray<short>();
    }
    [Test]
    public unsafe void XORInt32Array()
    {
        XORArray<int>();
    }
    [Test]
    public unsafe void XORInt64Array()
    {
        XORArray<long>();
    }
    [Test]
    public unsafe void XORSByteArray()
    {
        XORArray<sbyte>();
    }
    [Test]
    public unsafe void XORUInt16Array()
    {
        XORArray<ushort>();
    }
    [Test]
    public unsafe void XORUInt32Array()
    {
        XORArray<uint>();
    }
    [Test]
    public unsafe void XORUInt64Array()
    {
        XORArray<ulong>();
    }
}
