using System;
using System.Runtime.CompilerServices;

namespace Garyon.Objects
{
    /// <summary>Contains a set of <seealso cref="ConsoleColor"/> instances, for both the background and the foreground colors.</summary>
    public struct ConsoleColorSet
    {
        /// <summary>Gets a new instance of the default <seealso cref="ConsoleColorSet"/>.</summary>
        public static ConsoleColorSet Default => new ConsoleColorSet(ConsoleColor.Gray, ConsoleColor.Black);

        /// <summary>The foreground color.</summary>
        public ConsoleColor ForegroundColor { get; set; }
        /// <summary>The background color of the console.</summary>
        public ConsoleColor BackgroundColor { get; set; }

        /// <summary>Gets or sets the color tuple, defined as (ForegroundColor, BackgroundColor).</summary>
        public (ConsoleColor, ConsoleColor) ColorTuple
        {
            get => (ForegroundColor, BackgroundColor);
            set => (ForegroundColor, BackgroundColor) = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="ConsoleColorSet"/> struct from a foreground and a background color.</summary>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        public ConsoleColorSet(ConsoleColor foreground, ConsoleColor background) => (ForegroundColor, BackgroundColor) = (foreground, background);

        /// <summary>Sets the colors to the specified values.</summary>
        /// <param name="foreground">The foreground color to set.</param>
        /// <param name="background">The background color to set.</param>
        public void SetColors(ConsoleColor foreground, ConsoleColor background) => ColorTuple = (foreground, background);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(ConsoleColorSet left, ConsoleColorSet right) => left.ColorTuple == right.ColorTuple;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(ConsoleColorSet left, ConsoleColorSet right) => left.ColorTuple != right.ColorTuple;

        /// <summary>Determines whether this object is equal to another object.</summary>
        /// <param name="obj">The object to determine whether this is equal to.</param>
        /// <returns>The result of the comparison.</returns>
        public override bool Equals(object obj) => ColorTuple == ((ConsoleColorSet)obj).ColorTuple;
        /// <summary>Gets the hash code for this instance, based on the color tuple.</summary>
        /// <returns>The hash code of this instance.</returns>
        public override int GetHashCode() => ColorTuple.GetHashCode();
    }
}
