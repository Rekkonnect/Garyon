#if NEEDS_INUMERIC

using System;

namespace Garyon.Objects;

/// <summary>Represents an integer value. This interface encapsulates generalized functionality for the built-in primitive integer types (<seealso cref="byte"/>, <seealso cref="sbyte"/>, <seealso cref="short"/>, <seealso cref="ushort"/>, <seealso cref="int"/>, <seealso cref="uint"/>, <seealso cref="long"/>, <seealso cref="ulong"/>).</summary>
/// <typeparam name="T">The integer type this interface encapsulates generalized functionality for. Must be one of the valid types: <seealso cref="byte"/>, <seealso cref="sbyte"/>, <seealso cref="short"/>, <seealso cref="ushort"/>, <seealso cref="int"/>, <seealso cref="uint"/>, <seealso cref="long"/>, <seealso cref="ulong"/>.</typeparam>
public interface IInteger<T> : INumeric<T>, IEquatable<IInteger<T>>, IComparable<IInteger<T>>
    where T : unmanaged, IEquatable<T>, IComparable<T>
{
    /// <summary>Returns a value that is equal to the bitwise NOT of this instance's current value. The instance remains unaffected.</summary>
    /// <returns>The resulting value that is equal to applying bitwise NOT to the current value.</returns>
    /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
    public sealed T Not()
    {
        return Value switch
        {
            byte => (T)(object)(byte)(~(byte)(object)Value),
            sbyte => (T)(object)(sbyte)(~(sbyte)(object)Value),
            short => (T)(object)(short)(~(short)(object)Value),
            ushort => (T)(object)(ushort)(~(ushort)(object)Value),
            int => (T)(object)(~(int)(object)Value),
            uint => (T)(object)(~(uint)(object)Value),
            long => (T)(object)(~(long)(object)Value),
            ulong => (T)(object)(~(ulong)(object)Value),
            // Should never be reached
            _ => default,
        };
    }

    /// <summary>Returns a value that is equal to the bitwise OR of this instance's current value with another value. The instance remains unaffected.</summary>
    /// <param name="value">The value to OR the current value with.</param>
    /// <returns>The resulting value that is equal to applying bitwise OR to the current value and the specified value.</returns>
    /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
    public sealed T Or(IInteger<T> value)
    {
        return Value switch
        {
            byte => (T)(object)(byte)((byte)(object)Value | (byte)(object)value.Value),
            sbyte => (T)(object)(sbyte)((sbyte)(object)Value | (sbyte)(object)value.Value),
            short => (T)(object)(short)((short)(object)Value | (short)(object)value.Value),
            ushort => (T)(object)(ushort)((ushort)(object)Value | (ushort)(object)value.Value),
            int => (T)(object)((int)(object)Value | (int)(object)value.Value),
            uint => (T)(object)((uint)(object)Value | (uint)(object)value.Value),
            long => (T)(object)((long)(object)Value | (long)(object)value.Value),
            ulong => (T)(object)((ulong)(object)Value | (ulong)(object)value.Value),
            // Should never be reached
            _ => default,
        };
    }
    /// <summary>Returns a value that is equal to the bitwise AND of this instance's current value with another value. The instance remains unaffected.</summary>
    /// <param name="value">The value to AND the current value with.</param>
    /// <returns>The resulting value that is equal to applying bitwise AND to the current value and the specified value.</returns>
    /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
    public sealed T And(IInteger<T> value)
    {
        return Value switch
        {
            byte => (T)(object)(byte)((byte)(object)Value & (byte)(object)value.Value),
            sbyte => (T)(object)(sbyte)((sbyte)(object)Value & (sbyte)(object)value.Value),
            short => (T)(object)(short)((short)(object)Value & (short)(object)value.Value),
            ushort => (T)(object)(ushort)((ushort)(object)Value & (ushort)(object)value.Value),
            int => (T)(object)((int)(object)Value & (int)(object)value.Value),
            uint => (T)(object)((uint)(object)Value & (uint)(object)value.Value),
            long => (T)(object)((long)(object)Value & (long)(object)value.Value),
            ulong => (T)(object)((ulong)(object)Value & (ulong)(object)value.Value),
            // Should never be reached
            _ => default,
        };
    }
    /// <summary>Returns a value that is equal to the bitwise XOR of this instance's current value with another value. The instance remains unaffected.</summary>
    /// <param name="value">The value to XOR the current value with.</param>
    /// <returns>The resulting value that is equal to applying bitwise XOR to the current value and the specified value.</returns>
    /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
    public sealed T Xor(IInteger<T> value)
    {
        return Value switch
        {
            byte => (T)(object)(byte)((byte)(object)Value ^ (byte)(object)value.Value),
            sbyte => (T)(object)(sbyte)((sbyte)(object)Value ^ (sbyte)(object)value.Value),
            short => (T)(object)(short)((short)(object)Value ^ (short)(object)value.Value),
            ushort => (T)(object)(ushort)((ushort)(object)Value ^ (ushort)(object)value.Value),
            int => (T)(object)((int)(object)Value ^ (int)(object)value.Value),
            uint => (T)(object)((uint)(object)Value ^ (uint)(object)value.Value),
            long => (T)(object)((long)(object)Value ^ (long)(object)value.Value),
            ulong => (T)(object)((ulong)(object)Value ^ (ulong)(object)value.Value),
            // Should never be reached
            _ => default,
        };
    }

    /// <summary>Returns a value that is equal to shifting the current value to the left by the specified number of bits. The instance remains unaffected.</summary>
    /// <param name="shift">The number of bits to shift the current value to the left by.</param>
    /// <returns>The resulting value that is equal to shifting the current value to the left by the specified number of bits.</returns>
    /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
    public sealed T ShiftLeft(int shift)
    {
        return Value switch
        {
            byte => (T)(object)(byte)((byte)(object)Value << shift),
            sbyte => (T)(object)(sbyte)((sbyte)(object)Value << shift),
            short => (T)(object)(short)((short)(object)Value << shift),
            ushort => (T)(object)(ushort)((ushort)(object)Value << shift),
            int => (T)(object)((int)(object)Value << shift),
            uint => (T)(object)((uint)(object)Value << shift),
            long => (T)(object)((long)(object)Value << shift),
            ulong => (T)(object)((ulong)(object)Value << shift),
            // Should never be reached
            _ => default,
        };
    }
    /// <summary>Returns a value that is equal to shifting the current value to the right by the specified number of bits. The instance remains unaffected.</summary>
    /// <param name="shift">The number of bits to shift the current value to the right by.</param>
    /// <returns>The resulting value that is equal to shifting the current value to the right by the specified number of bits.</returns>
    /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
    public sealed T ShiftRight(int shift)
    {
        return Value switch
        {
            byte => (T)(object)(byte)((byte)(object)Value >> shift),
            sbyte => (T)(object)(sbyte)((sbyte)(object)Value >> shift),
            short => (T)(object)(short)((short)(object)Value >> shift),
            ushort => (T)(object)(ushort)((ushort)(object)Value >> shift),
            int => (T)(object)((int)(object)Value >> shift),
            uint => (T)(object)((uint)(object)Value >> shift),
            long => (T)(object)((long)(object)Value >> shift),
            ulong => (T)(object)((ulong)(object)Value >> shift),
            // Should never be reached
            _ => default,
        };
    }
}

#endif