using System.Collections.Generic;

namespace Garyon.Extensions;

/// <summary>Contains extensions for calculating the product of a collection.</summary>
public static class EnumerableProducts
{
    /// <summary>Calculates the product of a collection of <seealso cref="int"/> values.</summary>
    /// <param name="source">The collection of values whose product to calculate.</param>
    /// <returns>The product of all the <seealso cref="int"/> values.</returns>
    /// <remarks>Be wary of potential overflows. Consider using the <seealso cref="ProductInt64(IEnumerable{int})"/> method, if appropriate.</remarks>
    public static int Product(this IEnumerable<int> source)
    {
        int product = 1;
        foreach (int value in source)
            product *= value;

        return product;
    }
    /// <summary>Calculates the product of a collection of <seealso cref="uint"/> values.</summary>
    /// <param name="source">The collection of values whose product to calculate.</param>
    /// <returns>The product of all the <seealso cref="uint"/> values.</returns>
    /// <remarks>Be wary of potential overflows. Consider using the <seealso cref="ProductUInt64(IEnumerable{uint})"/> method, if appropriate.</remarks>
    public static uint Product(this IEnumerable<uint> source)
    {
        uint product = 1;
        foreach (uint value in source)
            product *= value;

        return product;
    }
    /// <summary>Calculates the product of a collection of <seealso cref="long"/> values.</summary>
    /// <param name="source">The collection of values whose product to calculate.</param>
    /// <returns>The product of all the <seealso cref="long"/> values.</returns>
    public static long Product(this IEnumerable<long> source)
    {
        long product = 1;
        foreach (long value in source)
            product *= value;

        return product;
    }
    /// <summary>Calculates the product of a collection of <seealso cref="ulong"/> values.</summary>
    /// <param name="source">The collection of values whose product to calculate.</param>
    /// <returns>The product of all the <seealso cref="ulong"/> values.</returns>
    public static ulong Product(this IEnumerable<ulong> source)
    {
        ulong product = 1;
        foreach (ulong value in source)
            product *= value;

        return product;
    }

    /// <summary>Calculates the product of a collection of <seealso cref="int"/> values.</summary>
    /// <param name="source">The collection of values whose product to calculate.</param>
    /// <returns>The product of all the <seealso cref="int"/> values, as a <seealso cref="long"/>.</returns>
    public static long ProductInt64(this IEnumerable<int> source)
    {
        long product = 1;
        foreach (int value in source)
            product *= value;

        return product;
    }
    /// <summary>Calculates the product of a collection of <seealso cref="int"/> values.</summary>
    /// <param name="source">The collection of values whose product to calculate.</param>
    /// <returns>The product of all the <seealso cref="int"/> values, as a <seealso cref="ulong"/>.</returns>
    public static ulong ProductUInt64(this IEnumerable<int> source)
    {
        ulong product = 1;
        foreach (int value in source)
            product *= (uint)value;

        return product;
    }
    /// <summary>Calculates the product of a collection of <seealso cref="uint"/> values.</summary>
    /// <param name="source">The collection of values whose product to calculate.</param>
    /// <returns>The product of all the <seealso cref="uint"/> values, as a <seealso cref="ulong"/>.</returns>
    public static ulong ProductUInt64(this IEnumerable<uint> source)
    {
        ulong product = 1;
        foreach (uint value in source)
            product *= value;

        return product;
    }
}