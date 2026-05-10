using Garyon.Extensions;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class SpanExtensionsTests
{
    [Test]
    public async Task IndexOfAndSliceHelpersTest()
    {
        Span<char> span = "alpha-beta-gamma".ToCharArray();
        ReadOnlySpan<char> readOnlySpan = "alpha-beta-gamma".AsSpan();

        int index = span.IndexOf('-', out int nextIndex);
        var before = new string(span.SliceBefore('-'));
        var after = new string(span.SliceAfter('-'));
        var between = new string(readOnlySpan.SliceBetween('-', '-'));

        await Assert.That(index).IsEqualTo(5);
        await Assert.That(nextIndex).IsEqualTo(6);
        await Assert.That(before).IsEqualTo("alpha");
        await Assert.That(after).IsEqualTo("beta-gamma");
        await Assert.That(between).IsEqualTo("beta");
    }

    [Test]
    public async Task SliceIndexFallbackHelpersTest()
    {
        Span<char> span = "alpha".ToCharArray();
        ReadOnlySpan<char> readOnlySpan = "alpha".AsSpan();

        var after = new string(span.SliceAfterIndex(-1));
        var before = new string(readOnlySpan.SliceBeforeIndex(-1));

        await Assert.That(after).IsEqualTo("alpha");
        await Assert.That(before).IsEqualTo("alpha");
    }

    [Test]
    public async Task SplitOnceHelpersTest()
    {
        ReadOnlySpan<char> found = "left:right".AsSpan();
        bool splitFound = found.SplitOnce(':', out var left, out var right);
        var leftString = left.ToString();
        var rightString = right.ToString();

        ReadOnlySpan<char> notFound = "single".AsSpan();
        bool splitMissing = notFound.SplitOnce(':', out var missingLeft, out var missingRight);
        var missingLeftString = missingLeft.ToString();
        var missingRightString = missingRight.ToString();

        await Assert.That(splitFound).IsTrue();
        await Assert.That(leftString).IsEqualTo("left");
        await Assert.That(rightString).IsEqualTo("right");
        await Assert.That(splitMissing).IsFalse();
        await Assert.That(missingLeftString).IsEqualTo("single");
        await Assert.That(missingRightString).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task SplitEnumerateAndSplitToStringsTest()
    {
        var commaParts = "a,b,,c".AsSpan().SplitToStrings(',');
        var spanParts = "one--two--three".AsSpan().SplitToStrings("--".AsSpan());

        bool firstMove;
        bool secondMove;
        bool thirdMove;
        bool fourthMove;
        string first;
        string second;
        string third;
        {
            var enumerator = "left::middle::right".AsSpan().SplitEnumerate("::".AsSpan());
            firstMove = enumerator.MoveNext();
            first = enumerator.Current.ToString();
            secondMove = enumerator.MoveNext();
            second = enumerator.Current.ToString();
            thirdMove = enumerator.MoveNext();
            third = enumerator.Current.ToString();
            fourthMove = enumerator.MoveNext();
        }

        await Assert.That(firstMove).IsTrue();
        await Assert.That(first).IsEqualTo("left");
        await Assert.That(secondMove).IsTrue();
        await Assert.That(second).IsEqualTo("middle");
        await Assert.That(thirdMove).IsTrue();
        await Assert.That(third).IsEqualTo("right");
        await Assert.That(fourthMove).IsFalse();

        await Assert.That(commaParts.Count).IsEqualTo(4);
        await Assert.That(commaParts[0]).IsEqualTo("a");
        await Assert.That(commaParts[1]).IsEqualTo("b");
        await Assert.That(commaParts[2]).IsEqualTo("");
        await Assert.That(commaParts[3]).IsEqualTo("c");
        await Assert.That(spanParts.Count).IsEqualTo(3);
        await Assert.That(spanParts[0]).IsEqualTo("one");
        await Assert.That(spanParts[1]).IsEqualTo("two");
        await Assert.That(spanParts[2]).IsEqualTo("three");
    }

    [Test]
    public async Task WrappingAndAdvanceSliceHelpersTest()
    {
        bool singleWrapped;
        bool spanWrapped;
        string singleInnerString;
        string spanInnerString;
        string advancedString;
        string mutableString;
        {
            ReadOnlySpan<char> wrapped = "[value]".AsSpan();
            Span<char> mutable = "prefix-value".ToCharArray();

            singleWrapped = wrapped.IsWrappedIn('[', ']', out var singleInner);
            spanWrapped = wrapped.IsWrappedIn("[".AsSpan(), "]".AsSpan(), out var spanInner);
            var advanced = mutable.AdvanceSlice(7);
            mutable.AdvanceSliceRef(7);
            singleInnerString = singleInner.ToString();
            spanInnerString = spanInner.ToString();
            advancedString = new string(advanced);
            mutableString = new string(mutable);
        }

        await Assert.That(singleWrapped).IsTrue();
        await Assert.That(singleInnerString).IsEqualTo("value");
        await Assert.That(spanWrapped).IsTrue();
        await Assert.That(spanInnerString).IsEqualTo("value");
        await Assert.That(advancedString).IsEqualTo("value");
        await Assert.That(mutableString).IsEqualTo("value");
    }

    [Test]
    public async Task StringReplacementHelpersTest()
    {
        var single = new StringReplacement(6, 4, "BETA");
        var replaced = "alpha beta gamma".Replace(
        [
            single,
            new StringReplacement(0, 5, "ALPHA"),
        ]);

        await Assert.That(single.Apply("alpha beta gamma")).IsEqualTo("alpha BETA gamma");
        await Assert.That(replaced).IsEqualTo("ALPHA BETA gamma");
    }

    [Test]
    public async Task SpanLineEnumeratorHelpersTest()
    {
        bool firstConsumed;
        string firstLine;
        bool foundNonEmpty;
        string nonEmptyLine;
        bool secondConsumed;
        string secondLine;
        {
            var enumerator = "\nfirst\n\nsecond".AsSpan().EnumerateLines();
            firstConsumed = enumerator.ConsumeNext(out var first);
            firstLine = first.ToString();
            foundNonEmpty = enumerator.SkipEmpty();
            nonEmptyLine = enumerator.Current.ToString();
            secondConsumed = enumerator.ConsumeNext(out var second);
            secondLine = second.ToString();
        }

        await Assert.That(firstConsumed).IsTrue();
        await Assert.That(firstLine).IsEqualTo("");
        await Assert.That(foundNonEmpty).IsTrue();
        await Assert.That(nonEmptyLine).IsEqualTo("first");
        await Assert.That(secondConsumed).IsTrue();
        await Assert.That(secondLine).IsEqualTo("");
    }

    [Test]
    public async Task StringOperatorRepeatHelpersTest()
    {
        await Assert.That("ab" * 3).IsEqualTo("ababab");
        await Assert.That(2 * "cd").IsEqualTo("cdcd");
    }

    [Test]
    public async Task SpanStringParsingAndSelectorHelpersTest()
    {
        int parsedFirst;
        int firstEndIndex;
        int lastNumberStartIndex;
        int parsedLast;
        long parsedLong;
        uint parsedUnsigned;
        string? emptyString;
        string nonEmptyString;
        {
            ReadOnlySpan<char> source = "id=-42; tail=123".AsSpan();
            parsedFirst = source.ParseFirstInt32(3, out firstEndIndex);
            lastNumberStartIndex = source.LastNumberStartIndex();
            parsedLast = source.ParseLastInt32();
            parsedLong = "123456".AsSpan().ParseInt64();
            parsedUnsigned = "42".AsSpan().ParseUInt32();
            emptyString = ReadOnlySpan<char>.Empty.ToStringOrNull();
            nonEmptyString = "value".AsSpan().ToStringOrNull()!;
        }

        await Assert.That(parsedFirst).IsEqualTo(-42);
        await Assert.That(firstEndIndex).IsEqualTo(6);
        await Assert.That(lastNumberStartIndex).IsEqualTo(13);
        await Assert.That(parsedLast).IsEqualTo(123);
        await Assert.That(parsedLong).IsEqualTo(123456L);
        await Assert.That(parsedUnsigned).IsEqualTo(42U);
        await Assert.That(emptyString).IsNull();
        await Assert.That(nonEmptyString).IsEqualTo("value");
    }
}
