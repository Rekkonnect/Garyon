using Garyon.Extensions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Tests.Extensions
{
    public class IEnumerableExtensionsTests
    {
        [Test]
        public void FlattenTest()
        {
            var numbers = new List<List<int>>
            {
                new List<int> { 0, 1, 2, 3 },
                new List<int> { 4, 5 },
                new List<int> { 6 },
                new List<int> { 7, 8 },
                new List<int> { 9, 10, 11, 12 },
            };
            int expectedLength = 0;
            foreach (var list in numbers)
                expectedLength += list.Count;

            var flattened = numbers.Flatten().ToArray();
            Assert.AreEqual(expectedLength, flattened.Length);

            for (int i = 0; i < expectedLength; i++)
                Assert.AreEqual(i, flattened[i]);
        }
    }
}
