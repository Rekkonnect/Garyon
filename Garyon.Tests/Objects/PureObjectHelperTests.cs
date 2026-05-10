using Garyon.Objects;
using Garyon.Objects.Advanced;
using Garyon.Objects.Disposable;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Objects;

public class PureObjectHelperTests
{
    [Test]
    public async Task AdvancedLazyCachesClearsAndRecomputesValuesTest()
    {
        int calls = 0;
        var lazy = new AdvancedLazy<int>(() => ++calls);

        await Assert.That(lazy.IsValueCreated).IsFalse();
        await Assert.That(lazy.ValueOrDefault).IsEqualTo(0);
        await Assert.That(lazy.GetValue()).IsEqualTo(1);
        await Assert.That(calls).IsEqualTo(1);

        lazy.ClearValue();

        await Assert.That(lazy.IsValueCreated).IsFalse();
        await Assert.That(lazy.GetValue()).IsEqualTo(2);
        await Assert.That(calls).IsEqualTo(2);
    }

    [Test]
    public async Task AdvancedLazyValueConstructorStartsCreatedTest()
    {
        var lazy = new AdvancedLazy<string>("ready");

        await Assert.That(lazy.IsValueCreated).IsTrue();
        await Assert.That(lazy.GetValue()).IsEqualTo("ready");
    }

    [Test]
    public async Task MemoizationDictionariesCacheAndClearValuesTest()
    {
        int syncCalls = 0;
        var memoized = new MemoizationDictionary<int, int>(value => ++syncCalls * value, 2);

        await Assert.That(memoized.Get(3)).IsEqualTo(3);
        await Assert.That(memoized.Get(3)).IsEqualTo(3);
        await Assert.That(syncCalls).IsEqualTo(1);
        memoized.Clear();
        await Assert.That(memoized.Get(3)).IsEqualTo(6);

        int asyncCalls = 0;
        var asyncMemoized = new AsyncMemoizationDictionary<int, int>(
            value => new ValueTask<int>(++asyncCalls * value),
            1,
            2);

        await Assert.That(await asyncMemoized.Get(4)).IsEqualTo(4);
        await Assert.That(await asyncMemoized.Get(4)).IsEqualTo(4);
        await Assert.That(asyncCalls).IsEqualTo(1);
        asyncMemoized.Clear();
        await Assert.That(await asyncMemoized.Get(4)).IsEqualTo(8);
    }

    [Test]
    public async Task LabelledObjectsFormatAndUpdateLabelsTest()
    {
        var labelled = new TestLabelledObject(42);
        var variable = new VariablyLabelledObject<int>("first", 7);

        variable.VariableLabel = "second";

        await Assert.That(labelled.ToString()).IsEqualTo("fixed: 42");
        await Assert.That(variable.Label).IsEqualTo("second");
        await Assert.That(variable.ToString()).IsEqualTo("second: 7");
    }

    [Test]
    public async Task RepeatedValueCollectionEnumeratesResetsAndCopiesTest()
    {
        var collection = new RepeatedValueCollection<string>("x", 3);
        using var enumerator = collection.GetEnumerator();

        await Assert.That(collection.ToArray().SequenceEqual(["x", "x", "x"])).IsTrue();
        await Assert.That(collection.SequenceEqual(["x", "x", "x"])).IsTrue();
        await Assert.That(((IEnumerable)collection).GetEnumerator().MoveNext()).IsTrue();
        await Assert.That(enumerator.MoveNext()).IsTrue();
        await Assert.That(enumerator.Current).IsEqualTo("x");
        enumerator.Reset();
        await Assert.That(enumerator.MoveNext()).IsTrue();

        collection.Count = 0;
        await Assert.That(collection).IsEmpty();
    }

    [Test]
    public async Task IntervalBoundsExtremumAndEquatableHelpersTest()
    {
        OpenInterval<int> equalToFive = 5;
        var greaterOrEqualToFive = new OpenInterval<int>(5, ComparisonKinds.GreaterOrEqual);
        var defaultBounds = ValueBounds<int>.Default;
        var equatable = new TestEquatable(4);

        await Assert.That(equalToFive.Contains(5)).IsTrue();
        await Assert.That(equalToFive.Contains(6)).IsFalse();
        await Assert.That(greaterOrEqualToFive.Contains(6)).IsTrue();
        await Assert.That(defaultBounds.Min).IsEqualTo(0);
        await Assert.That(defaultBounds.Max).IsEqualTo(0);
        await Assert.That(Extremum.Minimum.TargetComparisonResult()).IsEqualTo(ComparisonResult.Less);
        await Assert.That(Extremum.Maximum.TargetComparisonResult()).IsEqualTo(ComparisonResult.Greater);
        await Assert.That(equatable.Equals(new TestEquatable(4))).IsTrue();
        await Assert.That(equatable.Equals(new TestEquatable(5))).IsFalse();
        await Assert.That(equatable.Equals(null)).IsFalse();
        await Assert.That(equatable.Equals((object)"not comparable")).IsFalse();
    }

    [Test]
    public async Task SingletonSharedInstanceAndTypeValueCountersTest()
    {
        var first = Singleton<SharedInstanceSample>.Instance;
        var second = Singleton<SharedInstanceSample>.Instance;
        var counters = new TypeValueCounterDictionary();

        counters.SetValue<string>(3);
        counters.Add(typeof(int), 2);

        await Assert.That(ReferenceEquals(first, second)).IsTrue();
        await Assert.That(ReferenceEquals(first, SharedInstanceSample.Shared)).IsTrue();
        await Assert.That(counters.GetValue<string>()).IsEqualTo(3);
        await Assert.That(counters.GetValue<int>()).IsEqualTo(2);
    }

    [Test]
    public void NoOpDisposableBlockCanBeDisposedTest()
    {
        using IDisposable block = new NoOpDisposableBlock();
    }

    private sealed class TestLabelledObject(int value) : LabelledObject<int>(value)
    {
        public override string Label => "fixed";
    }

    private sealed class TestEquatable(int value) : BaseEquatable<TestEquatable>
    {
        private readonly int comparableValue = value;

        protected override bool EqualsCore(TestEquatable other) => comparableValue == other.comparableValue;
    }

    private sealed class SharedInstanceSample : ISharedInstance;
}
