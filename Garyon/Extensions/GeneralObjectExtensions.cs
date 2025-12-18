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
        /// This method does NOT validate whether the recursively-typed property
        /// may fall into a circular path. Be absolutely cautious when calling
        /// materialization methods that immediately force full enumeration,
        /// possibly causing an infinite loop.
        /// <br/>
        /// To avoid the infinite loop on potentially recursive properties,
        /// consider combining this with
        /// <see cref="IEnumerableExtensions.UntilFirstRecursive"/>.
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
    }
}
