using BenchmarkDotNet.Attributes;
using Garyon.Objects.Enumerators;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Benchmarking.Extensions
{
    public class EnumerableFlattening2D
    {
        private readonly List<IEnumerable<int>> ints = new();

        [GlobalSetup]
        public void Setup()
        {
            for (int i = 0; i < 30; i++)
                ints.Add(Enumerable.Range(0, 20));
        }

        [Benchmark(Baseline = true)]
        public void InlineEnumerate()
        {
            foreach (var list in ints)
                foreach (int i in list)
                {
                    // Something
                }
        }
        [Benchmark]
        public void YieldFlatten()
        {
            foreach (int i in YieldFlatten2D(ints))
            {
                // Something
            }
        }
        [Benchmark]
        public void SelectMany()
        {
            foreach (int i in ints.SelectMany(a => a))
            {
                // Something
            }
        }
        [Benchmark]
        public void FlattenedEnumerables()
        {
            foreach (int i in new FlattenedEnumerables2D<int>(ints))
            {
                // Something
            }
        }

        private static IEnumerable<T> YieldFlatten2D<T>(IEnumerable<IEnumerable<T>> source)
        {
            foreach (var e in source)
                foreach (var v in e)
                    yield return v;
        }
    }
}
