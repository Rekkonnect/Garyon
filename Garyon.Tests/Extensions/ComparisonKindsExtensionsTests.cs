using Garyon.Objects;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class ComparisonKindsExtensionsTests
{
    [Test]
    public async Task MatchesAndHasKindsTest()
    {
        await Assert.That(ComparisonKinds.LessOrEqual.Matches(ComparisonResult.Less)).IsTrue();
        await Assert.That(ComparisonKinds.LessOrEqual.Matches(ComparisonResult.Greater)).IsFalse();
        await Assert.That(ComparisonKinds.All.HasKinds(ComparisonKinds.Equal)).IsTrue();
        await Assert.That(ComparisonKinds.Greater.HasKinds(ComparisonKinds.Less)).IsFalse();
    }

    [Test]
    public async Task AsResultTest()
    {
        await Assert.That(ComparisonKinds.Less.AsResult()).IsEqualTo(ComparisonResult.Less);
        await Assert.That(ComparisonKinds.Equal.AsResult()).IsEqualTo(ComparisonResult.Equal);
        await Assert.That(ComparisonKinds.Greater.AsResult()).IsEqualTo(ComparisonResult.Greater);
    }

    [Test]
    public async Task AsResultThrowsForNonSingleKindTest()
    {
        bool threw = false;
        try
        {
            _ = ComparisonKinds.All.AsResult();
        }
        catch (ArgumentException)
        {
            threw = true;
        }

        await Assert.That(threw).IsTrue();
    }

    [Test]
    public async Task AsComparisonKindTest()
    {
        await Assert.That(ComparisonResult.Less.AsComparisonKind).IsEqualTo(ComparisonKinds.Less);
        await Assert.That(ComparisonResult.Equal.AsComparisonKind).IsEqualTo(ComparisonKinds.Equal);
        await Assert.That(ComparisonResult.Greater.AsComparisonKind).IsEqualTo(ComparisonKinds.Greater);
    }

    [Test]
    public async Task AsComparisonKindThrowsForInvalidValueTest()
    {
        bool threw = false;
        try
        {
            _ = ((ComparisonResult)5).AsComparisonKind;
        }
        catch (InvalidEnumArgumentException)
        {
            threw = true;
        }

        await Assert.That(threw).IsTrue();
    }
}
