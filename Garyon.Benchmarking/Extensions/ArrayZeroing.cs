using BenchmarkDotNet.Attributes;
using Garyon.Benchmarking.Exceptions;
using Garyon.QualityControl.Extensions;
using System;
using static Garyon.Exceptions.ThrowHelper;
using static Garyon.Extensions.ArrayExtensions.UnsafeArrayZeroingHelpers;

namespace Garyon.Benchmarking.Extensions
{
    [IterationCount(25)]
    public class ArrayZeroing : ArrayManipulationExtensionsQualityControlAsset
    {
        [Benchmark]
        public unsafe void ByteArrayZeroingVector256()
        {
            fixed (byte* t = TargetByteArray)
                if (!ZeroOutByteArrayVector256(t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
        [Benchmark(Baseline = true)]
        public unsafe void ByteArrayZeroing()
        {
            Array.Clear(TargetByteArray, 0, ArrayLength);
        }
        [Benchmark]
        public unsafe void ByteArrayZeroingVector128()
        {
            fixed (byte* t = TargetByteArray)
                if (!ZeroOutByteArrayVector128(t, ArrayLength))
                    Throw<InstructionSetBenchmarkException>();
        }
    }
}