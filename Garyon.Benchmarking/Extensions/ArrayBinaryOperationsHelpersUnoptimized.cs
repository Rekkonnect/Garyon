using BenchmarkDotNet.Attributes;
using Garyon.Benchmarking.Exceptions;
using Garyon.QualityControl.Extensions;
using static Garyon.Exceptions.ThrowHelper;
using static Garyon.Functions.PointerHelpers.SIMDPointerBinaryOperations;

namespace Garyon.Benchmarking.Extensions
{
    public class ArrayBinaryOperationsHelpersUnoptimized : ArrayManipulationExtensionsQualityControlAsset
    {
        private const byte mask = 11;

        #region AND
        [Benchmark]
        [BenchmarkCategory("AND Byte", "Unoptimized")]
        public unsafe void ANDByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetByteArray[i] = (byte)(OriginalByteArray[i] & mask);
        }
        [Benchmark]
        [BenchmarkCategory("AND Int16", "Unoptimized")]
        public unsafe void ANDInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt16Array[i] = (short)(OriginalInt16Array[i] & mask);
        }
        [Benchmark]
        [BenchmarkCategory("AND Int32", "Unoptimized")]
        public unsafe void ANDInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt32Array[i] = OriginalInt32Array[i] & mask;
        }
        [Benchmark]
        [BenchmarkCategory("AND Int64", "Unoptimized")]
        public unsafe void ANDInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt64Array[i] = OriginalInt64Array[i] & mask;
        }
        [Benchmark]
        [BenchmarkCategory("AND SByte", "Unoptimized")]
        public unsafe void ANDSByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetSByteArray[i] = (sbyte)(OriginalSByteArray[i] & (sbyte)mask);
        }
        [Benchmark]
        [BenchmarkCategory("AND UInt16", "Unoptimized")]
        public unsafe void ANDUInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt16Array[i] = (ushort)(OriginalUInt16Array[i] & mask);
        }
        [Benchmark]
        [BenchmarkCategory("AND UInt32", "Unoptimized")]
        public unsafe void ANDUInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt32Array[i] = OriginalUInt32Array[i] & mask;
        }
        [Benchmark]
        [BenchmarkCategory("AND UInt64", "Unoptimized")]
        public unsafe void ANDUInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt64Array[i] = OriginalUInt64Array[i] & mask;
        }
        #endregion

        #region OR
        [Benchmark]
        [BenchmarkCategory("OR Byte", "Unoptimized")]
        public unsafe void ORByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetByteArray[i] = (byte)(OriginalByteArray[i] & mask);
        }
        [Benchmark]
        [BenchmarkCategory("OR Int16", "Unoptimized")]
        public unsafe void ORInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt16Array[i] = (short)(OriginalInt16Array[i] & mask);
        }
        [Benchmark]
        [BenchmarkCategory("OR Int32", "Unoptimized")]
        public unsafe void ORInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt32Array[i] = OriginalInt32Array[i] & mask;
        }
        [Benchmark]
        [BenchmarkCategory("OR Int64", "Unoptimized")]
        public unsafe void ORInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt64Array[i] = OriginalInt64Array[i] & mask;
        }
        [Benchmark]
        [BenchmarkCategory("OR SByte", "Unoptimized")]
        public unsafe void ORSByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetSByteArray[i] = (sbyte)(OriginalSByteArray[i] & (sbyte)mask);
        }
        [Benchmark]
        [BenchmarkCategory("OR UInt16", "Unoptimized")]
        public unsafe void ORUInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt16Array[i] = (ushort)(OriginalUInt16Array[i] & mask);
        }
        [Benchmark]
        [BenchmarkCategory("OR UInt32", "Unoptimized")]
        public unsafe void ORUInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt32Array[i] = OriginalUInt32Array[i] & mask;
        }
        [Benchmark]
        [BenchmarkCategory("OR UInt64", "Unoptimized")]
        public unsafe void ORUInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt64Array[i] = OriginalUInt64Array[i] & mask;
        }
        #endregion

        #region XOR
        [Benchmark]
        [BenchmarkCategory("XOR Byte", "Unoptimized")]
        public unsafe void XORByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetByteArray[i] = (byte)(OriginalByteArray[i] & mask);
        }
        [Benchmark]
        [BenchmarkCategory("XOR Int16", "Unoptimized")]
        public unsafe void XORInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt16Array[i] = (short)(OriginalInt16Array[i] & mask);
        }
        [Benchmark]
        [BenchmarkCategory("XOR Int32", "Unoptimized")]
        public unsafe void XORInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt32Array[i] = OriginalInt32Array[i] & mask;
        }
        [Benchmark]
        [BenchmarkCategory("XOR Int64", "Unoptimized")]
        public unsafe void XORInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt64Array[i] = OriginalInt64Array[i] & mask;
        }
        [Benchmark]
        [BenchmarkCategory("XOR SByte", "Unoptimized")]
        public unsafe void XORSByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetSByteArray[i] = (sbyte)(OriginalSByteArray[i] & (sbyte)mask);
        }
        [Benchmark]
        [BenchmarkCategory("XOR UInt16", "Unoptimized")]
        public unsafe void XORUInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt16Array[i] = (ushort)(OriginalUInt16Array[i] & mask);
        }
        [Benchmark]
        [BenchmarkCategory("XOR UInt32", "Unoptimized")]
        public unsafe void XORUInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt32Array[i] = OriginalUInt32Array[i] & mask;
        }
        [Benchmark]
        [BenchmarkCategory("XOR UInt64", "Unoptimized")]
        public unsafe void XORUInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt64Array[i] = OriginalUInt64Array[i] & mask;
        }
        #endregion
    }
}