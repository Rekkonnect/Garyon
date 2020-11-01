using Garyon.DataStructures;
using NUnit.Framework;
using System.Collections.Generic;

namespace Garyon.Tests.DataStructures
{
    [Parallelizable(ParallelScope.Children)]
    public class ValueCounterDictionaryTests
    {
        private static ValueCounterDictionary<char> testDictionary = new ValueCounterDictionary<char>();

        [OneTimeSetUp]
        public static void InitializeTestDictionary()
        {
            for (int i = 0; i < 5; i++)
                testDictionary.Add((char)('a' + i));
        }

        [Test]
        public void EnumerableInitializationTest()
        {
            var d = new ValueCounterDictionary<char>("CHARACTER COUNTER TEST");
            
            Assert.AreEqual(2, d[' ']);
            Assert.AreEqual(4, d['T']);
            Assert.AreEqual(11, d.Count);
        }
        [Test]
        public void AddTest()
        {
            var d = new ValueCounterDictionary<char>(testDictionary);
            d.Add('a', 4);
            Assert.AreEqual(5, d['a']);
        }
        [Test]
        public void AdjustCountersTest()
        {
            var d = new ValueCounterDictionary<char>(testDictionary);
            d.Add('a', 4);
            d.AdjustCounters('a', 'b', 3);
            Assert.AreEqual(2, d['a']);
            Assert.AreEqual(4, d['b']);
        }
    }
}
