using Garyon.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class IEnumeratorExtensionsTests
{
    [Test]
    public async Task GetEnumeratorAndWithResetStateTest()
    {
        var enumerator = new TrackingEnumerator([1, 2, 3]);
        _ = enumerator.MoveNext();
        _ = enumerator.MoveNext();

        IEnumerator nonGeneric = enumerator.GetEnumerator();
        var sameEnumerator = enumerator.GetEnumerator<TrackingEnumerator, int>();
        var resetEnumerator = enumerator.WithResetState();
        bool movedAfterReset = resetEnumerator.MoveNext();
        int current = resetEnumerator.Current;

        await Assert.That(ReferenceEquals(nonGeneric, enumerator)).IsTrue();
        await Assert.That(ReferenceEquals(sameEnumerator, enumerator)).IsTrue();
        await Assert.That(movedAfterReset).IsTrue();
        await Assert.That(current).IsEqualTo(1);
        await Assert.That(enumerator.ResetCount).IsEqualTo(1);
    }

    [Test]
    public async Task ToListToArrayAndToImmutableArrayMaterializeEnumeratorTest()
    {
        var enumerator = new TrackingEnumerator([4, 5, 6]);
        var list = enumerator.ToList();
        var array = enumerator.ToArray(true);
        ImmutableArray<int> immutable = enumerator.ToImmutableArray(true);

        await Assert.That(list.SequenceEqual([4, 5, 6])).IsTrue();
        await Assert.That(array.SequenceEqual([4, 5, 6])).IsTrue();
        await Assert.That(immutable.SequenceEqual([4, 5, 6])).IsTrue();
        await Assert.That(enumerator.ResetCount).IsEqualTo(2);
    }

    private sealed class TrackingEnumerator : IEnumerator<int>
    {
        private readonly int[] values;
        private int index = -1;

        public int ResetCount { get; private set; }

        public TrackingEnumerator(int[] values)
        {
            this.values = values;
        }

        public int Current => values[index];
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (index + 1 >= values.Length)
                return false;

            index++;
            return true;
        }

        public void Reset()
        {
            ResetCount++;
            index = -1;
        }

        public void Dispose()
        {
        }
    }
}
