#if HAS_SPAN

using System;
using System.Collections;
using System.Collections.Generic;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for the <seealso cref="Span{T}"/>
/// and <seealso cref="ReadOnlySpan{T}"/> types.
/// </summary>
public static class SpanExtensions
{
    #region IndexOf
    private static int IndexSlicer(int index, int length, out int nextIndex)
    {
        nextIndex = -1;
        if (index > -1)
        {
            nextIndex = index + length;
        }

        return index;
    }

    public static int IndexOf<TSource>(this Span<TSource> source, TSource delimiter, out int nextIndex)
        where TSource : IEquatable<TSource>
    {
        int index = source.IndexOf(delimiter);
        return IndexSlicer(index, 1, out nextIndex);
    }
    public static int IndexOf<TSource>(this ReadOnlySpan<TSource> source, TSource delimiter, out int nextIndex)
        where TSource : IEquatable<TSource>
    {
        int index = source.IndexOf(delimiter);
        return IndexSlicer(index, 1, out nextIndex);
    }

    public static int IndexOf<TSource>(this Span<TSource> source, Span<TSource> delimiter, out int nextIndex)
        where TSource : IEquatable<TSource>
    {
        int index = source.IndexOf(delimiter);
        return IndexSlicer(index, delimiter.Length, out nextIndex);
    }
    public static int IndexOf<TSource>(this Span<TSource> source, ReadOnlySpan<TSource> delimiter, out int nextIndex)
        where TSource : IEquatable<TSource>
    {
        int index = source.IndexOf(delimiter);
        return IndexSlicer(index, delimiter.Length, out nextIndex);
    }
    public static int IndexOf<TSource>(this ReadOnlySpan<TSource> source, Span<TSource> delimiter, out int nextIndex)
        where TSource : IEquatable<TSource>
    {
        int index = source.IndexOf(delimiter);
        return IndexSlicer(index, delimiter.Length, out nextIndex);
    }
    public static int IndexOf<TSource>(this ReadOnlySpan<TSource> source, ReadOnlySpan<TSource> delimiter, out int nextIndex)
        where TSource : IEquatable<TSource>
    {
        int index = source.IndexOf(delimiter);
        return IndexSlicer(index, delimiter.Length, out nextIndex);
    }
    #endregion

    #region Slicing
    /// <summary>
    /// Gets the slice of the span after the first occurrence
    /// of the specified delimiter.
    /// </summary>
    /// <typeparam name="TSource">The type of the values in the span.</typeparam>
    /// <param name="source">The source span that will be sliced.</param>
    /// <param name="delimiter">The delimiter to find in the span.</param>
    /// <returns>
    /// The slice after the first occurrence of the delimiter if
    /// it is found in the span, otherwise the entire source span.
    /// </returns>
    public static Span<TSource> SliceAfter<TSource>(this Span<TSource> source, TSource delimiter)
        where TSource : IEquatable<TSource>
    {
        source.IndexOf(delimiter, out int startIndex);
        return source.SliceAfterIndex(startIndex);
    }
    /// <summary>
    /// Gets the slice of the span before the first occurrence
    /// of the specified delimiter.
    /// </summary>
    /// <typeparam name="TSource">The type of the values in the span.</typeparam>
    /// <param name="source">The source span that will be sliced.</param>
    /// <param name="delimiter">The delimiter to find in the span.</param>
    /// <returns>
    /// The slice before the first occurrence of the delimiter if
    /// it is found in the span, otherwise the entire source span.
    /// </returns>
    public static Span<TSource> SliceBefore<TSource>(this Span<TSource> source, TSource delimiter)
        where TSource : IEquatable<TSource>
    {
        int endIndex = source.IndexOf(delimiter);
        return source.SliceBeforeIndex(endIndex);
    }

