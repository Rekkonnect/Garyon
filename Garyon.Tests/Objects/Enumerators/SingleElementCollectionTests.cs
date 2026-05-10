using Garyon.Objects.Enumerators;
using System.Collections;
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

    [Test]
    public async Task EqualityHashCodeAndWithElementTest()
    {
        var collection = new SingleElementCollection<string>("value");
        var copy = new SingleElementCollection<string>(collection);
        var changed = collection.WithElement("next");
        var enumerator = ((IEnumerable)collection).GetEnumerator();

        await Assert.That(collection.Count).IsEqualTo(1);
        await Assert.That(copy == collection).IsTrue();
        await Assert.That(copy != changed).IsTrue();
        await Assert.That(copy.Equals((object)collection)).IsTrue();
        await Assert.That(collection.GetHashCode()).IsEqualTo("value".GetHashCode());
        await Assert.That(changed.Single()).IsEqualTo("next");
        await Assert.That(enumerator.MoveNext()).IsTrue();
        await Assert.That(enumerator.Current).IsEqualTo("value");
    }
}
