using Garyon.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class StringExtensionsTests
{
    [Test]
    public async Task IndexOfAfterTest()
    {
        const string source = "alpha-beta-gamma-beta";

        await Assert.That(source.IndexOfAfter("alpha")).IsEqualTo(5);
        await Assert.That(source.IndexOfAfter("beta", 6)).IsEqualTo(10);
    }

    [Test]
    public async Task FindOccurrenceTest()
    {
        const string source = "alpha-beta-gamma-beta";

        await Assert.That(source.FindOccurrence("beta", 2)).IsEqualTo(21);
        await Assert.That(source.FindOccurrenceFromEnd("beta", 2)).IsEqualTo(6);
        await Assert.That(source.FindOccurrence("beta", 3)).IsEqualTo(-1);

        bool threw = false;
        try
        {
            _ = source.FindOccurrence("beta", 0);
        }
        catch (ArgumentException)
        {
            threw = true;
        }
        await Assert.That(threw).IsTrue();
    }

    [Test]
    public async Task RemoveLastTest()
    {
        await Assert.That("abcd".RemoveLast()).IsEqualTo("abc");
        await Assert.That("abcdef".RemoveLast(2)).IsEqualTo("abcd");
    }

    [Test]
    public async Task ReplacementAndRepeatHelpersTest()
    {
        await Assert.That("aabcc".RemoveCharacterRepetitions().Trim('\0')).IsEqualTo("abc");
        await Assert.That("cat scatter cat".ReplaceWholeWord("cat", "dog")).IsEqualTo("dog scatter dog");
        await Assert.That("ab".Repeat(3)).IsEqualTo("ababab");
        Assert.Throws<ArgumentOutOfRangeException>(() => "ab".Repeat(-1));
    }

    [Test]
    public async Task SubstringBetweenStringsTest()
    {
        await Assert.That("prefix[value]suffix".Substring("[", "]")).IsEqualTo("value");
    }

    [Test]
    public async Task ReplaceSubstringByIndexTest()
    {
        await Assert.That("abcdef".Replace("XY", 2, 3)).IsEqualTo("abXYf");
    }

    [Test]
    public async Task NormalizeLineEndingsTest()
    {
        await Assert.That("\r\n\r\n\r".NormalizeLineEndings()).IsEqualTo("\n\n\n");
    }

    [Test]
    public async Task IsValidHexStringTest()
    {
        await Assert.That("0Fa9".IsValidHexString()).IsTrue();
        await Assert.That("0Fg9".IsValidHexString()).IsFalse();
    }

    [Test]
    public async Task ParseInt32Test()
    {
        await Assert.That("123".ParseInt32()).IsEqualTo(123);
    }

    [Test]
    public async Task TryParseInt32Test()
    {
        await Assert.That("456".TryParseInt32(out var parsed)).IsTrue();
        await Assert.That(parsed).IsEqualTo(456);
        await Assert.That("x".TryParseInt32(out _)).IsFalse();
    }

    [Test]
    public async Task ParseNullableInt32Test()
    {
        await Assert.That("789".ParseNullableInt32()).IsEqualTo(789);
        await Assert.That("x".ParseNullableInt32()).IsNull();
        await Assert.That("15".ParseNullableUInt32()).IsEqualTo(15U);
        await Assert.That("16".ParseNullableInt64()).IsEqualTo(16L);
        await Assert.That("17".ParseNullableUInt64()).IsEqualTo(17UL);
        await Assert.That("1.5".ParseNullableSingle()).IsEqualTo(1.5f);
        await Assert.That("2.5".ParseNullableDouble()).IsEqualTo(2.5d);
    }

    [Test]
    public async Task AdditionalParseAndSubstringUntilHelpersTest()
    {
        await Assert.That("12".ParseByte()).IsEqualTo((byte)12);
        await Assert.That("13".ParseSByte()).IsEqualTo((sbyte)13);
        await Assert.That("14".ParseInt16()).IsEqualTo((short)14);
        await Assert.That("15".ParseUInt16()).IsEqualTo((ushort)15);
        await Assert.That("16".ParseUInt32()).IsEqualTo(16U);
        await Assert.That("17".ParseInt64()).IsEqualTo(17L);
        await Assert.That("18".ParseUInt64()).IsEqualTo(18UL);
        await Assert.That("1.25".ParseSingle()).IsEqualTo(1.25f);
        await Assert.That("2.25".ParseDouble()).IsEqualTo(2.25d);
        await Assert.That("3.25".ParseDecimal()).IsEqualTo(3.25m);
        await Assert.That("abc:def:ghi".SubstringUntilFirst(':')).IsEqualTo("abc");
        await Assert.That("abc:def:ghi".SubstringUntilFirst(":def")).IsEqualTo("abc");
        await Assert.That("abc:def:ghi".SubstringUntilLast(':')).IsEqualTo("abc:def");
        await Assert.That("abc:def:ghi".SubstringUntilLast(":def")).IsEqualTo("abc");
    }

    [Test]
    public async Task TryParseAdditionalHelpersTest()
    {
        await Assert.That("10".TryParseByte(out var parsedByte)).IsTrue();
        await Assert.That(parsedByte).IsEqualTo((byte)10);
        await Assert.That("11".TryParseSByte(out var parsedSByte)).IsTrue();
        await Assert.That(parsedSByte).IsEqualTo((sbyte)11);
        await Assert.That("12".TryParseInt16(out var parsedInt16)).IsTrue();
        await Assert.That(parsedInt16).IsEqualTo((short)12);
        await Assert.That("13".TryParseUInt16(out var parsedUInt16)).IsTrue();
        await Assert.That(parsedUInt16).IsEqualTo((ushort)13);
        await Assert.That("14".TryParseUInt32(out var parsedUInt32)).IsTrue();
        await Assert.That(parsedUInt32).IsEqualTo(14U);
        await Assert.That("15".TryParseInt64(out var parsedInt64)).IsTrue();
        await Assert.That(parsedInt64).IsEqualTo(15L);
        await Assert.That("16".TryParseUInt64(out var parsedUInt64)).IsTrue();
        await Assert.That(parsedUInt64).IsEqualTo(16UL);
        await Assert.That("1.5".TryParseSingle(out var parsedSingle)).IsTrue();
        await Assert.That(parsedSingle).IsEqualTo(1.5f);
        await Assert.That("2.5".TryParseDouble(out var parsedDouble)).IsTrue();
        await Assert.That(parsedDouble).IsEqualTo(2.5d);
        await Assert.That("3.5".TryParseDecimal(out var parsedDecimal)).IsTrue();
        await Assert.That(parsedDecimal).IsEqualTo(3.5m);
    }

    [Test]
    public async Task EnsureStartsWithTest()
    {
        await Assert.That("path".EnsureStartsWith("/")).IsEqualTo("/path");
        await Assert.That("/path".EnsureStartsWith("/")).IsEqualTo("/path");
        await Assert.That(((string?)null).EnsureStartsWith("/")).IsEqualTo("/");
    }

    [Test]
    public async Task EnsureEndsWithTest()
    {
        await Assert.That("path".EnsureEndsWith("/")).IsEqualTo("path/");
        await Assert.That("path/".EnsureEndsWith("/")).IsEqualTo("path/");
        await Assert.That(((string?)null).EnsureEndsWith("/")).IsEqualTo("/");
    }

    [Test]
    public async Task NullIfEmptyTest()
    {
        await Assert.That(string.Empty.NullIfEmpty()).IsNull();
        await Assert.That(" ".NullIfEmpty()).IsEqualTo(" ");
    }

    [Test]
    public async Task NullIfEmptyOrWhitespaceTest()
    {
        await Assert.That(" ".NullIfEmptyOrWhitespace()).IsNull();
    }

    [Test]
    public async Task CombineTest()
    {
        await Assert.That(new[] { "a", "b", "c" }.Combine(',')).IsEqualTo("a,b,c");
        await Assert.That(new[] { "a", "b", "c" }.Combine("::")).IsEqualTo("a::b::c");
        await Assert.That(new[] { "a", "b", "c" }.Combine()).IsEqualTo("abc");
    }

    [Test]
    public async Task NonEmptyTest()
    {
        await Assert.That(new string?[] { "a", null, "", "b" }.NonEmpty().SequenceEqual(["a", "b"])).IsTrue();
    }
}
