using Garyon.Objects.Enumerators;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Tests.Objects.Enumerators;

public class SingleOrEnumerableTests
{
    [Test]
    public void TestSingle()
    {
        const int value = 3;
        var instance = new SingleOrEnumerable<int>(value);
        TestSingle(value, instance);
    }
    [Test]
    public void TestEnumerable()
    {
        int[] enumerable = { 1, 2, 5, 67, 348 };
        var instance = new SingleOrEnumerable<int>(enumerable);
        TestEnumerable(enumerable, instance);
    }

    [Test]
    public void TestAssign()
    {
        const int value = 3;
        int[] enumerable = { 1, 2, 5, 67, 348 };

        var instance = new SingleOrEnumerable<int>(value);
        TestSingle(value, instance);

        instance.Assign(enumerable);
        TestEnumerable(enumerable, instance);

        instance.Assign(value);
        TestSingle(value, instance);
    }

    private void TestSingle<T>(T value, SingleOrEnumerable<T> instance)
    {
        Assert.Catch(() => _ = instance.Enumerable);
        Assert.AreEqual(value, instance.Single);

        var enumeratedArray = instance.ToArray();
        CollectionAssert.AreEqual(new[] { value }, enumeratedArray);
    }
    private void TestEnumerable<T>(IEnumerable<T> enumerable, SingleOrEnumerable<T> instance)
    {
        Assert.Catch(() => _ = instance.Single);
        CollectionAssert.AreEqual(enumerable, instance.Enumerable);

        var enumeratedArray = instance.ToArray();
        CollectionAssert.AreEqual(enumerable, enumeratedArray);
    }
}
