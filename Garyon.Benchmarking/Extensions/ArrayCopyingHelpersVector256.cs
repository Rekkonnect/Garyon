using BenchmarkDotNet.Attributes;
using Garyon.Benchmarking.Exceptions;
using Garyon.QualityControl.Extensions;
using static Garyon.Extensions.ArrayCasting.UnsafeArrayCopyingHelpers;
using static Garyon.Exceptions.ThrowHelper;

namespace Garyon.Benchmarking.Extensions
{
    public class ArrayCopyingHelpersVector256 : ArrayCopyingExtensionsQualityControlAsset
    {
        #region Generic
        #region byte[] -> T[]
        [Benchmark]
        public unsafe void ByteToByteArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void ByteToInt16ArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void ByteToInt32ArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void ByteToInt64ArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void ByteToSingleArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void ByteToDoubleArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region short[] -> T[]
        [Benchmark]
        public unsafe void Int16ToInt16ArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int16ToInt32ArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int16ToInt64ArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int16ToSingleArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int16ToDoubleArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region int[] -> T[]
        [Benchmark]
        public unsafe void Int32ToInt32ArrayUnsafeGeneric()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int32ToInt64ArrayUnsafeGeneric()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int32ToSingleArrayUnsafeGeneric()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int32ToDoubleArrayUnsafeGeneric()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region long[] -> T[]
        [Benchmark]
        public unsafe void Int64ToInt64ArrayUnsafeGeneric()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region float[] -> T[]
        [Benchmark]
        public unsafe void SingleToInt32ArrayUnsafeGeneric()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void SingleToDoubleArrayUnsafeGeneric()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region double[] -> T[]
        [Benchmark]
        public unsafe void DoubleToInt32ArrayUnsafeGeneric()
        {
            fixed (double* o = OriginalDoubleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector256Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion
        #endregion

        #region Non-Generic
        #region byte[] -> T[]
        [Benchmark]
        public unsafe void ByteToByteArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!CopyToByteArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void ByteToInt16ArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (short* t = TargetInt16Array)
                if (!CopyToInt16ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void ByteToInt32ArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToInt32ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void ByteToInt64ArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (long* t = TargetInt64Array)
                if (!CopyToInt64ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void ByteToSingleArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (float* t = TargetSingleArray)
                if (!CopyToSingleArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void ByteToDoubleArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToDoubleArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region short[] -> T[]
        [Benchmark]
        public unsafe void Int16ToInt16ArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!CopyToInt16ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int16ToInt32ArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToInt32ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int16ToInt64ArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToInt64ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int16ToSingleArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToSingleArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int16ToDoubleArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToDoubleArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region int[] -> T[]
        [Benchmark]
        public unsafe void Int32ToInt32ArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToInt32ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int32ToInt64ArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToInt64ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int32ToSingleArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToSingleArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int32ToDoubleArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToDoubleArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region long[] -> T[]
        [Benchmark]
        public unsafe void Int64ToInt64ArrayUnsafe()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToInt64ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region float[] -> T[]
        [Benchmark]
        public unsafe void SingleToInt32ArrayUnsafe()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToInt32ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void SingleToDoubleArrayUnsafe()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToDoubleArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region double[] -> T[]
        [Benchmark]
        public unsafe void DoubleToInt32ArrayUnsafe()
        {
            fixed (double* o = OriginalDoubleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToInt32ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion
        #endregion
    }
}