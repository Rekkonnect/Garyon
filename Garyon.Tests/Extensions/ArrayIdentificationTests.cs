using Garyon.Functions.Arrays;
using NUnit.Framework;

namespace Garyon.Tests.Extensions;

[Parallelizable(ParallelScope.Children)]
public class ArrayIdentificationTests
{
    [Test]
    public void IsArrayOfType()
    {
        Assert.IsTrue(ArrayIdentification.IsArrayOfType<object[], object>());
        Assert.IsTrue(ArrayIdentification.IsArrayOfType<object[,], object>());
        Assert.IsTrue(ArrayIdentification.IsArrayOfType<object[,,], object>());
        Assert.IsTrue(ArrayIdentification.IsArrayOfType<object[,,,], object>());

        Assert.IsFalse(ArrayIdentification.IsArrayOfType<object[], int>());
        Assert.IsFalse(ArrayIdentification.IsArrayOfType<object[], short>());
        Assert.IsFalse(ArrayIdentification.IsArrayOfType<short[], int>());
        Assert.IsTrue(ArrayIdentification.IsArrayOfType<int[], int>());
    }
    [Test]
    public void IsJaggedArrayOfType()
    {
        Assert.IsTrue(ArrayIdentification.IsArrayOfType<object[][], object>(2));
        Assert.IsTrue(ArrayIdentification.IsArrayOfType<object[,][], object>(2));
        Assert.IsTrue(ArrayIdentification.IsArrayOfType<object[,][,], object>(2));
        Assert.IsTrue(ArrayIdentification.IsArrayOfType<object[,][,,,], object>(2));

        Assert.IsFalse(ArrayIdentification.IsArrayOfType<object[][][], object>(2));
        Assert.IsFalse(ArrayIdentification.IsArrayOfType<object[][][][], object>(2));
        Assert.IsTrue(ArrayIdentification.IsArrayOfType<object[][][][], object>(4));
        Assert.IsTrue(ArrayIdentification.IsArrayOfType<object[][][][][], object>(6));
        Assert.IsTrue(ArrayIdentification.IsArrayOfType<object[,,,][,][,,,][,,,,,,][,,], object>(6));
    }
}
