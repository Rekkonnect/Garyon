using Garyon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

using static Extremum;

/// <summary>Provides extensions related to getting the extremum of a collection.</summary>
public static class EnumerableExtremumExtensions
{
#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
    /// <summary>Gets the extremum of a collection.</summary>
    /// <typeparam name="T">The type of elements stored in the collection.</typeparam>
    /// <param name="source">The source collection whose extremum to get.</param>
    /// <param name="extremum">The kind of extremum to get.</param>
    /// <returns>The extremum of the collection.</returns>
    public static T Extremum<T>(this IEnumerable<T> source, Extremum extremum)
    {
        return extremum switch
        {
            Minimum => source.Min(),
            Maximum => source.Max(),
        };
    }
    /// <summary>Transforms all elements of the collection to <seealso cref="int"/>, and gets the extremum of the transformed collection.</summary>
    /// <typeparam name="T">The type of elements stored in the collection.</typeparam>
    /// <param name="source">The source collection whose transformed collection's extremum to get.</param>
    /// <param name="extremum">The kind of extremum to get.</param>
    /// <param name="selector">The selector function that transforms every element of the collection into <seealso cref="int"/>.</param>
    /// <returns>The extremum of the transformed collection.</returns>
    public static int Extremum<T>(this IEnumerable<T> source, Extremum extremum, Func<T, int> selector)
    {
        return extremum switch
        {
            Minimum => source.Min(selector),
            Maximum => source.Max(selector),
        };
    }
    /// <summary>Transforms all elements of the collection to <seealso cref="int"/>?, and gets the extremum of the transformed collection.</summary>
    /// <typeparam name="T">The type of elements stored in the collection.</typeparam>
    /// <param name="source">The source collection whose transformed collection's extremum to get.</param>
    /// <param name="extremum">The kind of extremum to get.</param>
    /// <param name="selector">The selector function that transforms every element of the collection into <seealso cref="int"/>?.</param>
    /// <returns>The extremum of the transformed collection.</returns>
    public static int? Extremum<T>(this IEnumerable<T> source, Extremum extremum, Func<T, int?> selector)
    {
        return extremum switch
        {
            Minimum => source.Min(selector),
            Maximum => source.Max(selector),
        };
    }
    /// <summary>Transforms all elements of the collection to <seealso cref="long"/>, and gets the extremum of the transformed collection.</summary>
    /// <typeparam name="T">The type of elements stored in the collection.</typeparam>
    /// <param name="source">The source collection whose transformed collection's extremum to get.</param>
    /// <param name="extremum">The kind of extremum to get.</param>
    /// <param name="selector">The selector function that transforms every element of the collection into <seealso cref="long"/>.</param>
    /// <returns>The extremum of the transformed collection.</returns>
    public static long Extremum<T>(this IEnumerable<T> source, Extremum extremum, Func<T, long> selector)
    {
        return extremum switch
        {
            Minimum => source.Min(selector),
            Maximum => source.Max(selector),
        };
    }
    /// <summary>Transforms all elements of the collection to <seealso cref="long"/>?, and gets the extremum of the transformed collection.</summary>
    /// <typeparam name="T">The type of elements stored in the collection.</typeparam>
    /// <param name="source">The source collection whose transformed collection's extremum to get.</param>
    /// <param name="extremum">The kind of extremum to get.</param>
    /// <param name="selector">The selector function that transforms every element of the collection into <seealso cref="long"/>?.</param>
    /// <returns>The extremum of the transformed collection.</returns>
    public static long? Extremum<T>(this IEnumerable<T> source, Extremum extremum, Func<T, long?> selector)
    {
        return extremum switch
        {
            Minimum => source.Min(selector),
            Maximum => source.Max(selector),
        };
    }
    /// <summary>Transforms all elements of the collection to <seealso cref="float"/>, and gets the extremum of the transformed collection.</summary>
    /// <typeparam name="T">The type of elements stored in the collection.</typeparam>
    /// <param name="source">The source collection whose transformed collection's extremum to get.</param>
    /// <param name="extremum">The kind of extremum to get.</param>
    /// <param name="selector">The selector function that transforms every element of the collection into <seealso cref="float"/>.</param>
    /// <returns>The extremum of the transformed collection.</returns>
    public static float Extremum<T>(this IEnumerable<T> source, Extremum extremum, Func<T, float> selector)
    {
        return extremum switch
        {
            Minimum => source.Min(selector),
            Maximum => source.Max(selector),
        };
    }
    /// <summary>Transforms all elements of the collection to <seealso cref="float"/>?, and gets the extremum of the transformed collection.</summary>
    /// <typeparam name="T">The type of elements stored in the collection.</typeparam>
    /// <param name="source">The source collection whose transformed collection's extremum to get.</param>
    /// <param name="extremum">The kind of extremum to get.</param>
    /// <param name="selector">The selector function that transforms every element of the collection into <seealso cref="float"/>?.</param>
    /// <returns>The extremum of the transformed collection.</returns>
    public static float? Extremum<T>(this IEnumerable<T> source, Extremum extremum, Func<T, float?> selector)
    {
        return extremum switch
        {
            Minimum => source.Min(selector),
            Maximum => source.Max(selector),
        };
    }
    /// <summary>Transforms all elements of the collection to <seealso cref="double"/>, and gets the extremum of the transformed collection.</summary>
    /// <typeparam name="T">The type of elements stored in the collection.</typeparam>
    /// <param name="source">The source collection whose transformed collection's extremum to get.</param>
    /// <param name="extremum">The kind of extremum to get.</param>
    /// <param name="selector">The selector function that transforms every element of the collection into <seealso cref="double"/>.</param>
    /// <returns>The extremum of the transformed collection.</returns>
    public static double Extremum<T>(this IEnumerable<T> source, Extremum extremum, Func<T, double> selector)
    {
        return extremum switch
        {
            Minimum => source.Min(selector),
            Maximum => source.Max(selector),
        };
    }
    /// <summary>Transforms all elements of the collection to <seealso cref="double"/>?, and gets the extremum of the transformed collection.</summary>
    /// <typeparam name="T">The type of elements stored in the collection.</typeparam>
    /// <param name="source">The source collection whose transformed collection's extremum to get.</param>
    /// <param name="extremum">The kind of extremum to get.</param>
    /// <param name="selector">The selector function that transforms every element of the collection into <seealso cref="double"/>?.</param>
    /// <returns>The extremum of the transformed collection.</returns>
    public static double? Extremum<T>(this IEnumerable<T> source, Extremum extremum, Func<T, double?> selector)
    {
        return extremum switch
        {
            Minimum => source.Min(selector),
            Maximum => source.Max(selector),
        };
    }
    /// <summary>Transforms all elements of the collection to <seealso cref="decimal"/>, and gets the extremum of the transformed collection.</summary>
    /// <typeparam name="T">The type of elements stored in the collection.</typeparam>
    /// <param name="source">The source collection whose transformed collection's extremum to get.</param>
    /// <param name="extremum">The kind of extremum to get.</param>
    /// <param name="selector">The selector function that transforms every element of the collection into <seealso cref="decimal"/>.</param>
    /// <returns>The extremum of the transformed collection.</returns>
    public static decimal Extremum<T>(this IEnumerable<T> source, Extremum extremum, Func<T, decimal> selector)
    {
        return extremum switch
        {
            Minimum => source.Min(selector),
            Maximum => source.Max(selector),
        };
    }
    /// <summary>Transforms all elements of the collection to <seealso cref="decimal"/>?, and gets the extremum of the transformed collection.</summary>
    /// <typeparam name="T">The type of elements stored in the collection.</typeparam>
    /// <param name="source">The source collection whose transformed collection's extremum to get.</param>
    /// <param name="extremum">The kind of extremum to get.</param>
    /// <param name="selector">The selector function that transforms every element of the collection into <seealso cref="decimal"/>?.</param>
    /// <returns>The extremum of the transformed collection.</returns>
    public static decimal? Extremum<T>(this IEnumerable<T> source, Extremum extremum, Func<T, decimal?> selector)
    {
        return extremum switch
        {
            Minimum => source.Min(selector),
            Maximum => source.Max(selector),
        };
    }
    /// <summary>Transforms all elements of the collection to <typeparamref name="TResult"/>?, and gets the extremum of the transformed collection.</summary>
    /// <typeparam name="TSource">The type of elements stored in the collection.</typeparam>
    /// <typeparam name="TResult">The type of the transformed elements.</typeparam>
    /// <param name="source">The source collection whose transformed collection's extremum to get.</param>
    /// <param name="extremum">The kind of extremum to get.</param>
    /// <param name="selector">The selector function that transforms every element of the collection into <typeparamref name="TResult"/>?.</param>
    /// <returns>The extremum of the transformed collection.</returns>
    public static TResult? Extremum<TSource, TResult>(this IEnumerable<TSource> source, Extremum extremum, Func<TSource, TResult?> selector)
    {
        return extremum switch
        {
            Minimum => source.Min(selector),
            Maximum => source.Max(selector),
        };
    }
#pragma warning restore CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
}
