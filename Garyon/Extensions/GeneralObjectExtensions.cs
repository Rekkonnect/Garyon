using System;
using System.Collections.Generic;

namespace Garyon.Extensions;

/// <summary>
/// Provides general extensions for all objects.
/// </summary>
public static class GeneralObjectExtensions
{
    extension<T>(T source)
    {
        /*
         * Currently the following assigning/casting patterns are supported:
         * - Assign O into out O and return O
         * - Cast O into C and return C
         * - Cast O into out C and return O
         * 
         * Candidate patterns include:
         * - Assign O into out O and return cast into C
         *   Example usage:
         *   var derived = BaseGetter().CastSwitch<Derived>(out var b);
         *   // Base BaseGetter()
         *   // Derived : Base
         */

        /// <summary>
        /// Stores the value in the specified reference and returns it.
        /// </summary>
        /// <param name="reference">The reference into which to store the value.</param>
        /// <returns>The same value.</returns>
        /// <remarks>
        /// <para>
        /// This can find exceptional usage in builder-like patterns,
        /// whereby a new object being initialized is assigned without breaking
        /// the fluent pattern, allowing the object be referenced within the
        /// builder.
        /// </para>
        /// <para>
        /// Beware of copying rules when using this on structs.
        /// </para>
        /// </remarks>
        public T Into(out T reference)
        {
            reference = source;
            return source;
        }

        /// <summary>
        /// Casts the value into the specified type, stores it in the specified reference,
        /// and returns the original value.
        /// </summary>
        /// <param name="reference">The reference into which to store the value.</param>
        /// <returns>The same value.</returns>
        /// <remarks>
        /// <para>
        /// This can find exceptional usage in builder-like patterns,
        /// whereby a new object being initialized is assigned without breaking
        /// the fluent pattern, allowing the object be referenced within the
        /// builder.
        /// </para>
        /// </remarks>
        public T Into<TCast>(out TCast? reference)
            where TCast : class, T
        {
            reference = source as TCast;
            return source;
        }

        /// <summary>
        /// Attempts to cast the source object to the specified type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns>
        /// The instance cast into <typeparamref name="TResult"/> if the cast was successful,
        /// otherwise <see langword="default"/>.
        /// </returns>
        public TResult? CastOrDefault<TResult>()
        {
            if (source is TResult result)
                return result;

            return default;
        }

        /// <summary>
        /// Based on a recursively-typed property, invokes it on the result until
        /// the resulting instance is <see langword="null"/>.
        /// </summary>
        /// <returns>
        /// A lazily evaluated enumerable of values, with the first being the
        /// directly returned value from the property, and the last being the
        /// latest in the inheritance tree.
        /// <br/>
        /// For example, assume a type IterableInt with a property Successor defined
        /// as such:
        /// <code>
        /// public sealed class IterableInt(int value)
        /// {
        ///     public IterableInt? Successor
        ///     {
        ///         get
        ///         {
        ///             var next = value + 1;
        ///             if (next &lt; value)
        ///             {
        ///                 // We overflowed
        ///                 return null;
        ///             }
        ///
        ///             return new(next);
        ///         }
        ///     }
        /// }
        /// </code>
        /// Starting from IterableInt(1), this method will return a collection
        /// that contains [IterableInt(2), IterableInt(3), ..., IterableInt(int.MaxValue)],
        /// though lazily enumerated meaning that it won't force generating
        /// all the values in the collection until explicitly requested.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method does NOT validate whether the recursively-typed property
        /// may fall into a circular path. Be absolutely cautious when calling
        /// materialization methods that immediately force full enumeration,
        /// possibly causing an infinite loop.
        /// </para>
        /// <para>
        /// To avoid the infinite loop on potentially recursive properties,
        /// consider combining this with
        /// <see cref="IEnumerableExtensions.UntilFirstRecursive"/>.
        /// </para>
        /// </remarks>
        public IEnumerable<T> EnumerateRecursiveProperty(Func<T, T?> property)
        {
            var current = source;
            while (true)
            {
                var parent = property(current);
                if (parent is null)
                    yield break;

                yield return parent;
                current = parent;
            }
        }

        public T? DefaultIf(Predicate<T?> predicate)
        {
            if (predicate(source))
                return default;

            return source;
        }
    }
}
