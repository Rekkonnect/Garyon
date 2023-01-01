#if HAS_SPAN
using System;
#endif
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garyon.Extensions;

/// <summary>Provides extension methods for the <seealso cref="StringBuilder"/> class.</summary>
public static class StringBuilderExtensions
{
    /// <summary>Appends an array of characters, and appends a new line after that.</summary>
    /// <param name="builder">The <seealso cref="StringBuilder"/> instance on which to append.</param>
    /// <param name="chars">The array of characters to append.</param>
    /// <returns>The same <seealso cref="StringBuilder"/> instance.</returns>
    public static StringBuilder AppendLine(this StringBuilder builder, char[] chars)
    {
        return builder.Append(chars).AppendLine();
    }
#if HAS_SPAN
    /// <summary>Appends a sequence of characters, and appends a new line after that.</summary>
    /// <param name="builder">The <seealso cref="StringBuilder"/> instance on which to append.</param>
    /// <param name="chars">The sequence of characters to append.</param>
    /// <returns>The same <seealso cref="StringBuilder"/> instance.</returns>
    public static StringBuilder AppendLine(this StringBuilder builder, ReadOnlySpan<char> chars)
    {
        return builder.Append(chars).AppendLine();
    }
    /// <inheritdoc cref="AppendLine(StringBuilder, ReadOnlySpan{char})"/>
    public static StringBuilder AppendLine(this StringBuilder builder, ReadOnlyMemory<char> chars)
    {
        return builder.Append(chars).AppendLine();
    }
#endif
    /// <summary>Appends a sequence of characters, and appends a new line after that.</summary>
    /// <param name="builder">The <seealso cref="StringBuilder"/> instance on which to append.</param>
    /// <param name="chars">The sequence of characters to append.</param>
    /// <returns>The same <seealso cref="StringBuilder"/> instance.</returns>
    /// <param name="charCount">The number of characters to append.</param>
    public static unsafe StringBuilder AppendLine(this StringBuilder builder, char* chars, int charCount)
    {
        return builder.Append(chars, charCount).AppendLine();
    }
    /// <summary>Appends an object's string representation, and appends a new line after that.</summary>
    /// <param name="builder">The <seealso cref="StringBuilder"/> instance on which to append.</param>
    /// <param name="o">The object whose string representation to append.</param>
    /// <returns>The same <seealso cref="StringBuilder"/> instance.</returns>
    public static StringBuilder AppendLine(this StringBuilder builder, object o)
    {
        return builder.AppendLine(o.ToString());
    }

    /// <summary>Appends the string representations of objects in a collection, and appends a new line after that.</summary>
    /// <param name="builder">The <seealso cref="StringBuilder"/> instance on which to append.</param>
    /// <param name="objects">The objects whose string representations to append.</param>
    /// <returns>The same <seealso cref="StringBuilder"/> instance.</returns>
    public static StringBuilder AppendLines(this StringBuilder builder, IEnumerable objects)
    {
        return AppendLines(builder, objects.Cast<object>());
    }
    /// <summary>Appends the string representations of objects in a collection, and appends a new line after that.</summary>
    /// <param name="builder">The <seealso cref="StringBuilder"/> instance on which to append.</param>
    /// <param name="objects">The objects whose string representations to append.</param>
    /// <returns>The same <seealso cref="StringBuilder"/> instance.</returns>
    public static StringBuilder AppendLines(this StringBuilder builder, IEnumerable<object> objects)
    {
        foreach (var o in objects)
            builder.AppendLine(o);
        return builder;
    }
    /// <summary>Appends the string representations of objects in a collection, and appends a new line after that.</summary>
    /// <param name="builder">The <seealso cref="StringBuilder"/> instance on which to append.</param>
    /// <param name="objects">The objects whose string representations to append.</param>
    /// <returns>The same <seealso cref="StringBuilder"/> instance.</returns>
    public static StringBuilder AppendLines(this StringBuilder builder, params object[] objects)
    {
        return AppendLines(builder, objects.AsEnumerable());
    }

    /// <summary>Copies this <seealso cref="StringBuilder"/> into a new instance.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> to copy.</param>
    /// <returns>The new copied <seealso cref="StringBuilder"/> instance.</returns>
    /// <remarks>
    /// In .NET Core 3.1 onwards, this uses the GetChunks method that
    /// enumerates the chunks contained in the builder, whereas for the
    /// earlier versions of the framework the builder is converted into
    /// the full string, and the new instance is initialized through that.
    /// </remarks>
    public static StringBuilder Copy(this StringBuilder s)
    {
#if HAS_GET_CHUNKS
        var result = new StringBuilder(s.Length);
        foreach (var c in s.GetChunks())
            result.Append(c);
        return result;
#else
        return new(s.ToString());
#endif
    }

