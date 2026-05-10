using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Garyon.Extensions.ArrayExtensions;

/// <summary>
/// Contains functions about sorting arrays.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ArraySorting
{
    /// <summary>
    /// Sorts the array, affecting the original array. Returns the instance of
    /// the original array.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the array elements.
    /// </typeparam>
    /// <param name="array">
    /// The original array to sort.
    /// </param>
    public static T[] Sort<T>(this T[] array)
    {
        Array.Sort(array);
        return array;
    }

    /// <summary>
    /// Sorts the array by a custom <seealso cref="IComparer{T}"/>. The original
    /// instance is affected.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the values stored in the array.
    /// </typeparam>
    /// <param name="array">
    /// The array that will be sorted.
    /// </param>
    /// <param name="comparer">
    /// The comparer that will be used when comparing the values during sorting.
    /// </param>
    /// <returns>
    /// The original array instance.
    /// </returns>
    public static T[] SortBy<T>(this T[] array, IComparer<T> comparer)
    {
        Array.Sort(array, comparer);
        return array;
    }

    /// <summary>
    /// Sorts the array by a custom <seealso cref="Comparison{T}"/>. The
    /// original instance is affected.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the values stored in the array.
    /// </typeparam>
    /// <param name="array">
    /// The array that will be sorted.
    /// </param>
    /// <param name="comparison">
    /// The comparison function that will be used when comparing the values
    /// during sorting.
    /// </param>
    /// <returns>
    /// The original array instance.
    /// </returns>
    public static T[] SortBy<T>(this T[] array, Comparison<T> comparison)
    {
        Array.Sort(array, comparison);
        return array;
    }
}