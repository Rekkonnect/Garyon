using BenchmarkDotNet.Attributes;

namespace Garyon.Benchmarking.Mathematics;

public class Int64Factorial
{
    [Params(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)]
    public int Number;

    // Optimized implementation starts being faster at 4 onwards

    [Benchmark(Baseline = true)]
    public void Linear()
    {
        Linear(Number);
    }
    [Benchmark]
    public void Optimized()
    {
        Optimized(Number);
    }

    private static long Linear(long n)
    {
        long result = 1;
        for (long i = 2; i <= n; i++)
            result *= i;
        return result;
    }
    private static long Optimized(long n)
    {
        long result = 1;

        long currentFactor = n;
        long currentIncrement = currentFactor - 2;

        for (long i = 1; i <= n / 2; i++)
        {
            result *= currentFactor;

            currentFactor += currentIncrement;
            currentIncrement -= 2;
        }

        if (n % 2 == 1)
            result *= n / 2 + 1;

        return result;
    }
}
