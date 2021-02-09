using BenchmarkDotNet.Attributes;
using Garyon.Benchmarking.Exceptions;
using Garyon.QualityControl.Extensions;
using static Garyon.Functions.PointerHelpers.SIMDPointerConversion;
using static Garyon.Exceptions.ThrowHelper;

namespace Garyon.Benchmarking.Extensions
{
    public class ArrayCopyingHelpersVector256 : ArrayManipulationExtensionsQualityControlAsset
    {
        #region byte[] -> T[]
        [Benchmark]
        [BenchmarkCategory("Byte > Byte", "Unsafe Vector256")]
        public unsafe void ByteToByteArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!CopyToByteArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Byte > Int16", "Unsafe Vector256")]
        public unsafe void ByteToInt16ArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (short* t = TargetInt16Array)
                if (!CopyToInt16ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Byte > Int32", "Unsafe Vector256")]
        public unsafe void ByteToInt32ArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToInt32ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Byte > Int64", "Unsafe Vector256")]
        public unsafe void ByteToInt64ArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (long* t = TargetInt64Array)
                if (!CopyToInt64ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Byte > Single", "Unsafe Vector256")]
        public unsafe void ByteToSingleArrayUnsafe()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (float* t = TargetSingleArray)
                if (!CopyToSingleArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Byte > Double", "Unsafe Vector256")]
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
        [BenchmarkCategory("Int16 > Int16", "Unsafe Vector256")]
        public unsafe void Int16ToInt16ArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!CopyToInt16ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int16 > Int32", "Unsafe Vector256")]
        public unsafe void Int16ToInt32ArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToInt32ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int16 > Int64", "Unsafe Vector256")]
        public unsafe void Int16ToInt64ArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToInt64ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int16 > Single", "Unsafe Vector256")]
        public unsafe void Int16ToSingleArrayUnsafe()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToSingleArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int16 > Double", "Unsafe Vector256")]
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
        [BenchmarkCategory("Int32 > Int32", "Unsafe Vector256")]
        public unsafe void Int32ToInt32ArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToInt32ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int32 > Int64", "Unsafe Vector256")]
        public unsafe void Int32ToInt64ArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToInt64ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int32 > Single", "Unsafe Vector256")]
        public unsafe void Int32ToSingleArrayUnsafe()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToSingleArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int32 > Double", "Unsafe Vector256")]
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
        [BenchmarkCategory("Int64 > Int64", "Unsafe Vector256")]
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
        [BenchmarkCategory("Single > Int32", "Unsafe Vector256")]
        public unsafe void SingleToInt32ArrayUnsafe()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToInt32ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Single > Single", "Unsafe Vector256")]
        public unsafe void SingleToSingleArrayUnsafe()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (float* t = TargetSingleArray)
                if (!CopyToInt64ArrayVector256((long*)o, (long*)t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Single > Double", "Unsafe Vector256")]
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
        [BenchmarkCategory("Double > Int32", "Unsafe Vector256")]
        public unsafe void DoubleToInt32ArrayUnsafe()
        {
            fixed (double* o = OriginalDoubleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToInt32ArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Double > Single", "Unsafe Vector256")]
        public unsafe void DoubleToSingleArrayUnsafe()
        {
            fixed (double* o = OriginalDoubleArray)
            fixed (float* t = TargetSingleArray)
                if (!CopyToSingleArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Double > Double", "Unsafe Vector256")]
        public unsafe void DoubleToDoubleArrayUnsafe()
        {
            fixed (double* o = OriginalDoubleArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToInt64ArrayVector256((long*)o, (long*)t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion
    }
}
