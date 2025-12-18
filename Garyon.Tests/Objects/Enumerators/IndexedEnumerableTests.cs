using Garyon.Extensions;
using Garyon.Objects.Enumerators;
using System.Threading.Tasks;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;

namespace Garyon.Tests.Objects.Enumerators;

public class IndexedEnumerableTests
{
    [Test]
    public async Task EnumerationTest()
    {
        var ar = new[] { 1, 2, 3 };

        int index = 0;
        foreach (var i in ar.WithIndex())
        {
            await Assert.That(i).IsEqualTo(new IndexedEnumeratorResult<int>(index, ar[index]));
            index++;
        }
    }
}