    /// <summary>
    /// Gets the slice of the span between the first occurrences
    /// of the specified delimiters.
    /// </summary>
    /// <typeparam name="TSource">The type of the values in the span.</typeparam>
    /// <param name="source">The source span that will be sliced.</param>
    /// <param name="delimiterStart">
    /// The first delimiter to find in the source span.
    /// </param>
    /// <param name="delimiterEnd">
    /// The second delimiter to find in the sliced span after the
    /// first delimiter.
    /// </param>
    /// <returns>
    /// The slice after the first occurrence of the start delimiter and
    /// before the first occurrence of the end delimiter.
    /// </returns>
    /// <remarks>
    /// This method uses a combination of
    /// <seealso cref="SliceAfter{TSource}(Span{TSource}, TSource)"/>
    /// and <seealso cref="SliceBefore{TSource}(Span{TSource}, TSource)"/>.
    /// </remarks>
    public static Span<TSource> SliceBetween<TSource>(this Span<TSource> source, TSource delimiterStart, TSource delimiterEnd)
        where TSource : IEquatable<TSource>
    {
        source = SliceAfter(source, delimiterStart);
        return SliceBefore(source, delimiterEnd);
    }

    /// <inheritdoc cref="SliceAfter{TSource}(Span{TSource}, TSource)"/>
    public static ReadOnlySpan<TSource> SliceAfter<TSource>(this ReadOnlySpan<TSource> source, TSource delimiter)
        where TSource : IEquatable<TSource>
    {
        source.IndexOf(delimiter, out int startIndex);
        return source.SliceAfterIndex(startIndex);
    }
    /// <inheritdoc cref="SliceBefore{TSource}(Span{TSource}, TSource)"/>
    public static ReadOnlySpan<TSource> SliceBefore<TSource>(this ReadOnlySpan<TSource> source, TSource delimiter)
        where TSource : IEquatable<TSource>
    {
        int endIndex = source.IndexOf(delimiter);
        return source.SliceBeforeIndex(endIndex);
    }

    /// <remarks>
    /// This method uses a combination of
    /// <seealso cref="SliceAfter{TSource}(ReadOnlySpan{TSource}, TSource)"/>
    /// and <seealso cref="SliceBefore{TSource}(ReadOnlySpan{TSource}, TSource)"/>.
    /// </remarks>
    /// <inheritdoc cref="SliceBetween{TSource}(Span{TSource}, TSource, TSource)"/>
    public static ReadOnlySpan<TSource> SliceBetween<TSource>(this ReadOnlySpan<TSource> source, TSource delimiterStart, TSource delimiterEnd)
        where TSource : IEquatable<TSource>
    {
        source = SliceAfter(source, delimiterStart);
        return SliceBefore(source, delimiterEnd);
    }

    /// <inheritdoc cref="SliceAfter{TSource}(Span{TSource}, TSource)"/>
    public static Span<TSource> SliceAfter<TSource>(this Span<TSource> source, Span<TSource> delimiter)
        where TSource : IEquatable<TSource>
    {
        source.IndexOf(delimiter, out int startIndex);
        return source.SliceAfterIndex(startIndex);
    }
    /// <inheritdoc cref="SliceBefore{TSource}(Span{TSource}, TSource)"/>
    public static Span<TSource> SliceBefore<TSource>(this Span<TSource> source, Span<TSource> delimiter)
        where TSource : IEquatable<TSource>
    {
        int endIndex = source.IndexOf(delimiter);
        return source.SliceBeforeIndex(endIndex);
    }

    /// <inheritdoc cref="SliceAfter{TSource}(Span{TSource}, TSource)"/>
    public static Span<TSource> SliceAfter<TSource>(this Span<TSource> source, ReadOnlySpan<TSource> delimiter)
        where TSource : IEquatable<TSource>
    {
        source.IndexOf(delimiter, out int startIndex);
        return source.SliceAfterIndex(startIndex);
    }
    /// <inheritdoc cref="SliceBefore{TSource}(Span{TSource}, TSource)"/>
    public static Span<TSource> SliceBefore<TSource>(this Span<TSource> source, ReadOnlySpan<TSource> delimiter)
        where TSource : IEquatable<TSource>
    {
        int endIndex = source.IndexOf(delimiter);
        return source.SliceBeforeIndex(endIndex);
    }

    /// <inheritdoc cref="SliceAfter{TSource}(Span{TSource}, TSource)"/>
    public static ReadOnlySpan<TSource> SliceAfter<TSource>(this ReadOnlySpan<TSource> source, Span<TSource> delimiter)
        where TSource : IEquatable<TSource>
    {
        source.IndexOf(delimiter, out int startIndex);
        return source.SliceAfterIndex(startIndex);
    }
    /// <inheritdoc cref="SliceBefore{TSource}(Span{TSource}, TSource)"/>
    public static ReadOnlySpan<TSource> SliceBefore<TSource>(this ReadOnlySpan<TSource> source, Span<TSource> delimiter)
        where TSource : IEquatable<TSource>
    {
        int endIndex = source.IndexOf(delimiter);
        return source.SliceBeforeIndex(endIndex);
    }

