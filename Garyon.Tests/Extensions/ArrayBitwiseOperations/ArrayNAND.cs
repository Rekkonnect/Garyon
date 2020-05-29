using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerBitwiseOperations;
using static Garyon.Tests.Resources.AssertionHelpers;

namespace Garyon.Tests.Extensions.ArrayBitwiseOperations
{
    public class ArrayNAND : ArrayBitwiseOperationsHelpersVector128
    {
        [Test]
        public unsafe void NANDByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!NANDArrayVector128(o, t, Mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((byte)~(OriginalByteArray[i] & Mask), TargetByteArray[i]);
        }
        [Test]
        public unsafe void NANDInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!NANDArrayVector128(o, t, Mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalInt16Array[i] & Mask), TargetInt16Array[i]);
        }
        [Test]
        public unsafe void NANDInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!NANDArrayVector128(o, t, Mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalInt32Array[i] & Mask), TargetInt32Array[i]);
        }
        [Test]
        public unsafe void NANDInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!NANDArrayVector128(o, t, Mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalInt64Array[i] & Mask), TargetInt64Array[i]);
        }
        [Test]
        public unsafe void NANDSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!NANDArrayVector128(o, t, (sbyte)Mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalSByteArray[i] & Mask), TargetSByteArray[i]);
        }
        [Test]
        public unsafe void NANDUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!NANDArrayVector128(o, t, Mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((ushort)~(OriginalUInt16Array[i] & Mask), TargetUInt16Array[i]);
        }
        [Test]
        public unsafe void NANDUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!NANDArrayVector128(o, t, Mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalUInt32Array[i] & Mask), TargetUInt32Array[i]);
        }
        [Test]
        public unsafe void NANDUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!NANDArrayVector128(o, t, Mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalUInt64Array[i] & Mask), TargetUInt64Array[i]);
        }
    }
}
