using NUnit.Framework;
using static Garyon.Functions.PointerHelpers.SIMDPointerConversion;

namespace Garyon.Tests.Extensions
{
    public class ArrayCopyingHelpersUnvirtualizedVector128Tests : ArrayCopyingHelpersTestsBase
    {
        protected unsafe override ArrayManipulationOperation<TOrigin, TTarget> GetArrayManipulationOperationDelegate<TOrigin, TTarget>() => CopyToArrayVector128;

        #region byte[] -> T[]
        [Test]
        public unsafe void ByteToByteArray()
        {
            CopyArray(OriginalByteArray, TargetByteArray);
        }
        [Test]
        public unsafe void ByteToInt16Array()
        {
            CopyArray(OriginalByteArray, TargetInt16Array);
        }
        [Test]
        public unsafe void ByteToInt32Array()
        {
            CopyArray(OriginalByteArray, TargetInt32Array);
        }
        [Test]
        public unsafe void ByteToInt64Array()
        {
            CopyArray(OriginalByteArray, TargetInt64Array);
        }
        [Test]
        public unsafe void ByteToSingleArray()
        {
            CopyArray(OriginalByteArray, TargetSingleArray);
        }
        [Test]
        public unsafe void ByteToDoubleArray()
        {
            CopyArray(OriginalByteArray, TargetDoubleArray);
        }
        #endregion

        #region short[] -> T[]
        [Test]
        public unsafe void Int16ToByteArray()
        {
            CopyArray(OriginalInt16Array, TargetByteArray);
        }
        [Test]
        public unsafe void Int16ToInt16Array()
        {
            CopyArray(OriginalInt16Array, TargetInt16Array);
        }
        [Test]
        public unsafe void Int16ToInt32Array()
        {
            CopyArray(OriginalInt16Array, TargetInt32Array);
        }
        [Test]
        public unsafe void Int16ToInt64Array()
        {
            CopyArray(OriginalInt16Array, TargetInt64Array);
        }
        [Test]
        public unsafe void Int16ToSingleArray()
        {
            CopyArray(OriginalInt16Array, TargetSingleArray);
        }
        [Test]
        public unsafe void Int16ToDoubleArray()
        {
            CopyArray(OriginalInt16Array, TargetDoubleArray);
        }
        #endregion

        #region int[] -> T[]
        [Test]
        public unsafe void Int32ToByteArray()
        {
            CopyArray(OriginalInt32Array, TargetByteArray);
        }
        [Test]
        public unsafe void Int32ToInt16Array()
        {
            CopyArray(OriginalInt32Array, TargetInt16Array);
        }
        [Test]
        public unsafe void Int32ToInt32Array()
        {
            CopyArray(OriginalInt32Array, TargetInt32Array);
        }
        [Test]
        public unsafe void Int32ToInt64Array()
        {
            CopyArray(OriginalInt32Array, TargetInt64Array);
        }
        [Test]
        public unsafe void Int32ToSingleArray()
        {
            CopyArray(OriginalInt32Array, TargetSingleArray);
        }
        [Test]
        public unsafe void Int32ToDoubleArray()
        {
            CopyArray(OriginalInt32Array, TargetDoubleArray);
        }
        #endregion

        #region long[] -> T[]
        [Test]
        public unsafe void Int64ToByteArray()
        {
            CopyArray(OriginalInt64Array, TargetByteArray);
        }
        [Test]
        public unsafe void Int64ToInt16Array()
        {
            CopyArray(OriginalInt64Array, TargetInt16Array);
        }
        [Test]
        public unsafe void Int64ToInt32Array()
        {
            CopyArray(OriginalInt64Array, TargetInt32Array);
        }
        [Test]
        public unsafe void Int64ToInt64Array()
        {
            CopyArray(OriginalInt64Array, TargetInt64Array);
        }
        #endregion

        #region float[] -> T[]
        [Test]
        public unsafe void SingleToInt32Array()
        {
            CopyArray(OriginalSingleArray, TargetInt32Array);
        }
        [Test]
        public unsafe void SingleToSingleArray()
        {
            CopyArray(OriginalSingleArray, TargetSingleArray);
        }
        [Test]
        public unsafe void SingleToDoubleArray()
        {
            CopyArray(OriginalSingleArray, TargetDoubleArray);
        }
        #endregion

        #region double[] -> T[]
        [Test]
        public unsafe void DoubleToInt32Array()
        {
            CopyArray(OriginalDoubleArray, TargetInt32Array);
        }
        [Test]
        public unsafe void DoubleToDoubleArray()
        {
            CopyArray(OriginalDoubleArray, TargetDoubleArray);
        }
        #endregion
    }
}
