using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace Garyon.Extensions
{
    /// <summary>Contains extension methods for numeric structures.</summary>
    public static class NumericExtensions
    {
        #region IsPowerOfTwo
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this byte value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount(value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1. Negative values are ignored.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this short value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount((uint)value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1. Negative values are ignored.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this int value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount((uint)value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1. Negative values are ignored.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this long value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount((ulong)value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1. Negative values are ignored.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this sbyte value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount((uint)value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this ushort value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount(value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this uint value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount(value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        /// <summary>Determines whether the value is a power of 2, using popcnt if supported, otherwise counting whether there only is 1 bit equal to 1.</summary>
        /// <param name="value">The value to determine whether it is a power of 2.</param>
        public static bool IsPowerOfTwo(this ulong value)
        {
            if (Popcnt.IsSupported)
                return BitOperations.PopCount(value) == 1;
            return value > 0 && (value & (value - 1)) == 0;
        }
        #endregion

        #region OneOrGreater
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="value">The value to determine whether it is ≥1.</param>
        public static byte OneOrGreater(this byte value) => value < 1 ? 1 : value;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="value">The value to determine whether it is ≥1.</param>
        public static short OneOrGreater(this short value) => value < 1 ? 1 : value;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="value">The value to determine whether it is ≥1.</param>
        public static int OneOrGreater(this int value) => value < 1 ? 1 : value;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="value">The value to determine whether it is ≥1.</param>
        public static long OneOrGreater(this long value) => value < 1 ? 1 : value;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="value">The value to determine whether it is ≥1.</param>
        public static sbyte OneOrGreater(this sbyte value) => value < 1 ? 1 : value;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="value">The value to determine whether it is ≥1.</param>
        public static ushort OneOrGreater(this ushort value) => value < 1 ? 1 : value;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="value">The value to determine whether it is ≥1.</param>
        public static uint OneOrGreater(this uint value) => value < 1 ? 1 : value;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="value">The value to determine whether it is ≥1.</param>
        public static ulong OneOrGreater(this ulong value) => value < 1 ? 1 : value;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="value">The value to determine whether it is ≥1.</param>
        public static float OneOrGreater(this float value) => value < 1 ? 1 : value;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="value">The value to determine whether it is ≥1.</param>
        public static double OneOrGreater(this double value) => value < 1 ? 1 : value;
        /// <summary>Determines whether the value is ≥1 and returns the value if ≥1, otherwise returns 1.</summary>
        /// <param name="value">The value to determine whether it is ≥1.</param>
        public static decimal OneOrGreater(this decimal value) => value < 1 ? 1 : value;
        #endregion

        #region ZeroOrGreater
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="value">The value to determine whether it is ≥0.</param>
        public static byte ZeroOrGreater(this byte value) => value < 0 ? 0 : value;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="value">The value to determine whether it is ≥0.</param>
        public static short ZeroOrGreater(this short value) => value < 0 ? 0 : value;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="value">The value to determine whether it is ≥0.</param>
        public static int ZeroOrGreater(this int value) => value < 0 ? 0 : value;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="value">The value to determine whether it is ≥0.</param>
        public static long ZeroOrGreater(this long value) => value < 0 ? 0 : value;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="value">The value to determine whether it is ≥0.</param>
        public static sbyte ZeroOrGreater(this sbyte value) => value < 0 ? 0 : value;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="value">The value to determine whether it is ≥0.</param>
        public static ushort ZeroOrGreater(this ushort value) => value < 0 ? 0 : value;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="value">The value to determine whether it is ≥0.</param>
        public static uint ZeroOrGreater(this uint value) => value < 0 ? 0 : value;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="value">The value to determine whether it is ≥0.</param>
        public static ulong ZeroOrGreater(this ulong value) => value < 0 ? 0 : value;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="value">The value to determine whether it is ≥0.</param>
        public static float ZeroOrGreater(this float value) => value < 0 ? 0 : value;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="value">The value to determine whether it is ≥0.</param>
        public static double ZeroOrGreater(this double value) => value < 0 ? 0 : value;
        /// <summary>Determines whether the value is ≥0 and returns the value if ≥0, otherwise returns 0.</summary>
        /// <param name="value">The value to determine whether it is ≥0.</param>
        public static decimal ZeroOrGreater(this decimal value) => value < 0 ? 0 : value;
        #endregion
    }
}
