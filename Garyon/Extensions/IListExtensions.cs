using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garyon.Extensions;

/// <summary>Provides extension methods for the <seealso cref="IList{T}"/> interface.</summary>
public static class IListExtensions
{
#if HAS_SLICES
    /// <summary>Inserts an element to the list at the specified index.</summary>
    /// <typeparam name="T">The type of the elements contained in the <seealso cref="IList{T}"/>.</typeparam>
    /// <param name="list">The list to add an element to.</param>
    /// <param name="index">The index that the element will be located at after insertion.</param>
    /// <param name="element">The element to insert to the list.</param>
    public static void Insert<T>(this IList<T> list, Index index, T element)
    {
        list.Insert(index.GetOffset(list.Count), element);
    }
    /// <summary>Removes an element from the list at the specified index.</summary>
    /// <typeparam name="T">The type of the elements contained in the <seealso cref="IList{T}"/>.</typeparam>
    /// <param name="list">The list to remove an element from.</param>
    /// <param name="index">The index of the element in the list to remove.</param>
    public static void RemoveAt<T>(this IList<T> list, Index index)
    {
        list.RemoveAt(index.GetOffset(list.Count));
    }
#endif
    /// <summary>Removes the last element from the provided list.</summary>
    /// <typeparam name="T">The type of the elements contained in the <seealso cref="IList{T}"/>.</typeparam>
    /// <param name="list">The list to remove the last element from.</param>
    public static void RemoveLast<T>(this IList<T> list) => list.RemoveAt(list.Count - 1);

    /// <summary>Swaps two elements in the <seealso cref="IList{T}"/>.</summary>
    /// <typeparam name="T">The type of the elements contained in the <seealso cref="IList{T}"/>.</typeparam>
    /// <param name="list">The <seealso cref="IList{T}"/> within which to swap two elements.</param>
    /// <param name="a">The index of the first element to swap.</param>
    /// <param name="b">The index of the second element to swap.</param>
    /// <returns>The instance of the <seealso cref="IList{T}"/> in which two elements were swapped.</returns>
    public static IList<T> Swap<T>(this IList<T> list, int a, int b)
    {
        (list[b], list[a]) = (list[a], list[b]);
        return list;
    }
#if HAS_SLICES
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
#endif

#if HAS_ASYNC_ENUMERABLE
    /// <summary>Adds a range of elements from an <seealso cref="IAsyncEnumerable{T}"/> into the <seealso cref="IList{T}"/>.</summary>
    /// <typeparam name="T">The type of the elements contained in the <seealso cref="IList{T}"/>.</typeparam>
    /// <param name="list">The <seealso cref="IList{T}"/> to which to add the elements.</param>
    /// <param name="collection">The <seealso cref="IAsyncEnumerable{T}"/> whose elements to add to the provided list.</param>
    /// <returns>A <seealso cref="Task"/> representing the operation of adding all the elements.</returns>
    public static async Task AddRangeAsync<T>(this IList<T> list, IAsyncEnumerable<T> collection)
    {
        await foreach (var element in collection)
            list.Add(element);
    }
    
    /// <inheritdoc cref="AddRangeAsync{T}(IList{T}, IAsyncEnumerable{T})"/>
    public static async Task AddRangeAsync<T>(this IList<T> list, IAsyncEnumerable<IEnumerable<T>> collection)
    {
        await foreach (var enumerable in collection)
            list.AddRange(enumerable);
    }
#endif

    /// <summary>
    /// Removes an element from the list at the specified index
    /// and decrements the provided index by reference.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the list.</typeparam>
    /// <param name="list">The list from which to remove the element.</param>
    /// <param name="index">
    /// A reference to the index that will be decremented after removing
    /// the element from the list.
    /// </param>
    public static void RemoveAtDecrement<T>(this IList<T> list, ref int index)
    {
        list.RemoveAt(index);
        index--;
    }

    /// <summary>
    /// Pops the last element from the list and returns it.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the list.</typeparam>
    /// <param name="list">The list from which to pop the element.</param>
    /// <returns>The popped element.</returns>
    public static T Pop<T>(this IList<T> list)
    {
        var last = list.Last();
        list.RemoveLast();
        return last;
    }

    public static T? AtIndexOrDefaultReadOnly<T>(
        this IReadOnlyList<T> source, int index)
    {
        TryGetAtIndexReadOnly(source, index, out var value);
        return value;
    }

    public static T? AtIndexOrDefault<T>(
        this IList<T> source, int index)
    {
        TryGetAtIndex(source, index, out var value);
        return value;
    }

    public static bool TryGetAtIndexReadOnly<T>(
        this IReadOnlyList<T> source, int index, out T? value)
    {
        if (source.Count > index)
        {
            value = source[index];
            return true;
        }

        value = default;
        return false;
    }

    public static bool TryGetAtIndex<T>(
        this IList<T> source, int index, out T? value)
    {
        if (source.Count > index)
        {
            value = source[index];
            return true;
        }

        value = default;
        return false;
    }

    public static T? SingleOrDefaultReadOnly<T>(this IReadOnlyList<T> source)
    {
        if (source.Count is 1)
        {
            return source[0];
        }

        return default;
    }

    public static T? SingleOrDefault<T>(this IList<T> source)
    {
        if (source.Count is 1)
        {
            return source[0];
        }

        return default;
    }

    #region IList
    /// <summary>
    /// Clears the entire list and sets its contents to the given range.
    /// </summary>
    /// <param name="list">
    /// The <seealso cref="IList"/> to clear and whose items to set.
    /// </param>
    /// <param name="items">The items to set to the list.</param>
    public static void ClearSetRange(this IList list, IEnumerable items)
    {
        list.Clear();
        list.Add(items);
    }

    /// <summary>
    /// Adds a range of items to the given list.
    /// </summary>
    /// <param name="list">
    /// The <seealso cref="IList"/> on which to add the items.
    /// </param>
    /// <param name="items">The items to add to the list.</param>
    public static void AddRange(this IList list, IEnumerable items)
    {
        foreach (var item in items)
        {
            list.Add(item);
        }
    }
    #endregion
}
