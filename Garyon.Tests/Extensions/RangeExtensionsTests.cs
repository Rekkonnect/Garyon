using Garyon.Extensions;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class RangeExtensionsTests
{
    [Test]
    public async Task GetStartAndEndTest()
    {
        var range = 1..^1;
        range.GetStartAndEnd(5, out int start, out int end);

        await Assert.That(start).IsEqualTo(1);
        await Assert.That(end).IsEqualTo(4);
    }
}
