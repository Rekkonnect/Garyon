using System;

namespace Garyon.Extensions.Comparison;

/// <summary>
/// Matches the result of a comparison along with its <see cref="ComparisonSource{T}"/>.
/// The result is an integer indicating the relative order of the compared values,
/// following the value semantics of the result of <see cref="IComparable{T}.CompareTo(T)"/>.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="Source">The comparison source.</param>
/// <param name="Result">The result of the comparison.</param>
public readonly record struct ComparisonInfo<T>(
    ComparisonSource<T> Source, int Result)
{
    /// <summary>
    /// Determines whether the compared values from the source are equal.
    /// </summary>
    public bool AreEqual => Result is 0;
    /// <summary>
    /// Determines whether the compared values from the source are different.
    /// </summary>
    public bool AreDifferent => Result is not 0;

    /// <summary>
    /// Additionally compares the same values by another predicate.
    /// </summary>
    /// <typeparam name="TResult">The type of the selected value.</typeparam>
    /// <param name="selector">The value selector to apply to the compared instances.</param>
    /// <returns>
    /// A comparison info representing the result of the additional comparison.
    /// The returned value will be the same as this current instance if
    /// <see cref="AreDifferent"/> returns true, otherwise it will be the result of
    /// <see cref="ComparisonSource{T}.By{TResult}(Func{T, TResult})"/> called on the same source.
    /// </returns>
    public ComparisonInfo<T> ThenBy<TResult>(Func<T, TResult> selector)
        where TResult : IComparable<TResult>
    {
        if (AreDifferent)
        {
            return this;
        }

        return Source.By(selector);
    }
}
