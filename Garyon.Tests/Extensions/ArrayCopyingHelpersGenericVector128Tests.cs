using Garyon.Extensions;
using Garyon.Extensions.ArrayExtensions;
using Garyon.QualityControl.Extensions;
using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerConversion;
using static Garyon.Tests.Resources.AssertionHelpers;

namespace Garyon.Tests.Extensions
{
    public class ArrayCopyingHelpersGenericVector128Tests : ArrayManipulationExtensionsQualityControlAsset
    {
        #region byte[] -> T[]
        [Test]
        public unsafe void ByteToByteArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalByteArray[i], TargetByteArray[i]);
        }
        [Test]
        public unsafe void ByteToInt16ArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalByteArray[i], TargetInt16Array[i]);
        }
        [Test]
        public unsafe void ByteToInt32ArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalByteArray[i], TargetInt32Array[i]);
        }
        [Test]
        public unsafe void ByteToInt64ArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalByteArray[i], TargetInt64Array[i]);
        }
        [Test]
        public unsafe void ByteToSingleArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalByteArray[i], TargetSingleArray[i]);
        }
        [Test]
        public unsafe void ByteToDoubleArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalByteArray[i], TargetDoubleArray[i]);
        }
        #endregion

        #region short[] -> T[]
        [Test]
        public unsafe void Int16ToByteArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (byte* t = TargetByteArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt16Array[i], TargetByteArray[i]);
        }
        [Test]
        public unsafe void Int16ToInt16ArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt16Array[i], TargetInt16Array[i]);
        }
        [Test]
        public unsafe void Int16ToInt32ArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt16Array[i], TargetInt32Array[i]);
        }
        [Test]
        public unsafe void Int16ToInt64ArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt16Array[i], TargetInt64Array[i]);
        }
        [Test]
        public unsafe void Int16ToSingleArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt16Array[i], TargetSingleArray[i]);
        }
        [Test]
        public unsafe void Int16ToDoubleArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt16Array[i], TargetDoubleArray[i]);
        }
        #endregion

        #region int[] -> T[]
        [Test]
        public unsafe void Int32ToByteArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (byte* t = TargetByteArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt32Array[i], TargetByteArray[i]);
        }
        [Test]
        public unsafe void Int32ToInt16ArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt32Array[i], TargetInt16Array[i]);
        }
        [Test]
        public unsafe void Int32ToInt32ArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt32Array[i], TargetInt32Array[i]);
        }
        [Test]
        public unsafe void Int32ToInt64ArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt32Array[i], TargetInt64Array[i]);
        }
        [Test]
        public unsafe void Int32ToSingleArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt32Array[i], TargetSingleArray[i]);
        }
        [Test]
        public unsafe void Int32ToDoubleArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt32Array[i], TargetDoubleArray[i]);
        }
        #endregion

        #region long[] -> T[]
        [Test]
        public unsafe void Int64ToByteArrayUnsafe()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (byte* t = TargetByteArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt64Array[i], TargetByteArray[i]);
        }
        [Test]
        public unsafe void Int64ToInt16ArrayUnsafe()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt64Array[i], TargetInt16Array[i]);
        }
        [Test]
        public unsafe void Int64ToInt32ArrayUnsafe()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt64Array[i], TargetInt32Array[i]);
        }
        [Test]
        public unsafe void Int64ToInt64ArrayUnsafe()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalInt64Array[i], TargetInt64Array[i]);
        }
        #endregion

        #region float[] -> T[]
        [Test]
        public unsafe void SingleToInt32ArrayUnsafe()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalSingleArray[i], TargetInt32Array[i]);
        }
        [Test]
        public unsafe void SingleToDoubleArrayUnsafe()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalSingleArray[i], TargetDoubleArray[i]);
        }
        #endregion

        #region double[] -> T[]
        [Test]
        public unsafe void DoubleToInt32ArrayUnsafe()
        {
            fixed (double* o = OriginalDoubleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.AreEqual(OriginalDoubleArray[i], TargetInt32Array[i]);
        }
        #endregion
    }
}
