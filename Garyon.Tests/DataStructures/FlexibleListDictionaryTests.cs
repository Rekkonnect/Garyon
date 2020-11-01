using Garyon.DataStructures;
using NUnit.Framework;
using System.Collections.Generic;

namespace Garyon.Tests.DataStructures
{
    [Parallelizable(ParallelScope.Children)]
    public class FlexibleListDictionaryTests
    {
        private static FlexibleListDictionary<int, char> testDictionary = new FlexibleListDictionary<int, char>();

        [OneTimeSetUp]
        public static void InitializeTestDictionary()
        {
            for (int i = 0; i < 5; i++)
                testDictionary.Add(i, (char)('a' + i));
        }

        [Test]
        public void InitializationTest()
        {
            var d = new FlexibleDictionary<int, string>
            {
                [1] = "a"
            };
            Assert.AreEqual("a", d[1]);
            Assert.AreEqual(1, d.Count);
        }
        [Test]
        public void EnumerableInitializationTest()
        {
            var values = new[] { 1, 4, 1, 6, 2, 2 };
            var uniqueValues = new[] { 1, 2, 4, 6 };
            var d = new FlexibleListDictionary<int, string>(values);
            foreach (var v in uniqueValues)
                Assert.AreNotEqual(default(List<string>), d[v]);
            Assert.AreEqual(4, d.Count);
        }
        [Test]
        public void CloneTest()
        {
            var cloned = testDictionary.Clone();
            for (int i = 0; i < 5; i++)
                Assert.AreEqual((char)('a' + i), cloned[i][0]);
            Assert.AreEqual(testDictionary.Count, cloned.Count);

            cloned[10].Add('f');
            Assert.IsFalse(testDictionary.ContainsKey(10));
            Assert.IsTrue(cloned.ContainsKey(10));

            Assert.IsTrue(cloned.ContainsKey(1));
            cloned[1].Add('z');
            Assert.IsTrue(cloned[1].Contains('z'));
            Assert.IsFalse(testDictionary[1].Contains('z'));
        }

        [Test]
        public void TryGetValueTest()
        {
            var d = new FlexibleListDictionary<string, int>
            {
                { "a", 2 },
                { "b", 4 },
                { "c", 54 },
                { "d", new List<int> { 123, 3, 5 } },
            };

            bool found = d.TryGetValue("a", 0, out int value);
            Assert.IsTrue(found);
            Assert.AreEqual(2, value);

            found = d.TryGetValue("a", 1, out value);
            Assert.IsFalse(found);

            found = d.TryGetValue("d", 2, out value);
            Assert.IsTrue(found);
            Assert.AreEqual(5, value);

            found = d.TryGetValue("d", 3, out value);
            Assert.IsFalse(found);
        }
    }
}
