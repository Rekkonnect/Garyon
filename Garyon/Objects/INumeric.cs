using Microsoft.VisualBasic.CompilerServices;
using System;

namespace Garyon.Objects
{
    /// <summary>Represents a numeric value. This interface encapsulates generalized functionality for the built-in primitive numeric types (<seealso cref="byte"/>, <seealso cref="sbyte"/>, <seealso cref="short"/>, <seealso cref="ushort"/>, <seealso cref="int"/>, <seealso cref="uint"/>, <seealso cref="long"/>, <seealso cref="ulong"/>, <seealso cref="float"/>, <seealso cref="double"/>, <seealso cref="decimal"/>).</summary>
    /// <typeparam name="T">The numeric type this interface encapsulates generalized functionality for. Must be one of the valid types: <seealso cref="byte"/>, <seealso cref="sbyte"/>, <seealso cref="short"/>, <seealso cref="ushort"/>, <seealso cref="int"/>, <seealso cref="uint"/>, <seealso cref="long"/>, <seealso cref="ulong"/>, <seealso cref="float"/>, <seealso cref="double"/>, <seealso cref="decimal"/>.</typeparam>
    public interface INumeric<T> : IEquatable<INumeric<T>>, IComparable<INumeric<T>>
        where T : unmanaged, IEquatable<T>, IComparable<T>
    {
        /// <summary>Gets the smallest possible value of a <typeparamref name="T"/>. It uses the respective MinValue <see langword="const"/> field.</summary>
        public static T MinValue
        {
            get
            {
                switch (default(T))
                {
                    case byte _:
                        return (T)(object)byte.MinValue;
                    case sbyte _:
                        return (T)(object)sbyte.MinValue;
                    case short _:
                        return (T)(object)short.MinValue;
                    case ushort _:
                        return (T)(object)ushort.MinValue;
                    case int _:
                        return (T)(object)int.MinValue;
                    case uint _:
                        return (T)(object)uint.MinValue;
                    case long _:
                        return (T)(object)long.MinValue;
                    case ulong _:
                        return (T)(object)ulong.MinValue;
                    case float _:
                        return (T)(object)float.MinValue;
                    case double _:
                        return (T)(object)double.MinValue;
                    case decimal _:
                        return (T)(object)decimal.MinValue;
                }
                // Should never be reached
                return default;
            }
        }
        /// <summary>Gets the largest possible value of a <typeparamref name="T"/>. It uses the respective MaxValue <see langword="const"/> field.</summary>
        public static T MaxValue
        {
            get
            {
                switch (default(T))
                {
                    case byte _:
                        return (T)(object)byte.MaxValue;
                    case sbyte _:
                        return (T)(object)sbyte.MaxValue;
                    case short _:
                        return (T)(object)short.MaxValue;
                    case ushort _:
                        return (T)(object)ushort.MaxValue;
                    case int _:
                        return (T)(object)int.MaxValue;
                    case uint _:
                        return (T)(object)uint.MaxValue;
                    case long _:
                        return (T)(object)long.MaxValue;
                    case ulong _:
                        return (T)(object)ulong.MaxValue;
                    case float _:
                        return (T)(object)float.MaxValue;
                    case double _:
                        return (T)(object)double.MaxValue;
                    case decimal _:
                        return (T)(object)decimal.MaxValue;
                }
                // Should never be reached
                return default;
            }
        }

        /// <summary>Gets the numeric value.</summary>
        public T Value { get; }

        /// <summary>Returns a value that is equal to this instance's current value increased by one. The instance remains unaffected.</summary>
        /// <returns>The resulting value that is equal to adding one to the current value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T AddOne()
        {
            switch (Value)
            {
                case byte _:
                    return (T)(object)(byte)((byte)(object)Value + 1);
                case sbyte _:
                    return (T)(object)(sbyte)((sbyte)(object)Value + 1);
                case short _:
                    return (T)(object)(short)((short)(object)Value + 1);
                case ushort _:
                    return (T)(object)(ushort)((ushort)(object)Value + 1);
                case int _:
                    return (T)(object)((int)(object)Value + 1);
                case uint _:
                    return (T)(object)((uint)(object)Value + 1);
                case long _:
                    return (T)(object)((long)(object)Value + 1);
                case ulong _:
                    return (T)(object)((ulong)(object)Value + 1);
                case float _:
                    return (T)(object)((float)(object)Value + 1);
                case double _:
                    return (T)(object)((double)(object)Value + 1);
                case decimal _:
                    return (T)(object)((decimal)(object)Value + 1);
            }
            // Should never be reached
            return default;
        }
        /// <summary>Returns a value that is equal to this instance's current value decreased by one. The instance remains unaffected.</summary>
        /// <returns>The resulting value that is equal to subtracting one from the current value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T SubtractOne()
        {
            switch (Value)
            {
                case byte _:
                    return (T)(object)(byte)((byte)(object)Value - 1);
                case sbyte _:
                    return (T)(object)(sbyte)((sbyte)(object)Value - 1);
                case short _:
                    return (T)(object)(short)((short)(object)Value - 1);
                case ushort _:
                    return (T)(object)(ushort)((ushort)(object)Value - 1);
                case int _:
                    return (T)(object)((int)(object)Value - 1);
                case uint _:
                    return (T)(object)((uint)(object)Value - 1);
                case long _:
                    return (T)(object)((long)(object)Value - 1);
                case ulong _:
                    return (T)(object)((ulong)(object)Value - 1);
                case float _:
                    return (T)(object)((float)(object)Value - 1);
                case double _:
                    return (T)(object)((double)(object)Value - 1);
                case decimal _:
                    return (T)(object)((decimal)(object)Value - 1);
            }
            // Should never be reached
            return default;
        }
        /// <summary>Returns a value that is equal to this instance's current value increased by the specified value. The instance remains unaffected.</summary>
        /// <param name="value">The value to add to the current value.</param>
        /// <returns>The resulting value that is equal to adding the specified value to the current value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T Add(INumeric<T> value)
        {
            switch (Value)
            {
                case byte _:
                    return (T)(object)(byte)((byte)(object)Value + (byte)(object)value.Value);
                case sbyte _:
                    return (T)(object)(sbyte)((sbyte)(object)Value + (sbyte)(object)value.Value);
                case short _:
                    return (T)(object)(short)((short)(object)Value + (short)(object)value.Value);
                case ushort _:
                    return (T)(object)(ushort)((ushort)(object)Value + (ushort)(object)value.Value);
                case int _:
                    return (T)(object)((int)(object)Value + (int)(object)value.Value);
                case uint _:
                    return (T)(object)((uint)(object)Value + (uint)(object)value.Value);
                case long _:
                    return (T)(object)((long)(object)Value + (long)(object)value.Value);
                case ulong _:
                    return (T)(object)((ulong)(object)Value + (ulong)(object)value.Value);
                case float _:
                    return (T)(object)((float)(object)Value + (float)(object)value.Value);
                case double _:
                    return (T)(object)((double)(object)Value + (double)(object)value.Value);
                case decimal _:
                    return (T)(object)((decimal)(object)Value + (decimal)(object)value.Value);
            }
            // Should never be reached
            return default;
        }
        /// <summary>Returns a value that is equal to this instance's current value decreased by the specified value. The instance remains unaffected.</summary>
        /// <param name="value">The value to subtract from the current value.</param>
        /// <returns>The resulting value that is equal to subtracting the specified value from the current value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T Subtract(INumeric<T> value)
        {
            switch (Value)
            {
                case byte _:
                    return (T)(object)(byte)((byte)(object)Value - (byte)(object)value.Value);
                case sbyte _:
                    return (T)(object)(sbyte)((sbyte)(object)Value - (sbyte)(object)value.Value);
                case short _:
                    return (T)(object)(short)((short)(object)Value - (short)(object)value.Value);
                case ushort _:
                    return (T)(object)(ushort)((ushort)(object)Value - (ushort)(object)value.Value);
                case int _:
                    return (T)(object)((int)(object)Value - (int)(object)value.Value);
                case uint _:
                    return (T)(object)((uint)(object)Value - (uint)(object)value.Value);
                case long _:
                    return (T)(object)((long)(object)Value - (long)(object)value.Value);
                case ulong _:
                    return (T)(object)((ulong)(object)Value - (ulong)(object)value.Value);
                case float _:
                    return (T)(object)((float)(object)Value - (float)(object)value.Value);
                case double _:
                    return (T)(object)((double)(object)Value - (double)(object)value.Value);
                case decimal _:
                    return (T)(object)((decimal)(object)Value - (decimal)(object)value.Value);
            }
            // Should never be reached
            return default;
        }
        /// <summary>Returns a value that is equal to this instance's current value multiplied by the specified value. The instance remains unaffected.</summary>
        /// <param name="value">The value to multiply the current value by.</param>
        /// <returns>The resulting value that is equal to multiplying the current value by the specified value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T Multiply(INumeric<T> value)
        {
            switch (Value)
            {
                case byte _:
                    return (T)(object)(byte)((byte)(object)Value * (byte)(object)value.Value);
                case sbyte _:
                    return (T)(object)(sbyte)((sbyte)(object)Value * (sbyte)(object)value.Value);
                case short _:
                    return (T)(object)(short)((short)(object)Value * (short)(object)value.Value);
                case ushort _:
                    return (T)(object)(ushort)((ushort)(object)Value * (ushort)(object)value.Value);
                case int _:
                    return (T)(object)((int)(object)Value * (int)(object)value.Value);
                case uint _:
                    return (T)(object)((uint)(object)Value * (uint)(object)value.Value);
                case long _:
                    return (T)(object)((long)(object)Value * (long)(object)value.Value);
                case ulong _:
                    return (T)(object)((ulong)(object)Value * (ulong)(object)value.Value);
                case float _:
                    return (T)(object)((float)(object)Value * (float)(object)value.Value);
                case double _:
                    return (T)(object)((double)(object)Value * (double)(object)value.Value);
                case decimal _:
                    return (T)(object)((decimal)(object)Value * (decimal)(object)value.Value);
            }
            // Should never be reached
            return default;
        }
        /// <summary>Returns a value that is equal to this instance's current value divided by the specified value. The instance remains unaffected.</summary>
        /// <param name="value">The value to divide the current value by.</param>
        /// <returns>The resulting value that is equal to dividing the current value by the specified value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T Divide(INumeric<T> value)
        {
            switch (Value)
            {
                case byte _:
                    return (T)(object)(byte)((byte)(object)Value / (byte)(object)value.Value);
                case sbyte _:
                    return (T)(object)(sbyte)((sbyte)(object)Value / (sbyte)(object)value.Value);
                case short _:
                    return (T)(object)(short)((short)(object)Value / (short)(object)value.Value);
                case ushort _:
                    return (T)(object)(ushort)((ushort)(object)Value / (ushort)(object)value.Value);
                case int _:
                    return (T)(object)((int)(object)Value / (int)(object)value.Value);
                case uint _:
                    return (T)(object)((uint)(object)Value / (uint)(object)value.Value);
                case long _:
                    return (T)(object)((long)(object)Value / (long)(object)value.Value);
                case ulong _:
                    return (T)(object)((ulong)(object)Value / (ulong)(object)value.Value);
                case float _:
                    return (T)(object)((float)(object)Value / (float)(object)value.Value);
                case double _:
                    return (T)(object)((double)(object)Value / (double)(object)value.Value);
                case decimal _:
                    return (T)(object)((decimal)(object)Value / (decimal)(object)value.Value);
            }
            // Should never be reached
            return default;
        }
        /// <summary>Returns a value that is equal to the remainder of this instance's current value divided by the specified value. The instance remains unaffected.</summary>
        /// <param name="value">The value to divide the current value by.</param>
        /// <returns>The resulting value that is equal to the remainder of dividing the current value by the specified value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T Modulo(INumeric<T> value)
        {
            switch (Value)
            {
                case byte _:
                    return (T)(object)(byte)((byte)(object)Value % (byte)(object)value.Value);
                case sbyte _:
                    return (T)(object)(sbyte)((sbyte)(object)Value % (sbyte)(object)value.Value);
                case short _:
                    return (T)(object)(short)((short)(object)Value % (short)(object)value.Value);
                case ushort _:
                    return (T)(object)(ushort)((ushort)(object)Value % (ushort)(object)value.Value);
                case int _:
                    return (T)(object)((int)(object)Value % (int)(object)value.Value);
                case uint _:
                    return (T)(object)((uint)(object)Value % (uint)(object)value.Value);
                case long _:
                    return (T)(object)((long)(object)Value % (long)(object)value.Value);
                case ulong _:
                    return (T)(object)((ulong)(object)Value % (ulong)(object)value.Value);
                case float _:
                    return (T)(object)((float)(object)Value % (float)(object)value.Value);
                case double _:
                    return (T)(object)((double)(object)Value % (double)(object)value.Value);
                case decimal _:
                    return (T)(object)((decimal)(object)Value % (decimal)(object)value.Value);
            }
            // Should never be reached
            return default;
        }

        /// <summary>Determines whether this instance equals another <seealso cref="INumeric{T}"/> instance.</summary>
        /// <param name="other">The other <seealso cref="INumeric{T}"/> to determine whether it is equal.</param>
        /// <returns><see langword="true"/> if this <seealso cref="INumeric{T}"/> equals the other <seealso cref="INumeric{T}"/> instance, otherwise <see langword="false"/>.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed bool EqualsNumeric(INumeric<T>? other)
        {
            if (other is null)
                return false;

            return Value.Equals(other.Value);
        }
        /// <summary>Compares this instance to another <seealso cref="INumeric{T}"/> instance.</summary>
        /// <param name="other">The other <seealso cref="INumeric{T}"/> to compare it to.</param>
        /// <returns>The result of the <seealso cref="IComparable{T}.CompareTo(T)"/>.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed int CompareToNumeric(INumeric<T>? other)
        {
            if (other is null)
                return int.MaxValue;

            return Value.CompareTo(other.Value);
        }
    }
}