using Garyon.Extensions;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class StringBuilderExtensionsTests
{
    [Test]
    public async Task AppendLineOverloadsTest()
    {
        var builder = new StringBuilder();
        builder.AppendLine(['a', 'b']);
        builder.AppendLine('!');
        builder.AppendLine(123);
        builder.AppendLines((IEnumerable)new ArrayList { "x", 5 });
        builder.AppendLines("y", 6);

        var expected = string.Join(System.Environment.NewLine, new[] { "ab", "!", "123", "x", "5", "y", "6", string.Empty });
        await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendUpperAndLowerTest()
    {
        var builder = new StringBuilder();
        builder.AppendUpper("Abc");
        builder.Append('|');
        builder.AppendLower("AbC");

        await Assert.That(builder.ToString()).IsEqualTo("ABC|abc");
    }

    [Test]
    public async Task AppendLineCountTest()
    {
        var builder = new StringBuilder("x");
        builder.AppendLineCount(2);

        await Assert.That(builder.ToString()).IsEqualTo($"x{System.Environment.NewLine}{System.Environment.NewLine}");
    }

    [Test]
    public async Task CopyTest()
    {
        var original = new StringBuilder("abc");
        var copy = original.Copy();
        copy.Append('d');

        await Assert.That(original.ToString()).IsEqualTo("abc");
        await Assert.That(copy.ToString()).IsEqualTo("abcd");
    }

    [Test]
    public async Task RemoveHelpersTest()
    {
        var builder = new StringBuilder("abcdef");
        builder.Remove(3);
        await Assert.That(builder.ToString()).IsEqualTo("abc");

        builder.RemoveLast();
        await Assert.That(builder.ToString()).IsEqualTo("ab");

        builder.RemoveLast(2);
        await Assert.That(builder.ToString()).IsEqualTo(string.Empty);

        builder.RemoveLastOrNone();
        await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task LastHelpersTest()
    {
        var builder = new StringBuilder("abc;");

        await Assert.That(builder.Last()).IsEqualTo(';');
        await Assert.That(builder.LastOrNull()).IsEqualTo(';');
        await Assert.That(builder.EndsWith(';')).IsTrue();

        builder.RemoveLastIfEndsWith(';');
        await Assert.That(builder.ToString()).IsEqualTo("abc");
        await Assert.That(new StringBuilder().LastOrNull()).IsNull();
        await Assert.That(((StringBuilder?)null).LastOrNull()).IsNull();
    }

    [Test]
    public async Task TrimHelpersTest()
    {
        var builder = new StringBuilder("  abc  ");
        builder.TrimStart();
        await Assert.That(builder.ToString()).IsEqualTo("abc  ");

        builder.TrimEnd();
        await Assert.That(builder.ToString()).IsEqualTo("abc");
    }

    [Test]
    public async Task AppendSliceAndReplaceTest()
    {
        var builder = new StringBuilder();
        builder.Append("abcdef", 2);
        builder.Append('|');
        builder.Append("uvwxyz", 1, 3);
        builder.Replace("--", 2, 3);

        await Assert.That(builder.ToString()).IsEqualTo("cd--vwx");
    }

    [Test]
    public async Task SubstringHelpersTest()
    {
        var builder = new StringBuilder("abcdef");

        await Assert.That(builder.Substring(1, 3)).IsEqualTo("bcd");
        await Assert.That(builder.SubstringBuilder(2, 2).ToString()).IsEqualTo("cd");
        await Assert.That(builder.SubstringStart(2)).IsEqualTo("ab");
        await Assert.That(builder.SubstringStartBuilder(3).ToString()).IsEqualTo("abc");
        await Assert.That(builder.SubstringLast(2)).IsEqualTo("ef");
        await Assert.That(builder.SubstringLastBuilder(3).ToString()).IsEqualTo("def");
        await Assert.That(builder.EndsWith("ef")).IsTrue();
    }
}
