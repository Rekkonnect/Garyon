using System;
using System.Collections;
using System.Collections.Generic;

namespace Garyon.Objects.Enumerators;

/// <summary>
/// Denotes an empty enumerator. During its enumeration, no values
/// are yielded.
/// </summary>
/// <typeparam name="T">The type of values that are being (not) enumerated.</typeparam>
public class EmptyEnumerator<T> : IEnumerator<T>
{
    /// <summary>
    /// Gets the single instance of the empty enumerator for the given type.
    /// </summary>
    public static EmptyEnumerator<T> Instance { get; } = new EmptyEnumerator<T>();

    private EmptyEnumerator() { }

    /// <summary>
    /// Always throws an exception, as this enumerator should not yield
    /// any values.
    /// </summary>
    public T Current => throw new InvalidOperationException("The empty enumerator does not yield any values.");
    object IEnumerator.Current => Current;

    void IDisposable.Dispose()
    {
    }

    /// <summary>
    /// Always returns false, as no value is being enumerated.
    /// </summary>
    /// <returns><see langword="false"/>.</returns>
    public bool MoveNext()
    {
        return false;
    }

    void IEnumerator.Reset()
    {
    }
}
