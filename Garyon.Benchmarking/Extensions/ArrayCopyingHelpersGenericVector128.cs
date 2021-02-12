using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Garyon.Benchmarking.Exceptions;
using Garyon.QualityControl.Extensions;
using static Garyon.Exceptions.ThrowHelper;
using static Garyon.Functions.PointerHelpers.SIMDPointerConversion;

namespace Garyon.Benchmarking.Extensions
{
    [HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.CacheMisses)]
    public class ArrayCopyingHelpersGenericVector128 : ArrayManipulationExtensionsQualityControlAsset
    {
        #region byte[] -> T[]
        [Benchmark]
        [BenchmarkCategory("Byte > Byte", "Unsafe Generic")]
        public unsafe void ByteToByteArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Byte > Int16", "Unsafe Generic")]
        public unsafe void ByteToInt16ArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Byte > Int32", "Unsafe Generic")]
        public unsafe void ByteToInt32ArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Byte > Int64", "Unsafe Generic")]
        public unsafe void ByteToInt64ArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Byte > Single", "Unsafe Generic")]
        public unsafe void ByteToSingleArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Byte > Double", "Unsafe Generic")]
        public unsafe void ByteToDoubleArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region short[] -> T[]
        [Benchmark]
        [BenchmarkCategory("Int16 > Byte", "Unsafe Generic")]
        public unsafe void Int16ToByteArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (byte* t = TargetByteArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int16 > Int16", "Unsafe Generic")]
        public unsafe void Int16ToInt16ArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int16 > Int32", "Unsafe Generic")]
        public unsafe void Int16ToInt32ArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int16 > Int64", "Unsafe Generic")]
        public unsafe void Int16ToInt64ArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int16 > Single", "Unsafe Generic")]
        public unsafe void Int16ToSingleArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int16 > Double", "Unsafe Generic")]
        public unsafe void Int16ToDoubleArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region int[] -> T[]
        [Benchmark]
        [BenchmarkCategory("Int32 > Byte", "Unsafe Generic")]
        public unsafe void Int32ToByteArrayUnsafeGeneric()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (byte* t = TargetByteArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int32 > Int16", "Unsafe Generic")]
        public unsafe void Int32ToInt16ArrayUnsafeGeneric()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int32 > Int32", "Unsafe Generic")]
        public unsafe void Int32ToInt32ArrayUnsafeGeneric()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int32 > Int64", "Unsafe Generic")]
        public unsafe void Int32ToInt64ArrayUnsafeGeneric()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int32 > Single", "Unsafe Generic")]
        public unsafe void Int32ToSingleArrayUnsafeGeneric()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int32 > Double", "Unsafe Generic")]
        public unsafe void Int32ToDoubleArrayUnsafeGeneric()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region long[] -> T[]
        [Benchmark]
        [BenchmarkCategory("Int64 > Byte", "Unsafe Generic")]
        public unsafe void Int64ToByteArrayUnsafeGeneric()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (byte* t = TargetByteArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int64 > Int16", "Unsafe Generic")]
        public unsafe void Int64ToInt16ArrayUnsafeGeneric()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int64 > Int32", "Unsafe Generic")]
        public unsafe void Int64ToInt32ArrayUnsafeGeneric()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Int64 > Int64", "Unsafe Generic")]
        public unsafe void Int64ToInt64ArrayUnsafeGeneric()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region float[] -> T[]
        [Benchmark]
        [BenchmarkCategory("Single > Int32", "Unsafe Generic")]
        public unsafe void SingleToInt32ArrayUnsafeGeneric()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Single > Single", "Unsafe Generic")]
        public unsafe void SingleToSingleArrayUnsafeGeneric()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Single > Double", "Unsafe Generic")]
        public unsafe void SingleToDoubleArrayUnsafeGeneric()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region double[] -> T[]
        [Benchmark]
        [BenchmarkCategory("Double > Int32", "Unsafe Generic")]
        public unsafe void DoubleToInt32ArrayUnsafeGeneric()
        {
            fixed (double* o = OriginalDoubleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Double > Single", "Unsafe Generic")]
        public unsafe void DoubleToSingleArrayUnsafeGeneric()
        {
            fixed (double* o = OriginalDoubleArray)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("Double > Double", "Unsafe Generic")]
        public unsafe void DoubleToDoubleArrayUnsafeGeneric()
        {
            fixed (double* o = OriginalDoubleArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion
    }
}
