using Garyon.Extensions;
using System.Collections;
using System.Collections.Specialized;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class NonGenericICollectionExtensionsTests
{
    [Test]
    public async Task IsEmptyCollectionTest()
    {
        var collection = new ArrayList();
        await Assert.That(((ICollection)collection).IsEmptyCollection).IsTrue();
        collection.Add(1);
        await Assert.That(((ICollection)collection).IsEmptyCollection).IsFalse();
    }
}
