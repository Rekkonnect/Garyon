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
            PerformManipulation(OriginalByteArray, TargetByteArray);
        }
        [Test]
        public unsafe void ByteToInt16ArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetInt16Array);
        }
        [Test]
        public unsafe void ByteToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetInt32Array);
        }
        [Test]
        public unsafe void ByteToInt64ArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetInt64Array);
        }
        [Test]
        public unsafe void ByteToSingleArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetSingleArray);
        }
        [Test]
        public unsafe void ByteToDoubleArrayUnsafe()
        {
            PerformManipulation(OriginalByteArray, TargetDoubleArray);
        }
        #endregion

        #region short[] -> T[]
        [Test]
        public unsafe void Int16ToInt16ArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetInt16Array);
        }
        [Test]
        public unsafe void Int16ToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetInt32Array);
        }
        [Test]
        public unsafe void Int16ToInt64ArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetInt64Array);
        }
        [Test]
        public unsafe void Int16ToSingleArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetSingleArray);
        }
        [Test]
        public unsafe void Int16ToDoubleArrayUnsafe()
        {
            PerformManipulation(OriginalInt16Array, TargetDoubleArray);
        }
        #endregion

        #region int[] -> T[]
        [Test]
        public unsafe void Int32ToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalInt32Array, TargetInt32Array);
        }
        [Test]
        public unsafe void Int32ToInt64ArrayUnsafe()
        {
            PerformManipulation(OriginalInt32Array, TargetInt64Array);
        }
        [Test]
        public unsafe void Int32ToSingleArrayUnsafe()
        {
            PerformManipulation(OriginalInt32Array, TargetSingleArray);
        }
        [Test]
        public unsafe void Int32ToDoubleArrayUnsafe()
        {
            PerformManipulation(OriginalInt32Array, TargetDoubleArray);
        }
        #endregion

        #region long[] -> T[]
        [Test]
        public unsafe void Int64ToInt64ArrayUnsafe()
        {
            PerformManipulation(OriginalInt64Array, TargetInt64Array);
        }
        #endregion

        #region float[] -> T[]
        [Test]
        public unsafe void SingleToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalSingleArray, TargetInt32Array);
        }
        [Test]
        public unsafe void SingleToSingleArrayUnsafe()
        {
            PerformManipulation(OriginalSingleArray, TargetSingleArray);
        }
        [Test]
        public unsafe void SingleToDoubleArrayUnsafe()
        {
            PerformManipulation(OriginalSingleArray, TargetDoubleArray);
        }
        #endregion

        #region double[] -> T[]
        [Test]
        public unsafe void DoubleToInt32ArrayUnsafe()
        {
            PerformManipulation(OriginalDoubleArray, TargetInt32Array);
        }
        [Test]
        public unsafe void DoubleToDoubleArrayUnsafe()
        {
            PerformManipulation(OriginalDoubleArray, TargetDoubleArray);
        }
        #endregion
    }
}
