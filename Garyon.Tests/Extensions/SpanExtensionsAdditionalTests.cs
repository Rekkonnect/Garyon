using Garyon.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class SpanExtensionsAdditionalTests
{
    [Test]
    public async Task SpanDelimiterOverloadsAndSplitOnceVariantsTest()
    {
        int spanIndex;
        int readOnlyIndex;
        int nextIndexFromSpan;
        int nextIndexFromReadOnly;
        string splitLeft;
        string splitRight;
        string missingLeft;
        string missingRight;
        string betweenFromSpan;
        string betweenFromReadOnly;
        {
            Span<char> span = "left::middle::right".ToCharArray();
            ReadOnlySpan<char> readOnly = "left::middle::right".AsSpan();
            Span<char> delimiter = "::".ToCharArray();
            ReadOnlySpan<char> readOnlyDelimiter = "::".AsSpan();

            spanIndex = span.IndexOf(delimiter, out nextIndexFromSpan);
            readOnlyIndex = readOnly.IndexOf(readOnlyDelimiter, out nextIndexFromReadOnly);
            _ = span.SplitOnce(readOnlyDelimiter, out var left, out var right);
            _ = readOnly.SplitOnce(delimiter, out var readOnlyLeft, out var readOnlyRight);
            _ = span.SplitOnce("--".AsSpan(), out var missingLeftSpan, out var missingRightSpan);
            betweenFromSpan = new string(span.SliceBetween(delimiter, readOnlyDelimiter));
            betweenFromReadOnly = readOnly.SliceBetween(readOnlyDelimiter, delimiter).ToString();
            splitLeft = new string(left);
            splitRight = new string(right);
            missingLeft = new string(missingLeftSpan);
            missingRight = new string(missingRightSpan);
            _ = readOnlyLeft;
            _ = readOnlyRight;
        }

        await Assert.That(spanIndex).IsEqualTo(4);
        await Assert.That(readOnlyIndex).IsEqualTo(4);
        await Assert.That(nextIndexFromSpan).IsEqualTo(6);
        await Assert.That(nextIndexFromReadOnly).IsEqualTo(6);
        await Assert.That(splitLeft).IsEqualTo("left");
        await Assert.That(splitRight).IsEqualTo("middle::right");
        await Assert.That(missingLeft).IsEqualTo("left::middle::right");
        await Assert.That(missingRight).IsEqualTo(string.Empty);
        await Assert.That(betweenFromSpan).IsEqualTo("middle");
        await Assert.That(betweenFromReadOnly).IsEqualTo("middle");
    }

    [Test]
    public async Task SplitSelectAdvanceSliceRefAndWrappingNegativeCasesTest()
    {
        string[] charSplit;
        string[] spanSplit;
        bool wrappedSingle;
        bool wrappedSpan;
        string advancedReadOnly;
        string advancedByRef;
        {
            ReadOnlySpan<char> source = "a,b,c".AsSpan();
            Span<char> mutable = "one::two::three".ToCharArray();
            ReadOnlySpan<char> readOnly = "prefix-value".AsSpan();

            charSplit = source.SplitSelect(',', static span => span.ToString()).ToArray();
            spanSplit = mutable.SplitSelect("::".AsSpan(), static span => span.ToString()).ToArray();
            wrappedSingle = "value]".AsSpan().IsWrappedIn('[', ']', out _);
            wrappedSpan = "<value".AsSpan().IsWrappedIn("<".AsSpan(), "/>".AsSpan(), out _);
            var advanced = readOnly.AdvanceSlice(7);
            readOnly.AdvanceSliceRef(7);
            advancedReadOnly = advanced.ToString();
            advancedByRef = readOnly.ToString();
        }

        await Assert.That(charSplit.Length).IsEqualTo(3);
        await Assert.That(charSplit[0]).IsEqualTo("a");
        await Assert.That(charSplit[1]).IsEqualTo("b");
        await Assert.That(charSplit[2]).IsEqualTo("c");
        await Assert.That(spanSplit.Length).IsEqualTo(3);
        await Assert.That(spanSplit[0]).IsEqualTo("one");
        await Assert.That(spanSplit[1]).IsEqualTo("two");
        await Assert.That(spanSplit[2]).IsEqualTo("three");
        await Assert.That(wrappedSingle).IsFalse();
        await Assert.That(wrappedSpan).IsFalse();
        await Assert.That(advancedReadOnly).IsEqualTo("value");
        await Assert.That(advancedByRef).IsEqualTo("value");
    }

    [Test]
    public async Task SplitEnumeratorResetTest()
    {
        string[] firstRun;
        string[] secondRun;
        {
            var enumerator = "a::b::c".AsSpan().SplitEnumerate("::".AsSpan());
            var first = new System.Collections.Generic.List<string>();
            while (enumerator.MoveNext())
                first.Add(enumerator.Current.ToString());

            enumerator.Reset();
            var second = new System.Collections.Generic.List<string>();
            while (enumerator.MoveNext())
                second.Add(enumerator.Current.ToString());

            firstRun = [.. first];
            secondRun = [.. second];
        }

        await Assert.That(firstRun.SequenceEqual(["a", "b", "c"])).IsTrue();
        await Assert.That(secondRun.SequenceEqual(["a", "b", "c"])).IsTrue();
    }
}
