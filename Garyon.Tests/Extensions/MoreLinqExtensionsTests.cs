using Garyon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class MoreLinqExtensionsTests
{
    [Test]
    public async Task WherePredicateTest()
    {
        var wherePredicate = new[] { 1, 2, 3, 4 }.WherePredicate(i => i % 2 == 0).ToArray();
        await Assert.That(wherePredicate.SequenceEqual([2, 4])).IsTrue();
    }

    [Test]
    public async Task WhereOrSourceTest()
    {
        var whereOrSource = new[] { 1, 2, 3 }.WhereOrSource(null).ToArray();
        await Assert.That(whereOrSource.SequenceEqual([1, 2, 3])).IsTrue();
    }

    [Test]
    public async Task SelectOrDefaultTest()
    {
        var selected = new[] { 1, 2, 3 }.SelectOrDefault(i => $"#{i}")!.ToArray();
        await Assert.That(selected.SequenceEqual(["#1", "#2", "#3"])).IsTrue();
        await Assert.That(new[] { 1, 2, 3 }.SelectOrDefault<int, string>(null)).IsNull();
    }

    [Test]
    public async Task OnlyOrDefaultTest()
    {
        await Assert.That(new[] { 5 }.OnlyOrDefault()).IsEqualTo(5);
        await Assert.That(new[] { "a", "b" }.OnlyOrDefault()).IsNull();
        await Assert.That(((IEnumerable<string>?)null).OnlyOrDefault()).IsNull();
    }

    [Test]
    public async Task AnyDeepTest()
    {
        await Assert.That(new[] { Array.Empty<int>(), [1] }.AnyDeep()).IsTrue();
        await Assert.That(new[] { Array.Empty<int>(), Array.Empty<int>() }.AnyDeep()).IsFalse();
    }

    [Test]
    public async Task EnumeratePerformActionTest()
    {
        var visited = new List<string>();
        var hadAny = new[] { "a", "b", "c" }.EnumeratePerformAction(
            firstElementAction: s => visited.Add($"first:{s}"),
            lastElementAction: s => visited.Add($"last:{s}"),
            enumerationAction: s => visited.Add($"each:{s}"));
        await Assert.That(hadAny).IsTrue();
        await Assert.That(visited.SequenceEqual(
        [
            "first:a",
            "each:a",
            "each:b",
            "each:c",
            "last:c",
        ])).IsTrue();
        await Assert.That(Array.Empty<int>().EnumeratePerformAction<int>(null, null, _ => { })).IsFalse();

        await Assert.That(new[] { 1, 2, 3 }.CountAtLeast(2)).IsTrue();
        await Assert.That(new[] { 1, 2, 3 }.CountAtMost(3)).IsTrue();
        await Assert.That(new[] { 1, 2, 3 }.CountExactly(3)).IsTrue();
        await Assert.That(new[] { 1, 2, 3 }.CountExactly(2)).IsFalse();
        await Assert.That(new[] { 1, 2, 3 }.CountBetween(2, 3)).IsTrue();
        await Assert.That(new[] { 1, 2, 3, 4 }.CountBetween(2, 3)).IsFalse();
        await Assert.That(new[] { 1, 2, 3, 4 }.CountAtLeast(i => i % 2 == 0, 1)).IsTrue();
        await Assert.That(new[] { 1, 2, 3, 4 }.CountAtMost(i => i % 2 == 0, 3)).IsTrue();
        await Assert.That(new[] { 1, 2, 3, 4 }.CountExactly(i => i % 2 == 0, 2)).IsTrue();
        await Assert.That(new[] { 1, 2, 3, 4 }.CountBetween(i => i % 2 == 0, 1, 2)).IsTrue();

        object?[] values = [1, null, "x", null];
        await Assert.That(values.NotNullCount()).IsEqualTo(2);
        await Assert.That(values.NullCount()).IsEqualTo(2);
        await Assert.That(values.WhereNotNull().Count()).IsEqualTo(2);
        await Assert.That(new[] { 1, 2, 3, 4 }.WhereNot(i => i % 2 == 0).SequenceEqual([1, 3])).IsTrue();
        await Assert.That(5.Apply(i => i * 2)).IsEqualTo(10);

        var applyTarget = new List<int>();
        var sameInstance = applyTarget.Apply(l => l.Add(1));
        await Assert.That(ReferenceEquals(applyTarget, sameInstance)).IsTrue();
        await Assert.That(applyTarget.SequenceEqual([1])).IsTrue();

        var grouped = new[]
        {
            (Key: "a", Value: 1),
            (Key: "a", Value: 2),
            (Key: "b", Value: 3),
        }.GroupBy(x => x.Key, x => x.Value);
        var listDictionary = grouped.ToListDictionary();
        await Assert.That(listDictionary["a"].SequenceEqual([1, 2])).IsTrue();
        await Assert.That(listDictionary["b"].SequenceEqual([3])).IsTrue();
    }

    [Test]
    public async Task ZipTest()
    {
        var zipped = MoreLinqExtensions.Zip([1, 2], ["a", "b", "c"]).ToArray();
        await Assert.That(zipped.Length).IsEqualTo(2);
        await Assert.That(zipped[0].First).IsEqualTo(1);
        await Assert.That(zipped[0].Second).IsEqualTo("a");
    }
}
