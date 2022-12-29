using BenchmarkDotNet.Attributes;

namespace Garyon.Benchmarking.UsefulKnowledge;

public class SameStructDifferentFieldInitializations
{
    [Benchmark]
    public void BytesInit()
    {
        new Bytes();
    }
    [Benchmark]
    public void ShortsInit()
    {
        new Shorts();
    }
    [Benchmark]
    public void IntsInit()
    {
        new Ints();
    }
    [Benchmark(Baseline = true)]
    public void LongsInit()
    {
        new Longs();
    }
    [Benchmark]
    public void FixedBytesInit()
    {
        new FixedBytes();
    }
    [Benchmark]
    public void FixedShortsInit()
    {
        new FixedShorts();
    }
    [Benchmark]
    public void FixedIntsInit()
    {
        new FixedInts();
    }
    [Benchmark]
    public void FixedLongsInit()
    {
        new FixedLongs();
    }
    [Benchmark]
    public void MixedInit()
    {
        new Mixed();
    }

    private struct Bytes
    {
        byte v0, v1, v2, v3, v4, v5, v6, v7;
    }
    private struct Shorts
    {
        short v0, v1, v2, v3;
    }
    private struct Ints
    {
        int v0, v1;
    }
    private struct Longs
    {
        long v0;
    }
    private unsafe struct FixedBytes
    {
        fixed byte bytes[8];
    }
    private unsafe struct FixedShorts
    {
        fixed short shorts[4];
    }
    private unsafe struct FixedInts
    {
        fixed int ints[2];
    }
    private unsafe struct FixedLongs
    {
        fixed long longs[1];
    }
    private struct Mixed
    {
        byte b0, b1;
        short s0;
        int i0;
    }
}
