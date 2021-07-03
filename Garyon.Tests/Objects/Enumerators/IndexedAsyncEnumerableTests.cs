using Garyon.Extensions;
using Garyon.Objects.Enumerators;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Garyon.Tests.Objects.Enumerators
{
    public class IndexedAsyncEnumerableTests
    {
        [Test]
        public void EnumerationTest()
        {
            int index = 0;

            Task.WaitAll(Enumerate());

            async Task Enumerate()
            {
                await foreach (var i in GetEnumerable().WithIndex())
                {
                    Assert.AreEqual(new IndexedEnumeratorResult<int>(index, index + 1), i);
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
}
