using System;
using System.Numerics;

namespace Garyon.Mathematics;

/// <summary>
/// Provides math-related extensions on number types.
/// </summary>
public static class MathNumberExtensions
{
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

    /// <summary>
    /// Returns the square of the given number.
    /// </summary>
    /// <param name="x">
    /// The number to square.
    /// </param>
    /// <returns>
    /// The square of the number, equal to x * x.
    /// </returns>
    public static int Square(this int x) => x * x;
    /// <summary>
    /// Returns the square of the given number.
    /// </summary>
    /// <param name="x">
    /// The number to square.
    /// </param>
    /// <returns>
    /// The square of the number, equal to x * x.
    /// </returns>
    public static uint Square(this uint x) => x * x;
    /// <summary>
    /// Returns the square of the given number.
    /// </summary>
    /// <param name="x">
    /// The number to square.
    /// </param>
    /// <returns>
    /// The square of the number, equal to x * x.
    /// </returns>
    public static long Square(this long x) => x * x;
    /// <summary>
    /// Returns the square of the given number.
    /// </summary>
    /// <param name="x">
    /// The number to square.
    /// </param>
    /// <returns>
    /// The square of the number, equal to x * x.
    /// </returns>
    public static ulong Square(this ulong x) => x * x;
    /// <summary>
    /// Returns the square of the given number.
    /// </summary>
    /// <param name="x">
    /// The number to square.
    /// </param>
    /// <returns>
    /// The square of the number, equal to x * x.
    /// </returns>
    public static float Square(this float x) => x * x;
    /// <summary>
    /// Returns the square of the given number.
    /// </summary>
    /// <param name="x">
    /// The number to square.
    /// </param>
    /// <returns>
    /// The square of the number, equal to x * x.
    /// </returns>
    public static double Square(this double x) => x * x;
    /// <summary>
    /// Returns the square of the given number.
    /// </summary>
    /// <param name="x">
    /// The number to square.
    /// </param>
    /// <returns>
    /// The square of the number, equal to x * x.
    /// </returns>
    public static decimal Square(this decimal x) => x * x;

    /// <summary>
    /// Shorthand for <see cref="Math.Pow(double, double)"/>.
    /// </summary>
    /// <param name="x">
    /// The base number.
    /// </param>
    /// <param name="exponent">
    /// The exponent to raise the base to.
    /// </param>
    /// <returns>
    /// The result of the power operation.
    /// </returns>
    public static double Pow(this double x, double exponent) => Math.Pow(x, exponent);

    /// <summary>
    /// Calculates the square root of the value using
    /// <see cref="Math.Sqrt(double)"/>.
    /// </summary>
    public static double Sqrt(this double value)
    {
        return Math.Sqrt(value);
    }

#if HAS_MATHF
    /// <summary>
    /// Calculates the square root of the value using
    /// <see cref="MathF.Sqrt(float)"/>.
    /// </summary>
    public static float Sqrt(this float value)
    {
        return MathF.Sqrt(value);
    }
#endif

    /// <summary>
    /// Calculates the base-e log of the value using
    /// <see cref="Math.Log(double)"/>.
    /// </summary>
    public static double Log(this double value)
    {
        return Math.Log(value);
    }

    /// <summary>
    /// Calculates the log of the value using
    /// <see cref="Math.Log(double, double)"/>.
    /// </summary>
    public static double Log(this double value, double @base)
    {
        return Math.Log(value, @base);
    }

    /// <summary>
    /// Calculates the base-10 log of the value using
    /// <see cref="Math.Log10(double)"/>.
    /// </summary>
    public static double Log10(this double value)
    {
        return Math.Log10(value);
    }

#if HAS_MATH_LOG2
    /// <summary>
    /// Calculates the base-2 log of the value using
    /// <see cref="Math.Log2(double)"/>.
    /// </summary>
    public static double Log2(this double value)
    {
        return Math.Log2(value);
    }
#endif

#if HAS_MATH_ILOGB
    /// <summary>
    /// Calculates the base-2 integer log of the value using
    /// <see cref="Math.ILogB(double)"/>.
    /// </summary>
    public static int ILogB(this double value)
    {
        return Math.ILogB(value);
    }
#endif

#if HAS_INUMBER
    /// <summary>
    /// Divides the specified integer value by two, rounding towards zero, using
    /// the defined shift operators.
    /// </summary>
    public static T Halve<T>(this T value)
        where T : IBinaryInteger<T>, IShiftOperators<T, int, T>
    {
        if (T.IsNegative(value))
        {
            value++;
        }
        return value >> 1;
    }
#endif
}
