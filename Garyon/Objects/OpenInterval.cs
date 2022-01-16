using Garyon.Extensions;
using System;

namespace Garyon.Objects
{
    /// <summary>Represents an open interval to the one direction from a given value.</summary>
    /// <typeparam name="T">The type of the reference value for the open interval.</typeparam>
    /// <param name="Value">The open interval's reference value.</param>
    /// <param name="ComparisonKinds">The comparison kinds representing the direction of the open interval. It is applied to the left side of the reference value, for instance &gt;= 4.</param>
    public record OpenInterval<T>(T Value, ComparisonKinds ComparisonKinds)
        where T : IComparable<T>
    {
        /// <summary>Determines whether a value is contained in this open interval.</summary>
        /// <param name="other">The other value to compare against this open interval.</param>
        /// <returns><see langword="true"/> if the value is contained in the open interval, meaning that <paramref name="other"/> [comparison] <seealso cref="Value"/> is <see langword="true"/>, otherwise <see langword="false"/>.</returns>
        public bool Contains(T other) => other.SatisfiesComparison(Value, ComparisonKinds);

        /// <summary>Converts a value into an <seealso cref="OpenInterval{T}"/>, considering the open interval as the single value interval, ranging between the same value.</summary>
        /// <param name="value">The reference value of the open interval.</param>
        public static implicit operator OpenInterval<T>(T value) => new(value, ComparisonKinds.Equal);
    }
}