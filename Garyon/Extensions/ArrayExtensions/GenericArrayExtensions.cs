using Garyon.Exceptions;
using Garyon.Functions.Arrays;
using System;

namespace Garyon.Extensions.ArrayExtensions;

/// <summary>
/// Provides generic extension methods for arrays.
/// </summary>
public static class GenericArrayExtensions
{
    /// <summary>
    /// Copies the original array and returns a new array which contains the
    /// same elements at the same indices.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the array elements.
    /// </typeparam>
    /// <param name="array">
    /// The original array to copy.
    /// </param>
    public static T[] CopyArray<T>(this T[] array)
    {
        T[] result = new T[array.Length];
        for (int i = 0; i < array.Length; i++)
            result[i] = array[i];
        return result;
    }

    /// <summary>
    /// Moves an array element at a specified index to another. This affects the
    /// original array and returns its instance.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the array elements.
    /// </typeparam>
    /// <param name="array">
    /// The original array whose element to move.
    /// </param>
    /// <param name="from">
    /// The old index of the element to move.
    /// </param>
    /// <param name="to">
    /// The new index of the element to move.
    /// </param>
    public static T[] MoveElement<T>(this T[] array, int from, int to)
    {
        ValidateIndex(array, from, false);
        ValidateIndex(array, to, false);

        T moved = array[from];
        for (int i = from; i < to; i++)
            array[i] = array[i + 1];
        for (int i = from; i > to; i--)
            array[i] = array[i - 1];
        array[to] = moved;
        return array;
    }
    /// <summary>
    /// Moves an array element at a specified index to the end of the array.
    /// This affects the original array and returns its instance.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the array elements.
    /// </typeparam>
    /// <param name="array">
    /// The original array whose element to move.
    /// </param>
    /// <param name="from">
    /// The old index of the element to move.
    /// </param>
    public static T[] MoveElementToEnd<T>(this T[] array, int from) => array.MoveElement(from, array.Length - 1);
    /// <summary>
    /// Moves an array element at a specified index to the start of the array.
    /// This affects the original array and returns its instance.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the array elements.
    /// </typeparam>
    /// <param name="array">
    /// The original array whose element to move.
    /// </param>
    /// <param name="from">
    /// The old index of the element to move.
    /// </param>
    public static T[] MoveElementToStart<T>(this T[] array, int from) => array.MoveElement(from, 0);

    /// <summary>
    /// Reverses the elements of the array. Returns the instance of the original
    /// array.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the array elements.
    /// </typeparam>
    /// <param name="array">
    /// The original array to reverse.
    /// </param>
    public static T[] Reverse<T>(this T[] array)
    {
        for (int i = 0; i < array.Length / 2; i++)
            array.Swap(i, array.Length - 1 - i);
        return array;
    }
    /// <summary>
    /// Swaps two elements in the array. Returns the instance of the original
    /// array.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the array elements.
    /// </typeparam>
    /// <param name="array">
    /// The array whose elements to swap.
    /// </param>
    /// <param name="a">
    /// The index of the first element to swap.
    /// </param>
    /// <param name="b">
    /// The index of the second element to swap.
    /// </param>
    public static T[] Swap<T>(this T[] array, int a, int b)
    {
        ValidateIndex(array, a, false);
        ValidateIndex(array, b, false);

        T t = array[a];
        array[a] = array[b];
        array[b] = t;
        return array;
    }

    /// <summary>
    /// Creates an <see cref="ArraySegment{T}"/> that reflects the entire
    /// array's range.
    /// </summary>
    public static ArraySegment<T> Segment<T>(this T[] array)
    {
        return new(array);
    }

    /// <summary>
    /// Creates an <see cref="ArraySegment{T}"/> that reflects a subset of the
    /// array's range.
    /// </summary>
    public static ArraySegment<T> Segment<T>(this T[] array, int start, int count)
    {
        return new(array, start, count);
    }

