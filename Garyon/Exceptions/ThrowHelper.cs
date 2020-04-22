using System;
using System.Runtime.CompilerServices;

namespace Garyon.Exceptions
{
    /// <summary>A helper class providing tools for throwing exceptions. It should be preferred to use in code that demands optimization.</summary>
    public static class ThrowHelper
    {
        /// <summary>Throws a new exception of the type <typeparamref name="T"/>.</summary>
        /// <typeparam name="T">The exception type to throw.</typeparam>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Throw<T>()
            where T : Exception, new()
        {
            throw new T();
        }
        /// <summary>Throws a new exception of the type <typeparamref name="T"/>.</summary>
        /// <typeparam name="T">The exception type to throw.</typeparam>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Throw<T>(string message)
            where T : Exception
        {
            var constructor = typeof(T).GetConstructor(new Type[] { typeof(string) });
            throw constructor.Invoke(new object[] { message }) as T;
        }
        /// <summary>Throws a new exception of the type <typeparamref name="T"/>.</summary>
        /// <typeparam name="T">The exception type to throw.</typeparam>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Throw<T>(string message, Exception innerException)
            where T : Exception
        {
            var constructor = typeof(T).GetConstructor(new Type[] { typeof(string), typeof(Exception) });
            throw constructor.Invoke(new object[] { message, innerException }) as T;
        }

        /// <summary>Throws an exception whose instance is already created.</summary>
        /// <param name="e">The exception instance to throw.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Throw(Exception e) => throw e;
    }
}
