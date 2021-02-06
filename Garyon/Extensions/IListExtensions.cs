using System;
using System.Collections.Generic;

namespace Garyon.Extensions
{
    /// <summary>Provides extension methods for the <seealso cref="IList{T}"/> interface.</summary>
    public static class IListExtensions
    {
        /// <summary>Inserts an element to the list at the specified index.</summary>
        /// <typeparam name="T">The type of the elements that are contained in the collection.</typeparam>
        /// <param name="list">The list to add an element to.</param>
        /// <param name="index">The index that the element will be located at after insertion.</param>
        /// <param name="element">The element to insert to the list.</param>
        public static void Insert<T>(this IList<T> list, Index index, T element)
        {
            list.Insert(index.GetOffset(list.Count), element);
        }
        /// <summary>Removes an element from the list at the specified index.</summary>
        /// <typeparam name="T">The type of the elements that are contained in the collection.</typeparam>
        /// <param name="list">The list to remove an element from.</param>
        /// <param name="index">The index of the element in the list to remove.</param>
        public static void RemoveAt<T>(this IList<T> list, Index index)
        {
            list.RemoveAt(index.GetOffset(list.Count));
        }

        /// <summary>Swaps two elements in the <seealso cref="IList{T}"/>.</summary>
        /// <typeparam name="T">The type of the elements contained in the <seealso cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <seealso cref="IList{T}"/> within which to swap two elements.</param>
        /// <param name="a">The index of the first element to swap.</param>
        /// <param name="b">The index of the second element to swap.</param>
        /// <returns>The instance of the <seealso cref="IList{T}"/> in which two elements were swapped.</returns>
        public static IList<T> Swap<T>(this IList<T> list, int a, int b)
        {
            T t = list[a];
            list[a] = list[b];
            list[b] = t;
            return list;
        }
        /// <summary>Swaps two elements in the <seealso cref="IList{T}"/>.</summary>
        /// <typeparam name="T">The type of the elements contained in the <seealso cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <seealso cref="IList{T}"/> within which to swap two elements.</param>
        /// <param name="a">The index of the first element to swap.</param>
        /// <param name="b">The index of the second element to swap.</param>
        /// <returns>The instance of the <seealso cref="IList{T}"/> in which two elements were swapped.</returns>
        public static IList<T> Swap<T>(this IList<T> list, Index a, Index b)
        {
            int count = list.Count;
            return list.Swap(a.GetOffset(count), b.GetOffset(count));
        }
    }
}
