using Garyon.Extensions;
using System;
using System.Threading.Tasks;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;

namespace Garyon.Tests.Extensions;

public class DateTimeExtensionsTests
{
    private static readonly DateTime sample = new DateTime(2020, 05, 29, 03, 20, 11, 190).AddTicks(4321);
    private static readonly DateTime previousRoundedYearSample = new(2020, 01, 01);
    private static readonly DateTime nextRoundedYearSample = new(2021, 01, 01);

    #region Individual Components
    [Test]
    public async Task WithMillisecondTest()
    {
        await AssertEqualComponents(DateTimeExtensions.WithMillisecond, DateTimeComponent.Millisecond, 420);
    }
    [Test]
    public async Task WithSecondTest()
    {
        await AssertEqualComponents(DateTimeExtensions.WithSecond, DateTimeComponent.Second, 35);
    }
    [Test]
    public async Task WithMinuteTest()
    {
        await AssertEqualComponents(DateTimeExtensions.WithMinute, DateTimeComponent.Minute, 46);
    }
    [Test]
    public async Task WithHourTest()
    {
        await AssertEqualComponents(DateTimeExtensions.WithHour, DateTimeComponent.Hour, 11);
    }
    [Test]
    public async Task WithDayTest()
    {
        await AssertEqualComponents(DateTimeExtensions.WithDay, DateTimeComponent.Day, 12);
    }
    [Test]
    public async Task WithMonthTest()
    {
        await AssertEqualComponents(DateTimeExtensions.WithMonth, DateTimeComponent.Month, 09);
    }
    [Test]
    public async Task WithYearTest()
    {
        await AssertEqualComponents(DateTimeExtensions.WithYear, DateTimeComponent.Year, 2022);
    }

    private static async Task AssertEqualComponents(Adjuster adjuster, DateTimeComponent adjustedComponent, int adjustedValue)
    {
        var adjusted = adjuster(sample, adjustedValue);

        for (var component = DateTimeComponent.Millisecond; component <= DateTimeComponent.Year; component++)
        {
            await AssertEqualComponent(component);
        }

        async Task AssertEqualComponent(DateTimeComponent testedComponent)
        {
            var adjustedDateTimeComponent = adjusted.GetComponent(testedComponent);
            var expectedComponent = testedComponent == adjustedComponent ? adjustedValue : sample.GetComponent(testedComponent);
            await Assert.That(adjustedDateTimeComponent).IsEqualTo(expectedComponent);
        }
    }

    private delegate DateTime Adjuster(DateTime dateTime, int adjustedComponentValue);

