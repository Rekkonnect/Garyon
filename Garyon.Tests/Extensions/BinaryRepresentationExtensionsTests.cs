using Garyon.Extensions;
using NUnit.Framework;
using System;

namespace Garyon.Tests.Extensions
{
    public class BinaryRepresentationExtensionsTests
    {
        private const ulong  u64Value = 0b_00100100_00011010_11110101_00000101_01010101_10101010_01110010_00100101;
        private const uint   u32Value = 0b_01010101_10101010_01110010_00100101;
        private const ushort u16Value = 0b_01110010_00100101;
        private const byte    u8Value = 0b_00100101;

        private const long   i64Value =  (long)u64Value;
        private const int    i32Value =   (int)u32Value;
        private const short  i16Value = (short)u16Value;
        private const sbyte   i8Value = (sbyte)u8Value;

        private const string u64ValueRepresentation = "00100100_00011010_11110101_00000101_01010101_10101010_01110010_00100101";
        private const string u32ValueRepresentation = "01010101_10101010_01110010_00100101";
        private const string u16ValueRepresentation = "01110010_00100101";
        private const string  u8ValueRepresentation = "00100101";

        #region Binary Representation
        private static readonly int[] edgeTotalBitsValues = { 0, -1 };

        [Test]
        public void ByteBinaryRepresentationTest()
        {
            var expectedRepresentation = u8ValueRepresentation.Replace("_", "");
            var actualRepresentation = u8Value.GetBinaryRepresentation();
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            foreach (int edgeValue in edgeTotalBitsValues)
                Assert.Throws<ArgumentException>(() => default(byte).GetBinaryRepresentation(edgeValue));
        }
        [Test]
        public void SByteBinaryRepresentationTest()
        {
            var expectedRepresentation = u8ValueRepresentation.Replace("_", "");
            var actualRepresentation = i8Value.GetBinaryRepresentation();
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            foreach (int edgeValue in edgeTotalBitsValues)
                Assert.Throws<ArgumentException>(() => default(sbyte).GetBinaryRepresentation(edgeValue));
        }
        [Test]
        public void Int16BinaryRepresentationTest()
        {
            var expectedRepresentation = u16ValueRepresentation.Replace("_", "");
            var actualRepresentation = i16Value.GetBinaryRepresentation();
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            foreach (int edgeValue in edgeTotalBitsValues)
                Assert.Throws<ArgumentException>(() => default(short).GetBinaryRepresentation(edgeValue));
        }
        [Test]
        public void UInt16BinaryRepresentationTest()
        {
            var expectedRepresentation = u16ValueRepresentation.Replace("_", "");
            var actualRepresentation = u16Value.GetBinaryRepresentation();
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            foreach (int edgeValue in edgeTotalBitsValues)
                Assert.Throws<ArgumentException>(() => default(ushort).GetBinaryRepresentation(edgeValue));
        }
        [Test]
        public void Int32BinaryRepresentationTest()
        {
            var expectedRepresentation = u32ValueRepresentation.Replace("_", "");
            var actualRepresentation = i32Value.GetBinaryRepresentation();
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            foreach (int edgeValue in edgeTotalBitsValues)
                Assert.Throws<ArgumentException>(() => default(int).GetBinaryRepresentation(edgeValue));
        }
        [Test]
        public void UInt32BinaryRepresentationTest()
        {
            var expectedRepresentation = u32ValueRepresentation.Replace("_", "");
            var actualRepresentation = u32Value.GetBinaryRepresentation();
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            foreach (int edgeValue in edgeTotalBitsValues)
                Assert.Throws<ArgumentException>(() => default(uint).GetBinaryRepresentation(edgeValue));
        }
        [Test]
        public void Int64BinaryRepresentationTest()
        {
            var expectedRepresentation = u64ValueRepresentation.Replace("_", "");
            var actualRepresentation = i64Value.GetBinaryRepresentation();
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            foreach (int edgeValue in edgeTotalBitsValues)
                Assert.Throws<ArgumentException>(() => default(long).GetBinaryRepresentation(edgeValue));
        }
        [Test]
        public void UInt64BinaryRepresentationTest()
        {
            var expectedRepresentation = u64ValueRepresentation.Replace("_", "");
            var actualRepresentation = u64Value.GetBinaryRepresentation();
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            foreach (int edgeValue in edgeTotalBitsValues)
                Assert.Throws<ArgumentException>(() => default(ulong).GetBinaryRepresentation(edgeValue));
        }
        #endregion

