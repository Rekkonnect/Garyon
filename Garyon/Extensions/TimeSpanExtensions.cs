using Garyon.Exceptions;
using Garyon.Objects;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Garyon.Extensions
{
    /// <summary>Represents a component in the <seealso cref="TimeSpan"/> struct.</summary>
    public enum TimeSpanComponent
    {
        /// <summary>Represents the <seealso cref="TimeSpan.Ticks"/> component.</summary>
        Ticks = 0,

        /// <summary>Represents the <seealso cref="TimeSpan.Milliseconds"/> component.</summary>
        Milliseconds,
        /// <summary>Represents the <seealso cref="TimeSpan.Seconds"/> component.</summary>
        Seconds,
        /// <summary>Represents the <seealso cref="TimeSpan.Minutes"/> component.</summary>
        Minutes,
        /// <summary>Represents the <seealso cref="TimeSpan.Hours"/> component.</summary>
        Hours,
        /// <summary>Represents the <seealso cref="TimeSpan.Days"/> component.</summary>
        Days,
    }

    /// <summary>Provides extensions for the <seealso cref="TimeSpan"/> struct.</summary>
    public static class TimeSpanExtensions
    {
        /// <summary>Gets the sign of the given <seealso cref="TimeSpan"/>.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> instance whose sign to get.</param>
        /// <returns>The sign of the <seealso cref="TimeSpan.Ticks"/> property, in accordance with <seealso cref="Math.Sign(long)"/>.</returns>
        public static int Sign(this TimeSpan timeSpan) => Math.Sign(timeSpan.Ticks);

        /// <summary>Gets the specified component of the given <seealso cref="TimeSpan"/> instance.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> instance whose component to get.</param>
        /// <param name="component">The component of the <seealso cref="TimeSpan"/> instance to get.</param>
        /// <returns>The requested component of the <seealso cref="TimeSpan"/> instance, represented by the specified <seealso cref="TimeSpanComponent"/>.</returns>
        public static long GetComponentInt64(this TimeSpan timeSpan, TimeSpanComponent component) => component switch
        {
            TimeSpanComponent.Ticks => timeSpan.Ticks,
            _ => GetComponent(timeSpan, component),
        };
        /// <summary>Gets the specified component of the given <seealso cref="TimeSpan"/> instance.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> instance whose component to get.</param>
        /// <param name="component">The component of the <seealso cref="TimeSpan"/> instance to get. To get the <seealso cref="TimeSpanComponent.Ticks"/> component, use <seealso cref="GetComponentInt64(TimeSpan, TimeSpanComponent)"/>.</param>
        /// <returns>The requested component of the <seealso cref="TimeSpan"/> instance, represented by the specified <seealso cref="TimeSpanComponent"/>.</returns>
        public static int GetComponent(this TimeSpan timeSpan, TimeSpanComponent component) => component switch
        {
            TimeSpanComponent.Milliseconds => timeSpan.Milliseconds,
            TimeSpanComponent.Seconds => timeSpan.Seconds,
            TimeSpanComponent.Minutes => timeSpan.Minutes,
            TimeSpanComponent.Hours => timeSpan.Hours,
            TimeSpanComponent.Days => timeSpan.Days,
        };

        #region Within Time Unit
        /// <summary>Creates a new <seealso cref="TimeSpan"/> instance reflecting the duration within the final day.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> instance whose same-day duration to calculate.</param>
        /// <returns>The duration within the final day. In other words, this reflects the same duration, except the <seealso cref="TimeSpan.Days"/> property is 0.</returns>
        public static TimeSpan WithinDay(this TimeSpan timeSpan)
        {
            return WithinTimeUnit(timeSpan, TimeSpan.TicksPerDay);
        }
        /// <summary>Creates a new <seealso cref="TimeSpan"/> instance reflecting the duration within the final hour.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> instance whose same-hour duration to calculate.</param>
        /// <returns>The duration within the final hour. In other words, this reflects the same duration, except the <seealso cref="TimeSpan.Days"/> and <seealso cref="TimeSpan.Hours"/> properties are 0.</returns>
        public static TimeSpan WithinHour(this TimeSpan timeSpan)
        {
            return WithinTimeUnit(timeSpan, TimeSpan.TicksPerHour);
        }
        /// <summary>Creates a new <seealso cref="TimeSpan"/> instance reflecting the duration within the final minute.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> instance whose same-minute duration to calculate.</param>
        /// <returns>The duration within the final minute. In other words, this reflects the same duration, except the <seealso cref="TimeSpan.Days"/>, <seealso cref="TimeSpan.Hours"/> and <seealso cref="TimeSpan.Minutes"/> properties are 0.</returns>
        public static TimeSpan WithinMinute(this TimeSpan timeSpan)
        {
            return WithinTimeUnit(timeSpan, TimeSpan.TicksPerMinute);
        }
        /// <summary>Creates a new <seealso cref="TimeSpan"/> instance reflecting the duration within the final second.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> instance whose same-second duration to calculate.</param>
        /// <returns>The duration within the final second. In other words, this reflects the same duration, except the <seealso cref="TimeSpan.Days"/>, <seealso cref="TimeSpan.Hours"/>, <seealso cref="TimeSpan.Minutes"/> and <seealso cref="TimeSpan.Seconds"/> properties are 0.</returns>
        public static TimeSpan WithinSecond(this TimeSpan timeSpan)
        {
            return WithinTimeUnit(timeSpan, TimeSpan.TicksPerSecond);
        }
        /// <summary>Creates a new <seealso cref="TimeSpan"/> instance reflecting the duration within the final millisecond.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> instance whose same-millisecond duration to calculate.</param>
        /// <returns>The duration within the final millisecond. In other words, this reflects the same duration, except the <seealso cref="TimeSpan.Days"/>, <seealso cref="TimeSpan.Hours"/>, <seealso cref="TimeSpan.Minutes"/>, <seealso cref="TimeSpan.Seconds"/> and <seealso cref="TimeSpan.Milliseconds"/> properties are 0.</returns>
        public static TimeSpan WithinMillisecond(this TimeSpan timeSpan)
        {
            return WithinTimeUnit(timeSpan, TimeSpan.TicksPerMillisecond);
        }

        private static TimeSpan WithinTimeUnit(this TimeSpan timeSpan, in long ticksPerUnit)
        {
            return new(timeSpan.Ticks % ticksPerUnit);
        }
        #endregion

        #region With Components
        /// <summary>Creates a copy of a given <seealso cref="TimeSpan"/>, where the days component of the copied value is set to a specified value.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> from which to create the copy with the adjusted days component.</param>
        /// <param name="days">The value of the days component of the resulting copied <seealso cref="TimeSpan"/>. It must match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</param>
        /// <returns>A copy of the original <seealso cref="TimeSpan"/> with the days component set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="days"/> does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</exception>
        public static TimeSpan WithDays(this TimeSpan timeSpan, int days)
        {
            ValidateDaysComponent(timeSpan, days);
            return timeSpan.Add(TimeSpan.FromDays(days - timeSpan.Days));
        }
        /// <summary>Creates a copy of a given <seealso cref="TimeSpan"/>, where the hours component of the copied value is set to a specified value.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> from which to create the copy with the adjusted hours component.</param>
        /// <param name="hours">The value of the hours component of the resulting copied <seealso cref="TimeSpan"/>. It must be within the range [-23, 23] and match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</param>
        /// <returns>A copy of the original <seealso cref="TimeSpan"/> with the hours component set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="hours"/> is outside the range [-23, 23] -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</exception>
        public static TimeSpan WithHours(this TimeSpan timeSpan, int hours)
        {
            ValidateHoursComponent(timeSpan, hours);
            return timeSpan.Add(TimeSpan.FromHours(hours - timeSpan.Hours));
        }
        /// <summary>Creates a copy of a given <seealso cref="TimeSpan"/>, where the minutes component of the copied value is set to a specified value.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> from which to create the copy with the adjusted minutes component.</param>
        /// <param name="minutes">The value of the minutes component of the resulting copied <seealso cref="TimeSpan"/>. It must be within the range [-59, 59] and match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</param>
        /// <returns>A copy of the original <seealso cref="TimeSpan"/> with the minutes component set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="minutes"/> is outside the range [-59, 59] -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</exception>
        public static TimeSpan WithMinutes(this TimeSpan timeSpan, int minutes)
        {
            ValidateMinutesComponent(timeSpan, minutes);
            return timeSpan.Add(TimeSpan.FromMinutes(minutes - timeSpan.Minutes));
        }
        /// <summary>Creates a copy of a given <seealso cref="TimeSpan"/>, where the seconds component of the copied value is set to a specified value.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> from which to create the copy with the adjusted seconds component.</param>
        /// <param name="seconds">The value of the seconds component of the resulting copied <seealso cref="TimeSpan"/>. It must be within the range [-59, 59] and match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</param>
        /// <returns>A copy of the original <seealso cref="TimeSpan"/> with the seconds component set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="seconds"/> is outside the range [-59, 59] -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</exception>
        public static TimeSpan WithSeconds(this TimeSpan timeSpan, int seconds)
        {
            ValidateSecondsComponent(timeSpan, seconds);
            return timeSpan.Add(TimeSpan.FromSeconds(seconds - timeSpan.Seconds));
        }
        /// <summary>Creates a copy of a given <seealso cref="TimeSpan"/>, where the milliseconds component of the copied value is set to a specified value.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> from which to create the copy with the adjusted milliseconds component.</param>
        /// <param name="milliseconds">The value of the milliseconds component of the resulting copied <seealso cref="TimeSpan"/>. It must be within the range [-999, 999] and match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</param>
        /// <returns>A copy of the original <seealso cref="TimeSpan"/> with the milliseconds component set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="milliseconds"/> is outside the range [-999, 999] -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</exception>
        public static TimeSpan WithMilliseconds(this TimeSpan timeSpan, int milliseconds)
        {
            ValidateMillisecondsComponent(timeSpan, milliseconds);
            return timeSpan.Add(TimeSpan.FromMilliseconds(milliseconds - timeSpan.Milliseconds));
        }

        /// <summary>Creates a copy of a given <seealso cref="TimeSpan"/>, where the specified component of the copied value is set to the specified value.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> from which to create the copy with the adjusted time components.</param>
        /// <param name="value">The value to set the component to.</param>
        /// <param name="component">The component of the <seealso cref="TimeSpan"/> instance that will be changed. <seealso cref="TimeSpanComponent.Ticks"/> is not accepted in this overload, consider using <seealso cref="WithComponent(TimeSpan, long, TimeSpanComponent)"/>.</param>
        /// <returns>A copy of the original <seealso cref="TimeSpan"/> with the specified component set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="value"/> falls out of the valid range for the given component, or an invalid day is represented.</exception>
        [ExcludeFromCodeCoverage(Justification = "Trivial")]
        public static TimeSpan WithComponent(this TimeSpan timeSpan, int value, TimeSpanComponent component)
        {
            return component switch
            {
                TimeSpanComponent.Milliseconds => timeSpan.WithMilliseconds(value),
                TimeSpanComponent.Seconds => timeSpan.WithSeconds(value),
                TimeSpanComponent.Minutes => timeSpan.WithMinutes(value),
                TimeSpanComponent.Hours => timeSpan.WithHours(value),
                TimeSpanComponent.Days => timeSpan.WithDays(value),
            };
        }
        /// <summary>Creates a copy of a given <seealso cref="TimeSpan"/>, where the specified component of the copied value is set to the specified value.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> from which to create the copy with the adjusted time components.</param>
        /// <param name="value">The value to set the component to.</param>
        /// <param name="component">The component of the <seealso cref="TimeSpan"/> instance that will be changed. <seealso cref="TimeSpanComponent.Ticks"/> is also accepted.</param>
        /// <returns>A copy of the original <seealso cref="TimeSpan"/> with the specified component set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The given value for <paramref name="value"/> falls out of the valid range for the given component, or an invalid day is represented.</exception>
        [ExcludeFromCodeCoverage(Justification = "Trivial")]
        public static TimeSpan WithComponent(this TimeSpan timeSpan, long value, TimeSpanComponent component)
        {
            return component switch
            {
                TimeSpanComponent.Ticks => new(value),

                _ => timeSpan.WithComponent((int)value, component),
            };
        }

        /*
         * With* extensions:
         * 
         * Done
         * - ss.ms
         * - mm:ss
         * - mm:ss.ms
         * - hh:mm
         * 
         * Remaining
         * - hh:mm:ss
         * - hh:mm:ss.ms
         * - dd:hh
         * - dd:hh:mm
         */

        /// <summary>Creates a copy of a given <seealso cref="TimeSpan"/>, where the seconds and milliseconds components of the copied value are set to a specified value.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> from which to create the copy with the adjusted seconds and milliseconds components.</param>
        /// <param name="seconds">The value of the seconds component of the resulting copied <seealso cref="TimeSpan"/>. It must be within the range [-59, 59] and match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</param>
        /// <param name="milliseconds">The value of the milliseconds component of the resulting copied <seealso cref="TimeSpan"/>. It must be within the range [-999, 999] and match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</param>
        /// <returns>A copy of the original <seealso cref="TimeSpan"/> with the seconds and milliseconds components set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The given value for <paramref name="milliseconds"/> is outside the range [-999, 999]
        /// -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero
        /// -or- the given value for <paramref name="seconds"/> is outside the range [-59, 59]
        /// -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.
        /// </exception>
        public static TimeSpan WithSecondsMilliseconds(this TimeSpan timeSpan, int seconds, int milliseconds)
        {
            ValidateSecondsComponent(timeSpan, seconds);
            ValidateMillisecondsComponent(timeSpan, milliseconds);
            int currentMilliseconds = timeSpan.Seconds * 1000 + timeSpan.Milliseconds;
            int targetMilliseconds = seconds * 1000 + milliseconds;
            return timeSpan.Add(TimeSpan.FromMilliseconds(targetMilliseconds - currentMilliseconds));
        }
        /// <summary>Creates a copy of a given <seealso cref="TimeSpan"/>, where the minutes, seconds and milliseconds components of the copied value are set to a specified value.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> from which to create the copy with the adjusted minutes, seconds and milliseconds components.</param>
        /// <param name="minutes">The value of the minutes component of the resulting copied <seealso cref="TimeSpan"/>. It must be within the range [-59, 59] and match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</param>
        /// <param name="seconds">The value of the seconds component of the resulting copied <seealso cref="TimeSpan"/>. It must be within the range [-59, 59] and match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</param>
        /// <param name="milliseconds">The value of the milliseconds component of the resulting copied <seealso cref="TimeSpan"/>. It must be within the range [-999, 999] and match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</param>
        /// <returns>A copy of the original <seealso cref="TimeSpan"/> with the minutes, seconds and milliseconds components set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The given value for <paramref name="milliseconds"/> is outside the range [-999, 999]
        /// -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero
        /// -or- the given value for <paramref name="seconds"/> is outside the range [-59, 59]
        /// -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero
        /// -or- the given value for <paramref name="minutes"/> is outside the range [-59, 59]
        /// -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.
        /// </exception>
        public static TimeSpan WithMinutesSecondsMilliseconds(this TimeSpan timeSpan, int minutes, int seconds, int milliseconds)
        {
            ValidateMinutesComponent(timeSpan, minutes);
            ValidateSecondsComponent(timeSpan, seconds);
            ValidateMillisecondsComponent(timeSpan, milliseconds);
            int currentMilliseconds = (timeSpan.Minutes * 60 + timeSpan.Seconds) * 1000 + timeSpan.Milliseconds;
            int targetMilliseconds = (minutes * 60 + seconds) * 1000 + milliseconds;
            return timeSpan.Add(TimeSpan.FromMilliseconds(targetMilliseconds - currentMilliseconds));
        }
        /// <summary>Creates a copy of a given <seealso cref="TimeSpan"/>, where the minutes and seconds components of the copied value are set to a specified value.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> from which to create the copy with the adjusted minutes and seconds components.</param>
        /// <param name="minutes">The value of the minutes component of the resulting copied <seealso cref="TimeSpan"/>. It must be within the range [-59, 59] and match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</param>
        /// <param name="seconds">The value of the seconds component of the resulting copied <seealso cref="TimeSpan"/>. It must be within the range [-59, 59] and match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</param>
        /// <returns>A copy of the original <seealso cref="TimeSpan"/> with the minutes and seconds components set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The given value for <paramref name="seconds"/> is outside the range [-59, 59]
        /// -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero
        /// -or- the given value for <paramref name="minutes"/> is outside the range [-59, 59]
        /// -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.
        /// </exception>
        public static TimeSpan WithMinutesSeconds(this TimeSpan timeSpan, int minutes, int seconds)
        {
            ValidateMinutesComponent(timeSpan, minutes);
            ValidateSecondsComponent(timeSpan, seconds);
            int currentSeconds = timeSpan.Minutes * 60 + timeSpan.Seconds;
            int targetSeconds = minutes * 60 + seconds;
            return timeSpan.Add(TimeSpan.FromSeconds(targetSeconds - currentSeconds));
        }
        /// <summary>Creates a copy of a given <seealso cref="TimeSpan"/>, where the hours and minutes components of the copied value are set to a specified value.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> from which to create the copy with the adjusted hours and minutes components.</param>
        /// <param name="hours">The value of the hours component of the resulting copied <seealso cref="TimeSpan"/>. It must be within the range [-59, 59] and match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</param>
        /// <param name="minutes">The value of the minutes component of the resulting copied <seealso cref="TimeSpan"/>. It must be within the range [-23, 23] and match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.</param>
        /// <returns>A copy of the original <seealso cref="TimeSpan"/> with the hours and minutes components set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The given value for <paramref name="minutes"/> is outside the range [-59, 59]
        /// -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero
        /// -or- the given value for <paramref name="hours"/> is outside the range [-23, 23]
        /// -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.
        /// </exception>
        public static TimeSpan WithHoursMinutes(this TimeSpan timeSpan, int hours, int minutes)
        {
            ValidateHoursComponent(timeSpan, hours);
            ValidateMinutesComponent(timeSpan, minutes);
            int currentMinutes = timeSpan.Hours * 60 + timeSpan.Minutes;
            int targetMinutes = hours * 60 + minutes;
            return timeSpan.Add(TimeSpan.FromMinutes(targetMinutes - currentMinutes));
        }

        /// <summary>Creates a copy of a given <seealso cref="TimeSpan"/>, where the minutes and seconds components of the copied value are set to a specified value.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> from which to create the copy with the adjusted minutes and seconds components.</param>
        /// <param name="minuteSecond">The minutes and seconds components of the resulting copied <seealso cref="TimeSpan"/>.</param>
        /// <returns>A copy of the original <seealso cref="TimeSpan"/> with the minutes and seconds components set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The given value for the seconds component is outside the range [-59, 59]
        /// -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero
        /// -or- the given value for the minutes component is outside the range [-59, 59]
        /// -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.
        /// </exception>
        public static TimeSpan WithMinutesSeconds(this TimeSpan timeSpan, MinuteSecond minuteSecond)
        {
            return timeSpan.WithMinutesSeconds(minuteSecond.Minute, minuteSecond.Second);
        }
        /// <summary>Creates a copy of a given <seealso cref="TimeSpan"/>, where the hours and minutes components of the copied value are set to a specified value.</summary>
        /// <param name="timeSpan">The <seealso cref="TimeSpan"/> from which to create the copy with the adjusted hours and minutes components.</param>
        /// <param name="hourMinute">The hours and minutes components of the resulting copied <seealso cref="TimeSpan"/>.</param>
        /// <returns>A copy of the original <seealso cref="TimeSpan"/> with the hours and minutes components set to the specified value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The given value for the minutes component is outside the range [-59, 59]
        /// -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero
        /// -or- the given value for the hours component is outside the range [-23, 23]
        /// -or- does not match the sign of the given <seealso cref="TimeSpan"/>, if non-zero.
        /// </exception>
        public static TimeSpan WithHoursMinutes(this TimeSpan timeSpan, HourMinute hourMinute)
        {
            return timeSpan.WithHoursMinutes(hourMinute.Hour, hourMinute.Minute);
        }

        #region Validation
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateDaysComponent(TimeSpan timeSpan, int days)
        {
            int timeSpanSign = timeSpan.Sign();
            if (timeSpanSign is 0)
                return;

            int daysSign = Math.Sign(days);
            if (daysSign is 0)
                return;

            if (timeSpanSign != daysSign)
                ThrowHelper.Throw<ArgumentOutOfRangeException>($"The component's value must match the sign of the time span.");
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateHoursComponent(TimeSpan timeSpan, int hours)
        {
            int sign = timeSpan.Sign();
            bool invalid = sign switch
            {
                <  0 => hours is < -23 or > 0,
                >= 0 => hours is < 0   or > 23,
            };

            if (invalid)
                ThrowArgumentOutOfRangeException(nameof(hours), 23);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateMinutesComponent(TimeSpan timeSpan, int minutes)
        {
            ValidateSecondsOrMinutesComponent(timeSpan, minutes, nameof(minutes));
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateSecondsComponent(TimeSpan timeSpan, int seconds)
        {
            ValidateSecondsOrMinutesComponent(timeSpan, seconds, nameof(seconds));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateSecondsOrMinutesComponent(TimeSpan timeSpan, int value, string component)
        {
            int sign = timeSpan.Sign();
            bool invalid = sign switch
            {
                <  0 => value is < -59 or > 0,
                >= 0 => value is < 0   or > 59,
            };

            if (invalid)
                ThrowArgumentOutOfRangeException(component, 59);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ValidateMillisecondsComponent(TimeSpan timeSpan, int milliseconds)
        {
            int sign = timeSpan.Sign();
            bool invalid = sign switch
            {
                <  0 => milliseconds is < -999 or > 0,
                >= 0 => milliseconds is <    0 or > 999,
            };

            if (invalid)
                ThrowArgumentOutOfRangeException(nameof(milliseconds), 999);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ThrowArgumentOutOfRangeException(string component, int rangeEdges)
        {
            ThrowHelper.Throw<ArgumentOutOfRangeException>($"The {component} component must range in [-{rangeEdges}, {rangeEdges}], while also matching the sign of the time span.");
        }
        #endregion
        #endregion
    }
}
