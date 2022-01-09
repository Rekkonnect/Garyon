using System;
using System.Collections.Generic;

namespace Garyon.Extensions
{
    /// <summary>Contains functions about sorting arrays.</summary>
    public static class ArraySorting
    {
        /// <summary>Sorts the array by a custom <seealso cref="IComparer{T}"/>. The original instance is affected.</summary>
        /// <typeparam name="T">The type of the values stored in the array.</typeparam>
        /// <param name="array">The array that will be sorted.</param>
        /// <param name="comparer">The comparer that will be used when comparing the values during sorting.</param>
        /// <returns>The original array instance.</returns>
        public static T[] SortBy<T>(this T[] array, IComparer<T> comparer)
        {
            Array.Sort(array, comparer);
            return array;
        }
        /// <summary>Sorts the array by a custom <seealso cref="Comparison{T}"/>. The original instance is affected.</summary>
        /// <typeparam name="T">The type of the values stored in the array.</typeparam>
        /// <param name="array">The array that will be sorted.</param>
        /// <param name="comparison">The comparison function that will be used when comparing the values during sorting.</param>
        /// <returns>The original array instance.</returns>
        public static T[] SortBy<T>(this T[] array, Comparison<T> comparison)
        {
            Array.Sort(array, comparison);
            return array;
        }
    }
}