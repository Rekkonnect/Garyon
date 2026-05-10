using Garyon.Extensions;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class CollectionToStringExtensionsTests
{
    [Test]
    public async Task ToListStringTest()
    {
        await Assert.That(new[] { 1, 2, 3 }.ToListString()).IsEqualTo("1, 2, 3");
        await Assert.That(new[] { 1, 2, 3 }.ToListString("|")).IsEqualTo("1|2|3");
    }
}
