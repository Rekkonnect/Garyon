using Garyon.Extensions;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class SpanStringExtensionsTests
{
    [Test]
    public async Task ParseAndTryParseHelpersTest()
    {
        int parsedInt;
        long parsedLong;
        uint parsedUInt;
        ulong parsedULong;
        float parsedFloat;
        double parsedDouble;
        decimal parsedDecimal;
        bool tryParseInt;
        bool tryParseDouble;
        int parsedStyled;
        bool tryParseStyled;
        string? nullString;
        string nonNullString;
        {
            parsedInt = "42".AsMemory().Span.ParseInt32();
            parsedLong = "43".AsMemory().Span.ParseInt64();
            parsedUInt = "44".AsMemory().Span.ParseUInt32();
            parsedULong = "45".AsMemory().Span.ParseUInt64();
            parsedFloat = "1.5".AsMemory().Span.ParseSingle();
            parsedDouble = "2.5".AsMemory().Span.ParseDouble();
            parsedDecimal = "3.5".AsMemory().Span.ParseDecimal();
            tryParseInt = "46".AsMemory().Span.TryParseInt32(out var value) && value == 46;
            tryParseDouble = "bad".AsMemory().Span.TryParseDouble(out _);
            tryParseStyled = "FF".AsMemory().Span.TryParseInt32(NumberStyles.HexNumber, CultureInfo.InvariantCulture, out parsedStyled);
            nullString = ReadOnlySpan<char>.Empty.ToStringOrNull();
            nonNullString = "abc".AsMemory().Span.ToStringOrNull()!;
        }

        await Assert.That(parsedInt).IsEqualTo(42);
        await Assert.That(parsedLong).IsEqualTo(43L);
        await Assert.That(parsedUInt).IsEqualTo(44U);
        await Assert.That(parsedULong).IsEqualTo(45UL);
        await Assert.That(parsedFloat).IsEqualTo(1.5f);
        await Assert.That(parsedDouble).IsEqualTo(2.5d);
        await Assert.That(parsedDecimal).IsEqualTo(3.5m);
        await Assert.That(tryParseInt).IsTrue();
        await Assert.That(tryParseDouble).IsFalse();
        await Assert.That(tryParseStyled).IsTrue();
        await Assert.That(parsedStyled).IsEqualTo(255);
        await Assert.That(nullString).IsNull();
        await Assert.That(nonNullString).IsEqualTo("abc");
    }

    [Test]
    public async Task NumberSliceAndSplitHelpersTest()
    {
        int first;
        int endIndex;
        bool tryParseFirst;
        int tryValue;
        int tryEndIndex;
        int lastStartIndex;
        int last;
        ulong lastUnsigned;
        string[] splitByChar;
        string[] splitBySpan;
        string[] selectedLines;
        {
            var source = "value=-42 and 255".AsMemory().Span;
            first = source.ParseFirstInt32(6, out endIndex);
            tryParseFirst = source.TryParseFirstInt32(6, out tryValue, out tryEndIndex);
            lastStartIndex = source.LastNumberStartIndex();
            last = source.ParseLastInt32();
            lastUnsigned = "id 123".AsMemory().Span.ParseLastUInt64();
            splitByChar = [.. "a,b,c".AsMemory().Span.SplitToStrings(',')];
            splitBySpan = [.. "one::two::three".AsMemory().Span.SplitToStrings("::".AsMemory().Span)];
            selectedLines = [.. "x\ny".AsMemory().Span.SelectLines(static line => line.ToString())];
        }

        await Assert.That(first).IsEqualTo(-42);
        await Assert.That(endIndex).IsEqualTo(9);
        await Assert.That(tryParseFirst).IsTrue();
        await Assert.That(tryValue).IsEqualTo(-42);
        await Assert.That(tryEndIndex).IsEqualTo(9);
        await Assert.That(lastStartIndex).IsEqualTo(14);
        await Assert.That(last).IsEqualTo(255);
        await Assert.That(lastUnsigned).IsEqualTo(123UL);
        await Assert.That(splitByChar.SequenceEqual(["a", "b", "c"])).IsTrue();
        await Assert.That(splitBySpan.SequenceEqual(["one", "two", "three"])).IsTrue();
        await Assert.That(selectedLines.SequenceEqual(["x", "y"])).IsTrue();
    }
}
