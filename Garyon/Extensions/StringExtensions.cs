using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garyon.Extensions;

/// <summary>Provides extension methods for the <seealso cref="string"/> class.</summary>
public static class StringExtensions
{
    #region String
    #region Find
    /// <summary>Finds a substring within the string. Returns the index of the first character where the match occurred, otherwise -1.</summary>
    /// <param name="s">The string within which the search will be performed.</param>
    /// <param name="match">The substring to match from the original string.</param>
    /// <param name="occurrence">The 1-based index of the occurrence to find.</param>
    public static int FindOccurrence(this string s, string match, int occurrence)
    {
        if (occurrence <= 0)
            throw new ArgumentException("The occurrence cannot be non-positive.");

        int startIndex = 0;
        for (int i = 0; i < occurrence; i++)
        {
            int next = s.IndexOfAfter(match, startIndex);
            if (next < 0)
                return -1;

            startIndex = next;
        }

        return startIndex;
    }
    /// <summary>Finds a substring within the string from the end. Returns the index of the first character where the match occurred, otherwise -1.</summary>
    /// <param name="s">The string within which the search will be performed.</param>
    /// <param name="match">The substring to match from the original string.</param>
    /// <param name="occurrence">The 1-based index of the occurrence to find.</param>
    public static int FindOccurrenceFromEnd(this string s, string match, int occurrence)
    {
        if (occurrence <= 0)
            throw new ArgumentException("The occurrence cannot be non-positive.");

        int startIndex = s.Length - 1;
        for (int i = 0; i < occurrence; i++)
        {
            int next = s.LastIndexOf(match, startIndex);
            if (next < 0)
                return -1;

            startIndex = next;
        }

        return startIndex;
    }
    #endregion

    #region IndexOf
    /// <summary>Gets the index of the first character after the first occurrence of the given sequence in the original string.</summary>
    /// <param name="source">The original string from which to get the first occurrence.</param>
    /// <param name="match">The matching sequence to find the first occurrence of.</param>
    /// <returns>The index of the first character after the first occurrence of <paramref name="match"/> in <paramref name="source"/>, if found, otherwise -1.</returns>
    public static int IndexOfAfter(this string source, string match)
    {
        return source.IndexOfAfter(match, 0);
    }
    /// <summary>Gets the index of the first character after the first occurrence of the given sequence in the original string.</summary>
    /// <param name="source">The original string from which to get the first occurrence.</param>
    /// <param name="match">The matching sequence to find the first occurrence of.</param>
    /// <param name="startIndex">The index of the first character to include to the search, from which onwards the search will be performed.</param>
    /// <returns>The index of the first character after the first occurrence of <paramref name="match"/> in <paramref name="source"/>, if found, otherwise -1.</returns>
    public static int IndexOfAfter(this string source, string match, int startIndex)
    {
        return source.IndexOfAfter(match, startIndex, source.Length - startIndex);
    }
    /// <summary>Gets the index of the first character after the first occurrence of the given sequence in the original string.</summary>
    /// <param name="source">The original string from which to get the first occurrence.</param>
    /// <param name="match">The matching sequence to find the first occurrence of.</param>
    /// <param name="startIndex">The index of the first character to include to the search, from which onwards the search will be performed.</param>
    /// <param name="length">The number of characters that will be searched, starting from the provided starting index.</param>
    /// <returns>The index of the first character after the first occurrence of <paramref name="match"/> in <paramref name="source"/>, if found, otherwise -1.</returns>
    public static int IndexOfAfter(this string source, string match, int startIndex, int length)
    {
        int index = source.IndexOf(match, startIndex, length);
        return index is -1 ? -1 : index + match.Length;
    }
    // TODO: Add more overloads completing the IndexOf API extension
    #endregion

    #region Manipulation
    /// <summary>Removes one character from the end of the string.</summary>
    /// <param name="s">The string whose last character to remove.</param>
    /// <returns>The string without the removed last character.</returns>
    public static string RemoveLast(this string s) => s.RemoveLast(1);
    /// <summary>Removes a number of characters from the end of the string.</summary>
    /// <param name="s">The string whose last characters to remove.</param>
    /// <param name="characters">The number of characters to remove.</param>
    /// <returns>The string without the removed last characters.</returns>
    public static string RemoveLast(this string s, int characters) => s.Remove(s.Length - characters);
    /// <summary>Returns a string that removes repetitions of the same character. Example: <code>RemoveCharacterRepetitions("aabcc") = "abc"</code></summary>
    /// <param name="s">The string to remove the character repetitions from.</param>
    public static string RemoveCharacterRepetitions(this string s)
    {
        if (s.Length is 0)
            return "";
        char[] chars = new char[s.Length];
        int nextIndex = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (chars[nextIndex] != s[i])
            {
                nextIndex++;
                chars[nextIndex] = s[i];
            }
        }

