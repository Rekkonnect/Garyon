using Garyon.Extensions;
using Garyon.Objects;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class IComparableExtensionsTests
{
    [Test]
    public async Task NonGenericComparisonHelpersTest()
    {
        IComparable value = 5;

        await Assert.That(value.LessThan(6)).IsTrue();
        await Assert.That(value.GreaterThan(4)).IsTrue();
        await Assert.That(value.LessThanOrEqual(5)).IsTrue();
        await Assert.That(value.GreaterThanOrEqual(5)).IsTrue();
        await Assert.That(value.EqualTo(5)).IsTrue();
        await Assert.That(value.NotEqualTo(6)).IsTrue();
        await Assert.That(value.SatisfiesComparison(4, ComparisonKinds.Greater)).IsTrue();
        await Assert.That(value.MatchesComparisonResult(4, ComparisonResult.Greater)).IsTrue();
        await Assert.That(value.GetComparisonResult(5)).IsEqualTo(ComparisonResult.Equal);
    }

    [Test]
    public async Task GenericComparisonHelpersTest()
    {
        IComparable<int> value = 5;

        await Assert.That(value.LessThan(6)).IsTrue();
        await Assert.That(value.GreaterThan(4)).IsTrue();
        await Assert.That(value.LessThanOrEqual(5)).IsTrue();
        await Assert.That(value.GreaterThanOrEqual(5)).IsTrue();
        await Assert.That(value.EqualTo(5)).IsTrue();
        await Assert.That(value.NotEqualTo(6)).IsTrue();
        await Assert.That(value.SatisfiesComparison(4, ComparisonKinds.Greater)).IsTrue();
        await Assert.That(value.MatchesComparisonResult(4, ComparisonResult.Greater)).IsTrue();
        await Assert.That(value.GetComparisonResult(6)).IsEqualTo(ComparisonResult.Less);
    }

    [Test]
    public async Task AssignExtremumHelpersTest()
    {
        int min = 10;
        min.AssignMin(5);
        await Assert.That(min).IsEqualTo(5);

        int max = 10;
        max.AssignMax(15);
        await Assert.That(max).IsEqualTo(15);

        int extremum = 10;
        extremum.AssignExtremum(20, ComparisonResult.Greater);
        await Assert.That(extremum).IsEqualTo(20);
    }
}
