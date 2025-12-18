using System;
using System.Numerics;

namespace Garyon.Mathematics;

/// <summary>
/// Provides extensions directly onto <see cref="Math"/>.
/// Helpful when the extensions are meant to be provided as
/// method groups onto delegate variables.
/// </summary>
public static class MathExtensions
{
    extension(Math)
    {
#if HAS_INUMBER
        /// <summary>
        /// Returns the square of the given number.
        /// </summary>
        /// <typeparam name="T">The type of the number to square.</typeparam>
        /// <param name="x">The number to square.</param>
        /// <returns>The square of the number, equal to x * x.</returns>
        public static INumber<T> Square<T>(T x)
            where T : INumber<T>, IMultiplyOperators<T, T, T>
        {
            return x * x;
        }
#endif

        /// <summary>Returns the square of the given number.</summary>
        /// <param name="x">The number to square.</param>
        /// <returns>The square of the number, equal to x * x.</returns>
        public static int Square(int x) => x * x;
        /// <summary>Returns the square of the given number.</summary>
        /// <param name="x">The number to square.</param>
        /// <returns>The square of the number, equal to x * x.</returns>
        public static uint Square(uint x) => x * x;
        /// <summary>Returns the square of the given number.</summary>
        /// <param name="x">The number to square.</param>
        /// <returns>The square of the number, equal to x * x.</returns>
        public static long Square(long x) => x * x;
        /// <summary>Returns the square of the given number.</summary>
        /// <param name="x">The number to square.</param>
        /// <returns>The square of the number, equal to x * x.</returns>
        public static ulong Square(ulong x) => x * x;
        /// <summary>Returns the square of the given number.</summary>
        /// <param name="x">The number to square.</param>
        /// <returns>The square of the number, equal to x * x.</returns>
        public static float Square(float x) => x * x;
        /// <summary>Returns the square of the given number.</summary>
        /// <param name="x">The number to square.</param>
        /// <returns>The square of the number, equal to x * x.</returns>
        public static double Square(double x) => x * x;
        /// <summary>Returns the square of the given number.</summary>
        /// <param name="x">The number to square.</param>
        /// <returns>The square of the number, equal to x * x.</returns>
        public static decimal Square(decimal x) => x * x;
    }
}
