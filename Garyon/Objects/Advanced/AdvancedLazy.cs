using System;

namespace Garyon.Objects.Advanced;

/// <summary>Provides a mecahnism for a lazily initializable value.</summary>
/// <typeparam name="T">The type of the lazily initializable value.</typeparam>
public sealed class AdvancedLazy<T>
{
    private T value;
    private readonly Func<T> factory;

    /// <summary>Determines whether the value has been created or not.</summary>
    public bool IsValueCreated { get; private set; }

    /// <summary>Gets the lazily initializable value. If the value has not been initialized yet, this property activates its initialization.</summary>
    public T Value
    {
        get
        {
            if (IsValueCreated)
                return value;

            IsValueCreated = true;
            return value = factory();
        }
    }

    /// <summary>Initializes a new instance of the <seealso cref="AdvancedLazy{T}"/> class from a given value.</summary>
    /// <param name="value">The value to initialize this instance with.</param>
    public AdvancedLazy(T value)
        : this(() => value)
    {
        IsValueCreated = true;
        this.value = value;
    }
    /// <summary>Initializes a new instance of the <seealso cref="AdvancedLazy{T}"/> class from a value creator method.</summary>
    /// <param name="valueCreator">The value creator method that will be used to lazily initialize the value.</param>
    public AdvancedLazy(Func<T> valueCreator)
    {
        factory = valueCreator;
    }

    /// <summary>Destroys the cached value. Upon the next call of the <seealso cref="Value"/> property, the value will be lazily initialized again.</summary>
    public void DestroyValue()
    {
        IsValueCreated = false;
        value = default;
    }
}
