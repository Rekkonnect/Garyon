using System.Collections.Generic;

namespace Garyon.Extensions;

/// <summary>Contains extension functions for the <seealso cref="ICollection{T}"/> interface.</summary>
public static class ICollectionExtensions
{
    /// <summary>Adds a range of elements into an <seealso cref="ICollection{T}"/>.</summary>
    /// <typeparam name="T">The type of elements stored in the <seealso cref="ICollection{T}"/>.</typeparam>
    /// <param name="c">The <seealso cref="ICollection{T}"/> to add elements in.</param>
    /// <param name="elements">The elements to add into the collection.</param>
    public static void AddRange<T>(this ICollection<T> c, params T[] elements)
    {
        AddRange(c, (IEnumerable<T>)elements);
    }

    /// <summary>Adds a range of elements into an <seealso cref="ICollection{T}"/>.</summary>
    /// <typeparam name="T">The type of elements stored in the <seealso cref="ICollection{T}"/>.</typeparam>
    /// <param name="c">The <seealso cref="ICollection{T}"/> to add elements in.</param>
    /// <param name="elements">The elements to add into the collection.</param>
    public static void AddRange<T>(this ICollection<T> c, IEnumerable<T> elements)
    {
        foreach (var e in elements)
            c.Add(e);
    }

    /// <summary>
    /// Clears the entire collection and sets its contents to the given range.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements stored in the <seealso cref="ICollection{T}"/>.
    /// </typeparam>
    /// <param name="collection">
    /// The <seealso cref="ICollection{T}"/> to clear and whose items to set.
    /// </param>
    /// <param name="items">The items to set to the collection.</param>
    public static void ClearSetRange<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        collection.Clear();
        collection.AddRange(items);
    }

    /// <summary>
    /// Adds an item if it is not <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the items contained in the <see cref="ICollection{T}"/>.
    /// </typeparam>
    /// <param name="collection">The collection on which to add the item.</param>
    /// <param name="item">The item to add, if not <see langword="null"/>.</param>
    public static void AddNotNull<T>(this ICollection<T> collection, T? item)
    {
        if (item is null)
            return;

        collection.Add(item);
    }
}
