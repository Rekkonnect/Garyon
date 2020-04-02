using Garyon.QualityControl.Extensions;
using Garyon.Tests.Resources;
using NUnit.Framework;
using System.Diagnostics;
using System.Threading;
using static Garyon.Functions.PointerHelpers.SIMDPointerConversion;
using static Garyon.Tests.Resources.AssertionHelpers;

namespace Garyon.Tests.Extensions
{
    public class ArrayCopyingHelpersVector256Tests : ArrayCopyingExtensionsQualityControlAsset
    {
        #region byte[] -> T[]
        [Test]
        public unsafe void ByteToByteArrayUnsafe()
        {
            DebugAssertionHelpers.TestArrays(OriginalByteArray, TargetByteArray);
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!CopyToByteArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalByteArray[i] == TargetByteArray[i]);
        }
        [Test]
        public unsafe void ByteToInt16ArrayUnsafe()
        {
            DebugAssertionHelpers.TestArrays(OriginalByteArray, TargetInt16Array);
            fixed (byte* o = OriginalByteArray)
            fixed (short* t = TargetInt16Array)
                if (!CopyToInt16ArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalByteArray[i] == TargetInt16Array[i]);
        }
        [Test]
        public unsafe void ByteToInt32ArrayUnsafe()
        {
            DebugAssertionHelpers.TestArrays(OriginalByteArray, TargetInt32Array);
            fixed (byte* o = OriginalByteArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToInt32ArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalByteArray[i] == TargetInt32Array[i]);
        }
        [Test]
        public unsafe void ByteToInt64ArrayUnsafe()
        {
            DebugAssertionHelpers.TestArrays(OriginalByteArray, TargetInt64Array);
            fixed (byte* o = OriginalByteArray)
            fixed (long* t = TargetInt64Array)
                if (!CopyToInt64ArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalByteArray[i] == TargetInt64Array[i]);
        }
        [Test]
        public unsafe void ByteToSingleArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (float* t = TargetSingleArray)
                if (!CopyToSingleArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalByteArray[i] == TargetSingleArray[i]);
        }
        [Test]
        public unsafe void ByteToDoubleArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToDoubleArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalByteArray[i] == TargetDoubleArray[i]);
        }
        #endregion

        #region short[] -> T[]
        [Test]
        public unsafe void Int16ToInt16ArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!CopyToInt16ArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt16Array[i] == TargetInt16Array[i]);
        }
        [Test]
        public unsafe void Int16ToInt32ArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToInt32ArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt16Array[i] == TargetInt32Array[i]);
        }
        [Test]
        public unsafe void Int16ToInt64ArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToInt64ArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt16Array[i] == TargetInt64Array[i]);
        }
        [Test]
        public unsafe void Int16ToSingleArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToSingleArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt16Array[i] == TargetSingleArray[i]);
        }
        [Test]
        public unsafe void Int16ToDoubleArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToDoubleArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt16Array[i] == TargetDoubleArray[i]);
        }
        #endregion

        #region int[] -> T[]
        [Test]
        public unsafe void Int32ToInt32ArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToInt32ArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt32Array[i] == TargetInt32Array[i]);
        }
        [Test]
        public unsafe void Int32ToInt64ArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToInt64ArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt32Array[i] == TargetInt64Array[i]);
        }
        [Test]
        public unsafe void Int32ToSingleArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToSingleArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt32Array[i] == TargetSingleArray[i]);
        }
        [Test]
        public unsafe void Int32ToDoubleArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToDoubleArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt32Array[i] == TargetDoubleArray[i]);
        }
        #endregion

        #region long[] -> T[]
        [Test]
        public unsafe void Int64ToInt64ArrayUnsafe()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToInt64ArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt64Array[i] == TargetInt64Array[i]);
        }
        #endregion

        #region float[] -> T[]
        [Test]
        public unsafe void SingleToInt32ArrayUnsafe()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToInt32ArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalSingleArray[i] == TargetInt32Array[i]);
        }
        [Test]
        public unsafe void SingleToDoubleArrayUnsafe()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToDoubleArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalSingleArray[i] == TargetDoubleArray[i]);
        }
        #endregion

        #region double[] -> T[]
        [Test]
        public unsafe void DoubleToInt32ArrayUnsafe()
        {
            fixed (double* o = OriginalDoubleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToInt32ArrayVector256(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalDoubleArray[i] == TargetInt32Array[i]);
        }
        #endregion
    }
}