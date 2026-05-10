using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Garyon.Extensions.Comparison;

/// <summary>
/// Represents an asynchronously composed comparison chain that can continue
/// mixing synchronous and asynchronous steps without breaking the fluent API.
/// </summary>
/// <typeparam name="T">
/// The type of values being compared.
/// </typeparam>
public readonly struct AsyncComparisonInfo<T>
{
    private readonly Task<ComparisonInfo<T>> _comparisonTask;

    internal AsyncComparisonInfo(ValueTask<ComparisonInfo<T>> comparisonTask)
    {
        _comparisonTask = comparisonTask.AsTask();
    }

    internal AsyncComparisonInfo(Task<ComparisonInfo<T>> comparisonTask)
    {
        _comparisonTask = comparisonTask;
    }

    /// <summary>
    /// Returns the underlying task-like object producing the composed comparison info.
    /// </summary>
    public Task<ComparisonInfo<T>> AsComparisonInfoAsync()
    {
        return _comparisonTask;
    }

    /// <summary>
    /// Continues the chain with a synchronous selector.
    /// </summary>
    public AsyncComparisonInfo<T> ThenBy<TResult>(Func<T, TResult> selector)
        where TResult : IComparable<TResult>
    {
        return new(ThenByAwaitAsync(_comparisonTask, selector));
    }

    /// <summary>
    /// Continues the chain with an asynchronous selector returning a <see cref="Task{TResult}"/>.
    /// </summary>
    public AsyncComparisonInfo<T> ThenByAsync<TResult>(Func<T, Task<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        return new(ThenByAsyncAwaitAsync(_comparisonTask, selector));
    }

    /// <summary>
    /// Continues the chain with an asynchronous selector returning a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    public AsyncComparisonInfo<T> ThenByAsync<TResult>(Func<T, ValueTask<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        return new(ThenByAsyncAwaitAsync(_comparisonTask, selector));
    }

    /// <summary>
    /// Continues the chain with a synchronous selector in descending order.
    /// </summary>
    public AsyncComparisonInfo<T> ThenByDesc<TResult>(Func<T, TResult> selector)
        where TResult : IComparable<TResult>
    {
        return new(ThenByDescAwaitAsync(_comparisonTask, selector));
    }

    /// <summary>
    /// Continues the chain with an asynchronous selector in descending order returning a <see cref="Task{TResult}"/>.
    /// </summary>
    public AsyncComparisonInfo<T> ThenByDescAsync<TResult>(Func<T, Task<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        return new(ThenByDescAsyncAwaitAsync(_comparisonTask, selector));
    }

    /// <summary>
    /// Continues the chain with an asynchronous selector in descending order returning a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    public AsyncComparisonInfo<T> ThenByDescAsync<TResult>(Func<T, ValueTask<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        return new(ThenByDescAsyncAwaitAsync(_comparisonTask, selector));
    }

    /// <summary>
    /// Awaits the chain and returns the comparison result.
    /// </summary>
    public Task<int> ResultAsync()
    {
        return GetResultAwaitAsync(_comparisonTask);
    }

    /// <summary>
    /// Makes the chain awaitable, yielding the comparison result.
    /// </summary>
    public TaskAwaiter<int> GetAwaiter()
    {
        return ResultAsync().GetAwaiter();
    }

    private static async Task<int> GetResultAwaitAsync(Task<ComparisonInfo<T>> previous)
    {
        var info = await previous.NoContext;
        return info.Result;
    }

    private static async Task<ComparisonInfo<T>> ThenByAwaitAsync<TResult>(
        Task<ComparisonInfo<T>> previous,
        Func<T, TResult> selector)
        where TResult : IComparable<TResult>
    {
        var info = await previous.NoContext;
        return info.ThenBy(selector);
    }

    private static async Task<ComparisonInfo<T>> ThenByAsyncAwaitAsync<TResult>(
        Task<ComparisonInfo<T>> previous,
        Func<T, Task<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        var info = await previous.NoContext;
        return await info.ThenBy(selector).NoContext;
    }

    private static async Task<ComparisonInfo<T>> ThenByAsyncAwaitAsync<TResult>(
        Task<ComparisonInfo<T>> previous,
        Func<T, ValueTask<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        var info = await previous.NoContext;
        return await info.ThenBy(selector).NoContext;
    }

    private static async Task<ComparisonInfo<T>> ThenByDescAwaitAsync<TResult>(
        Task<ComparisonInfo<T>> previous,
        Func<T, TResult> selector)
        where TResult : IComparable<TResult>
    {
        var info = await previous.NoContext;
        return info.ThenByDesc(selector);
    }

    private static async Task<ComparisonInfo<T>> ThenByDescAsyncAwaitAsync<TResult>(
        Task<ComparisonInfo<T>> previous,
        Func<T, Task<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        var info = await previous.NoContext;
        return await info.ThenByDesc(selector).NoContext;
    }

    private static async Task<ComparisonInfo<T>> ThenByDescAsyncAwaitAsync<TResult>(
        Task<ComparisonInfo<T>> previous,
        Func<T, ValueTask<TResult>> selector)
        where TResult : IComparable<TResult>
    {
        var info = await previous.NoContext;
        return await info.ThenByDesc(selector).NoContext;
    }
}
