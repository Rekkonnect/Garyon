using Garyon.Extensions;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class ExtendedDateTimeExtensionsTests
{
    [Test]
    public async Task DateOnlyConversionTest()
    {
        var dateTime = new DateTime(2024, 2, 3, 4, 5, 6);
        var dateOnly = dateTime.ToDateOnly();

        await Assert.That(dateOnly).IsEqualTo(new DateOnly(2024, 2, 3));
        await Assert.That(dateOnly.ToDateTime()).IsEqualTo(new DateTime(2024, 2, 3));
    }

    [Test]
    public async Task DateOnlySubtractTest()
    {
        var left = new DateOnly(2024, 2, 10);
        var right = new DateOnly(2024, 2, 3);

        await Assert.That(left.Subtract(right)).IsEqualTo(TimeSpan.FromDays(7));
    }

    [Test]
    public async Task DateTimeAndTimeSpanComponentHelpersTest()
    {
        var dateTime = new DateTime(2024, 2, 3, 4, 5, 6, 7).AddTicks(8);
        var timeSpan = new TimeSpan(-1, -2, -3, -4, -5);

        await Assert.That(dateTime.GetComponentInt64(DateTimeComponent.Ticks)).IsEqualTo(dateTime.Ticks);
        await Assert.That(timeSpan.Sign()).IsEqualTo(-1);
        await Assert.That(timeSpan.Absolute()).IsEqualTo(TimeSpan.FromTicks(Math.Abs(timeSpan.Ticks)));
        await Assert.That(timeSpan.GetComponentInt64(TimeSpanComponent.Ticks)).IsEqualTo(timeSpan.Ticks);
    }

    [Test]
    public async Task DayOfWeekOffsetHelpersTest()
    {
        await Assert.That(DayOfWeek.Sunday.ShiftRegardingStartingWeekDay(DayOfWeek.Monday)).IsEqualTo(DayOfWeek.Monday);
        await Assert.That(DayOfWeek.Wednesday.DaysSinceWeekStart(DayOfWeek.Monday)).IsEqualTo(2);
        await Assert.That(DayOfWeek.Sunday.DaysSinceWeekStart(DayOfWeek.Monday)).IsEqualTo(6);
    }
}
