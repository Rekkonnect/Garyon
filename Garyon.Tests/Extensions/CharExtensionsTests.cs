using Garyon.Extensions;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class CharExtensionsTests
{
    [Test]
    public async Task EnglishLetterChecksTest()
    {
        await Assert.That('a'.IsLowerCaseEnglishLetter()).IsTrue();
        await Assert.That('A'.IsUpperCaseEnglishLetter()).IsTrue();
        await Assert.That('A'.IsEnglishLetter()).IsTrue();
        await Assert.That('4'.IsEnglishLetterOrDigit()).IsTrue();
    }

    [Test]
    public async Task NumericValueHelpersTest()
    {
        await Assert.That('4'.GetNumericValueInteger()).IsEqualTo(4);
        await Assert.That('x'.GetNumericValueInteger()).IsEqualTo(-1);
        await Assert.That('7'.GetNumericalValue()).IsEqualTo(7);
    }

    [Test]
    public async Task ValidHexCharacterTest()
    {
        await Assert.That('F'.IsValidHexCharacter()).IsTrue();
        await Assert.That('g'.IsValidHexCharacter()).IsFalse();
    }

    [Test]
    public async Task InvariantCasingTest()
    {
        await Assert.That('Z'.ToLowerInvariant()).IsEqualTo('z');
        await Assert.That('a'.ToUpperInvariant()).IsEqualTo('A');
    }

    [Test]
    public async Task Base64CharacterTest()
    {
        await Assert.That('+'.IsBase64Character()).IsTrue();
        await Assert.That('?'.IsBase64Character()).IsFalse();
    }
}
