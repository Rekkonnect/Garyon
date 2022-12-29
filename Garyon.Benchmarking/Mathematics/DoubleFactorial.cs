using BenchmarkDotNet.Attributes;

namespace Garyon.Benchmarking.Mathematics;

public class DoubleFactorial
{
    [Params(5, 12, 16, 23, 27, 32, 36, 38, 39, 40, 42, 45, 49)]
    public double Number;

    // Optimized implementation starts being faster at 39 onwards

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

    private static double Linear(double n)
    {
        double result = 1;
        for (double i = 2; i <= n; i++)
            result *= i;
        return result;
    }
    private static double Optimized(double n)
    {
        double result = 1;

        double currentFactor = n;
        double currentIncrement = currentFactor - 2;

        for (double i = 1; i <= n / 2; i++)
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
