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
                return default(T) switch
                {
                    byte _ => (T)(object)byte.MinValue,
                    sbyte _ => (T)(object)sbyte.MinValue,
                    short _ => (T)(object)short.MinValue,
                    ushort _ => (T)(object)ushort.MinValue,
                    int _ => (T)(object)int.MinValue,
                    uint _ => (T)(object)uint.MinValue,
                    long _ => (T)(object)long.MinValue,
                    ulong _ => (T)(object)ulong.MinValue,
                    float _ => (T)(object)float.MinValue,
                    double _ => (T)(object)double.MinValue,
                    decimal _ => (T)(object)decimal.MinValue,
                    // Should never be reached
                    _ => default,
                };
            }
        }
        /// <summary>Gets the largest possible value of a <typeparamref name="T"/>. It uses the respective MaxValue <see langword="const"/> field.</summary>
        public static T MaxValue
        {
            get
            {
                return default(T) switch
                {
                    byte _ => (T)(object)byte.MaxValue,
                    sbyte _ => (T)(object)sbyte.MaxValue,
                    short _ => (T)(object)short.MaxValue,
                    ushort _ => (T)(object)ushort.MaxValue,
                    int _ => (T)(object)int.MaxValue,
                    uint _ => (T)(object)uint.MaxValue,
                    long _ => (T)(object)long.MaxValue,
                    ulong _ => (T)(object)ulong.MaxValue,
                    float _ => (T)(object)float.MaxValue,
                    double _ => (T)(object)double.MaxValue,
                    decimal _ => (T)(object)decimal.MaxValue,
                    // Should never be reached
                    _ => default,
                };
            }
        }

        /// <summary>Gets the numeric value.</summary>
        public T Value { get; }

        /// <summary>Returns a value that is equal to this instance's current value increased by one. The instance remains unaffected.</summary>
        /// <returns>The resulting value that is equal to adding one to the current value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T AddOne()
        {
            return Value switch
            {
                byte _ => (T)(object)(byte)((byte)(object)Value + 1),
                sbyte _ => (T)(object)(sbyte)((sbyte)(object)Value + 1),
                short _ => (T)(object)(short)((short)(object)Value + 1),
                ushort _ => (T)(object)(ushort)((ushort)(object)Value + 1),
                int _ => (T)(object)((int)(object)Value + 1),
                uint _ => (T)(object)((uint)(object)Value + 1),
                long _ => (T)(object)((long)(object)Value + 1),
                ulong _ => (T)(object)((ulong)(object)Value + 1),
                float _ => (T)(object)((float)(object)Value + 1),
                double _ => (T)(object)((double)(object)Value + 1),
                decimal _ => (T)(object)((decimal)(object)Value + 1),
                // Should never be reached
                _ => default,
            };
        }
        /// <summary>Returns a value that is equal to this instance's current value decreased by one. The instance remains unaffected.</summary>
        /// <returns>The resulting value that is equal to subtracting one from the current value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T SubtractOne()
        {
            return Value switch
            {
                byte _ => (T)(object)(byte)((byte)(object)Value - 1),
                sbyte _ => (T)(object)(sbyte)((sbyte)(object)Value - 1),
                short _ => (T)(object)(short)((short)(object)Value - 1),
                ushort _ => (T)(object)(ushort)((ushort)(object)Value - 1),
                int _ => (T)(object)((int)(object)Value - 1),
                uint _ => (T)(object)((uint)(object)Value - 1),
                long _ => (T)(object)((long)(object)Value - 1),
                ulong _ => (T)(object)((ulong)(object)Value - 1),
                float _ => (T)(object)((float)(object)Value - 1),
                double _ => (T)(object)((double)(object)Value - 1),
                decimal _ => (T)(object)((decimal)(object)Value - 1),
                // Should never be reached
                _ => default,
            };
        }
        /// <summary>Returns a value that is equal to this instance's current value increased by the specified value. The instance remains unaffected.</summary>
        /// <param name="value">The value to add to the current value.</param>
        /// <returns>The resulting value that is equal to adding the specified value to the current value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T Add(INumeric<T> value)
        {
            return Value switch
            {
                byte _ => (T)(object)(byte)((byte)(object)Value + (byte)(object)value.Value),
                sbyte _ => (T)(object)(sbyte)((sbyte)(object)Value + (sbyte)(object)value.Value),
                short _ => (T)(object)(short)((short)(object)Value + (short)(object)value.Value),
                ushort _ => (T)(object)(ushort)((ushort)(object)Value + (ushort)(object)value.Value),
                int _ => (T)(object)((int)(object)Value + (int)(object)value.Value),
                uint _ => (T)(object)((uint)(object)Value + (uint)(object)value.Value),
                long _ => (T)(object)((long)(object)Value + (long)(object)value.Value),
                ulong _ => (T)(object)((ulong)(object)Value + (ulong)(object)value.Value),
                float _ => (T)(object)((float)(object)Value + (float)(object)value.Value),
                double _ => (T)(object)((double)(object)Value + (double)(object)value.Value),
                decimal _ => (T)(object)((decimal)(object)Value + (decimal)(object)value.Value),
                // Should never be reached
                _ => default,
            };
        }
        /// <summary>Returns a value that is equal to this instance's current value decreased by the specified value. The instance remains unaffected.</summary>
        /// <param name="value">The value to subtract from the current value.</param>
        /// <returns>The resulting value that is equal to subtracting the specified value from the current value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T Subtract(INumeric<T> value)
        {
            return Value switch
            {
                byte _ => (T)(object)(byte)((byte)(object)Value - (byte)(object)value.Value),
                sbyte _ => (T)(object)(sbyte)((sbyte)(object)Value - (sbyte)(object)value.Value),
                short _ => (T)(object)(short)((short)(object)Value - (short)(object)value.Value),
                ushort _ => (T)(object)(ushort)((ushort)(object)Value - (ushort)(object)value.Value),
                int _ => (T)(object)((int)(object)Value - (int)(object)value.Value),
                uint _ => (T)(object)((uint)(object)Value - (uint)(object)value.Value),
                long _ => (T)(object)((long)(object)Value - (long)(object)value.Value),
                ulong _ => (T)(object)((ulong)(object)Value - (ulong)(object)value.Value),
                float _ => (T)(object)((float)(object)Value - (float)(object)value.Value),
                double _ => (T)(object)((double)(object)Value - (double)(object)value.Value),
                decimal _ => (T)(object)((decimal)(object)Value - (decimal)(object)value.Value),
                // Should never be reached
                _ => default,
            };
        }
        /// <summary>Returns a value that is equal to this instance's current value multiplied by the specified value. The instance remains unaffected.</summary>
        /// <param name="value">The value to multiply the current value by.</param>
        /// <returns>The resulting value that is equal to multiplying the current value by the specified value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T Multiply(INumeric<T> value)
        {
            return Value switch
            {
                byte _ => (T)(object)(byte)((byte)(object)Value * (byte)(object)value.Value),
                sbyte _ => (T)(object)(sbyte)((sbyte)(object)Value * (sbyte)(object)value.Value),
                short _ => (T)(object)(short)((short)(object)Value * (short)(object)value.Value),
                ushort _ => (T)(object)(ushort)((ushort)(object)Value * (ushort)(object)value.Value),
                int _ => (T)(object)((int)(object)Value * (int)(object)value.Value),
                uint _ => (T)(object)((uint)(object)Value * (uint)(object)value.Value),
                long _ => (T)(object)((long)(object)Value * (long)(object)value.Value),
                ulong _ => (T)(object)((ulong)(object)Value * (ulong)(object)value.Value),
                float _ => (T)(object)((float)(object)Value * (float)(object)value.Value),
                double _ => (T)(object)((double)(object)Value * (double)(object)value.Value),
                decimal _ => (T)(object)((decimal)(object)Value * (decimal)(object)value.Value),
                // Should never be reached
                _ => default,
            };
        }
        /// <summary>Returns a value that is equal to this instance's current value divided by the specified value. The instance remains unaffected.</summary>
        /// <param name="value">The value to divide the current value by.</param>
        /// <returns>The resulting value that is equal to dividing the current value by the specified value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T Divide(INumeric<T> value)
        {
            return Value switch
            {
                byte _ => (T)(object)(byte)((byte)(object)Value / (byte)(object)value.Value),
                sbyte _ => (T)(object)(sbyte)((sbyte)(object)Value / (sbyte)(object)value.Value),
                short _ => (T)(object)(short)((short)(object)Value / (short)(object)value.Value),
                ushort _ => (T)(object)(ushort)((ushort)(object)Value / (ushort)(object)value.Value),
                int _ => (T)(object)((int)(object)Value / (int)(object)value.Value),
                uint _ => (T)(object)((uint)(object)Value / (uint)(object)value.Value),
                long _ => (T)(object)((long)(object)Value / (long)(object)value.Value),
                ulong _ => (T)(object)((ulong)(object)Value / (ulong)(object)value.Value),
                float _ => (T)(object)((float)(object)Value / (float)(object)value.Value),
                double _ => (T)(object)((double)(object)Value / (double)(object)value.Value),
                decimal _ => (T)(object)((decimal)(object)Value / (decimal)(object)value.Value),
                // Should never be reached
                _ => default,
            };
        }
        /// <summary>Returns a value that is equal to the remainder of this instance's current value divided by the specified value. The instance remains unaffected.</summary>
        /// <param name="value">The value to divide the current value by.</param>
        /// <returns>The resulting value that is equal to the remainder of dividing the current value by the specified value.</returns>
        /// <remarks>This function should only be used internally in types that implement this interface. Prefer using the supported operator overloads for such operations.</remarks>
        public sealed T Modulo(INumeric<T> value)
        {
            return Value switch
            {
                byte _ => (T)(object)(byte)((byte)(object)Value % (byte)(object)value.Value),
                sbyte _ => (T)(object)(sbyte)((sbyte)(object)Value % (sbyte)(object)value.Value),
                short _ => (T)(object)(short)((short)(object)Value % (short)(object)value.Value),
                ushort _ => (T)(object)(ushort)((ushort)(object)Value % (ushort)(object)value.Value),
                int _ => (T)(object)((int)(object)Value % (int)(object)value.Value),
                uint _ => (T)(object)((uint)(object)Value % (uint)(object)value.Value),
                long _ => (T)(object)((long)(object)Value % (long)(object)value.Value),
                ulong _ => (T)(object)((ulong)(object)Value % (ulong)(object)value.Value),
                float _ => (T)(object)((float)(object)Value % (float)(object)value.Value),
                double _ => (T)(object)((double)(object)Value % (double)(object)value.Value),
                decimal _ => (T)(object)((decimal)(object)Value % (decimal)(object)value.Value),
                // Should never be reached
                _ => default,
            };
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