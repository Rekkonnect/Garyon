using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    private int CompareSelf()
    {
        if (Left is IComparable<T> leftComparable)
        {
            return leftComparable.CompareTo(Right);
        }

        return Comparer<T>.Default.Compare(Left, Right);
    }

    /// <summary>
    /// Compares the two values as themselves.
    /// </summary>
    /// <returns>
    /// A <see cref="ComparisonInfo{T}"/> with the result.
    /// </returns>
    public ComparisonInfo<T> Self()
    {
        return new(this, CompareSelf());
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

    /// <summary>
    /// Compares the two values by a projected value asynchronously, without breaking fluent chaining.
    /// </summary>
    public AsyncComparisonInfo<T> ByAsync<TResult>(Func<T, Task<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        return new(By(selector));
    }

    /// <summary>
    /// Compares the two values by a projected value asynchronously, without breaking fluent chaining.
    /// </summary>
    public AsyncComparisonInfo<T> ByAsync<TResult>(Func<T, ValueTask<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        return new(By(selector));
    }

    /// <summary>
    /// Compares the two values by a projected value asynchronously.
    /// </summary>
    public ValueTask<ComparisonInfo<T>> By<TResult>(Func<T, Task<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        var leftTask = selector(Left);
        var rightTask = selector(Right);

        if (leftTask.Status == System.Threading.Tasks.TaskStatus.RanToCompletion && rightTask.Status == System.Threading.Tasks.TaskStatus.RanToCompletion)
        {
            var left = leftTask.Result;
            var right = rightTask.Result;
            var comparison = left.CompareTo(right);
            return new ValueTask<ComparisonInfo<T>>(new ComparisonInfo<T>(this, comparison));
        }

        return ByAwaitAsync(this, leftTask, rightTask);
    }

    /// <summary>
    /// Compares the two values by a projected value asynchronously.
    /// </summary>
    public ValueTask<ComparisonInfo<T>> By<TResult>(Func<T, ValueTask<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        var leftTask = selector(Left);
        var rightTask = selector(Right);

        if (leftTask.IsCompletedSuccessfully && rightTask.IsCompletedSuccessfully)
        {
            var left = leftTask.Result;
            var right = rightTask.Result;
            var comparison = left.CompareTo(right);
            return new ValueTask<ComparisonInfo<T>>(new ComparisonInfo<T>(this, comparison));
        }

        return ByAwaitAsync(this, leftTask, rightTask);
    }

    /// <summary>
    /// Compares the two values by a projected value in descending order.
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
    public ComparisonInfo<T> ByDesc<TResult>(Func<T, TResult> selector)
        where TResult : IComparable<TResult>
    {
        var left = selector(Left);
        var right = selector(Right);
        var comparison = right.CompareTo(left);
        return new(this, comparison);
    }

    /// <summary>
    /// Compares the two values by a projected value asynchronously in descending order, without breaking fluent chaining.
    /// </summary>
    public AsyncComparisonInfo<T> ByDescAsync<TResult>(Func<T, Task<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        return new(ByDesc(selector));
    }

    /// <summary>
    /// Compares the two values by a projected value asynchronously in descending order, without breaking fluent chaining.
    /// </summary>
    public AsyncComparisonInfo<T> ByDescAsync<TResult>(Func<T, ValueTask<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        return new(ByDesc(selector));
    }

    /// <summary>
    /// Compares the two values by a projected value asynchronously in descending order.
    /// </summary>
    public ValueTask<ComparisonInfo<T>> ByDesc<TResult>(Func<T, Task<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        var leftTask = selector(Left);
        var rightTask = selector(Right);

        if (leftTask.Status == TaskStatus.RanToCompletion
            && rightTask.Status == TaskStatus.RanToCompletion)
        {
            var left = leftTask.Result;
            var right = rightTask.Result;
            var comparison = right.CompareTo(left);
            return new ValueTask<ComparisonInfo<T>>(new ComparisonInfo<T>(this, comparison));
        }

        return ByDescAwaitAsync(this, leftTask, rightTask);
    }

    /// <summary>
    /// Compares the two values by a projected value asynchronously in descending order.
    /// </summary>
    public ValueTask<ComparisonInfo<T>> ByDesc<TResult>(Func<T, ValueTask<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        var leftTask = selector(Left);
        var rightTask = selector(Right);

        if (leftTask.IsCompletedSuccessfully && rightTask.IsCompletedSuccessfully)
        {
            var left = leftTask.Result;
            var right = rightTask.Result;
            var comparison = right.CompareTo(left);
            return new ValueTask<ComparisonInfo<T>>(new ComparisonInfo<T>(this, comparison));
        }

        return ByDescAwaitAsync(this, leftTask, rightTask);
    }

    private static async ValueTask<ComparisonInfo<T>> ByAwaitAsync<TResult>(
        ComparisonSource<T> source,
        Task<TResult> leftTask,
        Task<TResult> rightTask)
        where TResult : IComparable<TResult>
    {
        var left = await leftTask.NoContext;
        var right = await rightTask.NoContext;
        var comparison = left.CompareTo(right);
        return new(source, comparison);
    }

    private static async ValueTask<ComparisonInfo<T>> ByAwaitAsync<TResult>(
        ComparisonSource<T> source,
        ValueTask<TResult> leftTask,
        ValueTask<TResult> rightTask)
        where TResult : IComparable<TResult>
    {
        var left = await leftTask.NoContext;
        var right = await rightTask.NoContext;
        var comparison = left.CompareTo(right);
        return new(source, comparison);
    }

    private static async ValueTask<ComparisonInfo<T>> ByDescAwaitAsync<TResult>(
        ComparisonSource<T> source,
        Task<TResult> leftTask,
        Task<TResult> rightTask)
        where TResult : IComparable<TResult>
    {
        var left = await leftTask.NoContext;
        var right = await rightTask.NoContext;
        var comparison = right.CompareTo(left);
        return new(source, comparison);
    }

    private static async ValueTask<ComparisonInfo<T>> ByDescAwaitAsync<TResult>(
        ComparisonSource<T> source,
        ValueTask<TResult> leftTask,
        ValueTask<TResult> rightTask)
        where TResult : IComparable<TResult>
    {
        var left = await leftTask.NoContext;
        var right = await rightTask.NoContext;
        var comparison = right.CompareTo(left);
        return new(source, comparison);
    }
}