    /// <inheritdoc cref="SliceAfter{TSource}(Span{TSource}, TSource)"/>
    public static ReadOnlySpan<TSource> SliceAfter<TSource>(this ReadOnlySpan<TSource> source, ReadOnlySpan<TSource> delimiter)
        where TSource : IEquatable<TSource>
    {
        source.IndexOf(delimiter, out int startIndex);
        return source.SliceAfterIndex(startIndex);
    }
    /// <inheritdoc cref="SliceBefore{TSource}(Span{TSource}, TSource)"/>
    public static ReadOnlySpan<TSource> SliceBefore<TSource>(this ReadOnlySpan<TSource> source, ReadOnlySpan<TSource> delimiter)
        where TSource : IEquatable<TSource>
    {
        int endIndex = source.IndexOf(delimiter);
        return source.SliceBeforeIndex(endIndex);
    }

    /// <remarks>
    /// This method uses a combination of
    /// <seealso cref="SliceAfter{TSource}(Span{TSource}, Span{TSource})"/>
    /// and <seealso cref="SliceBefore{TSource}(Span{TSource}, Span{TSource})"/>.
    /// </remarks>
    /// <inheritdoc cref="SliceBetween{TSource}(Span{TSource}, TSource, TSource)"/>
    public static Span<TSource> SliceBetween<TSource>(this Span<TSource> source, Span<TSource> delimiterStart, Span<TSource> delimiterEnd)
        where TSource : IEquatable<TSource>
    {
        source = SliceAfter(source, delimiterStart);
        return SliceBefore(source, delimiterEnd);
    }
    /// <remarks>
    /// This method uses a combination of
    /// <seealso cref="SliceAfter{TSource}(Span{TSource}, Span{TSource})"/>
    /// and <seealso cref="SliceBefore{TSource}(Span{TSource}, ReadOnlySpan{TSource})"/>.
    /// </remarks>
    /// <inheritdoc cref="SliceBetween{TSource}(Span{TSource}, TSource, TSource)"/>
    public static Span<TSource> SliceBetween<TSource>(this Span<TSource> source, Span<TSource> delimiterStart, ReadOnlySpan<TSource> delimiterEnd)
        where TSource : IEquatable<TSource>
    {
        source = SliceAfter(source, delimiterStart);
        return SliceBefore(source, delimiterEnd);
    }
    /// <remarks>
    /// This method uses a combination of
    /// <seealso cref="SliceAfter{TSource}(Span{TSource}, ReadOnlySpan{TSource})"/>
    /// and <seealso cref="SliceBefore{TSource}(Span{TSource}, Span{TSource})"/>.
    /// </remarks>
    /// <inheritdoc cref="SliceBetween{TSource}(Span{TSource}, TSource, TSource)"/>
    public static Span<TSource> SliceBetween<TSource>(this Span<TSource> source, ReadOnlySpan<TSource> delimiterStart, Span<TSource> delimiterEnd)
        where TSource : IEquatable<TSource>
    {
        source = SliceAfter(source, delimiterStart);
        return SliceBefore(source, delimiterEnd);
    }
    /// <remarks>
    /// This method uses a combination of
    /// <seealso cref="SliceAfter{TSource}(Span{TSource}, ReadOnlySpan{TSource})"/>
    /// and <seealso cref="SliceBefore{TSource}(Span{TSource}, ReadOnlySpan{TSource})"/>.
    /// </remarks>
    /// <inheritdoc cref="SliceBetween{TSource}(Span{TSource}, TSource, TSource)"/>
    public static Span<TSource> SliceBetween<TSource>(this Span<TSource> source, ReadOnlySpan<TSource> delimiterStart, ReadOnlySpan<TSource> delimiterEnd)
        where TSource : IEquatable<TSource>
    {
        source = SliceAfter(source, delimiterStart);
        return SliceBefore(source, delimiterEnd);
    }

