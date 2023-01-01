using Garyon.DataStructures;
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
    /// <param name="occurrence">The single-based (starting from 1) index of the occurrence to find.</param>
    public static int Find(this string s, string match, int occurrence)
    {
        if (occurrence <= 0)
            throw new ArgumentException("The occurrence cannot be non-positive.");
        int occurrences = 0;
        for (int i = 0; i <= s.Length - match.Length; i++)
        {
            bool found = true;
            for (int j = 0; j < match.Length && found; j++)
                if (s[i + j] != match[j])
                    found = false;
            if (found)
            {
                occurrences++;
                if (occurrences == occurrence)
                    return i;
            }
        }
        return -1;
    }
    /// <summary>Finds a substring within the string from the end. Returns the index of the first character where the match occurred, otherwise -1.</summary>
    /// <param name="s">The string within which the search will be performed.</param>
    /// <param name="match">The substring to match from the original string.</param>
    /// <param name="occurrence">The single-based (starting from 1) index of the occurrence to find.</param>
    public static int FindFromEnd(this string s, string match, int occurrence)
    {
        int occurrences = 0;
        for (int i = s.Length - match.Length; i >= 0; i--)
        {
            bool found = true;
            for (int j = 0; j < match.Length && found; j++)
                if (s[i + j] != match[j])
                    found = false;
            if (found)
            {
                occurrences++;
                if (occurrences == occurrence)
                    return i;
            }
        }
        return -1;
    }
    /// <summary>Finds a substring within the string from the end. Returns the index of the first character where the first match occurred, otherwise -1.</summary>
    /// <param name="s">The string within which the search will be performed.</param>
    /// <param name="match">The substring to match from the original string.</param>
    /// <param name="start">The starting index to perform the search.</param>
    /// <param name="end">The ending index to perform the search.</param>
    public static int FindFromEnd(this string s, string match, int start, int end)
    {
        return s.LastIndexOf(match, start, end - start);
    }
    /// <summary>Finds a substring within the string. Returns the indexes of the first character where all the matches occurred, otherwise -1.</summary>
    /// <param name="s">The string within which the search will be performed.</param>
    /// <param name="match">The substring to match from the original string.</param>
    public static int[] FindAll(this string s, string match)
    {
        var indices = new List<int>();
        for (int i = 0; i <= s.Length - match.Length; i++)
        {
            string sub = s.Substring(i, match.Length);
            if (sub == match)
                indices.Add(i);
        }
        return indices.ToArray();
    }
    /// <summary>Finds a substring within the string. Returns the indexes of the first character where all the matches occurred, otherwise -1.</summary>
    /// <param name="s">The string within which the search will be performed.</param>
    /// <param name="match">The substring to match from the original string.</param>
    /// <param name="start">The starting index to perform the search.</param>
    /// <param name="end">The ending index to perform the search.</param>
    public static int[] FindAll(this string s, string match, int start, int end)
    {
        var indices = new List<int>();
        for (int i = start; i <= end - match.Length; i++)
        {
            string sub = s.Substring(i, match.Length);
            if (sub == match)
                indices.Add(i);
        }
        return indices.ToArray();
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

    #region Number-Related
    /// <summary>Converts the number found in the end of the string into an <seealso cref="int"/>.</summary>
    /// <param name="s">The string on which the last decimal number to detect.</param>
    /// <returns>The last detected decimal number.</returns>
    public static int GetLastNumber(this string s)
    {
        int i = s.Length;
        while (i > 0 && s[i - 1].IsDigit())
            i--;
        if (i < s.Length)
            return int.Parse(s.Substring(i));
        throw new ArgumentException("The string has no number in the end.");
    }
    /// <summary>Removes the number found in the end of the string.</summary>
    /// <param name="s">The string whose number in the end to remove.</param>
    public static string RemoveLastNumber(this string s)
    {
        int i = s.Length;
        while (i > 0 && s[i - 1].IsDigit())
            i--;
        return s.Substring(0, i);
    }
    /// <summary>Removes the number found in the end of the string.</summary>
    /// <param name="s">The string whose number in the end to remove.</param>
    /// <param name="removedNumber">The number that was removed from the string.</param>
    public static string RemoveLastNumber(this string s, out int removedNumber)
    {
        int i = s.Length;
        while (i > 0 && s[i - 1].IsDigit())
            i--;

        if (i < s.Length)
            removedNumber = int.Parse(s.Substring(i));
        else
            removedNumber = 0;

        return s.Substring(0, i);
    }
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
    /// <summary>Returns a string that removes repetitions of the same character. Example: <code>RemoveCharacterRepetitions("aabcc") = "abc"</code>.</summary>
    /// <param name="s">The string to remove the character repetitions from.</param>
    public static string RemoveCharacterRepetitions(this string s)
    {
        char[] chars = new char[s.Length];
        if (s.Length > 0) // Just to avoid bad cases
            chars[0] = s[0];
        int x = 0;
        for (int i = 1; i < s.Length; i++)
            if (chars[x] != s[i])
                chars[++x] = s[i];
        return new string(chars);
    }
    /// <summary>Returns a dictionary containing the number of instances per character in the provided string.</summary>
    /// <param name="s">The string to get the character occurences of.</param>
    public static IDictionary<char, int> GetCharacterOccurences(this string s)
    {
        return new ValueCounterDictionary<char>(s);
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
    /// <summary>Checks whether at least one of the provided characters are contained in the string.</summary>
    /// <param name="s">The string to check whether it contains the specified characters.</param>
    /// <param name="c">The characters to check whether they are contained in the string.</param>
    public static bool Contains(this string s, params char[] c)
    {
        for (int i = 0; i < s.Length; i++)
            for (int j = 0; j < c.Length; j++)
                if (s[i] == c[j])
                    return true;
        return false;
    }
    /// <summary>Checks whether the string contains the provided substring as a whole word.</summary>
    /// <param name="s">The string to check whether it contains the specified substring as a whole word.</param>
    /// <param name="match">The substring to check whether it is contained in the original string as a whole word.</param>
    public static bool ContainsWholeWord(this string s, string match)
    {
        for (int i = s.Length - match.Length; i >= 0; i--)
            if (s.Substring(i, match.Length) == match)
                if ((i == 0 || (!s[i - 1].IsLetterOrDigit())) && (i >= s.Length - match.Length || (!s[i + match.Length].IsLetterOrDigit())))
                    return true;
        return false;
    }
    /// <summary>Checks whether this string matches another string regardless of its character casing.</summary>
    /// <param name="s">The original string.</param>
    /// <param name="match">The string to match.</param>
    public static bool MatchesStringCaseFree(this string s, string match)
    {
        if (s.Length != match.Length)
            return false;
        return s.ToLower() == match.ToLower();
    }
    /// <summary>Gets the occurrences of a specific character within the given string.</summary>
    /// <param name="s">The string.</param>
    /// <param name="match">The matching character to find in the string.</param>
    /// <returns>The number of occurrences of the character in the string.</returns>
    public static int GetCharacterOccurences(this string s, char match)
    {
        return s.Count(c => c == match);
    }
    /// <summary>Determines whether the string is a valid hex string.</summary>
    /// <param name="hex">The string.</param>
    /// <returns><see langword="true"/> if the string is a valid hex string; that is, it only consists of valid hex characters, otherwise <see langword="false"/>.</returns>
    public static bool IsValidHexString(this string hex)
    {
        return !hex.Any(c => !c.IsValidHexCharacter());
    }
    #endregion

    #region Casing
    /// <summary>Returns the words of a string in PascalCase in a single string.</summary>
    /// <param name="s">The string in PascalCase whose words to get.</param>
    /// <param name="separateDigitSequences">Determines whether numerical digit sequences will be treated as a new word.</param>
    public static string GetPascalCaseWordsString(this string s, bool separateDigitSequences = true) => s.GetPascalCaseWords(separateDigitSequences).CombineWords();
    /// <summary>Returns the words of a string in PascalCase.</summary>
    /// <param name="s">The string in PascalCase whose words to get.</param>
    /// <param name="separateDigitSequences">Determines whether numerical digit sequences will be treated as a new word.</param>
    public static string[] GetPascalCaseWords(this string s, bool separateDigitSequences = true)
    {
        var indices = new List<int>(s.Length / 5) { 0 }; // estimated word count

        bool wasLastCharacterUpper = true;
        bool wasLastCharacterDigit = false;
        bool isCurrentCharacterUpper;
        bool isCurrentCharacterDigit;
        int continuousUpperCases = 0;
        for (int i = 0; i < s.Length; i++)
        {
            isCurrentCharacterUpper = char.IsUpper(s[i]);
            isCurrentCharacterDigit = separateDigitSequences && char.IsDigit(s[i]);

            if ((!wasLastCharacterUpper && isCurrentCharacterUpper) || (!wasLastCharacterDigit && isCurrentCharacterDigit))
                indices.Add(i);
            else if (continuousUpperCases > 1 && !isCurrentCharacterUpper)
                indices.Add(i - 1);

            if (isCurrentCharacterUpper)
                continuousUpperCases++;
            else
                continuousUpperCases = 0;

            wasLastCharacterUpper = isCurrentCharacterUpper;
            wasLastCharacterDigit = isCurrentCharacterDigit;
        }

        indices.Add(s.Length);

        string[] words = new string[indices.Count - 1];
        for (int i = 0; i < words.Length; i++)
        {
            int nextIndex = indices[i + 1];
            int currentIndex = indices[i];
            int length = nextIndex - currentIndex;
            words[i] = s.Substring(currentIndex, length);
        }
        return words;
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

    /// <summary>Returns the lines of a string.</summary>
    /// <param name="s">The string whose lines to get.</param>
    /// <param name="normalizeLineEndings">Determines whether the line endings of the original string will be normalized before the splitting occurs. Helpful to improve performance on an already normalized string to avoid unnecessary line ending replacements from the normalization.</param>
    public static string[] GetLines(this string s, bool normalizeLineEndings = true) => (normalizeLineEndings ? s.NormalizeLineEndings() : s).Split('\n');
    #endregion

    #region String[]
    /// <summary>Splits an array of strings and returns a new two-dimensiional array containing the split strings. With indexing [i, j], i is the index of the string in the original array and j is the index of the split string.</summary>
    /// <param name="s">The array of strings.</param>
    /// <param name="separator">The separator of the strings.</param>
    public static string[,] Split(this string[] s, char separator) => s.SplitAsList(separator).ToTwoDimensionalArray();
    /// <summary>Splits an array of strings and returns a new two-dimensiional jagged array containing the split strings. With indexing [i][j], i is the index of the string in the original array and j is the index of the split string.</summary>
    /// <param name="s">The array of strings.</param>
    /// <param name="separator">The separator of the strings.</param>
    public static string[][] SplitAsJagged(this string[] s, char separator) => s.SplitAsList(separator).ToArray();
    /// <summary>Splits an array of strings and returns a new list of string arrays containing the split strings. With indexing [i][j], i is the index of the string in the original array and j is the index of the split string.</summary>
    /// <param name="s">The array of strings.</param>
    /// <param name="separator">The separator of the strings.</param>
    public static List<string[]> SplitAsList(this string[] s, char separator)
    {
        var separated = new List<string[]>();
        for (int i = 0; i < s.Length; i++)
            separated.Add(s[i].Split(separator));
        return separated;
    }
    #endregion

    #region IEnumerable<string>
    /// <summary>Finds the occurrences of a string in a string array and returns an array containing the indices of each occurrence in the original array.</summary>
    /// <param name="strings">The array containing the strings which will be evaluated.</param>
    /// <param name="match">The string to match.</param>
    public static int[] FindOccurrences(this IEnumerable<string> strings, string match)
    {
        if (strings == null)
            return Array.Empty<int>();

        var occurrences = new List<int>();
        
        int i = 0;
        foreach (var s in strings)
        {
            if (s == match)
                occurrences.Add(i);
            i++;
        }

        return occurrences.ToArray();
    }
    /// <summary>Replaces the characters of an array of strings and returns the new array.</summary>
    /// <param name="strings">The strings which will be replaced.</param>
    /// <param name="oldChar">The old character.</param>
    /// <param name="newChar">The new character.</param>
    public static string[] Replace(this IEnumerable<string> strings, char oldChar, char newChar)
    {
        var result = new string[strings.Count()];
        int i = 0;
        foreach (var s in strings)
        {
            result[i] = s.Replace(oldChar, newChar);
            i++;
        }
        return result;
    }
    /// <summary>Replaces the strings of an array of strings and returns the new array.</summary>
    /// <param name="strings">The strings which will be replaced.</param>
    /// <param name="oldString">The old string.</param>
    /// <param name="newString">The new string.</param>
    public static string[] Replace(this IEnumerable<string> strings, string oldString, string newString)
    {
        var result = new string[strings.Count()];
        int i = 0;
        foreach (var s in strings)
        {
            result[i] = s.Replace(oldString, newString);
            i++;
        }
        return result;
    }
    /// <summary>Replaces whole words of the strings of an array of strings and returns the new array.</summary>
    /// <param name="strings">The strings which will be replaced.</param>
    /// <param name="oldString">The old string.</param>
    /// <param name="newString">The new string.</param>
    public static string[] ReplaceWholeWord(this IEnumerable<string> strings, string oldString, string newString)
    {
        var result = new string[strings.Count()];
        int i = 0;
        foreach (var s in strings)
        {
            result[i] = s.ReplaceWholeWord(oldString, newString);
            i++;
        }
        return result;
    }
    /// <summary>Determines whether there is at least one occurrence of a string in a string of the string array.</summary>
    /// <param name="strings">The collection of strings.</param>
    /// <param name="match">The string to match.</param>
    public static bool ContainsSubstringAtLeastOnce(this IEnumerable<string> strings, string match)
    {
        if (strings == null)
            return false;
        foreach (var s in strings)
            if (s.Contains(match))
                return true;
        return false;
    }
    /// <summary>Determines whether there is at least one occurrence of a string as a whole word in a string of the string array.</summary>
    /// <param name="strings">The collection of strings.</param>
    /// <param name="match">The string to match.</param>
    public static bool ContainsWholeWordSubstringAtLeastOnce(this IEnumerable<string> strings, string match)
    {
        if (strings == null)
            return false;
        foreach (var s in strings)
            if (s.ContainsWholeWord(match))
                return true;
        return false;
    }
    /// <summary>Removes the empty elements of a string list and returns the new list.</summary>
    /// <param name="strings">The collection of strings.</param>
    public static IEnumerable<string> RemoveEmptyElements(this IEnumerable<string> strings)
    {
        var result = new List<string>();
        foreach (var s in strings)
            if (s.Any())
                result.Add(s);
        return result;
    }
    /// <summary>Combines the strings of a string array and returns the new string.</summary>
    /// <param name="strings">The collection of strings.</param>
    public static string CombineWords(this IEnumerable<string> strings) => strings.Combine(" ");
    /// <summary>Combines the strings of a string collection with a separator and returns the new string.</summary>
    /// <param name="strings">The collection of strings.</param>
    /// <param name="separator">The separator of the strings.</param>
    public static string Combine(this IEnumerable<string> strings, char separator)
    {
        if (!strings?.Any() ?? true)
            return "";

        int capacity = 0;
        foreach (var s in strings)
            capacity += s.Length + 1;

        var result = new StringBuilder(capacity);
        foreach (var s in strings)
            result.Append(s).Append(separator);
        return result.Remove(result.Length - 1, 1).ToString();
    }
    /// <summary>Combines the strings of a string collection with a separator and returns the new string.</summary>
    /// <param name="strings">The collection of strings.</param>
    /// <param name="separator">The separator of the strings.</param>
    public static string Combine(this IEnumerable<string> strings, string separator)
    {
        if (!strings?.Any() ?? true)
            return "";

        int capacity = 0;
        foreach (var s in strings)
            capacity += s.Length + separator.Length;

        var result = new StringBuilder(capacity);
        foreach (var s in strings)
            result.Append(s).Append(separator);
        return result.Remove(result.Length - separator.Length, separator.Length).ToString();
    }
    /// <summary>Combines the strings of a string collection and returns the new string.</summary>
    /// <param name="strings">The collection of strings.</param>
    public static string Combine(this IEnumerable<string> strings)
    {
        if (!strings?.Any() ?? true)
            return "";

        var result = new StringBuilder();
        foreach (var s in strings)
            result.Append(s);
        return result.ToString();
    }
    /// <summary>Combines the strings of a string collection with a separator and returns the new string.</summary>
    /// <param name="strings">The collection of strings.</param>
    /// <param name="predicate">The predicate which determines which strings will be combined.</param>
    /// <param name="separator">The separator of the strings.</param>
    public static string Combine(this IEnumerable<string> strings, Func<string, bool> predicate, string separator)
    {
        return strings.Where(predicate).Combine(separator);
    }
    /// <summary>Aggregates the provided items with the provided aggregator function if the items' count is greater than 0, otherwise returns an empty string.</summary>
    /// <param name="strings">The strings to aggregate.</param>
    /// <param name="aggregator">The aggregator function to use when aggregating the strings.</param>
    public static string AggregateIfContains(this IEnumerable<string> strings, Func<string, string, string> aggregator) => strings.Any() ? strings.Aggregate(aggregator) : "";
    #endregion

    #region Aggregators
    // TODO: Move that into Aggregators
    /// <summary>Aggregates two strings and splits them by a space.</summary>
    /// <param name="a">The first string to aggregate.</param>
    /// <param name="b">The second string to aggregate.</param>
    /// <returns>The aggregated string.</returns>
    public static string WordAggregator(string a, string b) => $"{a} {b}";
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
