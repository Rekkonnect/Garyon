using Garyon.QualityControl.Extensions;
using Garyon.QualityControl.SizedStructs;
using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerBitwiseOperations;
using static Garyon.Tests.Resources.AssertionHelpers;

namespace Garyon.Tests.Extensions
{
    public class ArrayBitwiseOperationsHelpersVector128 : ArrayManipulationExtensionsQualityControlAsset
    {
        private const byte mask = 11;

        #region Custom Sized Structs NOT
        [Test]
        public unsafe void NOTStruct3Array()
        {
            var origin = new Struct3[ArrayLength];
            var target = new Struct3[ArrayLength];
            fixed (Struct3* o = origin)
            fixed (Struct3* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct5Array()
        {
            var origin = new Struct5[ArrayLength];
            var target = new Struct5[ArrayLength];
            fixed (Struct5* o = origin)
            fixed (Struct5* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct6Array()
        {
            var origin = new Struct6[ArrayLength];
            var target = new Struct6[ArrayLength];
            fixed (Struct6* o = origin)
            fixed (Struct6* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct7Array()
        {
            var origin = new Struct7[ArrayLength];
            var target = new Struct7[ArrayLength];
            fixed (Struct7* o = origin)
            fixed (Struct7* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct9Array()
        {
            var origin = new Struct9[ArrayLength];
            var target = new Struct9[ArrayLength];
            fixed (Struct9* o = origin)
            fixed (Struct9* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct10Array()
        {
            var origin = new Struct10[ArrayLength];
            var target = new Struct10[ArrayLength];
            fixed (Struct10* o = origin)
            fixed (Struct10* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct11Array()
        {
            var origin = new Struct11[ArrayLength];
            var target = new Struct11[ArrayLength];
            fixed (Struct11* o = origin)
            fixed (Struct11* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct12Array()
        {
            var origin = new Struct12[ArrayLength];
            var target = new Struct12[ArrayLength];
            fixed (Struct12* o = origin)
            fixed (Struct12* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct13Array()
        {
            var origin = new Struct13[ArrayLength];
            var target = new Struct13[ArrayLength];
            fixed (Struct13* o = origin)
            fixed (Struct13* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct14Array()
        {
            var origin = new Struct14[ArrayLength];
            var target = new Struct14[ArrayLength];
            fixed (Struct14* o = origin)
            fixed (Struct14* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct15Array()
        {
            var origin = new Struct15[ArrayLength];
            var target = new Struct15[ArrayLength];
            fixed (Struct15* o = origin)
            fixed (Struct15* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct17Array()
        {
            var origin = new Struct17[ArrayLength];
            var target = new Struct17[ArrayLength];
            fixed (Struct17* o = origin)
            fixed (Struct17* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct18Array()
        {
            var origin = new Struct18[ArrayLength];
            var target = new Struct18[ArrayLength];
            fixed (Struct18* o = origin)
            fixed (Struct18* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct19Array()
        {
            var origin = new Struct19[ArrayLength];
            var target = new Struct19[ArrayLength];
            fixed (Struct19* o = origin)
            fixed (Struct19* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct20Array()
        {
            var origin = new Struct20[ArrayLength];
            var target = new Struct20[ArrayLength];
            fixed (Struct20* o = origin)
            fixed (Struct20* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct21Array()
        {
            var origin = new Struct21[ArrayLength];
            var target = new Struct21[ArrayLength];
            fixed (Struct21* o = origin)
            fixed (Struct21* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct22Array()
        {
            var origin = new Struct22[ArrayLength];
            var target = new Struct22[ArrayLength];
            fixed (Struct22* o = origin)
            fixed (Struct22* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct23Array()
        {
            var origin = new Struct23[ArrayLength];
            var target = new Struct23[ArrayLength];
            fixed (Struct23* o = origin)
            fixed (Struct23* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct24Array()
        {
            var origin = new Struct24[ArrayLength];
            var target = new Struct24[ArrayLength];
            fixed (Struct24* o = origin)
            fixed (Struct24* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct25Array()
        {
            var origin = new Struct25[ArrayLength];
            var target = new Struct25[ArrayLength];
            fixed (Struct25* o = origin)
            fixed (Struct25* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct26Array()
        {
            var origin = new Struct26[ArrayLength];
            var target = new Struct26[ArrayLength];
            fixed (Struct26* o = origin)
            fixed (Struct26* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct27Array()
        {
            var origin = new Struct27[ArrayLength];
            var target = new Struct27[ArrayLength];
            fixed (Struct27* o = origin)
            fixed (Struct27* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct28Array()
        {
            var origin = new Struct28[ArrayLength];
            var target = new Struct28[ArrayLength];
            fixed (Struct28* o = origin)
            fixed (Struct28* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct29Array()
        {
            var origin = new Struct29[ArrayLength];
            var target = new Struct29[ArrayLength];
            fixed (Struct29* o = origin)
            fixed (Struct29* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct30Array()
        {
            var origin = new Struct30[ArrayLength];
            var target = new Struct30[ArrayLength];
            fixed (Struct30* o = origin)
            fixed (Struct30* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        [Test]
        public unsafe void NOTStruct31Array()
        {
            var origin = new Struct31[ArrayLength];
            var target = new Struct31[ArrayLength];
            fixed (Struct31* o = origin)
            fixed (Struct31* t = target)
                if (!NOTArrayVector128CustomType(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~origin[i], target[i]);
        }
        #endregion

        #region NOT
        [Test]
        public unsafe void NOTByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!NOTArrayVector128(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((byte)~OriginalByteArray[i], TargetByteArray[i]);
        }
        [Test]
        public unsafe void NOTInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!NOTArrayVector128(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~OriginalInt16Array[i], TargetInt16Array[i]);
        }
        [Test]
        public unsafe void NOTInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!NOTArrayVector128(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~OriginalInt32Array[i], TargetInt32Array[i]);
        }
        [Test]
        public unsafe void NOTInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!NOTArrayVector128(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~OriginalInt64Array[i], TargetInt64Array[i]);
        }
        [Test]
        public unsafe void NOTSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!NOTArrayVector128(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~OriginalSByteArray[i], TargetSByteArray[i]);
        }
        [Test]
        public unsafe void NOTUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!NOTArrayVector128(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((ushort)~OriginalUInt16Array[i], TargetUInt16Array[i]);
        }
        [Test]
        public unsafe void NOTUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!NOTArrayVector128(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~OriginalUInt32Array[i], TargetUInt32Array[i]);
        }
        [Test]
        public unsafe void NOTUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!NOTArrayVector128(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~OriginalUInt64Array[i], TargetUInt64Array[i]);
        }
        #endregion

        #region AND
        [Test]
        public unsafe void ANDByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!ANDArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalByteArray[i] & mask), TargetByteArray[i]);
        }
        [Test]
        public unsafe void ANDInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!ANDArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalInt16Array[i] & mask), TargetInt16Array[i]);
        }
        [Test]
        public unsafe void ANDInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!ANDArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalInt32Array[i] & mask), TargetInt32Array[i]);
        }
        [Test]
        public unsafe void ANDInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!ANDArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalInt64Array[i] & mask), TargetInt64Array[i]);
        }
        [Test]
        public unsafe void ANDSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!ANDArrayVector128(o, t, (sbyte)mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalSByteArray[i] & mask), TargetSByteArray[i]);
        }
        [Test]
        public unsafe void ANDUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!ANDArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalUInt16Array[i] & mask), TargetUInt16Array[i]);
        }
        [Test]
        public unsafe void ANDUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!ANDArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalUInt32Array[i] & mask), TargetUInt32Array[i]);
        }
        [Test]
        public unsafe void ANDUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!ANDArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalUInt64Array[i] & mask), TargetUInt64Array[i]);
        }
        #endregion

        #region OR
        [Test]
        public unsafe void ORByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!ORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalByteArray[i] | mask), TargetByteArray[i]);
        }
        [Test]
        public unsafe void ORInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!ORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalInt16Array[i] | mask), TargetInt16Array[i]);
        }
        [Test]
        public unsafe void ORInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!ORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalInt32Array[i] | mask), TargetInt32Array[i]);
        }
        [Test]
        public unsafe void ORInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!ORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalInt64Array[i] | mask), TargetInt64Array[i]);
        }
        [Test]
        public unsafe void ORSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!ORArrayVector128(o, t, (sbyte)mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalSByteArray[i] | mask), TargetSByteArray[i]);
        }
        [Test]
        public unsafe void ORUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!ORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalUInt16Array[i] | mask), TargetUInt16Array[i]);
        }
        [Test]
        public unsafe void ORUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!ORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalUInt32Array[i] | mask), TargetUInt32Array[i]);
        }
        [Test]
        public unsafe void ORUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!ORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalUInt64Array[i] | mask), TargetUInt64Array[i]);
        }
        #endregion