    /// <remarks>
    /// This method uses a combination of
    /// <seealso cref="SliceAfter{TSource}(ReadOnlySpan{TSource}, Span{TSource})"/>
    /// and <seealso cref="SliceBefore{TSource}(ReadOnlySpan{TSource}, Span{TSource})"/>.
    /// </remarks>
    /// <inheritdoc cref="SliceBetween{TSource}(Span{TSource}, TSource, TSource)"/>
    public static ReadOnlySpan<TSource> SliceBetween<TSource>(this ReadOnlySpan<TSource> source, Span<TSource> delimiterStart, Span<TSource> delimiterEnd)
        where TSource : IEquatable<TSource>
    {
        source = SliceAfter(source, delimiterStart);
        return SliceBefore(source, delimiterEnd);
    }
    /// <remarks>
    /// This method uses a combination of
    /// <seealso cref="SliceAfter{TSource}(ReadOnlySpan{TSource}, Span{TSource})"/>
    /// and <seealso cref="SliceBefore{TSource}(ReadOnlySpan{TSource}, ReadOnlySpan{TSource})"/>.
    /// </remarks>
    /// <inheritdoc cref="SliceBetween{TSource}(Span{TSource}, TSource, TSource)"/>
    public static ReadOnlySpan<TSource> SliceBetween<TSource>(this ReadOnlySpan<TSource> source, Span<TSource> delimiterStart, ReadOnlySpan<TSource> delimiterEnd)
        where TSource : IEquatable<TSource>
    {
        source = SliceAfter(source, delimiterStart);
        return SliceBefore(source, delimiterEnd);
    }
    /// <remarks>
    /// This method uses a combination of
    /// <seealso cref="SliceAfter{TSource}(ReadOnlySpan{TSource}, ReadOnlySpan{TSource})"/>
    /// and <seealso cref="SliceBefore{TSource}(ReadOnlySpan{TSource}, Span{TSource})"/>.
    /// </remarks>
    /// <inheritdoc cref="SliceBetween{TSource}(Span{TSource}, TSource, TSource)"/>
    public static ReadOnlySpan<TSource> SliceBetween<TSource>(this ReadOnlySpan<TSource> source, ReadOnlySpan<TSource> delimiterStart, Span<TSource> delimiterEnd)
        where TSource : IEquatable<TSource>
    {
        source = SliceAfter(source, delimiterStart);
        return SliceBefore(source, delimiterEnd);
    }
    /// <remarks>
    /// This method uses a combination of
    /// <seealso cref="SliceAfter{TSource}(ReadOnlySpan{TSource}, ReadOnlySpan{TSource})"/>
    /// and <seealso cref="SliceBefore{TSource}(ReadOnlySpan{TSource}, ReadOnlySpan{TSource})"/>.
    /// </remarks>
    /// <inheritdoc cref="SliceBetween{TSource}(Span{TSource}, TSource, TSource)"/>
    public static ReadOnlySpan<TSource> SliceBetween<TSource>(this ReadOnlySpan<TSource> source, ReadOnlySpan<TSource> delimiterStart, ReadOnlySpan<TSource> delimiterEnd)
        where TSource : IEquatable<TSource>
    {
        source = SliceAfter(source, delimiterStart);
        return SliceBefore(source, delimiterEnd);
    }

    public static Span<TSource> SliceAfterIndex<TSource>(this Span<TSource> source, int startIndex)
    {
        if (startIndex < 0)
            return source;

        return source[startIndex..];
    }
    public static ReadOnlySpan<TSource> SliceAfterIndex<TSource>(this ReadOnlySpan<TSource> source, int startIndex)
    {
        if (startIndex < 0)
            return source;

        return source[startIndex..];
    }
    public static Span<TSource> SliceBeforeIndex<TSource>(this Span<TSource> source, int endIndex)
    {
        if (endIndex < 0)
            return source;

        return source[..endIndex];
    }
    public static ReadOnlySpan<TSource> SliceBeforeIndex<TSource>(this ReadOnlySpan<TSource> source, int endIndex)
    {
        if (endIndex < 0)
            return source;

        return source[..endIndex];
    }
    #endregion

