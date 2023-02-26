using Garyon.Reflection;
using NUnit.Framework;
using System;

namespace Garyon.Tests.Reflection;

public class CodedEnumInfoTests
{
    [Test]
    public void GetCodeObject()
    {
        AssertCode(ColorCodes.Red, Color.Red);
        AssertCode(ColorCodes.Green, Color.Green);
        AssertCode(ColorCodes.Blue, Color.Blue);

        AssertThrow(Color.NoCode);
        AssertThrow(null);

        static void AssertCode(string targetCode, Enum instance)
        {
            Assert.AreEqual(targetCode, CodedEnumInfo.GetCode(instance));
        }
        static void AssertThrow(Enum instance)
        {
            Assert.Catch(() => CodedEnumInfo.GetCode(instance));
        }
    }
    [Test]
    public void GetCode()
    {
        AssertCode(ColorCodes.Red, Color.Red);
        AssertCode(ColorCodes.Green, Color.Green);
        AssertCode(ColorCodes.Blue, Color.Blue);

        AssertThrow(Color.NoCode);

        static void AssertCode<TEnum>(string targetCode, TEnum instance)
            where TEnum : struct, Enum
        {
            Assert.AreEqual(targetCode, CodedEnumInfo.GetCode(instance));
        }
        static void AssertThrow<TEnum>(TEnum instance)
            where TEnum : struct, Enum
        {
            Assert.Catch(() => CodedEnumInfo.GetCode(instance));
        }
    }
    [Test]
    public void ParseCodeObject()
    {
        AssertCode(ColorCodes.Red, Color.Red);
        AssertCode(ColorCodes.Green, Color.Green);
        AssertCode(ColorCodes.Blue, Color.Blue);

        AssertThrow("GARBAGE", typeof(Color));
        AssertThrow(null, typeof(Color));

        static void AssertCode(string code, Enum targetValue)
        {
            Assert.AreEqual(targetValue, CodedEnumInfo.ParseCode(targetValue.GetType(), code));
        }
        static void AssertThrow(string code, Type enumType)
        {
            Assert.Catch(() => CodedEnumInfo.ParseCode(enumType, code));
        }
    }
    [Test]
    public void ParseCode()
    {
        AssertCode(ColorCodes.Red, Color.Red);
        AssertCode(ColorCodes.Green, Color.Green);
        AssertCode(ColorCodes.Blue, Color.Blue);

        AssertThrow<Color>("GARBAGE");
        AssertThrow<Color>(null);

        static void AssertCode<TEnum>(string code, TEnum targetValue)
            where TEnum : struct, Enum
        {
            Assert.AreEqual(targetValue, CodedEnumInfo.ParseCode<TEnum>(code));
        }
        static void AssertThrow<TEnum>(string code)
            where TEnum : struct, Enum
        {
            Assert.Catch(() => CodedEnumInfo.ParseCode<TEnum>(code));
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
