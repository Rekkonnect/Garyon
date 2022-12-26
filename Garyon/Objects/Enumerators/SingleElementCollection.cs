using System;
using System.Collections;
using System.Collections.Generic;

namespace Garyon.Objects.Enumerators;

/// <summary>Represents a <seealso cref="IReadOnlyCollection{T}"/> that contains a single element.</summary>
/// <typeparam name="T">The type of the stored element.</typeparam>
public readonly struct SingleElementCollection<T> : IReadOnlyCollection<T?>, IEquatable<SingleElementCollection<T?>>
{
    private readonly T? element;

    /// <inheritdoc/>
    public int Count => 1;

    /// <summary>Initializes a new instance of the <seealso cref="SingleElementCollection{T}"/> struct, out of the single element that will be contained in the collection.</summary>
    /// <param name="singleElement">The single element that will be contained in the collection. It may be <see langword="null"/>.</param>
    public SingleElementCollection(T? singleElement)
    {
        element = singleElement;
    }
    /// <summary>Initializes a new instance of the <seealso cref="SingleElementCollection{T}"/> struct, out of another <see cref="SingleElementCollection{T}"/>.</summary>
    /// <param name="other">The other <seealso cref="SingleElementCollection{T}"/> out of which to initialize this new collection.</param>
    public SingleElementCollection(SingleElementCollection<T?> other)
    {
        element = other.element;
    }

    /// <summary>Constructs a new <seealso cref="SingleElementCollection{T}"/> that contains the provided element.</summary>
    /// <param name="newElement">The new element that will be contained.</param>
    /// <returns>A new <seealso cref="SingleElementCollection{T}"/> containing the provided element.</returns>
    public SingleElementCollection<T?> WithElement(T? newElement) => new(newElement);

    /// <inheritdoc/>
    public IEnumerator<T?> GetEnumerator() => element.EnumerateSingle();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc/>
    public bool Equals(SingleElementCollection<T?> other)
    {
        return Equals(element, other.element);
    }
    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj is SingleElementCollection<T?> other && Equals(other);
    }
    /// <summary>Gets the hash code of the current collection, which is determined by its only contained element.</summary>
    /// <returns>The hash code of its only contained element, if not <see langword="null"/>, otherwise <see langword="default"/>.</returns>
    public override int GetHashCode() => element?.GetHashCode() ?? default;

    public static bool operator ==(SingleElementCollection<T?> left, SingleElementCollection<T?> right)
    {
        return left.Equals(right);
    }
    public static bool operator !=(SingleElementCollection<T?> left, SingleElementCollection<T?> right)
    {
        return !(left == right);
    }
}
