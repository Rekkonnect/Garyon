using Garyon.Mathematics;
using NUnit.Framework;

namespace Garyon.Tests.Mathematics
{
    [Parallelizable(ParallelScope.Children)]
    public class OverflowingTests
    {
        [Test]
        public void CheckIfAdditionOverflowsInt32Test()
        {
            const int max = int.MaxValue;
            const int min = int.MinValue;

            AssertAdditionOverflow(true, max, 1);
            AssertAdditionOverflow(true, min, -1);

            AssertAdditionOverflow(false, max, 0);
            AssertAdditionOverflow(false, min, 0);

            AssertAdditionOverflow(false, max, -1);
            AssertAdditionOverflow(false, min, 1);

            AssertAdditionOverflow(false, max / 2, max / 2);
            AssertAdditionOverflow(false, max / 2, max / 2 + 1);
            AssertAdditionOverflow(true, max / 2 + 1, max / 2 + 1);
            AssertAdditionOverflow(false, min / 2, min / 2);
            AssertAdditionOverflow(true, min / 2, min / 2 - 1);
            AssertAdditionOverflow(true, min / 2 - 1, min / 2 - 1);
        }
        [Test]
        public void CheckIfAdditionOverflowsUInt32Test()
        {
            const uint max = uint.MaxValue;

            AssertAdditionOverflow(true, max, 1);

            AssertAdditionOverflow(false, max, 0);

            AssertAdditionOverflow(false, max / 2, max / 2);
            AssertAdditionOverflow(false, max / 2, max / 2 + 1);
            AssertAdditionOverflow(true, max / 2 + 1, max / 2 + 1);
        }
        [Test]
        public void CheckIfAdditionOverflowsInt64Test()
        {
            const long max = long.MaxValue;
            const long min = long.MinValue;

            AssertAdditionOverflow(true, max, 1);
            AssertAdditionOverflow(true, min, -1);

            AssertAdditionOverflow(false, max, 0);
            AssertAdditionOverflow(false, min, 0);

            AssertAdditionOverflow(false, max, -1);
            AssertAdditionOverflow(false, min, 1);

            AssertAdditionOverflow(false, max / 2, max / 2);
            AssertAdditionOverflow(false, max / 2, max / 2 + 1);
            AssertAdditionOverflow(true, max / 2 + 1, max / 2 + 1);
            AssertAdditionOverflow(false, min / 2, min / 2);
            AssertAdditionOverflow(true, min / 2, min / 2 - 1);
            AssertAdditionOverflow(true, min / 2 - 1, min / 2 - 1);
        }
        [Test]
        public void CheckIfAdditionOverflowsUInt64Test()
        {
            const ulong max = ulong.MaxValue;

            AssertAdditionOverflow(true, max, 1);

            AssertAdditionOverflow(false, max, 0);

            AssertAdditionOverflow(false, max / 2, max / 2);
            AssertAdditionOverflow(false, max / 2, max / 2 + 1);
            AssertAdditionOverflow(true, max / 2 + 1, max / 2 + 1);
        }
        [Test]
        public void CheckIfMultiplicationOverflowsInt32Test()
        {
            const int max = int.MaxValue;
            const int min = int.MinValue;

            AssertMultiplicationOverflow(false, max, 1);
            AssertMultiplicationOverflow(false, max, -1);
            AssertMultiplicationOverflow(false, min, 1);
            AssertMultiplicationOverflow(true, min, -1);

            AssertMultiplicationOverflow(false, max, 0);
            AssertMultiplicationOverflow(false, min, 0);

            AssertMultiplicationOverflow(false, max / 2, 2);
            AssertMultiplicationOverflow(true, max / 2 + 1, 2);
            AssertMultiplicationOverflow(false, min / 2, 2);
            AssertMultiplicationOverflow(true, min / 2 - 1, 2);

            AssertMultiplicationOverflow(false, max / 2, -2);
            AssertMultiplicationOverflow(false, max / 2 + 1, -2);
            AssertMultiplicationOverflow(true, min / 2, -2);
            AssertMultiplicationOverflow(true, min / 2 - 1, -2);
        }
        [Test]
        public void CheckIfMultiplicationOverflowsUInt32Test()
        {
            const uint max = uint.MaxValue;

            AssertMultiplicationOverflow(false, max, 1);

            AssertMultiplicationOverflow(false, max, 0);

            AssertMultiplicationOverflow(false, max / 2, 2);
            AssertMultiplicationOverflow(true, max / 2 + 1, 2);
        }
        [Test]
        public void CheckIfMultiplicationOverflowsInt64Test()
        {
            const long max = long.MaxValue;
            const long min = long.MinValue;

            AssertMultiplicationOverflow(false, max, 1);
            AssertMultiplicationOverflow(false, max, -1);
            AssertMultiplicationOverflow(false, min, 1);
            AssertMultiplicationOverflow(true, min, -1);

            AssertMultiplicationOverflow(false, max, 0);
            AssertMultiplicationOverflow(false, min, 0);

            AssertMultiplicationOverflow(false, max / 2, 2);
            AssertMultiplicationOverflow(true, max / 2 + 1, 2);
            AssertMultiplicationOverflow(false, min / 2, 2);
            AssertMultiplicationOverflow(true, min / 2 - 1, 2);

            AssertMultiplicationOverflow(false, max / 2, -2);
            AssertMultiplicationOverflow(false, max / 2 + 1, -2);
            AssertMultiplicationOverflow(true, min / 2, -2);
            AssertMultiplicationOverflow(true, min / 2 - 1, -2);
        }
        [Test]
        public void CheckIfMultiplicationOverflowsUInt64Test()
        {
            const ulong max = ulong.MaxValue;

            AssertMultiplicationOverflow(false, max, 1);

            AssertMultiplicationOverflow(false, max, 0);

            AssertMultiplicationOverflow(false, max / 2, 2);
            AssertMultiplicationOverflow(true, max / 2 + 1, 2);
        }

