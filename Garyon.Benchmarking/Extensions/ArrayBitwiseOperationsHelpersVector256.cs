using BenchmarkDotNet.Attributes;
using Garyon.Benchmarking.Exceptions;
using Garyon.QualityControl.Extensions;
using static Garyon.Exceptions.ThrowHelper;
using static Garyon.Functions.PointerHelpers.SIMDPointerBitwiseOperations;

namespace Garyon.Benchmarking.Extensions
{
    public class ArrayBitwiseOperationsHelpersVector256 : ArrayManipulationExtensionsQualityControlAsset
    {
        private const byte mask = 11;

        #region NOT
        [Benchmark]
        [BenchmarkCategory("NOT Byte", "Unsafe Vector256")]
        public unsafe void NOTByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NOT Int16", "Unsafe Vector256")]
        public unsafe void NOTInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NOT Int32", "Unsafe Vector256")]
        public unsafe void NOTInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NOT Int64", "Unsafe Vector256")]
        public unsafe void NOTInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NOT SByte", "Unsafe Vector256")]
        public unsafe void NOTSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NOT UInt16", "Unsafe Vector256")]
        public unsafe void NOTUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NOT UInt32", "Unsafe Vector256")]
        public unsafe void NOTUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NOT UInt64", "Unsafe Vector256")]
        public unsafe void NOTUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!NOTArrayVector256(o, t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region AND
        [Benchmark]
        [BenchmarkCategory("AND Byte", "Unsafe Vector256")]
        public unsafe void ANDByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!ANDArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("AND Int16", "Unsafe Vector256")]
        public unsafe void ANDInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!ANDArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("AND Int32", "Unsafe Vector256")]
        public unsafe void ANDInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!ANDArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("AND Int64", "Unsafe Vector256")]
        public unsafe void ANDInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!ANDArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("AND SByte", "Unsafe Vector256")]
        public unsafe void ANDSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!ANDArrayVector256(o, t, (sbyte)mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("AND UInt16", "Unsafe Vector256")]
        public unsafe void ANDUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!ANDArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("AND UInt32", "Unsafe Vector256")]
        public unsafe void ANDUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!ANDArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("AND UInt64", "Unsafe Vector256")]
        public unsafe void ANDUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!ANDArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region OR
        [Benchmark]
        [BenchmarkCategory("OR Byte", "Unsafe Vector256")]
        public unsafe void ORByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!ORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("OR Int16", "Unsafe Vector256")]
        public unsafe void ORInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!ORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("OR Int32", "Unsafe Vector256")]
        public unsafe void ORInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!ORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("OR Int64", "Unsafe Vector256")]
        public unsafe void ORInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!ORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("OR SByte", "Unsafe Vector256")]
        public unsafe void ORSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!ORArrayVector256(o, t, (sbyte)mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("OR UInt16", "Unsafe Vector256")]
        public unsafe void ORUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!ORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("OR UInt32", "Unsafe Vector256")]
        public unsafe void ORUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!ORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("OR UInt64", "Unsafe Vector256")]
        public unsafe void ORUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!ORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region XOR
        [Benchmark]
        [BenchmarkCategory("XOR Byte", "Unsafe Vector256")]
        public unsafe void XORByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!XORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("XOR Int16", "Unsafe Vector256")]
        public unsafe void XORInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!XORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("XOR Int32", "Unsafe Vector256")]
        public unsafe void XORInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!XORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("XOR Int64", "Unsafe Vector256")]
        public unsafe void XORInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!XORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("XOR SByte", "Unsafe Vector256")]
        public unsafe void XORSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!XORArrayVector256(o, t, (sbyte)mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("XOR UInt16", "Unsafe Vector256")]
        public unsafe void XORUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!XORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("XOR UInt32", "Unsafe Vector256")]
        public unsafe void XORUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!XORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("XOR UInt64", "Unsafe Vector256")]
        public unsafe void XORUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!XORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region NAND
        [Benchmark]
        [BenchmarkCategory("NAND Byte", "Unsafe Vector256")]
        public unsafe void NANDByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!NANDArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NAND Int16", "Unsafe Vector256")]
        public unsafe void NANDInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!NANDArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NAND Int32", "Unsafe Vector256")]
        public unsafe void NANDInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!NANDArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NAND Int64", "Unsafe Vector256")]
        public unsafe void NANDInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!NANDArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NAND SByte", "Unsafe Vector256")]
        public unsafe void NANDSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!NANDArrayVector256(o, t, (sbyte)mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NAND UInt16", "Unsafe Vector256")]
        public unsafe void NANDUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!NANDArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NAND UInt32", "Unsafe Vector256")]
        public unsafe void NANDUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!NANDArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NAND UInt64", "Unsafe Vector256")]
        public unsafe void NANDUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!NANDArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region NOR
        [Benchmark]
        [BenchmarkCategory("NOR Byte", "Unsafe Vector256")]
        public unsafe void NORByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!NORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NOR Int16", "Unsafe Vector256")]
        public unsafe void NORInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!NORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NOR Int32", "Unsafe Vector256")]
        public unsafe void NORInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!NORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NOR Int64", "Unsafe Vector256")]
        public unsafe void NORInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!NORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NOR SByte", "Unsafe Vector256")]
        public unsafe void NORSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!NORArrayVector256(o, t, (sbyte)mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NOR UInt16", "Unsafe Vector256")]
        public unsafe void NORUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!NORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NOR UInt32", "Unsafe Vector256")]
        public unsafe void NORUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!NORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("NOR UInt64", "Unsafe Vector256")]
        public unsafe void NORUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!NORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion

        #region XNOR
        [Benchmark]
        [BenchmarkCategory("XNOR Byte", "Unsafe Vector256")]
        public unsafe void XNORByteArray()
        {
            fixed (byte* o = OriginalByteArray)
            fixed (byte* t = TargetByteArray)
                if (!XNORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("XNOR Int16", "Unsafe Vector256")]
        public unsafe void XNORInt16Array()
        {
            fixed (short* o = OriginalInt16Array)
            fixed (short* t = TargetInt16Array)
                if (!XNORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("XNOR Int32", "Unsafe Vector256")]
        public unsafe void XNORInt32Array()
        {
            fixed (int* o = OriginalInt32Array)
            fixed (int* t = TargetInt32Array)
                if (!XNORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("XNOR Int64", "Unsafe Vector256")]
        public unsafe void XNORInt64Array()
        {
            fixed (long* o = OriginalInt64Array)
            fixed (long* t = TargetInt64Array)
                if (!XNORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("XNOR SByte", "Unsafe Vector256")]
        public unsafe void XNORSByteArray()
        {
            fixed (sbyte* o = OriginalSByteArray)
            fixed (sbyte* t = TargetSByteArray)
                if (!XNORArrayVector256(o, t, (sbyte)mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("XNOR UInt16", "Unsafe Vector256")]
        public unsafe void XNORUInt16Array()
        {
            fixed (ushort* o = OriginalUInt16Array)
            fixed (ushort* t = TargetUInt16Array)
                if (!XNORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("XNOR UInt32", "Unsafe Vector256")]
        public unsafe void XNORUInt32Array()
        {
            fixed (uint* o = OriginalUInt32Array)
            fixed (uint* t = TargetUInt32Array)
                if (!XNORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark]
        [BenchmarkCategory("XNOR UInt64", "Unsafe Vector256")]
        public unsafe void XNORUInt64Array()
        {
            fixed (ulong* o = OriginalUInt64Array)
            fixed (ulong* t = TargetUInt64Array)
                if (!XNORArrayVector256(o, t, mask, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        #endregion
    }
}
