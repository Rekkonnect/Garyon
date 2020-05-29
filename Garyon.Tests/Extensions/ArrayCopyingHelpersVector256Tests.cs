using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerConversion;

namespace Garyon.Tests.Extensions
{
    public class ArrayCopyingHelpersVector256Tests : ArrayCopyingHelpersTestsBase
    {
        #region byte[] -> T[]
        [Test]
        public unsafe void ByteToByteArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetByteArray, CopyToByteArrayVector256);
        }
        [Test]
        public unsafe void ByteToInt16ArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetInt16Array, CopyToInt16ArrayVector256);
        }
        [Test]
        public unsafe void ByteToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetInt32Array, CopyToInt32ArrayVector256);
        }
        [Test]
        public unsafe void ByteToInt64ArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetInt64Array, CopyToInt64ArrayVector256);
        }
        [Test]
        public unsafe void ByteToSingleArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetSingleArray, CopyToSingleArrayVector256);
        }
        [Test]
        public unsafe void ByteToDoubleArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetDoubleArray, CopyToDoubleArrayVector256);
        }
        #endregion

        #region short[] -> T[]
        [Test]
        public unsafe void Int16ToInt16ArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetInt16Array, CopyToInt16ArrayVector256);
        }
        [Test]
        public unsafe void Int16ToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetInt32Array, CopyToInt32ArrayVector256);
        }
        [Test]
        public unsafe void Int16ToInt64ArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetInt64Array, CopyToInt64ArrayVector256);
        }
        [Test]
        public unsafe void Int16ToSingleArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetSingleArray, CopyToSingleArrayVector256);
        }
        [Test]
        public unsafe void Int16ToDoubleArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetDoubleArray, CopyToDoubleArrayVector256);
        }
        #endregion

        #region int[] -> T[]
        [Test]
        public unsafe void Int32ToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalInt32Array, TargetInt32Array, CopyToInt32ArrayVector256);
        }
        [Test]
        public unsafe void Int32ToInt64ArrayUnsafe()
        {
            PerformManipulation(OriginalInt32Array, TargetInt64Array, CopyToInt64ArrayVector256);
        }
        [Test]
        public unsafe void Int32ToSingleArrayUnsafe()
        {
            PerformManipulation(OriginalInt32Array, TargetSingleArray, CopyToSingleArrayVector256);
        }
        [Test]
        public unsafe void Int32ToDoubleArrayUnsafe()
        {
            PerformManipulation(OriginalInt32Array, TargetDoubleArray, CopyToDoubleArrayVector256);
        }
        #endregion

        #region long[] -> T[]
        [Test]
        public unsafe void Int64ToInt64ArrayUnsafe()
        {
            PerformManipulation(OriginalInt64Array, TargetInt64Array, CopyToInt64ArrayVector256);
        }
        #endregion

        #region float[] -> T[]
        [Test]
        public unsafe void SingleToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalSingleArray, TargetInt32Array, CopyToInt32ArrayVector256);
        }
        [Test]
        public unsafe void SingleToDoubleArrayUnsafe()
        {
            PerformManipulation(OriginalSingleArray, TargetDoubleArray, CopyToDoubleArrayVector256);
        }
        #endregion

        #region double[] -> T[]
        [Test]
        public unsafe void DoubleToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalDoubleArray, TargetInt32Array, CopyToInt32ArrayVector256);
        }
        #endregion
    }
}
