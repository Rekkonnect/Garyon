using BenchmarkDotNet.Attributes;
using Garyon.QualityControl.Extensions;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Benchmarking.UsefulKnowledge;

public class VectorLoading : ArrayManipulationExtensionsQualityControlAsset
{
    [Benchmark]
    public unsafe void PointerAlignmentOverhead()
    {
        int size = sizeof(Vector128<byte>);
        fixed (byte* a = OriginalByteArray)
        {
            byte* result = a - (int)a % size + size;
        }
    }
    [Benchmark]
    public unsafe void PointerOffsettingOverhead()
    {
        fixed (byte* a = OriginalByteArray)
        {
            byte* result = a + 1;
        }
    }

    [Benchmark(Baseline = true)]
    public unsafe void LoadVector128Aligned()
    {
        int size = sizeof(Vector128<byte>);
        fixed (byte* a = OriginalByteArray)
            Sse2.LoadAlignedVector128(a - (int)a % size + size);
    }
    [Benchmark]
    public unsafe void LoadVector128Unaligned0() => LoadVector128Unaligned(0);
    [Benchmark]
    public unsafe void LoadVector128Unaligned1() => LoadVector128Unaligned(1);
    [Benchmark]
    public unsafe void LoadVector128Unaligned2() => LoadVector128Unaligned(2);
    [Benchmark]
    public unsafe void LoadVector128Unaligned3() => LoadVector128Unaligned(3);
    [Benchmark]
    public unsafe void LoadVector128Unaligned4() => LoadVector128Unaligned(4);
    [Benchmark]
    public unsafe void LoadVector128Unaligned5() => LoadVector128Unaligned(5);
    [Benchmark]
    public unsafe void LoadVector128Unaligned6() => LoadVector128Unaligned(6);
    [Benchmark]
    public unsafe void LoadVector128Unaligned7() => LoadVector128Unaligned(7);
    [Benchmark]
    public unsafe void LoadVector128Unaligned8() => LoadVector128Unaligned(8);
    [Benchmark]
    public unsafe void LoadVector128Unaligned9() => LoadVector128Unaligned(9);
    [Benchmark]
    public unsafe void LoadVector128Unaligned10() => LoadVector128Unaligned(10);
    [Benchmark]
    public unsafe void LoadVector128Unaligned11() => LoadVector128Unaligned(11);
    [Benchmark]
    public unsafe void LoadVector128Unaligned12() => LoadVector128Unaligned(12);
    [Benchmark]
    public unsafe void LoadVector128Unaligned13() => LoadVector128Unaligned(13);
    [Benchmark]
    public unsafe void LoadVector128Unaligned14() => LoadVector128Unaligned(14);
    [Benchmark]
    public unsafe void LoadVector128Unaligned15() => LoadVector128Unaligned(15);

    private unsafe void LoadVector128Unaligned(int offset)
    {
        fixed (byte* a = OriginalByteArray)
            Sse2.LoadVector128(a + offset);
    }
}