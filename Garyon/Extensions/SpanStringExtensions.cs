using Garyon.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;

using SpanString = System.ReadOnlySpan<char>;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for the <see cref="SpanString"/> type.
/// </summary>
public static class SpanStringExtensions
{
    #region Fluent
    public static int ParseInt32(this SpanString spanString)
    {
        return int.Parse(spanString);
    }
    public static long ParseInt64(this SpanString spanString)
    {
        return long.Parse(spanString);
    }
    public static uint ParseUInt32(this SpanString spanString)
    {
        return uint.Parse(spanString);
    }
    public static ulong ParseUInt64(this SpanString spanString)
    {
        return ulong.Parse(spanString);
    }
    public static float ParseSingle(this SpanString spanString)
    {
        return float.Parse(spanString);
    }
    public static double ParseDouble(this SpanString spanString)
    {
        return double.Parse(spanString);
    }
    public static decimal ParseDecimal(this SpanString spanString)
    {
        return decimal.Parse(spanString);
    }

    public static bool TryParseInt32(this SpanString spanString, out int value)
    {
        return int.TryParse(spanString, out value);
    }
    public static bool TryParseInt64(this SpanString spanString, out long value)
    {
        return long.TryParse(spanString, out value);
    }
    public static bool TryParseUInt32(this SpanString spanString, out uint value)
    {
        return uint.TryParse(spanString, out value);
    }
    public static bool TryParseUInt64(this SpanString spanString, out ulong value)
    {
        return ulong.TryParse(spanString, out value);
    }
    public static bool TryParseSingle(this SpanString spanString, out float value)
    {
        return float.TryParse(spanString, out value);
    }
    public static bool TryParseDouble(this SpanString spanString, out double value)
    {
        return double.TryParse(spanString, out value);
    }
    public static bool TryParseDecimal(this SpanString spanString, out decimal value)
    {
        return decimal.TryParse(spanString, out value);
    }

    public static bool TryParseInt32(this SpanString spanString, NumberStyles numberStyles, IFormatProvider? formatProvider, out int value)
    {
        return int.TryParse(spanString, numberStyles, formatProvider, out value);
    }
    public static bool TryParseInt64(this SpanString spanString, NumberStyles numberStyles, IFormatProvider? formatProvider, out long value)
    {
        return long.TryParse(spanString, numberStyles, formatProvider, out value);
    }
    public static bool TryParseUInt32(this SpanString spanString, NumberStyles numberStyles, IFormatProvider? formatProvider, out uint value)
    {
        return uint.TryParse(spanString, numberStyles, formatProvider, out value);
    }
    public static bool TryParseUInt64(this SpanString spanString, NumberStyles numberStyles, IFormatProvider? formatProvider, out ulong value)
    {
        return ulong.TryParse(spanString, numberStyles, formatProvider, out value);
    }
    public static bool TryParseSingle(this SpanString spanString, NumberStyles numberStyles, IFormatProvider? formatProvider, out float value)
    {
        return float.TryParse(spanString, numberStyles, formatProvider, out value);
    }
    public static bool TryParseDouble(this SpanString spanString, NumberStyles numberStyles, IFormatProvider? formatProvider, out double value)
    {
        return double.TryParse(spanString, numberStyles, formatProvider, out value);
    }
    public static bool TryParseDecimal(this SpanString spanString, NumberStyles numberStyles, IFormatProvider? formatProvider, out decimal value)
    {
        return decimal.TryParse(spanString, numberStyles, formatProvider, out value);
    }
    #endregion

    public static int ParseFirstInt32(this SpanString spanString, int startingIndex, out int endIndex)
    {
        if (spanString.TryParseFirstInt32(startingIndex, out int value, out endIndex))
            return value;

        ThrowHelper.Throw<ArgumentException>("The number could not be parsed from that index.");
        return -1;
    }
    public static bool TryParseFirstInt32(this SpanString spanString, int startingIndex, out int value, out int endIndex)
    {
        endIndex = startingIndex;
        if (spanString[endIndex] is '+' or '-')
            endIndex++;

        for (; endIndex < spanString.Length; endIndex++)
            if (!spanString[endIndex].IsDigit())
                break;

        return spanString[startingIndex..endIndex].TryParseInt32(out value);
    }

    public static int LastNumberStartIndex(this SpanString spanString)
    {
        int startIndex = spanString.Length - 1;

        if (!spanString[startIndex].IsDigit())
            return -1;

        while (startIndex > 0)
        {
            int next = startIndex - 1;
            if (!spanString[next].IsDigit())
                break;

            startIndex = next;
        }

        return startIndex;
    }
    public static SpanString LastNumberSlice(this SpanString spanString)
    {
        int startIndex = LastNumberStartIndex(spanString);
        return spanString[startIndex..];
    }
    public static int ParseLastInt32(this SpanString spanString)
    {
        return LastNumberSlice(spanString).ParseInt32();
    }
    public static long ParseLastInt64(this SpanString spanString)
    {
        return LastNumberSlice(spanString).ParseInt64();
    }
    public static uint ParseLastUInt32(this SpanString spanString)
    {
        return LastNumberSlice(spanString).ParseUInt32();
    }
    public static ulong ParseLastUInt64(this SpanString spanString)
    {
        return LastNumberSlice(spanString).ParseUInt64();
    }

    public static IReadOnlyList<string> SplitToStrings(this SpanString spanString, string delimiter)
    {
        return spanString.SplitSelect(delimiter, span => new string(span));
    }
    public static IReadOnlyList<string> SplitToStrings(this SpanString spanString, char delimiter)
    {
        return spanString.SplitSelect(delimiter, span => new string(span));
    }
}

/// <summary>
/// Defines a selector delegate that converts a
/// <seealso cref="SpanString"/> into a value.
/// </summary>
/// <typeparam name="T">The type of the converted value.</typeparam>
/// <param name="spanString">
/// The <seealso cref="SpanString"/> that is being converted.
/// </param>
/// <returns>The converted value.</returns>
public delegate T SpanStringSelector<T>(SpanString spanString);
