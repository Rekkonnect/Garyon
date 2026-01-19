using System;
using System.Collections.Generic;

namespace Garyon.Extensions.Comparison;

/// <summary>
/// Defines the comparison source of two values of the same type.
/// </summary>
/// <typeparam name="T">
/// The type of values being compared.
/// </typeparam>
/// <param name="Left">
/// The left value in the comparison.
/// </param>
/// <param name="Right">
/// The right value in the comparison.
/// </param>
public readonly record struct ComparisonSource<T>(T Left, T Right)
{
    /// <summary>
    /// Compares the two values as themselves.
    /// </summary>
    /// <returns>
    /// A <see cref="ComparisonInfo{T}"/> with the result.
    /// </returns>
    public ComparisonInfo<T> Self()
    {
        int comparison;
        if (Left is IComparable<T> leftComparable)
        {
            comparison = leftComparable.CompareTo(Right);
        }
        else
        {
            comparison = Comparer<T>.Default.Compare(Left, Right);
        }
        return new(this, comparison);
    }

    /// <summary>
    /// Compares the two values by a projected value that implements
    /// <see cref="IComparable{T}"/>.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the projected value that will be compared.
    /// </typeparam>
    /// <param name="selector">
    /// The selector of the compared values.
    /// </param>
    /// <returns>
    /// A <see cref="ComparisonInfo{T}"/> with the result.
    /// </returns>
    public ComparisonInfo<T> By<TResult>(Func<T, TResult> selector)
        where TResult : IComparable<TResult>
    {
        var left = selector(Left);
        var right = selector(Right);
        var comparison = left.CompareTo(right);
        return new(this, comparison);
    }
}
