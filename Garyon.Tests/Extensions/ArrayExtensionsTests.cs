using Garyon.Extensions.ArrayExtensions;
using NUnit.Framework;

namespace Garyon.Tests.Extensions
{
    public class ArrayExtensionsTests
    {
        [Test]
        public void ShowRangesTests()
        {
            var values = new[] { 1, 2, 3, 6, 7, 9, 12, 15, 16, 17, 18, 21, 22 };
            var expectedRange = "1-3, 6-7, 9, 12, 15-18, 21-22";
            Assert.AreEqual(expectedRange, values.ShowValuesWithRanges());

            values = new[] { 1, 2, 3, 6, 7, 9, 12, 15, 16, 17, 18, 21, 22, 23 };
            expectedRange = "1-3, 6-7, 9, 12, 15-18, 21-23";
            Assert.AreEqual(expectedRange, values.ShowValuesWithRanges());

            values = new[] { 1, 2, 3, 6, 7, 9, 12, 15, 16, 17, 18, 21 };
            expectedRange = "1-3, 6-7, 9, 12, 15-18, 21";
            Assert.AreEqual(expectedRange, values.ShowValuesWithRanges());
        }
    }
}
