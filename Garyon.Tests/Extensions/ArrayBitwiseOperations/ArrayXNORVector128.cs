using Garyon.Functions.UnmanagedHelpers;
using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerBitwiseOperations;

namespace Garyon.Tests.Extensions.ArrayBitwiseOperations
{
    public class ArrayXNORVector128 : ArrayManipulationHelpersTestsBase
    {
        protected unsafe void XNORArray<TOrigin>()
            where TOrigin : unmanaged
        {
            PerformManipulationArray<TOrigin>(XNORArrayVector128);
        }

        protected override unsafe object GetExpectedResult<TOrigin, TTarget>(TOrigin* origin, int index) => ValueManipulation.XNORRescaleMask(*(TTarget*)(origin + index), Mask);

        [Test]
        public unsafe void XNORByteArray()
        {
            XNORArray<byte>();
        }
        [Test]
        public unsafe void XNORInt16Array()
        {
            XNORArray<short>();
        }
        [Test]
        public unsafe void XNORInt32Array()
        {
            XNORArray<int>();
        }
        [Test]
        public unsafe void XNORInt64Array()
        {
            XNORArray<long>();
        }
        [Test]
        public unsafe void XNORSByteArray()
        {
            XNORArray<sbyte>();
        }
        [Test]
        public unsafe void XNORUInt16Array()
        {
            XNORArray<ushort>();
        }
        [Test]
        public unsafe void XNORUInt32Array()
        {
            XNORArray<uint>();
        }
        [Test]
        public unsafe void XNORUInt64Array()
        {
            XNORArray<ulong>();
        }
    }
}
