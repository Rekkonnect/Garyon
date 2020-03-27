using Garyon.Extensions;
using NUnit.Framework;

namespace Garyon.Tests.Extensions
{
    public class GenericArrayExtensionsTests
    {
        [Test]
        public void IsArrayOfType()
        {
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfType<object[], object>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfType<object[,], object>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfType<object[,,], object>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfType<object[,,,], object>());

            Assert.IsFalse(GenericArrayExtensions.IsArrayOfType<object[], int>());
            Assert.IsFalse(GenericArrayExtensions.IsArrayOfType<object[], short>());
            Assert.IsFalse(GenericArrayExtensions.IsArrayOfType<short[], int>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfType<int[], int>());

            Assert.IsTrue(GenericArrayExtensions.IsArrayOfByte<byte[]>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfInt16<short[]>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfInt32<int[]>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfInt64<long[]>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfSByte<sbyte[]>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfUInt16<ushort[]>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfUInt32<uint[]>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfUInt64<ulong[]>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfSingle<float[]>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfDouble<double[]>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfDecimal<decimal[]>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfChar<char[]>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfBoolean<bool[]>());
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfString<string[]>());
        }
        [Test]
        public void IsJaggedArrayOfType()
        {
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfType<object[][], object>(2));
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfType<object[,][], object>(2));
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfType<object[,][,], object>(2));
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfType<object[,][,,,], object>(2));

            Assert.IsFalse(GenericArrayExtensions.IsArrayOfType<object[][][], object>(2));
            Assert.IsFalse(GenericArrayExtensions.IsArrayOfType<object[][][][], object>(2));
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfType<object[][][][], object>(4));
            Assert.IsTrue(GenericArrayExtensions.IsArrayOfType<object[][][][][], object>(6));
        }
    }
}
