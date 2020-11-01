using Garyon.Mathematics;
using System.Numerics;

namespace Garyon.Objects
{
    /// <summary>Provides a mechanism that optimally handles operations on a <seealso cref="BigInteger"/> instance by caching smaller chunks of values before being applied to the big boi.</summary>
    public sealed class CacheableBigInteger
    {
        private long cachedAddend = 0;
        private long cachedMultiplier = 1;
        private long cachedDividend = 1;

        private BigInteger cachedValue;

        /// <summary>Gets or sets the value of the <seealso cref="BigInteger"/> instance.</summary>
        public BigInteger Value
        {
            get
            {
                PerformOperations();
                return cachedValue;
            }
            set
            {
                ResetCachedFields();
                cachedValue = value;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="CacheableBigInteger"/> class.</summary>
        public CacheableBigInteger() : this(BigInteger.Zero) { }
        /// <summary>Initializes a new instance of the <seealso cref="CacheableBigInteger"/> class.</summary>
        /// <param name="value">The value to initialize the instance from.</param>
        public CacheableBigInteger(long value) : this((BigInteger)value) { }
        /// <summary>Initializes a new instance of the <seealso cref="CacheableBigInteger"/> class.</summary>
        /// <param name="value">The value to initialize the instance from.</param>
        public CacheableBigInteger(BigInteger value) => cachedValue = value;

        #region Operations Int64
        /// <summary>Adds a value to the current <seealso cref="BigInteger"/> value.</summary>
        /// <param name="value">The value to add to the current <seealso cref="BigInteger"/> value.</param>
        public void Add(long value)
        {
            if (cachedMultiplier != 1 || cachedDividend != 1)
                PerformOperations();

            if (Overflowing.CheckIfAdditionOverflows(cachedAddend, value))
            {
                cachedValue += cachedAddend;
                cachedAddend = 0;
            }

            cachedAddend += value;
        }
        /// <summary>Subtracts a value from the current <seealso cref="BigInteger"/> value.</summary>
        /// <param name="value">The value to subtract from the current <seealso cref="BigInteger"/> value.</param>
        public void Subtract(long value) => Add(-value);
        /// <summary>Multiplies a value to the current <seealso cref="BigInteger"/> value.</summary>
        /// <param name="value">The value to multiply the current <seealso cref="BigInteger"/> value by.</param>
        public void Multiply(long value)
        {
            if (cachedAddend != 0 || cachedDividend != 1)
                PerformOperations();

            if (Overflowing.CheckIfMultiplicationOverflows(cachedMultiplier, value))
            {
                cachedValue *= cachedMultiplier;
                cachedMultiplier = 1;
            }

            cachedMultiplier *= value;
        }
        /// <summary>Divides a value from the current <seealso cref="BigInteger"/> value.</summary>
        /// <param name="value">The value to divide the current <seealso cref="BigInteger"/> value by.</param>
        public void Divide(long value)
        {
            if (cachedAddend != 0 || cachedMultiplier != 1)
                PerformOperations();

            if (Overflowing.CheckIfMultiplicationOverflows(cachedDividend, value))
            {
                cachedValue /= cachedDividend;
                cachedDividend = 1;
            }

            cachedDividend *= value;
        }
        #endregion

        #region Operations BigInteger
        /// <summary>Adds a value to the current <seealso cref="BigInteger"/> value.</summary>
        /// <param name="value">The value to add to the current <seealso cref="BigInteger"/> value.</param>
        public void Add(BigInteger value) => cachedValue = Value + value;
        /// <summary>Subtracts a value from the current <seealso cref="BigInteger"/> value.</summary>
        /// <param name="value">The value to subtract from the current <seealso cref="BigInteger"/> value.</param>
        public void Subtract(BigInteger value) => cachedValue = Value - value;
        /// <summary>Multiplies a value to the current <seealso cref="BigInteger"/> value.</summary>
        /// <param name="value">The value to multiply the current <seealso cref="BigInteger"/> value by.</param>
        public void Multiply(BigInteger value) => cachedValue = Value * value;
        /// <summary>Divides a value from the current <seealso cref="BigInteger"/> value.</summary>
        /// <param name="value">The value to divide the current <seealso cref="BigInteger"/> value by.</param>
        public void Divide(BigInteger value) => cachedValue = Value / value;
        #endregion

        public static implicit operator CacheableBigInteger(long value) => new CacheableBigInteger(value);
        public static implicit operator CacheableBigInteger(BigInteger value) => new CacheableBigInteger(value);
        public static implicit operator BigInteger(CacheableBigInteger value) => value.Value;

        private void PerformOperations()
        {
            if (cachedAddend != 0)
                cachedValue += cachedAddend;
            if (cachedMultiplier != 1)
                cachedValue *= cachedMultiplier;
            if (cachedDividend != 1)
                cachedValue /= cachedDividend;

            ResetCachedFields();
        }
        private void ResetCachedFields()
        {
            cachedAddend = 0;
            cachedMultiplier = 1;
            cachedDividend = 1;
        }
    }
}
