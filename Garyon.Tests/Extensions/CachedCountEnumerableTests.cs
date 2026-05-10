using Garyon.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class CachedCountEnumerableTests
{
    [Test]
    public async Task CachedCountEnumerableForceCountTest()
    {
        IEnumerable source = YieldUntyped(1, 2, 3);
        var cached = new CachedCountEnumerable(source);

        await Assert.That(cached.CachedCount).IsNull();
        await Assert.That(cached.MinCount).IsEqualTo(0);
        await Assert.That(cached.ForceCount()).IsEqualTo(3);
        await Assert.That(cached.CachedCount).IsEqualTo(3);

        static IEnumerable YieldUntyped(params int[] values)
        {
            foreach (var value in values)
                yield return value;
        }
    }

    [Test]
    public async Task CachedCountEnumerableTracksMinCountAndCommitsCountTest()
    {
        IEnumerable source = YieldUntyped(1, 2, 3);
        var cached = new CachedCountEnumerable(source);
        var enumerator = ((IEnumerable)cached).GetEnumerator();

        await Assert.That(enumerator.MoveNext()).IsTrue();
        await Assert.That(cached.MinCount).IsEqualTo(1);
        await Assert.That(cached.CachedCount).IsNull();

        await Assert.That(enumerator.MoveNext()).IsTrue();
        await Assert.That(cached.MinCount).IsEqualTo(2);

        await Assert.That(enumerator.MoveNext()).IsTrue();
        await Assert.That(cached.MinCount).IsEqualTo(3);

        await Assert.That(enumerator.MoveNext()).IsFalse();
        await Assert.That(cached.CachedCount).IsEqualTo(3);

        static IEnumerable YieldUntyped(params int[] values)
        {
            foreach (var value in values)
                yield return value;
        }
    }

    [Test]
    public async Task CachedCountEnumerableUsesKnownCountWithoutEnumerationTest()
    {
        IEnumerable source = new[] { 1, 2, 3 };
        var cached = new CachedCountEnumerable(source);

        await Assert.That(cached.CachedCount).IsEqualTo(3);
        await Assert.That(cached.MinCount).IsEqualTo(3);
        await Assert.That(cached.ForceCount()).IsEqualTo(3);
    }

    [Test]
    public async Task CachedCountEnumerableResetResetsTrackedCountTest()
    {
        int[] sourceArray = [1, 2];
        IEnumerable source = sourceArray;
        var cached = new CachedCountEnumerable(source);
        var enumerator = ((IEnumerable)cached).GetEnumerator();

        await Assert.That(enumerator.MoveNext()).IsTrue();
        enumerator.Reset();
        await Assert.That(enumerator.MoveNext()).IsTrue();
        await Assert.That(cached.MinCount).IsEqualTo(2);
    }

}
