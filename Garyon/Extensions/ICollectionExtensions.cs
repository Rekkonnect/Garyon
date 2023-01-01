using System.Collections.Generic;

namespace Garyon.Extensions;

/// <summary>Contains extension functions for the <seealso cref="ICollection{T}"/> interface.</summary>
public static class ICollectionExtensions
{
    /// <summary>Adds a range of elements into an <seealso cref="ICollection{T}"/>.</summary>
    /// <typeparam name="T">The type of elements stored in the <seealso cref="ICollection{T}"/>.</typeparam>
    /// <param name="c">The <seealso cref="ICollection{T}"/> to add elements in.</param>
    /// <param name="elements">The elements to add into the collection.</param>
    public static void AddRange<T>(this ICollection<T> c, params T[] elements) => AddRange(c, (IEnumerable<T>)elements);
    /// <summary>Adds a range of elements into an <seealso cref="ICollection{T}"/>.</summary>
    /// <typeparam name="T">The type of elements stored in the <seealso cref="ICollection{T}"/>.</typeparam>
    /// <param name="c">The <seealso cref="ICollection{T}"/> to add elements in.</param>
    /// <param name="elements">The elements to add into the collection.</param>
    public static void AddRange<T>(this ICollection<T> c, IEnumerable<T> elements)
    {
        foreach (var e in elements)
            c.Add(e);
    }
}
