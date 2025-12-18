using Garyon.Extensions;
using NUnit.Framework;

namespace Garyon.Tests.Extensions;

public class StringExtensions
{
    [Test]
    public void SubstringUntilFirstString()
    {
        const string source = "[ERROR]: Something: Another: More";

        var substring = source.SubstringUntilFirst(": ");
        const string expected = "[ERROR]";
        Assert.AreEqual(expected, substring);
    }
    [Test]
    public void SubstringUntilFirstChar()
    {
        const string source = "[ERROR]: Something: Another: More";

        var substring = source.SubstringUntilFirst(':');
        const string expected = "[ERROR]";
        Assert.AreEqual(expected, substring);
    }
}
