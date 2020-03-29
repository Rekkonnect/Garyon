using Garyon.Extensions;
using Garyon.QualityControl.Extensions;
using NUnit.Framework;
using static Garyon.Extensions.ArrayCasting.UnsafeArrayCopyingHelpers;
using static Garyon.Tests.Resources.AssertionHelpers;

namespace Garyon.Tests.Extensions
{
    public class ArrayCopyingHelpersGenericVector128Tests : ArrayCopyingExtensionsQualityControlAsset
    {
        #region byte[] -> T[]
        [Test]
        public unsafe void ByteToByteArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalByteArray.GetPointer(), TargetByteArray.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalByteArray[i] == TargetByteArray[i]);
        }
        [Test]
        public unsafe void ByteToInt16ArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalByteArray.GetPointer(), TargetInt16Array.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalByteArray[i] == TargetInt16Array[i]);
        }
        [Test]
        public unsafe void ByteToInt32ArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalByteArray.GetPointer(), TargetInt32Array.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalByteArray[i] == TargetInt32Array[i]);
        }
        [Test]
        public unsafe void ByteToInt64ArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalByteArray.GetPointer(), TargetInt64Array.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalByteArray[i] == TargetInt64Array[i]);
        }
        [Test]
        public unsafe void ByteToSingleArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalByteArray.GetPointer(), TargetSingleArray.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalByteArray[i] == TargetSingleArray[i]);
        }
        [Test]
        public unsafe void ByteToDoubleArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalByteArray.GetPointer(), TargetDoubleArray.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalByteArray[i] == TargetDoubleArray[i]);
        }
        #endregion

        #region short[] -> T[]
        [Test]
        public unsafe void Int16ToByteArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt16Array.GetPointer(), TargetByteArray.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt16Array[i] == TargetByteArray[i]);
        }
        [Test]
        public unsafe void Int16ToInt16ArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt16Array.GetPointer(), TargetInt16Array.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt16Array[i] == TargetInt16Array[i]);
        }
        [Test]
        public unsafe void Int16ToInt32ArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt16Array.GetPointer(), TargetInt32Array.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt16Array[i] == TargetInt32Array[i]);
        }
        [Test]
        public unsafe void Int16ToInt64ArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt16Array.GetPointer(), TargetInt64Array.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt16Array[i] == TargetInt64Array[i]);
        }
        [Test]
        public unsafe void Int16ToSingleArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt16Array.GetPointer(), TargetSingleArray.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt16Array[i] == TargetSingleArray[i]);
        }
        [Test]
        public unsafe void Int16ToDoubleArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt16Array.GetPointer(), TargetDoubleArray.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt16Array[i] == TargetDoubleArray[i]);
        }
        #endregion

        #region int[] -> T[]
        [Test]
        public unsafe void Int32ToByteArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt32Array.GetPointer(), TargetByteArray.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt32Array[i] == TargetByteArray[i]);
        }
        [Test]
        public unsafe void Int32ToInt16ArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt32Array.GetPointer(), TargetInt16Array.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt32Array[i] == TargetInt16Array[i]);
        }
        [Test]
        public unsafe void Int32ToInt32ArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt32Array.GetPointer(), TargetInt32Array.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt32Array[i] == TargetInt32Array[i]);
        }
        [Test]
        public unsafe void Int32ToInt64ArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt32Array.GetPointer(), TargetInt64Array.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt32Array[i] == TargetInt64Array[i]);
        }
        [Test]
        public unsafe void Int32ToSingleArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt32Array.GetPointer(), TargetSingleArray.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt32Array[i] == TargetSingleArray[i]);
        }
        [Test]
        public unsafe void Int32ToDoubleArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt32Array.GetPointer(), TargetDoubleArray.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt32Array[i] == TargetDoubleArray[i]);
        }
        #endregion

        #region long[] -> T[]
        [Test]
        public unsafe void Int64ToByteArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt64Array.GetPointer(), TargetByteArray.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt64Array[i] == TargetByteArray[i]);
        }
        [Test]
        public unsafe void Int64ToInt16ArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt64Array.GetPointer(), TargetInt16Array.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt64Array[i] == TargetInt16Array[i]);
        }
        [Test]
        public unsafe void Int64ToInt32ArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt64Array.GetPointer(), TargetInt32Array.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt64Array[i] == TargetInt32Array[i]);
        }
        [Test]
        public unsafe void Int64ToInt64ArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalInt64Array.GetPointer(), TargetInt64Array.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalInt64Array[i] == TargetInt64Array[i]);
        }
        #endregion

        #region float[] -> T[]
        [Test]
        public unsafe void SingleToInt32ArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalSingleArray.GetPointer(), TargetInt32Array.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalSingleArray[i] == TargetInt32Array[i]);
        }
        [Test]
        public unsafe void SingleToDoubleArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalSingleArray.GetPointer(), TargetDoubleArray.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalSingleArray[i] == TargetDoubleArray[i]);
        }
        #endregion

        #region double[] -> T[]
        [Test]
        public unsafe void DoubleToInt32ArrayUnsafe()
        {
            if (!CopyToArrayVector128Generic(OriginalDoubleArray.GetPointer(), TargetInt32Array.GetPointer(), ArrayLength))
                UnsupportedInstructionSet();

            for (int i = 0; i < ArrayLength; i++)
                Assert.IsTrue(OriginalDoubleArray[i] == TargetInt32Array[i]);
        }
        #endregion
    }
}