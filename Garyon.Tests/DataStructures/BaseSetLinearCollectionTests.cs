using Garyon.DataStructures;
using Garyon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.DataStructures;

public abstract class BaseSetLinearCollectionTests
{
    protected static readonly int[] SampleNumbers = { 1, 2, 4, 5, 7 };

    [Test]
    public async Task ContainsTest()
    {
        var instance = CreateSampleInstance();
        for (int i = 0; i < 10; i++)
            await Assert.That(instance.Contains(i)).IsEqualTo(SampleNumbers.Contains(i));
    }
    [Test]
    public async Task AddTest()
    {
        var instance = InitializeInstance<int>();
        await Assert.That(instance.Add(2)).IsTrue();
        await Assert.That(instance.Add(2)).IsFalse();
        await Assert.That(instance.Count).IsEqualTo(1);

        instance.AddRange(new[] { 1, 2, 2, 2, 4 });
        await Assert.That(instance.Count).IsEqualTo(3);
        await Assert.That(instance.Contains(1)).IsTrue();
        await Assert.That(instance.Contains(4)).IsTrue();
    }
    [Test]
    public async Task RemoveTest()
    {
        var instance = CreateSampleInstance();

        await AssertRemove(instance, 0);
        await AssertRemove(instance, 1);

        var removedRange = instance.RemoveRange(instance.Count + 5).ToArray();
        await Assert.That(removedRange.Length).IsEqualTo(SampleNumbers.Length - 2);

        async Task AssertRemove(BaseSetLinearCollection<int> instance, int index)
        {
            int removed = instance.Remove();
            await Assert.That(removed).IsEqualTo(SampleNumbers[ProjectToCollectionIndex(index)]);
            await Assert.That(instance.Count).IsEqualTo(SampleNumbers.Length - index - 1);
        }
    }
    [Test]
    public async Task PeekTest()
    {
        var instance = CreateSampleInstance();
        await Assert.That(instance.Peek()).IsEqualTo(SampleNumbers[ProjectToCollectionIndex(0)]);
    }
    [Test]
    public async Task ClearTest()
    {
        var instance = CreateSampleInstance();
        instance.Clear();
        await Assert.That(instance.IsEmpty).IsTrue();
        await Assert.That(instance).IsEmpty();
    }

    [Test]
    public async Task CopyToTest()
    {
        var instance = CreateSampleInstance();
        var array = new int[SampleNumbers.Length];
        instance.CopyTo(array, 0);
        await Assert.That(array).IsEquivalentTo(TransformForExpectedEnumerationOrder(SampleNumbers));
    }
    [Test]
    public async Task EnumerationTest()
    {
        await Assert.That(CreateSampleInstance()).IsEquivalentTo(TransformForExpectedEnumerationOrder(SampleNumbers));
    }

    protected abstract Index ProjectToCollectionIndex(Index index);
    protected abstract IEnumerable<T> TransformForExpectedEnumerationOrder<T>(IEnumerable<T> enumerable);

    protected abstract BaseSetLinearCollection<T> InitializeInstance<T>();

    private BaseSetLinearCollection<int> CreateSampleInstance()
    {
        var instance = InitializeInstance<int>();
        instance.AddRange(SampleNumbers);
        return instance;
    }
}