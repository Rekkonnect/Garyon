using Garyon.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Garyon.Objects.Enumerators;

/// <summary>
/// Represents either a single value, an enumerable, or no value at all.
/// </summary>
/// <typeparam name="T">The type of the stored value(s).</typeparam>
public class SingleOrEnumerable<T> : IEnumerable<T>
{
    private T single;
    private IEnumerable<T> enumerable;

    /// <summary>
    /// Gets the kind of value(s) stored in this instance.
    /// </summary>
    public SingleOrEnumerableKind Kind { get; private set; }

    /// <summary>
    /// Gets or sets the single value of this instance.
    /// </summary>
    /// <remarks>
    /// If the <seealso cref="Kind"/> property does not return
    /// <seealso cref="SingleOrEnumerableKind.Single"/>, an
    /// exception is thrown.
    /// <br/>
    /// Setting the single value automatically adjusts the kind
    /// of the stored value to
    /// <seealso cref="SingleOrEnumerableKind.Single"/>.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the current kind is not
    /// <seealso cref="SingleOrEnumerableKind.Single"/> when
    /// trying to <see langword="get"/> the single value.
    /// </exception>
    public T Single
    {
        get
        {
            if (Kind is not SingleOrEnumerableKind.Single)
            {
                throw new InvalidOperationException("The current object does not represent a single value.");
            }

            return single;
        }
        set
        {
            Assign(value);
        }
    }
    /// <summary>
    /// Gets or sets the enumerable values of this instance.
    /// </summary>
    /// <remarks>
    /// If the <seealso cref="Kind"/> property does not return
    /// <seealso cref="SingleOrEnumerableKind.Enumerable"/>, an
    /// exception is thrown.
    /// <br/>
    /// Setting the enumerable values automatically adjusts the kind
    /// of the stored value to
    /// <seealso cref="SingleOrEnumerableKind.Enumerable"/>.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the current kind is not
    /// <seealso cref="SingleOrEnumerableKind.Enumerable"/> when
    /// trying to <see langword="get"/> the enumerable values.
    /// </exception>
    public IEnumerable<T> Enumerable
    {
        get
        {
            if (Kind is not SingleOrEnumerableKind.Enumerable)
            {
                throw new InvalidOperationException("The current object does not represent an enumerable value.");
            }

            return enumerable;
        }
        set
        {
            Assign(value);
        }
    }

    public SingleOrEnumerable() { }

    public SingleOrEnumerable(T value)
    {
        Single = value;
    }
    public SingleOrEnumerable(IEnumerable<T> enumerable)
    {
        Enumerable = enumerable;
    }

    public void Assign(SingleOrEnumerable<T> other)
    {
        Kind = other.Kind;

        switch (other.Kind)
        {
            case SingleOrEnumerableKind.Single:
                single = other.single;
                break;

            case SingleOrEnumerableKind.Enumerable:
                enumerable = other.enumerable;
                break;
        }
    }
    public void Assign(T value)
    {
        Kind = SingleOrEnumerableKind.Single;
        single = value;
    }
    public void Assign(IEnumerable<T> value)
    {
        Kind = SingleOrEnumerableKind.Enumerable;
        enumerable = value;
    }

    /// <summary>
    /// Sets the kind of the stored values to
    /// <seealso cref="SingleOrEnumerableKind.None"/>.
    /// </summary>
    public void Reset()
    {
        Kind = SingleOrEnumerableKind.None;
    }

    /// <summary>
    /// Returns an enumerator for this instance.
    /// </summary>
    /// <returns>
    /// <list type="bullet">
    /// <item>
    /// <seealso cref="EmptyEnumerator{T}.Instance"/> for
    /// <seealso cref="SingleOrEnumerableKind.None"/>
    /// </item>
    /// <item>
    /// A <seealso cref="SingleValueEnumerator{T}"/> instance
    /// for <seealso cref="SingleOrEnumerableKind.Single"/>
    /// </item>
    /// <item>
    /// The enumerable values' enumerator instance for
    /// <seealso cref="SingleOrEnumerableKind.Enumerable"/>
    /// </item>
    /// </list></returns>
    public IEnumerator<T> GetEnumerator()
    {
        return Kind switch
        {
            SingleOrEnumerableKind.None => EmptyEnumerator<T>.Instance,
            SingleOrEnumerableKind.Single => single.EnumerateSingle(),
            SingleOrEnumerableKind.Enumerable => enumerable.GetEnumerator(),
            _ => throw new UnreachableException(),
        };
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
