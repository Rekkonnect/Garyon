using Garyon.Functions;
using Polyfills;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

/// <summary>
/// Provides more LINQ-like extensions that are not that likely to be considered
/// candidates for being imported into System.Linq.
/// </summary>
public static class MoreLinqExtensions
{
    /// <inheritdoc cref="Enumerable.Where{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>
    public static IEnumerable<T> WherePredicate<T>(this IEnumerable<T> source, Predicate<T> predicate) => source.Where(new Func<T, bool>(predicate));

    /// <summary>
    /// Filters out the contents of the enumerable, if a predicate is provided.
    /// Otherwise, returns the source enumerable as-is.
    /// </summary>
    public static IEnumerable<T> WhereOrSource<T>(this IEnumerable<T> source, Func<T, bool>? predicate)
    {
        return predicate is null ? source : source.Where(predicate);
    }

    /// <summary>
    /// Selects the enumerable's items based on a selector. If the selector is
    /// <see langword="null"/>, a <see langword="null"/> enumerable is returned
    /// instead.
    /// </summary>
    public static IEnumerable<TResult>? SelectOrDefault<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, TResult>? selector)
    {
        return selector is null ? null : source.Select(selector);
    }

    /// <summary>
    /// Gets the only element of the sequence, if it only has one element,
    /// otherwise returns <see langword="default"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the sequence.
    /// </typeparam>
    /// <param name="source">
    /// The source sequence.
    /// </param>
    /// <returns>
    /// The only element of the sequence, or <see langword="default"/>.
    /// </returns>
    public static T? OnlyOrDefault<T>(this IEnumerable<T>? source)
    {
        if (source is null)
            return default;

        T? first = default;
        bool hasElements = false;

        foreach (var s in source)
        {
            if (hasElements)
                return default;

            first = s;
            hasElements = true;
        }

        return first;
    }

    /// <summary>
    /// Determines whether there are any elements contained in any sequence of
    /// the sequences that are provided.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements stored in the sequences.
    /// </typeparam>
    /// <param name="source">
    /// The sequence of sequences to analyze on whether any elements are
    /// contained.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if there is at least one element in any of the
    /// sequences, otherwise <see langword="false"/>.
    /// </returns>
    public static bool AnyDeep<T>(this IEnumerable<IEnumerable<T>> source)
    {
        foreach (var e in source)
            if (e.Any())
                return true;
        return false;
    }

    /// <summary>
    /// Performs an action for each element in a collection, while also
    /// performing an additional specified action for the first and the last
    /// elements.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the collection.
    /// </typeparam>
    /// <param name="source">
    /// The source collection whose elements to perform the actions on. If the
    /// collection is empty, no action will be taken.
    /// </param>
    /// <param name="firstElementAction">
    /// The action to perform on the first element.
    /// </param>
    /// <param name="lastElementAction">
    /// The action to perform on the last element.
    /// </param>
    /// <param name="enumerationAction">
    /// The action to perform on all enumerated elements. This is also applied
    /// to the first and the last elements.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="source"/> contained at least
    /// one element and actions were performed, otherwise
    /// <see langword="false"/>.
    /// </returns>
    public static bool EnumeratePerformAction<T>(this IEnumerable<T> source, Action<T>? firstElementAction, Action<T>? lastElementAction, Action<T> enumerationAction)
    {
        var enumerator = source.GetEnumerator();

        // If there are no elements in the source, early exist without performing actions
        if (!enumerator.MoveNext())
            return false;

        var first = enumerator.Current;
        if (firstElementAction is not null)
            firstElementAction(first);

        T last;

        do
        {
            last = enumerator.Current;
            enumerationAction(last);
        }
        while (enumerator.MoveNext());

        if (lastElementAction is not null)
            lastElementAction(last);

        return true;
    }

    /// <summary>
    /// Determines whether there are at least a specified number of occurrences
    /// of elements in an enumerable.
    /// </summary>
    /// <param name="minCount">
    /// The minimum number of elements that must exist in the enumerable.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the provided collection contains at least
    /// <paramref name="minCount"/> elements, otherwise <see langword="false"/>.
    /// </returns>
    public static bool CountAtLeast<T>(this IEnumerable<T> source, int minCount)
    {
        return source.Skip(minCount).Any();
    }

    /// <summary>
    /// Determines whether there are at most a specified number of occurrences
    /// of elements in an enumerable.
    /// </summary>
    /// <param name="maxCount">
    /// The maximum number of elements that must exist in the enumerable.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the provided collection contains at most
    /// <paramref name="maxCount"/> elements, otherwise <see langword="false"/>.
    /// </returns>
    public static bool CountAtMost<T>(this IEnumerable<T> source, int maxCount)
    {
        return source.Skip(maxCount).HasNone();
    }

    /// <summary>
    /// Determines whether there are at least a specified number of occurrences
    /// of elements meeting a condition in a collection.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the collection.
    /// </typeparam>
    /// <param name="source">
    /// The source collection whose elements to compare against the condition.
    /// </param>
    /// <param name="predicate">
    /// The condition that the elements must meet.
    /// </param>
    /// <param name="occurrences">
    /// The minimum number of elements that must meet the condition.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the provided collection contains at least
    /// <paramref name="occurrences"/> elements that meet the given condition,
    /// otherwise <see langword="false"/>.
    /// </returns>
    public static bool CountAtLeast<T>(this IEnumerable<T> source, Func<T, bool> predicate, int occurrences)
    {
        return source.Where(predicate).CountAtLeast(occurrences);
    }

    /// <summary>
    /// Determines whether there are at most a specified number of occurrences
    /// of elements meeting a condition in a collection.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the collection.
    /// </typeparam>
    /// <param name="source">
    /// The source collection whose elements to compare against the condition.
    /// </param>
    /// <param name="predicate">
    /// The condition that the elements must meet.
    /// </param>
    /// <param name="occurrences">
    /// The minimum number of elements that must meet the condition.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the provided collection contains at least
    /// <paramref name="occurrences"/> elements that meet the given condition,
    /// otherwise <see langword="false"/>.
    /// </returns>
    public static bool CountAtMost<T>(this IEnumerable<T> source, Func<T, bool> predicate, int occurrences)
    {
        return source.Where(predicate).CountAtMost(occurrences);
    }

    /// <summary>
    /// Determines whether there are exactly a specified number of occurrences
    /// of elements in an enumerable.
    /// </summary>
    /// <param name="count">
    /// The exact number of elements that must exist in the enumerable.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the provided collection contains exactly
    /// <paramref name="count"/> elements, otherwise <see langword="false"/>.
    /// </returns>
    public static bool CountExactly<T>(this IEnumerable<T> source, int count)
    {
        if (count < 0)
            return false;

        if (source.TryGetNonEnumeratedCount(out var quickCount))
        {
            return quickCount == count;
        }

        using var enumerator = source.GetEnumerator();

        for (int i = 0; i < count; i++)
            if (!enumerator.MoveNext())
                return false;

        return !enumerator.MoveNext();
    }

    /// <summary>
    /// Determines whether there are exactly a specified number of occurrences
    /// of elements meeting a condition in a collection.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the collection.
    /// </typeparam>
    /// <param name="source">
    /// The source collection whose elements to compare against the condition.
    /// </param>
    /// <param name="predicate">
    /// The condition that the elements must meet.
    /// </param>
    /// <param name="occurrences">
    /// The exact number of elements that must meet the given condition.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the provided collection contains exactly
    /// <paramref name="occurrences"/> elements that meet the given condition,
    /// otherwise <see langword="false"/>.
    /// </returns>
    public static bool CountExactly<T>(this IEnumerable<T> source, Func<T, bool> predicate, int occurrences)
    {
        return source.Where(predicate).CountExactly(occurrences);
    }

    /// <summary>
    /// Determines whether there are between a specified range (inclusive) of
    /// occurrences of elements in an enumerable.
    /// </summary>
    /// <param name="minCount">
    /// The minimum number of elements that must exist in the enumerable.
    /// </param>
    /// <param name="maxCount">
    /// The maximum number of elements that may exist in the enumerable.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the provided collection contains between
    /// <paramref name="minCount"/> and <paramref name="maxCount"/> (inclusive)
    /// elements, otherwise <see langword="false"/>.
    /// </returns>
    public static bool CountBetween<T>(this IEnumerable<T> source, int minCount, int maxCount)
    {
        if (minCount > maxCount)
            return false;
        if (maxCount < 0)
            return false;
        if (minCount < 0)
            minCount = 0;

        if (source.TryGetNonEnumeratedCount(out var quickCount))
        {
            return quickCount >= minCount && quickCount <= maxCount;
        }

        using var enumerator = source.GetEnumerator();

        int count = 0;
        while (count <= maxCount && enumerator.MoveNext())
            count++;

        return count >= minCount && count <= maxCount;
    }

    /// <summary>
    /// Determines whether there are between a specified range (inclusive) of
    /// occurrences of elements meeting a condition in a collection.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the collection.
    /// </typeparam>
    /// <param name="source">
    /// The source collection whose elements to compare against the condition.
    /// </param>
    /// <param name="predicate">
    /// The condition that the elements must meet.
    /// </param>
    /// <param name="minOccurrences">
    /// The minimum number of elements that must meet the given condition.
    /// </param>
    /// <param name="maxOccurrences">
    /// The maximum number of elements that may meet the given condition.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the provided collection contains between
    /// <paramref name="minOccurrences"/> and <paramref name="maxOccurrences"/>
    /// (inclusive) elements that meet the given condition, otherwise
    /// <see langword="false"/>.
    /// </returns>
    public static bool CountBetween<T>(this IEnumerable<T> source, Func<T, bool> predicate, int minOccurrences, int maxOccurrences)
    {
        return source.Where(predicate).CountBetween(minOccurrences, maxOccurrences);
    }

    /// <summary>
    /// Gets the count of elements that are not <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements stored in the <seealso cref="IEnumerable{T}"/>.
    /// </typeparam>
    /// <param name="source">
    /// The <seealso cref="IEnumerable{T}"/> whose elements to iterate.
    /// </param>
    /// <returns>
    /// The total number of elements that are not <see langword="null"/>.
    /// </returns>
    public static int NotNullCount<T>(this IEnumerable<T> source)
    {
        return source.Count(Predicates.NotNull);
    }
    /// <summary>
    /// Gets the count of elements that are not <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements stored in the <seealso cref="IEnumerable{T}"/>.
    /// </typeparam>
    /// <param name="source">
    /// The <seealso cref="IEnumerable{T}"/> whose elements to iterate.
    /// </param>
    /// <returns>
    /// The total number of elements that are not <see langword="null"/>.
    /// </returns>
    public static int NullCount<T>(this IEnumerable<T> source)
    {
        return source.Count(Predicates.Null);
    }
    /// <summary>
    /// Filters the items and returns only those that are not null.
    /// </summary>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source)
    {
        return source.Where(Predicates.NotNull)!;
    }

    /// <summary>
    /// Checks all elements against a predicate and returns only those elements
    /// that do NOT meet the condition.
    /// </summary>
    /// <param name="source">
    /// The sequence of elements to filter.
    /// </param>
    /// <param name="predicate">
    /// A function to test each element for a condition. Elements for which this
    /// function returns <see langword="false"/> are included in the result.
    /// </param>
    public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        return source.Where(p => !predicate(p));
    }

    /// <summary>
    /// Applies a function to the given value and returns the result.
    /// </summary>
    /// <typeparam name="TSource">
    /// The type of the source value.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the result value.
    /// </typeparam>
    /// <param name="source">
    /// The source value.
    /// </param>
    /// <param name="function">
    /// The function to apply to the source value.
    /// </param>
    /// <returns>
    /// The resulting value after applying <paramref name="function"/> to
    /// <paramref name="source"/>.
    /// </returns>
    public static TResult Apply<TSource, TResult>(this TSource source, Func<TSource, TResult> function)
    {
        return function(source);
    }

    /// <summary>
    /// Applies a function to the given value and returns the same value.
    /// </summary>
    /// <typeparam name="TSource">
    /// The type of the source value.
    /// </typeparam>
    /// <param name="source">
    /// The source value.
    /// </param>
    /// <param name="function">
    /// The function to apply to the source value.
    /// </param>
    /// <returns>
    /// The same source value for chaining purposes.
    /// </returns>
    public static TSource Apply<TSource>(this TSource source, Action<TSource> function)
    {
        function(source);
        return source;
    }

    /// <summary>
    /// Filters all groupings and returns only those that have non-null keys.
    /// </summary>
    public static IEnumerable<IGrouping<TKey, TValue>> WhereNotNullKeys<TKey, TValue>(
        this IEnumerable<IGrouping<TKey?, TValue>> grouping)
    {
        return grouping
            .Where(s => s.Key is not null)!
            .AsEnumerable<IGrouping<TKey, TValue>>()
            ;
    }

    /// <summary>
    /// Zips both sequences into a sequence of tuples.
    /// </summary>
    public static IEnumerable<(TA First, TB Second)> Zip<TA, TB>(
        this IEnumerable<TA> source,
        IEnumerable<TB> other)
    {
        return source.Zip(other, static (a, b) => (a, b));
    }

    /// <summary>
    /// Converts all groupings to a dictionary, with the values being a
    /// <see cref="List{T}"/>.
    /// </summary>
    public static Dictionary<TKey, List<TValue>> ToListDictionary<TKey, TValue>(
        this IEnumerable<IGrouping<TKey, TValue>> groupings)
        where TKey : notnull
    {
        return groupings.ToDictionary(s => s.Key, s => s.ToList());
    }

}
