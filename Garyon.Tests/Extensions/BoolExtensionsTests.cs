using Garyon.Extensions;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class BoolExtensionsTests
{
    [Test]
    public async Task NotTest()
    {
        await Assert.That(true.Not).IsFalse();
        await Assert.That(false.Not).IsTrue();
    }

    [Test]
    public async Task ToIntegerTest()
    {
        await Assert.That(true.ToInt32()).IsEqualTo(1);
        await Assert.That(false.ToInt64()).IsEqualTo(0);
    }

    [Test]
    public async Task SwitchValueTest()
    {
        await Assert.That(true.SwitchValue("left", "right")).IsEqualTo("left");
        await Assert.That(false.SwitchValue("left", "right")).IsEqualTo("right");
    }

    [Test]
    public async Task SwitchInvokeActionTest()
    {
        string branch = string.Empty;
        true.SwitchInvoke(() => branch = "true", () => branch = "false");
        await Assert.That(branch).IsEqualTo("true");
    }

    [Test]
    public async Task SwitchInvokeFunctionTest()
    {
        await Assert.That(false.SwitchInvoke(() => 1, () => 2)).IsEqualTo(2);
    }

    [Test]
    public async Task ValueOrNullTest()
    {
        await Assert.That(true.ValueOrNull(5)).IsEqualTo(5);
        await Assert.That(false.ValueOrNull(5)).IsNull();
    }

    [Test]
    public async Task ValueOrDefaultTest()
    {
        await Assert.That(true.ValueOrDefault("value")).IsEqualTo("value");
        await Assert.That(false.ValueOrDefault("value")).IsNull();
    }
}
