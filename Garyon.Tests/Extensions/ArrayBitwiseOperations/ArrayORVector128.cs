using Garyon.Functions.UnmanagedHelpers;
using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerBitwiseOperations;

namespace Garyon.Tests.Extensions.ArrayBitwiseOperations;

public class ArrayORVector128 : ArrayManipulationHelpersTestsBase
{
    protected unsafe void ORArray<TOrigin>()
        where TOrigin : unmanaged
    {
        PerformManipulationArray<TOrigin>(ORArrayVector128);
    }

    protected override unsafe object GetExpectedResult<TOrigin, TTarget>(TOrigin* origin, int index) => ValueManipulation.ORRescaleMask(*(TTarget*)(origin + index), Mask);

    [Test]
    public unsafe void ORByteArray()
    {
        ORArray<byte>();
    }
    [Test]
    public unsafe void ORInt16Array()
    {
        ORArray<short>();
    }
    [Test]
    public unsafe void ORInt32Array()
    {
        ORArray<int>();
    }
    [Test]
    public unsafe void ORInt64Array()
    {
        ORArray<long>();
    }
    [Test]
    public unsafe void ORSByteArray()
    {
        ORArray<sbyte>();
    }
    [Test]
    public unsafe void ORUInt16Array()
    {
        ORArray<ushort>();
    }
    [Test]
    public unsafe void ORUInt32Array()
    {
        ORArray<uint>();
    }
    [Test]
    public unsafe void ORUInt64Array()
    {
        ORArray<ulong>();
    }
}