    /// <summary>Removes a range of characters from the specified starting index to the end of the string.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> whose range of characters to remove.</param>
    /// <param name="startingIndex">The starting index from which to remove all characters.</param>
    public static StringBuilder Remove(this StringBuilder s, int startingIndex) => s.Remove(startingIndex, s.Length - startingIndex);
    /// <summary>Removes a number of characters from the end of the string.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> whose last characters to remove.</param>
    /// <param name="characterCount">The count of characters to remove from the end of the string.</param>
    public static StringBuilder RemoveLast(this StringBuilder s, int characterCount) => s.Remove(s.Length - characterCount, characterCount);
    /// <summary>Removes the last character of the string.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> whose last character to remove.</param>
    public static StringBuilder RemoveLast(this StringBuilder s) => s.RemoveLast(1);
    /// <summary>Removes the last character of the string if it has any characters, otherwise none.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> whose last character to remove, if any.</param>
    public static StringBuilder RemoveLastOrNone(this StringBuilder s) => s.RemoveLast(s.Length > 0 ? 1 : 0);
    /// <summary>Removes the last character of the string if it ends with the requested character, otherwise none.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> whose last character to remove, if the condition is met.</param>
    /// <param name="lastCharacter">The character that should be the last character of the string to remove the last character.</param>
    public static StringBuilder RemoveLastIfEndsWith(this StringBuilder s, char lastCharacter) => s.RemoveLast(s.EndsWith(lastCharacter) ? 1 : 0);
#if HAS_GET_CHUNKS
    /// <summary>Removes the last character of the string if it ends with the requested character, otherwise none.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> whose last character to remove, if the condition is met.</param>
    /// <param name="lastCharacters">The characters that should be the last characters of the string to remove them.</param>
    public static StringBuilder RemoveLastIfEndsWith(this StringBuilder s, string lastCharacters) => s.RemoveLast(s.EndsWith(lastCharacters) ? lastCharacters.Length : 0);
#endif

    /// <summary>Retrieves the last character of the string.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> whose last character to retrieve.</param>
    public static char Last(this StringBuilder s) => s[s.Length - 1];
    /// <summary>Retrieves the last character of the string if the string has any characters, otherwise returns <see langword="null"/> if the <seealso cref="StringBuilder"/> is <see langword="null"/> or if it has no characters.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> whose last character to retrieve.</param>
    public static char? LastOrNull(this StringBuilder s)
    {
        if (s is { Length: > 0 })
            return s[s.Length - 1];

        return null;
    }

    /// <summary>Determines whether the string ends with the specified character.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> to check whether it ends with the specified character.</param>
    /// <param name="lastCharacter">The character to check if it is the last character of the string.</param>
    public static bool EndsWith(this StringBuilder s, char lastCharacter) => s.Last() == lastCharacter;

#if HAS_GET_CHUNKS
    /// <summary>Determines whether the string ends with the specified substring.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> to check whether it ends with the specified substring.</param>
    /// <param name="lastCharacters">The characters to check if they are the last characters of the string.</param>
    public static bool EndsWith(this StringBuilder s, string lastCharacters) => s.SubstringLast(lastCharacters.Length) == lastCharacters;

    /// <summary>Returns a substring of this string with the specified number of characters from a starting index.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> to get the subtsring of.</param>
    /// <param name="startingIndex">The index of the original string that the new substring will start from.</param>
    /// <param name="length">The number of characters of the substring.</param>
    public static string Substring(this StringBuilder s, int startingIndex, int length) => s.SubstringBuilder(startingIndex, length).ToString();
    /// <summary>Returns a substring of this string with the specified number of characters from a starting index as a <seealso cref="StringBuilder"/>.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> to get the subtsring of.</param>
    /// <param name="startingIndex">The index of the original string that the new substring will start from.</param>
    /// <param name="length">The number of characters of the substring.</param>
    public static StringBuilder SubstringBuilder(this StringBuilder s, int startingIndex, int length)
    {
        var result = new StringBuilder(s.Length);
        int currentIndex = 0;
        int remaining = length;
        foreach (var c in s.GetChunks())
        {
            int chunkStart = startingIndex - currentIndex;

            if (chunkStart < 0)
                return result;

            if (chunkStart < c.Length)
            {
                int chunkEnd = chunkStart + remaining;
                if (chunkEnd > c.Length)
                    chunkEnd = c.Length;
                int chunkLength = chunkEnd - chunkStart;

                result.Append(c.Slice(chunkStart, chunkLength));
                remaining -= chunkLength;

                if (remaining <= 0)
                    return result;
            }

            currentIndex += c.Length;
        }
        return result;
    }
    /// <summary>Returns a substring of this string with the specified number of characters from its start.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> to get the subtsring of.</param>
    /// <param name="length">The number of characters of the substring.</param>
    public static string SubstringStart(this StringBuilder s, int length) => s.SubstringBuilder(0, length).ToString();
    /// <summary>Returns a substring of this string with the specified number of characters from its start as a <seealso cref="StringBuilder"/>.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> to get the subtsring of.</param>
    /// <param name="length">The number of characters of the substring.</param>
    public static StringBuilder SubstringStartBuilder(this StringBuilder s, int length) => s.SubstringBuilder(0, length);
    /// <summary>Returns a substring of this string with the specified number of characters from its end.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> to get the subtsring of.</param>
    /// <param name="length">The number of characters of the substring.</param>
    public static string SubstringLast(this StringBuilder s, int length) => s.SubstringBuilder(s.Length - length, length).ToString();
    /// <summary>Returns a substring of this string with the specified number of characters from its end as a <seealso cref="StringBuilder"/>.</summary>
    /// <param name="s">The <seealso cref="StringBuilder"/> to get the subtsring of.</param>
    /// <param name="length">The number of characters of the substring.</param>
    public static StringBuilder SubstringLastBuilder(this StringBuilder s, int length) => s.SubstringBuilder(s.Length - length, length);
#endif

