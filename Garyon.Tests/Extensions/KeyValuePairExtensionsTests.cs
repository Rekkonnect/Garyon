using Garyon.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class KeyValuePairExtensionsTests
{
    [Test]
    public async Task WithKeySameTypeTest()
    {
        var original = new KeyValuePair<int, string>(1, "value");
        var withKey = original.WithKey(2);

        await Assert.That(withKey.Key).IsEqualTo(2);
        await Assert.That(withKey.Value).IsEqualTo("value");
    }

    [Test]
    public async Task WithValueSameTypeTest()
    {
        var original = new KeyValuePair<int, string>(1, "value");
        var withValue = original.WithValue("next");

        await Assert.That(withValue.Key).IsEqualTo(1);
        await Assert.That(withValue.Value).IsEqualTo("next");
    }

    [Test]
    public async Task WithKeyNewTypeTest()
    {
        var original = new KeyValuePair<int, string>(1, "value");
        var withNewKey = original.WithKey("key");

        await Assert.That(withNewKey.Key).IsEqualTo("key");
        await Assert.That(withNewKey.Value).IsEqualTo("value");
    }

    [Test]
    public async Task WithValueNewTypeTest()
    {
        var original = new KeyValuePair<int, string>(1, "value");
        var withNewValue = original.WithValue(100);

        await Assert.That(withNewValue.Key).IsEqualTo(1);
        await Assert.That(withNewValue.Value).IsEqualTo(100);
    }
}

