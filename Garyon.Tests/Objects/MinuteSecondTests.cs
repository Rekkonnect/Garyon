using Garyon.Objects;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Assertions.Sources;
using TUnit.Core;

namespace Garyon.Tests.Objects;

public class MinuteSecondTests
{
    [Test]
    public async Task InitializationTest()
    {
        var ms = new MinuteSecond(11, 53);
        await AssertPropertiesAsync();

        ms = new MinuteSecond(11 * 60 + 53);
        await AssertPropertiesAsync();

        async Task AssertPropertiesAsync()
        {
            await Assert.That(ms.Minute).IsEqualTo(11);
            await Assert.That(ms.Second).IsEqualTo(53);
            await Assert.That(ms.TotalSeconds).IsEqualTo(11 * 60 + 53);
            await Assert.That(ms.TotalMinutes).IsEqualTo(11 + 53 / 60d);
            await Assert.That(ms.TotalHours).IsEqualTo((11 * 60 + 53) / (60d * 60));
        }
    }

    [Test]
    public async Task EqualsTest()
    {
        MinuteSecond expected = new(11, 53);
        await Assert.That(new MinuteSecond(11, 53)).IsEqualTo(expected);
    }

    [Test]
    public async Task AddTest()
    {
        var ms = new MinuteSecond(11, 53);
        ms.Add(4);
        await Assert.That(ms).IsEqualTo(new MinuteSecond(11, 57));
        ms.Add(4);
        await Assert.That(ms).IsEqualTo(new MinuteSecond(12, 01));
        ms.Add(62);
        await Assert.That(ms).IsEqualTo(new MinuteSecond(13, 03));

        ms.Add(1, 4);
        await Assert.That(ms).IsEqualTo(new MinuteSecond(14, 07));

        await Assert.That(new MinuteSecond(14, 37) + 23).IsEqualTo(new MinuteSecond(15, 00));
        await Assert.That(new MinuteSecond(13, 37) + new MinuteSecond(01, 23)).IsEqualTo(new MinuteSecond(15, 00));
    }
    [Test]
    public async Task SubtractTest()
    {
        var ms = new MinuteSecond(12, 01);
        ms.Subtract(4);
        await Assert.That(ms).IsEqualTo(new MinuteSecond(11, 57));
        ms.Subtract(4);
        await Assert.That(ms).IsEqualTo(new MinuteSecond(11, 53));
        ms.Subtract(62);
        await Assert.That(ms).IsEqualTo(new MinuteSecond(10, 51));

        ms.Subtract(1, 4);
        await Assert.That(ms).IsEqualTo(new MinuteSecond(09, 47));

        await Assert.That(new MinuteSecond(15, 00) - 23).IsEqualTo(new MinuteSecond(14, 37));
        await Assert.That(new MinuteSecond(15, 00) - new MinuteSecond(01, 23)).IsEqualTo(new MinuteSecond(13, 37));
    }

    [Test]
    public async Task ComparisonTest()
    {
        await Assert.That(new MinuteSecond(10, 59) < new MinuteSecond(11, 00)).IsTrue();
        await Assert.That(new MinuteSecond(11, 00) < new MinuteSecond(11, 00)).IsFalse();
        await Assert.That(new MinuteSecond(11, 01) < new MinuteSecond(11, 00)).IsFalse();

        await Assert.That(new MinuteSecond(10, 59) <= new MinuteSecond(11, 00)).IsTrue();
        await Assert.That(new MinuteSecond(11, 00) <= new MinuteSecond(11, 00)).IsTrue();
        await Assert.That(new MinuteSecond(11, 01) <= new MinuteSecond(11, 00)).IsFalse();

        await Assert.That(new MinuteSecond(10, 59) == new MinuteSecond(11, 00)).IsFalse();
        await Assert.That(new MinuteSecond(11, 00) == new MinuteSecond(11, 00)).IsTrue();
        await Assert.That(new MinuteSecond(11, 01) == new MinuteSecond(11, 00)).IsFalse();

        await Assert.That(new MinuteSecond(10, 59) >= new MinuteSecond(11, 00)).IsFalse();
        await Assert.That(new MinuteSecond(11, 00) >= new MinuteSecond(11, 00)).IsTrue();
        await Assert.That(new MinuteSecond(11, 01) >= new MinuteSecond(11, 00)).IsTrue();

        await Assert.That(new MinuteSecond(10, 59) > new MinuteSecond(11, 00)).IsFalse();
        await Assert.That(new MinuteSecond(11, 00) > new MinuteSecond(11, 00)).IsFalse();
        await Assert.That(new MinuteSecond(11, 01) > new MinuteSecond(11, 00)).IsTrue();

        await Assert.That(new MinuteSecond(10, 59) != new MinuteSecond(11, 00)).IsTrue();
        await Assert.That(new MinuteSecond(11, 00) != new MinuteSecond(11, 00)).IsFalse();
        await Assert.That(new MinuteSecond(11, 01) != new MinuteSecond(11, 00)).IsTrue();
    }

