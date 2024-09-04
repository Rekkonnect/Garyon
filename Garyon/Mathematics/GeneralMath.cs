﻿using Garyon.Exceptions;
using Garyon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Garyon.Mathematics;

/// <summary>Provides general mathematical functions that are not provided in the <seealso cref="Math"/> class.</summary>
public static class GeneralMath
{
    #region Power
    /// <summary>Calculates the power of a number raised to another number.</summary>
    /// <param name="base">The base.</param>
    /// <param name="exponent">The exponent.</param>
    /// <returns>The result of the power.</returns>
    /// <exception cref="InvalidOperationException">Thrown when both the base and the exponent are 0.</exception>
    public static int Power(int @base, int exponent)
    {
        if (exponent < 0)
            return 0;

        if (@base is 0 && exponent is 0)
            ThrowHelper.Throw<InvalidOperationException>("Cannot raise 0 to the power of 0.");

        if (@base is 0)
            return 0;
        if (@base is 1)
            return 1;
        if (exponent is 0)
            return 1;
        if (exponent is 1)
            return @base;

        int currentPower = @base;
        int result = 1;

        int remainingExponent = exponent;

        while (remainingExponent > 0)
        {
            if ((remainingExponent & 1) > 0)
                result *= currentPower;

            currentPower *= currentPower;
            remainingExponent /= 2;
        }

        return result;
    }
    /// <summary>Calculates the power of a number raised to another number.</summary>
    /// <param name="base">The base.</param>
    /// <param name="exponent">The exponent.</param>
    /// <returns>The result of the power.</returns>
    /// <exception cref="InvalidOperationException">Thrown when both the base and the exponent are 0.</exception>
    public static long Power(long @base, long exponent)
    {
        if (exponent < 0)
            return 0;

        if (@base is 0 && exponent is 0)
            ThrowHelper.Throw<InvalidOperationException>("Cannot raise 0 to the power of 0.");

        if (@base is 0)
            return 0;
        if (@base is 1)
            return 1;
        if (exponent is 0)
            return 1;
        if (exponent is 1)
            return @base;

        long currentPower = @base;
        long result = 1;

        long remainingExponent = exponent;

        while (remainingExponent > 0)
        {
            if ((remainingExponent & 1) > 0)
                result *= currentPower;

            currentPower *= currentPower;
            remainingExponent /= 2;
        }

        return result;
    }
    #endregion

    #region Factorial
    /// <summary>Calculates the factorial of a number.</summary>
    /// <param name="n">The number whose factorial to get.</param>
    /// <returns>The result of the factorial.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided number is negative.</exception>
    public static long Factorial(long n)
    {
        if (n < 0)
            ThrowHelper.Throw<ArgumentException>("The factorial of negative numbers is undefined.");

        return n switch
        {
            0 => 1,
            1 => 1,
            2 => 2,
            3 => 6,
            _ => Optimized(n),
        };

        static long Optimized(long n)
        {
            long result = 1;

            long currentFactor = n;
            long currentIncrement = currentFactor - 2;

            for (long i = 1; i <= n / 2; i++)
            {
                result *= currentFactor;

                currentFactor += currentIncrement;
                currentIncrement -= 2;
            }

            if (n % 2 is 1)
                result *= n / 2 + 1;

            return result;
        }
    }
    /// <summary>Calculates the factorial of a number.</summary>
    /// <param name="n">The number whose factorial to get. If it is not a round number, it will be rounded according to the <seealso cref="Math.Round(double)"/> function.</param>
    /// <returns>The result of the factorial.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided number is negative.</exception>
    public static double Factorial(double n)
    {
        if (n < 0)
            ThrowHelper.Throw<ArgumentException>("The factorial of negative numbers is undefined.");

        long rounded = (long)Math.Round(n);

        if (n is 0 or 1)
            return 1;

        if (n < 39)
            return Linear(rounded);

        return Optimized(rounded);

        static double Linear(long n)
        {
            double result = 1;
            for (double i = 2; i <= n; i++)
                result *= i;
            return result;
        }
        static double Optimized(long n)
        {
            double result = 1;

            double currentFactor = n;
            double currentIncrement = currentFactor - 2;

            for (long i = 1; i <= n / 2; i++)
            {
                result *= currentFactor;

                currentFactor += currentIncrement;
                currentIncrement -= 2;
            }

            if (n % 2 is 1)
                result *= n / 2 + 1;

            return result;
        }
    }
    /// <summary>Calculates the factorial of a number.</summary>
    /// <param name="n">The number whose factorial to get.</param>
    /// <returns>The result of the factorial.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided number is negative.</exception>
    public static BigInteger FactorialBigInteger(long n)
    {
        if (n < 0)
            ThrowHelper.Throw<ArgumentException>("The factorial of negative numbers is undefined.");

        return n switch
        {
            0 => 1,
            1 => 1,
            2 => 2,
            3 => 6,
            _ => Optimized(n)
        };

        static BigInteger Optimized(long n)
        {
            CacheableBigInteger result = 1;

            long currentFactor = n;
            long currentIncrement = currentFactor - 2;

            long bound = n / 2;

            for (long i = 1; i <= bound; i++)
            {
                result.Multiply(currentFactor);

                currentFactor += currentIncrement;
                currentIncrement -= 2;
            }

            if (n % 2 is 1)
                result.Multiply(n / 2 + 1);

            return result;
        }
    }
    #endregion

