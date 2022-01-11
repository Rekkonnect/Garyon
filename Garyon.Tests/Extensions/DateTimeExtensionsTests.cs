﻿using Garyon.Extensions;
using NUnit.Framework;
using System;

namespace Garyon.Tests.Extensions
{
    [Parallelizable(ParallelScope.Children)]
    public class DateTimeExtensionsTests
    {
        private static readonly DateTime sample = new DateTime(2020, 05, 29, 03, 20, 11, 190).AddTicks(4321);
        private static readonly DateTime previousRoundedYearSample = new(2020, 01, 01);
        private static readonly DateTime nextRoundedYearSample = new(2021, 01, 01);

        #region Individual Components
        [Test]
        public void WithMillisecondTest()
        {
            AssertEqualComponents(DateTimeExtensions.WithMillisecond, DateTimeComponent.Millisecond, 420);
        }
        [Test]
        public void WithSecondTest()
        {
            AssertEqualComponents(DateTimeExtensions.WithSecond, DateTimeComponent.Second, 35);
        }
        [Test]
        public void WithMinuteTest()
        {
            AssertEqualComponents(DateTimeExtensions.WithMinute, DateTimeComponent.Minute, 46);
        }
        [Test]
        public void WithHourTest()
        {
            AssertEqualComponents(DateTimeExtensions.WithHour, DateTimeComponent.Hour, 11);
        }
        [Test]
        public void WithDayTest()
        {
            AssertEqualComponents(DateTimeExtensions.WithDay, DateTimeComponent.Day, 12);
        }
        [Test]
        public void WithMonthTest()
        {
            AssertEqualComponents(DateTimeExtensions.WithMonth, DateTimeComponent.Month, 09);
        }
        [Test]
        public void WithYearTest()
        {
            AssertEqualComponents(DateTimeExtensions.WithYear, DateTimeComponent.Year, 2022);
        }

        private void AssertEqualComponents(Adjuster adjuster, DateTimeComponent adjustedComponent, int adjustedValue)
        {
            var adjusted = adjuster(sample, adjustedValue);

            Assert.AreEqual(adjustedComponent is DateTimeComponent.Millisecond ? adjustedValue : sample.Millisecond, adjusted.Millisecond);
            Assert.AreEqual(adjustedComponent is DateTimeComponent.Second ? adjustedValue : sample.Second, adjusted.Second);
            Assert.AreEqual(adjustedComponent is DateTimeComponent.Minute ? adjustedValue : sample.Minute, adjusted.Minute);
            Assert.AreEqual(adjustedComponent is DateTimeComponent.Hour ? adjustedValue : sample.Hour, adjusted.Hour);
            Assert.AreEqual(adjustedComponent is DateTimeComponent.Day ? adjustedValue : sample.Day, adjusted.Day);
            Assert.AreEqual(adjustedComponent is DateTimeComponent.Month ? adjustedValue : sample.Month, adjusted.Month);
            Assert.AreEqual(adjustedComponent is DateTimeComponent.Year ? adjustedValue : sample.Year, adjusted.Year);
        }

        private enum DateTimeComponent
        {
            Millisecond,
            Second,
            Minute,
            Hour,
            Day,
            Month,
            Year,
        }

        private delegate DateTime Adjuster(DateTime dateTime, int adjustedComponentValue);

