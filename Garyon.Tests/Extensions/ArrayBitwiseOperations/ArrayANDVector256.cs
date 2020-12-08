using Garyon.Functions.UnmanagedHelpers;
using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerBitwiseOperations;

namespace Garyon.Tests.Extensions.ArrayBitwiseOperations
{
    public class ArrayANDVector256 : ArrayManipulationHelpersTestsBase
    {
        protected unsafe void ANDArray<TOrigin>()
            where TOrigin : unmanaged
        {
            PerformManipulationArray<TOrigin>(ANDArrayVector256);
        }

        protected override unsafe object GetExpectedResult<TOrigin, TTarget>(TOrigin* origin, int index) => ValueManipulation.ANDRescaleMask(*(TTarget*)(origin + index), Mask);

        [Test]
        public unsafe void ANDByteArray()
        {
            ANDArray<byte>();
        }
        [Test]
        public unsafe void ANDInt16Array()
        {
            ANDArray<short>();
        }
        [Test]
        public unsafe void ANDInt32Array()
        {
            ANDArray<int>();
        }
        [Test]
        public unsafe void ANDInt64Array()
        {
            ANDArray<long>();
        }
        [Test]
        public unsafe void ANDSByteArray()
        {
            ANDArray<sbyte>();
        }
        [Test]
        public unsafe void ANDUInt16Array()
        {
            ANDArray<ushort>();
        }
        [Test]
        public unsafe void ANDUInt32Array()
        {
            ANDArray<uint>();
        }
        [Test]
        public unsafe void ANDUInt64Array()
        {
            ANDArray<ulong>();
        }
    }
}
