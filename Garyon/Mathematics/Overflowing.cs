using static System.Math;

namespace Garyon.Mathematics
{
    /// <summary>Contains functions related to overflowing or underflowing.</summary>
    public static class Overflowing
    {
        // Hate how this entire code has to be copy-pasted

        #region Addition
        /// <summary>Determines whether adding two numbers will result in either an overflow or an underflow.</summary>
        /// <param name="x">The one value to add.</param>
        /// <param name="y">The other value to add.</param>
        /// <returns>Whether adding the two numbers will result in either an overflow or an underflow.</returns>
        public static bool CheckIfAdditionOverflows(int x, int y)
        {
            if (x > 0 && y > 0 && y > int.MaxValue - x)
                return true;

            if (x < 0 && y < 0 && y < int.MinValue - x)
                return true;

            return false;
        }
        /// <summary>Determines whether adding two numbers will result in either an overflow or an underflow.</summary>
        /// <param name="x">The one value to add.</param>
        /// <param name="y">The other value to add.</param>
        /// <returns>Whether adding the two numbers will result in either an overflow or an underflow.</returns>
        public static bool CheckIfAdditionOverflows(uint x, uint y)
        {
            if (y > uint.MaxValue - x)
                return true;

            return false;
        }
        /// <summary>Determines whether adding two numbers will result in either an overflow or an underflow.</summary>
        /// <param name="x">The one value to add.</param>
        /// <param name="y">The other value to add.</param>
        /// <returns>Whether adding the two numbers will result in either an overflow or an underflow.</returns>
        public static bool CheckIfAdditionOverflows(long x, long y)
        {
            if (x > 0 && y > 0 && y > long.MaxValue - x)
                return true;

            if (x < 0 && y < 0 && y < long.MinValue - x)
                return true;

            return false;
        }
        /// <summary>Determines whether adding two numbers will result in either an overflow or an underflow.</summary>
        /// <param name="x">The one value to add.</param>
        /// <param name="y">The other value to add.</param>
        /// <returns>Whether adding the two numbers will result in either an overflow or an underflow.</returns>
        public static bool CheckIfAdditionOverflows(ulong x, ulong y)
        {
            if (y > ulong.MaxValue - x)
                return true;

            return false;
        }
        #endregion

        #region Multiplication
        /// <summary>Determines whether multiplying two numbers will result in either an overflow or an underflow.</summary>
        /// <param name="x">The one value to add.</param>
        /// <param name="y">The other value to add.</param>
        /// <returns>Whether multiplying the two numbers will result in either an overflow or an underflow.</returns>
        public static bool CheckIfMultiplicationOverflows(int x, int y)
        {
            // Cool pattern matching
            switch (x, y)
            {
                case (-1, int.MinValue):
                case (int.MinValue, -1):
                    return true;
                case (-1, _):
                case (0, _):
                case (1, _):
                case (_, -1):
                case (_, 0):
                case (_, 1):
                    return false;
            }

            int signX = Sign(x);
            int signY = Sign(y);
            int productSign = signX * signY;

            x = Abs(x);
            y = Abs(y);

            int quotient;

            if (productSign == 1)
                quotient = int.MaxValue / y;
            else
                quotient = Abs(int.MinValue / y);

            if (quotient < x)
                return true;

            return false;
        }
        /// <summary>Determines whether multiplying two numbers will result in either an overflow or an underflow.</summary>
        /// <param name="x">The one value to add.</param>
        /// <param name="y">The other value to add.</param>
        /// <returns>Whether multiplying the two numbers will result in either an overflow or an underflow.</returns>
        public static bool CheckIfMultiplicationOverflows(uint x, uint y)
        {
            switch (x, y)
            {
                case (0, _):
                case (1, _):
                case (_, 0):
                case (_, 1):
                    return false;
            }

            uint quotient = uint.MaxValue / y;

            if (quotient < x)
                return true;

            return false;
        }
        /// <summary>Determines whether multiplying two numbers will result in either an overflow or an underflow.</summary>
        /// <param name="x">The one value to add.</param>
        /// <param name="y">The other value to add.</param>
        /// <returns>Whether multiplying the two numbers will result in either an overflow or an underflow.</returns>
        public static bool CheckIfMultiplicationOverflows(long x, long y)
        {
            switch (x, y)
            {
                case (-1, long.MinValue):
                case (long.MinValue, -1):
                    return true;
                case (-1, _):
                case (0, _):
                case (1, _):
                case (_, -1):
                case (_, 0):
                case (_, 1):
                    return false;
            }

            long signX = Sign(x);
            long signY = Sign(y);
            long productSign = signX * signY;

            x = Abs(x);
            y = Abs(y);

            long quotient;

            if (productSign == 1)
                quotient = long.MaxValue / y;
            else
                quotient = Abs(long.MinValue / y);

            if (quotient < x)
                return true;

            return false;
        }
        /// <summary>Determines whether multiplying two numbers will result in either an overflow or an underflow.</summary>
        /// <param name="x">The one value to add.</param>
        /// <param name="y">The other value to add.</param>
        /// <returns>Whether multiplying the two numbers will result in either an overflow or an underflow.</returns>
        public static bool CheckIfMultiplicationOverflows(ulong x, ulong y)
        {
            switch (x, y)
            {
                case (0, _):
                case (1, _):
                case (_, 0):
                case (_, 1):
                    return false;
            }

            ulong quotient = ulong.MaxValue / y;

            if (quotient < x)
                return true;

            return false;
        }
        #endregion
    }
}
