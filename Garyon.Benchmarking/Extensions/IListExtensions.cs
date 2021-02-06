using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;

namespace Garyon.Benchmarking.Extensions
{
    [SimpleJob(RuntimeMoniker.NetCoreApp50)]
    public class IListExtensions
    {
        private List<int> list = new();

        public IListExtensions()
        {
            for (int i = 0; i < 15; i++)
                list.Add(i);
        }

        [Benchmark(Baseline = true)]
        public void Swap()
        {
            for (int i = 0; i < 65536; i++)
                Swap(^(i % list.Count + 1), ^(i % list.Count + 1));
        }
        [Benchmark]
        public void SwapEfficient()
        {
            for (int i = 0; i < 65536; i++)
                SwapEfficient(^(i % list.Count + 1), ^(i % list.Count + 1));
        }

        private void Swap(Index a, Index b)
        {
            int t = list[a];
            list[a] = list[b];
            list[b] = t;
        }

        private void SwapEfficient(Index a, Index b)
        {
            int count = list.Count;
            int offsetA = a.GetOffset(count);
            int offsetB = b.GetOffset(count);
            int t = list[offsetA];
            list[offsetA] = list[offsetB];
            list[offsetB] = t;
        }
    }
}
