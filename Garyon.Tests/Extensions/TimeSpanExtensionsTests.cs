using Garyon.Extensions;
using System;
using System.Threading.Tasks;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;

namespace Garyon.Tests.Extensions;

public class TimeSpanExtensionsTests
{
    private static readonly TimeSpan sample = new TimeSpan(29, 03, 20, 11, 190).Add(TimeSpan.FromTicks(4123));

    #region Within Time Unit
    [Test]
    public async Task WithinDayTest()
    {
        var expected = sample.WithDays(0);
        var actual = sample.WithinDay();
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithinHourTest()
    {
        var expected = sample.WithDays(0).WithHours(0);
        var actual = sample.WithinHour();
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithinMinuteTest()
    {
        var expected = sample.WithDays(0).WithHours(0).WithMinutes(0);
        var actual = sample.WithinMinute();
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithinSecondTest()
    {
        var expected = sample.WithDays(0).WithHours(0).WithMinutes(0).WithSeconds(0);
        var actual = sample.WithinSecond();
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithinMillisecondTest()
    {
        var expected = sample.WithDays(0).WithHours(0).WithMinutes(0).WithSeconds(0).WithMilliseconds(0);
        var actual = sample.WithinMillisecond();
        await Assert.That(actual).IsEqualTo(expected);
    }
    #endregion

    #region With Components
    [Test]
    public async Task WithMillisecondsTest()
    {
        await AssertEqualComponents(TimeSpanExtensions.WithMilliseconds, TimeSpanComponent.Milliseconds, 420);
    }
    [Test]
    public async Task WithSecondsTestAsync()
    {
        await AssertEqualComponents(TimeSpanExtensions.WithSeconds, TimeSpanComponent.Seconds, 35);
    }
    [Test]
    public async Task WithMinutesTestAsync()
    {
        await AssertEqualComponents(TimeSpanExtensions.WithMinutes, TimeSpanComponent.Minutes, 46);
    }
    [Test]
    public async Task WithHoursTestAsync()
    {
        await AssertEqualComponents(TimeSpanExtensions.WithHours, TimeSpanComponent.Hours, 11);
    }
    [Test]
    public async Task WithDaysTestAsync()
    {
        await AssertEqualComponents(TimeSpanExtensions.WithDays, TimeSpanComponent.Days, 12);
    }

    private async static Task AssertEqualComponents(Adjuster adjuster, TimeSpanComponent adjustedComponent, int adjustedValue)
    {
        var adjusted = adjuster(sample, adjustedValue);

        for (var component = TimeSpanComponent.Milliseconds; component <= TimeSpanComponent.Days; component++)
        {
            await AssertEqualComponent(component);
        }

        async Task AssertEqualComponent(TimeSpanComponent testedComponent)
        {
            var adjustedTimeSpanComponent = adjusted.GetComponent(testedComponent);
            var expectedComponent = testedComponent == adjustedComponent ? adjustedValue : sample.GetComponent(testedComponent);
            await Assert.That(adjustedTimeSpanComponent).IsEqualTo(expectedComponent);
        }
    }

    private delegate TimeSpan Adjuster(TimeSpan timeSpan, int adjustedComponentValue);

    // Combination component adjusters can be broken down into multiple
    [Test]
    public async Task WithHoursMinutesTest()
    {
        var expected = sample.WithHours(10).WithMinutes(30);
        var actual = sample.WithHoursMinutes(10, 30);
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithMinutesSecondsTest()
    {
        var expected = sample.WithMinutes(59).WithSeconds(59);
        var actual = sample.WithMinutesSeconds(59, 59);
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithSecondsMillisecondsTests()
    {
        var expected = sample.WithSeconds(21).WithMilliseconds(654);
        var actual = sample.WithSecondsMilliseconds(21, 654);
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithMinutesSecondsMillisecondsTests()
    {
        var expected = sample.WithMinutes(45).WithSeconds(21).WithMilliseconds(654);
        var actual = sample.WithMinutesSecondsMilliseconds(45, 21, 654);
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithHoursMinutesSecondsTests()
    {
        var expected = sample.WithHours(01).WithMinutes(12).WithSeconds(23);
        var actual = sample.WithHoursMinutesSeconds(01, 12, 23);
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithHoursMinutesSecondsMillisecondsTests()
    {
        var expected = sample.WithHours(01).WithMinutes(12).WithSeconds(23).WithMilliseconds(456);
        var actual = sample.WithHoursMinutesSecondsMilliseconds(01, 12, 23, 456);
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithDaysHoursTest()
    {
        var expected = sample.WithDays(43).WithHours(23);
        var actual = sample.WithDaysHours(43, 23);
        await Assert.That(actual).IsEqualTo(expected);
    }
    [Test]
    public async Task WithDaysHoursMinutesTest()
    {
        var expected = sample.WithDays(43).WithHours(23).WithMinutes(15);
        var actual = sample.WithDaysHoursMinutes(43, 23, 15);
        await Assert.That(actual).IsEqualTo(expected);
    }
    #endregion
}