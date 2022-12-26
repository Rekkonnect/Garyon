using Garyon.Objects;
using System;
using System.Collections.Generic;

namespace Garyon.Extensions
{
    /// <summary>Contains a collection of early-termiating LINQ-like extensions.</summary>
    public static partial class EarlyTerminatingLinqExtensions
    {
        #region Documentation templates
        /// <param name="values">The collection of values whose sum to calculate.</param>
        /// <param name="target">The target value to compare the collection's sum against.</param>
        /// <remarks>
        /// It is not necessary that the entire collection is enumerated. Enumeration halts upon alternation of satisfying the given comparison.<br/>
        /// Overflows are not accounted for. Ensure that the sum of the entire collection will not overflow, causing this result to appear wrong.
        /// </remarks>
        static partial void SumSatisfiesDocTemplate(int values, int target);
        #endregion

        /// <inheritdoc cref="SumSatisfiesDocTemplate"/>
        /// <summary>Determines whether the sum of a collection of values is strictly less than the target.</summary>
        /// <returns><see langword="true"/> if the sum is strictly less than (&lt;) the target, otherwise <see langword="false"/>.</returns>
        public static bool SumsBelow(this IEnumerable<uint> values, uint target) => SumSatisfies(values, ComparisonKinds.Less, target);
        /// <inheritdoc cref="SumSatisfiesDocTemplate"/>
        /// <summary>Determines whether the sum of a collection of values is at most equal to the target.</summary>
        /// <returns><see langword="true"/> if the sum is less than or equal to (&lt;=) the target, otherwise <see langword="false"/>.</returns>
        public static bool SumsAtMost(this IEnumerable<uint> values, uint target) => SumSatisfies(values, ComparisonKinds.LessOrEqual, target);
        /// <inheritdoc cref="SumSatisfiesDocTemplate"/>
        /// <summary>Determines whether the sum of a collection of values is strictly greater than the target.</summary>
        /// <returns><see langword="true"/> if the sum is strictly greater than (&gt;) the target, otherwise <see langword="false"/>.</returns>
        public static bool SumsOver(this IEnumerable<uint> values, uint target) => SumSatisfies(values, ComparisonKinds.Greater, target);
        /// <inheritdoc cref="SumSatisfiesDocTemplate"/>
        /// <summary>Determines whether the sum of a collection of values is at least equal to the target.</summary>
        /// <returns><see langword="true"/> if the sum is greater than or equal to (&gt;=) the target, otherwise <see langword="false"/>.</returns>
        public static bool SumsAtLeast(this IEnumerable<uint> values, uint target) => SumSatisfies(values, ComparisonKinds.GreaterOrEqual, target);

        /// <inheritdoc cref="SumSatisfiesDocTemplate"/>
        /// <summary>Determines whether the sum of a collection of values satisfies a comparison against the target value.</summary>
        /// <param name="comparisonKinds">The comparison kinds that must be satisfied.</param>
        /// <returns><see langword="true"/> if the sum satisfies the given comparison, otherwise <see langword="false"/>.</returns>
        public static bool SumSatisfies(this IEnumerable<uint> values, ComparisonKinds comparisonKinds, uint target)
        {
            if (comparisonKinds is ComparisonKinds.All)
                return true;

            if (comparisonKinds is ComparisonKinds.Equal or ComparisonKinds.Different)
                return comparisonKinds.Matches(values.Sum().GetComparisonResult(target));

            uint sum = 0;
            bool initialPredicateValidity = sum.SatisfiesComparison(target, comparisonKinds);
            foreach (uint value in values)
            {
                sum += value;
                bool currentPredicateValidity = sum.SatisfiesComparison(target, comparisonKinds);
                if (currentPredicateValidity != initialPredicateValidity)
                    return currentPredicateValidity;
            }

            return initialPredicateValidity;
        }

        /// <inheritdoc cref="SumsBelow(IEnumerable{uint}, uint)"/>
        public static bool SumsBelow(this IEnumerable<ulong> values, ulong target) => SumSatisfies(values, ComparisonKinds.Less, target);
        /// <inheritdoc cref="SumsAtMost(IEnumerable{uint}, uint)"/>
        public static bool SumsAtMost(this IEnumerable<ulong> values, ulong target) => SumSatisfies(values, ComparisonKinds.LessOrEqual, target);
        /// <inheritdoc cref="SumsOver(IEnumerable{uint}, uint)"/>
        public static bool SumsOver(this IEnumerable<ulong> values, ulong target) => SumSatisfies(values, ComparisonKinds.Greater, target);
        /// <inheritdoc cref="SumsAtLeast(IEnumerable{uint}, uint)"/>
        public static bool SumsAtLeast(this IEnumerable<ulong> values, ulong target) => SumSatisfies(values, ComparisonKinds.GreaterOrEqual, target);

        /// <inheritdoc cref="SumSatisfies(IEnumerable{uint}, ComparisonKinds, uint)"/>
        public static bool SumSatisfies(this IEnumerable<ulong> values, ComparisonKinds comparisonKinds, ulong target)
        {
            if (comparisonKinds is ComparisonKinds.All)
                return true;

            if (comparisonKinds is ComparisonKinds.Equal or ComparisonKinds.Different)
                return comparisonKinds.Matches(values.Sum().GetComparisonResult(target));

            ulong sum = 0;
            bool initialPredicateValidity = sum.SatisfiesComparison(target, comparisonKinds);
            foreach (ulong value in values)
            {
                sum += value;
                bool currentPredicateValidity = sum.SatisfiesComparison(target, comparisonKinds);
                if (currentPredicateValidity != initialPredicateValidity)
                    return currentPredicateValidity;
            }

            return initialPredicateValidity;
        }

        /// <summary>Enumerates a collection, but halts enumeration as soon as a value that satisfies the given predicate is found.</summary>
        /// <typeparam name="T">The type of the values the collection contains.</typeparam>
        /// <param name="source">The collection whose values to enumerate and evaluate through <paramref name="predicate"/>.</param>
        /// <param name="predicate">The predicate which will cause enumeration to halt upon being satisfied.</param>
        /// <param name="includeHaltingValue">Determines whether the final value that causes enumeration to halt should be retuned or not.</param>
        /// <returns>The first elements that do not satisfy <paramref name="predicate"/>, and, optionally, the first value that satisfies it.</returns>
        public static IEnumerable<T> CutAt<T>(this IEnumerable<T> source, Predicate<T> predicate, bool includeHaltingValue = false)
        {
            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    if (includeHaltingValue)
                        yield return item;

                    yield break;
                }

                yield return item;
            }
        }
    }
}