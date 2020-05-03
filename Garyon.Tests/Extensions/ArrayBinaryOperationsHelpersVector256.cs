using Garyon.QualityControl.Extensions;
using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerBinaryOperations;
using static Garyon.Tests.Resources.AssertionHelpers;

namespace Garyon.Tests.Extensions
{
    public class ArrayBinaryOperationsHelpersVector256 : ArrayManipulationExtensionsQualityControlAsset
    {
        private const byte mask = 11;

        #region NOT
        [Test]
        public unsafe void NOTByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!NOTArrayVector256Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~OriginalByteArray[i] == TargetByteArray[i]);
        }
        [Test]
        public unsafe void NOTInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!NOTArrayVector256Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~OriginalInt16Array[i] == TargetInt16Array[i]);
        }
        [Test]
        public unsafe void NOTInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!NOTArrayVector256Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~OriginalInt32Array[i] == TargetInt32Array[i]);
        }
        [Test]
        public unsafe void NOTInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!NOTArrayVector256Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~OriginalInt64Array[i] == TargetInt64Array[i]);
        }
        [Test]
        public unsafe void NOTSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!NOTArrayVector256Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~OriginalSByteArray[i] == TargetSByteArray[i]);
        }
        [Test]
        public unsafe void NOTUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!NOTArrayVector256Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~OriginalUInt16Array[i] == TargetUInt16Array[i]);
        }
        [Test]
        public unsafe void NOTUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!NOTArrayVector256Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~OriginalUInt32Array[i] == TargetUInt32Array[i]);
        }
        [Test]
        public unsafe void NOTUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!NOTArrayVector256Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~OriginalUInt64Array[i] == TargetUInt64Array[i]);
        }
        #endregion

        #region NAND
        [Test]
        public unsafe void NANDByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!NANDArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~(OriginalByteArray[i] & mask) == TargetByteArray[i]);
        }
        [Test]
        public unsafe void NANDInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!NANDArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~(OriginalInt16Array[i] & mask) == TargetInt16Array[i]);
        }
        [Test]
        public unsafe void NANDInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!NANDArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~(OriginalInt32Array[i] & mask) == TargetInt32Array[i]);
        }
        [Test]
        public unsafe void NANDInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!NANDArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~(OriginalInt64Array[i] & mask) == TargetInt64Array[i]);
        }
        [Test]
        public unsafe void NANDSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!NANDArrayVector256Generic(o, t, (sbyte)mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~(OriginalSByteArray[i] & mask) == TargetSByteArray[i]);
        }
        [Test]
        public unsafe void NANDUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!NANDArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~(OriginalUInt16Array[i] & mask) == TargetUInt16Array[i]);
        }
        [Test]
        public unsafe void NANDUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!NANDArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~(OriginalUInt32Array[i] & mask) == TargetUInt32Array[i]);
        }
        [Test]
        public unsafe void NANDUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!NANDArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(~(OriginalUInt64Array[i] & mask) == TargetUInt64Array[i]);
        }
        #endregion

        #region AND
        [Test]
        public unsafe void ANDByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!ANDArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalByteArray[i] & mask) == TargetByteArray[i]);
        }
        [Test]
        public unsafe void ANDInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!ANDArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalInt16Array[i] & mask) == TargetInt16Array[i]);
        }
        [Test]
        public unsafe void ANDInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!ANDArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalInt32Array[i] & mask) == TargetInt32Array[i]);
        }
        [Test]
        public unsafe void ANDInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!ANDArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalInt64Array[i] & mask) == TargetInt64Array[i]);
        }
        [Test]
        public unsafe void ANDSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!ANDArrayVector256Generic(o, t, (sbyte)mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalSByteArray[i] & mask) == TargetSByteArray[i]);
        }
        [Test]
        public unsafe void ANDUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!ANDArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalUInt16Array[i] & mask) == TargetUInt16Array[i]);
        }
        [Test]
        public unsafe void ANDUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!ANDArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalUInt32Array[i] & mask) == TargetUInt32Array[i]);
        }
        [Test]
        public unsafe void ANDUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!ANDArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalUInt64Array[i] & mask) == TargetUInt64Array[i]);
        }
        #endregion

        #region OR
        [Test]
        public unsafe void ORByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!ORArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalByteArray[i] | mask) == TargetByteArray[i]);
        }
        [Test]
        public unsafe void ORInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!ORArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalInt16Array[i] | mask) == TargetInt16Array[i]);
        }
        [Test]
        public unsafe void ORInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!ORArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalInt32Array[i] | mask) == TargetInt32Array[i]);
        }
        [Test]
        public unsafe void ORInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!ORArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalInt64Array[i] | mask) == TargetInt64Array[i]);
        }
        [Test]
        public unsafe void ORSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!ORArrayVector256Generic(o, t, (sbyte)mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalSByteArray[i] | mask) == TargetSByteArray[i]);
        }
        [Test]
        public unsafe void ORUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!ORArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalUInt16Array[i] | mask) == TargetUInt16Array[i]);
        }
        [Test]
        public unsafe void ORUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!ORArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalUInt32Array[i] | mask) == TargetUInt32Array[i]);
        }
        [Test]
        public unsafe void ORUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!ORArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalUInt64Array[i] | mask) == TargetUInt64Array[i]);
        }
        #endregion

        #region XOR
        [Test]
        public unsafe void XORByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!XORArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalByteArray[i] ^ mask) == TargetByteArray[i]);
        }
        [Test]
        public unsafe void XORInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!XORArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalInt16Array[i] ^ mask) == TargetInt16Array[i]);
        }
        [Test]
        public unsafe void XORInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!XORArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalInt32Array[i] ^ mask) == TargetInt32Array[i]);
        }
        [Test]
        public unsafe void XORInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!XORArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalInt64Array[i] ^ mask) == TargetInt64Array[i]);
        }
        [Test]
        public unsafe void XORSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!XORArrayVector256Generic(o, t, (sbyte)mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalSByteArray[i] ^ mask) == TargetSByteArray[i]);
        }
        [Test]
        public unsafe void XORUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!XORArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalUInt16Array[i] ^ mask) == TargetUInt16Array[i]);
        }
        [Test]
        public unsafe void XORUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!XORArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalUInt32Array[i] ^ mask) == TargetUInt32Array[i]);
        }
        [Test]
        public unsafe void XORUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!XORArrayVector256Generic(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue((OriginalUInt64Array[i] ^ mask) == TargetUInt64Array[i]);
        }
        #endregion
    }
}