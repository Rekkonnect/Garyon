using Garyon.QualityControl.Extensions;
using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerConversion;
using static Garyon.Tests.Resources.AssertionHelpers;

namespace Garyon.Tests.Extensions
{
    public class ArrayCopyingHelpersVector128Tests : ArrayCopyingHelpersTestsBase
    {
        #region byte[] -> T[]
        [Test]
        public unsafe void ByteToByteArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetByteArray, CopyToByteArrayVector128);
        }
        [Test]
        public unsafe void ByteToInt16ArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetInt16Array, CopyToInt16ArrayVector128);
        }
        [Test]
        public unsafe void ByteToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetInt32Array, CopyToInt32ArrayVector128);
        }
        [Test]
        public unsafe void ByteToInt64ArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetInt64Array, CopyToInt64ArrayVector128);
        }
        [Test]
        public unsafe void ByteToSingleArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetSingleArray, CopyToSingleArrayVector128);
        }
        [Test]
        public unsafe void ByteToDoubleArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetDoubleArray, CopyToDoubleArrayVector128);
        }
        #endregion

        #region short[] -> T[]
        [Test]
        public unsafe void Int16ToByteArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetByteArray, CopyToByteArrayVector128);
        }
        [Test]
        public unsafe void Int16ToInt16ArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetInt16Array, CopyToInt16ArrayVector128);
        }
        [Test]
        public unsafe void Int16ToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetInt32Array, CopyToInt32ArrayVector128);
        }
        [Test]
        public unsafe void Int16ToInt64ArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetInt64Array, CopyToInt64ArrayVector128);
        }
        [Test]
        public unsafe void Int16ToSingleArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetSingleArray, CopyToSingleArrayVector128);
        }
        [Test]
        public unsafe void Int16ToDoubleArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetDoubleArray, CopyToDoubleArrayVector128);
        }
        #endregion

        #region int[] -> T[]
        [Test]
        public unsafe void Int32ToByteArrayUnsafe()
        {
            PerformManipulation(OriginalInt32Array, TargetByteArray, CopyToByteArrayVector128);
        }
        [Test]
        public unsafe void Int32ToInt16ArrayUnsafe()
        {
            PerformManipulation(OriginalInt32Array, TargetInt16Array, CopyToInt16ArrayVector128);
        }
        [Test]
        public unsafe void Int32ToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalInt32Array, TargetInt32Array, CopyToInt32ArrayVector128);
        }
        [Test]
        public unsafe void Int32ToInt64ArrayUnsafe()
        {
            PerformManipulation(OriginalInt32Array, TargetInt64Array, CopyToInt64ArrayVector128);
        }
        [Test]
        public unsafe void Int32ToSingleArrayUnsafe()
        {
            PerformManipulation(OriginalInt32Array, TargetSingleArray, CopyToSingleArrayVector128);
        }
        [Test]
        public unsafe void Int32ToDoubleArrayUnsafe()
        {
            PerformManipulation(OriginalInt32Array, TargetDoubleArray, CopyToDoubleArrayVector128);
        }
        #endregion

        #region long[] -> T[]
        [Test]
        public unsafe void Int64ToByteArrayUnsafe()
        {
            PerformManipulation(OriginalInt64Array, TargetByteArray, CopyToByteArrayVector128);
        }
        [Test]
        public unsafe void Int64ToInt16ArrayUnsafe()
        {
            PerformManipulation(OriginalInt64Array, TargetInt16Array, CopyToInt16ArrayVector128);
        }
        [Test]
        public unsafe void Int64ToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalInt64Array, TargetInt32Array, CopyToInt32ArrayVector128);
        }
        [Test]
        public unsafe void Int64ToInt64ArrayUnsafe()
        {
            PerformManipulation(OriginalInt64Array, TargetInt64Array, CopyToInt64ArrayVector128);
        }
        #endregion

        #region float[] -> T[]
        [Test]
        public unsafe void SingleToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalSingleArray, TargetInt32Array, CopyToInt32ArrayVector128);
        }
        [Test]
        public unsafe void SingleToDoubleArrayUnsafe()
        {
            PerformManipulation(OriginalSingleArray, TargetDoubleArray, CopyToDoubleArrayVector128);
        }
        #endregion

        #region double[] -> T[]
        [Test]
        public unsafe void DoubleToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalDoubleArray, TargetInt32Array, CopyToInt32ArrayVector128);
        }
        #endregion
    }
}
