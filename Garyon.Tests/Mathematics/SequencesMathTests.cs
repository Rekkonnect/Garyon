using Garyon.Mathematics;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Mathematics;

public class SequencesMathTests
{
    [Test]
    public async Task SumToMaxTest()
    {
        await Assert.That(SequencesMath.Sum(10)).IsEqualTo(55);
    }

    [Test]
    public async Task SumRangeTest()
    {
        await Assert.That(SequencesMath.Sum(3, 7)).IsEqualTo(25);
    }

    [Test]
    public async Task SumSingleValueRangeTest()
    {
        await Assert.That(SequencesMath.Sum(5, 5)).IsEqualTo(5);
    }
}
