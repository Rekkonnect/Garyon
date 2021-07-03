using Garyon.Exceptions;
using Garyon.Extensions;
using System;

namespace Garyon.Objects
{
    /// <summary>Represents an integer value. This struct encapsulates generalized functionality for the built-in primitive integer types (<seealso cref="byte"/>, <seealso cref="sbyte"/>, <seealso cref="short"/>, <seealso cref="ushort"/>, <seealso cref="int"/>, <seealso cref="uint"/>, <seealso cref="long"/>, <seealso cref="ulong"/>).</summary>
    /// <typeparam name="T">The integer type this struct encapsulates generalized functionality for. Must be one of the valid types: <seealso cref="byte"/>, <seealso cref="sbyte"/>, <seealso cref="short"/>, <seealso cref="ushort"/>, <seealso cref="int"/>, <seealso cref="uint"/>, <seealso cref="long"/>, <seealso cref="ulong"/>.</typeparam>
    public struct Integer<T> : IInteger<T>
        where T : unmanaged, IEquatable<T>, IComparable<T>
    {
        /// <summary>Gets the smallest possible value of a <typeparamref name="T"/>. It uses the respective MinValue <see langword="const"/> field.</summary>
        public static readonly Integer<T> MinValue = new(IInteger<T>.MinValue);
        /// <summary>Gets the largest possible value of a <typeparamref name="T"/>. It uses the respective MaxValue <see langword="const"/> field.</summary>
        public static readonly Integer<T> MaxValue = new(IInteger<T>.MaxValue);

        static Integer()
        {
            ValidateType();
        }

        /// <summary>Gets the value of this instance.</summary>
        public T Value { get; private init; }

        /// <summary>Initializes a new instance of the <seealso cref="Integer{T}"/> struct from a given value.</summary>
        /// <param name="value">The value.</param>
        public Integer(T value)
        {
            ValidateType();
            Value = value;
        }

        private static void ValidateType()
        {
            switch (default(T))
            {
                case byte:
                case sbyte:
                case short:
                case ushort:
                case int:
                case uint:
                case long:
                case ulong:
                    break;
                default:
                    ThrowHelper.Throw<TypeInitializationException>("The provided type is not a supported integer type.");
                    break;
            }
        }

        public static implicit operator Integer<T>(T value) => new(value);

        #region Operators
        #region Arithmetic Operations
        public static Integer<T> operator +(Integer<T> left, Integer<T> right) => new((left as INumeric<T>).Add(right));
        public static Integer<T> operator -(Integer<T> left, Integer<T> right) => new((left as INumeric<T>).Subtract(right));
        public static Integer<T> operator *(Integer<T> left, Integer<T> right) => new((left as INumeric<T>).Multiply(right));
        public static Integer<T> operator /(Integer<T> left, Integer<T> right) => new((left as INumeric<T>).Divide(right));
        public static Integer<T> operator %(Integer<T> left, Integer<T> right) => new((left as INumeric<T>).Modulo(right));
        public static Integer<T> operator ++(Integer<T> value) => new((value as IInteger<T>).AddOne());
        public static Integer<T> operator --(Integer<T> value) => new((value as IInteger<T>).SubtractOne());
        #endregion

        #region Bitwise Operations
        public static Integer<T> operator ~(Integer<T> value) => new((value as IInteger<T>).Not());
        public static Integer<T> operator &(Integer<T> left, Integer<T> right) => new((left as IInteger<T>).And(right));
        public static Integer<T> operator |(Integer<T> left, Integer<T> right) => new((left as IInteger<T>).Or(right));
        public static Integer<T> operator ^(Integer<T> left, Integer<T> right) => new((left as IInteger<T>).Xor(right));
        public static Integer<T> operator <<(Integer<T> value, int shift) => new((value as IInteger<T>).ShiftLeft(shift));
        public static Integer<T> operator >>(Integer<T> value, int shift) => new((value as IInteger<T>).ShiftRight(shift));
        #endregion

        #region Comparison Operations
        public static bool operator <(Integer<T> left, Integer<T> right) => (left as INumeric<T>).LessThan(right);
        public static bool operator <=(Integer<T> left, Integer<T> right) => (left as INumeric<T>).LessThanOrEqual(right);
        public static bool operator >(Integer<T> left, Integer<T> right) => (left as INumeric<T>).GreaterThan(right);
        public static bool operator >=(Integer<T> left, Integer<T> right) => (left as INumeric<T>).GreaterThanOrEqual(right);
        public static bool operator ==(Integer<T> left, Integer<T> right) => left.Equals(right);
        public static bool operator !=(Integer<T> left, Integer<T> right) => !left.Equals(right);
        #endregion
        #endregion

        public bool Equals(INumeric<T>? other) => (this as INumeric<T>).EqualsNumeric(other);
        public int CompareTo(INumeric<T>? other) => (this as INumeric<T>).CompareToNumeric(other);
        public bool Equals(IInteger<T>? other) => (this as IInteger<T>).EqualsNumeric(other);
        public int CompareTo(IInteger<T>? other) => (this as IInteger<T>).CompareToNumeric(other);

        public override bool Equals(object? obj) => obj is IInteger<T> n && Equals(n);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
    }
}
