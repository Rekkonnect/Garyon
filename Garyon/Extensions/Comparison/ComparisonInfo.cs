using System;
using System.Threading.Tasks;

namespace Garyon.Extensions.Comparison;

/// <summary>
/// Matches the result of a comparison along with its
/// <see cref="ComparisonSource{T}"/>. The result is an integer indicating the
/// relative order of the compared values, following the value semantics of the
/// result of <see cref="IComparable{T}.CompareTo(T)"/>.
/// </summary>
/// <typeparam name="T">
/// </typeparam>
/// <param name="Source">
/// The comparison source.
/// </param>
/// <param name="Result">
/// The result of the comparison.
/// </param>
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
    /// <typeparam name="TResult">
    /// The type of the selected value.
    /// </typeparam>
    /// <param name="selector">
    /// The value selector to apply to the compared instances.
    /// </param>
    /// <returns>
    /// A comparison info representing the result of the additional comparison.
    /// The returned value will be the same as this current instance if
    /// <see cref="AreDifferent"/> returns true, otherwise it will be the result
    /// of <see cref="ComparisonSource{T}.By{TResult}(Func{T, TResult})"/>
    /// called on the same source.
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

    /// <summary>
    /// Additionally compares the same values by another predicate asynchronously, without breaking fluent chaining.
    /// </summary>
    public AsyncComparisonInfo<T> ThenByAsync<TResult>(Func<T, Task<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        return new(ThenBy(selector));
    }

    /// <summary>
    /// Additionally compares the same values by another predicate asynchronously, without breaking fluent chaining.
    /// </summary>
    public AsyncComparisonInfo<T> ThenByAsync<TResult>(Func<T, ValueTask<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        return new(ThenBy(selector));
    }

    /// <summary>
    /// Additionally compares the same values by another predicate asynchronously.
    /// </summary>
    public ValueTask<ComparisonInfo<T>> ThenBy<TResult>(Func<T, Task<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        if (AreDifferent)
        {
            return new ValueTask<ComparisonInfo<T>>(this);
        }

        return Source.By(selector);
    }

    /// <summary>
    /// Additionally compares the same values by another predicate asynchronously.
    /// </summary>
    public ValueTask<ComparisonInfo<T>> ThenBy<TResult>(Func<T, ValueTask<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        if (AreDifferent)
        {
            return new ValueTask<ComparisonInfo<T>>(this);
        }

        return Source.By(selector);
    }

    /// <summary>
    /// Additionally compares the same values by another predicate in
    /// descending order.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the selected value.
    /// </typeparam>
    /// <param name="selector">
    /// The value selector to apply to the compared instances.
    /// </param>
    /// <returns>
    /// A comparison info representing the result of the additional comparison.
    /// The returned value will be the same as this current instance if
    /// <see cref="AreDifferent"/> returns true, otherwise it will be the result
    /// of <see cref="ComparisonSource{T}.ByDesc{TResult}(Func{T, TResult})"/>
    /// called on the same source.
    /// </returns>
    public ComparisonInfo<T> ThenByDesc<TResult>(Func<T, TResult> selector)
        where TResult : IComparable<TResult>
    {
        if (AreDifferent)
        {
            return this;
        }

        return Source.ByDesc(selector);
    }

    /// <summary>
    /// Additionally compares the same values by another predicate asynchronously in descending order,
    /// without breaking fluent chaining.
    /// </summary>
    public AsyncComparisonInfo<T> ThenByDescAsync<TResult>(Func<T, Task<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        return new(ThenByDesc(selector));
    }

    /// <summary>
    /// Additionally compares the same values by another predicate asynchronously in descending order,
    /// without breaking fluent chaining.
    /// </summary>
    public AsyncComparisonInfo<T> ThenByDescAsync<TResult>(Func<T, ValueTask<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        return new(ThenByDesc(selector));
    }

    /// <summary>
    /// Additionally compares the same values by another predicate asynchronously in descending order.
    /// </summary>
    public ValueTask<ComparisonInfo<T>> ThenByDesc<TResult>(Func<T, Task<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        if (AreDifferent)
        {
            return new ValueTask<ComparisonInfo<T>>(this);
        }

        return Source.ByDesc(selector);
    }

    /// <summary>
    /// Additionally compares the same values by another predicate asynchronously in descending order.
    /// </summary>
    public ValueTask<ComparisonInfo<T>> ThenByDesc<TResult>(Func<T, ValueTask<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        if (AreDifferent)
        {
            return new ValueTask<ComparisonInfo<T>>(this);
        }

        return Source.ByDesc(selector);
    }
}
