using Garyon.Exceptions;
using System;

namespace Garyon.Extensions
{
    /// <summary>Provides extensions for the binary representation of values.</summary>
    public static unsafe class BinaryRepresentationExtensions
    {
        #region Binary Representations
        /// <summary>Gets the binary representation of the <seealso cref="byte"/> value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits a <seealso cref="byte"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetBinaryRepresentation(this byte value, int totalBits = sizeof(byte) * 8)
        {
            totalBits = Math.Min(totalBits, sizeof(byte) * 8);
            return GetBinaryRepresentation((ulong)value, totalBits);
        }
        /// <summary>Gets the binary representation of the <seealso cref="sbyte"/> value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits a <seealso cref="sbyte"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetBinaryRepresentation(this sbyte value, int totalBits = sizeof(sbyte) * 8)
        {
            return GetBinaryRepresentation(*(byte*)&value, totalBits);
        }
        /// <summary>Gets the binary representation of the <seealso cref="short"/> value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits a <seealso cref="short"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetBinaryRepresentation(this short value, int totalBits = sizeof(short) * 8)
        {
            return GetBinaryRepresentation(*(ushort*)&value, totalBits);
        }
        /// <summary>Gets the binary representation of the <seealso cref="ushort"/> value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits a <seealso cref="ushort"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetBinaryRepresentation(this ushort value, int totalBits = sizeof(ushort) * 8)
        {
            totalBits = Math.Min(totalBits, sizeof(ushort) * 8);
            return GetBinaryRepresentation((ulong)value, totalBits);
        }
        /// <summary>Gets the binary representation of the <seealso cref="int"/> value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits an <seealso cref="int"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetBinaryRepresentation(this int value, int totalBits = sizeof(int) * 8)
        {
            return GetBinaryRepresentation(*(uint*)&value, totalBits);
        }
        /// <summary>Gets the binary representation of the <seealso cref="uint"/> value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits a <seealso cref="uint"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetBinaryRepresentation(this uint value, int totalBits = sizeof(uint) * 8)
        {
            totalBits = Math.Min(totalBits, sizeof(uint) * 8);
            return GetBinaryRepresentation((ulong)value, totalBits);
        }
        /// <summary>Gets the binary representation of the <seealso cref="long"/> value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits a <seealso cref="long"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetBinaryRepresentation(this long value, int totalBits = sizeof(long) * 8)
        {
            return GetBinaryRepresentation(*(ulong*)&value, totalBits);
        }
        /// <summary>Gets the binary representation of the <seealso cref="ulong"/> value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits a <seealso cref="ulong"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetBinaryRepresentation(this ulong value, int totalBits = sizeof(ulong) * 8)
        {
            if (totalBits <= 0)
                ThrowHelper.Throw<ArgumentException>("The total bits cannot be negative or zero.");

            totalBits = Math.Min(totalBits, sizeof(ulong) * 8);

            char[] chars = new char[totalBits];
            for (int i = 0; i < totalBits; i++)
                chars[^(i + 1)] = (value & (1UL << i)) > 0 ? '1' : '0';
            return new string(chars);
        }

