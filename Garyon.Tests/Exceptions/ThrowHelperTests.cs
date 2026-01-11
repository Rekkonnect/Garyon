using Garyon.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Exceptions;

public class ThrowHelperTests
{
    private const string exceptionLover = "I love exceptions";
    private const string aggregateExceptionLover = "I love aggregate exceptions";

    [Test]
    public async Task ThrowTest()
    {
        var thrown = new Exception();
        await TestThrowing(Throw, HandleException);

        void Throw() => ThrowHelper.Throw(thrown);
        async Task HandleException(Exception e)
        {
            await Assert.That(e).IsEqualTo(thrown);
        }
    }

    [Test]
    public async Task ThrowGenericTest()
    {
        await TestThrowing(Throw, HandleException);

        void Throw() => ThrowHelper.Throw<ArgumentException>();
        async Task HandleException(Exception e)
        {
            await Assert.That(e).IsTypeOf<ArgumentException>();
        }
    }
    [Test]
    public async Task ThrowGenericMessageTest()
    {
        await TestThrowing(Throw, HandleException);

        void Throw() => ThrowHelper.Throw<ArgumentException>(exceptionLover);
        async Task HandleException(Exception e)
        {
            await Assert.That(e).IsTypeOf<ArgumentException>();
            await Assert.That(e.Message).IsEqualTo(exceptionLover);
        }
    }
    [Test]
    public async Task ThrowGenericMessageInnerTest()
    {
        var inner = new InvalidOperationException();
        await TestThrowing(Throw, HandleException);

        void Throw() => ThrowHelper.Throw<ArgumentException>(exceptionLover, inner);
        async Task HandleException(Exception e)
        {
            await Assert.That(e).IsTypeOf<ArgumentException>();
            await Assert.That(e.Message).IsEqualTo(exceptionLover);
            await Assert.That(e.InnerException).IsEqualTo(inner);
        }
    }

    [Test]
    public async Task ThrowAggregateTest()
    {
        var inners = new Exception[] { new InvalidOperationException(), new ArithmeticException(), new OverflowException() };
        await TestThrowing(Throw, HandleException);

        void Throw() => ThrowHelper.ThrowAggregate(aggregateExceptionLover, inners);
        async Task HandleException(Exception e)
        {
            var a = await Assert.That(e).IsTypeOf<AggregateException>();
            await Assert.That(e.Message.Contains(aggregateExceptionLover)).IsTrue();
            await Assert.That(a.InnerExceptions).IsEquivalentTo(inners);
        }
    }
    [Test]
    public async Task ThrowAggregateGenericArrayTest()
    {
        var inners = new Exception[] { new InvalidOperationException(), new ArithmeticException(), new OverflowException() };
        await TestThrowing(Throw, HandleException);

        void Throw() => ThrowHelper.ThrowAggregate<ArrayAggregateException>(aggregateExceptionLover, inners);
        async Task HandleException(Exception e)
        {
            var a = await Assert.That(e).IsTypeOf<ArrayAggregateException>();
            await Assert.That(e.Message.Contains(aggregateExceptionLover)).IsTrue();
            await Assert.That(a.InnerExceptions).IsEquivalentTo(inners);
        }
    }
    [Test]
    public async Task ThrowAggregateGenericIEnumerableTest()
    {
        var inners = new Exception[] { new InvalidOperationException(), new ArithmeticException(), new OverflowException() };
        await TestThrowing(Throw, HandleException);

        void Throw() => ThrowHelper.ThrowAggregate<IEnumerableAggregateException>(aggregateExceptionLover, inners);
        async Task HandleException(Exception e)
        {
            var a = await Assert.That(e).IsTypeOf<IEnumerableAggregateException>();
            await Assert.That(e.Message.Contains(aggregateExceptionLover)).IsTrue();
            await Assert.That(a.InnerExceptions).IsEquivalentTo(inners);
        }
    }
    [Test]
    public async Task ThrowAggregateGenericBadExceptionTest()
    {
        var inners = new Exception[] { new InvalidOperationException(), new ArithmeticException(), new OverflowException() };
        await TestThrowing(Throw, HandleException);

        void Throw() => ThrowHelper.ThrowAggregate<BadAggregateException>(aggregateExceptionLover, inners);
        async Task HandleException(Exception e)
        {
            await Assert.That(e).IsTypeOf<MissingMethodException>();
        }
    }

    private async Task TestThrowing(Action thrower, Func<Exception, Task> exceptionHandler)
    {
        try
        {
            thrower();
        }
        catch (Exception e)
        {
            await exceptionHandler(e);
        }
    }

    private class ArrayAggregateException : AggregateException
    {
        public ArrayAggregateException(string message) : base(message) { }
        public ArrayAggregateException(string message, Exception[] innerExceptions) : base(message, innerExceptions) { }
    }
    private class IEnumerableAggregateException : AggregateException
    {
        public IEnumerableAggregateException(string message) : base(message) { }
        public IEnumerableAggregateException(string message, IEnumerable<Exception> innerExceptions) : base(message, innerExceptions) { }
    }
    private class BadAggregateException : AggregateException
    {
        public BadAggregateException(string message) : base(message) { }
    }
}