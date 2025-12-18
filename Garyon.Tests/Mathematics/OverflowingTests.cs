using Garyon.Mathematics;
using System.Threading.Tasks;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;

namespace Garyon.Tests.Mathematics;

public class OverflowingTests
{
    [Test]
    public async Task CheckIfAdditionOverflowsInt32TestAsync()
    {
        const int max = int.MaxValue;
        const int min = int.MinValue;

        await AssertAdditionOverflow(true, max, 1);
        await AssertAdditionOverflow(true, min, -1);

        await AssertAdditionOverflow(false, max, 0);
        await AssertAdditionOverflow(false, min, 0);

        await AssertAdditionOverflow(false, max, -1);
        await AssertAdditionOverflow(false, min, 1);

        await AssertAdditionOverflow(false, max / 2, max / 2);
        await AssertAdditionOverflow(false, max / 2, max / 2 + 1);
        await AssertAdditionOverflow(true, max / 2 + 1, max / 2 + 1);
        await AssertAdditionOverflow(false, min / 2, min / 2);
        await AssertAdditionOverflow(true, min / 2, min / 2 - 1);
        await AssertAdditionOverflow(true, min / 2 - 1, min / 2 - 1);
    }
    [Test]
    public async Task CheckIfAdditionOverflowsUInt32TestAsync()
    {
        const uint max = uint.MaxValue;

        await AssertAdditionOverflow(true, max, 1);

        await AssertAdditionOverflow(false, max, 0);

        await AssertAdditionOverflow(false, max / 2, max / 2);
        await AssertAdditionOverflow(false, max / 2, max / 2 + 1);
        await AssertAdditionOverflow(true, max / 2 + 1, max / 2 + 1);
    }
    [Test]
    public async Task CheckIfAdditionOverflowsInt64TestAsync()
    {
        const long max = long.MaxValue;
        const long min = long.MinValue;

        await AssertAdditionOverflow(true, max, 1);
        await AssertAdditionOverflow(true, min, -1);

        await AssertAdditionOverflow(false, max, 0);
        await AssertAdditionOverflow(false, min, 0);

        await AssertAdditionOverflow(false, max, -1);
        await AssertAdditionOverflow(false, min, 1);

        await AssertAdditionOverflow(false, max / 2, max / 2);
        await AssertAdditionOverflow(false, max / 2, max / 2 + 1);
        await AssertAdditionOverflow(true, max / 2 + 1, max / 2 + 1);
        await AssertAdditionOverflow(false, min / 2, min / 2);
        await AssertAdditionOverflow(true, min / 2, min / 2 - 1);
        await AssertAdditionOverflow(true, min / 2 - 1, min / 2 - 1);
    }
    [Test]
    public async Task CheckIfAdditionOverflowsUInt64TestAsync()
    {
        const ulong max = ulong.MaxValue;

        await AssertAdditionOverflow(true, max, 1);

        await AssertAdditionOverflow(false, max, 0);

        await AssertAdditionOverflow(false, max / 2, max / 2);
        await AssertAdditionOverflow(false, max / 2, max / 2 + 1);
        await AssertAdditionOverflow(true, max / 2 + 1, max / 2 + 1);
    }
    [Test]
    public async Task CheckIfMultiplicationOverflowsInt32TestAsync()
    {
        const int max = int.MaxValue;
        const int min = int.MinValue;

        await AssertMultiplicationOverflow(false, max, 1);
        await AssertMultiplicationOverflow(false, max, -1);
        await AssertMultiplicationOverflow(false, min, 1);
        await AssertMultiplicationOverflow(true, min, -1);

        await AssertMultiplicationOverflow(false, max, 0);
        await AssertMultiplicationOverflow(false, min, 0);

        await AssertMultiplicationOverflow(false, max / 2, 2);
        await AssertMultiplicationOverflow(true, max / 2 + 1, 2);
        await AssertMultiplicationOverflow(false, min / 2, 2);
        await AssertMultiplicationOverflow(true, min / 2 - 1, 2);

        await AssertMultiplicationOverflow(false, max / 2, -2);
        await AssertMultiplicationOverflow(false, max / 2 + 1, -2);
        await AssertMultiplicationOverflow(true, min / 2, -2);
        await AssertMultiplicationOverflow(true, min / 2 - 1, -2);
    }
    [Test]
    public async Task CheckIfMultiplicationOverflowsUInt32TestAsync()
    {
        const uint max = uint.MaxValue;

        await AssertMultiplicationOverflow(false, max, 1);

        await AssertMultiplicationOverflow(false, max, 0);

        await AssertMultiplicationOverflow(false, max / 2, 2);
        await AssertMultiplicationOverflow(true, max / 2 + 1, 2);
    }
    [Test]
    public async Task CheckIfMultiplicationOverflowsInt64TestAsync()
    {
        const long max = long.MaxValue;
        const long min = long.MinValue;

        await AssertMultiplicationOverflow(false, max, 1);
        await AssertMultiplicationOverflow(false, max, -1);
        await AssertMultiplicationOverflow(false, min, 1);
        await AssertMultiplicationOverflow(true, min, -1);

        await AssertMultiplicationOverflow(false, max, 0);
        await AssertMultiplicationOverflow(false, min, 0);

        await AssertMultiplicationOverflow(false, max / 2, 2);
        await AssertMultiplicationOverflow(true, max / 2 + 1, 2);
        await AssertMultiplicationOverflow(false, min / 2, 2);
        await AssertMultiplicationOverflow(true, min / 2 - 1, 2);

        await AssertMultiplicationOverflow(false, max / 2, -2);
        await AssertMultiplicationOverflow(false, max / 2 + 1, -2);
        await AssertMultiplicationOverflow(true, min / 2, -2);
        await AssertMultiplicationOverflow(true, min / 2 - 1, -2);
    }
    [Test]
    public async Task CheckIfMultiplicationOverflowsUInt64TestAsync()
    {
        const ulong max = ulong.MaxValue;

        await AssertMultiplicationOverflow(false, max, 1);

        await AssertMultiplicationOverflow(false, max, 0);

        await AssertMultiplicationOverflow(false, max / 2, 2);
        await AssertMultiplicationOverflow(true, max / 2 + 1, 2);
    }