        /// <summary>Gets the binary representation of the <seealso cref="byte"/> value, while also grouping the digits.</summary>
        /// <param name="value">The value.</param>
        /// <param name="groupLength">The length of each digit group in the resulting binary representation. Must be non-negative. If 0, no grouping is performed.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits a <seealso cref="byte"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetGroupedBinaryRepresentation(this byte value, int groupLength, int totalBits = sizeof(byte) * 8)
        {
            totalBits = Math.Min(totalBits, sizeof(byte) * 8);
            return GetGroupedBinaryRepresentation((ulong)value, groupLength, totalBits);
        }
        /// <summary>Gets the binary representation of the <seealso cref="sbyte"/> value, while also grouping the digits.</summary>
        /// <param name="value">The value.</param>
        /// <param name="groupLength">The length of each digit group in the resulting binary representation. Must be non-negative. If 0, no grouping is performed.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits a <seealso cref="sbyte"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetGroupedBinaryRepresentation(this sbyte value, int groupLength, int totalBits = sizeof(sbyte) * 8)
        {
            return GetGroupedBinaryRepresentation(*(byte*)&value, groupLength, totalBits);
        }
        /// <summary>Gets the binary representation of the <seealso cref="short"/> value, while also grouping the digits.</summary>
        /// <param name="value">The value.</param>
        /// <param name="groupLength">The length of each digit group in the resulting binary representation. Must be non-negative. If 0, no grouping is performed.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits a <seealso cref="short"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetGroupedBinaryRepresentation(this short value, int groupLength, int totalBits = sizeof(short) * 8)
        {
            return GetGroupedBinaryRepresentation(*(ushort*)&value, groupLength, totalBits);
        }
        /// <summary>Gets the binary representation of the <seealso cref="ushort"/> value, while also grouping the digits.</summary>
        /// <param name="value">The value.</param>
        /// <param name="groupLength">The length of each digit group in the resulting binary representation. Must be non-negative. If 0, no grouping is performed.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits a <seealso cref="ushort"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetGroupedBinaryRepresentation(this ushort value, int groupLength, int totalBits = sizeof(ushort) * 8)
        {
            totalBits = Math.Min(totalBits, sizeof(ushort) * 8);
            return GetGroupedBinaryRepresentation((ulong)value, groupLength, totalBits);
        }
        /// <summary>Gets the binary representation of the <seealso cref="int"/> value, while also grouping the digits.</summary>
        /// <param name="value">The value.</param>
        /// <param name="groupLength">The length of each digit group in the resulting binary representation. Must be non-negative. If 0, no grouping is performed.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits a <seealso cref="int"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetGroupedBinaryRepresentation(this int value, int groupLength, int totalBits = sizeof(int) * 8)
        {
            return GetGroupedBinaryRepresentation(*(uint*)&value, groupLength, totalBits);
        }
        /// <summary>Gets the binary representation of the <seealso cref="uint"/> value, while also grouping the digits.</summary>
        /// <param name="value">The value.</param>
        /// <param name="groupLength">The length of each digit group in the resulting binary representation. Must be non-negative. If 0, no grouping is performed.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits a <seealso cref="uint"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetGroupedBinaryRepresentation(this uint value, int groupLength, int totalBits = sizeof(uint) * 8)
        {
            totalBits = Math.Min(totalBits, sizeof(uint) * 8);
            return GetGroupedBinaryRepresentation((ulong)value, groupLength, totalBits);
        }
        /// <summary>Gets the binary representation of the <seealso cref="long"/> value, while also grouping the digits.</summary>
        /// <param name="value">The value.</param>
        /// <param name="groupLength">The length of each digit group in the resulting binary representation. Must be non-negative. If 0, no grouping is performed.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits a <seealso cref="long"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetGroupedBinaryRepresentation(this long value, int groupLength, int totalBits = sizeof(long) * 8)
        {
            return GetGroupedBinaryRepresentation(*(ulong*)&value, groupLength, totalBits);
        }
        /// <summary>Gets the binary representation of the <seealso cref="ulong"/> value, while also grouping the digits.</summary>
        /// <param name="value">The value.</param>
        /// <param name="groupLength">The length of each digit group in the resulting binary representation. Must be non-negative. If 0, no grouping is performed.</param>
        /// <param name="totalBits">The total bits to display. Defaults to the number of bits a <seealso cref="ulong"/> value contains.</param>
        /// <returns>The binary representation of the value.</returns>
        public static string GetGroupedBinaryRepresentation(this ulong value, int groupLength, int totalBits = sizeof(ulong) * 8)
        {
            if (groupLength < 0)
                ThrowHelper.Throw<ArgumentException>("The group length cannot be negative.");

            var baseRepresentation = value.GetBinaryRepresentation(totalBits);
            if (groupLength == 0)
                return baseRepresentation;

            var representationChars = baseRepresentation.ToCharArray();
            int totalGroups = totalBits / groupLength;

            if (totalBits % groupLength > 0)
                totalGroups++;

            int resultingLength = totalBits + totalGroups - 1;
            var resultingChars = new char[resultingLength];

            for (int i = 0; i < totalBits; i++)
            {
                for (int j = 0; j < groupLength; j++)
                {
                    int index = i * groupLength + j + 1;
                    if (index + i > resultingLength)
                        break;

                    resultingChars[^(index + i)] = representationChars[^index];
                }
                int emptyCharIndex = (i + 1) * (groupLength + 1);
                if (emptyCharIndex < resultingLength)
                    resultingChars[^emptyCharIndex] = ' ';
            }

            return new string(resultingChars);
        }
        #endregion
    }
}
