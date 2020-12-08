using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerConversion;

namespace Garyon.Tests.Extensions
{
    public class ArrayCopyingHelpersGenericVector256Tests : ArrayCopyingHelpersTestsBase
    {
        protected unsafe override ArrayManipulationOperation<TOrigin, TTarget> GetArrayManipulationOperationDelegate<TOrigin, TTarget>() => CopyToArrayVector256Generic;

        #region byte[] -> T[]
        [Test]
        public unsafe void ByteToByteArrayUnsafe()
        {
            CopyArray(OriginalByteArray, TargetByteArray);
        }
        [Test]
        public unsafe void ByteToInt16ArrayUnsafe()
        {
            CopyArray(OriginalByteArray, TargetInt16Array);
        }
        [Test]
        public unsafe void ByteToInt32ArrayUnsafe()
        {
            CopyArray(OriginalByteArray, TargetInt32Array);
        }
        [Test]
        public unsafe void ByteToInt64ArrayUnsafe()
        {
            CopyArray(OriginalByteArray, TargetInt64Array);
        }
        [Test]
        public unsafe void ByteToSingleArrayUnsafe()
        {
            CopyArray(OriginalByteArray, TargetSingleArray);
        }
        [Test]
        public unsafe void ByteToDoubleArrayUnsafe()
        {
            CopyArray(OriginalByteArray, TargetDoubleArray);
        }
        #endregion

        #region short[] -> T[]
        [Test]
        public unsafe void Int16ToInt16ArrayUnsafe()
        {
            CopyArray(OriginalInt16Array, TargetInt16Array);
        }
        [Test]
        public unsafe void Int16ToInt32ArrayUnsafe()
        {
            CopyArray(OriginalInt16Array, TargetInt32Array);
        }
        [Test]
        public unsafe void Int16ToInt64ArrayUnsafe()
        {
            CopyArray(OriginalInt16Array, TargetInt64Array);
        }
        [Test]
        public unsafe void Int16ToSingleArrayUnsafe()
        {
            CopyArray(OriginalInt16Array, TargetSingleArray);
        }
        [Test]
        public unsafe void Int16ToDoubleArrayUnsafe()
        {
            CopyArray(OriginalInt16Array, TargetDoubleArray);
        }
        #endregion

        #region int[] -> T[]
        [Test]
        public unsafe void Int32ToInt32ArrayUnsafe()
        {
            CopyArray(OriginalInt32Array, TargetInt32Array);
        }
        [Test]
        public unsafe void Int32ToInt64ArrayUnsafe()
        {
            CopyArray(OriginalInt32Array, TargetInt64Array);
        }
        [Test]
        public unsafe void Int32ToSingleArrayUnsafe()
        {
            CopyArray(OriginalInt32Array, TargetSingleArray);
        }
        [Test]
        public unsafe void Int32ToDoubleArrayUnsafe()
        {
            CopyArray(OriginalInt32Array, TargetDoubleArray);
        }
        #endregion

        #region long[] -> T[]
        [Test]
        public unsafe void Int64ToInt64ArrayUnsafe()
        {
            CopyArray(OriginalInt64Array, TargetInt64Array);
        }
        #endregion

        #region float[] -> T[]
        [Test]
        public unsafe void SingleToInt32ArrayUnsafe()
        {
            CopyArray(OriginalSingleArray, TargetInt32Array);
        }
        [Test]
        public unsafe void SingleToSingleArrayUnsafe()
        {
            CopyArray(OriginalSingleArray, TargetSingleArray);
        }
        [Test]
        public unsafe void SingleToDoubleArrayUnsafe()
        {
            CopyArray(OriginalSingleArray, TargetDoubleArray);
        }
        #endregion

        #region double[] -> T[]
        [Test]
        public unsafe void DoubleToInt32ArrayUnsafe()
        {
            CopyArray(OriginalDoubleArray, TargetInt32Array);
        }
        [Test]
        public unsafe void DoubleToDoubleArrayUnsafe()
        {
            CopyArray(OriginalDoubleArray, TargetDoubleArray);
        }
        #endregion
    }
}
