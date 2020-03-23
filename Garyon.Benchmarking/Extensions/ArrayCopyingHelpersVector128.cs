using BenchmarkDotNet.Attributes;
using Garyon.Benchmarking.Exceptions;
using Garyon.Extensions;
using Garyon.QualityControl.Extensions;
using static Garyon.Extensions.ArrayCasting.UnsafeArrayCopyingHelpers;

namespace Garyon.Benchmarking.Extensions
{
    public class ArrayCopyingHelpersVector128 : ArrayCopyingExtensionsQualityControlAsset
    {
        #region byte[] -> T[]
        [Benchmark]
        public unsafe void ByteToByteArrayUnsafe()
        {
            if (!CopyToByteArrayVector128(OriginalByteArray.GetPointer(), TargetByteArray.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void ByteToInt16ArrayUnsafe()
        {
            if (!CopyToInt16ArrayVector128(OriginalByteArray.GetPointer(), TargetInt16Array.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void ByteToInt32ArrayUnsafe()
        {
            if (!CopyToInt32ArrayVector128(OriginalByteArray.GetPointer(), TargetInt32Array.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void ByteToInt64ArrayUnsafe()
        {
            if (!CopyToInt64ArrayVector128(OriginalByteArray.GetPointer(), TargetInt64Array.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void ByteToSingleArrayUnsafe()
        {
            if (!CopyToSingleArrayVector128(OriginalByteArray.GetPointer(), TargetSingleArray.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void ByteToDoubleArrayUnsafe()
        {
            if (!CopyToDoubleArrayVector128(OriginalByteArray.GetPointer(), TargetDoubleArray.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        #endregion

        #region short[] -> T[]
        [Benchmark]
        public unsafe void Int16ToByteArrayUnsafe()
        {
            if (!CopyToByteArrayVector128(OriginalInt16Array.GetPointer(), TargetByteArray.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void Int16ToInt16ArrayUnsafe()
        {
            if (!CopyToInt16ArrayVector128(OriginalInt16Array.GetPointer(), TargetInt16Array.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void Int16ToInt32ArrayUnsafe()
        {
            if (!CopyToInt32ArrayVector128(OriginalInt16Array.GetPointer(), TargetInt32Array.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void Int16ToInt64ArrayUnsafe()
        {
            if (!CopyToInt64ArrayVector128(OriginalInt16Array.GetPointer(), TargetInt64Array.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void Int16ToSingleArrayUnsafe()
        {
            if (!CopyToSingleArrayVector128(OriginalInt16Array.GetPointer(), TargetSingleArray.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void Int16ToDoubleArrayUnsafe()
        {
            if (!CopyToDoubleArrayVector128(OriginalInt16Array.GetPointer(), TargetDoubleArray.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        #endregion

        #region int[] -> T[]
        [Benchmark]
        public unsafe void Int32ToByteArrayUnsafe()
        {
            if (!CopyToByteArrayVector128(OriginalInt32Array.GetPointer(), TargetByteArray.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void Int32ToInt16ArrayUnsafe()
        {
            if (!CopyToInt16ArrayVector128(OriginalInt32Array.GetPointer(), TargetInt16Array.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void Int32ToInt32ArrayUnsafe()
        {
            if (!CopyToInt32ArrayVector128(OriginalInt32Array.GetPointer(), TargetInt32Array.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void Int32ToInt64ArrayUnsafe()
        {
            if (!CopyToInt64ArrayVector128(OriginalInt32Array.GetPointer(), TargetInt64Array.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void Int32ToSingleArrayUnsafe()
        {
            if (!CopyToSingleArrayVector128(OriginalInt32Array.GetPointer(), TargetSingleArray.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void Int32ToDoubleArrayUnsafe()
        {
            if (!CopyToDoubleArrayVector128(OriginalInt32Array.GetPointer(), TargetDoubleArray.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        #endregion

        #region long[] -> T[]
        [Benchmark]
        public unsafe void Int64ToByteArrayUnsafe()
        {
            if (!CopyToByteArrayVector128(OriginalInt64Array.GetPointer(), TargetByteArray.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void Int64ToInt16ArrayUnsafe()
        {
            if (!CopyToInt16ArrayVector128(OriginalInt64Array.GetPointer(), TargetInt16Array.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void Int64ToInt32ArrayUnsafe()
        {
            if (!CopyToInt32ArrayVector128(OriginalInt64Array.GetPointer(), TargetInt32Array.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void Int64ToInt64ArrayUnsafe()
        {
            if (!CopyToInt64ArrayVector128(OriginalInt64Array.GetPointer(), TargetInt64Array.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        #endregion

        #region float[] -> T[]
        [Benchmark]
        public unsafe void SingleToInt32ArrayUnsafe()
        {
            if (!CopyToInt32ArrayVector128(OriginalSingleArray.GetPointer(), TargetInt32Array.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        [Benchmark]
        public unsafe void SingleToDoubleArrayUnsafe()
        {
            if (!CopyToDoubleArrayVector128(OriginalSingleArray.GetPointer(), TargetDoubleArray.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        #endregion

        #region double[] -> T[]
        [Benchmark]
        public unsafe void DoubleToInt32ArrayUnsafe()
        {
            if (!CopyToInt32ArrayVector128(OriginalDoubleArray.GetPointer(), TargetInt32Array.GetPointer(), ArrayLength))
                throw new InstructionSetBenchmarkException();
        }
        #endregion
    }
}