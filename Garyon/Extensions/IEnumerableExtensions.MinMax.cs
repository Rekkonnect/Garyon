#if !HAS_INUMBER

using Garyon.Functions;
using Garyon.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Garyon.Extensions;

/// <summary>
/// Contains extensions for the <seealso cref="IEnumerable{T}"/> interface.
/// </summary>
public static partial class IEnumerableExtensions
{
    // Behold the true copy-paste hell
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<byte>? MinMax(this IEnumerable<byte> source)
    {
        var hasAny = false;
        byte min = byte.MaxValue;
        byte max = byte.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;

            hasAny = true;
        }

        if (!hasAny)
        {
            return null;
        }
        return new(min, max);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<sbyte>? MinMax(this IEnumerable<sbyte> source)
    {
        var hasAny = false;
        sbyte min = sbyte.MaxValue;
        sbyte max = sbyte.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;

            hasAny = true;
        }

        if (!hasAny)
        {
            return null;
        }
        return new(min, max);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<short>? MinMax(this IEnumerable<short> source)
    {
        var hasAny = false;
        short min = short.MaxValue;
        short max = short.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;

            hasAny = true;
        }

        if (!hasAny)
        {
            return null;
        }
        return new(min, max);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<ushort>? MinMax(this IEnumerable<ushort> source)
    {
        var hasAny = false;
        ushort min = ushort.MaxValue;
        ushort max = ushort.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;

            hasAny = true;
        }

        if (!hasAny)
        {
            return null;
        }
        return new(min, max);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<int>? MinMax(this IEnumerable<int> source)
    {
        var hasAny = false;
        int min = int.MaxValue;
        int max = int.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;

            hasAny = true;
        }

        if (!hasAny)
        {
            return null;
        }
        return new(min, max);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<uint>? MinMax(this IEnumerable<uint> source)
    {
        var hasAny = false;
        uint min = uint.MaxValue;
        uint max = uint.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;

            hasAny = true;
        }

        if (!hasAny)
        {
            return null;
        }
        return new(min, max);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<long>? MinMax(this IEnumerable<long> source)
    {
        var hasAny = false;
        long min = long.MaxValue;
        long max = long.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;

            hasAny = true;
        }

        if (!hasAny)
        {
            return null;
        }
        return new(min, max);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<ulong>? MinMax(this IEnumerable<ulong> source)
    {
        var hasAny = false;
        ulong min = ulong.MaxValue;
        ulong max = ulong.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;

            hasAny = true;
        }

        if (!hasAny)
        {
            return null;
        }
        return new(min, max);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<float>? MinMax(this IEnumerable<float> source)
    {
        var hasAny = false;
        float min = float.MaxValue;
        float max = float.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;

            hasAny = true;
        }

        if (!hasAny)
        {
            return null;
        }
        return new(min, max);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<double>? MinMax(this IEnumerable<double> source)
    {
        var hasAny = false;
        double min = double.MaxValue;
        double max = double.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;

            hasAny = true;
        }

        if (!hasAny)
        {
            return null;
        }
        return new(min, max);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<decimal>? MinMax(this IEnumerable<decimal> source)
    {
        var hasAny = false;
        decimal min = decimal.MaxValue;
        decimal max = decimal.MinValue;

        foreach (var v in source)
        {
            if (v < min)
                min = v;
            if (v > max)
                max = v;

            hasAny = true;
        }

        if (!hasAny)
        {
            return null;
        }
        return new(min, max);
    }

    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values of the objects in the enumerable that are
    /// non-<see langword="null"/>, otherwise <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<byte>? MinMax(this IEnumerable<byte?> source)
    {
        var filtered = source.GetValuedElements();
        return MinMax(filtered);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values of the objects in the enumerable that are
    /// non-<see langword="null"/>, otherwise <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<sbyte>? MinMax(this IEnumerable<sbyte?> source)
    {
        var filtered = source.GetValuedElements();
        return MinMax(filtered);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values of the objects in the enumerable that are
    /// non-<see langword="null"/>, otherwise <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<short>? MinMax(this IEnumerable<short?> source)
    {
        var filtered = source.GetValuedElements();
        return MinMax(filtered);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values of the objects in the enumerable that are
    /// non-<see langword="null"/>, otherwise <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<ushort>? MinMax(this IEnumerable<ushort?> source)
    {
        var filtered = source.GetValuedElements();
        return MinMax(filtered);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values of the objects in the enumerable that are
    /// non-<see langword="null"/>, otherwise <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<int>? MinMax(this IEnumerable<int?> source)
    {
        var filtered = source.GetValuedElements();
        return MinMax(filtered);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values of the objects in the enumerable that are
    /// non-<see langword="null"/>, otherwise <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<uint>? MinMax(this IEnumerable<uint?> source)
    {
        var filtered = source.GetValuedElements();
        return MinMax(filtered);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values of the objects in the enumerable that are
    /// non-<see langword="null"/>, otherwise <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<long>? MinMax(this IEnumerable<long?> source)
    {
        var filtered = source.GetValuedElements();
        return MinMax(filtered);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values of the objects in the enumerable that are
    /// non-<see langword="null"/>, otherwise <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<ulong>? MinMax(this IEnumerable<ulong?> source)
    {
        var filtered = source.GetValuedElements();
        return MinMax(filtered);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values of the objects in the enumerable that are
    /// non-<see langword="null"/>, otherwise <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<float>? MinMax(this IEnumerable<float?> source)
    {
        var filtered = source.GetValuedElements();
        return MinMax(filtered);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values of the objects in the enumerable that are
    /// non-<see langword="null"/>, otherwise <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<double>? MinMax(this IEnumerable<double?> source)
    {
        var filtered = source.GetValuedElements();
        return MinMax(filtered);
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <returns>
    /// The minimum and maximum values of the objects in the enumerable that are
    /// non-<see langword="null"/>, otherwise <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<decimal>? MinMax(this IEnumerable<decimal?> source)
    {
        var filtered = source.GetValuedElements();
        return MinMax(filtered);
    }

    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<byte>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, byte> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<sbyte>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, sbyte> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<short>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, short> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<ushort>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, ushort> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<int>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<uint>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, uint> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<long>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<ulong>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<float>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<double>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<decimal>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
    {
        return MinMax(source.Select(selector));
    }

    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<byte>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, byte?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<sbyte>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, sbyte?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<short>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, short?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<ushort>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, ushort?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    public static ValueBounds<int>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<uint>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, uint?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<long>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<ulong>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<float>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<double>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
    {
        return MinMax(source.Select(selector));
    }
    /// <summary>
    /// Gets the minimum and maximum values within the enumerable.
    /// </summary>
    /// <param name="source">
    /// The enumerable. It must be non-<see langword="null"/>.
    /// </param>
    /// <param name="selector">
    /// The selector.
    /// </param>
    /// <returns>
    /// The minimum and maximum values, or <see langword="null"/> if the
    /// enumerable had no values.
    /// </returns>
    /// <remarks>
    /// This forces enumeration of the enumerable.
    /// </remarks>
    public static ValueBounds<decimal>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
    {
        return MinMax(source.Select(selector));
    }
}

#endif
