using Garyon.DataStructures;
using NUnit.Framework;
using System.Collections.Generic;

namespace Garyon.Tests.DataStructures
{
    [Parallelizable(ParallelScope.Children)]
    [Ignore("Covariant return types that were introduced in C# 9.0 break NUnit on OneTimeSetUp")]
    public class FlexibleListDictionaryTests
    {
        private static FlexibleListDictionary<int, char> testDictionary = new FlexibleListDictionary<int, char>();

        [OneTimeSetUp]
        public static void InitializeTestDictionary()
        {
            for (int i = 0; i < 5; i++)
                testDictionary[i].Add((char)('a' + i));
        }

        [Test]
        public void InitializationTest()
        {
            var d = new FlexibleListDictionary<int, string>
            {
                [1] = new() { "a" }
            };
            Assert.AreEqual("a", d[1][0]);
            Assert.AreEqual(1, d[1].Count);
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
                { "a", new() { 2 } },
                { "b", new() { 4 } },
                { "c", new() { 54 } },
                { "d", new() { 123, 3, 5 } },
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
