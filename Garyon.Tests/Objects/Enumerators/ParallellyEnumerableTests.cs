using Garyon.Extensions.ArrayExtensions;
using Garyon.Objects.Enumerators;
using NUnit.Framework;
using System.Linq;

namespace Garyon.Tests.Objects.Enumerators
{
    public class ParallellyEnumerableTests
    {
        [Test]
        public void Enumerate2Test()
        {
            int[] first = { 2, 4, 7, 8, 12 };
            string[] second = { "a", "c", "f" };

            (int, string)[] expectedIntString =
            {
                (2, "a"),
                (4, "c"),
                (7, "f"),
                (8, null),
                (12, null),
            };
            (string, int)[] expectedStringInt = expectedIntString.Select(a => (a.Item2, a.Item1)).ToArray();

            Assert.AreEqual(expectedIntString, new ParallellyEnumerable<int, string>(first, second));
            Assert.AreEqual(expectedStringInt, new ParallellyEnumerable<string, int>(second, first));
        }

        [Test]
        public void Enumerate3Test()
        {
            int[] first = { 2, 4, 7, 8, 12 };
            string[] second = { "a", "c", "f" };
            double[] third = { 4.3, 3.1, -0.3, 7 };

            (int, string, double)[] expectedIntStringDouble =
            {
                (2, "a", 4.3),
                (4, "c", 3.1),
                (7, "f", -0.3),
                (8, null, 7),
                (12, null, default),
            };
            (int, double, string)[] expectedIntDoubleString = expectedIntStringDouble.Select(a => (a.Item1, a.Item3, a.Item2)).ToArray();
            (string, int, double)[] expectedStringIntDouble = expectedIntStringDouble.Select(a => (a.Item2, a.Item1, a.Item3)).ToArray();
            (string, double, int)[] expectedStringDoubleInt = expectedIntStringDouble.Select(a => (a.Item2, a.Item3, a.Item1)).ToArray();
            (double, int, string)[] expectedDoubleIntString = expectedIntStringDouble.Select(a => (a.Item3, a.Item1, a.Item2)).ToArray();
            (double, string, int)[] expectedDoubleStringInt = expectedIntStringDouble.Select(a => (a.Item3, a.Item2, a.Item1)).ToArray();

            Assert.AreEqual(expectedIntStringDouble, new ParallellyEnumerable<int, string, double>(first, second, third));
            Assert.AreEqual(expectedIntDoubleString, new ParallellyEnumerable<int, double, string>(first, third, second));
            Assert.AreEqual(expectedStringIntDouble, new ParallellyEnumerable<string, int, double>(second, first, third));
            Assert.AreEqual(expectedStringDoubleInt, new ParallellyEnumerable<string, double, int>(second, third, first));
            Assert.AreEqual(expectedDoubleIntString, new ParallellyEnumerable<double, int, string>(third, first, second));
            Assert.AreEqual(expectedDoubleStringInt, new ParallellyEnumerable<double, string, int>(third, second, first));
        }
        [Test]
        public void Enumerate4Test()
        {
            int[] first = { 2, 4, 7, 8, 12 };
            string[] second = { "a", "c", "f" };
            double[] third = { 4.3, 3.1, -0.3, 7 };
            bool[] fourth = { true, true, false };

            (int, string, double, bool)[] expectedIntStringDoubleBool =
            {
                (2, "a", 4.3, true),
                (4, "c", 3.1, true),
                (7, "f", -0.3, false),
                (8, null, 7, default),
                (12, null, default, default),
            };

            Assert.AreEqual(expectedIntStringDoubleBool, new ParallellyEnumerable<int, string, double, bool>(first, second, third, fourth));
        }
    }
}
