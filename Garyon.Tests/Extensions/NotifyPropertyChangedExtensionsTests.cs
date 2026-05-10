using Garyon.Extensions;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class NotifyPropertyChangedExtensionsTests
{
    [Test]
    public async Task NewItemsTest()
    {
        var addArgs = new NotifyCollectionChangedEventArgs(
            NotifyCollectionChangedAction.Add,
            new List<string> { "a", "b" });
        var addedItems = addArgs.NewItems<string>().ToArray();
        await Assert.That(addedItems.SequenceEqual(["a", "b"])).IsTrue();

        var resetArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
        await Assert.That(resetArgs.NewItems<string>().Any()).IsFalse();
    }
}
