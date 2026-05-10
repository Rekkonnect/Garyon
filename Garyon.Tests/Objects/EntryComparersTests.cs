using Garyon.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Objects;

public class EntryComparersTests
{
    [Test]
    public async Task EntryComparerVariantsTest()
    {
        await Assert.That(EntryKeyComparer<int, string>.Default.Compare(new(1, "b"), new(2, "a")) < 0).IsTrue();
        await Assert.That(EntryValueComparer<int, string>.Default.Compare(new(2, "b"), new(1, "a")) > 0).IsTrue();
        await Assert.That(EntryKeyOverValueComparer<int, string>.Default.Compare(new(1, "a"), new(1, "b")) < 0).IsTrue();
        await Assert.That(EntryValueOverKeyComparer<int, string>.Default.Compare(new(2, "a"), new(1, "a")) > 0).IsTrue();
    }

    [Test]
    public async Task EntryComparersReturnZeroForEqualEntriesTest()
    {
        var entry = new KeyValuePair<int, string>(1, "a");

        await Assert.That(EntryKeyComparer<int, string>.Default.Compare(entry, entry)).IsEqualTo(0);
        await Assert.That(EntryValueComparer<int, string>.Default.Compare(entry, entry)).IsEqualTo(0);
        await Assert.That(EntryKeyOverValueComparer<int, string>.Default.Compare(entry, entry)).IsEqualTo(0);
        await Assert.That(EntryValueOverKeyComparer<int, string>.Default.Compare(entry, entry)).IsEqualTo(0);
    }
}
