using Garyon.Extensions;
using NUnit.Framework;
using System;

namespace Garyon.Tests.Extensions;

[Parallelizable(ParallelScope.Children)]
public class TimeSpanExtensionsTests
{
    private static readonly TimeSpan sample = new TimeSpan(29, 03, 20, 11, 190).Add(TimeSpan.FromTicks(4123));

    #region Within Time Unit
    [Test]
    public void WithinDayTest()
    {
        var expected = sample.WithDays(0);
        var actual = sample.WithinDay();
        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void WithinHourTest()
    {
        var expected = sample.WithDays(0).WithHours(0);
        var actual = sample.WithinHour();
        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void WithinMinuteTest()
    {
        var expected = sample.WithDays(0).WithHours(0).WithMinutes(0);
        var actual = sample.WithinMinute();
        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void WithinSecondTest()
    {
        var expected = sample.WithDays(0).WithHours(0).WithMinutes(0).WithSeconds(0);
        var actual = sample.WithinSecond();
        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void WithinMillisecondTest()
    {
        var expected = sample.WithDays(0).WithHours(0).WithMinutes(0).WithSeconds(0).WithMilliseconds(0);
        var actual = sample.WithinMillisecond();
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region With Components
    [Test]
    public void WithMillisecondsTest()
    {
        AssertEqualComponents(TimeSpanExtensions.WithMilliseconds, TimeSpanComponent.Milliseconds, 420);
    }
    [Test]
    public void WithSecondsTest()
    {
        AssertEqualComponents(TimeSpanExtensions.WithSeconds, TimeSpanComponent.Seconds, 35);
    }
    [Test]
    public void WithMinutesTest()
    {
        AssertEqualComponents(TimeSpanExtensions.WithMinutes, TimeSpanComponent.Minutes, 46);
    }
    [Test]
    public void WithHoursTest()
    {
        AssertEqualComponents(TimeSpanExtensions.WithHours, TimeSpanComponent.Hours, 11);
    }
    [Test]
    public void WithDaysTest()
    {
        AssertEqualComponents(TimeSpanExtensions.WithDays, TimeSpanComponent.Days, 12);
    }

    private static void AssertEqualComponents(Adjuster adjuster, TimeSpanComponent adjustedComponent, int adjustedValue)
    {
        var adjusted = adjuster(sample, adjustedValue);

        for (var component = TimeSpanComponent.Milliseconds; component <= TimeSpanComponent.Days; component++)
        {
            AssertEqualComponent(component);
        }

        void AssertEqualComponent(TimeSpanComponent testedComponent)
        {
            var adjustedTimeSpanComponent = adjusted.GetComponent(testedComponent);
            var expectedComponent = testedComponent == adjustedComponent ? adjustedValue : sample.GetComponent(testedComponent);
            Assert.AreEqual(expectedComponent, adjustedTimeSpanComponent);
        }
    }

    private delegate TimeSpan Adjuster(TimeSpan timeSpan, int adjustedComponentValue);

    // Combination component adjusters can be broken down into multiple
    [Test]
    public void WithHoursMinutesTest()
    {
        var expected = sample.WithHours(10).WithMinutes(30);
        var actual = sample.WithHoursMinutes(10, 30);
        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void WithMinutesSecondsTest()
    {
        var expected = sample.WithMinutes(59).WithSeconds(59);
        var actual = sample.WithMinutesSeconds(59, 59);
        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void WithSecondsMillisecondsTests()
    {
        var expected = sample.WithSeconds(21).WithMilliseconds(654);
        var actual = sample.WithSecondsMilliseconds(21, 654);
        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void WithMinutesSecondsMillisecondsTests()
    {
        var expected = sample.WithMinutes(45).WithSeconds(21).WithMilliseconds(654);
        var actual = sample.WithMinutesSecondsMilliseconds(45, 21, 654);
        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void WithHoursMinutesSecondsTests()
    {
        var expected = sample.WithHours(01).WithMinutes(12).WithSeconds(23);
        var actual = sample.WithHoursMinutesSeconds(01, 12, 23);
        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void WithHoursMinutesSecondsMillisecondsTests()
    {
        var expected = sample.WithHours(01).WithMinutes(12).WithSeconds(23).WithMilliseconds(456);
        var actual = sample.WithHoursMinutesSecondsMilliseconds(01, 12, 23, 456);
        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void WithDaysHoursTest()
    {
        var expected = sample.WithDays(43).WithHours(23);
        var actual = sample.WithDaysHours(43, 23);
        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void WithDaysHoursMinutesTest()
    {
        var expected = sample.WithDays(43).WithHours(23).WithMinutes(15);
        var actual = sample.WithDaysHoursMinutes(43, 23, 15);
        Assert.AreEqual(expected, actual);
    }
    #endregion
}
