using Garyon.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class IReadOnlyCollectionExtensionsTests
{
    [Test]
    public async Task IsEmptyReturnsTrueForEmptyCollectionTest()
    {
        IReadOnlyCollection<int> collection = System.Array.Empty<int>();

        await Assert.That(collection.IsEmpty).IsTrue();
    }

    [Test]
    public async Task IsEmptyReturnsFalseForNonEmptyCollectionTest()
    {
        IReadOnlyCollection<int> collection = new List<int> { 1 };

        await Assert.That(collection.IsEmpty).IsFalse();
    }
}
