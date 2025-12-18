using Garyon.Extensions;
using System.Threading.Tasks;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;

namespace Garyon.Tests.Extensions;

public class StringExtensions
{
    [Test]
    public async Task SubstringUntilFirstString()
    {
        const string source = "[ERROR]: Something: Another: More";

        var substring = source.SubstringUntilFirst(": ");
        const string expected = "[ERROR]";
        await Assert.That(substring).IsEqualTo(expected);
    }
    [Test]
    public async Task SubstringUntilFirstChar()
    {
        const string source = "[ERROR]: Something: Another: More";

        var substring = source.SubstringUntilFirst(':');
        const string expected = "[ERROR]";
        await Assert.That(substring).IsEqualTo(expected);
    }
}