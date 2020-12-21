using System;

namespace Garyon.Extensions
{
    /// <summary>Provides extensions for unsafe context.</summary>
    public static unsafe class UnsafeExtensions
    {
        /// <summary>Gets a <see cref="ReadOnlySpan{T}"/> containing the bytes of the provided value.</summary>
        /// <typeparam name="T">The type of the value whose bytes to get.</typeparam>
        /// <param name="value">The value whose bytes to get.</param>
        /// <returns>The bytes of the value.</returns>
        public static ReadOnlySpan<byte> GetBytes<T>(this T value)
            where T : unmanaged
        {
            return new ReadOnlySpan<byte>(&value, sizeof(T));
        }
        /// <summary>Gets the value of an <see langword="unmanaged"/> <see langword="struct"/>, given a <seealso cref="Span{T}"/>.</summary>
        /// <typeparam name="T">The type of the <see langword="unmanaged"/> <see langword="struct"/>.</typeparam>
        /// <param name="span">The <seealso cref="Span{T}"/> from which to get the bytes of the struct.</param>
        public static T GetValueFromSpan<T>(this Span<byte> span)
            where T : unmanaged
        {
            fixed (byte* bytes = span)
                return *(T*)bytes;
        }
    }
}
