using Garyon.Extensions;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class RepeatExtensionsTests
{
    [Test]
    public async Task RepeatedCharTest()
    {
        await Assert.That('x'.Repeated(3).ToString()).IsEqualTo("xxx");
        await Assert.That('x'.Repeated(0).ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task RepeatedStringTest()
    {
        await Assert.That("ab".Repeated(3).ToString()).IsEqualTo("ababab");
        await Assert.That("ab".Repeated(3).Length).IsEqualTo(6);
    }

    [Test]
    public async Task RepeatTest()
    {
        await Assert.That("ab".Repeat(0)).IsEqualTo(string.Empty);
        await Assert.That("ab".Repeat(1)).IsEqualTo("ab");
        await Assert.That("ab".Repeat(2)).IsEqualTo("abab");
    }

    [Test]
    public async Task RepeatNegativeCountThrowsTest()
    {
        bool threw = false;
        try
        {
            _ = "ab".Repeat(-1);
        }
        catch (ArgumentOutOfRangeException e)
        {
            threw = true;
            await Assert.That(e.ParamName).IsEqualTo("count");
        }

        await Assert.That(threw).IsTrue();
    }
}
