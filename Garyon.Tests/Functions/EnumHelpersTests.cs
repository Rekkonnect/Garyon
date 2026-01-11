using Garyon.Functions;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Functions;

public class EnumHelpersTests
{
    [Test]
    public async Task GetEntryCountTest()
    {
        await Assert.That(EnumHelpers.GetEntryCount<TestEnum>()).IsEqualTo(3);
    }

    private enum TestEnum
    {
        A,
        B,
        C,
    }
}