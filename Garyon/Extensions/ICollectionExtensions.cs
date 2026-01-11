using System.Collections.Generic;

namespace Garyon.Extensions;

/// <summary>Contains extension functions for the <seealso cref="ICollection{T}"/> interface.</summary>
public static class ICollectionExtensions
{
    extension<T>(ICollection<T> collection)
    {
        /// <summary>Adds a range of elements into an <seealso cref="ICollection{T}"/>.</summary>
        /// <param name="elements">The elements to add into the collection.</param>
        public void AddRange(params T[] elements)
        {
            AddRange(collection, (IEnumerable<T>)elements);
        }

        /// <summary>Adds a range of elements into an <seealso cref="ICollection{T}"/>.</summary>
        /// <param name="elements">The elements to add into the collection.</param>
        public void AddRange(IEnumerable<T> elements)
        {
            foreach (var e in elements)
                collection.Add(e);
        }

        /// <summary>
        /// Clears the entire collection and sets its contents to the given range.
        /// </summary>
        /// <param name="items">The items to set to the collection.</param>
        public void ClearSetRange(IEnumerable<T> items)
        {
            collection.Clear();
            collection.AddRange(items);
        }

        /// <summary>
        /// Adds an item if it is not <see langword="null"/>.
        /// </summary>
        /// <param name="item">The item to add, if not <see langword="null"/>.</param>
        public void AddNotNull(T? item)
        {
            if (item is null)
                return;

            collection.Add(item);
        }
    }
}
