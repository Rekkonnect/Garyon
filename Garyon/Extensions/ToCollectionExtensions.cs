using Garyon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Garyon.Extensions;

/// <summary>Contains extensions related to converting <seealso cref="IEnumerable{T}"/> instances into collections.</summary>
public static class ToCollectionExtensions
{
#if !HAS_TO_HASHSET
    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
    {
        return new(source);
    }
#endif

    #region To X
#if HAS_DICTIONARY_KVPS_CTOR
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        where TKey : notnull
    {
        return new(source);
    }
#endif
    public static SortedSet<T> ToSortedSet<T>(this IEnumerable<T> source)
    {
        return new(source);
    }
    public static SortedList<TKey, TValue> ToSortedList<TKey, TValue>(this IDictionary<TKey, TValue> source)
        where TKey : notnull
    {
        return new(source);
    }
    public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IDictionary<TKey, TValue> source)
        where TKey : notnull
    {
        return new(source);
    }
    #endregion

    #region To X or empty
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
    /// <summary>Converts a provided sequence of elements into a <seealso cref="SortedSet{T}"/>.</summary>
    /// <typeparam name="T">The type of elements stored in the <seealso cref="IEnumerable{T}"/>.</typeparam>
    /// <param name="source">The source <seealso cref="IEnumerable{T}"/> to convert into a <seealso cref="SortedSet{T}"/>.</param>
    /// <returns>A newly created <seealso cref="SortedSet{T}"/> containing the source sequence of elements, if not <see langword="null"/>, otherwise an empty set.</returns>
    public static SortedSet<T> ToSortedSetOrEmpty<T>(this IEnumerable<T>? source)
    {
        return source?.ToSortedSet() ?? new();
    }
    /// <summary>Converts a provided sequence of elements into a <seealso cref="SortedList{TKey, TValue}"/>.</summary>
    /// <typeparam name="TKey">The type of keys stored in the <seealso cref="IDictionary{TKey, TValue}"/>.</typeparam>
    /// <typeparam name="TValue">The type of values stored in the <seealso cref="IDictionary{TKey, TValue}"/>.</typeparam>
    /// <param name="source">The source <seealso cref="IDictionary{TKey, TValue}"/> to convert into a <seealso cref="SortedList{TKey, TValue}"/>.</param>
    /// <returns>A newly created <seealso cref="SortedList{TKey, TValue}"/> containing the source sequence of elements, if not <see langword="null"/>, otherwise an empty dictionary.</returns>
    public static SortedList<TKey, TValue> ToSortedListOrEmpty<TKey, TValue>(this IDictionary<TKey, TValue>? source)
        where TKey : notnull
    {
        return source?.ToSortedList() ?? new();
    }
    /// <summary>Converts a provided sequence of elements into a <seealso cref="SortedDictionary{TKey, TValue}"/>.</summary>
    /// <typeparam name="TKey">The type of keys stored in the <seealso cref="IDictionary{TKey, TValue}"/>.</typeparam>
    /// <typeparam name="TValue">The type of values stored in the <seealso cref="IDictionary{TKey, TValue}"/>.</typeparam>
    /// <param name="source">The source <seealso cref="IDictionary{TKey, TValue}"/> to convert into a <seealso cref="SortedDictionary{TKey, TValue}"/>.</param>
    /// <returns>A newly created <seealso cref="SortedDictionary{TKey, TValue}"/> containing the source sequence of elements, if not <see langword="null"/>, otherwise an empty dictionary.</returns>
    public static SortedDictionary<TKey, TValue> ToSortedDictionaryOrEmpty<TKey, TValue>(this IDictionary<TKey, TValue>? source)
        where TKey : notnull
    {
        return source?.ToSortedDictionary() ?? new();
    }
    #endregion

    #region To X or existing
    #region Mutable
    /// <summary>
    /// Attempts to upcast the given source into an array of
    /// elements of the given source type, otherwise creates a
    /// new array and returns it.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements contained in the source and the
    /// returned array.
    /// </typeparam>
    /// <param name="source">The source of elements.</param>
    /// <returns>
    /// The source collection as an array of elements, if it was
    /// already, or a new array containing the enumerated values
    /// of the source.
    /// </returns>
    public static T[] ToArrayOrExisting<T>(this IEnumerable<T> source)
    {
        if (source is T[] array)
            return array;

        return source.ToArray();
    }
    /// <summary>
    /// Attempts to upcast the given source into a list of
    /// elements of the given source type, otherwise creates a
    /// new list and returns it.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements contained in the source and the
    /// returned list.
    /// </typeparam>
    /// <param name="source">The source of elements.</param>
    /// <returns>
    /// The source collection as a list of elements, if it was
    /// already, or a new list containing the enumerated values
    /// of the source.
    /// </returns>
    public static List<T> ToListOrExisting<T>(this IEnumerable<T> source)
    {
        if (source is List<T> list)
            return list;

        return source.ToList();
    }
    /// <summary>
    /// Attempts to upcast the given source into a set of
    /// elements of the given source type, otherwise creates a
    /// new set and returns it.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements contained in the source and the
    /// returned set.
    /// </typeparam>
    /// <param name="source">The source of elements.</param>
    /// <returns>
    /// The source collection as a set of elements, if it was
    /// already, or a new set containing the enumerated values
    /// of the source.
    /// </returns>
    public static HashSet<T> ToHashSetOrExisting<T>(this IEnumerable<T> source)
    {
        if (source is HashSet<T> set)
            return set;

        return source.ToHashSet();
    }

    /// <summary>
    /// Attempts to upcast the given source into an
    /// <seealso cref="ICollection{T}"/>, otherwise creates a
    /// new array and returns it <see langword="as"/>
    /// <seealso cref="ICollection{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements contained in the source and the
    /// returned collection.
    /// </typeparam>
    /// <param name="source">The source of elements.</param>
    /// <returns>
    /// The source collection <see langword="as"/>
    /// <seealso cref="ICollection{T}"/>, if it was already,
    /// or a new array containing the enumerated
    /// values of the source.
    /// </returns>
    public static ICollection<T> ToCollectionOrExisting<T>(this IEnumerable<T> source)
    {
        if (source is ICollection<T> collection)
            return collection;

        return source.ToList();
    }
    /// <summary>
    /// Attempts to upcast the given source into a
    /// <seealso cref="ISet{T}"/>, otherwise creates a
    /// new <seealso cref="HashSet{T}"/> and returns it
    /// <see langword="as"/> <seealso cref="ISet{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements contained in the source and the
    /// returned set.
    /// </typeparam>
    /// <param name="source">The source of elements.</param>
    /// <returns>
    /// The source collection <see langword="as"/>
    /// <seealso cref="ISet{T}"/>, if it was already,
    /// or a new <seealso cref="HashSet{T}"/> containing the enumerated
    /// values of the source.
    /// </returns>
    public static ISet<T> ToSetOrExisting<T>(this IEnumerable<T> source)
    {
        if (source is ISet<T> set)
            return set;

        return source.ToHashSet();
    }
    #endregion

    #region Read only
    /// <summary>
    /// Attempts to upcast the given source into a
    /// <seealso cref="IReadOnlyCollection{T}"/>, otherwise creates a
    /// new collection and returns it
    /// <see langword="as"/> <seealso cref="IReadOnlyCollection{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements contained in the source and the
    /// returned collection.
    /// </typeparam>
    /// <param name="source">The source of elements.</param>
    /// <returns>
    /// The source collection <see langword="as"/>
    /// <seealso cref="IReadOnlyCollection{T}"/>, if it was already,
    /// or a new collection containing the enumerated
    /// values of the source.
    /// </returns>
    public static IReadOnlyCollection<T> ToReadOnlyCollectionOrExisting<T>(this IEnumerable<T> source)
    {
        if (source is IReadOnlyCollection<T> collection)
            return collection;

        return source.ToList();
    }
    /// <summary>
    /// Attempts to upcast the given source into a
    /// <seealso cref="IReadOnlyList{T}"/>, otherwise creates a
    /// new collection and returns it
    /// <see langword="as"/> <seealso cref="IReadOnlyList{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements contained in the source and the
    /// returned list.
    /// </typeparam>
    /// <param name="source">The source of elements.</param>
    /// <returns>
    /// The source collection <see langword="as"/>
    /// <seealso cref="IReadOnlyList{T}"/>, if it was already,
    /// or a new collection containing the enumerated
    /// values of the source.
    /// </returns>
    public static IReadOnlyList<T> ToReadOnlyListOrExisting<T>(this IEnumerable<T> source)
    {
        if (source is IReadOnlyList<T> list)
            return list;

        return source.ToList();
    }

