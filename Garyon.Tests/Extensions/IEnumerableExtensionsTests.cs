using Garyon.Extensions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Tests.Extensions;

public class IEnumerableExtensionsTests
{
    [Test]
    public void FlattenTest()
    {
        var numbers = new List<List<int>>
        {
            new List<int> { 0, 1, 2, 3 },
            new List<int> { 4, 5 },
            new List<int>(0),
            new List<int> { 6 },
            new List<int> { 7, 8 },
            new List<int> { 9, 10, 11, 12 },
        };

        var flattened = numbers.Flatten().ToArray();
        var flattenedSelectMany = numbers.SelectMany(a => a).ToArray();
        Assert.AreEqual(flattenedSelectMany, flattened);
    }
}
