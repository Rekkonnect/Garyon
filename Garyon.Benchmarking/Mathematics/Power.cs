using BenchmarkDotNet.Attributes;
using Garyon.Mathematics;
using System;

namespace Garyon.Benchmarking.Mathematics;

public class Power
{
    [Params(0, 1, 2, 3, 5)]
    public int Base;
    [Params(1, 2, 3, 4, 5, 7, 8, 12, 15, 32, 47)]
    public int Exponent;

    [Benchmark]
    public void MathPow()
    {
        Math.Pow(Base, Exponent);
    }

    [Benchmark]
    public void CustomPowerInt32()
    {
        GeneralMath.Power(Base, Exponent);
    }
    [Benchmark]
    public void CustomPowerInt64()
    {
        GeneralMath.Power(Base, (long)Exponent);
    }
}
