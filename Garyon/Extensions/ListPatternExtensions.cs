#if HAS_SPAN
using System;
#endif

using System.Collections.Generic;

namespace Garyon.Extensions
{
    /// <summary>
    /// Provides extensions for commonly-known types using list patterns.
    /// </summary>
    public static class ListPatternExtensions
    {
        /// <summary>
        /// Tries to get a single element from the given read-only list
        /// of a specified type.
        /// </summary>
        /// <typeparam name="TBase">The type of the elements in the list.</typeparam>
        /// <typeparam name="TDerived">
        /// The type of the single element that must be matched.
        /// </typeparam>
        /// <param name="list">
        /// The read-only list whose elements to evaluate.
        /// </param>
        /// <param name="single">
        /// The single element of the specified type to get. If the list does not
        /// contain a single element, or the single element is not of the specified
        /// type, this is <see langword="default"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the list only contains a single element that
        /// is of type <typeparamref name="TDerived"/>, otherwise <see langword="false"/>.
        /// </returns>
        public static bool TryGetSingle<TBase, TDerived>(this IReadOnlyList<TBase> list, out TDerived single)
            where TDerived : TBase
        {
            if (list is [TDerived singleDerived])
            {
                single = singleDerived;
                return true;
            }
            single = default;
            return false;
        }
        /// <summary>
        /// Tries to get a single element from the given array
        /// of a specified type.
        /// </summary>
        /// <typeparam name="TBase">The type of the elements in the array.</typeparam>
        /// <typeparam name="TDerived">
        /// The type of the single element that must be matched.
        /// </typeparam>
        /// <param name="array">
        /// The array whose elements to evaluate.
        /// </param>
        /// <param name="single">
        /// The single element of the specified type to get. If the array does not
        /// contain a single element, or the single element is not of the specified
        /// type, this is <see langword="default"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the array only contains a single element that
        /// is of type <typeparamref name="TDerived"/>, otherwise <see langword="false"/>.
        /// </returns>
        public static bool TryGetSingle<TBase, TDerived>(this TBase[] array, out TDerived single)
            where TDerived : TBase
        {
            if (array is [TDerived singleDerived])
            {
                single = singleDerived;
                return true;
            }
            single = default;
            return false;
        }

#if HAS_SPAN
        /// <summary>
        /// Tries to get a single element from the given span
        /// of a specified type.
        /// </summary>
        /// <typeparam name="TBase">The type of the elements in the span.</typeparam>
        /// <typeparam name="TDerived">
        /// The type of the single element that must be matched.
        /// </typeparam>
        /// <param name="span">
        /// The span whose elements to evaluate.
        /// </param>
        /// <param name="single">
        /// The single element of the specified type to get. If the span does not
        /// contain a single element, or the single element is not of the specified
        /// type, this is <see langword="default"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the span only contains a single element that
        /// is of type <typeparamref name="TDerived"/>, otherwise <see langword="false"/>.
        /// </returns>
        public static bool TryGetSingle<TBase, TDerived>(this Span<TBase> span, out TDerived single)
            where TDerived : TBase
        {
            if (span is [TDerived singleDerived])
            {
                single = singleDerived;
                return true;
            }
            single = default;
            return false;
        }
        /// <summary>
        /// Tries to get a single element from the given read-only span
        /// of a specified type.
        /// </summary>
        /// <typeparam name="TBase">The type of the elements in the span.</typeparam>
        /// <typeparam name="TDerived">
        /// The type of the single element that must be matched.
        /// </typeparam>
        /// <param name="span">
        /// The read-only span whose elements to evaluate.
        /// </param>
        /// <param name="single">
        /// The single element of the specified type to get. If the span does not
        /// contain a single element, or the single element is not of the specified
        /// type, this is <see langword="default"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the span only contains a single element that
        /// is of type <typeparamref name="TDerived"/>, otherwise <see langword="false"/>.
        /// </returns>
        public static bool TryGetSingle<TBase, TDerived>(this ReadOnlySpan<TBase> span, out TDerived single)
            where TDerived : TBase
        {
            if (span is [TDerived singleDerived])
            {
                single = singleDerived;
                return true;
            }
            single = default;
            return false;
        }
#endif
    }
}

namespace System
{
#if !HAS_SLICES
    // I love this hack
    file struct Index
    {
        public Index(int index) { }
        public Index(int index, bool fromEnd) { }

        public int GetOffset(int offset) => offset;
    }
#endif
}
