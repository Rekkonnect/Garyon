using Garyon.Functions;
using Garyon.Objects.Enumerators;
using Polyfills;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

public static partial class IEnumerableExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        /// <summary>
        /// Filters out the provided collection's elements based on the given
        /// predicate, and separates the matched from the unmatched ones,
        /// storing them in the respective collections.
        /// </summary>
        /// <param name="predicate">
        /// The predicate based on which to filter the elements out.
        /// </param>
        /// <param name="matched">
        /// The new collection that will contain the elements that matched the
        /// predicate.
        /// </param>
        /// <param name="unmatched">
        /// The new collection that will contain the elements that did not match
        /// the predicate.
        /// </param>
        public void Dissect(Predicate<T> predicate, out IEnumerable<T> matched, out IEnumerable<T> unmatched)
        {
            if (source.HasNone())
            {
                matched = [];
                unmatched = [];
                return;
            }

            var minCount = source.GetNonEnumeratedCountOrDefault();
            var matchedList = new List<T>(minCount);
            var unmatchedList = new List<T>(minCount);

            matched = matchedList;
            unmatched = unmatchedList;

            foreach (var element in source)
            {
                var targetList = predicate(element) ? matchedList : unmatchedList;
                targetList.Add(element);
            }
        }

        /// <summary>
        /// Determines whether all elements in a sequence are distinct according to a value selector.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if all elements in the sequence have distinct values
        /// as returned from the value selector; otherwise, <see langword="false"/>.
        /// If the collection is empty, <see langword="true"/> is returned.
        /// </returns>
        public bool AllDistinctBy<TValue>(
            Func<T, TValue> selector)
        {
            int distinctCount = source.DistinctBy(selector).Count();
            int sourceCount = source.Count();
            return sourceCount == distinctCount;
        }

        /// <summary>
        /// Enumerates a collection until the first duplicate item is found.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if all elements in the sequence have distinct
        /// values as returned from the value selector; otherwise,
        /// <see langword="false"/>. If the collection is empty,
        /// <see langword="true"/> is returned.
        /// </returns>
        /// <remarks>
        /// This is a very helpful method to avoid infinite loops on
        /// lazily-evaluated collections with potentially recursive data.
        /// </remarks>
        public IEnumerable<T> UntilFirstRecursive()
        {
            var items = new HashSet<T>();

            foreach (var item in source)
            {
                var added = items.Add(item);
                if (!added)
                {
                    yield break;
                }

                yield return item;
            }
        }

        /// <summary>
        /// Concatenates a given sequence of elements with another single
        /// element, appending it to the end of the enumerated result.
        /// </summary>
        /// <param name="value">
        /// The single value to concatenate with the sequence.
        /// </param>
        /// <returns>
        /// The original sequence concatenated with the single element.
        /// </returns>
        public IEnumerable<T> ConcatSingleValue(T value)
        {
            return source.Concat(new SingleElementCollection<T>(value));
        }

        /// <inheritdoc cref="Enumerable.Concat{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/>
        public IEnumerable<T> Concat(params IReadOnlyList<T> second)
        {
            return source.Concat((IEnumerable<T>)second);
        }
    }

    extension<T>(IEnumerable<T?> source)
        where T : struct
    {
        /// <summary>
        /// Gets the values of the non-<see langword="null"/> elements in the
        /// provided collection of nullable struct elements.
        /// </summary>
        /// <returns>
        /// The values of the non-<see langword="null"/> elements in the
        /// provided collection.
        /// </returns>
        public IEnumerable<T> GetValuedElements()
        {
            return source.WhereNotNull().Select(Selectors.ValueReturner);
        }
    }

    extension<T>(IEnumerable<T?> source)
        where T : class
    {
        /// <summary>
        /// Casts the enumerable into one with non-<see langword="null"/>
        /// reference-type elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This cast does not actually perform any filtering, and the
        /// collection may still contain <see langword="null"/> values if not
        /// filtered before.
        /// </para>
        /// <para>
        /// To properly filter for <see langword="null"/> values, use
        /// <see cref="MoreLinqExtensions.WhereNotNull{T}(IEnumerable{T})"/> or
        /// an equivalent filter.
        /// </para>
        /// </remarks>
        public IEnumerable<T> AsNonNull()
        {
            return source.Cast<T>();
        }
    }

    extension<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source)
    {
        /// <summary>
        /// Projects each <seealso cref="KeyValuePair{TKey, TValue}"/> in the
        /// sequence into a new <seealso cref="KeyValuePair{TKey, TValue}"/>
        /// with keys selected from the provided selector function.
        /// </summary>
        /// <typeparam name="TNewKey">
        /// The type of the key in the new
        /// <seealso cref="KeyValuePair{TKey, TValue}"/> sequence.
        /// </typeparam>
        /// <param name="keySelector">
        /// The function that transforms the initial <typeparamref name="TKey"/>
        /// into a <typeparamref name="TNewKey"/>.
        /// </param>
        /// <returns>
        /// The projected sequence.
        /// </returns>
        public IEnumerable<KeyValuePair<TNewKey, TValue>> SelectKeys<TNewKey>(
            Func<TKey, TNewKey> keySelector)
        {
            return source.Select(kvp => kvp.WithKey(keySelector(kvp.Key)));
        }

        /// <summary>
        /// Projects each <seealso cref="KeyValuePair{TKey, TValue}"/> in the
        /// sequence into a new <seealso cref="KeyValuePair{TKey, TValue}"/>
        /// with values selected from the provided selector function.
        /// </summary>
        /// <typeparam name="TNewValue">
        /// The type of the value in the new
        /// <seealso cref="KeyValuePair{TKey, TValue}"/> sequence.
        /// </typeparam>
        /// <param name="valueSelector">
        /// The function that transforms the initial
        /// <typeparamref name="TValue"/> into a
        /// <typeparamref name="TNewValue"/>.
        /// </param>
        /// <returns>
        /// The projected sequence.
        /// </returns>
        public IEnumerable<KeyValuePair<TKey, TNewValue>> SelectValues<TNewValue>(
            Func<TValue, TNewValue> valueSelector)
        {
            return source.Select(kvp => kvp.WithValue(valueSelector(kvp.Value)));
        }
    }
}
