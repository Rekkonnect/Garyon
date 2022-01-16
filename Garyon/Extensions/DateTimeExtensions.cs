using Garyon.Exceptions;
using Garyon.Objects;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using static System.TimeSpan;

namespace Garyon.Extensions
{
    /// <summary>Represents a component in the <seealso cref="DateTime"/> struct.</summary>
    public enum DateTimeComponent
    {
        /// <summary>Represents the <seealso cref="DateTime.Ticks"/> component.</summary>
        Ticks = 0,

        /// <summary>Represents the <seealso cref="DateTime.Millisecond"/> component.</summary>
        Millisecond,
        /// <summary>Represents the <seealso cref="DateTime.Second"/> component.</summary>
        Second,
        /// <summary>Represents the <seealso cref="DateTime.Minute"/> component.</summary>
        Minute,
        /// <summary>Represents the <seealso cref="DateTime.Hour"/> component.</summary>
        Hour,
        /// <summary>Represents the <seealso cref="DateTime.Day"/> component.</summary>
        Day,
        /// <summary>Represents the <seealso cref="DateTime.Month"/> component.</summary>
        Month,
        /// <summary>Represents the <seealso cref="DateTime.Year"/> component.</summary>
        Year,
    }

    /// <summary>Prvoides extension functions for the <seealso cref="DateTime"/> struct.</summary>
    public static class DateTimeExtensions
    {
        /// <summary>Gets the specified component of the given <seealso cref="DateTime"/> instance.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> instance whose component to get.</param>
        /// <param name="component">The component of the <seealso cref="DateTime"/> instance to get.</param>
        /// <returns>The requested component of the <seealso cref="DateTime"/> instance, represented by the specified <seealso cref="DateTimeComponent"/>.</returns>
        public static long GetComponentInt64(this DateTime dateTime, DateTimeComponent component) => component switch
        {
            DateTimeComponent.Ticks => dateTime.Ticks,
            _ => GetComponent(dateTime, component),
        };
        /// <summary>Gets the specified component of the given <seealso cref="DateTime"/> instance.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> instance whose component to get.</param>
        /// <param name="component">The component of the <seealso cref="DateTime"/> instance to get. To get the <seealso cref="DateTimeComponent.Ticks"/> component, use <seealso cref="GetComponentInt64(DateTime, DateTimeComponent)"/>.</param>
        /// <returns>The requested component of the <seealso cref="DateTime"/> instance, represented by the specified <seealso cref="DateTimeComponent"/>.</returns>
        public static int GetComponent(this DateTime dateTime, DateTimeComponent component) => component switch
        {
            DateTimeComponent.Millisecond => dateTime.Millisecond,
            DateTimeComponent.Second => dateTime.Second,
            DateTimeComponent.Minute => dateTime.Minute,
            DateTimeComponent.Hour => dateTime.Hour,
            DateTimeComponent.Day => dateTime.Day,
            DateTimeComponent.Month => dateTime.Month,
            DateTimeComponent.Year => dateTime.Year,
        };

        #region Shortcuts
        /// <summary>Gets the days the month of the provided <seealso cref="DateTime"/> has.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> whose month's day count to get.</param>
        /// <returns>The number of days the month of <paramref name="dateTime"/> has.</returns>
        [ExcludeFromCodeCoverage(Justification = "Trivial")]
        public static int DaysInMonth(this DateTime dateTime) => DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        /// <summary>Determines whether the year of the provided <seealso cref="DateTime"/> is a leap year.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> whose year to determine if it is a leap one.</param>
        /// <returns>Whether the year of <paramref name="dateTime"/> is a leap year.</returns>
        [ExcludeFromCodeCoverage(Justification = "Trivial")]
        public static bool IsInLeapYear(this DateTime dateTime) => DateTime.IsLeapYear(dateTime.Year);
        /// <summary>Gets the days the year of the provided <seealso cref="DateTime"/> has.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> whose year's day count to get.</param>
        /// <returns>The number of days the year of <paramref name="dateTime"/> has.</returns>
        [ExcludeFromCodeCoverage(Justification = "Trivial")]
        public static int DaysInYear(this DateTime dateTime) => dateTime.IsInLeapYear() ? 366 : 365;

