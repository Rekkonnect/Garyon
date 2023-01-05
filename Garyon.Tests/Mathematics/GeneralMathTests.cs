using Garyon.Mathematics;
using NUnit.Framework;
using System;
using System.Numerics;

namespace Garyon.Tests.Mathematics;

[Parallelizable(ParallelScope.Children)]
public class GeneralMathTests
{
    [Test]
    public void Power()
    {
        Assert.AreEqual(0, GeneralMath.Power(5, -1));
        Assert.AreEqual(0L, GeneralMath.Power(5L, -1));
        
        Assert.AreEqual(0, GeneralMath.Power(0, 5));
        Assert.AreEqual(0, GeneralMath.Power(0, 1));
        Assert.Throws<InvalidOperationException>(() => GeneralMath.Power(0, 0));
        
        Assert.AreEqual(0L, GeneralMath.Power(0L, 5));
        Assert.AreEqual(0L, GeneralMath.Power(0L, 1));
        Assert.Throws<InvalidOperationException>(() => GeneralMath.Power(0L, 0));
        
        for (int b = 1; b < 5; b++)
        {
            int currentBase = 1;

            for (int exponent = 0; exponent < 16; exponent++)
            {
                Assert.AreEqual(currentBase, GeneralMath.Power(b, exponent));
                Assert.AreEqual((long)currentBase, GeneralMath.Power((long)b, exponent));
                currentBase *= b;
            }
        }
    }
    [Test]
    public void Factorial()
    {
        Assert.AreEqual(1L, GeneralMath.Factorial(0));
        var r = new Random();
        for (int i = 0; i < 16; i++)
        {
            int n = -r.Next();
            Assert.Throws<ArgumentException>(() => GeneralMath.Factorial(n));
            Assert.Throws<ArgumentException>(() => GeneralMath.Factorial((double)n));
        }

        long currentResult = 1;

        for (int multiplier = 1; multiplier <= 20; multiplier++)
        {
            currentResult *= multiplier;
            Assert.AreEqual(currentResult, GeneralMath.Factorial(multiplier));
            Assert.AreEqual((double)currentResult, GeneralMath.Factorial((double)multiplier));
        }

        double currentDoubleResult = currentResult;

        for (int multiplier = 21; ; multiplier++)
        {
            currentDoubleResult *= multiplier;
            if (double.IsPositiveInfinity(currentDoubleResult))
                break;

            double delta = Math.Pow(10, (long)Math.Log10(currentDoubleResult) - 14);
            Assert.AreEqual(currentDoubleResult, GeneralMath.Factorial((double)multiplier), delta, multiplier.ToString());
        }
    }
    [Test]
    public void BigIntegerFactorial()
    {
        Assert.AreEqual(BigInteger.One, GeneralMath.FactorialBigInteger(0));            
        var r = new Random();
        for (int i = 0; i < 16; i++)
            Assert.Throws<ArgumentException>(() => GeneralMath.FactorialBigInteger(-r.Next()));

        BigInteger currentResult = 1;

        for (int multiplier = 1; multiplier <= 100; multiplier++)
        {
            currentResult *= multiplier;
            Assert.AreEqual(currentResult, GeneralMath.FactorialBigInteger(multiplier));
        }
    }
}