    private async static Task AssertAdditionOverflow(bool expected, int x, int y)
    {
        await Assert.That(Overflowing.CheckIfAdditionOverflows(x, y)).IsEqualTo(expected);
        await Assert.That(Overflowing.CheckIfAdditionOverflows(y, x)).IsEqualTo(expected);
        await Assert.That(() => x += y).ThrowsNothing();
    }
    private async static Task AssertAdditionOverflow(bool expected, uint x, uint y)
    {
        await Assert.That(Overflowing.CheckIfAdditionOverflows(x, y)).IsEqualTo(expected);
        await Assert.That(Overflowing.CheckIfAdditionOverflows(y, x)).IsEqualTo(expected);
        await Assert.That(() => x += y).ThrowsNothing();
    }
    private async static Task AssertAdditionOverflow(bool expected, long x, long y)
    {
        await Assert.That(Overflowing.CheckIfAdditionOverflows(x, y)).IsEqualTo(expected);
        await Assert.That(Overflowing.CheckIfAdditionOverflows(y, x)).IsEqualTo(expected);
        await Assert.That(() => x += y).ThrowsNothing();
    }
    private async static Task AssertAdditionOverflow(bool expected, ulong x, ulong y)
    {
        await Assert.That(Overflowing.CheckIfAdditionOverflows(x, y)).IsEqualTo(expected);
        await Assert.That(Overflowing.CheckIfAdditionOverflows(y, x)).IsEqualTo(expected);
        await Assert.That(() => x += y).ThrowsNothing();
    }

    private async static Task AssertMultiplicationOverflow(bool expected, int x, int y)
    {
        await Assert.That(Overflowing.CheckIfMultiplicationOverflows(x, y)).IsEqualTo(expected);
        await Assert.That(Overflowing.CheckIfMultiplicationOverflows(y, x)).IsEqualTo(expected);
        await Assert.That(() => x *= y).ThrowsNothing();
    }
    private async static Task AssertMultiplicationOverflow(bool expected, uint x, uint y)
    {
        await Assert.That(Overflowing.CheckIfMultiplicationOverflows(x, y)).IsEqualTo(expected);
        await Assert.That(Overflowing.CheckIfMultiplicationOverflows(y, x)).IsEqualTo(expected);
        await Assert.That(() => x *= y).ThrowsNothing();
    }
    private async static Task AssertMultiplicationOverflow(bool expected, long x, long y)
    {
        await Assert.That(Overflowing.CheckIfMultiplicationOverflows(x, y)).IsEqualTo(expected);
        await Assert.That(Overflowing.CheckIfMultiplicationOverflows(y, x)).IsEqualTo(expected);
        await Assert.That(() => x *= y).ThrowsNothing();
    }
    private async static Task AssertMultiplicationOverflow(bool expected, ulong x, ulong y)
    {
        await Assert.That(Overflowing.CheckIfMultiplicationOverflows(x, y)).IsEqualTo(expected);
        await Assert.That(Overflowing.CheckIfMultiplicationOverflows(y, x)).IsEqualTo(expected);
        await Assert.That(() => x *= y).ThrowsNothing();
    }
}