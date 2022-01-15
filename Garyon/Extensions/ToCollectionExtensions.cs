using Garyon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Garyon.Extensions
{
    /// <summary>Contains extensions related to converting <seealso cref="IEnumerable{T}"/> instances into collections.</summary>
    public static class ToCollectionExtensions
    {
        /// <summary>Converts a provided sequence of elements into an array.</summary>
        /// <typeparam name="T">The type of elements stored in the <seealso cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="source">The source <seealso cref="IEnumerable{T}"/> to convert into an array.</param>
        /// <returns>A newly created array containing the source sequence of elements, if not <see langword="null"/>, otherwise <seealso cref="Array.Empty{T}"/>.</returns>
        public static T[] ToArrayOrEmpty<T>(this IEnumerable<T>? source)
        {
            return source?.ToArray() ?? Array.Empty<T>();
        }
        /// <summary>Converts a provided sequence of elements into a <seealso cref="List{T}"/>.</summary>
        /// <typeparam name="T">The type of elements stored in the <seealso cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="source">The source <seealso cref="IEnumerable{T}"/> to convert into a <seealso cref="List{T}"/>.</param>
        /// <returns>A newly created <seealso cref="List{T}"/> containing the source sequence of elements, if not <see langword="null"/>, otherwise an empty list.</returns>
        public static List<T> ToListOrEmpty<T>(this IEnumerable<T>? source)
        {
            return source?.ToList() ?? new();
        }
        /// <summary>Converts a provided sequence of elements into a <seealso cref="HashSet{T}"/>.</summary>
        /// <typeparam name="T">The type of elements stored in the <seealso cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="source">The source <seealso cref="IEnumerable{T}"/> to convert into a <seealso cref="HashSet{T}"/>.</param>
        /// <returns>A newly created <seealso cref="HashSet{T}"/> containing the source sequence of elements, if not <see langword="null"/>, otherwise an empty set.</returns>
        public static HashSet<T> ToHashSetOrEmpty<T>(this IEnumerable<T>? source)
        {
            return source?.ToHashSet() ?? new();
        }
    }
}