        private static void AssertAdditionOverflow(bool expected, int x, int y)
        {
            Assert.AreEqual(expected, Overflowing.CheckIfAdditionOverflows(x, y));
            Assert.AreEqual(expected, Overflowing.CheckIfAdditionOverflows(y, x));
            Assert.DoesNotThrow(() => x += y);
        }
        private static void AssertAdditionOverflow(bool expected, uint x, uint y)
        {
            Assert.AreEqual(expected, Overflowing.CheckIfAdditionOverflows(x, y));
            Assert.AreEqual(expected, Overflowing.CheckIfAdditionOverflows(y, x));
            Assert.DoesNotThrow(() => x += y);
        }
        private static void AssertAdditionOverflow(bool expected, long x, long y)
        {
            Assert.AreEqual(expected, Overflowing.CheckIfAdditionOverflows(x, y));
            Assert.AreEqual(expected, Overflowing.CheckIfAdditionOverflows(y, x));
            Assert.DoesNotThrow(() => x += y);
        }
        private static void AssertAdditionOverflow(bool expected, ulong x, ulong y)
        {
            Assert.AreEqual(expected, Overflowing.CheckIfAdditionOverflows(x, y));
            Assert.AreEqual(expected, Overflowing.CheckIfAdditionOverflows(y, x));
            Assert.DoesNotThrow(() => x += y);
        }

        private static void AssertMultiplicationOverflow(bool expected, int x, int y)
        {
            Assert.AreEqual(expected, Overflowing.CheckIfMultiplicationOverflows(x, y));
            Assert.AreEqual(expected, Overflowing.CheckIfMultiplicationOverflows(y, x));
            Assert.DoesNotThrow(() => x *= y);
        }
        private static void AssertMultiplicationOverflow(bool expected, uint x, uint y)
        {
            Assert.AreEqual(expected, Overflowing.CheckIfMultiplicationOverflows(x, y));
            Assert.AreEqual(expected, Overflowing.CheckIfMultiplicationOverflows(y, x));
            Assert.DoesNotThrow(() => x *= y);
        }
        private static void AssertMultiplicationOverflow(bool expected, long x, long y)
        {
            Assert.AreEqual(expected, Overflowing.CheckIfMultiplicationOverflows(x, y));
            Assert.AreEqual(expected, Overflowing.CheckIfMultiplicationOverflows(y, x));
            Assert.DoesNotThrow(() => x *= y);
        }
        private static void AssertMultiplicationOverflow(bool expected, ulong x, ulong y)
        {
            Assert.AreEqual(expected, Overflowing.CheckIfMultiplicationOverflows(x, y));
            Assert.AreEqual(expected, Overflowing.CheckIfMultiplicationOverflows(y, x));
            Assert.DoesNotThrow(() => x *= y);
        }
    }
}
