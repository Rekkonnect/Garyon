namespace Garyon.Extensions
{
    /// <summary>Provides inline extensions for applying unary operators.</summary>
    public static class UnaryOperatorExtensions
    {
        /// <summary>Negates the given value (the "-" operator), and stores the result to the provided reference.</summary>
        /// <param name="value">The reference to the value whose negation will be stored.</param>
        /// <remarks>This is a reference extension; the value of the reference is adjusted.</remarks>
        public static void Negate(this ref sbyte value) => value = (sbyte)-value;
        /// <inheritdoc cref="Negate(ref sbyte)"/>
        public static void Negate(this ref short value) => value = (short)-value;
        /// <inheritdoc cref="Negate(ref sbyte)"/>
        public static void Negate(this ref int value) => value = -value;
        /// <inheritdoc cref="Negate(ref sbyte)"/>
        public static void Negate(this ref long value) => value = -value;
        /// <inheritdoc cref="Negate(ref sbyte)"/>
        public static void Negate(this ref float value) => value = -value;
        /// <inheritdoc cref="Negate(ref sbyte)"/>
        public static void Negate(this ref double value) => value = -value;
        /// <inheritdoc cref="Negate(ref sbyte)"/>
        public static void Negate(this ref decimal value) => value = -value;

        /// <summary>Inverts the given value bitwise (the "~" operator), and stores the result to the provided reference.</summary>
        /// <param name="value">The reference to the value whose bitwise inversion will be stored.</param>
        /// <remarks>This is a reference extension; the value of the reference is adjusted.</remarks>
        public static void InvertBitwise(this ref byte value) => value = (byte)~value;
        /// <inheritdoc cref="InvertBitwise(ref byte)"/>
        public static void InvertBitwise(this ref sbyte value) => value = (sbyte)~value;
        /// <inheritdoc cref="InvertBitwise(ref byte)"/>
        public static void InvertBitwise(this ref short value) => value = (short)~value;
        /// <inheritdoc cref="InvertBitwise(ref byte)"/>
        public static void InvertBitwise(this ref ushort value) => value = (ushort)~value;
        /// <inheritdoc cref="InvertBitwise(ref byte)"/>
        public static void InvertBitwise(this ref int value) => value = ~value;
        /// <inheritdoc cref="InvertBitwise(ref byte)"/>
        public static void InvertBitwise(this ref uint value) => value = ~value;
        /// <inheritdoc cref="InvertBitwise(ref byte)"/>
        public static void InvertBitwise(this ref long value) => value = ~value;
        /// <inheritdoc cref="InvertBitwise(ref byte)"/>
        public static void InvertBitwise(this ref ulong value) => value = ~value;

        /// <summary>Inverts the given value (the "!" operator), and stores the result to the provided reference.</summary>
        /// <param name="value">The reference to the value whose inversion will be stored.</param>
        /// <remarks>This is a reference extension; the value of the reference is adjusted.</remarks>
        public static void Invert(this ref bool value) => value = !value;
    }
}
