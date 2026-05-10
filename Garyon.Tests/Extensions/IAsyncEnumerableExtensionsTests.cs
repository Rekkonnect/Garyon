using Garyon.Extensions;
using Garyon.Objects.Enumerators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class IAsyncEnumerableExtensionsTests
{
    [Test]
    public async Task FlattenAsyncFromAsyncEnumerableOfEnumerablesTest()
    {
        IAsyncEnumerable<IEnumerable<int>> source = GetAsyncEnumerable(
        [
            new[] { 1, 2 },
            [],
            [3, 4, 5],
        ]);

        var flattened = await source.FlattenAsync().ToListAsync();

        await Assert.That(flattened.SequenceEqual([1, 2, 3, 4, 5])).IsTrue();
    }

    [Test]
    public async Task FlattenAsyncFromEnumerableOfAsyncEnumerablesTest()
    {
        var source = new[]
        {
            GetAsyncEnumerable([1, 2]),
            GetAsyncEnumerable(System.Array.Empty<int>()),
            GetAsyncEnumerable([3, 4, 5]),
        };

        var flattened = await source.FlattenAsync().ToListAsync();

        await Assert.That(flattened.SequenceEqual([1, 2, 3, 4, 5])).IsTrue();
    }

    [Test]
    public async Task FlattenAsyncFromAsyncEnumerableOfAsyncEnumerablesTest()
    {
        var source = GetAsyncEnumerable(
        [
            GetAsyncEnumerable([1, 2]),
            GetAsyncEnumerable(System.Array.Empty<int>()),
            GetAsyncEnumerable([3, 4, 5]),
        ]);

        var flattened = await source.FlattenAsync().ToListAsync();

        await Assert.That(flattened.SequenceEqual([1, 2, 3, 4, 5])).IsTrue();
    }

    [Test]
    public async Task WithIndexTest()
    {
        var results = await GetAsyncEnumerable([10, 20, 30]).WithIndex().ToListAsync();

        await Assert.That(results.Count).IsEqualTo(3);
        await Assert.That(results[0]).IsEqualTo(new IndexedEnumeratorResult<int>(0, 10));
        await Assert.That(results[1]).IsEqualTo(new IndexedEnumeratorResult<int>(1, 20));
        await Assert.That(results[2]).IsEqualTo(new IndexedEnumeratorResult<int>(2, 30));
    }

    [Test]
    public async Task ForEachAsyncActionTest()
    {
        var values = new List<int>();

        await GetAsyncEnumerable([1, 2, 3]).ForEachAsync(values.Add);

        await Assert.That(values.SequenceEqual([1, 2, 3])).IsTrue();
    }

    [Test]
    public async Task ForEachAsyncIndexedActionTest()
    {
        var values = new List<IndexedEnumeratorResult<int>>();

        await GetAsyncEnumerable([10, 20, 30]).ForEachAsync((index, value) => values.Add(new IndexedEnumeratorResult<int>(index, value)));

        await Assert.That(values.Count).IsEqualTo(3);
        await Assert.That(values[0]).IsEqualTo(new IndexedEnumeratorResult<int>(0, 10));
        await Assert.That(values[1]).IsEqualTo(new IndexedEnumeratorResult<int>(1, 20));
        await Assert.That(values[2]).IsEqualTo(new IndexedEnumeratorResult<int>(2, 30));
    }

    [Test]
    public async Task ToListAsyncFromAsyncEnumerableTest()
    {
        var list = await GetAsyncEnumerable([4, 5, 6]).ToListAsync();

        await Assert.That(list.SequenceEqual([4, 5, 6])).IsTrue();
    }

    [Test]
    public async Task ToListAsyncFromAsyncEnumerableOfEnumerablesTest()
    {
        IAsyncEnumerable<IEnumerable<int>> source = GetAsyncEnumerable(
        [
            new[] { 1, 2 },
            [],
            [3, 4],
        ]);

        var list = await source.ToListAsync();

        await Assert.That(list.SequenceEqual([1, 2, 3, 4])).IsTrue();
    }

    private static async IAsyncEnumerable<T> GetAsyncEnumerable<T>(IEnumerable<T> source)
    {
        foreach (var item in source)
        {
            await Task.Yield();
            yield return item;
        }
    }
}