#if HAS_READONLY_SET
    /// <summary>
    /// Attempts to upcast the given source into a
    /// <seealso cref="IReadOnlySet{T}"/>, otherwise creates a
    /// new collection and returns it
    /// <see langword="as"/> <seealso cref="IReadOnlySet{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements contained in the source and the
    /// returned set.
    /// </typeparam>
    /// <param name="source">The source of elements.</param>
    /// <returns>
    /// The source collection <see langword="as"/>
    /// <seealso cref="IReadOnlySet{T}"/>, if it was already,
    /// or a new collection containing the enumerated
    /// values of the source.
    /// </returns>
    public static IReadOnlySet<T> ToReadOnlySetOrExisting<T>(this IEnumerable<T> source)
    {
        if (source is IReadOnlySet<T> set)
            return set;

        return source.ToHashSet();
    }
#endif
    #endregion

    #region Dictionary
#if HAS_DICTIONARY_KVPS_CTOR
    /// <summary>
    /// Attempts to upcast the given source into a
    /// <seealso cref="IDictionary{TKey, TValue}"/>, otherwise creates a
    /// new collection and returns it
    /// <see langword="as"/> <seealso cref="IDictionary{TKey, TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">
    /// The type of keys contained in the source and the
    /// returned dictionary.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// The type of values contained in the source and the
    /// returned dictionary.
    /// </typeparam>
    /// <param name="source">The source of elements.</param>
    /// <returns>
    /// The source collection <see langword="as"/>
    /// <seealso cref="IDictionary{TKey, TValue}"/>, if it was already,
    /// or a new collection containing the enumerated
    /// values of the source.
    /// </returns>
    public static IDictionary<TKey, TValue> ToDictionaryOrExisting<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        where TKey : notnull
    {
        if (source is IDictionary<TKey, TValue> dictionary)
            return dictionary;

        return source.ToDictionary();
    }
    /// <summary>
    /// Attempts to upcast the given source into a
    /// <seealso cref="IReadOnlyDictionary{TKey, TValue}"/>, otherwise creates a
    /// new collection and returns it
    /// <see langword="as"/> <seealso cref="IReadOnlyDictionary{TKey, TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">
    /// The type of keys contained in the source and the
    /// returned dictionary.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// The type of values contained in the source and the
    /// returned dictionary.
    /// </typeparam>
    /// <param name="source">The source of elements.</param>
    /// <returns>
    /// The source collection <see langword="as"/>
    /// <seealso cref="IReadOnlyDictionary{TKey, TValue}"/>, if it was already,
    /// or a new collection containing the enumerated
    /// values of the source.
    /// </returns>
    public static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionaryOrExisting<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        where TKey : notnull
    {
        if (source is IReadOnlyDictionary<TKey, TValue> dictionary)
            return dictionary;

        return source.ToDictionary();
    }
#endif
    #endregion
    #endregion
}