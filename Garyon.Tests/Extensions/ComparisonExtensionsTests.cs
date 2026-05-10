using Garyon.Extensions.Comparison;
using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class ComparisonExtensionsTests
{
    private sealed record Person(string Name, int Age);
    private sealed record Employee(string Department, int Level, decimal Salary, string Name);

    [Test]
    public async Task ComparisonHelpersTest()
    {
        var peopleByName = new Person("Alice", 20).BeginCompare(new Person("Bob", 10)).By(p => p.Name);
        await Assert.That(peopleByName.AreDifferent).IsTrue();
        await Assert.That(peopleByName.AreEqual).IsFalse();

        var sameNameComparison = new Person("Alice", 20).BeginCompare(new Person("Alice", 25)).By(p => p.Name).ThenBy(p => p.Age);
        await Assert.That(sameNameComparison.AreDifferent).IsTrue();
        await Assert.That(sameNameComparison.Result).IsLessThan(0);

        var selfComparison = 5.BeginCompare(5).Self();
        await Assert.That(selfComparison.AreEqual).IsTrue();
        await Assert.That(selfComparison.Result).IsEqualTo(0);
    }

    [Test]
    public async Task DescendingComparisonHelpersTest()
    {
        var levelComparison = new Employee("Engineering", 3, 100_000m, "Alice")
            .BeginCompare(new Employee("Engineering", 2, 120_000m, "Bob"))
            .By(static e => e.Department)
            .ThenByDesc(static e => e.Level);

        await Assert.That(levelComparison.Result).IsLessThan(0);

        var salaryComparison = new Employee("Engineering", 3, 100_000m, "Alice")
            .BeginCompare(new Employee("Engineering", 3, 120_000m, "Bob"))
            .By(static e => e.Department)
            .ThenByDesc(static e => e.Level)
            .ThenByDesc(static e => e.Salary)
            .ThenBy(static e => e.Name);

        await Assert.That(salaryComparison.Result).IsGreaterThan(0);
    }

    [Test]
    public async Task DescendingComparisonHelpersSortCollectionTest()
    {
        List<Employee> employees =
        [
            new("Engineering", 3, 100_000m, "Alice"),
            new("Engineering", 3, 120_000m, "Bob"),
            new("Engineering", 2, 150_000m, "Carol"),
            new("Design", 4, 95_000m, "Dora"),
        ];

        employees.Sort(static (left, right) => left.BeginCompare(right)
            .By(static e => e.Department)
            .ThenByDesc(static e => e.Level)
            .ThenByDesc(static e => e.Salary)
            .ThenBy(static e => e.Name)
            .Result);

        await Assert.That(employees[0].Name).IsEqualTo("Dora");
        await Assert.That(employees[1].Name).IsEqualTo("Bob");
        await Assert.That(employees[2].Name).IsEqualTo("Alice");
        await Assert.That(employees[3].Name).IsEqualTo("Carol");
    }

    [Test]
    public async Task AsyncComparisonHelpersTest()
    {
        var personLeft = new Person("Alice", 20);
        var personRight = new Person("Bob", 10);

        var nameCallCount = 0;
        ValueTask<string> GetNameAsync(Person p)
        {
            nameCallCount++;
            return ValueTask.FromResult(p.Name);
        }

        var nameComparison = await personLeft.BeginCompare(personRight).By(GetNameAsync);
        await Assert.That(nameComparison.AreDifferent).IsTrue();
        await Assert.That(nameCallCount).IsEqualTo(2);

        var ageCallCount = 0;
        Task<int> GetAgeAsync(Person p)
        {
            ageCallCount++;
            return Task.FromResult(p.Age);
        }

        var differentByName = personLeft.BeginCompare(personRight).By(static p => p.Name);
        var thenByAge = differentByName.ThenBy(GetAgeAsync);
        await Assert.That(ageCallCount).IsEqualTo(0);
        await Assert.That((await thenByAge).Result).IsEqualTo(differentByName.Result);

        var skippedAsyncCallCount = 0;
        Task<int> GetAgeAsyncSkipped(Person p)
        {
            skippedAsyncCallCount++;
            return Task.FromResult(p.Age);
        }

        var skippedResult = await differentByName
            .ThenByAsync(GetAgeAsyncSkipped);

        await Assert.That(skippedAsyncCallCount).IsEqualTo(0);
        await Assert.That(skippedResult).IsEqualTo(differentByName.Result);

        var equalByName = new Person("Alice", 20).BeginCompare(new Person("Alice", 25)).By(static p => p.Name);
        var equalAgeCallCount = 0;
        ValueTask<int> GetAgeValueTaskAsync(Person p)
        {
            equalAgeCallCount++;
            return ValueTask.FromResult(p.Age);
        }

        var thenByAgeWhenEqual = await equalByName.ThenBy(GetAgeValueTaskAsync);
        await Assert.That(equalAgeCallCount).IsEqualTo(2);
        await Assert.That(thenByAgeWhenEqual.Result).IsLessThan(0);

        var descAgeComparison = await equalByName.ThenByDesc(GetAgeAsync);
        await Assert.That(descAgeComparison.Result).IsGreaterThan(0);

        var fluentRankCallCount = 0;
        ValueTask<int> GetRankAlwaysEqualAsync(Person p)
        {
            fluentRankCallCount++;
            return ValueTask.FromResult(0);
        }

        var fluentAgeCallCount = 0;
        int GetAge(Person p)
        {
            fluentAgeCallCount++;
            return p.Age;
        }

        var fluentResult = await equalByName
            .ThenByAsync(GetRankAlwaysEqualAsync)
            .ThenBy(GetAge);

        await Assert.That(fluentRankCallCount).IsEqualTo(2);
        await Assert.That(fluentAgeCallCount).IsEqualTo(2);
        await Assert.That(fluentResult).IsLessThan(0);
    }
}