        #region XOR
        [Test]
        public unsafe void XORByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!XORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalByteArray[i] ^ mask), TargetByteArray[i]);
        }
        [Test]
        public unsafe void XORInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!XORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalInt16Array[i] ^ mask), TargetInt16Array[i]);
        }
        [Test]
        public unsafe void XORInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!XORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalInt32Array[i] ^ mask), TargetInt32Array[i]);
        }
        [Test]
        public unsafe void XORInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!XORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalInt64Array[i] ^ mask), TargetInt64Array[i]);
        }
        [Test]
        public unsafe void XORSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!XORArrayVector128(o, t, (sbyte)mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalSByteArray[i] ^ mask), TargetSByteArray[i]);
        }
        [Test]
        public unsafe void XORUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!XORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalUInt16Array[i] ^ mask), TargetUInt16Array[i]);
        }
        [Test]
        public unsafe void XORUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!XORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalUInt32Array[i] ^ mask), TargetUInt32Array[i]);
        }
        [Test]
        public unsafe void XORUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!XORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((OriginalUInt64Array[i] ^ mask), TargetUInt64Array[i]);
        }
        #endregion

        #region NAND
        [Test]
        public unsafe void NANDByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!NANDArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((byte)~(OriginalByteArray[i] & mask), TargetByteArray[i]);
        }
        [Test]
        public unsafe void NANDInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!NANDArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalInt16Array[i] & mask), TargetInt16Array[i]);
        }
        [Test]
        public unsafe void NANDInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!NANDArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalInt32Array[i] & mask), TargetInt32Array[i]);
        }
        [Test]
        public unsafe void NANDInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!NANDArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalInt64Array[i] & mask), TargetInt64Array[i]);
        }
        [Test]
        public unsafe void NANDSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!NANDArrayVector128(o, t, (sbyte)mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalSByteArray[i] & mask), TargetSByteArray[i]);
        }
        [Test]
        public unsafe void NANDUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!NANDArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((ushort)~(OriginalUInt16Array[i] & mask), TargetUInt16Array[i]);
        }
        [Test]
        public unsafe void NANDUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!NANDArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalUInt32Array[i] & mask), TargetUInt32Array[i]);
        }
        [Test]
        public unsafe void NANDUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!NANDArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalUInt64Array[i] & mask), TargetUInt64Array[i]);
        }
        #endregion

        #region NOR
        [Test]
        public unsafe void NORByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!NORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((byte)~(OriginalByteArray[i] | mask), TargetByteArray[i]);
        }
        [Test]
        public unsafe void NORInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!NORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalInt16Array[i] | mask), TargetInt16Array[i]);
        }
        [Test]
        public unsafe void NORInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!NORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalInt32Array[i] | mask), TargetInt32Array[i]);
        }
        [Test]
        public unsafe void NORInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!NORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalInt64Array[i] | mask), TargetInt64Array[i]);
        }
        [Test]
        public unsafe void NORSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!NORArrayVector128(o, t, (sbyte)mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalSByteArray[i] | mask), TargetSByteArray[i]);
        }
        [Test]
        public unsafe void NORUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!NORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((ushort)~(OriginalUInt16Array[i] | mask), TargetUInt16Array[i]);
        }
        [Test]
        public unsafe void NORUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!NORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalUInt32Array[i] | mask), TargetUInt32Array[i]);
        }
        [Test]
        public unsafe void NORUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!NORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalUInt64Array[i] | mask), TargetUInt64Array[i]);
        }
        #endregion

        #region XNOR
        [Test]
        public unsafe void XNORByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!XNORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((byte)~(OriginalByteArray[i] ^ mask), TargetByteArray[i]);
        }
        [Test]
        public unsafe void XNORInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!XNORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalInt16Array[i] ^ mask), TargetInt16Array[i]);
        }
        [Test]
        public unsafe void XNORInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!XNORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalInt32Array[i] ^ mask), TargetInt32Array[i]);
        }
        [Test]
        public unsafe void XNORInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!XNORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalInt64Array[i] ^ mask), TargetInt64Array[i]);
        }
        [Test]
        public unsafe void XNORSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!XNORArrayVector128(o, t, (sbyte)mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalSByteArray[i] ^ mask), TargetSByteArray[i]);
        }
        [Test]
        public unsafe void XNORUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!XNORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual((ushort)~(OriginalUInt16Array[i] ^ mask), TargetUInt16Array[i]);
        }
        [Test]
        public unsafe void XNORUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!XNORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalUInt32Array[i] ^ mask), TargetUInt32Array[i]);
        }
        [Test]
        public unsafe void XNORUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!XNORArrayVector128(o, t, mask, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(~(OriginalUInt64Array[i] ^ mask), TargetUInt64Array[i]);
        }
        #endregion
    }
}
