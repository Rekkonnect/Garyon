using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace Garyon.Benchmarking;

public class ListPatternVsHardCoding
{
    private readonly IReadOnlyList<B> _single = [new(1)];
    private readonly IReadOnlyList<B> _empty = [];
    private readonly IReadOnlyList<B> _multi = [new(1), new(2), new(3)];
    private readonly IReadOnlyList<A> _singleNonMatching = [new(1)];

    public IReadOnlyList<IReadOnlyList<A>> ListValues()
    {
        return
        [
            (IReadOnlyList<A>)_single,
            (IReadOnlyList<A>)_empty,
            (IReadOnlyList<A>)_multi,
            _singleNonMatching,
        ];
    }

    [ParamsSource(nameof(ListValues))]
    public IReadOnlyList<A> List;

    [Benchmark(Baseline = true)]
    public (bool, A) Pattern()
    {
        if (List is [B single])
        {
            return (true, single);
        }
        return (false, null);
    }
    [Benchmark]
    public (bool, A) HardCoded()
    {
        if (List.Count is 1 && List[0] is B single)
        {
            return (true, single);
        }
        return (false, null);
    }
    [Benchmark]
    public (bool, A) HardCoded2()
    {
        B single;
        if (List.Count is 1)
        {
            single = List[0] as B;
            return (true, single);
        }
        return (false, null);
    }

    public record A(int Value);
    public record B(int Value) : A(Value);
}
