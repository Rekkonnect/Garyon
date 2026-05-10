using Garyon.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class IEnumerableExtensionsAdditionalTests
{
    [Test]
    public async Task EnumerableHelpersWithEmptyAndNullableValuesTest()
    {
        System.Array.Empty<int>().Dissect(i => i > 0, out var matched, out var unmatched);

        await Assert.That(matched).IsEmpty();
        await Assert.That(unmatched).IsEmpty();
        await Assert.That(new int?[] { 1, null, 2 }.GetValuedElements().SequenceEqual([1, 2])).IsTrue();
        await Assert.That(new string?[] { "a", null }.AsNonNull().SequenceEqual(["a", null!])).IsTrue();
    }

    [Test]
    public async Task CountCachingAndNonEnumeratedCountHelpersTest()
    {
        var cached = Yield(1, 2, 3).WithCountCaching();

        await Assert.That(cached.ForceCount()).IsEqualTo(3);
        await Assert.That(cached.SequenceEqual([1, 2, 3])).IsTrue();
        await Assert.That(new[] { 1, 2, 3 }.GetNonEnumeratedCountOrDefault()).IsEqualTo(3);
        await Assert.That(Yield(1, 2, 3).GetNonEnumeratedCountOrDefault()).IsEqualTo(0);
        await Assert.That(new[] { 1, 2, 3 }.EqualsNonEnumeratedCount(new List<int> { 4, 5, 6 })).IsEqualTo(true);
        await Assert.That(Yield(1, 2, 3).EqualsNonEnumeratedCount([1, 2, 3])).IsEqualTo(false);
    }

    [Test]
    public async Task DissectAndUntilFirstRecursiveTest()
    {
        var source = new[] { 1, 2, 3, 4 };
        source.Dissect(i => i % 2 is 0, out var matched, out var unmatched);

        await Assert.That(matched.SequenceEqual([2, 4])).IsTrue();
        await Assert.That(unmatched.SequenceEqual([1, 3])).IsTrue();
        await Assert.That(new[] { 1, 2, 3, 2, 4 }.UntilFirstRecursive().SequenceEqual([1, 2, 3])).IsTrue();
    }

    [Test]
    public async Task ConcatAndFlattenVariantsTest()
    {
        var concatenated = new[] { 1 }.ConcatMultiple([2, 3], [4]);
        var concatenatedSingle = new[] { 1, 2 }.ConcatSingleValue(3);
        IEnumerable<IEnumerable<int>> source2D =
        [
            [1, 2],
            System.Array.Empty<int>(),
            [3, 4],
        ];
        var flattened2D = source2D.FlattenInstant();
        IEnumerable<IEnumerable<IEnumerable<int>>> source3D =
        [
            new[]
            {
                [1, 2],
                System.Array.Empty<int>(),
            },
            [
                new[] { 3, 4 },
            ],
        ];
        var flattened3D = source3D.Flatten();

        await Assert.That(concatenated.SequenceEqual([1, 2, 3, 4])).IsTrue();
        await Assert.That(concatenatedSingle.SequenceEqual([1, 2, 3])).IsTrue();
        await Assert.That(flattened2D.SequenceEqual([1, 2, 3, 4])).IsTrue();
        await Assert.That(flattened3D.SequenceEqual([1, 2, 3, 4])).IsTrue();
    }

    [Test]
    public async Task CartesianProductAndProductHelpersTest()
    {
        var values = new[] { 2, 3, 5 };

        var cartesian = values.CartesianProduct().ToArray();
        var materialized = values.MaterializedCartesianProduct();
        var excludingSame = values.MaterializedCartesianProductExcludingSame();
        var homogenous = values.MaterializedHomogenousCartesianProduct();

        await Assert.That(cartesian.SequenceEqual(
        [
            (2, 2), (2, 3), (2, 5),
            (3, 2), (3, 3), (3, 5),
            (5, 2), (5, 3), (5, 5),
        ])).IsTrue();
        await Assert.That(materialized.SequenceEqual(cartesian)).IsTrue();
        await Assert.That(excludingSame.SequenceEqual(
        [
            (2, 3), (2, 5),
            (3, 2), (3, 5),
            (5, 2), (5, 3),
        ])).IsTrue();
        await Assert.That(homogenous.SequenceEqual([(2, 3), (2, 5), (3, 5)])).IsTrue();
        await Assert.That(values.Product()).IsEqualTo(30);
        await Assert.That(new uint[] { 2, 3, 5 }.Product()).IsEqualTo(30U);
        await Assert.That(new long[] { 2, 3, 5 }.Product()).IsEqualTo(30L);
        await Assert.That(new ulong[] { 2, 3, 5 }.Product()).IsEqualTo(30UL);
        await Assert.That(values.ProductInt64()).IsEqualTo(30L);
        await Assert.That(values.ProductUInt64()).IsEqualTo(30UL);
        await Assert.That(new uint[] { 2, 3, 5 }.ProductUInt64()).IsEqualTo(30UL);
    }

    [Test]
    public async Task KeyValuePairProjectionHelpersTest()
    {
        var source = new[]
        {
            new KeyValuePair<int, string>(1, "a"),
            new KeyValuePair<int, string>(2, "bb"),
        };

        var selectedKeys = source.SelectKeys(k => $"k{k}");
        var selectedValues = source.SelectValues(v => v.Length);

        await Assert.That(selectedKeys.SequenceEqual(
        [
            new KeyValuePair<string, string>("k1", "a"),
            new KeyValuePair<string, string>("k2", "bb"),
        ])).IsTrue();
        await Assert.That(selectedValues.SequenceEqual(
        [
            new KeyValuePair<int, int>(1, 1),
            new KeyValuePair<int, int>(2, 2),
        ])).IsTrue();
    }

    [Test]
    public async Task ExtremumAndOfAnyTypeHelpersTest()
    {
        Creature[] creatures = [new Dog(), new Cat(), new Fish(), new Bird()];
        var pets = creatures.OfAnyType<Creature, Dog, Cat>().ToArray();
        var landAnimals = creatures.OfAnyType<Creature, Dog, Cat, Bird>().ToArray();
        var allCreatures = creatures.OfAnyType<Creature, Dog, Cat, Fish, Bird>().ToArray();
        object[] values = [1, "two", 3L, 4D, '5'];
        var numbers = new[] { 4, 2, 8 };

        await Assert.That(pets.Length).IsEqualTo(2);
        await Assert.That(pets[0]).IsTypeOf<Dog>();
        await Assert.That(pets[1]).IsTypeOf<Cat>();
        await Assert.That(landAnimals.Length).IsEqualTo(3);
        await Assert.That(landAnimals[0]).IsTypeOf<Dog>();
        await Assert.That(landAnimals[1]).IsTypeOf<Cat>();
        await Assert.That(landAnimals[2]).IsTypeOf<Bird>();
        await Assert.That(allCreatures.Length).IsEqualTo(4);
        await Assert.That(numbers.Extremum(Garyon.Objects.Extremum.Minimum)).IsEqualTo(2);
        await Assert.That(numbers.Extremum(Garyon.Objects.Extremum.Maximum)).IsEqualTo(8);
        await Assert.That(values.Extremum(Garyon.Objects.Extremum.Maximum, v => v.ToString()!.Length)).IsEqualTo(3);
        await Assert.That(values.Extremum(Garyon.Objects.Extremum.Minimum, v => (long?)v.ToString()!.Length)).IsEqualTo(1L);
        await Assert.That(values.Extremum<object, string>(Garyon.Objects.Extremum.Maximum, v => v.ToString())).IsEqualTo("two");
    }

    private static IEnumerable<int> Yield(params int[] values)
    {
        foreach (var value in values)
            yield return value;
    }

    private class Creature;
    private sealed class Dog : Creature;
    private sealed class Cat : Creature;
    private sealed class Fish : Creature;
    private sealed class Bird : Creature;
}