    // I SUMMON YOU, COPY-PASTE!
    #region Min
    /// <summary>Returns the smallest <seealso cref="byte"/> from a <seealso cref="byte"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="byte"/> that was found in the <seealso cref="byte"/>[].</returns>
    public static byte Min(params byte[] values) => Min((IEnumerable<byte>)values);
    /// <summary>Returns the smallest <seealso cref="byte"/> from a collection of <seealso cref="byte"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="byte"/> that was found in the collection of <seealso cref="byte"/>s.</returns>
    public static byte Min(IEnumerable<byte> values)
    {
        if (!values.Any())
            return byte.MinValue;

        byte min = byte.MaxValue;

        foreach (byte v in values)
            min = Math.Min(min, v);

        return min;
    }
    /// <summary>Returns the smallest <seealso cref="sbyte"/> from a <seealso cref="sbyte"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="sbyte"/> that was found in the <seealso cref="sbyte"/>[].</returns>
    public static sbyte Min(params sbyte[] values) => Min((IEnumerable<sbyte>)values);
    /// <summary>Returns the smallest <seealso cref="sbyte"/> from a collection of <seealso cref="sbyte"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="sbyte"/> that was found in the collection of <seealso cref="sbyte"/>s.</returns>
    public static sbyte Min(IEnumerable<sbyte> values)
    {
        if (!values.Any())
            return sbyte.MinValue;

        sbyte min = sbyte.MaxValue;

        foreach (sbyte v in values)
            min = Math.Min(min, v);

        return min;
    }
    /// <summary>Returns the smallest <seealso cref="short"/> from a <seealso cref="short"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="short"/> that was found in the <seealso cref="short"/>[].</returns>
    public static short Min(params short[] values) => Min((IEnumerable<short>)values);
    /// <summary>Returns the smallest <seealso cref="short"/> from a collection of <seealso cref="short"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="short"/> that was found in the collection of <seealso cref="short"/>s.</returns>
    public static short Min(IEnumerable<short> values)
    {
        if (!values.Any())
            return short.MinValue;

        short min = short.MaxValue;

        foreach (short v in values)
            min = Math.Min(min, v);

        return min;
    }
    /// <summary>Returns the smallest <seealso cref="ushort"/> from a <seealso cref="ushort"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="ushort"/> that was found in the <seealso cref="ushort"/>[].</returns>
    public static ushort Min(params ushort[] values) => Min((IEnumerable<ushort>)values);
    /// <summary>Returns the smallest <seealso cref="ushort"/> from a collection of <seealso cref="ushort"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="ushort"/> that was found in the collection of <seealso cref="ushort"/>s.</returns>
    public static ushort Min(IEnumerable<ushort> values)
    {
        if (!values.Any())
            return ushort.MinValue;

        ushort min = ushort.MaxValue;

        foreach (ushort v in values)
            min = Math.Min(min, v);

        return min;
    }
    /// <summary>Returns the smallest <seealso cref="int"/> from a <seealso cref="int"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="int"/> that was found in the <seealso cref="int"/>[].</returns>
    public static int Min(params int[] values) => Min((IEnumerable<int>)values);
    /// <summary>Returns the smallest <seealso cref="int"/> from a collection of <seealso cref="int"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="int"/> that was found in the collection of <seealso cref="int"/>s.</returns>
    public static int Min(IEnumerable<int> values)
    {
        if (!values.Any())
            return int.MinValue;

        int min = int.MaxValue;

        foreach (int v in values)
            min = Math.Min(min, v);

        return min;
    }
    /// <summary>Returns the smallest <seealso cref="uint"/> from a <seealso cref="uint"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="uint"/> that was found in the <seealso cref="uint"/>[].</returns>
    public static uint Min(params uint[] values) => Min((IEnumerable<uint>)values);
    /// <summary>Returns the smallest <seealso cref="uint"/> from a collection of <seealso cref="uint"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="uint"/> that was found in the collection of <seealso cref="uint"/>s.</returns>
    public static uint Min(IEnumerable<uint> values)
    {
        if (!values.Any())
            return uint.MinValue;

        uint min = uint.MaxValue;

        foreach (uint v in values)
            min = Math.Min(min, v);

        return min;
    }
    /// <summary>Returns the smallest <seealso cref="long"/> from a <seealso cref="long"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="long"/> that was found in the <seealso cref="long"/>[].</returns>
    public static long Min(params long[] values) => Min((IEnumerable<long>)values);
    /// <summary>Returns the smallest <seealso cref="long"/> from a collection of <seealso cref="long"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="long"/> that was found in the collection of <seealso cref="long"/>s.</returns>
    public static long Min(IEnumerable<long> values)
    {
        if (!values.Any())
            return long.MinValue;

        long min = long.MaxValue;

        foreach (long v in values)
            min = Math.Min(min, v);

        return min;
    }
    /// <summary>Returns the smallest <seealso cref="ulong"/> from a <seealso cref="ulong"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="ulong"/> that was found in the <seealso cref="ulong"/>[].</returns>
    public static ulong Min(params ulong[] values) => Min((IEnumerable<ulong>)values);
    /// <summary>Returns the smallest <seealso cref="ulong"/> from a collection of <seealso cref="ulong"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest <seealso cref="ulong"/> that was found in the collection of <seealso cref="ulong"/>s.</returns>
    public static ulong Min(IEnumerable<ulong> values)
    {
        if (!values.Any())
            return ulong.MinValue;

        ulong min = ulong.MaxValue;

        foreach (ulong v in values)
            min = Math.Min(min, v);

        return min;
    }

#if NET7_0_OR_GREATER
    /// <inheritdoc cref="Min{T}(IEnumerable{T})"/>
    public static T Min<T>(params T[] values)
        where T : INumber<T>, IMinMaxValue<T>
    {
        return Min((IEnumerable<T>)values);
    }
    /// <summary>Returns the smallest value from a collection of values.</summary>
    /// <typeparam name="T">The type of the number values to compare.</typeparam>
    /// <param name="values">The values to compare.</param>
    /// <returns>The smallest value that was found in the collection of values.</returns>
    public static T Min<T>(IEnumerable<T> values)
        where T : INumber<T>, IMinMaxValue<T>
    {
        if (!values.Any())
            return T.MinValue;

        T min = T.MaxValue;

        foreach (T v in values)
            min = T.Min(min, v);

        return min;
    }
#endif
    #endregion

