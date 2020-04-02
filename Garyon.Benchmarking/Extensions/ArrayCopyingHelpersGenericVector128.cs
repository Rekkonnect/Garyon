using BenchmarkDotNet.Attributes;
using Garyon.Benchmarking.Exceptions;
using Garyon.QualityControl.Extensions;
using static Garyon.Functions.PointerHelpers.SIMDPointerConversion;
using static Garyon.Exceptions.ThrowHelper;

namespace Garyon.Benchmarking.Extensions
{
    public class ArrayCopyingHelpersGenericVector128 : ArrayCopyingExtensionsQualityControlAsset
    {
        #region Generic
        #region byte[] -> T[]
        [Benchmark]
        public unsafe void ByteToByteArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void ByteToInt16ArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void ByteToInt32ArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void ByteToInt64ArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        //[Benchmark]
        public unsafe void ByteToSingleArrayUnsafeGeneric()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        //[Benchmark]
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
        public unsafe void Int16ToByteArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (byte* t = TargetByteArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int16ToInt16ArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int16ToInt32ArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int16ToInt64ArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        //[Benchmark]
        public unsafe void Int16ToSingleArrayUnsafeGeneric()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        //[Benchmark]
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
        public unsafe void Int32ToByteArrayUnsafeGeneric()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (byte* t = TargetByteArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int32ToInt16ArrayUnsafeGeneric()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int32ToInt32ArrayUnsafeGeneric()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int32ToInt64ArrayUnsafeGeneric()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        //[Benchmark]
        public unsafe void Int32ToSingleArrayUnsafeGeneric()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        //[Benchmark]
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
        public unsafe void Int64ToByteArrayUnsafeGeneric()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (byte* t = TargetByteArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int64ToInt16ArrayUnsafeGeneric()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (short* t = TargetInt16Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int64ToInt32ArrayUnsafeGeneric()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void Int64ToInt64ArrayUnsafeGeneric()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region float[] -> T[]
        //[Benchmark]
        public unsafe void SingleToInt32ArrayUnsafeGeneric()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        public unsafe void SingleToSingleArrayUnsafeGeneric()
        {
            fixed (float* o = OriginalSingleArray)
            fixed (float* t = TargetSingleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
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
        public unsafe void DoubleToDoubleArrayUnsafeGeneric()
        {
            fixed (double* o = OriginalDoubleArray)
            fixed (double* t = TargetDoubleArray)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        //[Benchmark]
        public unsafe void DoubleToInt32ArrayUnsafeGeneric()
        {
            fixed (double* o = OriginalDoubleArray)
            fixed (int* t = TargetInt32Array)
                if (!CopyToArrayVector128Generic(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion
        #endregion

        #region Manual
        #region byte[] -> T[]
        [Benchmark]
        public void ByteToByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetByteArray[i] = OriginalByteArray[i];
        }
        [Benchmark]
        public void ByteToInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt16Array[i] = OriginalByteArray[i];
        }
        [Benchmark]
        public void ByteToInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt32Array[i] = OriginalByteArray[i];
        }
        [Benchmark]
        public void ByteToInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt64Array[i] = OriginalByteArray[i];
        }
        #endregion
        #region byte[] -> T[]
        [Benchmark]
        public void Int16ToByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetByteArray[i] = (byte)OriginalInt16Array[i];
        }
        [Benchmark]
        public void Int16ToInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt16Array[i] = OriginalInt16Array[i];
        }
        [Benchmark]
        public void Int16ToInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt32Array[i] = OriginalInt16Array[i];
        }
        [Benchmark]
        public void Int16ToInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt64Array[i] = OriginalInt16Array[i];
        }
        #endregion
        #region byte[] -> T[]
        [Benchmark]
        public void Int32ToByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetByteArray[i] = (byte)OriginalInt32Array[i];
        }
        [Benchmark]
        public void Int32ToInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt16Array[i] = (short)OriginalInt32Array[i];
        }
        [Benchmark]
        public void Int32ToInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt32Array[i] = OriginalInt32Array[i];
        }
        [Benchmark]
        public void Int32ToInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt64Array[i] = OriginalInt32Array[i];
        }
        #endregion
        #region byte[] -> T[]
        [Benchmark]
        public void Int64ToByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetByteArray[i] = (byte)OriginalInt64Array[i];
        }
        [Benchmark]
        public void Int64ToInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt16Array[i] = (short)OriginalInt64Array[i];
        }
        [Benchmark]
        public void Int64ToInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt32Array[i] = (int)OriginalInt64Array[i];
        }
        [Benchmark]
        public void Int64ToInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt64Array[i] = (long)OriginalInt64Array[i];
        }
        #endregion
        #endregion
    }
}