    #region SplitOnce
    /// <summary>
    /// Splits the given span based on the first occurrence of the
    /// delimiter, returning the left and right slices.
    /// </summary>
    /// <param name="span">The span to delimit.</param>
    /// <param name="delimiter">The delimiter to find in the span.</param>
    /// <param name="left">
    /// The left segment of the span up until before the delimiter.
    /// If the delimiter is not found, this will equal the entire span.
    /// </param>
    /// <param name="right">
    /// The right segment of the span starting from the next character
    /// after the first occurrence of the delimiter. If the delimiter is
    /// not found, this will equal <see langword="default"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the delimiter was found at least once,
    /// otherwise <see langword="false"/>.
    /// </returns>
    public static bool SplitOnce<TSource>(this ReadOnlySpan<TSource> span, TSource delimiter, out ReadOnlySpan<TSource> left, out ReadOnlySpan<TSource> right)
        where TSource : IEquatable<TSource>
    {
        int index = span.IndexOf(delimiter, out int nextIndex);
        return SplitOnce(span, index, nextIndex, out left, out right);
    }

    /// <inheritdoc cref="SplitOnce{TSource}(ReadOnlySpan{TSource}, TSource, out ReadOnlySpan{TSource}, out ReadOnlySpan{TSource})"/>
    public static bool SplitOnce<TSource>(this Span<TSource> span, TSource delimiter, out Span<TSource> left, out Span<TSource> right)
        where TSource : IEquatable<TSource>
    {
        int index = span.IndexOf(delimiter, out int nextIndex);
        return SplitOnce(span, index, nextIndex, out left, out right);
    }

    /// <inheritdoc cref="SplitOnce{TSource}(ReadOnlySpan{TSource}, TSource, out ReadOnlySpan{TSource}, out ReadOnlySpan{TSource})"/>
    public static bool SplitOnce<TSource>(this ReadOnlySpan<TSource> span, ReadOnlySpan<TSource> delimiter, out ReadOnlySpan<TSource> left, out ReadOnlySpan<TSource> right)
        where TSource : IEquatable<TSource>
    {
        int index = span.IndexOf(delimiter, out int nextIndex);
        return SplitOnce(span, index, nextIndex, out left, out right);
    }

    /// <inheritdoc cref="SplitOnce{TSource}(ReadOnlySpan{TSource}, TSource, out ReadOnlySpan{TSource}, out ReadOnlySpan{TSource})"/>
    public static bool SplitOnce<TSource>(this Span<TSource> span, ReadOnlySpan<TSource> delimiter, out Span<TSource> left, out Span<TSource> right)
        where TSource : IEquatable<TSource>
    {
        int index = span.IndexOf(delimiter, out int nextIndex);
        return SplitOnce(span, index, nextIndex, out left, out right);
    }

    /// <inheritdoc cref="SplitOnce{TSource}(ReadOnlySpan{TSource}, TSource, out ReadOnlySpan{TSource}, out ReadOnlySpan{TSource})"/>
    public static bool SplitOnce<TSource>(this ReadOnlySpan<TSource> span, Span<TSource> delimiter, out ReadOnlySpan<TSource> left, out ReadOnlySpan<TSource> right)
        where TSource : IEquatable<TSource>
    {
        int index = span.IndexOf(delimiter, out int nextIndex);
        return SplitOnce(span, index, nextIndex, out left, out right);
    }

    /// <inheritdoc cref="SplitOnce{TSource}(ReadOnlySpan{TSource}, TSource, out ReadOnlySpan{TSource}, out ReadOnlySpan{TSource})"/>
    public static bool SplitOnce<TSource>(this Span<TSource> span, Span<TSource> delimiter, out Span<TSource> left, out Span<TSource> right)
        where TSource : IEquatable<TSource>
    {
        int index = span.IndexOf(delimiter, out int nextIndex);
        return SplitOnce(span, index, nextIndex, out left, out right);
    }

    private static bool SplitOnce<TSource>(this ReadOnlySpan<TSource> span, int index, int nextIndex, out ReadOnlySpan<TSource> left, out ReadOnlySpan<TSource> right)
        where TSource : IEquatable<TSource>
    {
        left = span;
        right = default;

        if (index < 0)
            return false;

        left = span[..index];
        right = span[nextIndex..];
        return true;
    }
    private static bool SplitOnce<TSource>(this Span<TSource> span, int index, int nextIndex, out Span<TSource> left, out Span<TSource> right)
        where TSource : IEquatable<TSource>
    {
        left = span;
        right = default;

        if (index < 0)
            return false;

        left = span[..index];
        right = span[nextIndex..];
        return true;
    }
    #endregion

