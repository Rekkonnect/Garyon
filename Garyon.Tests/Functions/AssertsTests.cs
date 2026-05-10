using Garyon.Exceptions;
using Garyon.Functions;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Functions;

public class AssertsTests
{
    [Test]
    public async Task TrueFalseNullAndNotNullTest()
    {
        Asserts.True(true);
        Asserts.False(false);
        Asserts.Null<string>(null);
        Asserts.NotNull("value");

        var trueException = Assert.Throws<AssertionException>(() => Asserts.True(false));
        var falseException = Assert.Throws<AssertionException>(() => Asserts.False(true));
        var nullException = Assert.Throws<AssertionException>(() => Asserts.Null("value"));
        var notNullException = Assert.Throws<AssertionException>(() => Asserts.NotNull<string>(null));

        await Assert.That(trueException.Message.Contains("to be true")).IsTrue();
        await Assert.That(falseException.Message.Contains("to be false")).IsTrue();
        await Assert.That(nullException.Message.Contains("to be null")).IsTrue();
        await Assert.That(notNullException.Message.Contains("to not be null")).IsTrue();
    }

    [Test]
    public async Task TypeDefaultAndEqualityAssertionsTest()
    {
        var typed = Asserts.OfType<string>("value");
        Asserts.Default(0);
        Asserts.NotDefault(1);
        Asserts.Equal(5, 5);
        Asserts.NotEqual(5, 6);

        var typeException = Assert.Throws<AssertionException>(() => Asserts.OfType<int>("value"));
        var defaultException = Assert.Throws<AssertionException>(() => Asserts.Default(1));
        var notDefaultException = Assert.Throws<AssertionException>(() => Asserts.NotDefault(0));
        var equalException = Assert.Throws<AssertionException>(() => Asserts.Equal(1, 2));
        var notEqualException = Assert.Throws<AssertionException>(() => Asserts.NotEqual(1, 1));

        await Assert.That(typed).IsEqualTo("value");
        await Assert.That(typeException.Message.Contains("to be of type")).IsTrue();
        await Assert.That(defaultException.Message.Contains("default value")).IsTrue();
        await Assert.That(notDefaultException.Message.Contains("not be the default value")).IsTrue();
        await Assert.That(equalException.Message.Contains("to be equal")).IsTrue();
        await Assert.That(notEqualException.Message.Contains("to not be equal")).IsTrue();
    }

    [Test]
    public async Task ComparisonAssertionsTest()
    {
        Asserts.LessThan(1, 2);
        Asserts.LessThanOrEqual(2, 2);
        Asserts.GreaterThan(3, 2);
        Asserts.GreaterThanOrEqual(3, 3);

        var lessThanException = Assert.Throws<AssertionException>(() => Asserts.LessThan(2, 1));
        var lessThanOrEqualException = Assert.Throws<AssertionException>(() => Asserts.LessThanOrEqual(2, 1));
        var greaterThanException = Assert.Throws<AssertionException>(() => Asserts.GreaterThan(1, 2));
        var greaterThanOrEqualException = Assert.Throws<AssertionException>(() => Asserts.GreaterThanOrEqual(1, 2));

        await Assert.That(lessThanException.Message.Contains("less than")).IsTrue();
        await Assert.That(lessThanOrEqualException.Message.Contains("less than or equal")).IsTrue();
        await Assert.That(greaterThanException.Message.Contains("greater than")).IsTrue();
        await Assert.That(greaterThanOrEqualException.Message.Contains("greater than or equal")).IsTrue();
    }
}
