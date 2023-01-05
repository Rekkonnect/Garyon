using Garyon.Objects;
using NUnit.Framework;
using System;

namespace Garyon.Tests.Objects;

[Parallelizable(ParallelScope.Children)]
public class MinuteSecondTests
{
    [Test]
    public void InitializationTest()
    {
        var ms = new MinuteSecond(11, 53);
        AssertProperties();

        ms = new MinuteSecond(11 * 60 + 53);
        AssertProperties();

        void AssertProperties()
        {
            Assert.AreEqual(11, ms.Minute);
            Assert.AreEqual(53, ms.Second);
            Assert.AreEqual(11 * 60 + 53, ms.TotalSeconds);
            Assert.AreEqual(11 + 53 / 60d, ms.TotalMinutes);
            Assert.AreEqual((11 * 60 + 53) / (60d * 60), ms.TotalHours);
        }
    }

    [Test]
    public void EqualsTest()
    {
        Assert.AreEqual(new MinuteSecond(11, 53), new MinuteSecond(11, 53));
    }

    [Test]
    public void AddTest()
    {
        var ms = new MinuteSecond(11, 53);
        ms.Add(4);
        Assert.AreEqual(new MinuteSecond(11, 57), ms);
        ms.Add(4);
        Assert.AreEqual(new MinuteSecond(12, 01), ms);
        ms.Add(62);
        Assert.AreEqual(new MinuteSecond(13, 03), ms);

        ms.Add(1, 4);
        Assert.AreEqual(new MinuteSecond(14, 07), ms);

        Assert.AreEqual(new MinuteSecond(15, 00), new MinuteSecond(14, 37) + 23);
        Assert.AreEqual(new MinuteSecond(15, 00), new MinuteSecond(13, 37) + new MinuteSecond(01, 23));
    }
    [Test]
    public void SubtractTest()
    {
        var ms = new MinuteSecond(12, 01);
        ms.Subtract(4);
        Assert.AreEqual(new MinuteSecond(11, 57), ms);
        ms.Subtract(4);
        Assert.AreEqual(new MinuteSecond(11, 53), ms);
        ms.Subtract(62);
        Assert.AreEqual(new MinuteSecond(10, 51), ms);

        ms.Subtract(1, 4);
        Assert.AreEqual(new MinuteSecond(09, 47), ms);

        Assert.AreEqual(new MinuteSecond(14, 37), new MinuteSecond(15, 00) - 23);
        Assert.AreEqual(new MinuteSecond(13, 37), new MinuteSecond(15, 00) - new MinuteSecond(01, 23));
    }

    [Test]
    public void ComparisonTest()
    {
        Assert.IsTrue(new MinuteSecond(10, 59) < new MinuteSecond(11, 00));
        Assert.IsFalse(new MinuteSecond(11, 00) < new MinuteSecond(11, 00));
        Assert.IsFalse(new MinuteSecond(11, 01) < new MinuteSecond(11, 00));

        Assert.IsTrue(new MinuteSecond(10, 59) <= new MinuteSecond(11, 00));
        Assert.IsTrue(new MinuteSecond(11, 00) <= new MinuteSecond(11, 00));
        Assert.IsFalse(new MinuteSecond(11, 01) <= new MinuteSecond(11, 00));

        Assert.IsFalse(new MinuteSecond(10, 59) == new MinuteSecond(11, 00));
        Assert.IsTrue(new MinuteSecond(11, 00) == new MinuteSecond(11, 00));
        Assert.IsFalse(new MinuteSecond(11, 01) == new MinuteSecond(11, 00));

        Assert.IsFalse(new MinuteSecond(10, 59) >= new MinuteSecond(11, 00));
        Assert.IsTrue(new MinuteSecond(11, 00) >= new MinuteSecond(11, 00));
        Assert.IsTrue(new MinuteSecond(11, 01) >= new MinuteSecond(11, 00));

        Assert.IsFalse(new MinuteSecond(10, 59) > new MinuteSecond(11, 00));
        Assert.IsFalse(new MinuteSecond(11, 00) > new MinuteSecond(11, 00));
        Assert.IsTrue(new MinuteSecond(11, 01) > new MinuteSecond(11, 00));

        Assert.IsTrue(new MinuteSecond(10, 59) != new MinuteSecond(11, 00));
        Assert.IsFalse(new MinuteSecond(11, 00) != new MinuteSecond(11, 00));
        Assert.IsTrue(new MinuteSecond(11, 01) != new MinuteSecond(11, 00));
    }