    #region Split
    // Behold the greatest copy-paste of your life

    /// <summary>
    /// Splits the given span based on a delimiter, and selects each split
    /// section into a target value using a selector.
    /// </summary>
    /// <typeparam name="TSource">The type of the values stored in the span.</typeparam>
    /// <typeparam name="TResult">The type of the selected values.</typeparam>
    /// <param name="source">
    /// The source <seealso cref="ReadOnlySpan{T}"/> that will be delimited by the
    /// given delimiter into multiple segments, and immediately converted.
    /// </param>
    /// <param name="delimiter">The <seealso cref="string"/> ddelimiter.</param>
    /// <param name="selector">
    /// The <seealso cref="ReadOnlySpanSelector{TSource, TResult}"/> instance that will select the
    /// delimited sections into the returned values. Must not be <see langword="null"/>.
    /// </param>
    /// <returns>
    /// The values returned from the selector as a readonly list, in the order
    /// they were found in the source <seealso cref="ReadOnlySpan{T}"/>.
    /// </returns>
    /// <remarks>
    /// Use <see cref="ToCollectionExtensions.ToListOrExisting{T}(IEnumerable{T})"/>
    /// to get the values as a mutable list.
    /// </remarks>
    public static IReadOnlyList<TResult> SplitSelect<TSource, TResult>(this ReadOnlySpan<TSource> source, TSource delimiter, ReadOnlySpanSelector<TSource, TResult> selector)
        where TSource : IEquatable<TSource>
    {
        var results = new List<TResult>();

        var remainingSpan = source;
        while (true)
        {
            int delimiterIndex = remainingSpan.IndexOf(delimiter, out int nextIndex);
            if (delimiterIndex < 0)
                break;

            var delimitedSlice = remainingSpan[..delimiterIndex];
            AddResult(delimitedSlice);
            remainingSpan = remainingSpan[nextIndex..];
        }

        AddResult(remainingSpan);

        return results;

        void AddResult(ReadOnlySpan<TSource> span)
        {
            results.Add(selector(span));
        }
    }
    /// <inheritdoc cref="SplitSelect{TSource, TResult}(ReadOnlySpan{TSource}, TSource, ReadOnlySpanSelector{TSource, TResult})"/>
    public static IReadOnlyList<TResult> SplitSelect<TSource, TResult>(this ReadOnlySpan<TSource> source, ReadOnlySpan<TSource> delimiter, ReadOnlySpanSelector<TSource, TResult> selector)
        where TSource : IEquatable<TSource>
    {
        var results = new List<TResult>();

        var remainingSpan = source;
        while (true)
        {
            int delimiterIndex = remainingSpan.IndexOf(delimiter, out int nextIndex);
            if (delimiterIndex < 0)
                break;

            var delimitedSlice = remainingSpan[..delimiterIndex];
            AddResult(delimitedSlice);
            remainingSpan = remainingSpan[nextIndex..];
        }

        AddResult(remainingSpan);

        return results;

        void AddResult(ReadOnlySpan<TSource> span)
        {
            results.Add(selector(span));
        }
    }
    /// <inheritdoc cref="SplitSelect{TSource, TResult}(ReadOnlySpan{TSource}, TSource, ReadOnlySpanSelector{TSource, TResult})"/>
    public static IReadOnlyList<TResult> SplitSelect<TSource, TResult>(this ReadOnlySpan<TSource> source, Span<TSource> delimiter, ReadOnlySpanSelector<TSource, TResult> selector)
        where TSource : IEquatable<TSource>
    {
        var results = new List<TResult>();

        var remainingSpan = source;
        while (true)
        {
            int delimiterIndex = remainingSpan.IndexOf(delimiter, out int nextIndex);
            if (delimiterIndex < 0)
                break;

            var delimitedSlice = remainingSpan[..delimiterIndex];
            AddResult(delimitedSlice);
            remainingSpan = remainingSpan[nextIndex..];
        }

        AddResult(remainingSpan);

        return results;

        void AddResult(ReadOnlySpan<TSource> span)
        {
            results.Add(selector(span));
        }
    }
    /// <inheritdoc cref="SplitSelect{TSource, TResult}(ReadOnlySpan{TSource}, TSource, ReadOnlySpanSelector{TSource, TResult})"/>
    public static IReadOnlyList<TResult> SplitSelect<TSource, TResult>(this Span<TSource> source, TSource delimiter, ReadOnlySpanSelector<TSource, TResult> selector)
        where TSource : IEquatable<TSource>
    {
        var results = new List<TResult>();

        var remainingSpan = source;
        while (true)
        {
            int delimiterIndex = remainingSpan.IndexOf(delimiter, out int nextIndex);
            if (delimiterIndex < 0)
                break;

            var delimitedSlice = remainingSpan[..delimiterIndex];
            AddResult(delimitedSlice);
            remainingSpan = remainingSpan[nextIndex..];
        }

        AddResult(remainingSpan);

        return results;

        void AddResult(ReadOnlySpan<TSource> span)
        {
            results.Add(selector(span));
        }
    }
    /// <inheritdoc cref="SplitSelect{TSource, TResult}(ReadOnlySpan{TSource}, TSource, ReadOnlySpanSelector{TSource, TResult})"/>
    public static IReadOnlyList<TResult> SplitSelect<TSource, TResult>(this Span<TSource> source, ReadOnlySpan<TSource> delimiter, ReadOnlySpanSelector<TSource, TResult> selector)
        where TSource : IEquatable<TSource>
    {
        var results = new List<TResult>();

        var remainingSpan = source;
        while (true)
        {
            int delimiterIndex = remainingSpan.IndexOf(delimiter, out int nextIndex);
            if (delimiterIndex < 0)
                break;

            var delimitedSlice = remainingSpan[..delimiterIndex];
            AddResult(delimitedSlice);
            remainingSpan = remainingSpan[nextIndex..];
        }

        AddResult(remainingSpan);

        return results;

        void AddResult(ReadOnlySpan<TSource> span)
        {
            results.Add(selector(span));
        }
    }
    /// <inheritdoc cref="SplitSelect{TSource, TResult}(ReadOnlySpan{TSource}, TSource, ReadOnlySpanSelector{TSource, TResult})"/>
    public static IReadOnlyList<TResult> SplitSelect<TSource, TResult>(this Span<TSource> source, Span<TSource> delimiter, ReadOnlySpanSelector<TSource, TResult> selector)
        where TSource : IEquatable<TSource>
    {
        var results = new List<TResult>();

        var remainingSpan = source;
        while (true)
        {
            int delimiterIndex = remainingSpan.IndexOf(delimiter, out int nextIndex);
            if (delimiterIndex < 0)
                break;

            var delimitedSlice = remainingSpan[..delimiterIndex];
            AddResult(delimitedSlice);
            remainingSpan = remainingSpan[nextIndex..];
        }

        AddResult(remainingSpan);

        return results;

        void AddResult(ReadOnlySpan<TSource> span)
        {
            results.Add(selector(span));
        }
    }

