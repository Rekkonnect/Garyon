using BenchmarkDotNet.Attributes;
using Garyon.Functions;
using System;

namespace Garyon.Benchmarking.Functions;

public class EnumIsDefined
{
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Byte")]
    public void StandardByte()
    {
        Enum.IsDefined(typeof(EnumByte), (byte)4);
    }
    [Benchmark]
    [BenchmarkCategory("Byte")]
    public void CustomByte()
    {
        EnumHelpers.IsDefined<EnumByte, byte>(4);
    }

    [Benchmark]
    [BenchmarkCategory("SByte")]
    public void StandardSByte()
    {
        Enum.IsDefined(typeof(EnumSByte), (sbyte)4);
    }
    [Benchmark]
    [BenchmarkCategory("SByte")]
    public void CustomSByte()
    {
        EnumHelpers.IsDefined<EnumSByte, sbyte>(4);
    }

    [Benchmark]
    [BenchmarkCategory("Int16")]
    public void StandardInt16()
    {
        Enum.IsDefined(typeof(EnumInt16), (short)4);
    }
    [Benchmark]
    [BenchmarkCategory("Int16")]
    public void CustomInt16()
    {
        EnumHelpers.IsDefined<EnumInt16, short>(4);
    }

    [Benchmark]
    [BenchmarkCategory("UInt16")]
    public void StandardUInt16()
    {
        Enum.IsDefined(typeof(EnumUInt16), (ushort)4);
    }
    [Benchmark]
    [BenchmarkCategory("UInt16")]
    public void CustomUInt16()
    {
        EnumHelpers.IsDefined<EnumUInt16, ushort>(4);
    }

    [Benchmark]
    [BenchmarkCategory("Int32")]
    public void StandardInt32()
    {
        Enum.IsDefined(typeof(EnumInt32), 4);
    }
    [Benchmark]
    [BenchmarkCategory("Int32")]
    public void CustomInt32()
    {
        EnumHelpers.IsDefined<EnumInt32, int>(4);
    }

    [Benchmark]
    [BenchmarkCategory("UInt32")]
    public void StandardUInt32()
    {
        Enum.IsDefined(typeof(EnumUInt32), (uint)4);
    }
    [Benchmark]
    [BenchmarkCategory("UInt32")]
    public void CustomUInt32()
    {
        EnumHelpers.IsDefined<EnumUInt32, uint>(4);
    }

    [Benchmark]
    [BenchmarkCategory("Int64")]
    public void StandardInt64()
    {
        Enum.IsDefined(typeof(EnumInt64), (long)4);
    }
    [Benchmark]
    [BenchmarkCategory("Int64")]
    public void CustomInt64()
    {
        EnumHelpers.IsDefined<EnumInt64, long>(4);
    }

    [Benchmark]
    [BenchmarkCategory("UInt64")]
    public void StandardUInt64()
    {
        Enum.IsDefined(typeof(EnumUInt64), (ulong)4);
    }
    [Benchmark]
    [BenchmarkCategory("UInt64")]
    public void CustomUInt64()
    {
        EnumHelpers.IsDefined<EnumUInt64, ulong>(4);
    }

    private enum EnumByte : byte { A, B, C, D, E }
    private enum EnumSByte : sbyte { A, B, C, D, E }
    private enum EnumInt16 : short { A, B, C, D, E }
    private enum EnumUInt16 : ushort { A, B, C, D, E }
    private enum EnumInt32 : int { A, B, C, D, E }
    private enum EnumUInt32 : uint { A, B, C, D, E }
    private enum EnumInt64 : long { A, B, C, D, E }
    private enum EnumUInt64 : ulong { A, B, C, D, E }
}
