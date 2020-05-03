using BenchmarkDotNet.Attributes;
using Garyon.QualityControl.Extensions;

namespace Garyon.Benchmarking.Extensions
{
    public class ArrayBinaryOperationsHelpersUnoptimized : ArrayManipulationExtensionsQualityControlAsset
    {
        private const byte mask = 11;

        #region NOT
        [Benchmark]
        [BenchmarkCategory("NOT Byte", "Unoptimized")]
        public unsafe void NOTByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetByteArray[i] = (byte)~OriginalByteArray[i];
        }
        [Benchmark]
        [BenchmarkCategory("NOT Int16", "Unoptimized")]
        public unsafe void NOTInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt16Array[i] = (short)~OriginalInt16Array[i];
        }
        [Benchmark]
        [BenchmarkCategory("NOT Int32", "Unoptimized")]
        public unsafe void NOTInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt32Array[i] = ~OriginalInt32Array[i];
        }
        [Benchmark]
        [BenchmarkCategory("NOT Int64", "Unoptimized")]
        public unsafe void NOTInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt64Array[i] = ~OriginalInt64Array[i];
        }
        [Benchmark]
        [BenchmarkCategory("NOT SByte", "Unoptimized")]
        public unsafe void NOTSByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetSByteArray[i] = (sbyte)~OriginalSByteArray[i];
        }
        [Benchmark]
        [BenchmarkCategory("NOT UInt16", "Unoptimized")]
        public unsafe void NOTUInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt16Array[i] = (ushort)~OriginalUInt16Array[i];
        }
        [Benchmark]
        [BenchmarkCategory("NOT UInt32", "Unoptimized")]
        public unsafe void NOTUInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt32Array[i] = ~OriginalUInt32Array[i];
        }
        [Benchmark]
        [BenchmarkCategory("NOT UInt64", "Unoptimized")]
        public unsafe void NOTUInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt64Array[i] = ~OriginalUInt64Array[i];
        }
        #endregion

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

        #region NAND
        [Benchmark]
        [BenchmarkCategory("NAND Byte", "Unoptimized")]
        public unsafe void NANDByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetByteArray[i] = (byte)~(OriginalByteArray[i] & mask);
        }
        [Benchmark]
        [BenchmarkCategory("NAND Int16", "Unoptimized")]
        public unsafe void NANDInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt16Array[i] = (short)~(OriginalInt16Array[i] & mask);
        }
        [Benchmark]
        [BenchmarkCategory("NAND Int32", "Unoptimized")]
        public unsafe void NANDInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt32Array[i] = ~(OriginalInt32Array[i] & mask);
        }
        [Benchmark]
        [BenchmarkCategory("NAND Int64", "Unoptimized")]
        public unsafe void NANDInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt64Array[i] = ~(OriginalInt64Array[i] & mask);
        }
        [Benchmark]
        [BenchmarkCategory("NAND SByte", "Unoptimized")]
        public unsafe void NANDSByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetSByteArray[i] = (sbyte)~(OriginalSByteArray[i] & (sbyte)mask);
        }
        [Benchmark]
        [BenchmarkCategory("NAND UInt16", "Unoptimized")]
        public unsafe void NANDUInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt16Array[i] = (ushort)~(OriginalUInt16Array[i] & mask);
        }
        [Benchmark]
        [BenchmarkCategory("NAND UInt32", "Unoptimized")]
        public unsafe void NANDUInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt32Array[i] = ~(OriginalUInt32Array[i] & mask);
        }
        [Benchmark]
        [BenchmarkCategory("NAND UInt64", "Unoptimized")]
        public unsafe void NANDUInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt64Array[i] = ~(OriginalUInt64Array[i] & mask);
        }
        #endregion

        #region NOR
        [Benchmark]
        [BenchmarkCategory("NOR Byte", "Unoptimized")]
        public unsafe void NORByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetByteArray[i] = (byte)~(OriginalByteArray[i] | mask);
        }
        [Benchmark]
        [BenchmarkCategory("NOR Int16", "Unoptimized")]
        public unsafe void NORInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt16Array[i] = (short)~(OriginalInt16Array[i] | mask);
        }
        [Benchmark]
        [BenchmarkCategory("NOR Int32", "Unoptimized")]
        public unsafe void NORInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt32Array[i] = ~(OriginalInt32Array[i] | mask);
        }
        [Benchmark]
        [BenchmarkCategory("NOR Int64", "Unoptimized")]
        public unsafe void NORInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt64Array[i] = ~(OriginalInt64Array[i] | mask);
        }
        [Benchmark]
        [BenchmarkCategory("NOR SByte", "Unoptimized")]
        public unsafe void NORSByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetSByteArray[i] = (sbyte)~(OriginalSByteArray[i] | (sbyte)mask);
        }
        [Benchmark]
        [BenchmarkCategory("NOR UInt16", "Unoptimized")]
        public unsafe void NORUInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt16Array[i] = (ushort)~(OriginalUInt16Array[i] | mask);
        }
        [Benchmark]
        [BenchmarkCategory("NOR UInt32", "Unoptimized")]
        public unsafe void NORUInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt32Array[i] = ~(OriginalUInt32Array[i] | mask);
        }
        [Benchmark]
        [BenchmarkCategory("NOR UInt64", "Unoptimized")]
        public unsafe void NORUInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt64Array[i] = ~(OriginalUInt64Array[i] | mask);
        }
        #endregion

        #region XNOR
        [Benchmark]
        [BenchmarkCategory("XNOR Byte", "Unoptimized")]
        public unsafe void XNORByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetByteArray[i] = (byte)~(OriginalByteArray[i] ^ mask);
        }
        [Benchmark]
        [BenchmarkCategory("XNOR Int16", "Unoptimized")]
        public unsafe void XNORInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt16Array[i] = (short)~(OriginalInt16Array[i] ^ mask);
        }
        [Benchmark]
        [BenchmarkCategory("XNOR Int32", "Unoptimized")]
        public unsafe void XNORInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt32Array[i] = ~(OriginalInt32Array[i] ^ mask);
        }
        [Benchmark]
        [BenchmarkCategory("XNOR Int64", "Unoptimized")]
        public unsafe void XNORInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetInt64Array[i] = ~(OriginalInt64Array[i] ^ mask);
        }
        [Benchmark]
        [BenchmarkCategory("XNOR SByte", "Unoptimized")]
        public unsafe void XNORSByteArray()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetSByteArray[i] = (sbyte)~(OriginalSByteArray[i] ^ (sbyte)mask);
        }
        [Benchmark]
        [BenchmarkCategory("XNOR UInt16", "Unoptimized")]
        public unsafe void XNORUInt16Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt16Array[i] = (ushort)~(OriginalUInt16Array[i] ^ mask);
        }
        [Benchmark]
        [BenchmarkCategory("XNOR UInt32", "Unoptimized")]
        public unsafe void XNORUInt32Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt32Array[i] = ~(OriginalUInt32Array[i] ^ mask);
        }
        [Benchmark]
        [BenchmarkCategory("XNOR UInt64", "Unoptimized")]
        public unsafe void XNORUInt64Array()
        {
            for (int i = 0; i < ArrayLength; i++)
                TargetUInt64Array[i] = ~(OriginalUInt64Array[i] ^ mask);
        }
        #endregion

    }
}
