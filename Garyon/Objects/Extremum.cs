namespace Garyon.Objects
{
    /// <summary>Determines an extremum kind.</summary>
    public enum Extremum
    {
        /// <summary>Represents a minimum.</summary>
        Minimum = -1,
        /// <summary>Represents a maximum.</summary>
        Maximum = 1,
    }

    /// <summary>Provides extension functions for the <seealso cref="Extremum"/> enum.</summary>
    public static class ExtremumExtensions
    {
        /// <summary>Gets the target comparison result that the extremum meets when compared against the other elements.</summary>
        /// <param name="extremum">The extremum kind.</param>
        /// <returns>The <seealso cref="ComparisonResult"/> converted from the <seealso cref="Extremum"/>.</returns>
        public static ComparisonResult TargetComparisonResult(this Extremum extremum) => (ComparisonResult)extremum;
    }
}