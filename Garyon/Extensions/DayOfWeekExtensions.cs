using System;
using System.Diagnostics.CodeAnalysis;

namespace Garyon.Extensions
{
    /// <summary>Provides extensions for the <seealso cref="DayOfWeek"/> enum.</summary>
    public static class DayOfWeekExtensions
    {
        /// <summary>Shifts the given <seealso cref="DayOfWeek"/> to another, regarding the new starting day of week.</summary>
        /// <param name="value">The <seealso cref="DayOfWeek"/> to shift.</param>
        /// <param name="starting">The new starting <seealso cref="DayOfWeek"/>.</param>
        /// <returns>The <seealso cref="DayOfWeek"/> whose offset from <paramref name="starting"/> is the same offset from <seealso cref="DayOfWeek.Sunday"/>.</returns>
        public static DayOfWeek ShiftRegardingStartingWeekDay(this DayOfWeek value, DayOfWeek starting)
        {
            return (DayOfWeek)(((int)value + (int)starting) % 7);
        }

        /// <summary>Determines whether the given <seealso cref="DayOfWeek"/> represents a weekday, between Monday and Friday.</summary>
        /// <param name="day">The <seealso cref="DayOfWeek"/> to determine if it's a weekday.</param>
        /// <returns><see langword="true"/> if the value is between <seealso cref="DayOfWeek.Monday"/> and <seealso cref="DayOfWeek.Friday"/>, inclusive, otherwise false.</returns>
        [ExcludeFromCodeCoverage(Justification = "Trivial")]
        public static bool IsWeekday(this DayOfWeek day)
        {
            return day is >= DayOfWeek.Monday and <= DayOfWeek.Friday;
        }
        /// <summary>Determines whether the given <seealso cref="DayOfWeek"/> represents a weekend day, which is Saturday or Sunday.</summary>
        /// <param name="day">The <seealso cref="DayOfWeek"/> to determine if it's a weekend day.</param>
        /// <returns><see langword="true"/> if the value is either <seealso cref="DayOfWeek.Saturday"/> or <seealso cref="DayOfWeek.Sunday"/>, otherwise false.</returns>
        [ExcludeFromCodeCoverage(Justification = "Trivial")]
        public static bool IsWeekend(this DayOfWeek day)
        {
            return day is DayOfWeek.Saturday or DayOfWeek.Sunday;
        }
    }
}
