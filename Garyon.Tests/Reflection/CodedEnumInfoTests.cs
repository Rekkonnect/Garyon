using Garyon.Reflection;
using System;
using System.Threading.Tasks;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;

namespace Garyon.Tests.Reflection;

public class CodedEnumInfoTests
{
    [Test]
    public async Task GetCodeObject()
    {
        await AssertCode(ColorCodes.Red, Color.Red);
        await AssertCode(ColorCodes.Green, Color.Green);
        await AssertCode(ColorCodes.Blue, Color.Blue);

        await AssertThrow(Color.NoCode);
        await AssertThrow(null);

        static async Task AssertCode(string targetCode, Enum instance)
        {
            await Assert.That(CodedEnumInfo.GetCode(instance)).IsEqualTo(targetCode);
        }
        static async Task AssertThrow(Enum instance)
        {
            await Assert.That(() => CodedEnumInfo.GetCode(instance)).ThrowsException();
        }
    }
    [Test]
    public async Task GetCode()
    {
        await AssertCode(ColorCodes.Red, Color.Red);
        await AssertCode(ColorCodes.Green, Color.Green);
        await AssertCode(ColorCodes.Blue, Color.Blue);

        await AssertThrow(Color.NoCode);

        static async Task AssertCode<TEnum>(string targetCode, TEnum instance)
            where TEnum : struct, Enum
        {
            await Assert.That(CodedEnumInfo.GetCode(instance)).IsEqualTo(targetCode);
        }
        static async Task AssertThrow<TEnum>(TEnum instance)
            where TEnum : struct, Enum
        {
            await Assert.That(() => CodedEnumInfo.GetCode(instance)).ThrowsException();
        }
    }
    [Test]
    public async Task ParseCodeObject()
    {
        await AssertCode(ColorCodes.Red, Color.Red);
        await AssertCode(ColorCodes.Green, Color.Green);
        await AssertCode(ColorCodes.Blue, Color.Blue);

        await AssertThrow("GARBAGE", typeof(Color));
        await AssertThrow(null, typeof(Color));

        static async Task AssertCode(string code, Enum targetValue)
        {
            await Assert.That(CodedEnumInfo.ParseCode(targetValue.GetType(), code)).IsEqualTo(targetValue);
        }
        static async Task AssertThrow(string code, Type enumType)
        {
            await Assert.That(() => CodedEnumInfo.ParseCode(enumType, code)).ThrowsException();
        }
    }
    [Test]
    public async Task ParseCode()
    {
        await AssertCode(ColorCodes.Red, Color.Red);
        await AssertCode(ColorCodes.Green, Color.Green);
        await AssertCode(ColorCodes.Blue, Color.Blue);

        await AssertThrow<Color>("GARBAGE");
        await AssertThrow<Color>(null);

        static async Task AssertCode<TEnum>(string code, TEnum targetValue)
            where TEnum : struct, Enum
        {
            await Assert.That(CodedEnumInfo.ParseCode<TEnum>(code)).IsEqualTo(targetValue);
        }
        static async Task AssertThrow<TEnum>(string code)
            where TEnum : struct, Enum
        {
            await Assert.That(() => CodedEnumInfo.ParseCode<TEnum>(code)).ThrowsException();
        }
    }

    private static class ColorCodes
    {
        public const string
            Red = "RED",
            Green = "GREEN",
            Blue = "BLUE",
            Yellow = "YELLOW",
            Orange = "ORANGE";
    }

    private enum Color
    {
        [Code(ColorCodes.Red)]
        Red,
        [Code(ColorCodes.Green)]
        Green,
        [Code(ColorCodes.Blue)]
        Blue,
        [Code(ColorCodes.Yellow)]
        Yellow,
        [Code(ColorCodes.Orange)]
        Orange,

        NoCode,
    }
}