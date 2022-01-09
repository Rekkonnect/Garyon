using Garyon.Exceptions;
using Garyon.Objects;
using System;
using System.Diagnostics.CodeAnalysis;
using static System.TimeSpan;

namespace Garyon.Extensions
{
    /// <summary>Prvoides extension functions for the <seealso cref="DateTime"/> struct.</summary>
    public static class DateTimeExtensions
    {
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

        /*
         * TODO: With* functions:
         * - MM/DD
         * - YYYY/MM
         * - YYYY/MM/DD
         * 
         * Done:
         * - ss.ms
         * - mm:ss
         * - hh:mm
         * - hh:mm:ss
         * - hh:mm:ss.ms
         */

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
            return dateTime.AddSeconds(targetMilliseconds - currentMillseconds);
        }

        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the minute and the second components of the copied value are set to specified values.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted minute and second components.</param>
        /// <param name="minuteSecond">The minute and second components of the resulting copied <seealso cref="DateTime"/>.</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the minute and second components set to the specified values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="minuteSecond"/> represents a minute outside the range [0, 59].</exception>
        public static DateTime WithMinuteSecond(this DateTime dateTime, MinuteSecond minuteSecond)
        {
            return dateTime.WithMinuteSecond(minuteSecond.Minute, minuteSecond.Second);
        }
        /// <summary>Creates a copy of a given <seealso cref="DateTime"/>, where the hour and minute components of the copied value are set to specified values.</summary>
        /// <param name="dateTime">The <seealso cref="DateTime"/> from which to create the copy with the adjusted hour and minute components.</param>
        /// <param name="hourMinute">The hour and minute components of the resulting copied <seealso cref="DateTime"/>.</param>
        /// <returns>A copy of the original <seealso cref="DateTime"/> with the hour and minute components set to the specified values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="hourMinute"/> represents an hour outside the range [0, 23].</exception>
        public static DateTime WithHourMinute(this DateTime dateTime, HourMinute hourMinute)
        {
            return dateTime.WithHourMinute(hourMinute.Hour, hourMinute.Minute);
        }

        #region Validation
        // All validation functions will throw an exception with the appropriate message if the component is outside the valid range
        // Validate individual components
        private static void ValidateMillisecondComponent(int millisecond)
        {
            if (millisecond is < 0 or > 999)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The millisecond must range in [0, 999].");
        }
        private static void ValidateSecondComponent(int second)
        {
            if (second is < 0 or > 59)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The second must range in [0, 59].");
        }
        private static void ValidateMinuteComponent(int minute)
        {
            if (minute is < 0 or > 59)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The minute must range in [0, 59].");
        }
        private static void ValidateHourComponent(int hour)
        {
            if (hour is < 0 or > 23)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The hour must range in [0, 23].");
        }
        private static void ValidateDayComponent(int day)
        {
            if (day is < 1 or > 31)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The day must range in [1, 31].");
        }
        private static void ValidateMonthComponent(int month)
        {
            if (month is < 1 or > 12)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The month must range in [1, 12].");
        }
        private static void ValidateYearComponent(int year)
        {
            if (year is < 1 or > 9999)
                ThrowHelper.Throw<ArgumentOutOfRangeException>("The year must range in [1, 9999].");
        }

        // Validate the components and the date itself
        private static void ValidateDay(int year, int month, int day)
        {
            ValidateDayComponent(day);
            ValidateDate(year, month, day);
        }
        private static void ValidateMonth(int year, int month, int day)
        {
            ValidateMonthComponent(month);
            ValidateDate(year, month, day);
        }
        private static void ValidateYear(int year, int month, int day)
        {
            ValidateYearComponent(year);
            ValidateDate(year, month, day);
        }

        private static void ValidateDate(int year, int month, int day)
        {
            if (day > DateTime.DaysInMonth(year, month))
                ThrowHelper.Throw<ArgumentOutOfRangeException>($"Attempted to adjust the date to {year:D4}/{month:D2}/{day:D2} (YYYY/MM/DD), but the year/month combination contains less days than the given.");
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
