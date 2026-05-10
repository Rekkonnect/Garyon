using Garyon.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class GeneralObjectExtensionsTests
{
    private class BaseNode
    {
        public BaseNode? Parent { get; init; }
    }

    private sealed class DerivedNode : BaseNode
    {
        public string Name { get; init; } = string.Empty;
    }

    [Test]
    public async Task IntoSameTypeTest()
    {
        var value = 42;
        var returned = value.Into(out int captured);

        await Assert.That(returned).IsEqualTo(42);
        await Assert.That(captured).IsEqualTo(42);
    }

    [Test]
    public async Task EnumerateRecursivelyTest()
    {
        var root = new BaseNode();
        var child = new BaseNode { Parent = root };
        var leaf = new BaseNode { Parent = child };

        var results = leaf.EnumerateRecursively(n => n.Parent).ToArray();

        await Assert.That(results.Length).IsEqualTo(2);
        await Assert.That(ReferenceEquals(results[0], child)).IsTrue();
        await Assert.That(ReferenceEquals(results[1], root)).IsTrue();
    }

    [Test]
    public async Task DefaultIfTest()
    {
        string source = string.Empty;
        var defaulted = source.DefaultIf(string.IsNullOrEmpty);
        var preserved = "x".DefaultIf(string.IsNullOrEmpty);

        await Assert.That(defaulted).IsNull();
        await Assert.That(preserved).IsEqualTo("x");
    }
}
