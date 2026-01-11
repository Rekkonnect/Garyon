using Garyon.Extensions;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class DayOfWeekExtensionsTests
{
    [Test]
    public async Task DaysSinceFirst()
    {
        /*
         * Cheat sheet:
         * Su Mo Tu We Th Fr Sa
         *  0  1  2  3  4  5  6
         */

        await AssertOffset(DayOfWeek.Monday, DayOfWeek.Sunday, 1);
        await AssertOffset(DayOfWeek.Friday, DayOfWeek.Tuesday, 3);
        await AssertOffset(DayOfWeek.Monday, DayOfWeek.Wednesday, 5);

        static async Task AssertOffset(DayOfWeek dayOfWeek, DayOfWeek start, int expectedOffset)
        {
            await Assert.That(dayOfWeek.DaysSinceWeekStart(start)).IsEqualTo(expectedOffset);
        }
    }
}