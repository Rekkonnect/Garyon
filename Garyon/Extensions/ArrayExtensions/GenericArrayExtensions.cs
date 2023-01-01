using Garyon.Exceptions;
using Garyon.Functions.Arrays;
using System;
using System.Collections.Generic;

namespace Garyon.Extensions.ArrayExtensions;

/// <summary>Provides generic extension methods for arrays.</summary>
public static class GenericArrayExtensions
{
    /// <summary>Appends an element to the original array without altering it. Returns a new array which contains the appended element at its end.</summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The original array to which the element will be appended.</param>
    /// <param name="item">The item to append.</param>
    public static T[] Append<T>(this T[] array, T item) => array.InsertAt(array.Length, item);
    /// <summary>Appends the specified elements to the original array without altering it. Returns a new array which contains the appended element at its end.</summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The original array to which the elements will be appended.</param>
    /// <param name="items">The items to append.</param>
    public static T[] AppendRange<T>(this T[] array, T[] items) => array.InsertRangeAt(array.Length, items);
    /// <summary>Copies the original array and returns a new array which contains the same elements at the same indices.</summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The original array to copy.</param>
    public static T[] CopyArray<T>(this T[] array)
    {
        T[] result = new T[array.Length];
        for (int i = 0; i < array.Length; i++)
            result[i] = array[i];
        return result;
    }
    /// <summary>Creates a new array with a number of empty elements at the specified index with the specified length.</summary>
    /// <typeparam name="T">The type of the elements stored in the array.</typeparam>
    /// <param name="array">The initial array. It remains unaffected after the operation.</param>
    /// <param name="index">The index at which the gap starts.</param>
    /// <param name="length">The number of empty elements to include in the resulting array. If it's equal to 0, the function returns a copy of the original array without affecting it. The <paramref name="index"/> argument is ignored in that case.</param>
    /// <returns>The new array that contains the gap.</returns>
    public static T[] InsertGapAt<T>(this T[] array, int index, int length)
    {
        if (length < 0)
            ThrowHelper.Throw<ArgumentException>("The length cannot be negative.");

        if (length == 0)
            return array.CopyArray();

        ValidateIndex(array, index, true);

        if ((array?.Length).GetValueOrDefault() == 0)
            return new T[length];

        T[] result = new T[array.Length + length];

        for (int i = 0; i < index; i++)
            result[i] = array[i];
        for (int i = index; i < array.Length; i++)
            result[i + length] = array[i];
        return result;
    }
    /// <summary>Inserts an element at the array into a specified index without affecting the original array. Returns a new array which contains the elements of the original array and the inserted one.</summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The original array to which the element will be inserted.</param>
    /// <param name="item">The item to insert to the array.</param>
    /// <param name="index">The index of the inserted item in the array.</param>
    public static T[] InsertAt<T>(this T[] array, int index, T item)
    {
        var result = InsertGapAt(array, index, 1);
        result[index] = item;
        return result;
    }
    /// <summary>Inserts the specified elements into the array at a specified index without affecting the original array. Returns a new array which contains the elements of the original array and the inserted one.</summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The original array to which the elements will be inserted.</param>
    /// <param name="items">The items to insert to the array.</param>
    /// <param name="index">The index of the first inserted item in the array.</param>
    public static T[] InsertRangeAt<T>(this T[] array, int index, T[] items)
    {
        var result = InsertGapAt(array, index, items.Length);
        foreach (var (i, item) in items.WithIndex())
            result[index + i] = item;
        return result;
    }
    /// <summary>Moves an array element at a specified index to another. This affects the original array and returns its instance.</summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The original array whose element to move.</param>
    /// <param name="from">The old index of the element to move.</param>
    /// <param name="to">The new index of the element to move.</param>
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
    /// <summary>Moves an array element at a specified index to the end of the array. This affects the original array and returns its instance.</summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The original array whose element to move.</param>
    /// <param name="from">The old index of the element to move.</param>
    public static T[] MoveElementToEnd<T>(this T[] array, int from) => array.MoveElement(from, array.Length - 1);
    /// <summary>Moves an array element at a specified index to the start of the array. This affects the original array and returns its instance.</summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The original array whose element to move.</param>
    /// <param name="from">The old index of the element to move.</param>
    public static T[] MoveElementToStart<T>(this T[] array, int from) => array.MoveElement(from, 0);
    /// <summary>Prepends an element to the original array without altering it. Returns a new array which contains the prepended element at its end.</summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The original array to which the element will be prepended.</param>
    /// <param name="item">The item to prepend.</param>
    public static T[] Prepend<T>(this T[] array, T item) => array.InsertAt(0, item);
    /// <summary>Removes an element of the array at a specified index without affecting it. Returns a new array without the removed element.</summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The original array whose element will be removed.</param>
    /// <param name="index">The index of the element to remove.</param>
    public static T[] RemoveAt<T>(this T[] array, int index)
    {
        ValidateIndex(array, index, false);

        T[] result = new T[array.Length - 1];
        for (int i = 0; i < index; i++)
            result[i] = array[i];
        for (int i = index + 1; i < array.Length; i++)
            result[i - 1] = array[i];
        return result;
    }
    /// <summary>Removes the duplicate elements of the array without affecting it. Returns a new array without the duplicate elements.</summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The original array whose duplicate elements will be removed.</param>
    public static T[] RemoveDuplicates<T>(this T[] array)
    {
        var result = new List<T>();
        for (int i = 0; i < array.Length; i++)
            if (!result.Contains(array[i]))
                result.Add(array[i]);
        return result.ToArray();
    }
    /// <summary>Reverses the elements of the array. Returns the instance of the original array.</summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The original array to reverse.</param>
    public static T[] Reverse<T>(this T[] array)
    {
        for (int i = 0; i < array.Length / 2; i++)
            array.Swap(i, array.Length - 1 - i);
        return array;
    }
    /// <summary>Sorts the array, affecting the original array. Returns the instance of the original array.</summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The original array to sort.</param>
    public static T[] Sort<T>(this T[] array)
    {
        Array.Sort(array);
        return array;
    }
    /// <summary>Swaps two elements in the array. Returns the instance of the original array.</summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The array whose elements to swap.</param>
    /// <param name="a">The index of the first element to swap.</param>
    /// <param name="b">The index of the second element to swap.</param>
    public static T[] Swap<T>(this T[] array, int a, int b)
    {
        ValidateIndex(array, a, false);
        ValidateIndex(array, b, false);

        T t = array[a];
        array[a] = array[b];
        array[b] = t;
        return array;
    }

