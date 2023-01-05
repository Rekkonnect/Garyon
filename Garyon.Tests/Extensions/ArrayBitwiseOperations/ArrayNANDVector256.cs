using Garyon.Functions.UnmanagedHelpers;
using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerBitwiseOperations;

namespace Garyon.Tests.Extensions.ArrayBitwiseOperations;

public class ArrayNANDVector256 : ArrayManipulationHelpersTestsBase
{
    protected unsafe void NANDArray<TOrigin>()
        where TOrigin : unmanaged
    {
        PerformManipulationArray<TOrigin>(NANDArrayVector256);
    }

    protected override unsafe object GetExpectedResult<TOrigin, TTarget>(TOrigin* origin, int index) => ValueManipulation.NANDRescaleMask(*(TTarget*)(origin + index), Mask);

    [Test]
    public unsafe void NANDByteArray()
    {
        NANDArray<byte>();
    }
    [Test]
    public unsafe void NANDInt16Array()
    {
        NANDArray<short>();
    }
    [Test]
    public unsafe void NANDInt32Array()
    {
        NANDArray<int>();
    }
    [Test]
    public unsafe void NANDInt64Array()
    {
        NANDArray<long>();
    }
    [Test]
    public unsafe void NANDSByteArray()
    {
        NANDArray<sbyte>();
    }
    [Test]
    public unsafe void NANDUInt16Array()
    {
        NANDArray<ushort>();
    }
    [Test]
    public unsafe void NANDUInt32Array()
    {
        NANDArray<uint>();
    }
    [Test]
    public unsafe void NANDUInt64Array()
    {
        NANDArray<ulong>();
    }
}
