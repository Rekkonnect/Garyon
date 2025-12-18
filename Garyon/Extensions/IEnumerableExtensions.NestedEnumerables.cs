using Garyon.Objects;
using Garyon.Objects.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

#nullable enable

public static partial class IEnumerableExtensions
{
    /// <summary>Concatenates multiple <seealso cref="IEnumerable{T}"/>s and returns the concatenated result.</summary>
    /// <typeparam name="T">The type of the elements the <seealso cref="IEnumerable{T}"/>s hold.</typeparam>
    /// <param name="source">The source <seealso cref="IEnumerable{T}"/> to concatenate with the others.</param>
    /// <param name="others">The other <seealso cref="IEnumerable{T}"/>s to concatenate.</param>
    public static IEnumerable<T> ConcatMultiple<T>(this IEnumerable<T> source, params IEnumerable<T>[] others)
    {
        return ConcatMultiple(source, (IEnumerable<IEnumerable<T>>)others);
    }

#if HAS_SPAN
    /// <summary>Concatenates multiple <seealso cref="IEnumerable{T}"/>s and returns the concatenated result.</summary>
    /// <typeparam name="T">The type of the elements the <seealso cref="IEnumerable{T}"/>s hold.</typeparam>
    /// <param name="source">The source <seealso cref="IEnumerable{T}"/> to concatenate with the others.</param>
    /// <param name="others">The other <seealso cref="IEnumerable{T}"/>s to concatenate.</param>
    public static IEnumerable<T> ConcatMultiple<T>(this IEnumerable<T> source, params ReadOnlySpan<IEnumerable<T>> others)
    {
        var concatenated = source;
        foreach (var e in others)
            concatenated = concatenated.Concat(e);
        return concatenated;
    }
#endif

    /// <summary>Concatenates multiple <seealso cref="IEnumerable{T}"/>s and returns the concatenated result.</summary>
    /// <typeparam name="T">The type of the elements the <seealso cref="IEnumerable{T}"/>s hold.</typeparam>
    /// <param name="source">The source <seealso cref="IEnumerable{T}"/> to concatenate with the others.</param>
    /// <param name="others">The other <seealso cref="IEnumerable{T}"/>s to concatenate.</param>
    public static IEnumerable<T> ConcatMultiple<T>(this IEnumerable<T> source, params IEnumerable<IEnumerable<T>> others)
    {
        var concatenated = source;
        foreach (var e in others)
            concatenated = concatenated.Concat(e);
        return concatenated;
    }

    /// <summary>Flattens a collection of collections into a single collection. The resulting elements are contained in the order they are enumerated depth-first.</summary>
    /// <typeparam name="T">The type of elements contained in the collections.</typeparam>
    /// <param name="source">The collection of collections.</param>
    /// <returns>The flattened collection.</returns>
    public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source) => new FlattenedEnumerables2D<T>(source);
    /// <summary>Flattens a collection of collections of collections into a single collection. The resulting elements are contained in the order they are enumerated depth-first.</summary>
    /// <typeparam name="T">The type of elements contained in the collections.</typeparam>
    /// <param name="source">The collection of collections of collections.</param>
    /// <returns>The flattened collection.</returns>
    public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<IEnumerable<T>>> source) => new FlattenedEnumerables3D<T>(source);

    /// <summary>Flattens a collection of collections into a single collection. The resulting elements are contained in the order they are enumerated depth-first.</summary>
    /// <typeparam name="T">The type of elements contained in the collections.</typeparam>
    /// <param name="source">The collection of collections.</param>
    /// <returns>The flattened collection.</returns>
    /// <remarks>The function enumerates all the elements and caches them. This may induce a latency during enumeration of the collections. Prefer calling this function when enumeration of the same flattened collections is performed more than once.</remarks>
    public static IEnumerable<T> FlattenInstant<T>(this IEnumerable<IEnumerable<T>> source)
    {
        var result = new List<T>();
        foreach (var c in source)
            result.AddRange(c);
        return result;
    }
    /// <summary>Flattens a collection of collections of collections into a single collection. The resulting elements are contained in the order they are enumerated depth-first.</summary>
    /// <typeparam name="T">The type of elements contained in the collections.</typeparam>
    /// <param name="source">The collection of collections of collections.</param>
    /// <returns>The flattened collection.</returns>
    /// <remarks>The function enumerates all the elements and caches them. This may induce a latency during enumeration of the collections. Prefer calling this function when enumeration of the same flattened collections is performed more than once.</remarks>
    public static IEnumerable<T> FlattenInstant<T>(this IEnumerable<IEnumerable<IEnumerable<T>>> source)
    {
        var result = new List<T>();
        foreach (var c0 in source)
            foreach (var c1 in c0)
                result.AddRange(c1);
        return result;
    }
}
