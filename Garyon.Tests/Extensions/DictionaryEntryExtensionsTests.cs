using Garyon.Extensions;
using System.Collections;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class DictionaryEntryExtensionsTests
{
    [Test]
    public async Task ToKeyValuePairTest()
    {
        var entry = new DictionaryEntry("key", 42);
        var kvp = entry.ToKeyValuePair<string, int>();

        await Assert.That(kvp.Key).IsEqualTo("key");
        await Assert.That(kvp.Value).IsEqualTo(42);
    }
}
