using Garyon.Objects.Enumerators;
using NUnit.Framework;
using System.Linq;

namespace Garyon.Tests.Objects.Enumerators;

public class AffixedEnumerableTests
{
    [Test]
    public void TestPrefix()
    {
        int[] main = { 1, 3, 458, 1239 };
        var prefixed = main.WithPrefix(1, 2, 3);
        var prefixedArray = prefixed.ToArray();
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 1, 3, 458, 1239 }, prefixedArray);
    }
    [Test]
    public void TestSuffix()
    {
        int[] main = { 1, 3, 458, 1239 };
        var suffixed = main.WithSuffix(1, 2, 3);
        var suffixedArray = suffixed.ToArray();
        CollectionAssert.AreEqual(new[] { 1, 3, 458, 1239, 1, 2, 3 }, suffixedArray);
    }
    [Test]
    public void TestEmptyAffixes()
    {
        int[] main = { 1, 3, 458, 1239 };
        var affixed = main.WithPrefix()
                          .WithSuffix();
        var suffixedArray = affixed.ToArray();
        CollectionAssert.AreEqual(main, suffixedArray);
    }
    [Test]
    public void TestAffix()
    {
        int[] main = { 1, 3, 458, 1239 };
        var affixed = main.WithPrefix(4, 5, 6)
                          .WithSuffix(1, 2, 3);
        var suffixedArray = affixed.ToArray();
        CollectionAssert.AreEqual(new[] { 4, 5, 6, 1, 3, 458, 1239, 1, 2, 3 }, suffixedArray);
    }
}
