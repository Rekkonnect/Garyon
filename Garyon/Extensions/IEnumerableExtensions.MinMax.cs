#if !HAS_INUMBER

using Garyon.Functions;
using Garyon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

/// <summary>Contains extensions for the <seealso cref="IEnumerable{T}"/> interface.</summary>
public static partial class IEnumerableExtensions
{
    // Behold the true copy-paste hell
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<byte> MinMax(this IEnumerable<byte> source)
    {
        VerifyNonEmptyCollection(source);

        byte min = byte.MaxValue;
        byte max = byte.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;
        }

        return new(min, max);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<sbyte> MinMax(this IEnumerable<sbyte> source)
    {
        VerifyNonEmptyCollection(source);

        sbyte min = sbyte.MaxValue;
        sbyte max = sbyte.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;
        }

        return new(min, max);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<short> MinMax(this IEnumerable<short> source)
    {
        VerifyNonEmptyCollection(source);

        short min = short.MaxValue;
        short max = short.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;
        }

        return new(min, max);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<ushort> MinMax(this IEnumerable<ushort> source)
    {
        VerifyNonEmptyCollection(source);

        ushort min = ushort.MaxValue;
        ushort max = ushort.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;
        }

        return new(min, max);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<int> MinMax(this IEnumerable<int> source)
    {
        VerifyNonEmptyCollection(source);

        int min = int.MaxValue;
        int max = int.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;
        }

        return new(min, max);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<uint> MinMax(this IEnumerable<uint> source)
    {
        VerifyNonEmptyCollection(source);

        uint min = uint.MaxValue;
        uint max = uint.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;
        }

        return new(min, max);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<long> MinMax(this IEnumerable<long> source)
    {
        VerifyNonEmptyCollection(source);

        long min = long.MaxValue;
        long max = long.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;
        }

        return new(min, max);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<ulong> MinMax(this IEnumerable<ulong> source)
    {
        VerifyNonEmptyCollection(source);

        ulong min = ulong.MaxValue;
        ulong max = ulong.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;
        }

        return new(min, max);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<float> MinMax(this IEnumerable<float> source)
    {
        VerifyNonEmptyCollection(source);

        float min = float.MaxValue;
        float max = float.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;
        }

        return new(min, max);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<double> MinMax(this IEnumerable<double> source)
    {
        VerifyNonEmptyCollection(source);

        double min = double.MaxValue;
        double max = double.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;
        }

        return new(min, max);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<decimal> MinMax(this IEnumerable<decimal> source)
    {
        VerifyNonEmptyCollection(source);

        decimal min = decimal.MaxValue;
        decimal max = decimal.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;
        }

        return new(min, max);
    }

    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
    public static MinMaxResult<byte> MinMax(this IEnumerable<byte?> source)
    {
        var filtered = source.Where(e => e.HasValue);
        if (filtered.HasNone())
            return MinMaxResult<byte>.Default;
        return MinMax(filtered, static s => s.Value);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
    public static MinMaxResult<sbyte> MinMax(this IEnumerable<sbyte?> source)
    {
        var filtered = source.Where(e => e.HasValue);
        if (filtered.HasNone())
            return MinMaxResult<sbyte>.Default;
        return MinMax(filtered, static s => s.Value);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
    public static MinMaxResult<short> MinMax(this IEnumerable<short?> source)
    {
        var filtered = source.Where(e => e.HasValue);
        if (filtered.HasNone())
            return MinMaxResult<short>.Default;
        return MinMax(filtered, static s => s.Value);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
    public static MinMaxResult<ushort> MinMax(this IEnumerable<ushort?> source)
    {
        var filtered = source.Where(e => e.HasValue);
        if (filtered.HasNone())
            return MinMaxResult<ushort>.Default;
        return MinMax(filtered, static s => s.Value);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
    public static MinMaxResult<int> MinMax(this IEnumerable<int?> source)
    {
        var filtered = source.Where(e => e.HasValue);
        if (filtered.HasNone())
            return MinMaxResult<int>.Default;
        return MinMax(filtered, static s => s.Value);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
    public static MinMaxResult<uint> MinMax(this IEnumerable<uint?> source)
    {
        var filtered = source.Where(e => e.HasValue);
        if (filtered.HasNone())
            return MinMaxResult<uint>.Default;
        return MinMax(filtered, static s => s.Value);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
    public static MinMaxResult<long> MinMax(this IEnumerable<long?> source)
    {
        var filtered = source.Where(e => e.HasValue);
        if (filtered.HasNone())
            return MinMaxResult<long>.Default;
        return MinMax(filtered, static s => s.Value);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
    public static MinMaxResult<ulong> MinMax(this IEnumerable<ulong?> source)
    {
        var filtered = source.Where(e => e.HasValue);
        if (filtered.HasNone())
            return MinMaxResult<ulong>.Default;
        return MinMax(filtered, static s => s.Value);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
    public static MinMaxResult<float> MinMax(this IEnumerable<float?> source)
    {
        var filtered = source.Where(e => e.HasValue);
        if (filtered.HasNone())
            return MinMaxResult<float>.Default;
        return MinMax(filtered, static s => s.Value);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
    public static MinMaxResult<double> MinMax(this IEnumerable<double?> source)
    {
        var filtered = source.Where(e => e.HasValue);
        if (filtered.HasNone())
            return MinMaxResult<double>.Default;
        return MinMax(filtered, static s => s.Value);
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
    public static MinMaxResult<decimal> MinMax(this IEnumerable<decimal?> source)
    {
        var filtered = source.Where(e => e.HasValue);
        if (filtered.HasNone())
            return MinMaxResult<decimal>.Default;
        return MinMax(filtered, static s => s.Value);
    }

    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<byte> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, byte> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<sbyte> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, sbyte> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<short> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, short> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<ushort> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, ushort> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<int> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<uint> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, uint> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<long> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<ulong> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<float> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<double> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<decimal> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
    {
        return MinMax(source.Select(selector));
    }

    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<byte> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, byte?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<sbyte> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, sbyte?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<short> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, short?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<ushort> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, ushort?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<int> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<uint> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, uint?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<long> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<ulong> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<float> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<double> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<decimal> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
    {
        return MinMax(source.Select(selector));
    }
}

#endif