    /// <summary>Removes the all leading whitespace characters from the current <see cref="StringBuilder"/>.</summary>
    /// <param name="s">The <see cref="StringBuilder"/> from which to remove all leading whitespace characters.</param>
    /// <returns>The original <see cref="StringBuilder"/> instance.</returns>
    public static StringBuilder TrimStart(this StringBuilder s)
    {
        int index = 0;
        int length = s.Length;
        while (index < length && s[index].IsWhiteSpace())
            index++;
        return s.Remove(0, index);
    }
    /// <summary>Removes the all leading whitespace characters from the current <see cref="StringBuilder"/>.</summary>
    /// <param name="s">The <see cref="StringBuilder"/> from which to remove all leading whitespace characters.</param>
    /// <returns>The original <see cref="StringBuilder"/> instance.</returns>
    public static StringBuilder TrimEnd(this StringBuilder s)
    {
        int toRemove = 0;
        int length = s.Length;
        while (toRemove < length)
        {
            var nextChar = s[length - (toRemove + 1)];
            bool hasWhitespace = nextChar.IsWhiteSpace();
            if (!hasWhitespace)
                break;

            toRemove++;
        }

        return s.RemoveLast(toRemove);
    }

    /// <summary>Appends a string to the <seealso cref="StringBuilder"/> instance, from its start index up until its end.</summary>
    /// <param name="stringBuilder">The <seealso cref="StringBuilder"/> instance on which to append the string.</param>
    /// <param name="value">The string to append to the <seealso cref="StringBuilder"/> from the specified index onwards.</param>
    /// <param name="startIndex">The index of the first character to append to the <seealso cref="StringBuilder"/>.</param>
    /// <returns>The <seealso cref="StringBuilder"/> instance that the string was appended to.</returns>
    public static StringBuilder Append(this StringBuilder stringBuilder, string value, int startIndex)
    {
        return stringBuilder.Append(value, startIndex, value.Length - startIndex);
    }
    /// <summary>Appends a string to the <seealso cref="StringBuilder"/> instance, from its start index up until its end.</summary>
    /// <param name="stringBuilder">The <seealso cref="StringBuilder"/> instance on which to append the string.</param>
    /// <param name="value">The string to append to the <seealso cref="StringBuilder"/> from the specified index onwards.</param>
    /// <param name="startIndex">The index of the first character to append to the <seealso cref="StringBuilder"/>.</param>
    /// <param name="length">The number of characters to include from the appended string.</param>
    /// <returns>The <seealso cref="StringBuilder"/> instance that the string was appended to.</returns>
    public static StringBuilder Append(this StringBuilder stringBuilder, string value, int startIndex, int length)
    {
        return stringBuilder.Append(value, startIndex, length);
    }

    /// <summary>Replaces a part of the string with a new one.</summary>
    /// <param name="builder">The original <seealso cref="StringBuilder"/>.</param>
    /// <param name="replacedString">The new string to replace to the part of the original one.</param>
    /// <param name="startIndex">The starting index of the substring to replace.</param>
    /// <param name="length">The length of the substring to replace.</param>
    public static StringBuilder Replace(this StringBuilder builder, string replacedString, int startIndex, int length)
    {
        return builder.Remove(startIndex, length).Insert(startIndex, replacedString);
    }
}
