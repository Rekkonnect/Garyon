using Garyon.Functions.UnmanagedHelpers;
using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerBitwiseOperations;

namespace Garyon.Tests.Extensions.ArrayBitwiseOperations;

public class ArrayNORVector128 : ArrayManipulationHelpersTestsBase
{
    protected unsafe void NORArray<TOrigin>()
        where TOrigin : unmanaged
    {
        PerformManipulationArray<TOrigin>(NORArrayVector128);
    }

    protected override unsafe object GetExpectedResult<TOrigin, TTarget>(TOrigin* origin, int index) => ValueManipulation.NORRescaleMask(*(TTarget*)(origin + index), Mask);

    [Test]
    public unsafe void NORByteArray()
    {
        NORArray<byte>();
    }
    [Test]
    public unsafe void NORInt16Array()
    {
        NORArray<short>();
    }
    [Test]
    public unsafe void NORInt32Array()
    {
        NORArray<int>();
    }
    [Test]
    public unsafe void NORInt64Array()
    {
        NORArray<long>();
    }
    [Test]
    public unsafe void NORSByteArray()
    {
        NORArray<sbyte>();
    }
    [Test]
    public unsafe void NORUInt16Array()
    {
        NORArray<ushort>();
    }
    [Test]
    public unsafe void NORUInt32Array()
    {
        NORArray<uint>();
    }
    [Test]
    public unsafe void NORUInt64Array()
    {
        NORArray<ulong>();
    }
}
