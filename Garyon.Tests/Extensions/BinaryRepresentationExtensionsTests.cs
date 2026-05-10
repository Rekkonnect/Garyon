using Garyon.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class BinaryRepresentationExtensionsTests
{
    private const ulong u64Value = 0b_00100100_00011010_11110101_00000101_01010101_10101010_01110010_00100101;
    private const uint u32Value = 0b_01010101_10101010_01110010_00100101;
    private const ushort u16Value = 0b_01110010_00100101;
    private const byte u8Value = 0b_00100101;

    private const long i64Value = (long)u64Value;
    private const int i32Value = (int)u32Value;
    private const short i16Value = (short)u16Value;
    private const sbyte i8Value = (sbyte)u8Value;

    private const string u64ValueRepresentation = "00100100_00011010_11110101_00000101_01010101_10101010_01110010_00100101";
    private const string u32ValueRepresentation = "01010101_10101010_01110010_00100101";
    private const string u16ValueRepresentation = "01110010_00100101";
    private const string u8ValueRepresentation = "00100101";

    #region Binary Representation
    private static readonly int[] edgeTotalBitsValues = [0, -1];

    [Test]
    public async Task ByteBinaryRepresentationTest()
    {
        var expectedRepresentation = u8ValueRepresentation.Replace("_", "");
        var actualRepresentation = u8Value.GetBinaryRepresentation();
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        foreach (int edgeValue in edgeTotalBitsValues)
        {
            Assert.Throws<ArgumentException>(() => default(byte).GetBinaryRepresentation(edgeValue));
        }
    }
    [Test]
    public async Task SByteBinaryRepresentationTest()
    {
        var expectedRepresentation = u8ValueRepresentation.Replace("_", "");
        var actualRepresentation = i8Value.GetBinaryRepresentation();
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        foreach (int edgeValue in edgeTotalBitsValues)
        {
            Assert.Throws<ArgumentException>(() => default(sbyte).GetBinaryRepresentation(edgeValue));
        }
    }
    [Test]
    public async Task Int16BinaryRepresentationTest()
    {
        var expectedRepresentation = u16ValueRepresentation.Replace("_", "");
        var actualRepresentation = i16Value.GetBinaryRepresentation();
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        foreach (int edgeValue in edgeTotalBitsValues)
        {
            Assert.Throws<ArgumentException>(() => default(short).GetBinaryRepresentation(edgeValue));
        }
    }
    [Test]
    public async Task UInt16BinaryRepresentationTest()
    {
        var expectedRepresentation = u16ValueRepresentation.Replace("_", "");
        var actualRepresentation = u16Value.GetBinaryRepresentation();
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        foreach (int edgeValue in edgeTotalBitsValues)
        {
            Assert.Throws<ArgumentException>(() => default(ushort).GetBinaryRepresentation(edgeValue));
        }
    }
    [Test]
    public async Task Int32BinaryRepresentationTest()
    {
        var expectedRepresentation = u32ValueRepresentation.Replace("_", "");
        var actualRepresentation = i32Value.GetBinaryRepresentation();
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        foreach (int edgeValue in edgeTotalBitsValues)
        {
            Assert.Throws<ArgumentException>(() => default(int).GetBinaryRepresentation(edgeValue));
        }
    }
    [Test]
    public async Task UInt32BinaryRepresentationTest()
    {
        var expectedRepresentation = u32ValueRepresentation.Replace("_", "");
        var actualRepresentation = u32Value.GetBinaryRepresentation();
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        foreach (int edgeValue in edgeTotalBitsValues)
        {
            Assert.Throws<ArgumentException>(() => default(uint).GetBinaryRepresentation(edgeValue));
        }
    }
    [Test]
    public async Task Int64BinaryRepresentationTest()
    {
        var expectedRepresentation = u64ValueRepresentation.Replace("_", "");
        var actualRepresentation = i64Value.GetBinaryRepresentation();
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        foreach (int edgeValue in edgeTotalBitsValues)
        {
            Assert.Throws<ArgumentException>(() => default(long).GetBinaryRepresentation(edgeValue));
        }
    }
    [Test]
    public async Task UInt64BinaryRepresentationTest()
    {
        var expectedRepresentation = u64ValueRepresentation.Replace("_", "");
        var actualRepresentation = u64Value.GetBinaryRepresentation();
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        foreach (int edgeValue in edgeTotalBitsValues)
        {
            Assert.Throws<ArgumentException>(() => default(ulong).GetBinaryRepresentation(edgeValue));
        }
    }
    #endregion

    #region Grouped Binary Representation
    [Test]
    public async Task ByteGroupedBinaryRepresentationTest()
    {
        var baseExpectedRepresentation = u8ValueRepresentation.Replace('_', ' ');

        var expectedRepresentation = baseExpectedRepresentation;
        var actualRepresentation = u8Value.GetGroupedBinaryRepresentation(8);
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        const int bits = 8;
        for (int removedBits = 1; removedBits < bits; removedBits++)
        {
            var totalBits = bits - removedBits;
            actualRepresentation = u8Value.GetGroupedBinaryRepresentation(8, totalBits);
            await AssertRepresentationSubstring(baseExpectedRepresentation, actualRepresentation, totalBits);
        }

        Assert.Throws<ArgumentException>(() => default(byte).GetGroupedBinaryRepresentation(-1, -1));
    }
    [Test]
    public async Task SByteGroupedBinaryRepresentationTest()
    {
        var baseExpectedRepresentation = u8ValueRepresentation.Replace('_', ' ');

        var expectedRepresentation = baseExpectedRepresentation;
        var actualRepresentation = i8Value.GetGroupedBinaryRepresentation(8);
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        const int bits = 8;
        for (int removedBits = 1; removedBits < bits; removedBits++)
        {
            var totalBits = bits - removedBits;
            actualRepresentation = i8Value.GetGroupedBinaryRepresentation(8, totalBits);
            await AssertRepresentationSubstring(baseExpectedRepresentation, actualRepresentation, totalBits);
        }

        Assert.Throws<ArgumentException>(() => default(sbyte).GetGroupedBinaryRepresentation(-1, -1));
    }
    [Test]
    public async Task Int16GroupedBinaryRepresentationTest()
    {
        var baseExpectedRepresentation = u16ValueRepresentation.Replace('_', ' ');

        var expectedRepresentation = baseExpectedRepresentation;
        var actualRepresentation = i16Value.GetGroupedBinaryRepresentation(8);
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        const int bits = 16;
        for (int removedBits = 1; removedBits < bits; removedBits++)
        {
            var totalBits = bits - removedBits;
            actualRepresentation = i16Value.GetGroupedBinaryRepresentation(8, totalBits);
            await AssertRepresentationSubstring(baseExpectedRepresentation, actualRepresentation, totalBits);
        }

        Assert.Throws<ArgumentException>(() => default(short).GetGroupedBinaryRepresentation(-1, -1));
    }
    [Test]
    public async Task UInt16GroupedBinaryRepresentationTest()
    {
        var baseExpectedRepresentation = u16ValueRepresentation.Replace('_', ' ');

        var expectedRepresentation = baseExpectedRepresentation;
        var actualRepresentation = u16Value.GetGroupedBinaryRepresentation(8);
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        const int bits = 16;
        for (int removedBits = 1; removedBits < bits; removedBits++)
        {
            var totalBits = bits - removedBits;
            actualRepresentation = u16Value.GetGroupedBinaryRepresentation(8, totalBits);
            await AssertRepresentationSubstring(baseExpectedRepresentation, actualRepresentation, totalBits);
        }

        Assert.Throws<ArgumentException>(() => default(ushort).GetGroupedBinaryRepresentation(-1, -1));
    }
    [Test]
    public async Task Int32GroupedBinaryRepresentationTest()
    {
        var baseExpectedRepresentation = u32ValueRepresentation.Replace('_', ' ');

        var expectedRepresentation = baseExpectedRepresentation;
        var actualRepresentation = i32Value.GetGroupedBinaryRepresentation(8);
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        const int bits = 32;
        for (int removedBits = 1; removedBits < bits; removedBits++)
        {
            var totalBits = bits - removedBits;
            actualRepresentation = i32Value.GetGroupedBinaryRepresentation(8, totalBits);
            await AssertRepresentationSubstring(baseExpectedRepresentation, actualRepresentation, totalBits);
        }

        Assert.Throws<ArgumentException>(() => default(int).GetGroupedBinaryRepresentation(-1, -1));
    }
    [Test]
    public async Task UInt32GroupedBinaryRepresentationTest()
    {
        var baseExpectedRepresentation = u32ValueRepresentation.Replace('_', ' ');

        var expectedRepresentation = baseExpectedRepresentation;
        var actualRepresentation = u32Value.GetGroupedBinaryRepresentation(8);
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        const int bits = 32;
        for (int removedBits = 1; removedBits < bits; removedBits++)
        {
            var totalBits = bits - removedBits;
            actualRepresentation = u32Value.GetGroupedBinaryRepresentation(8, totalBits);
            await AssertRepresentationSubstring(baseExpectedRepresentation, actualRepresentation, totalBits);
        }

        Assert.Throws<ArgumentException>(() => default(uint).GetGroupedBinaryRepresentation(-1, -1));
    }
    [Test]
    public async Task Int64GroupedBinaryRepresentationTest()
    {
        var baseExpectedRepresentation = u64ValueRepresentation.Replace('_', ' ');

        var expectedRepresentation = baseExpectedRepresentation;
        var actualRepresentation = i64Value.GetGroupedBinaryRepresentation(8);
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        const int bits = 64;
        for (int removedBits = 1; removedBits < bits; removedBits++)
        {
            var totalBits = bits - removedBits;
            actualRepresentation = i64Value.GetGroupedBinaryRepresentation(8, totalBits);
            await AssertRepresentationSubstring(baseExpectedRepresentation, actualRepresentation, totalBits);
        }

        Assert.Throws<ArgumentException>(() => default(long).GetGroupedBinaryRepresentation(-1, -1));
    }
    [Test]
    public async Task UInt64GroupedBinaryRepresentationTest()
    {
        var baseExpectedRepresentation = u64ValueRepresentation.Replace('_', ' ');

        var expectedRepresentation = baseExpectedRepresentation;
        var actualRepresentation = u64Value.GetGroupedBinaryRepresentation(8);
        await Assert.That(actualRepresentation).IsEqualTo(expectedRepresentation);

        const int bits = 64;
        for (int removedBits = 1; removedBits < bits; removedBits++)
        {
            var totalBits = bits - removedBits;
            actualRepresentation = u64Value.GetGroupedBinaryRepresentation(8, totalBits);
            await AssertRepresentationSubstring(baseExpectedRepresentation, actualRepresentation, totalBits);
        }

        Assert.Throws<ArgumentException>(() => default(ulong).GetGroupedBinaryRepresentation(-1, -1));
    }

    private async static Task AssertRepresentationSubstring(
        string baseExpectedRepresentation,
        string actualRepresentation,
        int totalBits)
    {
        await Assert.That(actualRepresentation.StartsWith(' ')).IsFalse();
        await Assert.That(actualRepresentation.EndsWith(' ')).IsFalse();
        await Assert.That(BitCount(actualRepresentation)).IsEqualTo(totalBits);

        var substringLength = actualRepresentation.Length;
        var expectedSubstring = baseExpectedRepresentation[^substringLength..];
        await Assert.That(actualRepresentation).IsEqualTo(expectedSubstring);
    }

    private static int BitCount(string str)
    {
        return str.Count(c => c is '0' or '1');
    }
    #endregion
}