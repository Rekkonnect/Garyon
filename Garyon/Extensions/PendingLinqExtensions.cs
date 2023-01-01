using Garyon.Exceptions;
using System;
using System.Collections.Generic;

namespace Garyon.Extensions;

/// <summary>Contains functions that might be moved to dotnet/runtime (#46042).</summary>
public static class PendingLinqExtensions
{
    // When this goes to System.Linq.Enumerable (if at all), change the code responsible for the ThrowHelper
    // TODO: Change the code for the nullable variants of the functions to comply with that of dotnet/runtime

    /// <summary>Computes the sum of a sequence of <seealso cref="uint"/> values.</summary>
    /// <param name="source">A sequence of <seealso cref="uint"/> values to calculate the sum of.</param>
    /// <returns>The sum of the values in the sequence.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
    /// <exception cref="OverflowException">The sum is larger than <seealso cref="uint.MaxValue"/>.</exception>
    public static uint Sum(this IEnumerable<uint> source)
    {
        if (source == null)
        {
            ThrowHelper.Throw<ArgumentNullException>();
        }

        uint sum = 0;
        checked
        {
            foreach (uint v in source)
            {
                sum += v;
            }
        }

        return sum;
    }

    /// <summary>Computes the sum of a sequence of nullable <seealso cref="uint"/> values.</summary>
    /// <param name="source">A sequence of nullable <seealso cref="uint"/> values to calculate the sum of.</param>
    /// <returns>The sum of the values in the sequence.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
    /// <exception cref="OverflowException">The sum is larger than <seealso cref="uint.MaxValue"/>.</exception>
    public static uint? Sum(this IEnumerable<uint?> source)
    {
        if (source == null)
        {
            ThrowHelper.Throw<ArgumentNullException>();
        }

        uint? sum = 0;
        checked
        {
            foreach (uint? v in source)
            {
                sum += v;
            }
        }

        return sum;
    }

    /// <summary>Computes the sum of a sequence of <seealso cref="ulong"/> values.</summary>
    /// <param name="source">A sequence of <seealso cref="ulong"/> values to calculate the sum of.</param>
    /// <returns>The sum of the values in the sequence.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
    /// <exception cref="OverflowException">The sum is larger than <seealso cref="ulong.MaxValue"/>.</exception>
    public static ulong Sum(this IEnumerable<ulong> source)
    {
        if (source == null)
        {
            ThrowHelper.Throw<ArgumentNullException>();
        }

        ulong sum = 0;
        checked
        {
            foreach (ulong v in source)
            {
                sum += v;
            }
        }

        return sum;
    }

    /// <summary>Computes the sum of a sequence of nullable <seealso cref="ulong"/> values.</summary>
    /// <param name="source">A sequence of nullable <seealso cref="ulong"/> values to calculate the sum of.</param>
    /// <returns>The sum of the values in the sequence.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
    /// <exception cref="OverflowException">The sum is larger than <seealso cref="ulong.MaxValue"/>.</exception>
    public static ulong? Sum(this IEnumerable<ulong?> source)
    {
        if (source == null)
        {
            ThrowHelper.Throw<ArgumentNullException>();
        }

        ulong? sum = 0;
        checked
        {
            foreach (ulong? v in source)
            {
                sum += v;
            }
        }

        return sum;
    }

    // Selectors

    /// <summary>Computes the sum of the sequence of <seealso cref="uint"/> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of values that are used to calculate a sum.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The sum of the projected values.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
    /// <exception cref="OverflowException">The sum is larger than <seealso cref="uint.MaxValue"/>.</exception>
    public static uint Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, uint> selector)
    {
        if (source == null)
        {
            ThrowHelper.Throw<ArgumentNullException>();
        }

        if (selector == null)
        {
            ThrowHelper.Throw<ArgumentNullException>();
        }

        uint sum = 0;
        checked
        {
            foreach (TSource item in source)
            {
                sum += selector(item);
            }
        }

        return sum;
    }

    /// <summary>Computes the sum of the sequence of nullable <seealso cref="uint"/> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of values that are used to calculate a sum.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The sum of the projected values.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
    /// <exception cref="OverflowException">The sum is larger than <seealso cref="uint.MaxValue"/>.</exception>
    public static uint? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, uint?> selector)
    {
        if (source == null)
        {
            ThrowHelper.Throw<ArgumentNullException>();
        }

        if (selector == null)
        {
            ThrowHelper.Throw<ArgumentNullException>();
        }

        uint? sum = 0;
        checked
        {
            foreach (TSource item in source)
            {
                sum += selector(item);
            }
        }

        return sum;
    }

    /// <summary>Computes the sum of the sequence of <seealso cref="ulong"/> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of values that are used to calculate a sum.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The sum of the projected values.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
    /// <exception cref="OverflowException">The sum is larger than <seealso cref="ulong.MaxValue"/>.</exception>
    public static ulong Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong> selector)
    {
        if (source == null)
        {
            ThrowHelper.Throw<ArgumentNullException>();
        }

        if (selector == null)
        {
            ThrowHelper.Throw<ArgumentNullException>();
        }

        ulong sum = 0;
        checked
        {
            foreach (TSource item in source)
            {
                sum += selector(item);
            }
        }

        return sum;
    }

    /// <summary>Computes the sum of the sequence of nullable <seealso cref="ulong"/> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of values that are used to calculate a sum.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The sum of the projected values.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
    /// <exception cref="OverflowException">The sum is larger than <seealso cref="ulong.MaxValue"/>.</exception>
    public static ulong? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong?> selector)
    {
        if (source == null)
        {
            ThrowHelper.Throw<ArgumentNullException>();
        }

        if (selector == null)
        {
            ThrowHelper.Throw<ArgumentNullException>();
        }

        ulong? sum = 0;
        checked
        {
            foreach (TSource item in source)
            {
                sum += selector(item);
            }
        }

        return sum;
    }
}
