using Garyon.Extensions;
using Garyon.Extensions.ArrayExtensions;
using Garyon.Functions.Arrays;
using NUnit.Framework;

namespace Garyon.Tests.Extensions
{
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

            Assert.IsTrue(ArrayIdentification.IsArrayOfByte<byte[]>());
            Assert.IsTrue(ArrayIdentification.IsArrayOfInt16<short[]>());
            Assert.IsTrue(ArrayIdentification.IsArrayOfInt32<int[]>());
            Assert.IsTrue(ArrayIdentification.IsArrayOfInt64<long[]>());
            Assert.IsTrue(ArrayIdentification.IsArrayOfSByte<sbyte[]>());
            Assert.IsTrue(ArrayIdentification.IsArrayOfUInt16<ushort[]>());
            Assert.IsTrue(ArrayIdentification.IsArrayOfUInt32<uint[]>());
            Assert.IsTrue(ArrayIdentification.IsArrayOfUInt64<ulong[]>());
            Assert.IsTrue(ArrayIdentification.IsArrayOfSingle<float[]>());
            Assert.IsTrue(ArrayIdentification.IsArrayOfDouble<double[]>());
            Assert.IsTrue(ArrayIdentification.IsArrayOfDecimal<decimal[]>());
            Assert.IsTrue(ArrayIdentification.IsArrayOfChar<char[]>());
            Assert.IsTrue(ArrayIdentification.IsArrayOfBoolean<bool[]>());
            Assert.IsTrue(ArrayIdentification.IsArrayOfString<string[]>());
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
        }
    }
}
