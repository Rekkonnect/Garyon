using BenchmarkDotNet.Running;

namespace Garyon.Benchmarking;

public static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<ListPatternVsHardCoding>();
    }
}
