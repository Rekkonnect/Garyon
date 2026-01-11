using Garyon.Objects.Enumerators;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;

namespace Garyon.Tests.Objects.Enumerators;

public class SingleElementCollectionTests
{
    [Test]
    public async Task Test()
    {
        var collection = new SingleElementCollection<int>(2);
        await Assert.That(collection).IsEquivalentTo([2]);
    }
}