    public static SplitSpanEnumerator<TSource> SplitEnumerate<TSource>(
        this ReadOnlySpan<TSource> source, TSource delimiter)
        where TSource : IEquatable<TSource>
    {
        return new SplitSpanEnumerator<TSource>(source, delimiter);
    }

    public static SplitSpanEnumerator<TSource> SplitEnumerate<TSource>(
        this ReadOnlySpan<TSource> source, ReadOnlySpan<TSource> delimiter)
        where TSource : IEquatable<TSource>
    {
        return new SplitSpanEnumerator<TSource>(source, delimiter);
    }

    /// <summary>
    /// Defines a selector delegate that converts a span of values into another value.
    /// </summary>
    /// <typeparam name="TSource">The type of the contained values in the span.</typeparam>
    /// <typeparam name="TResult">The type of the converted value.</typeparam>
    /// <param name="source">The span that is being converted.</param>
    /// <returns>The converted value.</returns>
    public delegate TResult ReadOnlySpanSelector<TSource, TResult>(ReadOnlySpan<TSource> source);
    #endregion

    #region IsWrappedIn
    public static bool IsWrappedIn<T>(
        this ReadOnlySpan<T> source,
        T left,
        T right,
        out ReadOnlySpan<T> inner)
    {
        if (source is [var first, .. var innerSource, var last] && first.Equals(left) && last.Equals(right))
        {
            inner = innerSource;
            return true;
        }

        inner = default;
        return false;
    }

