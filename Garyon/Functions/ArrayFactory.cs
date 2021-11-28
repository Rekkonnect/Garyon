using System;

namespace Garyon.Functions
{
    /// <summary>Provides factory methods for arrays.</summary>
    public static class ArrayFactory
    {
        /// <summary>Creates a new single-dimensional array containing the single provided value.</summary>
        /// <typeparam name="T">The type of the stored values in the resulting array.</typeparam>
        /// <param name="value">The value to fill the resulting array with.</param>
        /// <param name="length">The length of the resulting array.</param>
        /// <returns>The resulting array containing the filling value in all of its indices.</returns>
        public static T[] CreateFilled<T>(T value, int length)
        {
            var result = new T[length];
            Array.Fill(result, value);
            return result;
        }
        /// <inheritdoc cref="CreateFilled{T}(T, int)"/>
        /// <summary>Creates a new two-dimensional array containing the single provided value.</summary>
        /// <param name="length0">The length of the first dimension of the resulting array.</param>
        /// <param name="length1">The length of the second dimension of the resulting array.</param>
        public static unsafe T[,] CreateFilled<T>(T value, int length0, int length1)
            where T : unmanaged
        {
            var result = new T[length0, length1];
            fixed (T* ptr = result)
            {
                PointerFunctions.Fill(ptr, result.Length, value);
                return result;
            }
        }
        /// <inheritdoc cref="CreateFilled{T}(T, int, int)"/>
        /// <summary>Creates a new three-dimensional array containing the single provided value.</summary>
        /// <param name="length2">The length of the third dimension of the resulting array.</param>
        public static unsafe T[,,] CreateFilled<T>(T value, int length0, int length1, int length2)
            where T : unmanaged
        {
            var result = new T[length0, length1, length2];
            fixed (T* ptr = result)
            {
                PointerFunctions.Fill(ptr, result.Length, value);
                return result;
            }
        }
    }
}