using Garyon.Objects;
using Garyon.Objects.Enumerators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Garyon.Extensions
{
    /// <summary>Contains extension functions for the <seealso cref="IAsyncEnumerable{T}"/> interface.</summary>
    public static class IAsyncEnumerableExtensions
    {
        #region Flattening
        /// <summary>Flattens an <seealso cref="IAsyncEnumerable{T}"/> of <seealso cref="IEnumerable{T}"/> into a single <seealso cref="IAsyncEnumerable{T}"/> asynchronously. The resulting elements are contained in the order they are enumerated.</summary>
        /// <typeparam name="T">The type of elements contained in the collections.</typeparam>
        /// <param name="source">The collection of collections.</param>
        /// <returns>The flattened <seealso cref="IAsyncEnumerable{T}"/>.</returns>
        public static async IAsyncEnumerable<T> FlattenAsync<T>(this IAsyncEnumerable<IEnumerable<T>> source)
        {
            await foreach (var e in source)
                foreach (var v in e)
                    yield return v;
        }
        /// <summary>Flattens an <seealso cref="IEnumerable{T}"/> of <seealso cref="IAsyncEnumerable{T}"/> into a single <seealso cref="IAsyncEnumerable{T}"/> asynchronously. The resulting elements are contained in the order they are enumerated.</summary>
        /// <typeparam name="T">The type of elements contained in the collections.</typeparam>
        /// <param name="source">The collection of collections.</param>
        /// <returns>The flattened <seealso cref="IAsyncEnumerable{T}"/>.</returns>
        public static async IAsyncEnumerable<T> FlattenAsync<T>(this IEnumerable<IAsyncEnumerable<T>> source)
        {
            foreach (var e in source)
                await foreach (var v in e)
                    yield return v;
        }
        /// <summary>Flattens an <seealso cref="IAsyncEnumerable{T}"/> of <seealso cref="IAsyncEnumerable{T}"/> into a single <seealso cref="IAsyncEnumerable{T}"/> asynchronously. The resulting elements are contained in the order they are enumerated.</summary>
        /// <typeparam name="T">The type of elements contained in the collections.</typeparam>
        /// <param name="source">The collection of collections.</param>
        /// <returns>The flattened <seealso cref="IAsyncEnumerable{T}"/>.</returns>
        public static async IAsyncEnumerable<T> FlattenAsync<T>(this IAsyncEnumerable<IAsyncEnumerable<T>> source)
        {
            await foreach (var e in source)
                await foreach (var v in e)
                    yield return v;
        }
        #endregion

        #region Enumeration
        /// <summary>Wraps the <seealso cref="IAsyncEnumerable{T}"/> into an <seealso cref="IndexedAsyncEnumerable{T}"/> for enumeration with index.</summary>
        /// <typeparam name="T">The type of the elements that are contained in the collection.</typeparam>
        /// <param name="enumerable">The <seealso cref="IAsyncEnumerable{T}"/> to enumerate with index.</param>
        /// <returns>The <seealso cref="IndexedAsyncEnumerable{T}"/> that wraps the <paramref name="enumerable"/> for indexed enumeration.</returns>
        public static IndexedAsyncEnumerable<T> WithIndex<T>(this IAsyncEnumerable<T> enumerable)
        {
            return new IndexedAsyncEnumerable<T>(enumerable);
        }
        #endregion

        #region For Each
        /// <summary>Performs an action on each of the elements contained in the collection.</summary>
        /// <typeparam name="T">The type of the elements that are contained in the collection.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="action">The action to perform on each of the elements.</param>
        public static async Task ForEachAsync<T>(this IAsyncEnumerable<T> source, Action<T> action)
        {
            await foreach (T e in source)
                action(e);
        }
        /// <summary>Performs an action on each of the elements contained in the collection.</summary>
        /// <typeparam name="T">The type of the elements that are contained in the collection.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="action">The action to perform on each of the elements.</param>
        public static async Task ForEachAsync<T>(this IAsyncEnumerable<T> source, IndexedEnumeratedElementAction<T> action)
        {
            int index = 0;
            await foreach (T e in source)
            {
                action(index, e);
                index++;
            }
        }
        #endregion

        /// <summary>Enumerates the entire <seealso cref="IAsyncEnumerable{T}"/> collection and creates a list with the enumerated elements in the order they were enumerated.</summary>
        /// <typeparam name="T">The type of the elements that are contained in the collection.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <returns>A <seealso cref="Task"/> wrapping the new <seealso cref="List{T}"/> containing all the elements that were enumerated from the <seealso cref="IAsyncEnumerable{T}"/> collection.</returns>
        public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source)
        {
            var result = new List<T>();
            await result.AddRangeAsync(source);
            return result;
        }
    }
}