    public static bool IsWrappedIn<T>(
        this ReadOnlySpan<T> source,
        ReadOnlySpan<T> left,
        ReadOnlySpan<T> right,
        out ReadOnlySpan<T> inner)
#if REQUIRES_IEQUATABLE_FOR_SPAN_SEQUENCE_EQUALS
        where T : IEquatable<T>
#endif
    {
        var leftLength = left.Length;
        var rightLength = right.Length;

        var requiredLength = leftLength + rightLength;
        if (source.Length < requiredLength)
        {
            inner = default;
            return false;
        }

        if (source[..leftLength].SequenceEqual(left) &&
            source[^rightLength..].SequenceEqual(right))
        {
            inner = source[leftLength..^rightLength];
            return true;
        }

        inner = default;
        return false;
    }
    #endregion

    #region AdvanceSlice
    public static Span<T> AdvanceSlice<T>(this Span<T> s, int count)
    {
        return s[count..];
    }
    public static void AdvanceSliceRef<T>(this ref Span<T> s, int count)
    {
        s = s.AdvanceSlice(count);
    }

    public static ReadOnlySpan<T> AdvanceSlice<T>(this ReadOnlySpan<T> s, int count)
    {
        return s[count..];
    }
    public static void AdvanceSliceRef<T>(this ref ReadOnlySpan<T> s, int count)
    {
        s = s.AdvanceSlice(count);
    }
    #endregion

    public ref struct SplitSpanEnumerator<TSource>
#if ALLOWS_REF_STRUCTS
        : IEnumerator<ReadOnlySpan<TSource>>
#endif
        where TSource : IEquatable<TSource>
    {
        private readonly ReadOnlySpan<TSource> _source;
        private readonly Delimiter _delimiter;

        private ReadOnlySpan<TSource> _slice;
        private ReadOnlySpan<TSource> _current;

        public readonly ReadOnlySpan<TSource> Current => _current;

#if ALLOWS_REF_STRUCTS
        object IEnumerator.Current
            => throw new InvalidOperationException("Cannot return the current slice as an object");
#endif

        private SplitSpanEnumerator(ReadOnlySpan<TSource> source, Delimiter delimiter)
        {
            _source = source;
            _slice = source;
            _delimiter = delimiter;
        }

        public SplitSpanEnumerator(ReadOnlySpan<TSource> source, TSource delimiter)
            : this(source, new Delimiter(delimiter)) { }

        public SplitSpanEnumerator(ReadOnlySpan<TSource> source, ReadOnlySpan<TSource> delimiter)
            : this(source, new Delimiter(delimiter)) { }

        public bool MoveNext()
        {
            while (true)
            {
                int delimiterIndex = _delimiter.IndexOf(_slice, out int nextIndex);
                if (delimiterIndex < 0)
                {
                    break;
                }

                var delimitedSlice = _slice[..delimiterIndex];
                _current = delimitedSlice;
                _slice = _slice[nextIndex..];
                return true;
            }

            _current = _slice;
            _slice = [];
            return _current.Length > 0;
        }

        public void Reset()
        {
            _slice = _source;
            _current = [];
        }

        public readonly SplitSpanEnumerator<TSource> GetEnumerator() => this;

#if ALLOWS_REF_STRUCTS
        readonly void IDisposable.Dispose()
        {
        }
#endif

        private readonly ref struct Delimiter
        {
            private readonly TSource _single;
            private readonly ReadOnlySpan<TSource> _span;

            public bool IsSingle => _span.Length is 0 or 1;

            public Delimiter(TSource single)
            {
                _single = single;
                _span = [];
            }

            public Delimiter(ReadOnlySpan<TSource> span)
            {
                _span = span;
                _single = default!;
            }

            public int IndexOf(ReadOnlySpan<TSource> source, out int nextIndex)
            {
                if (IsSingle)
                {
                    return source.IndexOf(GetSingleDelimiter(), out nextIndex);
                }

                return source.IndexOf(_span, out nextIndex);
            }

            private TSource GetSingleDelimiter()
            {
                if (_span is [var single])
                {
                    return single;
                }

                return _single;
            }
        }
    }
}

#endif
