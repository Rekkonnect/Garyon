using BenchmarkDotNet.Attributes;
using Garyon.Extensions.ArrayExtensions;
using Garyon.Extensions.ArrayExtensions.ArrayConverting;
using Garyon.Functions.PointerHelpers;
using Garyon.QualityControl.Extensions;
using System;

namespace Garyon.Benchmarking.Extensions;

public class SameTypeArrayCopying : ArrayManipulationExtensionsQualityControlAsset
{
    [Benchmark(Baseline = true)]
    public void Int32ArrayCopyingSystem()
    {
        Array.Copy(OriginalInt32Array, TargetInt32Array, ArrayLength);
    }
    //[Benchmark]
    public void Int32ArrayCopyingUnoptimized()
    {
        TargetInt32Array = OriginalInt32Array.CopyArray();
    }
    [Benchmark]
    public void Int32ArrayCopyingAccelerated()
    {
        OriginalInt32Array.CopyAccelerated(TargetInt32Array);
    }
    [Benchmark]
    public unsafe void Int32ArrayCopyingUnvirtualized()
    {
        fixed (int* o = OriginalInt32Array)
        fixed (int* t = TargetInt32Array)
            SIMDPointerConversion.CopyToArrayVector256(o, t, ArrayLength);
    }
}
