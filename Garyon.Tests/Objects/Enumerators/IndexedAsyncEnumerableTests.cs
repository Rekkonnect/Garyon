using Garyon.Extensions;
using Garyon.Objects.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Objects.Enumerators;

public class IndexedAsyncEnumerableTests
{
    [Test]
    public async Task EnumerationTest()
    {
        int index = 0;

        Task.WaitAll(Enumerate());

        async Task Enumerate()
        {
            await foreach (var i in GetEnumerable().WithIndex())
            {
                await Assert.That(i).IsEqualTo(new IndexedEnumeratorResult<int>(index, index + 1));
                index++;
            }
        }
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private static async IAsyncEnumerable<int> GetEnumerable()
    {
        for (int i = 1; i <= 3; i++)
            yield return i;
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
}