    #region Max
    /// <summary>Returns the largest <seealso cref="byte"/> from a <seealso cref="byte"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="byte"/> that was found in the <seealso cref="byte"/>[].</returns>
    public static byte Max(params byte[] values) => Max((IEnumerable<byte>)values);
    /// <summary>Returns the largest <seealso cref="byte"/> from a collection of <seealso cref="byte"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="byte"/> that was found in the collection of <seealso cref="byte"/>s.</returns>
    public static byte Max(IEnumerable<byte> values)
    {
        if (!values.Any())
            return byte.MaxValue;

        byte min = byte.MinValue;

        foreach (byte v in values)
            min = Math.Max(min, v);

        return min;
    }
    /// <summary>Returns the largest <seealso cref="sbyte"/> from a <seealso cref="sbyte"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="sbyte"/> that was found in the <seealso cref="sbyte"/>[].</returns>
    public static sbyte Max(params sbyte[] values) => Max((IEnumerable<sbyte>)values);
    /// <summary>Returns the largest <seealso cref="sbyte"/> from a collection of <seealso cref="sbyte"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="sbyte"/> that was found in the collection of <seealso cref="sbyte"/>s.</returns>
    public static sbyte Max(IEnumerable<sbyte> values)
    {
        if (!values.Any())
            return sbyte.MaxValue;

        sbyte min = sbyte.MinValue;

        foreach (sbyte v in values)
            min = Math.Max(min, v);

        return min;
    }
    /// <summary>Returns the largest <seealso cref="short"/> from a <seealso cref="short"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="short"/> that was found in the <seealso cref="short"/>[].</returns>
    public static short Max(params short[] values) => Max((IEnumerable<short>)values);
    /// <summary>Returns the largest <seealso cref="short"/> from a collection of <seealso cref="short"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="short"/> that was found in the collection of <seealso cref="short"/>s.</returns>
    public static short Max(IEnumerable<short> values)
    {
        if (!values.Any())
            return short.MaxValue;

        short min = short.MinValue;

        foreach (short v in values)
            min = Math.Max(min, v);

        return min;
    }
    /// <summary>Returns the largest <seealso cref="ushort"/> from a <seealso cref="ushort"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="ushort"/> that was found in the <seealso cref="ushort"/>[].</returns>
    public static ushort Max(params ushort[] values) => Max((IEnumerable<ushort>)values);
    /// <summary>Returns the largest <seealso cref="ushort"/> from a collection of <seealso cref="ushort"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="ushort"/> that was found in the collection of <seealso cref="ushort"/>s.</returns>
    public static ushort Max(IEnumerable<ushort> values)
    {
        if (!values.Any())
            return ushort.MaxValue;

        ushort min = ushort.MinValue;

        foreach (ushort v in values)
            min = Math.Max(min, v);

        return min;
    }
    /// <summary>Returns the largest <seealso cref="int"/> from a <seealso cref="int"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="int"/> that was found in the <seealso cref="int"/>[].</returns>
    public static int Max(params int[] values) => Max((IEnumerable<int>)values);
    /// <summary>Returns the largest <seealso cref="int"/> from a collection of <seealso cref="int"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="int"/> that was found in the collection of <seealso cref="int"/>s.</returns>
    public static int Max(IEnumerable<int> values)
    {
        if (!values.Any())
            return int.MaxValue;

        int min = int.MinValue;

        foreach (int v in values)
            min = Math.Max(min, v);

        return min;
    }
    /// <summary>Returns the largest <seealso cref="uint"/> from a <seealso cref="uint"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="uint"/> that was found in the <seealso cref="uint"/>[].</returns>
    public static uint Max(params uint[] values) => Max((IEnumerable<uint>)values);
    /// <summary>Returns the largest <seealso cref="uint"/> from a collection of <seealso cref="uint"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="uint"/> that was found in the collection of <seealso cref="uint"/>s.</returns>
    public static uint Max(IEnumerable<uint> values)
    {
        if (!values.Any())
            return uint.MaxValue;

        uint min = uint.MinValue;

        foreach (uint v in values)
            min = Math.Max(min, v);

        return min;
    }
    /// <summary>Returns the largest <seealso cref="long"/> from a <seealso cref="long"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="long"/> that was found in the <seealso cref="long"/>[].</returns>
    public static long Max(params long[] values) => Max((IEnumerable<long>)values);
    /// <summary>Returns the largest <seealso cref="long"/> from a collection of <seealso cref="long"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="long"/> that was found in the collection of <seealso cref="long"/>s.</returns>
    public static long Max(IEnumerable<long> values)
    {
        if (!values.Any())
            return long.MaxValue;

        long min = long.MinValue;

        foreach (long v in values)
            min = Math.Max(min, v);

        return min;
    }
    /// <summary>Returns the largest <seealso cref="ulong"/> from a <seealso cref="ulong"/>[].</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="ulong"/> that was found in the <seealso cref="ulong"/>[].</returns>
    public static ulong Max(params ulong[] values) => Max((IEnumerable<ulong>)values);
    /// <summary>Returns the largest <seealso cref="ulong"/> from a collection of <seealso cref="ulong"/>s.</summary>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest <seealso cref="ulong"/> that was found in the collection of <seealso cref="ulong"/>s.</returns>
    public static ulong Max(IEnumerable<ulong> values)
    {
        if (!values.Any())
            return ulong.MaxValue;

        ulong min = ulong.MinValue;

        foreach (ulong v in values)
            min = Math.Max(min, v);

        return min;
    }

#if HAS_INUMBER
    /// <inheritdoc cref="Max{T}(IEnumerable{T})"/>
    public static T Max<T>(params T[] values)
        where T : INumber<T>, IMinMaxValue<T>
    {
        return Max((IEnumerable<T>)values);
    }
    /// <summary>Returns the largest value from a collection of values.</summary>
    /// <typeparam name="T">The type of the number values to compare.</typeparam>
    /// <param name="values">The values to compare.</param>
    /// <returns>The largest value that was found in the collection of values.</returns>
    public static T Max<T>(IEnumerable<T> values)
        where T : INumber<T>, IMinMaxValue<T>
    {
        if (!values.Any())
            return T.MaxValue;

        T min = T.MinValue;

        foreach (T v in values)
            min = T.Max(min, v);

        return min;
    }
#endif
    #endregion

