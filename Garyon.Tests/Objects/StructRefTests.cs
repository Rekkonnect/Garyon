using Garyon.Objects;
using NUnit.Framework;

namespace Garyon.Tests.Objects;

public class StructRefTests
{
    [Test]
    public void ReferenceTest()
    {
        int value = 10;

        var ref0 = value.CreateReference();
        var ref1 = value.CreateReference();

        value = 25;

        Assert.AreEqual(value, ref0.GetReference());
        Assert.AreEqual(value, ref1.GetReference());

        ref0.GetReference() = 0;

        Assert.AreEqual(ref0.GetReference(), value);
        Assert.AreEqual(ref0.GetReference(), ref1.GetReference());

        ref1.GetReference() = 1;

        Assert.AreEqual(ref1.GetReference(), value);
        Assert.AreEqual(ref1.GetReference(), ref0.GetReference());
    }

    [Test]
    public void IrrelevantReferenceTest()
    {
        int value0 = 1;
        int value1 = 5;

        var ref0 = value0.CreateReference();
        var ref1 = value0.CreateReference();

        value0 = 2;

        ref1.SetReference(ref value1);

        Assert.AreEqual(value0, ref0.GetReference());
        Assert.AreEqual(value1, ref1.GetReference());
    }
}
