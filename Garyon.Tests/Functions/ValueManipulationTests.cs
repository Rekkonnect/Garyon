using Garyon.Functions.UnmanagedHelpers;
using NUnit.Framework;

namespace Garyon.Tests.Functions
{
    public class ValueManipulationTests
    {
        private const byte value8 = 0x01;
        private const ushort value16 = 0xFF01;
        private const uint value32 = 0x1337FF01;
        private const ulong value64 = 0x223344551337FF01;

        #region Byte > T
        [Test]
        public void RescaleByteToByte()
        {
            byte rescaled = ValueManipulation.Rescale<byte, byte>(value8);
            Assert.AreEqual(value8, rescaled);
        }
        [Test]
        public void RescaleByteToUInt16()
        {
            ushort rescaled = ValueManipulation.Rescale<byte, ushort>(value8);
            Assert.AreEqual(value8, rescaled);
        }
        [Test]
        public void RescaleByteToUInt32()
        {
            uint rescaled = ValueManipulation.Rescale<byte, uint>(value8);
            Assert.AreEqual(value8, rescaled);
        }
        [Test]
        public void RescaleByteToUInt64()
        {
            ulong rescaled = ValueManipulation.Rescale<byte, ulong>(value8);
            Assert.AreEqual(value8, rescaled);
        }
        #endregion

        #region UInt16 > T
        [Test]
        public void RescaleUInt16ToByte()
        {
            byte rescaled = ValueManipulation.Rescale<ushort, byte>(value16);
            Assert.AreEqual(value8, rescaled);
        }
        [Test]
        public void RescaleUInt16ToUInt16()
        {
            ushort rescaled = ValueManipulation.Rescale<ushort, ushort>(value16);
            Assert.AreEqual(value16, rescaled);
        }
        [Test]
        public void RescaleUInt16ToUInt32()
        {
            uint rescaled = ValueManipulation.Rescale<ushort, uint>(value16);
            Assert.AreEqual(value16, rescaled);
        }
        [Test]
        public void RescaleUInt16ToUInt64()
        {
            ulong rescaled = ValueManipulation.Rescale<ushort, ulong>(value16);
            Assert.AreEqual(value16, rescaled);
        }
        #endregion

        #region UInt32 > T
        [Test]
        public void RescaleUInt32ToByte()
        {
            byte rescaled = ValueManipulation.Rescale<uint, byte>(value32);
            Assert.AreEqual(value8, rescaled);
        }
        [Test]
        public void RescaleUInt32ToUInt16()
        {
            ushort rescaled = ValueManipulation.Rescale<uint, ushort>(value32);
            Assert.AreEqual(value16, rescaled);
        }
        [Test]
        public void RescaleUInt32ToUInt32()
        {
            uint rescaled = ValueManipulation.Rescale<uint, uint>(value32);
            Assert.AreEqual(value32, rescaled);
        }
        [Test]
        public void RescaleUInt32ToUInt64()
        {
            ulong rescaled = ValueManipulation.Rescale<uint, ulong>(value32);
            Assert.AreEqual(value32, rescaled);
        }
        #endregion

        #region UInt64 > T
        [Test]
        public void RescaleUInt64ToByte()
        {
            byte rescaled = ValueManipulation.Rescale<ulong, byte>(value64);
            Assert.AreEqual(value8, rescaled);
        }
        [Test]
        public void RescaleUInt64ToUInt16()
        {
            ushort rescaled = ValueManipulation.Rescale<ulong, ushort>(value64);
            Assert.AreEqual(value16, rescaled);
        }
        [Test]
        public void RescaleUInt64ToUInt32()
        {
            uint rescaled = ValueManipulation.Rescale<ulong, uint>(value64);
            Assert.AreEqual(value32, rescaled);
        }
        [Test]
        public void RescaleUInt64ToUInt64()
        {
            ulong rescaled = ValueManipulation.Rescale<ulong, ulong>(value64);
            Assert.AreEqual(value64, rescaled);
        }
        #endregion
    }
}
