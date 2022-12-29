using BenchmarkDotNet.Attributes;
using Garyon.Objects.Enumerators;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Benchmarking.Extensions;

public class EnumerableFlattening3D
{
    private readonly List<List<IEnumerable<int>>> ints = new();

    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < 10; i++)
        {
            ints.Add(new());
            for (int j = 0; j < 4; j++)
                ints[i].Add(Enumerable.Range(0, 15));
        }
    }

    [Benchmark(Baseline = true)]
    public void InlineEnumerate()
    {
        foreach (var list0 in ints)
            foreach (var list1 in list0)
                foreach (int i in list1)
                {
                    // Something
                }
    }
    [Benchmark]
    public void YieldFlatten()
    {
        foreach (int i in YieldFlatten3D(ints))
        {
            // Something
        }
    }
    [Benchmark]
    public void SelectManyTwice()
    {
        foreach (int i in ints.SelectMany(a => a).SelectMany(a => a))
        {
            // Something
        }
    }
    [Benchmark]
    public void FlattenedEnumerables()
    {
        foreach (int i in new FlattenedEnumerables3D<int>(ints))
        {
            // Something
        }
    }
    [Benchmark]
    public void FlattenedEnumerablesBackedEnumerator()
    {
        var enumerator = new FlattenedEnumerables3D<int>(ints).GetBackedEnumerator();
        while (enumerator.MoveNext())
        {
            int i = enumerator.Current;
        }
        enumerator.Dispose();
    }

    private static IEnumerable<T> YieldFlatten3D<T>(IEnumerable<IEnumerable<IEnumerable<T>>> source)
    {
        foreach (var e0 in source)
            foreach (var e1 in e0)
                foreach (var v in e1)
                    yield return v;
    }
}
