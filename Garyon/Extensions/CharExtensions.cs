using System.Globalization;

namespace Garyon.Extensions
{
    /// <summary>Provides extensions for the <seealso cref="char"/> type.</summary>
    public static class CharExtensions
    {
        #region Letters & Digits
        /// <summary>Checks whether the character is a lower case English letter.</summary>
        /// <param name="c">The character to check whether it is a lower case English letter.</param>
        public static bool IsLowerCaseEnglishLetter(this char c) => c >= 'a' && c <= 'z';
        /// <summary>Checks whether the character is a upper case English letter.</summary>
        /// <param name="c">The character to check whether it is a upper case English letter.</param>
        public static bool IsUpperCaseEnglishLetter(this char c) => c >= 'A' && c <= 'Z';
        /// <summary>Checks whether the character is a English letter character.</summary>
        /// <param name="c">The character to check whether it is a English letter character.</param>
        public static bool IsEnglishLetter(this char c) => IsLowerCaseEnglishLetter(c) || IsUpperCaseEnglishLetter(c);
        /// <summary>Checks whether the character is a English letter or a digit character.</summary>
        /// <param name="c">The character to check whether it is a English letter or a digit character.</param>
        public static bool IsEnglishLetterOrDigit(this char c) => IsEnglishLetter(c) || IsDigit(c);

        /// <summary>Gets the integer numeric value of the specified character.</summary>
        /// <param name="c">The character whose numeric value to get.</param>
        /// <returns>The integer numeric value that the character represents, otherwise -1.</returns>
        public static int GetNumericValueInteger(this char c) => IsDigit(c) ? c - '0' : -1;
        #endregion

        #region Misc
        /// <summary>Gets the numerical value of the decimal digit character.</summary>
        /// <param name="c">The decimal digit character.</param>
        /// <returns>The numerical value.</returns>
        public static int GetNumericalValue(this char c) => c - '0';
        /// <summary>Determines whether the character is a valid hex character.</summary>
        /// <param name="c">The character.</param>
        /// <returns><see langword="true"/> if the character is a valid hex character; equivalent to the regular expression [0-9a-fA-F], otherwise <see langword="false"/>.</returns>
        public static bool IsValidHexCharacter(this char c) => char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
        #endregion

        #region Extension Variants
        /// <inheritdoc cref="char.GetNumericValue(char)"/>
        public static double GetNumericValue(this char c) => char.GetNumericValue(c);
        /// <inheritdoc cref="char.GetUnicodeCategory(char)"/>
        public static UnicodeCategory GetUnicodeCategory(this char c) => char.GetUnicodeCategory(c);
        /// <inheritdoc cref="char.IsControl(char)"/>
        public static bool IsControl(this char c) => char.IsControl(c);
        /// <inheritdoc cref="char.IsDigit(char)"/>
        public static bool IsDigit(this char c) => char.IsDigit(c);
        /// <inheritdoc cref="char.IsHighSurrogate(char)"/>
        public static bool IsHighSurrogate(this char c) => char.IsHighSurrogate(c);
        /// <inheritdoc cref="char.IsLetter(char)"/>
        public static bool IsLetter(this char c) => char.IsLetter(c);
        /// <inheritdoc cref="char.IsLetterOrDigit(char)"/>
        public static bool IsLetterOrDigit(this char c) => char.IsLetterOrDigit(c);
        /// <inheritdoc cref="char.IsLower(char)"/>
        public static bool IsLower(this char c) => char.IsLower(c);
        /// <inheritdoc cref="char.IsLowSurrogate(char)"/>
        public static bool IsLowSurrogate(this char c) => char.IsLowSurrogate(c);
        /// <inheritdoc cref="char.IsNumber(char)"/>
        public static bool IsNumber(this char c) => char.IsNumber(c);
        /// <inheritdoc cref="char.IsPunctuation(char)"/>
        public static bool IsPunctuation(this char c) => char.IsPunctuation(c);
        /// <inheritdoc cref="char.IsSeparator(char)"/>
        public static bool IsSeparator(this char c) => char.IsSeparator(c);
        /// <inheritdoc cref="char.IsSurrogate(char)"/>
        public static bool IsSurrogate(this char c) => char.IsSurrogate(c);
        /// <inheritdoc cref="char.IsSymbol(char)"/>
        public static bool IsSymbol(this char c) => char.IsSymbol(c);
        /// <inheritdoc cref="char.IsUpper(char)"/>
        public static bool IsUpper(this char c) => char.IsUpper(c);
        /// <inheritdoc cref="char.IsWhiteSpace(char)"/>
        public static bool IsWhiteSpace(this char c) => char.IsWhiteSpace(c);
        /// <inheritdoc cref="char.ToLower(char, CultureInfo)"/>
        public static char ToLower(this char c, CultureInfo culture) => char.ToLower(c, culture);
        /// <inheritdoc cref="char.ToLower(char)"/>
        public static char ToLower(this char c) => char.ToLower(c);
        /// <inheritdoc cref="char.ToLowerInvariant(char)"/>
        public static char ToLowerInvariant(this char c) => char.ToLowerInvariant(c);
        /// <inheritdoc cref="char.ToUpper(char)"/>
        public static char ToUpper(this char c) => char.ToUpper(c);
        /// <inheritdoc cref="char.ToUpper(char, CultureInfo)"/>
        public static char ToUpper(this char c, CultureInfo culture) => char.ToUpper(c, culture);
        /// <inheritdoc cref="char.ToUpperInvariant(char)"/>
        public static char ToUpperInvariant(this char c) => char.ToUpperInvariant(c);
        #endregion

        #region Base 64
        /// <summary>Checks whether the character is a valid Base 64 character.</summary>
        /// <param name="c">The character to check whether it is a valid Base 64 character.</param>
        public static bool IsBase64Character(this char c) => IsDigit(c) || IsEnglishLetter(c) || c == '/' || c == '+' || c == '=';
        #endregion
    }
}
