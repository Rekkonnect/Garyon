using BenchmarkDotNet.Attributes;
using Garyon.Benchmarking.Exceptions;
using Garyon.QualityControl.Extensions;
using static Garyon.Functions.PointerHelpers.SIMDPointerConversion;
using static Garyon.Exceptions.ThrowHelper;

namespace Garyon.Benchmarking.Extensions
{
    public class ArrayCopyingHelpersUnvirtualizedVector256 : ArrayManipulationExtensionsQualityControlAsset
    {
        #region byte[] -> T[]
        [Benchmark]
        [BenchmarkCategory("Byte > Byte", "Unsafe Unvirtualized Vector256")]
        public unsafe void ByteToByteArrayUnsafeUnvirtualized()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Byte > Int16", "Unsafe Unvirtualized Vector256")]
        public unsafe void ByteToInt16ArrayUnsafeUnvirtualized()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Byte > Int32", "Unsafe Unvirtualized Vector256")]
        public unsafe void ByteToInt32ArrayUnsafeUnvirtualized()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Byte > Int64", "Unsafe Unvirtualized Vector256")]
        public unsafe void ByteToInt64ArrayUnsafeUnvirtualized()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Byte > Single", "Unsafe Unvirtualized Vector256")]
        public unsafe void ByteToSingleArrayUnsafeUnvirtualized()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Byte > Double", "Unsafe Unvirtualized Vector256")]
        public unsafe void ByteToDoubleArrayUnsafeUnvirtualized()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region short[] -> T[]
        [Benchmark]
        [BenchmarkCategory("Int16 > Int16", "Unsafe Unvirtualized Vector256")]
        public unsafe void Int16ToInt16ArrayUnsafeUnvirtualized()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int16 > Int32", "Unsafe Unvirtualized Vector256")]
        public unsafe void Int16ToInt32ArrayUnsafeUnvirtualized()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int16 > Int64", "Unsafe Unvirtualized Vector256")]
        public unsafe void Int16ToInt64ArrayUnsafeUnvirtualized()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int16 > Single", "Unsafe Unvirtualized Vector256")]
        public unsafe void Int16ToSingleArrayUnsafeUnvirtualized()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int16 > Double", "Unsafe Unvirtualized Vector256")]
        public unsafe void Int16ToDoubleArrayUnsafeUnvirtualized()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region int[] -> T[]
        [Benchmark]
        [BenchmarkCategory("Int32 > Int32", "Unsafe Unvirtualized Vector256")]
        public unsafe void Int32ToInt32ArrayUnsafeUnvirtualized()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int32 > Int64", "Unsafe Unvirtualized Vector256")]
        public unsafe void Int32ToInt64ArrayUnsafeUnvirtualized()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int32 > Single", "Unsafe Unvirtualized Vector256")]
        public unsafe void Int32ToSingleArrayUnsafeUnvirtualized()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int32 > Double", "Unsafe Unvirtualized Vector256")]
        public unsafe void Int32ToDoubleArrayUnsafeUnvirtualized()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region long[] -> T[]
        [Benchmark]
        [BenchmarkCategory("Int64 > Int64", "Unsafe Unvirtualized Vector256")]
        public unsafe void Int64ToInt64ArrayUnsafeUnvirtualized()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region float[] -> T[]
        [Benchmark]
        [BenchmarkCategory("Single > Int32", "Unsafe Unvirtualized Vector256")]
        public unsafe void SingleToInt32ArrayUnsafeUnvirtualized()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Single > Single", "Unsafe Unvirtualized Vector256")]
        public unsafe void SingleToSingleArrayUnsafeUnvirtualized()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Single > Double", "Unsafe Unvirtualized Vector256")]
        public unsafe void SingleToDoubleArrayUnsafeUnvirtualized()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region double[] -> T[]
        [Benchmark]
        [BenchmarkCategory("Double > Int32", "Unsafe Unvirtualized Vector256")]
        public unsafe void DoubleToInt32ArrayUnsafeUnvirtualized()
        {
            fixed (double* o = OriginalDoubleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Double > Single", "Unsafe Unvirtualized Vector256")]
        public unsafe void DoubleToSingleArrayUnsafeUnvirtualized()
        {
            fixed (double* o = OriginalDoubleArray)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Double > Double", "Unsafe Unvirtualized Vector256")]
        public unsafe void DoubleToDoubleArrayUnsafeUnvirtualized()
        {
            fixed (double* o = OriginalDoubleArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector256Unvirtualized(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion
    }
}
