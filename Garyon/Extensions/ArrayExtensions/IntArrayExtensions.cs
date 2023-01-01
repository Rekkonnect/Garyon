using Garyon.Exceptions;
using Garyon.Extensions.ArrayExtensions.ArrayConverting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Garyon.Extensions.ArrayExtensions;

// TODO: Consider implementing some of that functionality using the intrinsic acceleration pattern
// Examples including incrementation, array addition, subtraction, etc.

/// <summary>Provides generic extension methods for <seealso cref="int"/> arrays.</summary>
public static class IntArrayExtensions
{
    /// <summary>Returns a string containing the ranges of an array of integers in a formatted way.</summary>
    /// <param name="s">The array of integers containing the values whose ranges to retrieve. The array must only contain distinct elements. The array will be sorted without affecting the original instance.</param>
    public static string ShowValuesWithRanges(this int[] s)
    {
        s = s.CopyAccelerated().Sort(); // Sort the values
        var result = new StringBuilder();
        if (s.Length > 0)
        {
            int lastShownValue = s[0];
            int lastValueInCombo = s[0];
            result.Append(lastShownValue);
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] > lastValueInCombo + 1)
                {
                    if (result.Last() == '-')
                        result.Append(s[i - 1]);
                    result.Append($", {s[i]}");
                    lastValueInCombo = lastShownValue = s[i];
                }
                else if (s[i] == lastValueInCombo + 1)
                {
                    if (lastShownValue == lastValueInCombo)
                        result.Append('-');
                    lastValueInCombo = s[i];
                }
            }
            if (result.Last() == '-')
                result.Append(lastValueInCombo);
        }
        return result.ToString();
    }
    /// <summary>Returns a string containing the ranges of an array of integers in a formatted way.</summary>
    /// <param name="s">The array of integers containing the values whose ranges to retrieve. The array must only contain distinct elements. The array will be sorted without affecting the original instance.</param>
    public static string ShowSortedValuesWithRanges(this int[] s)
    {

        s = s.CopyAccelerated().Sort(); // Sort the values
        var result = new StringBuilder();
        if (s.Length > 0)
        {
            int lastShownValue = s[0];
            int lastValueInCombo = s[0];
            result.Append(lastShownValue);
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] > lastValueInCombo + 1)
                {
                    if (result.Last() == '-')
                        result.Append(s[i - 1]);
                    result.Append($", {s[i]}");
                    lastValueInCombo = lastShownValue = s[i];
                }
                else if (s[i] == lastValueInCombo + 1)
                {
                    if (lastShownValue == lastValueInCombo)
                        result.Append('-');
                    lastValueInCombo = s[i];
                }
            }
            if (result.Last() == '-')
                result.Append(lastValueInCombo);
        }
        return result.ToString();
    }

    /// <summary>Decrements all the integers of the array by a value.</summary>
    /// <param name="a">The array of integers whose values to decrement.</param>
    /// <param name="decrement">The value to decrement the integers by.</param>
    public static int[] Decrement(this int[] a, int decrement)
    {
        for (int i = 0; i < a.Length; i++)
            a[i] -= decrement;
        return a;
    }
    /// <summary>Decrements the integers of the array within the specified range by a value.</summary>
    /// <param name="a">The array of integers whose values to decrement.</param>
    /// <param name="decrement">The value to decrement the integers by.</param>
    /// <param name="from">The first index of the integers to decrement.</param>
    /// <param name="to">The last index of the integers to decrement.</param>
    public static int[] Decrement(this int[] a, int decrement, int from, int to)
    {
        for (int i = from; i <= to; i++)
            a[i] -= decrement;
        return a;
    }
    /// <summary>Decrements all the integers of the array by one.</summary>
    /// <param name="a">The array of integers whose values to decrement by one.</param>
    public static int[] DecrementByOne(this int[] a)
    {
        for (int i = 0; i < a.Length; i++)
            a[i]--;
        return a;
    }
    /// <summary>Decrements the integers of the array within the specified range by one.</summary>
    /// <param name="a">The array of integers whose values to decrement by one.</param>
    /// <param name="from">The first index of the integers to decrement by one.</param>
    /// <param name="to">The last index of the integers to decrement by one.</param>
    public static int[] DecrementByOne(this int[] a, int from, int to)
    {
        for (int i = from; i <= to; i++)
            a[i]--;
        return a;
    }
    /// <summary>Decrements all the integers of the array by a value.</summary>
    /// <param name="a">The array of integers whose values to increment.</param>
    /// <param name="increment">The value to increment the integers by.</param>
    public static int[] Increment(this int[] a, int increment)
    {
        for (int i = 0; i < a.Length; i++)
            a[i] += increment;
        return a;
    }
    /// <summary>Decrements the integers of the array within the specified range by a value.</summary>
    /// <param name="a">The array of integers whose values to increment.</param>
    /// <param name="increment">The value to increment the integers by.</param>
    /// <param name="from">The first index of the integers to increment.</param>
    /// <param name="to">The last index of the integers to increment.</param>
    public static int[] Increment(this int[] a, int increment, int from, int to)
    {
        for (int i = from; i <= to; i++)
            a[i] += increment;
        return a;
    }
    /// <summary>Decrements all the integers of the array by one.</summary>
    /// <param name="a">The array of integers whose values to increment by one.</param>
    public static int[] IncrementByOne(this int[] a)
    {
        for (int i = 0; i < a.Length; i++)
            a[i]++;
        return a;
    }
    /// <summary>Decrements the integers of the array within the specified range by one.</summary>
    /// <param name="a">The array of integers whose values to increment by one.</param>
    /// <param name="from">The first index of the integers to increment by one.</param>
    /// <param name="to">The last index of the integers to increment by one.</param>
    public static int[] IncrementByOne(this int[] a, int from, int to)
    {
        for (int i = from; i <= to; i++)
            a[i]++;
        return a;
    }

    /// <summary>Creates a new array containing the results of the subtractions of the two arrays' respective values.</summary>
    /// <param name="minuends">The minuend array, containing the original values.</param>
    /// <param name="subtrahends">The subtrahend array, containing the values that will be subtracted from the minuends.</param>
    /// <returns>The resulting array containing the differences of each respective pair of elements.</returns>
    public static int[] Subtract(this int[] minuends, int[] subtrahends)
    {
        if (minuends is null || subtrahends is null)
            ThrowHelper.Throw<ArgumentException>("Both arrays must be non-null.");
        if (minuends?.Length != subtrahends?.Length)
            ThrowHelper.Throw<ArgumentException>("Both arrays must have the same length.");

        int[] result = new int[minuends.Length];
        for (int i = 0; i < result.Length; i++)
            result[i] = minuends[i] - subtrahends[i];
        return result;
    }

    /// <summary>Returns a new array without the negative integer values of the original array.</summary>
    /// <param name="a">The array whose negative integer values will be removed.</param>
    public static int[] RemoveNegatives(this int[] a)
    {
        var result = new List<int>();
        for (int i = 0; i < a.Length; i++)
            if (a[i] >= 0)
                result.Add(a[i]);
        return result.ToArray();
    }
    /// <summary>Returns a new array whose integer values are different than their respective indices in the original array.</summary>
    /// <param name="a">The array whose integer values matching their respective indices will be removed.</param>
    public static int[] RemoveElementsMatchingIndices(this int[] a)
    {
        var result = new List<int>();
        for (int i = 0; i < a.Length; i++)
            if (a[i] != i)
                result.Add(a[i]);
        return result.ToArray();
    }
    /// <summary>Returns a new array containing the indices of the integers in the array matching the specified value.</summary>
    /// <param name="a">The array whose integer indices will be returned.</param>
    /// <param name="value">The value to match.</param>
    public static int[] GetIndicesOfMatchingValues(this int[] a, int value)
    {
        var indices = new List<int>();
        for (int i = 0; i < a.Length; i++)
            if (a[i] == value)
                indices.Add(i);
        return indices.ToArray();
    }
}
