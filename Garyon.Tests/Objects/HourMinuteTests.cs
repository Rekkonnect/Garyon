using Garyon.Objects;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Assertions.Sources;
using TUnit.Core;

namespace Garyon.Tests.Objects;

public class HourMinuteTests
{
    [Test]
    public async Task InitializationTest()
    {
        var hm = new HourMinute(11, 53);
        await AssertPropertiesAsync();

        hm = new HourMinute(11 * 60 + 53);
        await AssertPropertiesAsync();

        async Task AssertPropertiesAsync()
        {
            await Assert.That(hm.Hour).IsEqualTo(11);
            await Assert.That(hm.Minute).IsEqualTo(53);
            await Assert.That(hm.TotalSeconds).IsEqualTo((11 * 60 + 53) * 60);
            await Assert.That(hm.TotalMinutes).IsEqualTo(11 * 60 + 53);
            await Assert.That(hm.TotalHours).IsEqualTo(11 + 53 / 60d);
        }
    }

    [Test]
    public async Task EqualsTest()
    {
        HourMinute expected = new(11, 53);
        await Assert.That(new HourMinute(11, 53)).IsEqualTo(expected);
    }

    [Test]
    public async Task AddTest()
    {
        var hm = new HourMinute(11, 53);
        hm.Add(4);
        await Assert.That(hm).IsEqualTo(new HourMinute(11, 57));
        hm.Add(4);
        await Assert.That(hm).IsEqualTo(new HourMinute(12, 01));
        hm.Add(62);
        await Assert.That(hm).IsEqualTo(new HourMinute(13, 03));

        hm.Add(1, 4);
        await Assert.That(hm).IsEqualTo(new HourMinute(14, 07));

        await Assert.That(new HourMinute(14, 37) + 23).IsEqualTo(new HourMinute(15, 00));
        await Assert.That(new HourMinute(13, 37) + new HourMinute(01, 23)).IsEqualTo(new HourMinute(15, 00));
    }
    [Test]
    public async Task SubtractTest()
    {
        var hm = new HourMinute(12, 01);
        hm.Subtract(4);
        await Assert.That(hm).IsEqualTo(new HourMinute(11, 57));
        hm.Subtract(4);
        await Assert.That(hm).IsEqualTo(new HourMinute(11, 53));
        hm.Subtract(62);
        await Assert.That(hm).IsEqualTo(new HourMinute(10, 51));

        hm.Subtract(1, 4);
        await Assert.That(hm).IsEqualTo(new HourMinute(09, 47));

        await Assert.That(new HourMinute(15, 00) - 23).IsEqualTo(new HourMinute(14, 37));
        await Assert.That(new HourMinute(15, 00) - new HourMinute(01, 23)).IsEqualTo(new HourMinute(13, 37));
    }

    [Test]
    public async Task ComparisonTest()
    {
        await Assert.That(new HourMinute(10, 59) < new HourMinute(11, 00)).IsTrue();
        await Assert.That(new HourMinute(11, 00) < new HourMinute(11, 00)).IsFalse();
        await Assert.That(new HourMinute(11, 01) < new HourMinute(11, 00)).IsFalse();

        await Assert.That(new HourMinute(10, 59) <= new HourMinute(11, 00)).IsTrue();
        await Assert.That(new HourMinute(11, 00) <= new HourMinute(11, 00)).IsTrue();
        await Assert.That(new HourMinute(11, 01) <= new HourMinute(11, 00)).IsFalse();

        await Assert.That(new HourMinute(10, 59) == new HourMinute(11, 00)).IsFalse();
        await Assert.That(new HourMinute(11, 00) == new HourMinute(11, 00)).IsTrue();
        await Assert.That(new HourMinute(11, 01) == new HourMinute(11, 00)).IsFalse();

        await Assert.That(new HourMinute(10, 59) >= new HourMinute(11, 00)).IsFalse();
        await Assert.That(new HourMinute(11, 00) >= new HourMinute(11, 00)).IsTrue();
        await Assert.That(new HourMinute(11, 01) >= new HourMinute(11, 00)).IsTrue();

        await Assert.That(new HourMinute(10, 59) > new HourMinute(11, 00)).IsFalse();
        await Assert.That(new HourMinute(11, 00) > new HourMinute(11, 00)).IsFalse();
        await Assert.That(new HourMinute(11, 01) > new HourMinute(11, 00)).IsTrue();

        await Assert.That(new HourMinute(10, 59) != new HourMinute(11, 00)).IsTrue();
        await Assert.That(new HourMinute(11, 00) != new HourMinute(11, 00)).IsFalse();
        await Assert.That(new HourMinute(11, 01) != new HourMinute(11, 00)).IsTrue();
    }

