using Garyon.Objects;
using NUnit.Framework;
using System.Numerics;

namespace Garyon.Tests.Objects;

[Parallelizable(ParallelScope.Children)]
public class CacheableBigIntegerTests
{
    [Test]
    public void InitializationTest()
    {
        const long value0 = 3458;
        BigInteger value1 = BigInteger.Parse("4237894523145135423652643587459827");

        Assert.IsTrue(new CacheableBigInteger(value0).Value == value0);
        Assert.IsTrue(new CacheableBigInteger(value1).Value == value1);
    }

    [Test]
    public void OperationsTest()
    {
        BigInteger initial = BigInteger.Parse("4237894523145135423652643587459827");
        BigInteger result0 = initial * 5 * 2 * 10 * 100;
        BigInteger result1 = result0 + 50 + 100;
        BigInteger result2 = result1 / 10;
        BigInteger result3 = result2 - 5;
        BigInteger result4 = result3 * 10 * 1000000;
        BigInteger result5 = result4 - 1;
        BigInteger result6 = result5 / 100;

        var cacheable = new CacheableBigInteger(initial);
        cacheable.Multiply(5);
        cacheable.Multiply(2);
        cacheable.Multiply(10);
        cacheable.Multiply(100);
        cacheable.Add(50);
        cacheable.Add(100);
        cacheable.Divide(10);
        cacheable.Subtract(5);
        cacheable.Multiply(10);
        cacheable.Multiply(BigInteger.Parse("1000000"));
        cacheable.Subtract(BigInteger.Parse("1"));
        cacheable.Divide(BigInteger.Parse("100"));
        Assert.AreEqual(result6, cacheable.Value);
    }

    [Test]
    public void OverflowingTest()
    {
        var cacheable = new CacheableBigInteger(0);

        cacheable.Add(long.MaxValue);
        cacheable.Add(1);
        Assert.AreEqual(new BigInteger(long.MaxValue) + 1, cacheable.Value);

        cacheable.Value = new BigInteger(long.MaxValue) + 4;

        cacheable.Subtract(long.MaxValue);
        cacheable.Subtract(4);
        Assert.AreEqual(BigInteger.Zero, cacheable.Value);

        cacheable.Value = 1;

        cacheable.Multiply(long.MaxValue);
        cacheable.Multiply(2);
        Assert.AreEqual(new BigInteger(long.MaxValue) * 2, cacheable.Value);

        cacheable.Divide(long.MaxValue);
        cacheable.Divide(2);
        Assert.AreEqual(BigInteger.One, cacheable.Value);
    }
}
