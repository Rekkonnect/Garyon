using Garyon.Functions;
using NUnit.Framework;

namespace Garyon.Tests.Functions;

public class EnumHelpersTests
{
    [Test]
    public void GetEntryCountTest()
    {
        Assert.AreEqual(3, EnumHelpers.GetEntryCount<TestEnum>());
        Assert.AreEqual(3, EnumHelpers.GetEntryCount(typeof(TestEnum)));
    }

    private enum TestEnum
    {
        A,
        B,
        C,
    }
}
