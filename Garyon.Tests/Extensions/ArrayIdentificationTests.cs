using Garyon.Functions.Arrays;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class ArrayIdentificationTests
{
    [Test]
    public async Task IsArrayOfType()
    {
        await Assert.That(ArrayIdentification.IsArrayOfType<object[], object>()).IsTrue();
        await Assert.That(ArrayIdentification.IsArrayOfType<object[,], object>()).IsTrue();
        await Assert.That(ArrayIdentification.IsArrayOfType<object[,,], object>()).IsTrue();
        await Assert.That(ArrayIdentification.IsArrayOfType<object[,,,], object>()).IsTrue();

        await Assert.That(ArrayIdentification.IsArrayOfType<object[], int>()).IsFalse();
        await Assert.That(ArrayIdentification.IsArrayOfType<object[], short>()).IsFalse();
        await Assert.That(ArrayIdentification.IsArrayOfType<short[], int>()).IsFalse();
        await Assert.That(ArrayIdentification.IsArrayOfType<int[], int>()).IsTrue();
    }

    [Test]
    public async Task IsJaggedArrayOfType()
    {
        await Assert.That(ArrayIdentification.IsArrayOfType<object[][], object>(2)).IsTrue();
        await Assert.That(ArrayIdentification.IsArrayOfType<object[,][], object>(2)).IsTrue();
        await Assert.That(ArrayIdentification.IsArrayOfType<object[,][,], object>(2)).IsTrue();
        await Assert.That(ArrayIdentification.IsArrayOfType<object[,][,,,], object>(2)).IsTrue();

        await Assert.That(ArrayIdentification.IsArrayOfType<object[][][], object>(2)).IsFalse();
        await Assert.That(ArrayIdentification.IsArrayOfType<object[][][][], object>(2)).IsFalse();
        await Assert.That(ArrayIdentification.IsArrayOfType<object[][][][], object>(4)).IsTrue();
        await Assert.That(ArrayIdentification.IsArrayOfType<object[][][][][], object>(6)).IsTrue();
        await Assert.That(ArrayIdentification.IsArrayOfType<object[,,,][,][,,,][,,,,,,][,,], object>(6)).IsTrue();
    }
}