using Garyon.Extensions;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class StaticEnumerableExtensionsTests
{
    [Test]
    public async Task IndexRangeTest()
    {
        var indices = Enumerable.IndexRange(4).ToArray();
        await Assert.That(indices.SequenceEqual([0, 1, 2, 3])).IsTrue();
    }
}
