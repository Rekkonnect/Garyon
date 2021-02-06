using System;

namespace Garyon.Objects
{
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
            switch (Value)
            {
                case byte _:
                    return (T)(object)(byte)(~(byte)(object)Value);
                case sbyte _:
                    return (T)(object)(sbyte)(~(sbyte)(object)Value);
                case short _:
                    return (T)(object)(short)(~(short)(object)Value);
                case ushort _:
                    return (T)(object)(ushort)(~(ushort)(object)Value);
                case int _:
                    return (T)(object)(~(int)(object)Value);
                case uint _:
                    return (T)(object)(~(uint)(object)Value);
                case long _:
                    return (T)(object)(~(long)(object)Value);
                case ulong _:
                    return (T)(object)(~(ulong)(object)Value);
            }
            // Should never be reached
            return default;
        }

        /// <summary>Returns a value that is equal to the bitwise OR of this instance's current value with another value. The instance remains unaffected.</summary>
        /// <param name="value">The value to OR the current value with.</param>
        /// <returns>The resulting value that is equal to applying bitwise OR to the current value and the specified value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T Or(IInteger<T> value)
        {
            switch (Value)
            {
                case byte _:
                    return (T)(object)(byte)((byte)(object)Value | (byte)(object)value.Value);
                case sbyte _:
                    return (T)(object)(sbyte)((sbyte)(object)Value | (sbyte)(object)value.Value);
                case short _:
                    return (T)(object)(short)((short)(object)Value | (short)(object)value.Value);
                case ushort _:
                    return (T)(object)(ushort)((ushort)(object)Value | (ushort)(object)value.Value);
                case int _:
                    return (T)(object)((int)(object)Value | (int)(object)value.Value);
                case uint _:
                    return (T)(object)((uint)(object)Value | (uint)(object)value.Value);
                case long _:
                    return (T)(object)((long)(object)Value | (long)(object)value.Value);
                case ulong _:
                    return (T)(object)((ulong)(object)Value | (ulong)(object)value.Value);
            }
            // Should never be reached
            return default;
        }
        /// <summary>Returns a value that is equal to the bitwise AND of this instance's current value with another value. The instance remains unaffected.</summary>
        /// <param name="value">The value to AND the current value with.</param>
        /// <returns>The resulting value that is equal to applying bitwise AND to the current value and the specified value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T And(IInteger<T> value)
        {
            switch (Value)
            {
                case byte _:
                    return (T)(object)(byte)((byte)(object)Value & (byte)(object)value.Value);
                case sbyte _:
                    return (T)(object)(sbyte)((sbyte)(object)Value & (sbyte)(object)value.Value);
                case short _:
                    return (T)(object)(short)((short)(object)Value & (short)(object)value.Value);
                case ushort _:
                    return (T)(object)(ushort)((ushort)(object)Value & (ushort)(object)value.Value);
                case int _:
                    return (T)(object)((int)(object)Value & (int)(object)value.Value);
                case uint _:
                    return (T)(object)((uint)(object)Value & (uint)(object)value.Value);
                case long _:
                    return (T)(object)((long)(object)Value & (long)(object)value.Value);
                case ulong _:
                    return (T)(object)((ulong)(object)Value & (ulong)(object)value.Value);
            }
            // Should never be reached
            return default;
        }
        /// <summary>Returns a value that is equal to the bitwise XOR of this instance's current value with another value. The instance remains unaffected.</summary>
        /// <param name="value">The value to XOR the current value with.</param>
        /// <returns>The resulting value that is equal to applying bitwise XOR to the current value and the specified value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T Xor(IInteger<T> value)
        {
            switch (Value)
            {
                case byte _:
                    return (T)(object)(byte)((byte)(object)Value ^ (byte)(object)value.Value);
                case sbyte _:
                    return (T)(object)(sbyte)((sbyte)(object)Value ^ (sbyte)(object)value.Value);
                case short _:
                    return (T)(object)(short)((short)(object)Value ^ (short)(object)value.Value);
                case ushort _:
                    return (T)(object)(ushort)((ushort)(object)Value ^ (ushort)(object)value.Value);
                case int _:
                    return (T)(object)((int)(object)Value ^ (int)(object)value.Value);
                case uint _:
                    return (T)(object)((uint)(object)Value ^ (uint)(object)value.Value);
                case long _:
                    return (T)(object)((long)(object)Value ^ (long)(object)value.Value);
                case ulong _:
                    return (T)(object)((ulong)(object)Value ^ (ulong)(object)value.Value);
            }
            // Should never be reached
            return default;
        }

        /// <summary>Returns a value that is equal to shifting the current value to the left by the specified number of bits. The instance remains unaffected.</summary>
        /// <param name="shift">The number of bits to shift the current value to the left by.</param>
        /// <returns>The resulting value that is equal to shifting the current value to the left by the specified number of bits.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T ShiftLeft(int shift)
        {
            switch (Value)
            {
                case byte _:
                    return (T)(object)(byte)((byte)(object)Value << shift);
                case sbyte _:
                    return (T)(object)(sbyte)((sbyte)(object)Value << shift);
                case short _:
                    return (T)(object)(short)((short)(object)Value << shift);
                case ushort _:
                    return (T)(object)(ushort)((ushort)(object)Value << shift);
                case int _:
                    return (T)(object)((int)(object)Value << shift);
                case uint _:
                    return (T)(object)((uint)(object)Value << shift);
                case long _:
                    return (T)(object)((long)(object)Value << shift);
                case ulong _:
                    return (T)(object)((ulong)(object)Value << shift);
            }
            // Should never be reached
            return default;
        }
        /// <summary>Returns a value that is equal to shifting the current value to the right by the specified number of bits. The instance remains unaffected.</summary>
        /// <param name="shift">The number of bits to shift the current value to the right by.</param>
        /// <returns>The resulting value that is equal to shifting the current value to the right by the specified number of bits.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T ShiftRight(int shift)
        {
            switch (Value)
            {
                case byte _:
                    return (T)(object)(byte)((byte)(object)Value >> shift);
                case sbyte _:
                    return (T)(object)(sbyte)((sbyte)(object)Value >> shift);
                case short _:
                    return (T)(object)(short)((short)(object)Value >> shift);
                case ushort _:
                    return (T)(object)(ushort)((ushort)(object)Value >> shift);
                case int _:
                    return (T)(object)((int)(object)Value >> shift);
                case uint _:
                    return (T)(object)((uint)(object)Value >> shift);
                case long _:
                    return (T)(object)((long)(object)Value >> shift);
                case ulong _:
                    return (T)(object)((ulong)(object)Value >> shift);
            }
            // Should never be reached
            return default;
        }
    }
}
