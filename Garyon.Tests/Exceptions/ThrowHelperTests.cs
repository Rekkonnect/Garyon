using Garyon.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Garyon.Tests.Exceptions
{
    [Parallelizable(ParallelScope.Children)]
    public class ThrowHelperTests
    {
        private const string exceptionLover = "I love exceptions";
        private const string aggregateExceptionLover = "I love aggregate exceptions";

        [Test]
        public void ThrowTest()
        {
            var thrown = new Exception();
            TestThrowing(Throw, HandleException);

            void Throw() => ThrowHelper.Throw(thrown);
            void HandleException(Exception e) => Assert.AreEqual(thrown, e);
        }

        [Test]
        public void ThrowGenericTest()
        {
            TestThrowing(Throw, HandleException);

            void Throw() => ThrowHelper.Throw<ArgumentException>();
            void HandleException(Exception e) => Assert.IsInstanceOf(typeof(ArgumentException), e);
        }
        [Test]
        public void ThrowGenericMessageTest()
        {
            TestThrowing(Throw, HandleException);

            void Throw() => ThrowHelper.Throw<ArgumentException>(exceptionLover);
            void HandleException(Exception e)
            {
                Assert.IsInstanceOf(typeof(ArgumentException), e);
                Assert.AreEqual(exceptionLover, e.Message);
            }
        }
        [Test]
        public void ThrowGenericMessageInnerTest()
        {
            var inner = new InvalidOperationException();
            TestThrowing(Throw, HandleException);

            void Throw() => ThrowHelper.Throw<ArgumentException>(exceptionLover, inner);
            void HandleException(Exception e)
            {
                Assert.IsInstanceOf(typeof(ArgumentException), e);
                Assert.AreEqual(exceptionLover, e.Message);
                Assert.AreEqual(inner, e.InnerException);
            }
        }

        [Test]
        public void ThrowAggregateTest()
        {
            var inners = new Exception[] { new InvalidOperationException(), new ArithmeticException(), new OverflowException() };
            TestThrowing(Throw, HandleException);

            void Throw() => ThrowHelper.ThrowAggregate(aggregateExceptionLover, inners);
            void HandleException(Exception e)
            {
                Assert.IsInstanceOf(typeof(AggregateException), e);
                var a = e as AggregateException;
                Assert.IsTrue(e.Message.Contains(aggregateExceptionLover));
                Assert.AreEqual(inners, a.InnerExceptions);
            }
        }
        [Test]
        public void ThrowAggregateGenericArrayTest()
        {
            var inners = new Exception[] { new InvalidOperationException(), new ArithmeticException(), new OverflowException() };
            TestThrowing(Throw, HandleException);

            void Throw() => ThrowHelper.ThrowAggregate<ArrayAggregateException>(aggregateExceptionLover, inners);
            void HandleException(Exception e)
            {
                Assert.IsInstanceOf(typeof(ArrayAggregateException), e);
                var a = e as ArrayAggregateException;
                Assert.IsTrue(e.Message.Contains(aggregateExceptionLover));
                Assert.AreEqual(inners, a.InnerExceptions);
            }
        }
        [Test]
        public void ThrowAggregateGenericIEnumerableTest()
        {
            var inners = new Exception[] { new InvalidOperationException(), new ArithmeticException(), new OverflowException() };
            TestThrowing(Throw, HandleException);

            void Throw() => ThrowHelper.ThrowAggregate<IEnumerableAggregateException>(aggregateExceptionLover, inners);
            void HandleException(Exception e)
            {
                Assert.IsInstanceOf(typeof(IEnumerableAggregateException), e);
                var a = e as IEnumerableAggregateException;
                Assert.IsTrue(e.Message.Contains(aggregateExceptionLover));
                Assert.AreEqual(inners, a.InnerExceptions);
            }
        }
        [Test]
        public void ThrowAggregateGenericBadExceptionTest()
        {
            var inners = new Exception[] { new InvalidOperationException(), new ArithmeticException(), new OverflowException() };
            TestThrowing(Throw, HandleException);

            void Throw() => ThrowHelper.ThrowAggregate<BadAggregateException>(aggregateExceptionLover, inners);
            void HandleException(Exception e)
            {
                Assert.IsInstanceOf(typeof(MissingMethodException), e);
            }
        }

        private void TestThrowing(Action thrower, Action<Exception> exceptionHandler)
        {
            try
            {
                thrower();
            }
            catch (Exception e)
            {
                exceptionHandler(e);
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
}