        /// <summary>Gets the days the specified year contains.</summary>
        /// <param name="year">The year whose days to determine.</param>
        /// <returns>The number of days the given year has.</returns>
        [ExcludeFromCodeCoverage(Justification = "Trivial")]
        public static int DaysInYear(int year) => DaysInYear(DateTime.IsLeapYear(year));
        /// <summary>Gets the days a year contains, determined by whether it's a leap year or not.</summary>
        /// <param name="isLeapYear">Determines whether the year is a leap year or not.</param>
        /// <returns>The number of days the year has.</returns>
        [ExcludeFromCodeCoverage(Justification = "Trivial")]
        public static int DaysInYear(bool isLeapYear) => isLeapYear ? 366 : 365;
        #endregion

        #region Individual Adjustments
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the millisecond of the copied value is set to a specified value.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted millisecond component.</param>
        /// <param name="millisecond">The value of the millisecond component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 999].</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the millisecond set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="millisecond"/> is outside the range [0, 999].</exception>
        public static DateTime WithMillisecond(this DateTime dateTime, int millisecond)
        {
            ValidateMillisecondComponent(millisecond);
            return dateTime.AddMilliseconds(millisecond - dateTime.Millisecond);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the second of the copied value is set to a specified value.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted second component.</param>
        /// <param name="second">The value of the second component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 59].</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the second set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="second"/> is outside the range [0, 59].</exception>
        public static DateTime WithSecond(this DateTime dateTime, int second)
        {
            ValidateSecondComponent(second);
            return dateTime.AddSeconds(second - dateTime.Second);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the minute of the copied value is set to a specified value.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted minute component.</param>
        /// <param name="minute">The value of the minute component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 59].</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the minute set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="minute"/> is outside the range [0, 59].</exception>
        public static DateTime WithMinute(this DateTime dateTime, int minute)
        {
            ValidateMinuteComponent(minute);
            return dateTime.AddMinutes(minute - dateTime.Minute);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the hour of the copied value is set to a specified value.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted hour component.</param>
        /// <param name="hour">The value of the hour component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 23].</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the hour set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="hour"/> is outside the range [0, 23].</exception>
        public static DateTime WithHour(this DateTime dateTime, int hour)
        {
            ValidateHourComponent(hour);
            return dateTime.AddHours(hour - dateTime.Hour);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the day of the copied value is set to a specified value.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted day component.</param>
        /// <param name="day">The value of the day component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [1, 31] and available for the given year/month combination.</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the day set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="day"/> is outside the range [1, 31] -or- the year/month combination originating from the given <seealso cref="DateTime"/> does not have the specified day.</exception>
        public static DateTime WithDay(this DateTime dateTime, int day)
        {
            ValidateDay(dateTime.Year, dateTime.Month, day);
            return dateTime.AddDays(day - dateTime.Day);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the month of the copied value is set to a specified value.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted month component.</param>
        /// <param name="month">The value of the month component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [1, 12] and the given day should be available for the resulting year/month combination.</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the month set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="month"/> is outside the range [1, 12] -or- the resulting year/month combination does not have the day from the original <seealso cref="DateTime"/>.</exception>
        public static DateTime WithMonth(this DateTime dateTime, int month)
        {
            ValidateMonth(dateTime.Year, month, dateTime.Day);
            return dateTime.AddMonths(month - dateTime.Month);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the year of the copied value is set to a specified value.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted year component.</param>
        /// <param name="year">The value of the year component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [1, 9999] and the given day should be available for the resulting year/month combination.</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the year set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="year"/> is outside the range [1, 9999] -or- the resulting year/month combination does not have the day from the original <seealso cref="DateTime"/>.</exception>
        public static DateTime WithYear(this DateTime dateTime, int year)
        {
            ValidateYear(year, dateTime.Month, dateTime.Day);
            return dateTime.AddYears(year - dateTime.Year);
        }

        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the second and the millisecond components of the copied value are set to specified values.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted second and millisecond components.</param>
        /// <param name="second">The value of the second component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 59].</param>
        /// <param name="millisecond">The value of the second component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 999].</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the second and millisecond components set to the specified values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="second"/> is outside the range [0, 59] -or- the given value for <paramref name="millisecond"/> is outside the range [0, 999].</exception>
        public static DateTime WithSecondMillisecond(this DateTime dateTime, int second, int millisecond)
        {
            ValidateSecondComponent(second);
            ValidateMillisecondComponent(millisecond);
            int targetMilliseconds = second * 1000 + millisecond;
            int currentMilliseconds = dateTime.Second * 1000 + dateTime.Millisecond;
            return dateTime.AddMilliseconds(targetMilliseconds - currentMilliseconds);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the minute and the second components of the copied value are set to specified values.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted minute and second components.</param>
        /// <param name="minute">The value of the minute component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 59].</param>
        /// <param name="second">The value of the second component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 59].</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the minute and second components set to the specified values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="minute"/> is outside the range [0, 59] -or- the given value for <paramref name="second"/> is outside the range [0, 59].</exception>
        public static DateTime WithMinuteSecond(this DateTime dateTime, int minute, int second)
        {
            ValidateMinuteComponent(minute);
            ValidateSecondComponent(second);
            int targetSeconds = minute * 60 + second;
            int currentSeconds = dateTime.Minute * 60 + dateTime.Second;
            return dateTime.AddSeconds(targetSeconds - currentSeconds);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the hour and minute components of the copied value are set to specified values.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted hour and minute components.</param>
        /// <param name="hour">The value of the hour component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 23].</param>
        /// <param name="minute">The value of the minute component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 59].</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the hour and minute components set to the specified values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="hour"/> is outside the range [0, 23] -or- the given value for <paramref name="minute"/> is outside the range [0, 59].</exception>
        public static DateTime WithHourMinute(this DateTime dateTime, int hour, int minute)
        {
            ValidateHourComponent(hour);
            ValidateMinuteComponent(minute);
            int targetMinutes = hour * 60 + minute;
            int currentMinutes = dateTime.Hour * 60 + dateTime.Minute;
            return dateTime.AddMinutes(targetMinutes - currentMinutes);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the hour, minute and second components of the copied value are set to specified values.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted hour, minute and second components.</param>
        /// <param name="hour">The value of the hour component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 23].</param>
        /// <param name="minute">The value of the minute component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 59].</param>
        /// <param name="second">The value of the second component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 59].</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the hour, minute and second components set to the specified values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The given value for <paramref name="hour"/> is outside the range [0, 23]
        /// -or- the given value for <paramref name="minute"/> is outside the range [0, 59]
        /// -or- the given value for <paramref name="second"/> is outside the range [0, 59].
        /// </exception>
        public static DateTime WithHourMinuteSecond(this DateTime dateTime, int hour, int minute, int second)
        {
            ValidateHourComponent(hour);
            ValidateMinuteComponent(minute);
            ValidateSecondComponent(second);
            int targetSeconds = (hour * 60 + minute) * 60 + second;
            int currentSeconds = (dateTime.Hour * 60 + dateTime.Minute) * 60 + dateTime.Second;
            return dateTime.AddSeconds(targetSeconds - currentSeconds);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the hour, minute, second and millisecond components of the copied value are set to specified values.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted hour, minute, second and millisecond components.</param>
        /// <param name="hour">The value of the hour component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 23].</param>
        /// <param name="minute">The value of the minute component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 59].</param>
        /// <param name="second">The value of the second component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 59].</param>
        /// <param name="millisecond">The value of the millisecond component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [0, 999].</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the hour, minute, second and millisecond components set to the specified values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The given value for <paramref name="hour"/> is outside the range [0, 23]
        /// -or- the given value for <paramref name="minute"/> is outside the range [0, 59]
        /// -or- the given value for <paramref name="second"/> is outside the range [0, 59].
        /// -or- the given value for <paramref name="millisecond"/> is outside the range [0, 999].
        /// </exception>
        public static DateTime WithHourMinuteSecondMillisecond(this DateTime dateTime, int hour, int minute, int second, int millisecond)
        {
            ValidateHourComponent(hour);
            ValidateMinuteComponent(minute);
            ValidateSecondComponent(second);
            ValidateMillisecondComponent(millisecond);
            int targetMilliseconds = ((hour * 60 + minute) * 60 + second) * 1000 + millisecond;
            int currentMillseconds = ((dateTime.Hour * 60 + dateTime.Minute) * 60 + dateTime.Second) * 1000 + dateTime.Millisecond;
            return dateTime.AddMilliseconds(targetMilliseconds - currentMillseconds);
        }

        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the minute and the second components of the copied value are set to specified values.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted minute and second components.</param>
        /// <param name="minuteSecond">The minute and second components of the resulting copied <seealso cref="DateTime"/>.</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the minute and second components set to the specified values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="minuteSecond"/> represents a minute or a second outside the range [0, 59].</exception>
        [ExcludeFromCodeCoverage(Justification = "Trivial")]
        public static DateTime WithMinuteSecond(this DateTime dateTime, MinuteSecond minuteSecond)
        {
            return dateTime.WithMinuteSecond(minuteSecond.Minute, minuteSecond.Second);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the hour and minute components of the copied value are set to specified values.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted hour and minute components.</param>
        /// <param name="hourMinute">The hour and minute components of the resulting copied <seealso cref="DateTime"/>.</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the hour and minute components set to the specified values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="hourMinute"/> represents an hour outside the range [0, 23], or a minute outside the range [0, 59].</exception>
        [ExcludeFromCodeCoverage(Justification = "Trivial")]
        public static DateTime WithHourMinute(this DateTime dateTime, HourMinute hourMinute)
        {
            return dateTime.WithHourMinute(hourMinute.Hour, hourMinute.Minute);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the time components of the copied value are set to specified values.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted time components.</param>
        /// <param name="time">The time components of the resulting copied <seealso cref="DateTime"/>, whose <seealso cref="TimeSpan.Days"/> property should be 0.</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the time components set to the specified values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="time"/> represents an absolute time interval lasting at least an entire day.</exception>
        /// <remarks>
        /// Consider normalizing the <seealso cref="TimeSpan"/> using the <seealso cref="TimeSpanExtensions.WithinDay(TimeSpan)"/> extension.<br/>
        /// Since the <seealso cref="TimeSpan"/> also retains ticks below the millisecond interval, its ticks will replace the existing <seealso cref="DateTime"/> instance's.
        /// </remarks>
        public static DateTime WithTime(this DateTime dateTime, TimeSpan time)
        {
            if (time.Days != 0)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The time argument should have the day component set to 0.");

            return dateTime.Date + time;
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the specified component of the copied value is set to the specified value.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted time components.</param>
        /// <param name="value">The value to set the component to.</param>
        /// <param name="component">The component of the <seealso cref="DateTime"/> instance that will be changed. <seealso cref="DateTimeComponent.Ticks"/> is not accepted in this overload, consider using <seealso cref="WithComponent(DateTime, long, DateTimeComponent)"/>.</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the specified component set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="value"/> falls out of the valid range for the given component, or an invalid day is represented.</exception>
        [ExcludeFromCodeCoverage(Justification = "Trivial")]
        public static DateTime WithComponent(this DateTime dateTime, int value, DateTimeComponent component)
        {
            return component switch
            {
                DateTimeComponent.Millisecond => dateTime.WithMillisecond(value),
                DateTimeComponent.Second => dateTime.WithSecond(value),
                DateTimeComponent.Minute => dateTime.WithMinute(value),
                DateTimeComponent.Hour => dateTime.WithHour(value),
                DateTimeComponent.Day => dateTime.WithDay(value),
                DateTimeComponent.Month => dateTime.WithMonth(value),
                DateTimeComponent.Year => dateTime.WithYear(value),
            };
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the specified component of the copied value is set to the specified value.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted time components.</param>
        /// <param name="value">The value to set the component to.</param>
        /// <param name="component">The component of the <seealso cref="DateTime"/> instance that will be changed. <seealso cref="DateTimeComponent.Ticks"/> is also accepted.</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the specified component set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="value"/> falls out of the valid range for the given component, or an invalid day is represented.</exception>
        [ExcludeFromCodeCoverage(Justification = "Trivial")]
        public static DateTime WithComponent(this DateTime dateTime, long value, DateTimeComponent component)
        {
            return component switch
            {
                DateTimeComponent.Ticks => new(value),

                _ => dateTime.WithComponent((int)value, component),
            };
        }

        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the year and month components of the copied value are set to specified values.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted year and month components.</param>
        /// <param name="year">The value of the year component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [1, 9999].</param>
        /// <param name="month">The value of the month component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [1, 12].</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the year and month components set to the specified values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The given year, month, day combination is not a valid one
        /// -or- the given value for <paramref name="year"/> is outside the range [1, 9999]
        /// -or- the given value for <paramref name="month"/> is outside the range [1, 12].
        /// </exception>
        public static DateTime WithYearMonth(this DateTime dateTime, int year, int month)
        {
            return dateTime.WithYearMonthDay(year, month, dateTime.Day);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the month and day components of the copied value are set to specified values.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted month and day components.</param>
        /// <param name="month">The value of the month component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [1, 12].</param>
        /// <param name="day">The value of the day component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [1, 31].</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the month and day components set to the specified values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The given year, month, day combination is not a valid one
        /// -or- the given value for <paramref name="month"/> is outside the range [1, 12]
        /// -or- the given value for <paramref name="day"/> is outside the range [1, 31].
        /// </exception>
        public static DateTime WithMonthDay(this DateTime dateTime, int month, int day)
        {
            return dateTime.WithYearMonthDay(dateTime.Year, month, day);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the year, month and day components of the copied value are set to specified values.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted year, month and day components.</param>
        /// <param name="year">The value of the year component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [1, 9999].</param>
        /// <param name="month">The value of the month component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [1, 12].</param>
        /// <param name="day">The value of the day component of the resulting copied <seealso cref="DateTime"/>. It must be within the range [1, 31].</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the year, month and day components set to the specified values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The given year, month, day combination is not a valid one
        /// -or- the given value for <paramref name="year"/> is outside the range [1, 9999]
        /// -or- the given value for <paramref name="month"/> is outside the range [1, 12]
        /// -or- the given value for <paramref name="day"/> is outside the range [1, 31].
        /// </exception>
        public static DateTime WithYearMonthDay(this DateTime dateTime, int year, int month, int day)
        {
            ValidateDateAndComponents(year, month, day);
            return new DateTime(year, month, day) + dateTime.TimeOfDay;
        }

        /// <summary>Creates a <seealso cref="DateTime"/> instance only containing the time components of the original instance.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> instance whose time components to retrieve.</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the year, month and day components set to their default values (01/01/0001), and the time components remaining as is.</returns>
        /// <remarks>There is not much usage considered for this method, and the ideal design choice would involve using the <seealso cref="DateTime.TimeOfDay"/> property.</remarks>
        public static DateTime TimeOnly(this DateTime dateTime)
        {
            return new(dateTime.TimeOfDay.Ticks);
        }

        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the date is adjusted by the specified number of weeks.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted date.</param>
        /// <param name="weeks">The number of weeks to advance the <see cref="DateTime"/> by.</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the date adjusted by the given weeks.</returns>
        public static DateTime AddWeeks(this DateTime dateTime, int weeks)
        {
            return dateTime.AddDays(weeks * 7);
        }

        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the day is adjusted to the one specified in the same week.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted day.</param>
        /// <param name="dayOfWeek">The <seealso cref="DayOfWeek"/> reflecting the day in the current week the new day will be.</param>
        /// <param name="firstDayOfWeek">The first day of the week, within which the new day will be.</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the day of week set to the specified value.</returns>
        public static DateTime WithDayOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek, DayOfWeek firstDayOfWeek = DayOfWeek.Sunday)
        {
            var existingDayOfWeek = dateTime.DayOfWeek.ShiftRegardingStartingWeekDay(firstDayOfWeek);
            var nextDayOfWeek = dayOfWeek.ShiftRegardingStartingWeekDay(firstDayOfWeek);
            int days = nextDayOfWeek - existingDayOfWeek;
            return dateTime.AddDays(days);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the day is adjusted to the one specified in the same year.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted day.</param>
        /// <param name="day">The day in the same year that will be set. It must range in [1, 365] for normal years, or [1, 366] for leap years.</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the day of year set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The given value for <paramref name="day"/> is outside the range [1, 365], if the year is a normal one, or [1, 366] if the year is a leap one.
        /// </exception>
        public static DateTime WithDayOfYear(this DateTime dateTime, int day)
        {
            ValidateDayOfYear(dateTime.Year, day);

            int days = day - dateTime.DayOfYear;
            return dateTime.AddDays(days);
        }

        #region Validation
        // All validation functions will throw an exception with the appropriate message if the component is outside the valid range
        // Validate individual components
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateMillisecondComponent(int millisecond)
        {
            if (millisecond is < 0 or > 999)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The millisecond must range in [0, 999].");
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateSecondComponent(int second)
        {
            if (second is < 0 or > 59)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The second must range in [0, 59].");
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateMinuteComponent(int minute)
        {
            if (minute is < 0 or > 59)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The minute must range in [0, 59].");
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateHourComponent(int hour)
        {
            if (hour is < 0 or > 23)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The hour must range in [0, 23].");
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateDayComponent(int day)
        {
            if (day is < 1 or > 31)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The day must range in [1, 31].");
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateMonthComponent(int month)
        {
            if (month is < 1 or > 12)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The month must range in [1, 12].");
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateYearComponent(int year)
        {
            if (year is < 1 or > 9999)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The year must range in [1, 9999].");
        }

        // Validate the components and the date itself
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateDateAndComponents(int year, int month, int day)
        {
            ValidateYearComponent(year);
            ValidateMonthComponent(month);
            ValidateDayComponent(day);
            ValidateDate(year, month, day);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateDay(int year, int month, int day)
        {
            ValidateDayComponent(day);
            ValidateDate(year, month, day);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateMonth(int year, int month, int day)
        {
            ValidateMonthComponent(month);
            ValidateDate(year, month, day);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateYear(int year, int month, int day)
        {
            ValidateYearComponent(year);
            ValidateDate(year, month, day);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateDate(int year, int month, int day)
        {
            if (day > DateTime.DaysInMonth(year, month))
                ThrowHelper.Throw<ArgumentOutOfRangeException>($"Attempted to adjust the date to {year:D4}/{month:D2}/{day:D2} (YYYY/MM/DD), but the year/month combination contains less days than the given.");
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateDayOfYear(int year, int day)
        {
            if (day < 1)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The day must be greater than 0.");

            if (day > DaysInYear(year))
                ThrowHelper.Throw<ArgumentOutOfRangeException>($"Attempted to adjust the day of year to {day:D3}, year contains less days than the given.");
        }
        #endregion
        #endregion

        #region Rounding
        /// <summary>Rounds the given <seealso cref="DateTime"/> to the next millisecond, and possibly retains it if rounded.</summary>
        /// <param name="dateTime">The date time to round to the next millisecond.</param>
        /// <param name="retainIfRounded">Determines whether the result will be the same as the provided <seealso cref="DateTime"/> in the case that it already is rounded to a millisecond.</param>
        /// <returns>The resulting <seealso cref="DateTime"/> rounded to the next millisecond, as specified by <paramref name="retainIfRounded"/>.</returns>
        public static DateTime RoundToNextMillisecond(this DateTime dateTime, bool retainIfRounded = false)
        {
            return RoundToNextUnit(dateTime, TicksPerMillisecond, retainIfRounded);
        }
        /// <summary>Rounds the given <seealso cref="DateTime"/> to the next second, and possibly retains it if rounded.</summary>
        /// <param name="dateTime">The date time to round to the next second.</param>
        /// <param name="retainIfRounded">Determines whether the result will be the same as the provided <seealso cref="DateTime"/> in the case that it already is rounded to a second.</param>
        /// <returns>The resulting <seealso cref="DateTime"/> rounded to the next second, as specified by <paramref name="retainIfRounded"/>.</returns>
        public static DateTime RoundToNextSecond(this DateTime dateTime, bool retainIfRounded = false)
        {
            return RoundToNextUnit(dateTime, TicksPerSecond, retainIfRounded);
        }
        /// <summary>Rounds the given <seealso cref="DateTime"/> to the next minute, and possibly retains it if rounded.</summary>
        /// <param name="dateTime">The date time to round to the next minute.</param>
        /// <param name="retainIfRounded">Determines whether the result will be the same as the provided <seealso cref="DateTime"/> in the case that it already is rounded to a minute.</param>
        /// <returns>The resulting <seealso cref="DateTime"/> rounded to the next minute, as specified by <paramref name="retainIfRounded"/>.</returns>
        public static DateTime RoundToNextMinute(this DateTime dateTime, bool retainIfRounded = false)
        {
            return RoundToNextUnit(dateTime, TicksPerMinute, retainIfRounded);
        }
        /// <summary>Rounds the given <seealso cref="DateTime"/> to the next hour, and possibly retains it if rounded.</summary>
        /// <param name="dateTime">The date time to round to the next hour.</param>
        /// <param name="retainIfRounded">Determines whether the result will be the same as the provided <seealso cref="DateTime"/> in the case that it already is rounded to a hour.</param>
        /// <returns>The resulting <seealso cref="DateTime"/> rounded to the next hour, as specified by <paramref name="retainIfRounded"/>.</returns>
        public static DateTime RoundToNextHour(this DateTime dateTime, bool retainIfRounded = false)
        {
            return RoundToNextUnit(dateTime, TicksPerHour, retainIfRounded);
        }
        /// <summary>Rounds the given <seealso cref="DateTime"/> to the next day, and possibly retains it if rounded.</summary>
        /// <param name="dateTime">The date time to round to the next day.</param>
        /// <param name="retainIfRounded">Determines whether the result will be the same as the provided <seealso cref="DateTime"/> in the case that it already is rounded to a day.</param>
        /// <returns>The resulting <seealso cref="DateTime"/> rounded to the next day, as specified by <paramref name="retainIfRounded"/>.</returns>
        public static DateTime RoundToNextDay(this DateTime dateTime, bool retainIfRounded = false)
        {
            return RoundToNextUnit(dateTime, TicksPerDay, retainIfRounded);
        }
        /// <summary>Rounds the given <seealso cref="DateTime"/> to the next month, and possibly retains it if rounded.</summary>
        /// <param name="dateTime">The date time to round to the next month.</param>
        /// <param name="retainIfRounded">Determines whether the result will be the same as the provided <seealso cref="DateTime"/> in the case that it already is rounded to a month.</param>
        /// <returns>The resulting <seealso cref="DateTime"/> rounded to the next month, as specified by <paramref name="retainIfRounded"/>.</returns>
        public static DateTime RoundToNextMonth(this DateTime dateTime, bool retainIfRounded = false)
        {
            return RoundToMonth(dateTime, 1, retainIfRounded, true);
        }
        /// <summary>Rounds the given <seealso cref="DateTime"/> to the next year, and possibly retains it if rounded.</summary>
        /// <param name="dateTime">The date time to round to the next year.</param>
        /// <param name="retainIfRounded">Determines whether the result will be the same as the provided <seealso cref="DateTime"/> in the case that it already is rounded to a year.</param>
        /// <returns>The resulting <seealso cref="DateTime"/> rounded to the next year, as specified by <paramref name="retainIfRounded"/>.</returns>
        public static DateTime RoundToNextYear(this DateTime dateTime, bool retainIfRounded = false)
        {
            return RoundToYear(dateTime, 1, retainIfRounded, true);
        }

        /// <summary>Rounds the given <seealso cref="DateTime"/> to the previous millisecond, and possibly retains it if rounded.</summary>
        /// <param name="dateTime">The date time to round to the previous millisecond.</param>
        /// <param name="retainIfRounded">Determines whether the result will be the same as the provided <seealso cref="DateTime"/> in the case that it already is rounded to a millisecond.</param>
        /// <returns>The resulting <seealso cref="DateTime"/> rounded to the previous millisecond, as specified by <paramref name="retainIfRounded"/>.</returns>
        public static DateTime RoundToPreviousMillisecond(this DateTime dateTime, bool retainIfRounded = false)
        {
            return RoundToPreviousUnit(dateTime, TicksPerMillisecond, retainIfRounded);
        }
        /// <summary>Rounds the given <seealso cref="DateTime"/> to the previous second, and possibly retains it if rounded.</summary>
        /// <param name="dateTime">The date time to round to the previous second.</param>
        /// <param name="retainIfRounded">Determines whether the result will be the same as the provided <seealso cref="DateTime"/> in the case that it already is rounded to a second.</param>
        /// <returns>The resulting <seealso cref="DateTime"/> rounded to the previous second, as specified by <paramref name="retainIfRounded"/>.</returns>
        public static DateTime RoundToPreviousSecond(this DateTime dateTime, bool retainIfRounded = false)
        {
            return RoundToPreviousUnit(dateTime, TicksPerSecond, retainIfRounded);
        }
        /// <summary>Rounds the given <seealso cref="DateTime"/> to the previous minute, and possibly retains it if rounded.</summary>
        /// <param name="dateTime">The date time to round to the previous minute.</param>
        /// <param name="retainIfRounded">Determines whether the result will be the same as the provided <seealso cref="DateTime"/> in the case that it already is rounded to a minute.</param>
        /// <returns>The resulting <seealso cref="DateTime"/> rounded to the previous minute, as specified by <paramref name="retainIfRounded"/>.</returns>
        public static DateTime RoundToPreviousMinute(this DateTime dateTime, bool retainIfRounded = false)
        {
            return RoundToPreviousUnit(dateTime, TicksPerMinute, retainIfRounded);
        }
        /// <summary>Rounds the given <seealso cref="DateTime"/> to the previous hour, and possibly retains it if rounded.</summary>
        /// <param name="dateTime">The date time to round to the previous hour.</param>
        /// <param name="retainIfRounded">Determines whether the result will be the same as the provided <seealso cref="DateTime"/> in the case that it already is rounded to a hour.</param>
        /// <returns>The resulting <seealso cref="DateTime"/> rounded to the previous hour, as specified by <paramref name="retainIfRounded"/>.</returns>
        public static DateTime RoundToPreviousHour(this DateTime dateTime, bool retainIfRounded = false)
        {
            return RoundToPreviousUnit(dateTime, TicksPerHour, retainIfRounded);
        }
        /// <summary>Rounds the given <seealso cref="DateTime"/> to the previous day, and possibly retains it if rounded.</summary>
        /// <param name="dateTime">The date time to round to the previous day.</param>
        /// <param name="retainIfRounded">Determines whether the result will be the same as the provided <seealso cref="DateTime"/> in the case that it already is rounded to a day.</param>
        /// <returns>The resulting <seealso cref="DateTime"/> rounded to the previous day, as specified by <paramref name="retainIfRounded"/>.</returns>
        public static DateTime RoundToPreviousDay(this DateTime dateTime, bool retainIfRounded = false)
        {
            return RoundToPreviousUnit(dateTime, TicksPerDay, retainIfRounded);
        }
        /// <summary>Rounds the given <seealso cref="DateTime"/> to the previous month, and possibly retains it if rounded.</summary>
        /// <param name="dateTime">The date time to round to the previous month.</param>
        /// <param name="retainIfRounded">Determines whether the result will be the same as the provided <seealso cref="DateTime"/> in the case that it already is rounded to a month.</param>
        /// <returns>The resulting <seealso cref="DateTime"/> rounded to the previous month, as specified by <paramref name="retainIfRounded"/>.</returns>
        public static DateTime RoundToPreviousMonth(this DateTime dateTime, bool retainIfRounded = false)
        {
            return RoundToMonth(dateTime, 0, retainIfRounded, false);
        }
        /// <summary>Rounds the given <seealso cref="DateTime"/> to the previous year, and possibly retains it if rounded.</summary>
        /// <param name="dateTime">The date time to round to the previous year.</param>
        /// <param name="retainIfRounded">Determines whether the result will be the same as the provided <seealso cref="DateTime"/> in the case that it already is rounded to a year.</param>
        /// <returns>The resulting <seealso cref="DateTime"/> rounded to the previous year, as specified by <paramref name="retainIfRounded"/>.</returns>
        public static DateTime RoundToPreviousYear(this DateTime dateTime, bool retainIfRounded = false)
        {
            return RoundToYear(dateTime, 0, retainIfRounded, false);
        }

        private static DateTime RoundToMonth(this DateTime dateTime, int offset, bool retainIfRounded, bool targetRoundingState)
        {
            var result = new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(offset);
            if (retainIfRounded == targetRoundingState && dateTime.AddTicks(-1).Month != dateTime.Month)
                result = result.AddMonths(-1);

            return result;
        }
        private static DateTime RoundToYear(this DateTime dateTime, int offset, bool retainIfRounded, bool targetRoundingState)
        {
            var result = new DateTime(dateTime.Year, 1, 1).AddYears(offset);
            if (retainIfRounded == targetRoundingState && dateTime.AddTicks(-1).Year != dateTime.Year)
                result = result.AddYears(-1);

            return result;
        }

        private static DateTime RoundToPreviousUnit(this DateTime dateTime, long ticksPerUnit, bool retainIfRounded)
        {
            long reducedTicks = dateTime.Ticks % ticksPerUnit;
            if (!retainIfRounded && reducedTicks is 0)
                reducedTicks = ticksPerUnit;

            return new(dateTime.Ticks - reducedTicks);
        }
        private static DateTime RoundToNextUnit(this DateTime dateTime, long ticksPerUnit, bool retainIfRounded)
        {
            long remainingTicks = ticksPerUnit - dateTime.Ticks % ticksPerUnit;
            if (retainIfRounded && remainingTicks == ticksPerUnit)
                remainingTicks = 0;

            return new(dateTime.Ticks + remainingTicks);
        }
        #endregion
    }
}
