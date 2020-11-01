using Garyon.Objects;
using System;

namespace Garyon.Extensions
{
    /// <summary>Provides extension methods for the <seealso cref="IComparable"/> and <seealso cref="IComparable{T}"/> interfaces.</summary>
    public static class IComparableExtensions
    {
        #region IComparable
        /// <summary>Determines whether <paramref name="value"/> is less than <paramref name="other"/>.</summary>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is less than <paramref name="other"/>, otherwise <see langword="false"/>.</returns>
        public static bool LessThan(this IComparable value, object other) => value.CompareTo(other) < 0;
        /// <summary>Determines whether <paramref name="value"/> is greater than <paramref name="other"/>.</summary>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is greater than <paramref name="other"/>, otherwise <see langword="false"/>.</returns>
        public static bool GreaterThan(this IComparable value, object other) => value.CompareTo(other) > 0;
        /// <summary>Determines whether <paramref name="value"/> is less than or equal to <paramref name="other"/>.</summary>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is less than or equal to <paramref name="other"/>, otherwise <see langword="false"/>.</returns>
        public static bool LessThanOrEqual(this IComparable value, object other) => value.CompareTo(other) <= 0;
        /// <summary>Determines whether <paramref name="value"/> is greater than or equal to <paramref name="other"/>.</summary>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is greater than or equal to <paramref name="other"/>, otherwise <see langword="false"/>.</returns>
        public static bool GreaterThanOrEqual(this IComparable value, object other) => value.CompareTo(other) >= 0;
        /// <summary>Determines whether <paramref name="value"/> is equal to <paramref name="other"/>.</summary>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is equal to <paramref name="other"/>, otherwise <see langword="false"/>.</returns>
        public static bool EqualTo(this IComparable value, object other) => value.CompareTo(other) == 0;
        /// <summary>Determines whether <paramref name="value"/> is not equal to <paramref name="other"/>.</summary>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is not equal to <paramref name="other"/>, otherwise <see langword="false"/>.</returns>
        public static bool NotEqualTo(this IComparable value, object other) => value.CompareTo(other) != 0;

        /// <summary>Determines whether the comparison of <paramref name="value"/> and <paramref name="other"/> matches the expected result.</summary>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <param name="result">The expected comparison result.</param>
        /// <returns><see langword="true"/> if the comparison of <paramref name="value"/> with <paramref name="other"/> matches the expected result, otherwise <see langword="false"/>.</returns>
        public static bool MatchesComparisonResult(this IComparable value, object other, ComparisonResult result)
        {
            return MatchesComparisonResult(value.CompareTo(other), result);
        }
        /// <summary>Determines the result of the comparison of <paramref name="value"/> with <paramref name="other"/>.</summary>
        /// <typeparam name="object">The type of the value that <paramref name="value"/> can be compared with.</typeparam>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <returns>A <seealso cref="ComparisonResult"/> representing the result of the comparison of <paramref name="value"/> with <paramref name="other"/>.</returns>
        public static ComparisonResult GetComparisonResult(this IComparable value, object other)
        {
            return GetComparisonResult(value.CompareTo(other));
        }
        #endregion

        #region IComparable<T>
        /// <summary>Determines whether <paramref name="value"/> is less than <paramref name="other"/>.</summary>
        /// <typeparam name="T">The type of the value that <paramref name="value"/> can be compared with.</typeparam>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is less than <paramref name="other"/>, otherwise <see langword="false"/>.</returns>
        public static bool LessThan<T>(this IComparable<T> value, T other) => value.CompareTo(other) < 0;
        /// <summary>Determines whether <paramref name="value"/> is greater than <paramref name="other"/>.</summary>
        /// <typeparam name="T">The type of the value that <paramref name="value"/> can be compared with.</typeparam>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is greater than <paramref name="other"/>, otherwise <see langword="false"/>.</returns>
        public static bool GreaterThan<T>(this IComparable<T> value, T other) => value.CompareTo(other) > 0;
        /// <summary>Determines whether <paramref name="value"/> is less than or equal to <paramref name="other"/>.</summary>
        /// <typeparam name="T">The type of the value that <paramref name="value"/> can be compared with.</typeparam>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is less than or equal to <paramref name="other"/>, otherwise <see langword="false"/>.</returns>
        public static bool LessThanOrEqual<T>(this IComparable<T> value, T other) => value.CompareTo(other) <= 0;
        /// <summary>Determines whether <paramref name="value"/> is greater than or equal to <paramref name="other"/>.</summary>
        /// <typeparam name="T">The type of the value that <paramref name="value"/> can be compared with.</typeparam>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is greater than or equal to <paramref name="other"/>, otherwise <see langword="false"/>.</returns>
        public static bool GreaterThanOrEqual<T>(this IComparable<T> value, T other) => value.CompareTo(other) >= 0;
        /// <summary>Determines whether <paramref name="value"/> is equal to <paramref name="other"/>.</summary>
        /// <typeparam name="T">The type of the value that <paramref name="value"/> can be compared with.</typeparam>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is equal to <paramref name="other"/>, otherwise <see langword="false"/>.</returns>
        public static bool EqualTo<T>(this IComparable<T> value, T other) => value.CompareTo(other) == 0;
        /// <summary>Determines whether <paramref name="value"/> is not equal to <paramref name="other"/>.</summary>
        /// <typeparam name="T">The type of the value that <paramref name="value"/> can be compared with.</typeparam>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is not equal to <paramref name="other"/>, otherwise <see langword="false"/>.</returns>
        public static bool NotEqualTo<T>(this IComparable<T> value, T other) => value.CompareTo(other) != 0;

        /// <summary>Determines whether the comparison of <paramref name="value"/> and <paramref name="other"/> matches the expected result.</summary>
        /// <typeparam name="T">The type of the value that <paramref name="value"/> can be compared with.</typeparam>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <param name="result">The expected comparison result.</param>
        /// <returns><see langword="true"/> if the comparison of <paramref name="value"/> with <paramref name="other"/> matches the expected result, otherwise <see langword="false"/>.</returns>
        public static bool MatchesComparisonResult<T>(this IComparable<T> value, T other, ComparisonResult result)
        {
            return MatchesComparisonResult(value.CompareTo(other), result);
        }
        /// <summary>Determines the result of the comparison of <paramref name="value"/> with <paramref name="other"/>.</summary>
        /// <typeparam name="T">The type of the value that <paramref name="value"/> can be compared with.</typeparam>
        /// <param name="value">The provided value to compare.</param>
        /// <param name="other">The other value to compare.</param>
        /// <returns>A <seealso cref="ComparisonResult"/> representing the result of the comparison of <paramref name="value"/> with <paramref name="other"/>.</returns>
        public static ComparisonResult GetComparisonResult<T>(this IComparable<T> value, T other)
        {
            return GetComparisonResult(value.CompareTo(other));
        }
        #endregion

        private static bool MatchesComparisonResult(int comparison, ComparisonResult result)
        {
            return result switch
            {
                ComparisonResult.Less => comparison < 0,
                ComparisonResult.Greater => comparison > 0,
                ComparisonResult.Equal => comparison == 0,
                _ => false,
            };
        }
        private static ComparisonResult GetComparisonResult(int comparison)
        {
            if (comparison < 0)
                return ComparisonResult.Less;
            if (comparison > 0)
                return ComparisonResult.Greater;
            return ComparisonResult.Equal;
        }
    }
}