        // Combination component adjusters can be broken down into multiple
        [Test]
        public void WithHourMinuteTest()
        {
            var expected = sample.WithHour(10).WithMinute(30);
            var actual = sample.WithHourMinute(10, 30);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void WithMinuteSecondTest()
        {
            var expected = sample.WithMinute(10).WithSecond(30);
            var actual = sample.WithMinuteSecond(10, 30);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void WithSecondMillisecondTest()
        {
            var expected = sample.WithSecond(59).WithMillisecond(489);
            var actual = sample.WithSecondMillisecond(59, 489);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void WithHourMinuteSecondTest()
        {
            var expected = sample.WithHour(13).WithMinute(59).WithSecond(58);
            var actual = sample.WithHourMinuteSecond(13, 59, 58);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void WithHourMinuteSecondMillisecondTest()
        {
            var expected = sample.WithHour(13).WithMinute(59).WithSecond(58).WithMillisecond(987);
            var actual = sample.WithHourMinuteSecondMillisecond(13, 59, 58, 987);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void WithMonthDayTest()
        {
            var expected = sample.WithMonth(12).WithDay(21);
            var actual = sample.WithMonthDay(12, 21);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void WithYearMonthTest()
        {
            var expected = sample.WithYear(2022).WithMonth(5);
            var actual = sample.WithYearMonth(2022, 5);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void WithYearMonthDayTest()
        {
            var expected = sample.WithYear(2022).WithMonth(5).WithDay(31);
            var actual = sample.WithYearMonthDay(2022, 5, 31);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WithTimeTest()
        {
            var time = new TimeSpan(0, 12, 23, 42, 085);
            // Rounding must take place, since time span also includes individual ticks beyond the millisecond component, resetting them
            var expected = sample.WithHour(time.Hours).WithMinute(time.Minutes).WithSecond(time.Seconds).WithMillisecond(time.Milliseconds).RoundToPreviousMillisecond();
            var actual = sample.WithTime(time);
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Rounding
        [Test]
        public void RoundToNextMillisecondTest()
        {
            var afterRounding = new DateTime(2020, 05, 29, 03, 20, 11, 191);
            AssertNextRounding(afterRounding, DateTimeExtensions.RoundToNextMillisecond);
        }
        [Test]
        public void RoundToNextSecondTest()
        {
            var afterRounding = new DateTime(2020, 05, 29, 03, 20, 12, 000);
            AssertNextRounding(afterRounding, DateTimeExtensions.RoundToNextSecond);
        }
        [Test]
        public void RoundToNextMinuteTest()
        {
            var afterRounding = new DateTime(2020, 05, 29, 03, 21, 00, 000);
            AssertNextRounding(afterRounding, DateTimeExtensions.RoundToNextMinute);
        }
        [Test]
        public void RoundToNextHourTest()
        {
            var afterRounding = new DateTime(2020, 05, 29, 04, 00, 00, 000);
            AssertNextRounding(afterRounding, DateTimeExtensions.RoundToNextHour);
        }
        [Test]
        public void RoundToNextDayTest()
        {
            var afterRounding = new DateTime(2020, 05, 30, 00, 00, 00, 000);
            AssertNextRounding(afterRounding, DateTimeExtensions.RoundToNextDay);
        }
        [Test]
        public void RoundToNextMonthTest()
        {
            var afterRounding = new DateTime(2020, 06, 01, 00, 00, 00, 000);
            AssertNextRounding(afterRounding, DateTimeExtensions.RoundToNextMonth);
        }
        [Test]
        public void RoundToNextYearTest()
        {
            AssertNextRounding(nextRoundedYearSample, DateTimeExtensions.RoundToNextYear);
        }

        [Test]
        public void RoundToPreviousMillisecondTest()
        {
            var afterRounding = new DateTime(2020, 05, 29, 03, 20, 11, 190);
            AssertPreviousRounding(afterRounding, DateTimeExtensions.RoundToPreviousMillisecond);
        }
        [Test]
        public void RoundToPreviousSecondTest()
        {
            var afterRounding = new DateTime(2020, 05, 29, 03, 20, 11, 000);
            AssertPreviousRounding(afterRounding, DateTimeExtensions.RoundToPreviousSecond);
        }
        [Test]
        public void RoundToPreviousMinuteTest()
        {
            var afterRounding = new DateTime(2020, 05, 29, 03, 20, 00, 000);
            AssertPreviousRounding(afterRounding, DateTimeExtensions.RoundToPreviousMinute);
        }
        [Test]
        public void RoundToPreviousHourTest()
        {
            var afterRounding = new DateTime(2020, 05, 29, 03, 00, 00, 000);
            AssertPreviousRounding(afterRounding, DateTimeExtensions.RoundToPreviousHour);
        }
        [Test]
        public void RoundToPreviousDayTest()
        {
            var afterRounding = new DateTime(2020, 05, 29, 00, 00, 00, 000);
            AssertPreviousRounding(afterRounding, DateTimeExtensions.RoundToPreviousDay);
        }
        [Test]
        public void RoundToPreviousMonthTest()
        {
            var afterRounding = new DateTime(2020, 05, 01, 00, 00, 00, 000);
            AssertPreviousRounding(afterRounding, DateTimeExtensions.RoundToPreviousMonth);
        }
        [Test]
        public void RoundToPreviousYearTest()
        {
            AssertPreviousRounding(previousRoundedYearSample, DateTimeExtensions.RoundToPreviousYear);
        }

        private void AssertNextRounding(DateTime expectedRounded, DateTimeRounder rounder)
        {
            AssertRounding(expectedRounded, rounder);

            Assert.AreEqual(nextRoundedYearSample, rounder(nextRoundedYearSample, true));
        }
        private void AssertPreviousRounding(DateTime expectedRounded, DateTimeRounder rounder)
        {
            AssertRounding(expectedRounded, rounder);

            Assert.AreEqual(previousRoundedYearSample, rounder(previousRoundedYearSample, true));
        }
        private void AssertRounding(DateTime expectedRounded, DateTimeRounder rounder)
        {
            var result = rounder(sample, false);
            Assert.AreEqual(expectedRounded, result);

            var furtherRounded = rounder(result, false);
            var retainedRounded = rounder(result, true);
            Assert.AreNotEqual(furtherRounded, result);
            Assert.AreEqual(retainedRounded, result);
        }

        private delegate DateTime DateTimeRounder(DateTime dateTime, bool retainIfRounded);
        #endregion
    }
}