    #region Square

#if HAS_INUMBER
    /// <summary>
    /// Returns the square of the given number.
    /// </summary>
    /// <typeparam name="T">The type of the number to square.</typeparam>
    /// <param name="x">The number to square.</param>
    /// <returns>The square of the number, equal to x * x.</returns>
    public static INumber<T> Square<T>(this T x)
        where T : INumber<T>, IMultiplyOperators<T, T, T>
    {
        return x * x;
    }
#endif

    /// <summary>Returns the square of the given number.</summary>
    /// <param name="x">The number to square.</param>
    /// <returns>The square of the number, equal to x * x.</returns>
    public static int Square(this int x) => x * x;
    /// <summary>Returns the square of the given number.</summary>
    /// <param name="x">The number to square.</param>
    /// <returns>The square of the number, equal to x * x.</returns>
    public static uint Square(this uint x) => x * x;
    /// <summary>Returns the square of the given number.</summary>
    /// <param name="x">The number to square.</param>
    /// <returns>The square of the number, equal to x * x.</returns>
    public static long Square(this long x) => x * x;
    /// <summary>Returns the square of the given number.</summary>
    /// <param name="x">The number to square.</param>
    /// <returns>The square of the number, equal to x * x.</returns>
    public static ulong Square(this ulong x) => x * x;
    /// <summary>Returns the square of the given number.</summary>
    /// <param name="x">The number to square.</param>
    /// <returns>The square of the number, equal to x * x.</returns>
    public static float Square(this float x) => x * x;
    /// <summary>Returns the square of the given number.</summary>
    /// <param name="x">The number to square.</param>
    /// <returns>The square of the number, equal to x * x.</returns>
    public static double Square(this double x) => x * x;
    /// <summary>Returns the square of the given number.</summary>
    /// <param name="x">The number to square.</param>
    /// <returns>The square of the number, equal to x * x.</returns>
    public static decimal Square(this decimal x) => x * x;

    #endregion

    #region Pow

    /// <summary>
    /// Shorthand for <see cref="Math.Pow(double, double)"/>.
    /// </summary>
    /// <param name="x">The base number.</param>
    /// <param name="exponent">The exponent to raise the base to.</param>
    /// <returns>The result of the power operation.</returns>
    public static double Pow(this double x, double exponent) => Math.Pow(x, exponent);

    #endregion
}
