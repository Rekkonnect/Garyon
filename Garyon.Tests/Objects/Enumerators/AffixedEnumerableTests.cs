using Garyon.Objects.Enumerators;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Objects.Enumerators;

public class AffixedEnumerableTests
{
    [Test]
    public async Task TestPrefix()
    {
        int[] main = [1, 3, 458, 1239];
        var prefixed = main.WithPrefix(1, 2, 3);
        var prefixedArray = prefixed.ToArray();
        await Assert.That(prefixedArray).IsEquivalentTo([1, 2, 3, 1, 3, 458, 1239]);
    }
    [Test]
    public async Task TestSuffix()
    {
        int[] main = [1, 3, 458, 1239];
        var suffixed = main.WithSuffix(1, 2, 3);
        var suffixedArray = suffixed.ToArray();
        await Assert.That(suffixedArray).IsEquivalentTo([1, 3, 458, 1239, 1, 2, 3]);
    }
    [Test]
    public async Task TestEmptyAffixes()
    {
        int[] main = [1, 3, 458, 1239];
        var affixed = main
            .WithPrefix()
            .WithSuffix();
        var suffixedArray = affixed.ToArray();
        await Assert.That(suffixedArray).IsEquivalentTo(main);
    }
    [Test]
    public async Task TestAffix()
    {
        int[] main = [1, 3, 458, 1239];
        var affixed = main
            .WithPrefix(4, 5, 6)
            .WithSuffix(1, 2, 3);
        var suffixedArray = affixed.ToArray();
        await Assert.That(suffixedArray).IsEquivalentTo([4, 5, 6, 1, 3, 458, 1239, 1, 2, 3]);
    }
}