using Garyon.Objects;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Objects;

public class StructRefTests
{
    [Test]
    public async Task ReferenceTest()
    {
        int value = 10;

        var ref0 = value.CreateReference();
        var ref1 = value.CreateReference();

        value = 25;

        await Assert.That(ref0.GetReference()).IsEqualTo(value);
        await Assert.That(ref1.GetReference()).IsEqualTo(value);

        ref0.GetReference() = 0;

        await Assert.That(value).IsEqualTo(ref0.GetReference());
        await Assert.That(ref1.GetReference()).IsEqualTo(ref0.GetReference());

        ref1.GetReference() = 1;

        await Assert.That(value).IsEqualTo(ref1.GetReference());
        await Assert.That(ref0.GetReference()).IsEqualTo(ref1.GetReference());
    }

    [Test]
    public async Task IrrelevantReferenceTest()
    {
        int value0 = 1;
        int value1 = 5;

        var ref0 = value0.CreateReference();
        var ref1 = value0.CreateReference();

        value0 = 2;

        ref1.SetReference(ref value1);

        await Assert.That(ref0.GetReference()).IsEqualTo(value0);
        await Assert.That(ref1.GetReference()).IsEqualTo(value1);
    }
}