        return new string(chars);
    }
    /// <summary>Returns a substring that begins with the beginning matched string and ends before the ending matched string.</summary>
    /// <param name="s">The string whose substring to return.</param>
    /// <param name="from">The beginning matching string to begin the substring from.</param>
    /// <param name="to">The ending matching string to end the substring at.</param>
    public static string Substring(this string s, string from, string to)
    {
        int startIndex = s.IndexOfAfter(from);
        int endIndex = s.IndexOf(to);
        int length = endIndex - startIndex;
        return s.Substring(startIndex, length);
    }
    /// <summary>Replaces a part of the string with a new one.</summary>
    /// <param name="originalString">The original string.</param>
    /// <param name="replacedString">The new string to replace to the part of the original one.</param>
    /// <param name="startIndex">The starting index of the substring to replace.</param>
    /// <param name="length">The length of the substring to replace.</param>
    public static string Replace(this string originalString, string replacedString, int startIndex, int length)
    {
        return new StringBuilder(originalString).Replace(replacedString, startIndex, length).ToString();
    }
    /// <summary>Replaces a whole word of the original string and returns the new one.</summary>
    /// <param name="originalString">The original string which will be replaced.</param>
    /// <param name="oldString">The old part of the string in the original string to replace.</param>
    /// <param name="newString">The new part of the string which will be contained in the returned string.</param>
    public static string ReplaceWholeWord(this string originalString, string oldString, string newString)
    {
        if (oldString == newString)
            return originalString;

        var builder = new StringBuilder(originalString);

        int previousStart = 0;
        while (true)
        {
            int index = originalString.IndexOf(oldString, previousStart);
            if (index == -1)
                return builder.ToString();

            bool isWord = true;
            if (originalString[index - 1].IsLetterOrDigit())
            {
                isWord = false;
            }
            else
            {
                if (index + oldString.Length < originalString.Length)
                {
                    if (originalString[index + oldString.Length].IsLetterOrDigit())
                    {
                        isWord = false;
                    }
                }
            }

            if (!isWord)
            {
                previousStart = index;
                continue;
            }

            builder.Replace(newString, index, oldString.Length);
        }
    }
    /// <summary>Normalizes the line endings of a string to \n.</summary>
    /// <param name="s">The string whose line endings to normalize.</param>
    /// <returns>The string with normalized line endings.</returns>
    public static string NormalizeLineEndings(this string s) => s.Replace("\r\n", "\n").Replace('\r', '\n');

    /// <summary>Repeats a string a number of times.</summary>
    /// <param name="s">The string to repeat the specified number of times.</param>
    /// <param name="count">The number of times to repeat the string. The value cannot be negative.</param>
    /// <returns>The given string repeated the specified number of times. If <paramref name="count"/> is 0, <seealso cref="string.Empty"/> is returned.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="count"/> is negative.</exception>
    public static string Repeat(this string s, int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), "The count of repetitions cannot be a negative number.");

        if (count is 0)
            return string.Empty;

        if (count is 1)
            return s;

        var builder = new StringBuilder(s.Length * count);
        for (int i = 0; i < count; i++)
            builder.Append(s);
        return builder.ToString();
    }
    #endregion

    #region Checks
    /// <summary>Determines whether the string is a valid hex string.</summary>
    /// <param name="hex">The string.</param>
    /// <returns><see langword="true"/> if the string is a valid hex string; that is, it only consists of valid hex characters, otherwise <see langword="false"/>.</returns>
    public static bool IsValidHexString(this string hex)
    {
        return !hex.Any(c => !c.IsValidHexCharacter());
    }
    #endregion

    #region Parsing
    /// <summary>Parses the <seealso cref="byte"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to parse.</param>
    /// <returns>The <seealso cref="byte"/> value of the <seealso cref="string"/>.</returns>
    public static byte ParseByte(this string s) => byte.Parse(s);
    /// <summary>Parses the <seealso cref="sbyte"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to parse.</param>
    /// <returns>The <seealso cref="sbyte"/> value of the <seealso cref="string"/>.</returns>
    public static sbyte ParseSByte(this string s) => sbyte.Parse(s);
    /// <summary>Parses the <seealso cref="short"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to parse.</param>
    /// <returns>The <seealso cref="short"/> value of the <seealso cref="string"/>.</returns>
    public static short ParseInt16(this string s) => short.Parse(s);
    /// <summary>Parses the <seealso cref="ushort"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to parse.</param>
    /// <returns>The <seealso cref="ushort"/> value of the <seealso cref="string"/>.</returns>
    public static ushort ParseUInt16(this string s) => ushort.Parse(s);
    /// <summary>Parses the <seealso cref="int"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to parse.</param>
    /// <returns>The <seealso cref="int"/> value of the <seealso cref="string"/>.</returns>
    public static int ParseInt32(this string s) => int.Parse(s);
    /// <summary>Parses the <seealso cref="uint"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to parse.</param>
    /// <returns>The <seealso cref="uint"/> value of the <seealso cref="string"/>.</returns>
    public static uint ParseUInt32(this string s) => uint.Parse(s);
    /// <summary>Parses the <seealso cref="long"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to parse.</param>
    /// <returns>The <seealso cref="long"/> value of the <seealso cref="string"/>.</returns>
    public static long ParseInt64(this string s) => long.Parse(s);
    /// <summary>Parses the <seealso cref="ulong"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to parse.</param>
    /// <returns>The <seealso cref="ulong"/> value of the <seealso cref="string"/>.</returns>
    public static ulong ParseUInt64(this string s) => ulong.Parse(s);
    /// <summary>Parses the <seealso cref="float"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to parse.</param>
    /// <returns>The <seealso cref="float"/> value of the <seealso cref="string"/>.</returns>
    public static float ParseSingle(this string s) => float.Parse(s);
    /// <summary>Parses the <seealso cref="double"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to parse.</param>
    /// <returns>The <seealso cref="double"/> value of the <seealso cref="string"/>.</returns>
    public static double ParseDouble(this string s) => double.Parse(s);
    /// <summary>Parses the <seealso cref="decimal"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to parse.</param>
    /// <returns>The <seealso cref="decimal"/> value of the <seealso cref="string"/>.</returns>
    public static decimal ParseDecimal(this string s) => decimal.Parse(s);

    /// <summary>Tries to parse the <seealso cref="byte"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to try to parse.</param>
    /// <param name="value">The <seealso cref="byte"/> value of the <seealso cref="string"/>, if the parsing was successful, otherwise 0.</param>
    /// <returns><see langword="true"/> if the parsing was successful, otherwise <see langword="false"/>.</returns>
    public static bool TryParseByte(this string s, out byte value) => byte.TryParse(s, out value);
    /// <summary>Tries to parse the <seealso cref="sbyte"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to try to parse.</param>
    /// <param name="value">The <seealso cref="sbyte"/> value of the <seealso cref="string"/>, if the parsing was successful, otherwise 0.</param>
    /// <returns><see langword="true"/> if the parsing was successful, otherwise <see langword="false"/>.</returns>
    public static bool TryParseSByte(this string s, out sbyte value) => sbyte.TryParse(s, out value);
    /// <summary>Tries to parse the <seealso cref="short"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to try to parse.</param>
    /// <param name="value">The <seealso cref="short"/> value of the <seealso cref="string"/>, if the parsing was successful, otherwise 0.</param>
    /// <returns><see langword="true"/> if the parsing was successful, otherwise <see langword="false"/>.</returns>
    public static bool TryParseInt16(this string s, out short value) => short.TryParse(s, out value);
    /// <summary>Tries to parse the <seealso cref="ushort"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to try to parse.</param>
    /// <param name="value">The <seealso cref="ushort"/> value of the <seealso cref="string"/>, if the parsing was successful, otherwise 0.</param>
    /// <returns><see langword="true"/> if the parsing was successful, otherwise <see langword="false"/>.</returns>
    public static bool TryParseUInt16(this string s, out ushort value) => ushort.TryParse(s, out value);
    /// <summary>Tries to parse the <seealso cref="int"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to try to parse.</param>
    /// <param name="value">The <seealso cref="int"/> value of the <seealso cref="string"/>, if the parsing was successful, otherwise 0.</param>
    /// <returns><see langword="true"/> if the parsing was successful, otherwise <see langword="false"/>.</returns>
    public static bool TryParseInt32(this string s, out int value) => int.TryParse(s, out value);
    /// <summary>Tries to parse the <seealso cref="uint"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to try to parse.</param>
    /// <param name="value">The <seealso cref="uint"/> value of the <seealso cref="string"/>, if the parsing was successful, otherwise 0.</param>
    /// <returns><see langword="true"/> if the parsing was successful, otherwise <see langword="false"/>.</returns>
    public static bool TryParseUInt32(this string s, out uint value) => uint.TryParse(s, out value);
    /// <summary>Tries to parse the <seealso cref="long"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to try to parse.</param>
    /// <param name="value">The <seealso cref="long"/> value of the <seealso cref="string"/>, if the parsing was successful, otherwise 0.</param>
    /// <returns><see langword="true"/> if the parsing was successful, otherwise <see langword="false"/>.</returns>
    public static bool TryParseInt64(this string s, out long value) => long.TryParse(s, out value);
    /// <summary>Tries to parse the <seealso cref="ulong"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to try to parse.</param>
    /// <param name="value">The <seealso cref="ulong"/> value of the <seealso cref="string"/>, if the parsing was successful, otherwise 0.</param>
    /// <returns><see langword="true"/> if the parsing was successful, otherwise <see langword="false"/>.</returns>
    public static bool TryParseUInt64(this string s, out ulong value) => ulong.TryParse(s, out value);
    /// <summary>Tries to parse the <seealso cref="float"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to try to parse.</param>
    /// <param name="value">The <seealso cref="float"/> value of the <seealso cref="string"/>, if the parsing was successful, otherwise 0.</param>
    /// <returns><see langword="true"/> if the parsing was successful, otherwise <see langword="false"/>.</returns>
    public static bool TryParseSingle(this string s, out float value) => float.TryParse(s, out value);
    /// <summary>Tries to parse the <seealso cref="double"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to try to parse.</param>
    /// <param name="value">The <seealso cref="double"/> value of the <seealso cref="string"/>, if the parsing was successful, otherwise 0.</param>
    /// <returns><see langword="true"/> if the parsing was successful, otherwise <see langword="false"/>.</returns>
    public static bool TryParseDouble(this string s, out double value) => double.TryParse(s, out value);
    /// <summary>Tries to parse the <seealso cref="decimal"/> value of the specified <seealso cref="string"/>.</summary>
    /// <param name="s">The <seealso cref="string"/> to try to parse.</param>
    /// <param name="value">The <seealso cref="decimal"/> value of the <seealso cref="string"/>, if the parsing was successful, otherwise 0.</param>
    /// <returns><see langword="true"/> if the parsing was successful, otherwise <see langword="false"/>.</returns>
    public static bool TryParseDecimal(this string s, out decimal value) => decimal.TryParse(s, out value);
    #endregion

    #region Substring Until
    public static string SubstringUntilFirst(this string s, char match)
    {
        int index = s.IndexOf(match);
        if (index < 0)
            return s;

        return s.Substring(0, index);
    }
    public static string SubstringUntilFirst(this string s, string match)
    {
        int index = s.IndexOf(match);
        if (index < 0)
            return s;

        return s.Substring(0, index);
    }
    public static string SubstringUntilLast(this string s, char match)
    {
        int index = s.LastIndexOf(match);
        if (index < 0)
            return s;

        return s.Substring(0, index);
    }
    public static string SubstringUntilLast(this string s, string match)
    {
        int index = s.LastIndexOf(match);
        if (index < 0)
            return s;

        return s.Substring(0, index);
    }
    #endregion
    #endregion

    #region IEnumerable<string>
    /// <summary>Combines the strings of a string collection with a separator and returns the new string.</summary>
    /// <param name="strings">The collection of strings.</param>
    /// <param name="separator">The separator of the strings.</param>
    public static string Combine(this IEnumerable<string> strings, char separator)
    {
#if HAS_STRING_JOIN_CHAR
        return string.Join(separator, strings);
#else
        return string.Join(separator.ToString(), strings);
#endif
    }
    /// <summary>Combines the strings of a string collection with a separator and returns the new string.</summary>
    /// <param name="strings">The collection of strings.</param>
    /// <param name="separator">The separator of the strings.</param>
    public static string Combine(this IEnumerable<string> strings, string separator)
    {
        return string.Join(separator, strings);
    }
    /// <summary>Combines the strings of a string collection and returns the new string.</summary>
    /// <param name="strings">The collection of strings.</param>
    public static string Combine(this IEnumerable<string> strings)
    {
        return string.Concat(strings);
    }

    /// <summary>Gets all the non-null and non-empty strings from a collection.</summary>
    /// <param name="strings">The collection of strings.</param>
    public static IEnumerable<string> NonEmpty(this IEnumerable<string?> strings)
    {
        return strings.Where(string.IsNullOrEmpty);
    }
    #endregion

    #region Old
#if NETSTANDARD2_0
    public static bool StartsWith(this string s, char c)
    {
        return s.Length > 0
            && s[0] == c;
    }
    public static bool EndsWith(this string s, char c)
    {
        return s.Length > 0
            && s[s.Length - 1] == c;
    }
#endif
    #endregion
}
