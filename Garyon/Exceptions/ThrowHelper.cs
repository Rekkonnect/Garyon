using Garyon.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Garyon.Exceptions
{
    /// <summary>A helper class providing tools for throwing exceptions. It should be preferred to use in code that demands optimization.</summary>
    public static class ThrowHelper
    {
        #region Generic Throwers
        /// <summary>Throws a new exception of the type <typeparamref name="T"/>.</summary>
        /// <typeparam name="T">The exception type to throw.</typeparam>
        /// <returns>Never reaches the point of returning; the return type is so that an object can be used in <see langword="return"/> or <see langword="throw"/> statements (latter is not recommended).</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static T Throw<T>()
            where T : Exception, new()
        {
            throw new T();
        }
        /// <summary>Throws a new exception of the type <typeparamref name="T"/>.</summary>
        /// <typeparam name="T">The exception type to throw.</typeparam>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <returns>Never reaches the point of returning; the return type is so that an object can be used in <see langword="return"/> or <see langword="throw"/> statements (latter is not recommended).</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static T Throw<T>(string message)
            where T : Exception
        {
            var constructor = typeof(T).GetConstructor<string>();
            throw constructor.Invoke(new object[] { message }) as T;
        }
        /// <summary>Throws a new exception of the type <typeparamref name="T"/>.</summary>
        /// <typeparam name="T">The exception type to throw.</typeparam>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        /// <returns>Never reaches the point of returning; the return type is so that an object can be used in <see langword="return"/> or <see langword="throw"/> statements (latter is not recommended).</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static T Throw<T>(string message, Exception innerException)
            where T : Exception
        {
            return Throw<T, Exception>(message, innerException);
        }
        /// <summary>Throws a new aggregate exception of the type <typeparamref name="T"/>.</summary>
        /// <typeparam name="T">The exception type to throw.</typeparam>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
        /// <returns>Never reaches the point of returning; the return type is so that an object can be used in <see langword="return"/> or <see langword="throw"/> statements (latter is not recommended).</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static T ThrowAggregate<T>(string message, params Exception[] innerExceptions)
            where T : AggregateException
        {
            return ThrowAggregate<T>(message, innerExceptions, innerExceptions);
        }
        /// <summary>Throws a new aggregate exception of the type <typeparamref name="T"/>.</summary>
        /// <typeparam name="T">The exception type to throw.</typeparam>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
        /// <returns>Never reaches the point of returning; the return type is so that an object can be used in <see langword="return"/> or <see langword="throw"/> statements (latter is not recommended).</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static T ThrowAggregate<T>(string message, IEnumerable<Exception> innerExceptions)
            where T : AggregateException
        {
            return ThrowAggregate<T>(message, innerExceptions, null);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static T ThrowAggregate<T>(string message, IEnumerable<Exception> enumerableInner, Exception[]? arrayInner)
            where T : AggregateException
        {
            var resulting = GetThrowableException<T, IEnumerable<Exception>>(message, enumerableInner);
            if (resulting == null)
                resulting = GetThrowableException<T, Exception[]>(message, arrayInner ?? enumerableInner.ToArray());
            if (resulting == null)
                Throw<MissingMethodException>("The provided AggregateException type does not provide a constructor with an 'IEnumerable<Exception>' or an 'Exception[]' as the second argument.");
            throw resulting;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static TException Throw<TException, TInner>(string message, TInner inner)
            where TException : Exception
        {
            throw GetThrowableException<TException, TInner>(message, inner);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static TException? GetThrowableException<TException, TInner>(string message, TInner inner)
            where TException : Exception
        {
            var constructor = typeof(TException).GetConstructor<string, TInner>();
            return constructor?.Invoke(new object[] { message, inner }) as TException;
        }
        #endregion

        #region Instance Throwers
        /// <summary>Throws an exception whose instance is already created.</summary>
        /// <param name="e">The exception instance to throw.</param>
        /// <returns>Never reaches the point of returning; the return type is so that an object can be used in <see langword="return"/> or <see langword="throw"/> statements (latter is not recommended).</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Exception Throw(Exception e) => throw e;
        #endregion

        #region Standarized Exception Throwers
        /// <summary>Throws a new <seealso cref="AggregateException"/>.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
        /// <returns>Never reaches the point of returning; the return type is so that an object can be used in <see langword="return"/> or <see langword="throw"/> statements (latter is not recommended).</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Exception ThrowAggregate(string message, params Exception[] innerExceptions)
        {
            throw new AggregateException(message, innerExceptions);
        }
        #endregion
    }
}
