using Garyon.Extensions;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class HashCodeExtensionsTests
{
    [Test]
    public async Task AddRangeTest()
    {
        var fromRange = new HashCode().AddRange([1, 2, 3]).ToHashCode();

        var expected = new HashCode();
        expected.Add(1);
        expected.Add(2);
        expected.Add(3);

        await Assert.That(fromRange).IsEqualTo(expected.ToHashCode());
    }
}
