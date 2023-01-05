using Garyon.Extensions;
using Garyon.Objects.Enumerators;
using NUnit.Framework;

namespace Garyon.Tests.Objects.Enumerators;

public class IndexedEnumerableTests
{
    [Test]
    public void EnumerationTest()
    {
        var ar = new[] { 1, 2, 3 };

        int index = 0;
        foreach (var i in ar.WithIndex())
        {
            Assert.AreEqual(new IndexedEnumeratorResult<int>(index, ar[index]), i);
            index++;
        }
    }
}
