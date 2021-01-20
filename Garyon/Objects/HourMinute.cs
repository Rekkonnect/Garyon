using System;
using static System.Convert;

namespace Garyon.Objects
{
    /// <summary>Represents a time instance with the hour and minute.</summary>
    public struct HourMinute : IEquatable<HourMinute>, IHasHour, IHasMinute
    {
        /// <summary>Gets the current time's <seealso cref="HourMinute"/> representation.</summary>
        public static HourMinute Now => (HourMinute)DateTime.Now;
        /// <summary>Gets the <seealso cref="HourMinute"/> representation of the next minute from the current time.</summary>
        public static HourMinute NextMinute => Now + 1;
        /// <summary>Gets the <seealso cref="HourMinute"/> representation of the next hour from the current time.</summary>
        public static HourMinute NextHour => Now + 60;

        private short minutes;

        /// <summary>Gets or sets the total hours as a <seealso cref="double"/>.</summary>
        public double TotalHours
        {
            get => minutes / 60d;
            set => minutes = (short)(value * 60);
        }
        /// <summary>Gets or sets the total minutes.</summary>
        public int TotalMinutes
        {
            get => minutes;
            set => minutes = (short)value;
        }
        /// <summary>Gets the total seconds.</summary>
        public int TotalSeconds => minutes * 60;

        /// <summary>Gets or sets the hour.</summary>
        public int Hour
        {
            get => minutes / 60;
            set => minutes += (short)((value - Hour) * 60);
        }
        /// <summary>Gets or sets the minute.</summary>
        public int Minute
        {
            get => minutes % 60;
            set => minutes += (short)(value - Minute);
        }
        
        /// <summary>Initializes a new instance of the <seealso cref="HourMinute"/> struct from the total minutes of the time.</summary>
        /// <param name="totalMinutes">The total minutes of the time.</param>
        public HourMinute(int totalMinutes) => minutes = (short)totalMinutes;
        /// <summary>Initializes a new instance of the <seealso cref="HourMinute"/> struct from the hour and the minute of the time.</summary>
        /// <param name="hour">The hour of the time.</param>
        /// <param name="minute">The minute of the time.</param>
        public HourMinute(int hour, int minute) => minutes = (short)(hour * 60 + minute);

        /// <summary>Adds a number of minutes to the time of this instance.</summary>
        /// <param name="minutes">The minutes to add.</param>
        public void Add(int minutes) => TotalMinutes += minutes;
        /// <summary>Adds a number of minutes to the time of this instance.</summary>
        /// <param name="hours">The hours to add.</param>
        /// <param name="minutes">The minutes to add.</param>
        public void Add(int hours, int minutes) => Add(hours * 60 + minutes);
        /// <summary>Subtracts a number of minutes from the time of this instance.</summary>
        /// <param name="minutes">The minutes to subtract.</param>
        public void Subtract(int minutes) => Add(-minutes);
        /// <summary>Subtracts a number of minutes from the time of this instance.</summary>
        /// <param name="hours">The hours to subtract.</param>
        /// <param name="minutes">The minutes to subtract.</param>
        public void Subtract(int hours, int minutes) => Add(-hours, -minutes);

        public static HourMinute operator +(HourMinute hm, int minutes) => new HourMinute(hm.minutes + minutes);
        public static HourMinute operator +(HourMinute left, HourMinute right) => new HourMinute(left.minutes + right.minutes);
        public static HourMinute operator -(HourMinute hm, int minutes) => new HourMinute(hm.minutes - minutes);
        public static HourMinute operator -(HourMinute left, HourMinute right) => new HourMinute(left.minutes - right.minutes);

        public static bool operator <(HourMinute left, HourMinute right) => left.minutes < right.minutes;
        public static bool operator <=(HourMinute left, HourMinute right) => left.minutes <= right.minutes;
        public static bool operator ==(HourMinute left, HourMinute right) => left.minutes == right.minutes;
        public static bool operator >=(HourMinute left, HourMinute right) => left.minutes >= right.minutes;
        public static bool operator >(HourMinute left, HourMinute right) => left.minutes > right.minutes;
        public static bool operator !=(HourMinute left, HourMinute right) => left.minutes != right.minutes;

        public static implicit operator DateTimeOffset(HourMinute t) => DateTimeOffset.Now.Date.Add(t);
        public static implicit operator DateTime(HourMinute t) => DateTime.Now.Date.Add(t);
        public static implicit operator TimeSpan(HourMinute t) => new TimeSpan(t.Hour, t.Minute, 0);
        public static explicit operator HourMinute(DateTimeOffset t) => new HourMinute(t.Hour, t.Minute);
        public static explicit operator HourMinute(DateTime t) => new HourMinute(t.Hour, t.Minute);
        public static explicit operator HourMinute(TimeSpan t) => new HourMinute(t.Hours, t.Minutes);

        /// <summary>Parses the given string representation of an hour-minute time into a <seealso cref="HourMinute"/> instance.</summary>
        /// <param name="s">The string representation of an hour-minute of the form "HH:MM". The string may contain additional numbers split with ":", which will be ignored.</param>
        /// <returns>The parsed <seealso cref="HourMinute"/> instance.</returns>
        public static HourMinute Parse(string s)
        {
            var split = s.Split(':');
            return new HourMinute(ToInt32(split[0]), ToInt32(split[1]));
        }

        /// <summary>Determines whether another <seealso cref="HourMinute"/> instance is equal to this one.</summary>
        /// <param name="other">The other <seealso cref="HourMinute"/> instance.</param>
        /// <returns>A value determining whether both objects are equal or not.</returns>
        public bool Equals(HourMinute other) => other == this;
        public override bool Equals(object obj) => obj is HourMinute h && h == this;
        public override int GetHashCode() => minutes.GetHashCode();
        /// <summary>Gets the string representation of the hour-minute time.</summary>
        /// <returns>The string representation of the hour-minute time in the form "HH:MM".</returns>
        public override string ToString() => $"{Hour:D2}:{Minute:D2}";
    }
}
