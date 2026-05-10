using Garyon.Reflection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Reflection;

public class AttributeMappingTests
{
    [Test]
    public async Task GetCode()
    {
        var enumToCode = AttributeMapping
            .ForEnum<Color>()
            .WithAttributeKey<CodeAttribute, string>(static a => a.Code)
            .Build();

        await AssertCode(ColorCodes.Red, Color.Red);
        await AssertCode(ColorCodes.Green, Color.Green);
        await AssertCode(ColorCodes.Blue, Color.Blue);

        await AssertThrow(Color.NoCode);

        async Task AssertCode(string targetCode, Color instance)
        {
            await Assert.That(enumToCode[instance]).IsEqualTo(targetCode);
        }
        async Task AssertThrow(Color instance)
        {
            await Assert.That(() => _ = enumToCode[instance]).ThrowsException();
        }
    }

    [Test]
    public async Task ParseCode()
    {
        var enumToCode = AttributeMapping
            .ForEnum<Color>()
            .WithAttributeKey<CodeAttribute, string>(static a => a.Code)
            .Build();

        var codeToEnum = BuildReverse(enumToCode, StringComparer.Ordinal);

        await AssertCode(ColorCodes.Red, Color.Red);
        await AssertCode(ColorCodes.Green, Color.Green);
        await AssertCode(ColorCodes.Blue, Color.Blue);

        await AssertThrow("GARBAGE");
        await AssertThrow(null);

        async Task AssertCode(string code, Color targetValue)
        {
            await Assert.That(codeToEnum[code]).IsEqualTo(targetValue);
        }
        async Task AssertThrow(string? code)
        {
            await Assert.That(() => _ = codeToEnum[code!]).ThrowsException();
        }
    }

    private static Dictionary<string, TEnum> BuildReverse<TEnum>(Dictionary<TEnum, string> enumToCode, IEqualityComparer<string> comparer)
        where TEnum : struct, Enum
    {
        var dictionary = new Dictionary<string, TEnum>(enumToCode.Count, comparer);
        foreach (var pair in enumToCode)
        {
            dictionary.Add(pair.Value, pair.Key);
        }
        return dictionary;
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
