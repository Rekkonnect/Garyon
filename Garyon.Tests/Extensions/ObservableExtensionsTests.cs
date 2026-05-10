using Garyon.Extensions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class ObservableExtensionsTests
{
    [Test]
    public async Task RemoveRangeTest()
    {
        var observable = new ObservableCollection<int>(new[] { 1, 2, 3, 4 });
        observable.RemoveRange([2, 4, 10]);
        await Assert.That(observable.SequenceEqual([1, 3])).IsTrue();
    }
}
