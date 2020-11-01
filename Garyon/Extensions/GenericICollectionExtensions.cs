using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions
{
    /// <summary>Contains extension functions for the <seealso cref="ICollection{T}"/> interface.</summary>
    public static class GenericICollectionExtensions
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

        /// <summary>Merges the collection's lists into a single list.</summary>
        /// <typeparam name="T">The type of the elements in each list of the collection.</typeparam>
        /// <param name="l">The collection of lists to merge into a list.</param>
        public static List<T> Merge<T>(this ICollection<List<T>> l)
        {
            var result = new List<T>();
            for (int i = 0; i < l.Count; i++)
                result.AddRange(l.ElementAt(i));
            return result;
        }
    }
}
