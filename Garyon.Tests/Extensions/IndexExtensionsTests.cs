using Garyon.Extensions;
using NUnit.Framework;
using System;

namespace Garyon.Tests.Extensions
{
    [Parallelizable(ParallelScope.Children)]
    public class IndexExtensionsTests
    {
        [Test]
        public void InvertTest()
        {
            var ar = new[] { 0, 1, 2, 3, 4, 3, 2, 1, 0 };
            for (int i = 0; i < ar.Length; i++)
                Assert.AreEqual(ar[i], ar[((Index)i).Invert()]);
        }
    }
}
