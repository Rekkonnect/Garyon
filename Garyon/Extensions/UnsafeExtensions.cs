#if HAS_SPAN
using System;
using System.Runtime.InteropServices;
#endif

namespace Garyon.Extensions;

/// <summary>Provides extensions for unsafe context.</summary>
public static unsafe class UnsafeExtensions
{
#if HAS_SPAN
    /// <summary>Gets a <see cref="ReadOnlySpan{T}"/> containing the bytes of the provided value.</summary>
    /// <typeparam name="T">The type of the value whose bytes to get.</typeparam>
    /// <param name="value">The value whose bytes to get.</param>
    /// <returns>The bytes of the value.</returns>
    public static ReadOnlySpan<byte> GetBytes<T>(this ref T value)
        where T : unmanaged
    {
        return MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref value, 1));
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
#else
    /// <summary>Gets a byte array containing the bytes of the provided value.</summary>
    /// <typeparam name="T">The type of the value whose bytes to get.</typeparam>
    /// <param name="value">The value whose bytes to get.</param>
    /// <returns>The bytes of the value.</returns>
    /// <remarks>This operation might be expensive.</remarks>
    public static byte[] GetBytes<T>(this ref T value)
        where T : unmanaged
    {
        var bytes = new byte[sizeof(T)];
        fixed (byte* bytePointer = bytes)
        {
            T* ptr = (T*)bytePointer;
            *ptr = value;
        }
        return bytes;
    }
#endif
}
