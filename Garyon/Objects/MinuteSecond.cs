using System;

namespace Garyon.Objects
{
    /// <summary>Represents a time instance with the minute and second.</summary>
    public struct MinuteSecond : IEquatable<MinuteSecond>, IHasMinute, IHasSecond
    {
        /// <summary>Gets the current time's <seealso cref="MinuteSecond"/> representation.</summary>
        public static MinuteSecond Now => (MinuteSecond)DateTime.Now;
        /// <summary>Gets the <seealso cref="MinuteSecond"/> representation of the next second from the current time.</summary>
        public static MinuteSecond NextSecond => Now + 1;
        /// <summary>Gets the <seealso cref="MinuteSecond"/> representation of the next minute from the current time.</summary>
        public static MinuteSecond NextMinute => Now + 60;

        private short seconds;

        /// <summary>Gets or sets the total hours as a <seealso cref="double"/>.</summary>
        public double TotalHours
        {
            get => seconds / (60d * 60);
            set => seconds = (short)(value * 60 * 60);
        }
        /// <summary>Gets or sets the total minutes as a <seealso cref="double"/>.</summary>
        public double TotalMinutes
        {
            get => seconds / 60d;
            set => seconds = (short)(value * 60);
        }
        /// <summary>Gets the total seconds.</summary>
        public int TotalSeconds
        {
            get => seconds;
            set => seconds = (short)value;
        }

        /// <summary>Gets or sets the minute.</summary>
        public int Minute
        {
            get => seconds / 60;
            set => seconds += (short)((value - Minute) * 60);
        }
        /// <summary>Gets or sets the second.</summary>
        public int Second
        {
            get => seconds % 60;
            set => seconds += (short)(value - Second);
        }
        
        /// <summary>Initializes a new instance of the <seealso cref="MinuteSecond"/> struct from the total seconds of the time.</summary>
        /// <param name="totalSeconds">The total seconds of the time.</param>
        public MinuteSecond(int totalSeconds) => seconds = (short)totalSeconds;
        /// <summary>Initializes a new instance of the <seealso cref="MinuteSecond"/> struct from the minute and the second of the time.</summary>
        /// <param name="minute">The minute of the time.</param>
        /// <param name="second">The second of the time.</param>
        public MinuteSecond(int minute, int second) => seconds = (short)(minute * 60 + second);

        /// <summary>Adds a number of seconds to the time of this instance.</summary>
        /// <param name="seconds">The seconds to add.</param>
        public void Add(int seconds) => TotalSeconds += seconds;
        /// <summary>Adds a number of seconds to the time of this instance.</summary>
        /// <param name="minutes">The minutes to add.</param>
        /// <param name="seconds">The seconds to add.</param>
        public void Add(int minutes, int seconds) => Add(minutes * 60 + seconds);
        /// <summary>Subtracts a number of seconds from the time of this instance.</summary>
        /// <param name="seconds">The seconds to subtract.</param>
        public void Subtract(int seconds) => Add(-seconds);
        /// <summary>Subtracts a number of seconds from the time of this instance.</summary>
        /// <param name="minutes">The minutes to subtract.</param>
        /// <param name="seconds">The seconds to subtract.</param>
        public void Subtract(int minutes, int seconds) => Add(-minutes, -seconds);

        public static MinuteSecond operator +(MinuteSecond hm, int seconds) => new MinuteSecond(hm.seconds + seconds);
        public static MinuteSecond operator +(MinuteSecond left, MinuteSecond right) => new MinuteSecond(left.seconds + right.seconds);
        public static MinuteSecond operator -(MinuteSecond hm, int seconds) => new MinuteSecond(hm.seconds - seconds);
        public static MinuteSecond operator -(MinuteSecond left, MinuteSecond right) => new MinuteSecond(left.seconds - right.seconds);

        public static bool operator <(MinuteSecond left, MinuteSecond right) => left.seconds < right.seconds;
        public static bool operator <=(MinuteSecond left, MinuteSecond right) => left.seconds <= right.seconds;
        public static bool operator ==(MinuteSecond left, MinuteSecond right) => left.seconds == right.seconds;
        public static bool operator >=(MinuteSecond left, MinuteSecond right) => left.seconds >= right.seconds;
        public static bool operator >(MinuteSecond left, MinuteSecond right) => left.seconds > right.seconds;
        public static bool operator !=(MinuteSecond left, MinuteSecond right) => left.seconds != right.seconds;

        public static implicit operator DateTimeOffset(MinuteSecond t) => DateTimeOffset.Now.Date.Add(t);
        public static implicit operator DateTime(MinuteSecond t) => DateTime.Now.Date.Add(t);
        public static implicit operator TimeSpan(MinuteSecond t) => new TimeSpan(0, t.Minute, t.Second);
        public static explicit operator MinuteSecond(DateTimeOffset t) => new MinuteSecond(t.Minute, t.Second);
        public static explicit operator MinuteSecond(DateTime t) => new MinuteSecond(t.Minute, t.Second);
        public static explicit operator MinuteSecond(TimeSpan t) => new MinuteSecond(t.Minutes, t.Seconds);

        /// <summary>Parses the given string representation of an minute-second time into a <seealso cref="MinuteSecond"/> instance.</summary>
        /// <param name="s">The string representation of an minute-second of the form "MM:SS". The string may contain additional numbers split with ":", which will be ignored.</param>
        /// <returns>The parsed <seealso cref="MinuteSecond"/> instance.</returns>
        public static MinuteSecond Parse(string s)
        {
            var split = s.Split(':');
            return new MinuteSecond(Convert.ToInt32(split[0]), Convert.ToInt32(split[1]));
        }

        /// <summary>Determines whether another <seealso cref="MinuteSecond"/> instance is equal to this one.</summary>
        /// <param name="other">The other <seealso cref="MinuteSecond"/> instance.</param>
        /// <returns>A value determining whether both objects are equal or not.</returns>
        public bool Equals(MinuteSecond other) => other == this;
        public override bool Equals(object obj) => obj is MinuteSecond h && h == this;
        public override int GetHashCode() => seconds.GetHashCode();
        /// <summary>Gets the string representation of the minute-second time.</summary>
        /// <returns>The string representation of the minute-second time in the form "MM:SS".</returns>
        public override string ToString() => $"{Minute:D2}:{Second:D2}";
    }
}