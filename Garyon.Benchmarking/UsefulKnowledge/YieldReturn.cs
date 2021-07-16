using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Garyon.Benchmarking.UsefulKnowledge
{
    public class YieldReturn
    {
        [Params(200, 2500)]
        public int Count;

        private IEnumerable<int> ints;

        [GlobalSetup]
        public void Setup()
        {
            ints = Enumerable.Range(0, Count);
        }

        [Benchmark(Baseline = true)]
        public void Direct()
        {
            foreach (int i in ints)
            {
                // Something
            }
        }
        [Benchmark]
        public void YieldDepth0()
        {
            foreach (int i in EnumerateDepth0(ints))
            {
                // Something
            }
        }
        [Benchmark]
        public void YieldDepth1()
        {
            foreach (int i in EnumerateDepth1(ints))
            {
                // Something
            }
        }
        [Benchmark]
        public void YieldDepth2()
        {
            foreach (int i in EnumerateDepth2(ints))
            {
                // Something
            }
        }
        [Benchmark]
        public void YieldDepth3()
        {
            foreach (int i in EnumerateDepth3(ints))
            {
                // Something
            }
        }
        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void YieldDepthInlined0()
        {
            foreach (int i in EnumerateDepthInlined0(ints))
            {
                // Something
            }
        }
        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void YieldDepthInlined1()
        {
            foreach (int i in EnumerateDepthInlined1(ints))
            {
                // Something
            }
        }
        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void YieldDepthInlined2()
        {
            foreach (int i in EnumerateDepthInlined2(ints))
            {
                // Something
            }
        }
        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void YieldDepthInlined3()
        {
            foreach (int i in EnumerateDepthInlined3(ints))
            {
                // Something
            }
        }

        private static IEnumerable<int> EnumerateDepth0(IEnumerable<int> ints)
        {
            foreach (int i in ints)
                yield return i;
        }
        private static IEnumerable<int> EnumerateDepth1(IEnumerable<int> ints)
        {
            foreach (int i in EnumerateDepth0(ints))
                yield return i;
        }
        private static IEnumerable<int> EnumerateDepth2(IEnumerable<int> ints)
        {
            foreach (int i in EnumerateDepth1(ints))
                yield return i;
        }
        private static IEnumerable<int> EnumerateDepth3(IEnumerable<int> ints)
        {
            foreach (int i in EnumerateDepth2(ints))
                yield return i;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<int> EnumerateDepthInlined0(IEnumerable<int> ints)
        {
            foreach (int i in ints)
                yield return i;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<int> EnumerateDepthInlined1(IEnumerable<int> ints)
        {
            foreach (int i in EnumerateDepthInlined0(ints))
                yield return i;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<int> EnumerateDepthInlined2(IEnumerable<int> ints)
        {
            foreach (int i in EnumerateDepthInlined1(ints))
                yield return i;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<int> EnumerateDepthInlined3(IEnumerable<int> ints)
        {
            foreach (int i in EnumerateDepthInlined2(ints))
                yield return i;
        }
    }
}