    [Test]
    public async Task PropertySetterTest()
    {
        var ms = new MinuteSecond();
        ms.TotalHours = 0.125;
        await Assert.That(ms).IsEqualTo(new MinuteSecond(07, 30));
        ms.TotalMinutes = 14.25;
        await Assert.That(ms).IsEqualTo(new MinuteSecond(14, 15));
        ms.Minute = 16;
        await Assert.That(ms).IsEqualTo(new MinuteSecond(16, 15));
        ms.Second = 42;
        await Assert.That(ms).IsEqualTo(new MinuteSecond(16, 42));
    }

    [Test]
    public async Task ParseTest()
    {
        await Assert.That(MinuteSecond.Parse("09:52")).IsEqualTo(new MinuteSecond(09, 52));
        await Assert.That(MinuteSecond.Parse("09:52:51")).IsEqualTo(new MinuteSecond(09, 52));
        await Assert.That(MinuteSecond.Parse("9:03")).IsEqualTo(new MinuteSecond(09, 03));
        await Assert.That(MinuteSecond.Parse("21:3")).IsEqualTo(new MinuteSecond(21, 03));
        await Assert.That(MinuteSecond.Parse("9:3")).IsEqualTo(new MinuteSecond(09, 03));
    }

    [Test]
    public async Task ToStringTest()
    {
        await Assert.That(new MinuteSecond(09, 32).ToString()).IsEqualTo("09:32");
        await Assert.That(new MinuteSecond(16, 00).ToString()).IsEqualTo("16:00");
        await Assert.That(new MinuteSecond(00, 00).ToString()).IsEqualTo("00:00");
    }

    [Test]
    public async Task NowTest()
    {
        await ConstructEvaluateTwice(Evaluate).IsTrue();

        static bool Evaluate()
        {
            var msNow = MinuteSecond.Now;
            var dateTimeNow = DateTime.Now;

            return msNow == (MinuteSecond)dateTimeNow;
        }
    }
    [Test]
    public async Task NextMinuteTest()
    {
        await ConstructEvaluateTwice(Evaluate).IsTrue();

        static bool Evaluate()
        {
            var msNextMinute = MinuteSecond.NextMinute;
            var dateTimeNextMinute = DateTime.Now + new TimeSpan(0, 1, 0);

            return msNextMinute == (MinuteSecond)dateTimeNextMinute;
        }
    }
    [Test]
    public async Task NextSecondTest()
    {
        await ConstructEvaluateTwice(Evaluate).IsTrue();

        static bool Evaluate()
        {
            var msNextSecond = MinuteSecond.NextSecond;
            var dateTimeNextSecond = DateTime.Now + new TimeSpan(0, 0, 1);

            return msNextSecond == (MinuteSecond)dateTimeNextSecond;
        }
    }
    [Test]
    public async Task ToDateTimeTest()
    {
        await ConstructEvaluateTwice(Evaluate).IsTrue();

        static bool Evaluate()
        {
            var msNow = MinuteSecond.Now;
            DateTime dateTimeNow = msNow;

            return msNow == (MinuteSecond)dateTimeNow;
        }
    }
    [Test]
    public async Task ToDateTimeOffsetTest()
    {
        await ConstructEvaluateTwice(Evaluate).IsTrue();

        static bool Evaluate()
        {
            var msNow = MinuteSecond.Now;
            DateTimeOffset dateTimeOffsetNow = msNow;

            return msNow == (MinuteSecond)dateTimeOffsetNow;
        }
    }

    private static bool EvaluateTwice(Func<bool> test)
    {
        // Evaluate twice for the unlikely event the Now properties are retrieved at a different second
        for (int i = 0; i < 2; i++)
            if (test())
                return true;

        return false;
    }

#pragma warning disable TUnitAssertions0002 // Assert statement not awaited
    private static ValueAssertion<bool> ConstructEvaluateTwice(Func<bool> test)
    {
        return Assert.That(EvaluateTwice(test));
    }
#pragma warning restore TUnitAssertions0002 // Assert statement not awaited
}