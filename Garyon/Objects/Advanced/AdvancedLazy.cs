using System;
using System.Threading.Tasks;

namespace Garyon.Objects.Advanced;

/// <summary>Provides a mechanism for a lazily initializable value.</summary>
/// <typeparam name="T">The type of the lazily initializable value.</typeparam>
public sealed class AdvancedLazy<T>
{
    private T? _value;
    private readonly Func<T> _factory;

    /// <summary>Determines whether the value has been created or not.</summary>
    public bool IsValueCreated { get; private set; }

    /// <summary>
    /// Gets the lazily initialized value or <see langword="default"/> if it is not initialized yet.
    /// </summary>
    public T? ValueOrDefault => _value;

    /// <summary>
    /// Gets the lazily initializable value.
    /// If the value has not been initialized yet, this property activates its initialization.
    /// </summary>
    public T Value
    {
        get
        {
            if (IsValueCreated)
                return _value!;

            IsValueCreated = true;
            return _value = _factory();
        }
    }

    /// <summary>Initializes a new instance of the <seealso cref="AdvancedLazy{T}"/> class from a given value.</summary>
    /// <param name="value">The value to initialize this instance with.</param>
    public AdvancedLazy(T value)
        : this(() => value)
    {
        IsValueCreated = true;
        _value = value;
    }
    /// <summary>Initializes a new instance of the <seealso cref="AdvancedLazy{T}"/> class from a factory method.</summary>
    /// <param name="factory">The factory method that will be used to lazily initialize the value.</param>
    public AdvancedLazy(Func<T> factory)
    {
        _factory = factory;
    }

    /// <summary>
    /// Gets the value asynchronously, executing the factory method if the value has not
    /// been created yet.
    /// </summary>
    public async Task<T> GetValueAsync()
    {
        if (IsValueCreated)
        {
            return _value!;
        }

        return await CalculateValueAsync();
    }

    private Task<T> CalculateValueAsync()
    {
        var value = _factory();
        _value = value;
        IsValueCreated = true;
        return Task.FromResult(value);
    }

    /// <summary>
    /// Clears the cached value. Upon the next call of the
    /// <seealso cref="Value"/> property, the value will be lazily initialized again.
    /// </summary>
    public void ClearValue()
    {
        IsValueCreated = false;
        _value = default;
    }
}
