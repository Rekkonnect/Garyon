using System;
using System.Collections;
using System.Collections.Generic;

namespace Garyon.Objects.Enumerators;

public static class SingleValueEnumeratorExtensions
{
    /// <summary>
    /// Creates a new <seealso cref="SingleValueEnumerator{T}"/> instance
    /// for the given value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value which will be enumerated.</param>
    /// <returns>
    /// A <seealso cref="SingleValueEnumerator{T}"/> instance enumerating
    /// the given single value.
    /// </returns>
    public static SingleValueEnumerator<T> EnumerateSingle<T>(this T value)
    {
        return new(value);
    }
}

public class SingleValueEnumerator<T> : IEnumerator<T>
{
    private readonly T value;

    /// <summary>
    /// Gets the current state of the enumerator.
    /// </summary>
    public SingleValueEnumerationState State { get; private set; }

    public T Current
    {
        get
        {
            return State switch
            {
                SingleValueEnumerationState.Value => value,
                _ => throw new InvalidOperationException("The current state of the enumerator does not yield the single value."),
            };
        }
    }

    object IEnumerator.Current => Current;

    public SingleValueEnumerator(T singleValue)
    {
        value = singleValue;
        Reset();
    }

    public bool MoveNext()
    {
        State++;
        return State <= SingleValueEnumerationState.Value;
    }

    public void Reset()
    {
        State = SingleValueEnumerationState.Before;
    }

    void IDisposable.Dispose() { }
}
