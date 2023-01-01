﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

public static class Extensions
{
    public static bool MatchIndices(this List<int> l)
    {
        for (int i = 0; i < l.Count; i++)
            if (l[i] != i)
                return false;
        return true;
    }
    public static bool MatchIndicesFromEnd(this List<int> l, int length)
    {
        for (int i = l.Count - 1; i >= 0; i--)
            if (l[i] != length - l.Count + i)
                return false;
        return true;
    }
    public static int FindIndexInList(this List<int> l, int match)
    {
        for (int i = 0; i < l.Count; i++)
            if (l[i] == match)
                return i;
        return -1;
    }
    public static List<int> RemoveDuplicates(this List<int> l) => new HashSet<int>(l).ToList();
    public static List<int> RemoveNegatives(this List<int> l)
    {
        var newList = new List<int>();
        for (int i = 0; i < l.Count; i++)
            if (l[i] >= 0)
                newList.Add(l[i]);
        return newList;
    }
    public static int FindOccurences(this List<int> l, int match)
    {
        int result = 0;
        for (int i = 0; i < l.Count; i++)
            if (l[i] == match)
                result++;
        return result;
    }

    public static T[] GetInnerArray<T>(this T[,] ar, int innerArrayIndex)
    {
        T[] innerAr = new T[ar.GetLength(1)];
        for (int i = 0; i < innerAr.Length; i++)
            innerAr[i] = ar[innerArrayIndex, i];
        return innerAr;
    }
    public static int[] GetLengths<T>(this T[,] ar)
    {
        int[] lengths = new int[ar.Length];
        for (int i = 0; i < ar.Length; i++)
            lengths[i] = ar.GetInnerArray(i).Length;
        return lengths;
    }

    /// <summary>Calculates the next available zero-based index in a collection of reserved indices.</summary>
    /// <param name="reservedIndices">The <seealso cref="List{T}"/> containing the reserved indices.</param>
    /// <returns>The next available zero-based index.</returns>
    public static int GetNextAvailableZeroBasedIndex(this IEnumerable<int> reservedIndices)
    {
        var copy = new List<int>(reservedIndices);
        copy = copy.RemoveDuplicates();
        copy.Sort();

        // Evilly not checked right as soon as the list is sorted, to make the exception more expensive
        if (copy.First() < 0)
            throw new ArgumentException("The indices list cannot contain negative arguments.");

        // Some quick results
        if (copy.First() > 0)
            return 0;
        if (copy.Last() == copy.Count - 1)
            return copy.Count;

        int min = 1;
        int max = copy.Count - 2;
        int mid = 0;
        while (min <= max)
        {
            mid = (min + max) / 2;
            if (copy.BinarySearch(mid) == mid)
                min = mid + 1;
            else
                max = mid - 1;
        }
        return mid;
    }

    public static HashSet<T> Clone<T>(this HashSet<T> s) => new(s);
    public static SortedSet<T> Clone<T>(this SortedSet<T> s) => new(s);

    public static int[] GetInt32ArrayFromMultidimensionalInt32Array(int[,] a, int dimension, int index)
    {
        int[] ar = new int[a.GetLength(dimension)];
        for (int i = 0; i < ar.Length; i++)
        {
            if (dimension == 0)
                ar[i] = a[index, i];
            else if (dimension == 1)
                ar[i] = a[i, index];
        }
        return ar;
    }
    public static List<List<T>> ToList<T>(this T[,] ar)
    {
        var l = new List<List<T>>();
        for (int i = 0; i < ar.GetLength(0); i++)
        {
            var temp = new List<T>();
            for (int j = 0; j < ar.GetLength(1); j++)
                temp.Add(ar[i, j]);
            l.Add(temp);
        }
        return l;
    }
    public static List<int> ToInt32List(this string[] s)
    {
        var result = new List<int>();
        for (int i = 0; i < s.Length; i++)
            result.Add(Convert.ToInt32(s[i]));
        return result;
    }
}