    [Test]
    public async Task PropertySetterTest()
    {
        var hm = new HourMinute();
        hm.TotalHours = 14.25;
        await Assert.That(hm).IsEqualTo(new HourMinute(14, 15));
        hm.Hour = 16;
        await Assert.That(hm).IsEqualTo(new HourMinute(16, 15));
        hm.Minute = 42;
        await Assert.That(hm).IsEqualTo(new HourMinute(16, 42));
    }

    [Test]
    public async Task ParseTest()
    {
        await Assert.That(HourMinute.Parse("09:52")).IsEqualTo(new HourMinute(09, 52));
        await Assert.That(HourMinute.Parse("09:52:51")).IsEqualTo(new HourMinute(09, 52));
        await Assert.That(HourMinute.Parse("9:03")).IsEqualTo(new HourMinute(09, 03));
        await Assert.That(HourMinute.Parse("21:3")).IsEqualTo(new HourMinute(21, 03));
        await Assert.That(HourMinute.Parse("9:3")).IsEqualTo(new HourMinute(09, 03));
    }

    [Test]
    public async Task ToStringTest()
    {
        await Assert.That(new HourMinute(09, 32).ToString()).IsEqualTo("09:32");
        await Assert.That(new HourMinute(16, 00).ToString()).IsEqualTo("16:00");
        await Assert.That(new HourMinute(00, 00).ToString()).IsEqualTo("00:00");
    }

    [Test]
    public async Task NowTest()
    {
        await ConstructEvaluateTwice(Evaluate).IsTrue();

        static bool Evaluate()
        {
            var hmNow = HourMinute.Now;
            var dateTimeNow = DateTime.Now;

            return hmNow == (HourMinute)dateTimeNow;
        }
    }
    [Test]
    public async Task NextMinuteTest()
    {
        await ConstructEvaluateTwice(Evaluate).IsTrue();

        static bool Evaluate()
        {
            var hmNextMinute = HourMinute.NextMinute;
            var dateTimeNextMinute = DateTime.Now + new TimeSpan(0, 1, 0);

            return hmNextMinute == (HourMinute)dateTimeNextMinute;
        }
    }
    [Test]
    public async Task NextHourTest()
    {
        await ConstructEvaluateTwice(Evaluate).IsTrue();

        static bool Evaluate()
        {
            var hmNextHour = HourMinute.NextHour;
            var dateTimeNextHour = DateTime.Now + new TimeSpan(1, 0, 0);

            return hmNextHour == (HourMinute)dateTimeNextHour;
        }
    }
    [Test]
    public async Task ToDateTimeTest()
    {
        await ConstructEvaluateTwice(Evaluate).IsTrue();

        static bool Evaluate()
        {
            var hmNow = HourMinute.Now;
            DateTime dateTimeNow = hmNow;

            return hmNow == (HourMinute)dateTimeNow;
        }
    }
    [Test]
    public async Task ToDateTimeOffsetTest()
    {
        await ConstructEvaluateTwice(Evaluate).IsTrue();

        static bool Evaluate()
        {
            var hmNow = HourMinute.Now;
            DateTimeOffset dateTimeOffsetNow = hmNow;

            return hmNow == (HourMinute)dateTimeOffsetNow;
        }
    }

    private static bool EvaluateTwice(Func<bool> test)
    {
        // Evaluate twice for the extremely unlikely event the Now properties are retrieved at a different minute
        // It would be interesting to calculate how unlikely this event is, but it's probable nonetheless
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