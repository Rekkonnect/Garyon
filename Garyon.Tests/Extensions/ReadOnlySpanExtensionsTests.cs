using Garyon.Extensions;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class ReadOnlySpanExtensionsTests
{
    [Test]
    public async Task AsReadOnlyListTest()
    {
        ReadOnlySpan<int> span = [1, 2, 3];
        var list = span.AsReadOnlyList();

        await Assert.That(list.Count).IsEqualTo(3);
        await Assert.That(list[0]).IsEqualTo(1);
        await Assert.That(list[2]).IsEqualTo(3);
    }
}