        #region Grouped Binary Representation
        [Test]
        public void ByteGroupedBinaryRepresentationTest()
        {
            var baseExpectedRepresentation = u8ValueRepresentation.Replace('_', ' ');

            var expectedRepresentation = baseExpectedRepresentation;
            var actualRepresentation = u8Value.GetGroupedBinaryRepresentation(8);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[6..];
            actualRepresentation = u8Value.GetGroupedBinaryRepresentation(8, 8 - 6);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            Assert.Throws<ArgumentException>(() => default(byte).GetGroupedBinaryRepresentation(-1, -1));
        }
        [Test]
        public void SByteGroupedBinaryRepresentationTest()
        {
            var baseExpectedRepresentation = u8ValueRepresentation.Replace('_', ' ');

            var expectedRepresentation = baseExpectedRepresentation;
            var actualRepresentation = i8Value.GetGroupedBinaryRepresentation(8);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[6..];
            actualRepresentation = i8Value.GetGroupedBinaryRepresentation(8, 8 - 6);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            Assert.Throws<ArgumentException>(() => default(sbyte).GetGroupedBinaryRepresentation(-1, -1));
        }
        [Test]
        public void Int16GroupedBinaryRepresentationTest()
        {
            var baseExpectedRepresentation = u16ValueRepresentation.Replace('_', ' ');

            var expectedRepresentation = baseExpectedRepresentation;
            var actualRepresentation = i16Value.GetGroupedBinaryRepresentation(8);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[14..];
            actualRepresentation = i16Value.GetGroupedBinaryRepresentation(8, 16 - 13);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[9..];
            actualRepresentation = i16Value.GetGroupedBinaryRepresentation(8, 16 - 8);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[6..];
            actualRepresentation = i16Value.GetGroupedBinaryRepresentation(8, 16 - 6);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            Assert.Throws<ArgumentException>(() => default(short).GetGroupedBinaryRepresentation(-1, -1));
        }
        [Test]
        public void UInt16GroupedBinaryRepresentationTest()
        {
            var baseExpectedRepresentation = u16ValueRepresentation.Replace('_', ' ');

            var expectedRepresentation = baseExpectedRepresentation;
            var actualRepresentation = u16Value.GetGroupedBinaryRepresentation(8);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[14..];
            actualRepresentation = u16Value.GetGroupedBinaryRepresentation(8, 16 - 13);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[9..];
            actualRepresentation = u16Value.GetGroupedBinaryRepresentation(8, 16 - 8);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[6..];
            actualRepresentation = u16Value.GetGroupedBinaryRepresentation(8, 16 - 6);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            Assert.Throws<ArgumentException>(() => default(ushort).GetGroupedBinaryRepresentation(-1, -1));
        }
        [Test]
        public void Int32GroupedBinaryRepresentationTest()
        {
            var baseExpectedRepresentation = u32ValueRepresentation.Replace('_', ' ');

            var expectedRepresentation = baseExpectedRepresentation;
            var actualRepresentation = i32Value.GetGroupedBinaryRepresentation(8);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[27..];
            actualRepresentation = i32Value.GetGroupedBinaryRepresentation(8, 32 - 24);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[14..];
            actualRepresentation = i32Value.GetGroupedBinaryRepresentation(8, 32 - 13);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[6..];
            actualRepresentation = i32Value.GetGroupedBinaryRepresentation(8, 32 - 6);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            Assert.Throws<ArgumentException>(() => default(int).GetGroupedBinaryRepresentation(-1, -1));
        }
        [Test]
        public void UInt32GroupedBinaryRepresentationTest()
        {
            var baseExpectedRepresentation = u32ValueRepresentation.Replace('_', ' ');

            var expectedRepresentation = baseExpectedRepresentation;
            var actualRepresentation = u32Value.GetGroupedBinaryRepresentation(8);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[27..];
            actualRepresentation = u32Value.GetGroupedBinaryRepresentation(8, 32 - 24);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[14..];
            actualRepresentation = u32Value.GetGroupedBinaryRepresentation(8, 32 - 13);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[6..];
            actualRepresentation = u32Value.GetGroupedBinaryRepresentation(8, 32 - 6);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            Assert.Throws<ArgumentException>(() => default(uint).GetGroupedBinaryRepresentation(-1, -1));
        }
        [Test]
        public void Int64GroupedBinaryRepresentationTest()
        {
            var baseExpectedRepresentation = u64ValueRepresentation.Replace('_', ' ');

            var expectedRepresentation = baseExpectedRepresentation;
            var actualRepresentation = i64Value.GetGroupedBinaryRepresentation(8);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[27..];
            actualRepresentation = i64Value.GetGroupedBinaryRepresentation(8, 64 - 24);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[14..];
            actualRepresentation = i64Value.GetGroupedBinaryRepresentation(8, 64 - 13);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[6..];
            actualRepresentation = i64Value.GetGroupedBinaryRepresentation(8, 64 - 6);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            Assert.Throws<ArgumentException>(() => default(long).GetGroupedBinaryRepresentation(-1, -1));
        }
        [Test]
        public void UInt64GroupedBinaryRepresentationTest()
        {
            var baseExpectedRepresentation = u64ValueRepresentation.Replace('_', ' ');

            var expectedRepresentation = baseExpectedRepresentation;
            var actualRepresentation = u64Value.GetGroupedBinaryRepresentation(8);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[27..];
            actualRepresentation = u64Value.GetGroupedBinaryRepresentation(8, 64 - 24);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[14..];
            actualRepresentation = u64Value.GetGroupedBinaryRepresentation(8, 64 - 13);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = baseExpectedRepresentation[6..];
            actualRepresentation = u64Value.GetGroupedBinaryRepresentation(8, 64 - 6);
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            Assert.Throws<ArgumentException>(() => default(ulong).GetGroupedBinaryRepresentation(-1, -1));
        }
        #endregion
    }
}
