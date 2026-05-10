using System;
using System.Threading.Tasks;

namespace Garyon.Objects.Advanced;

/// <summary>
/// Provides a mechanism for a lazily initializable value that is created
/// asynchronously.
/// </summary>
/// <typeparam name="T">
/// The type of the lazily initializable value.
/// </typeparam>
/// <remarks>
/// This type caches the <see cref="Task{TResult}"/> returned by the factory. The
/// factory is invoked at most once unless <see cref="ClearValue"/> is called.
/// </remarks>
public sealed class AsyncLazy<T>
{
    private Task<T>? _valueTask;
    private readonly Func<Task<T>> _factory;
    private readonly object _syncLock = new();

    /// <summary>
    /// Determines whether the value has been created (i.e. the factory was
    /// invoked) or not.
    /// </summary>
    public bool IsValueCreated { get; private set; }

    /// <summary>
    /// Gets the cached <see cref="Task{TResult}"/> if the value has already been
    /// requested, or <see langword="null"/> otherwise.
    /// </summary>
    public Task<T>? ValueTaskOrDefault => _valueTask;

    /// <summary>
    /// Initializes a new instance of the <seealso cref="AsyncLazy{T}"/> class
    /// from a given value.
    /// </summary>
    /// <param name="value">
    /// The value to initialize this instance with.
    /// </param>
    public AsyncLazy(T value)
        : this(() => Task.FromResult(value))
    {
        IsValueCreated = true;
        _valueTask = Task.FromResult(value);
    }

    /// <summary>
    /// Initializes a new instance of the <seealso cref="AsyncLazy{T}"/> class
    /// from an async factory method.
    /// </summary>
    /// <param name="factory">
    /// The factory method that will be used to lazily initialize the value.
    /// </param>
    public AsyncLazy(Func<Task<T>> factory)
    {
        _factory = factory;
    }

    /// <summary>
    /// Gets the lazily initializable value asynchronously. If the value has not
    /// been created yet, this method activates its initialization.
    /// </summary>
    public Task<T> GetValueAsync()
    {
        lock (_syncLock)
        {
            if (_valueTask is not null)
                return _valueTask;

            var task = _factory();
            _valueTask = task;
            IsValueCreated = true;
            return task;
        }
    }

    /// <summary>
    /// Clears the cached value. Upon the next call of
    /// <seealso cref="GetValueAsync"/>, the value will be lazily initialized
    /// again.
    /// </summary>
    public void ClearValue()
    {
        lock (_syncLock)
        {
            IsValueCreated = false;
            _valueTask = null;
        }
    }
}