    // Combination component adjusters can be broken down into multiple
    [Test]
    public async Task WithHourMinuteTest()
    {
        var expected = sample.WithHour(10).WithMinute(30);
        var actual = sample.WithHourMinute(10, 30);
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithMinuteSecondTest()
    {
        var expected = sample.WithMinute(10).WithSecond(30);
        var actual = sample.WithMinuteSecond(10, 30);
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithSecondMillisecondTest()
    {
        var expected = sample.WithSecond(59).WithMillisecond(489);
        var actual = sample.WithSecondMillisecond(59, 489);
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithHourMinuteSecondTest()
    {
        var expected = sample.WithHour(13).WithMinute(59).WithSecond(58);
        var actual = sample.WithHourMinuteSecond(13, 59, 58);
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithHourMinuteSecondMillisecondTest()
    {
        var expected = sample.WithHour(13).WithMinute(59).WithSecond(58).WithMillisecond(987);
        var actual = sample.WithHourMinuteSecondMillisecond(13, 59, 58, 987);
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithMonthDayTest()
    {
        var expected = sample.WithMonth(12).WithDay(21);
        var actual = sample.WithMonthDay(12, 21);
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithYearMonthTest()
    {
        var expected = sample.WithYear(2022).WithMonth(5);
        var actual = sample.WithYearMonth(2022, 5);
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithYearMonthDayTest()
    {
        var expected = sample.WithYear(2022).WithMonth(5).WithDay(31);
        var actual = sample.WithYearMonthDay(2022, 5, 31);
        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task WithTimeTest()
    {
        var time = new TimeSpan(0, 12, 23, 42, 085);
        // Rounding must take place, since time span also includes individual ticks beyond the millisecond component, resetting them
        var expected = sample.WithHour(time.Hours).WithMinute(time.Minutes).WithSecond(time.Seconds).WithMillisecond(time.Milliseconds).RoundToPreviousMillisecond();
        var actual = sample.WithTime(time);
        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task WithDayOfWeekTest()
    {
        // Kinda trivial testing
        for (var starting = DayOfWeek.Sunday; starting <= DayOfWeek.Saturday; starting++)
        {
            var startingDate = sample.WithDayOfWeek(starting, starting);

            for (var next = DayOfWeek.Sunday; next <= DayOfWeek.Saturday; next++)
            {
                var nextDate = sample.WithDayOfWeek(next, starting);
                int offset = next.DaysSinceWeekStart(starting);

                var difference = nextDate - startingDate;
                await Assert.That(difference).IsEqualTo(TimeSpan.FromDays(offset));
            }
        }
    }
    #endregion

    #region Rounding
    [Test]
    public async Task RoundToNextMillisecondTest()
    {
        var afterRounding = new DateTime(2020, 05, 29, 03, 20, 11, 191);
        await AssertNextRounding(afterRounding, DateTimeExtensions.RoundToNextMillisecond);
    }
    [Test]
    public async Task RoundToNextSecondTest()
    {
        var afterRounding = new DateTime(2020, 05, 29, 03, 20, 12, 000);
        await AssertNextRounding(afterRounding, DateTimeExtensions.RoundToNextSecond);
    }
    [Test]
    public async Task RoundToNextMinuteTest()
    {
        var afterRounding = new DateTime(2020, 05, 29, 03, 21, 00, 000);
        await AssertNextRounding(afterRounding, DateTimeExtensions.RoundToNextMinute);
    }
    [Test]
    public async Task RoundToNextHourTest()
    {
        var afterRounding = new DateTime(2020, 05, 29, 04, 00, 00, 000);
        await AssertNextRounding(afterRounding, DateTimeExtensions.RoundToNextHour);
    }
    [Test]
    public async Task RoundToNextDayTest()
    {
        var afterRounding = new DateTime(2020, 05, 30, 00, 00, 00, 000);
        await AssertNextRounding(afterRounding, DateTimeExtensions.RoundToNextDay);
    }
    [Test]
    public async Task RoundToNextMonthTest()
    {
        var afterRounding = new DateTime(2020, 06, 01, 00, 00, 00, 000);
        await AssertNextRounding(afterRounding, DateTimeExtensions.RoundToNextMonth);
    }
    [Test]
    public async Task RoundToNextYearTest()
    {
        await AssertNextRounding(nextRoundedYearSample, DateTimeExtensions.RoundToNextYear);
    }

    [Test]
    public async Task RoundToPreviousMillisecondTest()
    {
        var afterRounding = new DateTime(2020, 05, 29, 03, 20, 11, 190);
        await AssertPreviousRounding(afterRounding, DateTimeExtensions.RoundToPreviousMillisecond);
    }
    [Test]
    public async Task RoundToPreviousSecondTest()
    {
        var afterRounding = new DateTime(2020, 05, 29, 03, 20, 11, 000);
        await AssertPreviousRounding(afterRounding, DateTimeExtensions.RoundToPreviousSecond);
    }
    [Test]
    public async Task RoundToPreviousMinuteTest()
    {
        var afterRounding = new DateTime(2020, 05, 29, 03, 20, 00, 000);
        await AssertPreviousRounding(afterRounding, DateTimeExtensions.RoundToPreviousMinute);
    }
    [Test]
    public async Task RoundToPreviousHourTest()
    {
        var afterRounding = new DateTime(2020, 05, 29, 03, 00, 00, 000);
        await AssertPreviousRounding(afterRounding, DateTimeExtensions.RoundToPreviousHour);
    }
    [Test]
    public async Task RoundToPreviousDayTest()
    {
        var afterRounding = new DateTime(2020, 05, 29, 00, 00, 00, 000);
        await AssertPreviousRounding(afterRounding, DateTimeExtensions.RoundToPreviousDay);
    }
    [Test]
    public async Task RoundToPreviousMonthTest()
    {
        var afterRounding = new DateTime(2020, 05, 01, 00, 00, 00, 000);
        await AssertPreviousRounding(afterRounding, DateTimeExtensions.RoundToPreviousMonth);
    }
    [Test]
    public async Task RoundToPreviousYearTest()
    {
        await AssertPreviousRounding(previousRoundedYearSample, DateTimeExtensions.RoundToPreviousYear);
    }

    private async Task AssertNextRounding(DateTime expectedRounded, DateTimeRounder rounder)
    {
        await AssertRounding(expectedRounded, rounder);

        await Assert.That(rounder(nextRoundedYearSample, true)).IsEqualTo(nextRoundedYearSample);
    }
    private async Task AssertPreviousRounding(DateTime expectedRounded, DateTimeRounder rounder)
    {
        await AssertRounding(expectedRounded, rounder);

        await Assert.That(rounder(previousRoundedYearSample, true)).IsEqualTo(previousRoundedYearSample);
    }
    private async Task AssertRounding(DateTime expectedRounded, DateTimeRounder rounder)
    {
        var result = rounder(sample, false);
        await Assert.That(result).IsEqualTo(expectedRounded);

        var furtherRounded = rounder(result, false);
        var retainedRounded = rounder(result, true);
        await Assert.That(result).IsNotEqualTo(furtherRounded);
        await Assert.That(result).IsEqualTo(retainedRounded);
    }

    private delegate DateTime DateTimeRounder(DateTime dateTime, bool retainIfRounded);
    #endregion
}