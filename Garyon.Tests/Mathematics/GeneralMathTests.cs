using Garyon.Mathematics;
using System;
using System.Numerics;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Mathematics;

public class GeneralMathTests
{
    [Test]
    public async Task Power()
    {
        await Assert.That(GeneralMath.Power(5, -1)).IsZero();
        await Assert.That(GeneralMath.Power(5L, -1)).IsZero();

        await Assert.That(GeneralMath.Power(0, 5)).IsZero();
        await Assert.That(GeneralMath.Power(0, 1)).IsZero();
        Assert.Throws<InvalidOperationException>(() => GeneralMath.Power(0, 0));

        await Assert.That(GeneralMath.Power(0L, 5)).IsZero();
        await Assert.That(GeneralMath.Power(0L, 1)).IsZero();
        Assert.Throws<InvalidOperationException>(() => GeneralMath.Power(0L, 0));

        for (int b = 1; b < 5; b++)
        {
            int currentBase = 1;

            for (int exponent = 0; exponent < 16; exponent++)
            {
                await Assert.That(GeneralMath.Power(b, exponent)).IsEqualTo(currentBase);
                await Assert.That(GeneralMath.Power((long)b, exponent)).IsEqualTo((long)currentBase);
                currentBase *= b;
            }
        }
    }

    [Test]
    public async Task Factorial()
    {
        await Assert.That(GeneralMath.Factorial(0)).IsEqualTo(1L);
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
            await Assert.That(GeneralMath.Factorial(multiplier)).IsEqualTo(currentResult);
            await Assert.That(GeneralMath.Factorial((double)multiplier)).IsEqualTo((double)currentResult);
        }

        double currentDoubleResult = currentResult;

        for (int multiplier = 21; ; multiplier++)
        {
            currentDoubleResult *= multiplier;
            if (double.IsPositiveInfinity(currentDoubleResult))
                break;

            double delta = Math.Pow(10, (long)Math.Log10(currentDoubleResult) - 14);
            await Assert.That(GeneralMath.Factorial((double)multiplier))
                .IsEqualTo(currentDoubleResult)
                .Within(delta)
                .Because(multiplier.ToString());
        }
    }

    [Test]
    public async Task BigIntegerFactorial()
    {
        await Assert.That(GeneralMath.FactorialBigInteger(0)).IsEqualTo(BigInteger.One);
        var r = new Random();
        for (int i = 0; i < 16; i++)
        {
            Assert.Throws<ArgumentException>(() => GeneralMath.FactorialBigInteger(-r.Next()));
        }

        BigInteger currentResult = 1;

        for (int multiplier = 1; multiplier <= 100; multiplier++)
        {
            currentResult *= multiplier;
            await Assert.That(GeneralMath.FactorialBigInteger(multiplier)).IsEqualTo(currentResult);
        }
    }

    [Test]
    public async Task MinMaxTest()
    {
        await Assert.That(GeneralMath.Min((byte)4, (byte)1, (byte)9)).IsEqualTo((byte)1);
        await Assert.That(GeneralMath.Min(new[] { 4, 1, 9 })).IsEqualTo(1);
        await Assert.That(GeneralMath.Min(System.Array.Empty<int>())).IsEqualTo(int.MinValue);
        await Assert.That(GeneralMath.Max((byte)4, (byte)1, (byte)9)).IsEqualTo((byte)9);
        await Assert.That(GeneralMath.Max(new[] { 4, 1, 9 })).IsEqualTo(9);
        await Assert.That(GeneralMath.Max(System.Array.Empty<int>())).IsEqualTo(int.MaxValue);
        await Assert.That(GeneralMath.Min(new long[] { 7, 2, 3 })).IsEqualTo(2L);
        await Assert.That(GeneralMath.Max(new long[] { 7, 2, 3 })).IsEqualTo(7L);
        await Assert.That(GeneralMath.Min(new ulong[] { 7, 2, 3 })).IsEqualTo(2UL);
        await Assert.That(GeneralMath.Max(new ulong[] { 7, 2, 3 })).IsEqualTo(7UL);
    }
}
