using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace Garyon.Benchmarking.UsefulKnowledge
{
    public class DictionaryInitializer
    {
        [Benchmark]
        public void InitializeWithAdd()
        {
            var d = new Dictionary<int, int>
            {
                { 1, 2 },
                { 2, 5 },
                { 3, 8 },
                { 4, 11 },
            };
        }

        [Benchmark]
        public void InitializeWithThisAccessor()
        {
            var d = new Dictionary<int, int>
            {
                [1] = 2,
                [2] = 5,
                [3] = 8,
                [4] = 11,
            };
        }
    }
}
