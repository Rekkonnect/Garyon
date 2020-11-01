using Garyon.DataStructures;
using NUnit.Framework;

namespace Garyon.Tests.DataStructures
{
    [Parallelizable(ParallelScope.Children)]
    public class FlexibleDictionaryTests
    {
        private static FlexibleDictionary<int, char> testDictionary = new FlexibleDictionary<int, char>();

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
            var d = new FlexibleDictionary<int, string>(values);
            foreach (var v in uniqueValues)
                Assert.AreEqual(default(string), d[v]);
            Assert.AreEqual(4, d.Count);
        }
        [Test]
        public void CloneTest()
        {
            var cloned = testDictionary.Clone();
            for (int i = 0; i < 5; i++)
                Assert.AreEqual((char)('a' + i), cloned[i]);
            Assert.AreEqual(testDictionary.Count, cloned.Count);

            cloned.Add(10, 'f');
            Assert.IsFalse(testDictionary.ContainsKey(10));
            Assert.IsTrue(cloned.ContainsKey(10));
        }
        [Test]
        public void ClearTest()
        {
            var cloned = testDictionary.Clone();
            cloned.Clear();
            foreach (var kvp in testDictionary)
                Assert.IsFalse(cloned.ContainsKey(kvp.Key));
            Assert.AreEqual(0, cloned.Count);
        }
        [Test]
        public void RemoveTest()
        {
            var cloned = testDictionary.Clone();
            cloned.Remove(1);
            foreach (var kvp in testDictionary)
                Assert.AreEqual(kvp.Key != 1, cloned.ContainsKey(kvp.Key));
            Assert.AreEqual(testDictionary.Count - 1, cloned.Count);
        }
        [Test]
        public void RemoveKeysTest()
        {
            var cloned = testDictionary.Clone();
            int removed = cloned.RemoveKeys(1, 2, 43);

            Assert.AreEqual(2, removed);
            Assert.IsTrue(cloned.ContainsKey(0));
            Assert.IsFalse(cloned.ContainsKey(1));
            Assert.IsFalse(cloned.ContainsKey(2));
            Assert.IsTrue(cloned.ContainsKey(3));
            Assert.IsTrue(cloned.ContainsKey(4));
            Assert.AreEqual(testDictionary.Count - removed, cloned.Count);
        }

        [Test]
        public void TryGetValueTest()
        {
            var d = new FlexibleDictionary<string, int>
            {
                ["a"] = 2,
            };

            bool found = d.TryGetValue("fsad", out int value);
            Assert.IsFalse(found);
            Assert.AreEqual(0, value);

            found = d.TryGetValue("a", out value);
            Assert.IsTrue(found);
            Assert.AreEqual(2, value);
        }

        [Test]
        public void AccessorTest()
        {
            var d = new FlexibleDictionary<string, int>();
            int value = d[""];
            Assert.AreEqual(0, value);
            d["a"] = 5;
            Assert.AreEqual(5, d["a"]);
            d[""] = 3;
            Assert.AreEqual(3, d[""]);
        }
    }
}
