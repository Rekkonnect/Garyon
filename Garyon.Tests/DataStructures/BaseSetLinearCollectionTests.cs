using Garyon.DataStructures;
using Garyon.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Tests.DataStructures;

[Parallelizable(ParallelScope.Children)]
public abstract class BaseSetLinearCollectionTests
{
    protected static readonly int[] SampleNumbers = { 1, 2, 4, 5, 7 };

    [Test]
    public void ContainsTest()
    {
        var instance = CreateSampleInstance();
        for (int i = 0; i < 10; i++)
            Assert.AreEqual(SampleNumbers.Contains(i), instance.Contains(i));
    }
    [Test]
    public void AddTest()
    {
        var instance = InitializeInstance<int>();
        Assert.IsTrue(instance.Add(2));
        Assert.IsFalse(instance.Add(2));
        Assert.AreEqual(1, instance.Count);

        instance.AddRange(new[] { 1, 2, 2, 2, 4 });
        Assert.AreEqual(3, instance.Count);
        Assert.IsTrue(instance.Contains(1));
        Assert.IsTrue(instance.Contains(4));
    }
    [Test]
    public void RemoveTest()
    {
        var instance = CreateSampleInstance();

        AssertRemove(instance, 0);
        AssertRemove(instance, 1);

        var removedRange = instance.RemoveRange(instance.Count + 5).ToArray();
        Assert.AreEqual(SampleNumbers.Length - 2, removedRange.Length);

        void AssertRemove(BaseSetLinearCollection<int> instance, int index)
        {
            int removed = instance.Remove();
            Assert.AreEqual(SampleNumbers[ProjectToCollectionIndex(index)], removed);
            Assert.AreEqual(SampleNumbers.Length - index - 1, instance.Count);
        }
    }
    [Test]
    public void PeekTest()
    {
        var instance = CreateSampleInstance();
        Assert.AreEqual(SampleNumbers[ProjectToCollectionIndex(0)], instance.Peek());
    }
    [Test]
    public void ClearTest()
    {
        var instance = CreateSampleInstance();
        instance.Clear();
        Assert.IsTrue(instance.IsEmpty);
        Assert.AreEqual(0, instance.Count);
    }

    [Test]
    public void CopyToTest()
    {
        var instance = CreateSampleInstance();
        var array = new int[SampleNumbers.Length];
        instance.CopyTo(array, 0);
        Assert.That(array, Is.EquivalentTo(TransformForExpectedEnumerationOrder(SampleNumbers)));
    }
    [Test]
    public void EnumerationTest()
    {
        Assert.That(CreateSampleInstance(), Is.EquivalentTo(TransformForExpectedEnumerationOrder(SampleNumbers)));
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
