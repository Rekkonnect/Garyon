using Garyon.Exceptions;
using Garyon.Extensions;
using System;

namespace Garyon.Objects
{
    // It is currently not checked whether T is a valid type upon calling the parameterless constructor
    // The only solution seems to be a syntax analyzer checking for that

    /// <summary>Represents a numeric value. This struct encapsulates generalized functionality for the built-in primitive numeric types (<seealso cref="byte"/>, <seealso cref="sbyte"/>, <seealso cref="short"/>, <seealso cref="ushort"/>, <seealso cref="int"/>, <seealso cref="uint"/>, <seealso cref="long"/>, <seealso cref="ulong"/>, <seealso cref="float"/>, <seealso cref="double"/>, <seealso cref="decimal"/>).</summary>
    /// <typeparam name="T">The numeric type this struct encapsulates generalized functionality for. Must be one of the valid types: <seealso cref="byte"/>, <seealso cref="sbyte"/>, <seealso cref="short"/>, <seealso cref="ushort"/>, <seealso cref="int"/>, <seealso cref="uint"/>, <seealso cref="long"/>, <seealso cref="ulong"/>, <seealso cref="float"/>, <seealso cref="double"/>, <seealso cref="decimal"/>.</typeparam>
    public struct Numeric<T> : INumeric<T>
        where T : unmanaged, IEquatable<T>, IComparable<T>
    {
        /// <summary>Gets the smallest possible value of a <typeparamref name="T"/>. It uses the respective MinValue <see langword="const"/> field.</summary>
        public static readonly Numeric<T> MinValue = new(INumeric<T>.MinValue);
        /// <summary>Gets the largest possible value of a <typeparamref name="T"/>. It uses the respective MaxValue <see langword="const"/> field.</summary>
        public static readonly Numeric<T> MaxValue = new(INumeric<T>.MaxValue);

        static Numeric()
        {
            ValidateType();
        }

        /// <summary>Gets the value of this instance.</summary>
        public T Value { get; private init; }

        /// <summary>Initializes a new instance of the <seealso cref="Numeric{T}"/> struct from a given value.</summary>
        /// <param name="value">The value.</param>
        public Numeric(T value)
            : this()
        {
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
                case float:
                case double:
                case decimal:
                    break;
                default:
                    ThrowHelper.Throw<TypeInitializationException>("The provided type is not a supported numeric type.");
                    break;
            }
        }

        public static implicit operator Numeric<T>(T value) => new(value);

        #region Operators
        #region Arithmetic Operations
        public static Numeric<T> operator +(Numeric<T> left, Numeric<T> right) => new((left as INumeric<T>).Add(right));
        public static Numeric<T> operator -(Numeric<T> left, Numeric<T> right) => new((left as INumeric<T>).Subtract(right));
        public static Numeric<T> operator *(Numeric<T> left, Numeric<T> right) => new((left as INumeric<T>).Multiply(right));
        public static Numeric<T> operator /(Numeric<T> left, Numeric<T> right) => new((left as INumeric<T>).Divide(right));
        public static Numeric<T> operator %(Numeric<T> left, Numeric<T> right) => new((left as INumeric<T>).Modulo(right));
        public static Numeric<T> operator ++(Numeric<T> left) => new((left as INumeric<T>).AddOne());
        public static Numeric<T> operator --(Numeric<T> left) => new((left as INumeric<T>).SubtractOne());
        #endregion

        #region Comparison Operations
        public static bool operator <(Numeric<T> left, Numeric<T> right) => left.LessThan(right);
        public static bool operator <=(Numeric<T> left, Numeric<T> right) => left.LessThanOrEqual(right);
        public static bool operator >(Numeric<T> left, Numeric<T> right) => left.GreaterThan(right);
        public static bool operator >=(Numeric<T> left, Numeric<T> right) => left.GreaterThanOrEqual(right);
        public static bool operator ==(Numeric<T> left, Numeric<T> right) => left.Equals(right);
        public static bool operator !=(Numeric<T> left, Numeric<T> right) => !left.Equals(right);
        #endregion
        #endregion

        public bool Equals(INumeric<T>? other) => (this as INumeric<T>).EqualsNumeric(other);
        public int CompareTo(INumeric<T>? other) => (this as INumeric<T>).CompareToNumeric(other);

        public override bool Equals(object? obj) => obj is INumeric<T> n && Equals(n);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
    }
}