    [Test]
    public void PropertySetterTest()
    {
        var ms = new MinuteSecond();
        ms.TotalHours = 0.125;
        Assert.AreEqual(new MinuteSecond(07, 30), ms);
        ms.TotalMinutes = 14.25;
        Assert.AreEqual(new MinuteSecond(14, 15), ms);
        ms.Minute = 16;
        Assert.AreEqual(new MinuteSecond(16, 15), ms);
        ms.Second = 42;
        Assert.AreEqual(new MinuteSecond(16, 42), ms);
    }

    [Test]
    public void ParseTest()
    {
        Assert.AreEqual(new MinuteSecond(09, 52), MinuteSecond.Parse("09:52"));
        Assert.AreEqual(new MinuteSecond(09, 52), MinuteSecond.Parse("09:52:51"));
        Assert.AreEqual(new MinuteSecond(09, 03), MinuteSecond.Parse("9:03"));
        Assert.AreEqual(new MinuteSecond(21, 03), MinuteSecond.Parse("21:3"));
        Assert.AreEqual(new MinuteSecond(09, 03), MinuteSecond.Parse("9:3"));
    }

    [Test]
    public void ToStringTest()
    {
        Assert.AreEqual("09:32", new MinuteSecond(09, 32).ToString());
        Assert.AreEqual("16:00", new MinuteSecond(16, 00).ToString());
        Assert.AreEqual("00:00", new MinuteSecond(00, 00).ToString());
    }

    [Test(ExpectedResult = true)]
    public bool NowTest()
    {
        return EvaluateTwice(Evaluate);

        static bool Evaluate()
        {
            var msNow = MinuteSecond.Now;
            var dateTimeNow = DateTime.Now;

            return msNow == (MinuteSecond)dateTimeNow;
        }
    }
    [Test(ExpectedResult = true)]
    public bool NextMinuteTest()
    {
        return EvaluateTwice(Evaluate);

        static bool Evaluate()
        {
            var msNextMinute = MinuteSecond.NextMinute;
            var dateTimeNextMinute = DateTime.Now + new TimeSpan(0, 1, 0);

            return msNextMinute == (MinuteSecond)dateTimeNextMinute;
        }
    }
    [Test(ExpectedResult = true)]
    public bool NextSecondTest()
    {
        return EvaluateTwice(Evaluate);

        static bool Evaluate()
        {
            var msNextSecond = MinuteSecond.NextSecond;
            var dateTimeNextSecond = DateTime.Now + new TimeSpan(0, 0, 1);

            return msNextSecond == (MinuteSecond)dateTimeNextSecond;
        }
    }
    [Test(ExpectedResult = true)]
    public bool ToDateTimeTest()
    {
        return EvaluateTwice(Evaluate);

        static bool Evaluate()
        {
            var msNow = MinuteSecond.Now;
            DateTime dateTimeNow = msNow;

            return msNow == (MinuteSecond)dateTimeNow;
        }
    }
    [Test(ExpectedResult = true)]
    public bool ToDateTimeOffsetTest()
    {
        return EvaluateTwice(Evaluate);

        static bool Evaluate()
        {
            var msNow = MinuteSecond.Now;
            DateTimeOffset dateTimeOffsetNow = msNow;

            return msNow == (MinuteSecond)dateTimeOffsetNow;
        }
    }

    private bool EvaluateTwice(Func<bool> test)
    {
        // Evaluate twice for the unlikely event the Now properties are retrieved at a different second
        for (int i = 0; i < 2; i++)
            if (test())
                return true;

        return false;
    }
}
