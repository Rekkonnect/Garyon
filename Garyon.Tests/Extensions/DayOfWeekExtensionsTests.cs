using Garyon.Extensions;
using NUnit.Framework;
using System;

namespace Garyon.Tests.Extensions;

[Parallelizable(ParallelScope.Children)]
public class DayOfWeekExtensionsTests
{
    [Test]
    public void DaysSinceFirst()
    {
        /*
         * Cheat sheet:
         * Su Mo Tu We Th Fr Sa
         *  0  1  2  3  4  5  6
         */

        AssertOffset(DayOfWeek.Monday, DayOfWeek.Sunday, 1);
        AssertOffset(DayOfWeek.Friday, DayOfWeek.Tuesday, 3);
        AssertOffset(DayOfWeek.Monday, DayOfWeek.Wednesday, 5);

        static void AssertOffset(DayOfWeek dayOfWeek, DayOfWeek start, int expectedOffset)
        {
            Assert.AreEqual(expectedOffset, dayOfWeek.DaysSinceWeekStart(start));
        }
    }
}