    /// <summary>
    /// Creates an <see cref="ArraySegment{T}"/> that reflects a subset of the
    /// array's range.
    /// </summary>
    /// <param name="array">
    /// The array whose segment to get.
    /// </param>
    /// <param name="start">
    /// The starting index within the array, inclusive.
    /// </param>
    /// <param name="end">
    /// The end index of the array, exclusive.
    /// </param>
    public static ArraySegment<T> SegmentFromBounds<T>(this T[] array, int start, int end)
    {
        int count = end - start;
        return new(array, start, count);
    }

    private static void ValidateIndex<T>(T[] array, int index, bool allowEndIndex)
    {
        if (index < 0)
            ThrowHelper.Throw<ArgumentException>("The index cannot be negative.");

        int length = (array?.Length).GetValueOrDefault();

        if (index > length)
            ThrowHelper.Throw<ArgumentException>("The index cannot be greater than the array's length.");

        if (!allowEndIndex)
            if (index == length)
                ThrowHelper.Throw<ArgumentException>("The index cannot be equal to the array's length in this instance.");
    }

    #region Array Identification Extensions

    /// <summary>
    /// Determines whether an array type contains elements of the provided type.
    /// It only checks for multidimensional arrays (of the form [(,)*]). Jagged
    /// arrays are not taken into consideration in this implementation. Also
    /// built as an extension method to allow per-instance call.
    /// </summary>
    /// <typeparam name="TElement">
    /// The type of the element the array stores.
    /// </typeparam>
    /// <param name="array">
    /// The array whose type to examine.
    /// </param>
    /// <returns>
    /// Whether the given array type stores elements of the
    /// <typeparamref name="TElement"/> type.
    /// </returns>
    public static bool IsArrayOfType<TElement>(this Array array) => ArrayIdentification.IsArrayOfType<TElement>(array.GetType());
    /// <summary>
    /// Determines whether an array type contains elements of the provided type.
    /// It also checks for jagged arrays up to a maximum jagging level. Also
    /// built as an extension method to allow per-instance call.
    /// </summary>
    /// <typeparam name="TElement">
    /// The type of the element the array stores.
    /// </typeparam>
    /// <param name="array">
    /// The array whose type to examine.
    /// </param>
    /// <param name="maxJaggingLevel">
    /// The maximum jagging level of the array (1 means up to [], 2 means up to
    /// [][], etc.).
    /// </param>
    /// <returns>
    /// Whether the given array type stores elements of the
    /// <typeparamref name="TElement"/> type.
    /// </returns>
    public static bool IsArrayOfType<TElement>(this Array array, int maxJaggingLevel) => ArrayIdentification.IsArrayOfType<TElement>(array.GetType(), maxJaggingLevel);
    #endregion

    #region Array
    /// <summary>
    /// Gets the lengths of the array's dimensions.
    /// </summary>
    /// <param name="array">
    /// The array whose dimension lengths to get.
    /// </param>
    /// <returns>
    /// An <seealso cref="int"/>[] containing the dimension lengths in order.
    /// </returns>
    public static int[] GetDimensionLengths(this Array array)
    {
        var lengths = new int[array.Rank];
        for (int i = 0; i < array.Rank; i++)
            lengths[i] = array.GetLength(i);
        return lengths;
    }
    /// <summary>
    /// Clears the array by zeroing out all its elements. This affects the
    /// original instance.
    /// </summary>
    /// <param name="array">
    /// The array to clear.
    /// </param>
    public static void Clear(this Array array)
    {
        Array.Clear(array, 0, array.Length);
    }
    /// <summary>
    /// Assigns the given <paramref name="value"/> of type
    /// <typeparamref name="T"/> to each element of the specified
    /// <paramref name="array"/>. This affects the original instance.
    /// </summary>
    /// <param name="array">
    /// The array to be filled.
    /// </param>
    /// <param name="value">
    /// The value to assign to each array element.
    /// </param>
    public static void Fill<T>(this T[] array, T value)
    {
#if HAS_ARRAY_FILL
        Array.Fill(array, value);
#else
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = value;
        }
#endif
    }
    #endregion
}
