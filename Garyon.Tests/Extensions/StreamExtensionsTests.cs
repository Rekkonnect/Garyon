using Garyon.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class StreamExtensionsTests
{
    [Test]
    public async Task ReachedEndRemainingBytesResetAndToByteArrayTest()
    {
        var original = Encoding.UTF8.GetBytes("hello");
        using var stream = new MemoryStream(original.ToArray());
        stream.Position = 2;

        long remaining = stream.RemainingBytes();
        bool reachedBeforeCopy = stream.ReachedEnd();
        var copied = stream.ToByteArray();
        bool reachedAfterCopy = stream.ReachedEnd();
        stream.ResetPosition();

        await Assert.That(remaining).IsEqualTo(3L);
        await Assert.That(reachedBeforeCopy).IsFalse();
        await Assert.That(copied.SequenceEqual(original)).IsTrue();
        await Assert.That(reachedAfterCopy).IsTrue();
        await Assert.That(stream.Position).IsEqualTo(0L);
    }

    [Test]
    public async Task WriteAndReadCurrentPositionHelpersTest()
    {
        using var stream = new MemoryStream();
        stream.Write((byte)12);
        stream.Write((short)-1234);
        stream.Write(56789);
        stream.Write(9876543210L);
        stream.Write((sbyte)-10);
        stream.Write((ushort)40000);
        stream.Write((uint)1234567890);
        stream.Write((ulong)1234567890123456789);
        stream.Write(1.25f);
        stream.WriteUTF8String("hé");
        stream.Write([9, 8, 7]);

        stream.ResetPosition();

        bool readByte = stream.TryReadByte(out var byteValue);
        short int16Value = stream.ReadInt16();
        int int32Value = stream.ReadInt32();
        long int64Value = stream.ReadInt64();
        sbyte sbyteValue = stream.ReadSByte();
        ushort uint16Value = stream.ReadUInt16();
        uint uint32Value = stream.ReadUInt32();
        ulong uint64Value = stream.ReadUInt64();
        float floatValue = stream.ReadValue<float>();
        string text = stream.ReadUTF8String(Encoding.UTF8.GetByteCount("hé"));
        bool readTailFirst = stream.TryReadByte(out var tailFirst);
        bool readTailSecond = stream.TryReadByte(out var tailSecond);
        bool readTailThird = stream.TryReadByte(out var tailThird);
        bool reachedEnd = stream.ReachedEnd();

        await Assert.That(readByte).IsTrue();
        await Assert.That(byteValue).IsEqualTo((byte)12);
        await Assert.That(int16Value).IsEqualTo((short)-1234);
        await Assert.That(int32Value).IsEqualTo(56789);
        await Assert.That(int64Value).IsEqualTo(9876543210L);
        await Assert.That(sbyteValue).IsEqualTo((sbyte)-10);
        await Assert.That(uint16Value).IsEqualTo((ushort)40000);
        await Assert.That(uint32Value).IsEqualTo((uint)1234567890);
        await Assert.That(uint64Value).IsEqualTo((ulong)1234567890123456789);
        await Assert.That(floatValue).IsEqualTo(1.25f);
        await Assert.That(text).IsEqualTo("hé");
        await Assert.That(readTailFirst).IsTrue();
        await Assert.That(readTailSecond).IsTrue();
        await Assert.That(readTailThird).IsTrue();
        await Assert.That(tailFirst).IsEqualTo((byte)9);
        await Assert.That(tailSecond).IsEqualTo((byte)8);
        await Assert.That(tailThird).IsEqualTo((byte)7);
        await Assert.That(reachedEnd).IsTrue();
    }

    [Test]
    public async Task WriteAndReadAtSpecifiedPositionHelpersTest()
    {
        using var stream = new MemoryStream(new byte[128]);
        stream.WriteAt(0, (byte)42);
        stream.WriteAt(1, (short)-7);
        stream.WriteAt(3, 123456);
        stream.WriteAt(7, 9876543210L);
        stream.WriteAt(15, (sbyte)-11);
        stream.WriteAt(16, (ushort)55);
        stream.WriteAt(18, (uint)66);
        stream.WriteAt(22, (ulong)77);
        stream.WriteAt(30, 1.5f);
        stream.Write(40, [1, 2, 3, 4]);
        stream.WriteASCIIStringAt(50, "ascii");
        stream.WriteUnicodeStringAt(60, "uni");
        stream.WriteUTF32StringAt(70, "u32");
        stream.WriteUTF8StringAt(90, "utf8");
        stream.WriteStringAt(100, "enc", Encoding.BigEndianUnicode);

        bool readByte = stream.TryReadByteAt(0, out var byteValue);
        bool readSByte = stream.TryReadSByteAt(15, out var sbyteValue);
        bool readUInt32 = stream.TryReadUInt32At(18, out var uint32Value);
        bool readMissing = stream.TryReadInt32At(126, out _);
        short int16Value = stream.ReadInt16At(1);
        int int32Value = stream.ReadInt32At(3);
        long int64Value = stream.ReadInt64At(7);
        ushort uint16Value = stream.ReadUInt16At(16);
        ulong uint64Value = stream.ReadUInt64At(22);
        float floatValue = stream.ReadValueAt<float>(30);
        byte bufferFirst = stream.ReadValueAt<byte>(40);
        string ascii = stream.ReadASCIIStringAt(50, Encoding.ASCII.GetByteCount("ascii"));
        string unicode = stream.ReadUnicodeStringAt(60, Encoding.Unicode.GetByteCount("uni"));
        string utf32 = stream.ReadUTF32StringAt(70, Encoding.UTF32.GetByteCount("u32"));
        string utf8 = stream.ReadUTF8StringAt(90, Encoding.UTF8.GetByteCount("utf8"));
        string encoded = stream.ReadStringAt(100, Encoding.BigEndianUnicode.GetByteCount("enc"), Encoding.BigEndianUnicode);

        await Assert.That(readByte).IsTrue();
        await Assert.That(byteValue).IsEqualTo((byte)42);
        await Assert.That(readSByte).IsTrue();
        await Assert.That(sbyteValue).IsEqualTo((sbyte)-11);
        await Assert.That(readUInt32).IsTrue();
        await Assert.That(uint32Value).IsEqualTo((uint)66);
        await Assert.That(readMissing).IsFalse();
        await Assert.That(int16Value).IsEqualTo((short)-7);
        await Assert.That(int32Value).IsEqualTo(123456);
        await Assert.That(int64Value).IsEqualTo(9876543210L);
        await Assert.That(uint16Value).IsEqualTo((ushort)55);
        await Assert.That(uint64Value).IsEqualTo((ulong)77);
        await Assert.That(floatValue).IsEqualTo(1.5f);
        await Assert.That(bufferFirst).IsEqualTo((byte)1);
        await Assert.That(ascii).IsEqualTo("ascii");
        await Assert.That(unicode).IsEqualTo("uni");
        await Assert.That(utf32).IsEqualTo("u32");
        await Assert.That(utf8).IsEqualTo("utf8");
        await Assert.That(encoded).IsEqualTo("enc");
    }

    [Test]
    public async Task ReadHelpersThrowOnEndOfStreamTest()
    {
        using var stream = new MemoryStream([1, 2, 3]);
        using var emptyStream = new MemoryStream();

        Assert.Throws<EndOfStreamException>(() => stream.ReadInt32());
        Assert.Throws<EndOfStreamException>(() => stream.ReadValue<long>());
        bool canRead = emptyStream.TryReadByte(out _);

        await Assert.That(canRead).IsFalse();
    }
}
