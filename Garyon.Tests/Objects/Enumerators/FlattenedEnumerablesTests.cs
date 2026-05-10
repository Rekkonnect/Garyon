using Garyon.Extensions;
using Garyon.Objects.Enumerators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Objects.Enumerators;

public class FlattenedEnumerablesTests
{
    [Test]
    public async Task FlattenedEnumerables2DEnumeratorResetTest()
    {
        IEnumerable<IEnumerable<int>> source =
        [
            [1, 2],
            [],
            [3],
        ];
        var flattened = new FlattenedEnumerables2D<int>(source);
        using var enumerator = flattened.GetEnumerator();

        await Assert.That(enumerator.MoveNext()).IsTrue();
        await Assert.That(enumerator.Current).IsEqualTo(1);

        enumerator.Reset();

        await Assert.That(enumerator.MoveNext()).IsTrue();
        await Assert.That(enumerator.Current).IsEqualTo(1);
    }

    [Test]
    public async Task FlattenedEnumerables3DBackedEnumeratorTest()
    {
        IEnumerable<IEnumerable<IEnumerable<int>>> source =
        [
            [
                [1, 2],
                [],
            ],
            [
                [3, 4],
            ],
        ];
        var flattened = new FlattenedEnumerables3D<int>(source);

        var enumerator = flattened.GetBackedEnumerator();
        var result = new List<int>();
        while (enumerator.MoveNext())
            result.Add(enumerator.Current);

        await Assert.That(result.SequenceEqual([1, 2, 3, 4])).IsTrue();
    }
}
