using Garyon.Extensions;
using Garyon.Extensions.Upcasting;
using Garyon.Objects;
using Garyon.Objects.Enumerators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class ExtensionCoverageAdditionalTests
{
    [Test]
    public async Task CollectionExtensionsAddCloneAndConvertCollectionsTest()
    {
        ICollection<string> collection = new List<string>();
        collection.AddRange("a", "b");
        collection.AddRange(["c"]);
        collection.AddNotNull(null);
        collection.AddNotNull("d");
        collection.ClearSetRange(["x", "y"]);

        var list = new List<int> { 1, 2 };
        var hashSet = new HashSet<int> { 2, 1 };
        var sortedSet = new SortedSet<int> { 2, 1 };
        var dictionary = new Dictionary<string, int> { ["b"] = 2, ["a"] = 1 };

        await Assert.That(collection.SequenceEqual(["x", "y"])).IsTrue();
        await Assert.That(list.Clone().SequenceEqual([1, 2])).IsTrue();
        await Assert.That(hashSet.Clone().SetEquals([1, 2])).IsTrue();
        await Assert.That(sortedSet.Clone().SequenceEqual([1, 2])).IsTrue();
        await Assert.That(dictionary.Clone()["a"]).IsEqualTo(1);
        await Assert.That(dictionary.ToSortedList().Keys.SequenceEqual(["a", "b"])).IsTrue();
        await Assert.That(dictionary.ToSortedDictionary().Keys.SequenceEqual(["a", "b"])).IsTrue();
        await Assert.That(new[] { 2, 1 }.ToSortedSet().SequenceEqual([1, 2])).IsTrue();
        await Assert.That(((IEnumerable<int>?)null).ToArrayOrEmpty()).IsEmpty();
        await Assert.That(((IEnumerable<int>?)null).ToListOrEmpty()).IsEmpty();
        await Assert.That(((IEnumerable<int>?)null).ToHashSetOrEmpty().Count).IsEqualTo(0);
        await Assert.That(((IEnumerable<int>?)null).ToSortedSetOrEmpty().Count).IsEqualTo(0);
        await Assert.That(((IDictionary<string, int>?)null).ToSortedListOrEmpty()).IsEmpty();
        await Assert.That(((IDictionary<string, int>?)null).ToSortedDictionaryOrEmpty()).IsEmpty();
    }

    [Test]
    public async Task ToCollectionOrExistingReturnsExistingOrMaterializedCollectionsTest()
    {
        int[] array = [1, 2];
        var list = new List<int> { 1, 2 };
        var set = new HashSet<int> { 1, 2 };
        IEnumerable<int> iterator = Enumerable.Range(1, 2).Select(static value => value);
        IEnumerable<KeyValuePair<string, int>> kvps =
        [
            new("a", 1),
        ];
        var dictionary = new Dictionary<string, int> { ["a"] = 1 };

        await Assert.That(ReferenceEquals(array, array.ToArrayOrExisting())).IsTrue();
        await Assert.That(iterator.ToArrayOrExisting().SequenceEqual([1, 2])).IsTrue();
        await Assert.That(ReferenceEquals(list, list.ToListOrExisting())).IsTrue();
        await Assert.That(iterator.ToListOrExisting().SequenceEqual([1, 2])).IsTrue();
        await Assert.That(ReferenceEquals(set, set.ToHashSetOrExisting())).IsTrue();
        await Assert.That(iterator.ToHashSetOrExisting().SetEquals([1, 2])).IsTrue();
        await Assert.That(ReferenceEquals(list, list.ToCollectionOrExisting())).IsTrue();
        await Assert.That(iterator.ToCollectionOrExisting().SequenceEqual([1, 2])).IsTrue();
        await Assert.That(ReferenceEquals(set, set.ToSetOrExisting())).IsTrue();
        await Assert.That(iterator.ToSetOrExisting().SetEquals([1, 2])).IsTrue();
        await Assert.That(ReferenceEquals(set, set.ToReadOnlySetOrExisting())).IsTrue();
        await Assert.That(iterator.ToReadOnlySetOrExisting().SetEquals([1, 2])).IsTrue();
        await Assert.That(ReferenceEquals(list, list.ToReadOnlyCollectionOrExisting())).IsTrue();
        await Assert.That(iterator.ToReadOnlyCollectionOrExisting().SequenceEqual([1, 2])).IsTrue();
        await Assert.That(ReferenceEquals(list, list.ToReadOnlyListOrExisting())).IsTrue();
        await Assert.That(iterator.ToReadOnlyListOrExisting().SequenceEqual([1, 2])).IsTrue();
        await Assert.That(ReferenceEquals(dictionary, dictionary.ToDictionaryOrExisting())).IsTrue();
        await Assert.That(kvps.ToDictionaryOrExisting()["a"]).IsEqualTo(1);
        await Assert.That(ReferenceEquals(dictionary, dictionary.ToReadOnlyDictionaryOrExisting())).IsTrue();
        await Assert.That(kvps.ToReadOnlyDictionaryOrExisting()["a"]).IsEqualTo(1);
    }

    [Test]
    public async Task DictionaryExtensionsCoverPreserveTransformAndNonGenericEnumerationTest()
    {
        var dictionary = new Dictionary<string, int>();
        dictionary.IncrementOrAddKeyValue("a");
        dictionary.IncrementOrAddKeyValue("a");
        dictionary.AddRange(new Dictionary<string, int> { ["b"] = 3 });

        await Assert.That(dictionary.ValueOrDefault("missing", 9)).IsEqualTo(9);
        await Assert.That(dictionary.ValueOrDefault(null, 8)).IsEqualTo(8);
        await Assert.That(dictionary.AddOrSet("a", 2)).IsFalse();
        await Assert.That(dictionary.AddOrSet("a", 5)).IsTrue();
        await Assert.That(dictionary.TryAddPreserve("a", 6, out var existing)).IsFalse();
        await Assert.That(existing).IsEqualTo(5);
        await Assert.That(dictionary.TryAddPreserve(new KeyValuePair<string, int>("a", 5))).IsTrue();
        await Assert.That(dictionary.TryAddPreserve(new KeyValuePair<string, int>("c", 7), out existing)).IsTrue();
        await Assert.That(dictionary.TryAddPreserveRange(new Dictionary<string, int> { ["c"] = 9, ["d"] = 10 })).IsFalse();
        await Assert.That(dictionary.AddOrSetRange(new Dictionary<string, int> { ["d"] = 10, ["a"] = 11 })).IsTrue();
        await Assert.That(dictionary.GetKeyValuePair("a")).IsEqualTo(new KeyValuePair<string, int>("a", 11));
        IDictionaryExtensions.Add(dictionary, new KeyValuePair<string, int>("e", 12));
        await Assert.That(dictionary["e"]).IsEqualTo(12);

        IReadOnlyDictionary<string, int> readOnly = dictionary;
        await Assert.That(readOnly.TransformKeys(static key => key.Length).ContainsKey(1)).IsTrue();
        await Assert.That(readOnly.TransformValues(static value => value.ToString())["a"]).IsEqualTo("11");

        IDictionary nonGeneric = new Hashtable { ["a"] = 1, ["b"] = 2 };
        var entries = nonGeneric.EnumerateDictionaryEntries().ToArray();
        var objectKvps = nonGeneric.EnumerateEntriesAsKvp().ToArray();
        var entryEnumerable = nonGeneric.EnumerateDictionaryEntries();
        using var entryEnumerator = entryEnumerable.GetEnumerator();
        await Assert.That(entryEnumerator.MoveNext()).IsTrue();
        _ = ((IEnumerator)entryEnumerator).Current;
        entryEnumerator.Reset();
        await Assert.That(((IEnumerable)entryEnumerable).GetEnumerator().MoveNext()).IsTrue();
        var kvpEnumerable = nonGeneric.EnumerateEntriesAsKvp();
        await Assert.That(((IEnumerable)kvpEnumerable).GetEnumerator().MoveNext()).IsTrue();

        await Assert.That(nonGeneric.Keys<string>().Order().SequenceEqual(["a", "b"])).IsTrue();
        await Assert.That(nonGeneric.Values<int>().Order().SequenceEqual([1, 2])).IsTrue();
        await Assert.That(entries.Length).IsEqualTo(2);
        await Assert.That(objectKvps.Select(static kvp => kvp.Key).Cast<string>().Order().SequenceEqual(["a", "b"])).IsTrue();
    }

    [Test]
    public async Task EnumerableAndLinqExtensionsCoverFilteringCuttingAndUnsignedSumsTest()
    {
        var source = new[] { 1, 2, 3, 2 };
        source.Dissect(static value => value % 2 == 0, out var even, out var odd);

        await Assert.That(source.EqualsUnordered([3, 2, 1, 2])).IsTrue();
        await Assert.That(source.EqualsUnordered([1, 2])).IsFalse();
        IEnumerable nonGenericFirst = new[] { "a", "b" };
        IEnumerable nonGenericSecond = new[] { "b", "a" };
        IEnumerable nonGenericMissing = new[] { "a", "c" };
        IEnumerable nonGenericLong = new[] { "a", "b", "c" };
        await Assert.That(nonGenericFirst.EqualsUnordered(nonGenericSecond)).IsTrue();
        await Assert.That(nonGenericFirst.EqualsUnordered(nonGenericLong)).IsFalse();
        await Assert.That(nonGenericFirst.EqualsUnordered(nonGenericMissing)).IsFalse();
        await Assert.That(((IEnumerable)new[] { "a", "a" }).EqualsUnordered(new[] { "a", "a" })).IsFalse();
        await Assert.That(nonGenericFirst.EqualsUnordered(YieldObjects("a", "b", "c"))).IsFalse();
        await Assert.That(YieldObjects("a", "b").EqualsUnordered(YieldObjects("a", "b", "c"))).IsFalse();
        await Assert.That(nonGenericFirst.EqualsUnordered(YieldObjects("a", "c"))).IsFalse();
        await Assert.That(even.SequenceEqual([2, 2])).IsTrue();
        await Assert.That(odd.SequenceEqual([1, 3])).IsTrue();
        await Assert.That(Array.Empty<int>().Apply(items => { items.Dissect(static value => value > 0, out var matched, out var unmatched); return matched.Any() || unmatched.Any(); })).IsFalse();
        await Assert.That(source.UntilFirstRecursive().SequenceEqual([1, 2, 3])).IsTrue();
        await Assert.That(new[] { 1, 2, 3 }.UntilFirstRecursive().SequenceEqual([1, 2, 3])).IsTrue();
        await Assert.That(source.ConcatSingleValue(4).SequenceEqual([1, 2, 3, 2, 4])).IsTrue();
        await Assert.That(source.Concat(5, 6).SequenceEqual([1, 2, 3, 2, 5, 6])).IsTrue();
        await Assert.That(new int?[] { 1, null, 2 }.GetValuedElements().SequenceEqual([1, 2])).IsTrue();
        await Assert.That(new string?[] { "a", null }.AsNonNull().First()).IsEqualTo("a");
        await Assert.That(new[] { new KeyValuePair<string, int>("one", 1) }.SelectKeys(static key => key.Length).Single().Key).IsEqualTo(3);
        await Assert.That(new[] { new KeyValuePair<string, int>("one", 1) }.SelectValues(static value => value + 1).Single().Value).IsEqualTo(2);

        var indexedSum = 0;
        var sum = 0;
        source.ForEach(value => sum += value);
        source.ForEach((index, value) => indexedSum += index * value);
        await Assert.That(sum).IsEqualTo(8);
        await Assert.That(indexedSum).IsEqualTo(14);

        await Assert.That(new uint[] { 1, 2, 3 }.SumsBelow(10)).IsTrue();
        await Assert.That(new uint[] { 1, 2, 3 }.SumsAtMost(6)).IsTrue();
        await Assert.That(new uint[] { 5, 6 }.SumsOver(10)).IsTrue();
        await Assert.That(new uint[] { 5, 6 }.SumsAtLeast(11)).IsTrue();
        await Assert.That(new uint[] { 5, 5 }.SumSatisfies(ComparisonKinds.All, 10)).IsTrue();
        await Assert.That(new uint[] { 5, 5 }.SumSatisfies(ComparisonKinds.Equal, 10)).IsTrue();
        await Assert.That(new uint[] { 1, 2 }.SumSatisfies(ComparisonKinds.Less, 10)).IsTrue();
        await Assert.That(new ulong[] { 1, 2, 3 }.SumsAtMost(6)).IsTrue();
        await Assert.That(new ulong[] { 1, 2, 3 }.SumsBelow(10)).IsTrue();
        await Assert.That(new ulong[] { 4, 6 }.SumsAtLeast(10)).IsTrue();
        await Assert.That(new ulong[] { 4, 6 }.SumsOver(9)).IsTrue();
        await Assert.That(new ulong[] { 4, 6 }.SumSatisfies(ComparisonKinds.All, 10)).IsTrue();
        await Assert.That(new ulong[] { 4, 6 }.SumSatisfies(ComparisonKinds.Different, 11)).IsTrue();
        await Assert.That(new uint[] { 1, 2 }.CutAt(static value => value < 2, true).SequenceEqual([1U, 2U])).IsTrue();
        await Assert.That(new uint[] { 1, 2 }.CutAt(static value => value < 2).SequenceEqual([1U])).IsTrue();
        await Assert.That(new uint[] { 1, 2 }.CutAt(static value => value < 3).SequenceEqual([1U, 2U])).IsTrue();

        await Assert.That(PendingLinqExtensions.Sum(new uint[] { 1, 2 })).IsEqualTo(3U);
        await Assert.That(PendingLinqExtensions.Sum(new uint?[] { 1, null, 2 })).IsNull();
        await Assert.That(PendingLinqExtensions.Sum(new ulong[] { 1, 2 })).IsEqualTo(3UL);
        await Assert.That(PendingLinqExtensions.Sum(new ulong?[] { 1, null, 2 })).IsNull();
        await Assert.That(PendingLinqExtensions.Sum(source, static value => (uint)value)).IsEqualTo(8U);
        await Assert.That(PendingLinqExtensions.Sum(source, static value => (uint?)value)).IsEqualTo(8U);
        await Assert.That(PendingLinqExtensions.Sum(source, static value => (ulong)value)).IsEqualTo(8UL);
        await Assert.That(PendingLinqExtensions.Sum(source, static value => (ulong?)value)).IsEqualTo(8UL);
        Assert.Throws<ArgumentNullException>(() => PendingLinqExtensions.Sum((IEnumerable<uint>)null!));
        Assert.Throws<ArgumentNullException>(() => PendingLinqExtensions.Sum((IEnumerable<uint?>)null!));
        Assert.Throws<ArgumentNullException>(() => PendingLinqExtensions.Sum((IEnumerable<ulong>)null!));
        Assert.Throws<ArgumentNullException>(() => PendingLinqExtensions.Sum((IEnumerable<ulong?>)null!));
        Assert.Throws<ArgumentNullException>(() => PendingLinqExtensions.Sum((IEnumerable<int>)null!, static value => (uint)value));
        Assert.Throws<ArgumentNullException>(() => PendingLinqExtensions.Sum(source, (Func<int, uint>)null!));
        Assert.Throws<ArgumentNullException>(() => PendingLinqExtensions.Sum((IEnumerable<int>)null!, static value => (uint?)value));
        Assert.Throws<ArgumentNullException>(() => PendingLinqExtensions.Sum(source, (Func<int, uint?>)null!));
        Assert.Throws<ArgumentNullException>(() => PendingLinqExtensions.Sum((IEnumerable<int>)null!, static value => (ulong)value));
        Assert.Throws<ArgumentNullException>(() => PendingLinqExtensions.Sum(source, (Func<int, ulong>)null!));
        Assert.Throws<ArgumentNullException>(() => PendingLinqExtensions.Sum((IEnumerable<int>)null!, static value => (ulong?)value));
        Assert.Throws<ArgumentNullException>(() => PendingLinqExtensions.Sum(source, (Func<int, ulong?>)null!));
        Assert.Throws<OverflowException>(() => PendingLinqExtensions.Sum(new[] { uint.MaxValue, 1U }));
        Assert.Throws<OverflowException>(() => PendingLinqExtensions.Sum(new uint?[] { uint.MaxValue, 1U }));
        Assert.Throws<OverflowException>(() => PendingLinqExtensions.Sum(new[] { ulong.MaxValue, 1UL }));
        Assert.Throws<OverflowException>(() => PendingLinqExtensions.Sum(new ulong?[] { ulong.MaxValue, 1UL }));
        Assert.Throws<OverflowException>(() => PendingLinqExtensions.Sum(source, static _ => uint.MaxValue));
        Assert.Throws<OverflowException>(() => PendingLinqExtensions.Sum(source, static _ => (uint?)uint.MaxValue));
        Assert.Throws<OverflowException>(() => PendingLinqExtensions.Sum(source, static _ => ulong.MaxValue));
        Assert.Throws<OverflowException>(() => PendingLinqExtensions.Sum(source, static _ => (ulong?)ulong.MaxValue));
    }

    [Test]
    public async Task MoreLinqRegexAndUpcastingExtensionsCoverRemainingHelpersTest()
    {
        var regex = new Regex("[a-z]+", RegexOptions.Compiled);
        regex.ColdStart();

        IEnumerable<int> source = [1, 2, 3];
        var actionLog = new List<string>();
        var grouped = new string?[] { "a", null, "bb" }
            .GroupBy(static value => value)
            .WhereNotNullKeys()
            .ToList();

        await Assert.That(source.WherePredicate(static value => value > 1).SequenceEqual([2, 3])).IsTrue();
        await Assert.That(source.AllDistinctBy(static value => value)).IsTrue();
        await Assert.That(new[] { "a", "b" }.AllDistinctBy(static value => value.Length)).IsFalse();
        await Assert.That(ReferenceEquals(source, source.WhereOrSource(null))).IsTrue();
        await Assert.That(source.WhereOrSource(static value => value < 3).SequenceEqual([1, 2])).IsTrue();
        await Assert.That(source.SelectOrDefault<int, int>(null)).IsNull();
        await Assert.That(source.SelectOrDefault(static value => value * 2)!.SequenceEqual([2, 4, 6])).IsTrue();
        await Assert.That(((IEnumerable<int>?)null).OnlyOrDefault()).IsEqualTo(0);
        await Assert.That(new[] { 4 }.OnlyOrDefault()).IsEqualTo(4);
        await Assert.That(source.OnlyOrDefault()).IsEqualTo(0);
        await Assert.That(new[] { Array.Empty<int>(), new[] { 1 } }.AnyDeep()).IsTrue();
        await Assert.That(source.EnumeratePerformAction(
            value => actionLog.Add($"first:{value}"),
            value => actionLog.Add($"last:{value}"),
            value => actionLog.Add($"item:{value}"))).IsTrue();
        await Assert.That(Array.Empty<int>().EnumeratePerformAction(null, null, _ => { })).IsFalse();
        await Assert.That(source.CountAtLeast(2)).IsTrue();
        await Assert.That(source.CountAtMost(3)).IsTrue();
        await Assert.That(source.CountAtLeast(static value => value > 1, 1)).IsTrue();
        await Assert.That(source.CountAtMost(static value => value > 1, 3)).IsTrue();
        await Assert.That(new string?[] { "a", null }.NotNullCount()).IsEqualTo(1);
        await Assert.That(new string?[] { "a", null }.NullCount()).IsEqualTo(1);
        await Assert.That(source.WhereNot(static value => value == 2).SequenceEqual([1, 3])).IsTrue();
        await Assert.That(2.Apply(static value => value * 3)).IsEqualTo(6);
        var applied = new List<int>().Apply(list => list.Add(1));
        await Assert.That(applied.SequenceEqual([1])).IsTrue();
        await Assert.That(grouped.ToListDictionary()[ "a" ].Single()).IsEqualTo("a");
        await Assert.That(MoreLinqExtensions.Zip(source, ["a", "b"]).SequenceEqual([(1, "a"), (2, "b")])).IsTrue();

        IList<BaseItem> baseItems = [new DerivedItem("a"), new DerivedItem("b")];
        var upcast = baseItems.UpcastList<BaseItem, DerivedItem>();
        await Assert.That(upcast.IsReadOnly).IsFalse();
        upcast.Add(new DerivedItem("c"));
        upcast.Insert(1, new DerivedItem("inserted"));
        var copied = new DerivedItem[4];
        upcast.CopyTo(copied, 0);

        await Assert.That(upcast.Count).IsEqualTo(4);
        await Assert.That(upcast[1].Name).IsEqualTo("inserted");
        IReadOnlyList<DerivedItem> upcastReadOnlyView = (IReadOnlyList<DerivedItem>)upcast;
        await Assert.That(upcastReadOnlyView[1].Name).IsEqualTo("inserted");
        await Assert.That(upcast.Contains((DerivedItem)baseItems[0])).IsTrue();
        await Assert.That(upcast.IndexOf((DerivedItem)baseItems[0])).IsEqualTo(0);
        await Assert.That(copied[2].Name).IsEqualTo("b");
        await Assert.That(((IEnumerable)upcast).GetEnumerator().MoveNext()).IsTrue();
        await Assert.That(upcast.Remove((DerivedItem)baseItems[0])).IsTrue();
        upcast[0] = new DerivedItem("replaced");
        upcast.RemoveAt(0);
        await Assert.That(upcast.Select(static item => item.Name).SequenceEqual(["b", "c"])).IsTrue();
        upcast.Clear();
        await Assert.That(upcast.Count).IsEqualTo(0);

        IReadOnlyList<BaseItem> readOnlyBaseItems =
        [
            new DerivedItem("b"),
            new DerivedItem("c"),
        ];
        var readOnlyUpcast = readOnlyBaseItems.UpcastReadOnlyList<BaseItem, DerivedItem>();
        await Assert.That(readOnlyUpcast[0].Name).IsEqualTo("b");
        await Assert.That(readOnlyUpcast.Count).IsEqualTo(2);
        await Assert.That(readOnlyUpcast.Select(static item => item.Name).SequenceEqual(["b", "c"])).IsTrue();
        await Assert.That(((IEnumerable)readOnlyUpcast).GetEnumerator().MoveNext()).IsTrue();
    }

    [Test]
    public async Task AdditionalEnumerableExtensionHelpersCoverageTest()
    {
        IEnumerable<int> source = [1, 2, 3];
        var cached = source.WithCountCaching();
        var singleValueEnumerator = SingleValueEnumeratorExtensions.EnumerateSingle(5);
        var list = singleValueEnumerator.ToList(resetEnumerator: true);

        await Assert.That(cached.GetNonEnumeratedCountOrDefault()).IsEqualTo(0);
        await Assert.That(cached.EqualsNonEnumeratedCount([4, 5, 6])).IsFalse();
        await Assert.That(cached.ForceCount()).IsEqualTo(3);
        await Assert.That(source.CountAtLeast(2)).IsTrue();
        await Assert.That(source.CountAtMost(3)).IsTrue();
        await Assert.That(source.CountAtLeast(static value => value > 1, 1)).IsTrue();
        await Assert.That(source.CountAtMost(static value => value > 1, 2)).IsTrue();
        await Assert.That(list.Single()).IsEqualTo(5);
    }

    private record BaseItem(string Name);
    private sealed record DerivedItem(string Name) : BaseItem(Name);

    private static IEnumerable YieldObjects(params object[] values)
    {
        foreach (var value in values)
            yield return value;
    }
}
