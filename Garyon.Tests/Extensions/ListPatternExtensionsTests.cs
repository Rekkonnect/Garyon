using Garyon.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class ListPatternExtensionsTests
{
    private class Animal { }
    private sealed class Dog : Animal { public string Name { get; init; } = string.Empty; }

    [Test]
    public async Task TryGetSingleTest()
    {
        IReadOnlyList<Animal> animalsList = [new Dog { Name = "Rex" }];
        var gotSingleList = animalsList.TryGetSingle<Animal, Dog>(out var dogFromList);
        await Assert.That(gotSingleList).IsTrue();
        await Assert.That(dogFromList!.Name).IsEqualTo("Rex");

        Animal[] animalsArray = [new Dog { Name = "Max" }];
        var gotSingleArray = animalsArray.TryGetSingle<Animal, Dog>(out var dogFromArray);
        await Assert.That(gotSingleArray).IsTrue();
        await Assert.That(dogFromArray!.Name).IsEqualTo("Max");

        Span<Animal> animalsSpan = [new Dog { Name = "Bolt" }];
        var gotSingleSpan = animalsSpan.TryGetSingle<Animal, Dog>(out var dogFromSpan);
        await Assert.That(gotSingleSpan).IsTrue();
        await Assert.That(dogFromSpan!.Name).IsEqualTo("Bolt");

        ReadOnlySpan<Animal> animalsReadOnlySpan = [new Dog { Name = "Luna" }];
        var gotSingleReadOnlySpan = animalsReadOnlySpan.TryGetSingle<Animal, Dog>(out var dogFromReadOnlySpan);
        await Assert.That(gotSingleReadOnlySpan).IsTrue();
        await Assert.That(dogFromReadOnlySpan!.Name).IsEqualTo("Luna");

        IReadOnlyList<Animal> wrongList = [new Animal(), new Dog()];
        await Assert.That(wrongList.TryGetSingle<Animal, Dog>(out _)).IsFalse();
    }
}