    /// <summary>Gets the indices of the elements in the array that match the specified values, using the <seealso cref="object.Equals(object?)"/> method to compare the values.</summary>
    /// <typeparam name="T">The type of the elements contained in the array.</typeparam>
    /// <param name="array">The array whose element indices that match the <paramref name="value"/> to get.</param>
    /// <param name="value">The value to match.</param>
    /// <returns>An array with the indices of the matching elements, ascendingly sorted.</returns>
    public static int[] GetIndicesOfMatchingValues<T>(this T[] array, T value)
    {
        var indices = new List<int>(array.Length);
        foreach (var (i, e) in array.WithIndex())
            if (e.Equals(value))
                indices.Add(i);
        return indices.ToArray();
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
    public static bool IsArrayOfByte(this Array array) => ArrayIdentification.IsArrayOfType<byte>(array.GetType());
    public static bool IsArrayOfInt16(this Array array) => ArrayIdentification.IsArrayOfType<short>(array.GetType());
    public static bool IsArrayOfInt32(this Array array) => ArrayIdentification.IsArrayOfType<int>(array.GetType());
    public static bool IsArrayOfInt64(this Array array) => ArrayIdentification.IsArrayOfType<long>(array.GetType());
    public static bool IsArrayOfSByte(this Array array) => ArrayIdentification.IsArrayOfType<sbyte>(array.GetType());
    public static bool IsArrayOfUInt16(this Array array) => ArrayIdentification.IsArrayOfType<ushort>(array.GetType());
    public static bool IsArrayOfUInt32(this Array array) => ArrayIdentification.IsArrayOfType<uint>(array.GetType());
    public static bool IsArrayOfUInt64(this Array array) => ArrayIdentification.IsArrayOfType<ulong>(array.GetType());
    public static bool IsArrayOfSingle(this Array array) => ArrayIdentification.IsArrayOfType<float>(array.GetType());
    public static bool IsArrayOfDouble(this Array array) => ArrayIdentification.IsArrayOfType<double>(array.GetType());
    public static bool IsArrayOfDecimal(this Array array) => ArrayIdentification.IsArrayOfType<decimal>(array.GetType());
    public static bool IsArrayOfChar(this Array array) => ArrayIdentification.IsArrayOfType<char>(array.GetType());
    public static bool IsArrayOfBoolean(this Array array) => ArrayIdentification.IsArrayOfType<bool>(array.GetType());
    public static bool IsArrayOfString(this Array array) => ArrayIdentification.IsArrayOfType<string>(array.GetType());

    /// <summary>Determines whether an array type contains elements of the provided type. It only checks for multidimensional arrays (of the form [(,)*]). Jagged arrays are not taken into consideration in this implementation. Also built as an extension method to allow per-instance call.</summary>
    /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
    /// <param name="array">The array whose type to examine.</param>
    /// <returns>Whether the given array type stores elements of the <typeparamref name="TElement"/> type.</returns>
    public static bool IsArrayOfType<TElement>(this Array array) => ArrayIdentification.IsArrayOfType<TElement>(array.GetType());
    /// <summary>Determines whether an array type contains elements of the provided type. It also checks for jagged arrays up to a maximum jagging level. Also built as an extension method to allow per-instance call.</summary>
    /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
    /// <param name="array">The array whose type to examine.</param>
    /// <param name="maxJaggingLevel">The maximum jagging level of the array (1 means up to [], 2 means up to [][], etc.).</param>
    /// <returns>Whether the given array type stores elements of the <typeparamref name="TElement"/> type.</returns>
    public static bool IsArrayOfType<TElement>(this Array array, int maxJaggingLevel) => ArrayIdentification.IsArrayOfType<TElement>(array.GetType(), maxJaggingLevel);
    #endregion

    #region Array
    /// <summary>Gets the lengths of the array's dimensions.</summary>
    /// <param name="array">The array whose dimension lengths to get.</param>
    /// <returns>An <seealso cref="int"/>[] containing the dimension lengths in order.</returns>
    public static int[] GetDimensionLengths(this Array array)
    {
        var lengths = new int[array.Rank];
        for (int i = 0; i < array.Rank; i++)
            lengths[i] = array.GetLength(i);
        return lengths;
    }
    /// <summary>Clears the array by zeroing out all its elements. This affects the original instance.</summary>
    /// <param name="array">The array to clear.</param>
    public static void Clear(this Array array)
    {
        Array.Clear(array, 0, array.Length);
    }
    /// <summary>Assigns the given <paramref name="value"/> of type <typeparamref name="T"/> to each element of the specified <paramref name="array"/>. This affects the original instance.</summary>
    /// <param name="array">The array to be filled.</param>
    /// <param name="value">The value to assign to each array element.</param>
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
