using System;

namespace Garyon.Extensions
{
    /// <summary>Provides extensions for the <seealso cref="TimeSpan"/> struct.</summary>
    public static class TimeSpanExtensions
    {
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

        // TODO: Add With* extensions
    }
}
