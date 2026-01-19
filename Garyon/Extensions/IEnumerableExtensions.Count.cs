using Garyon.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

public static partial class IEnumerableExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        /// <summary>
        /// Determines whether the enumerable is empty or not by using
        /// <see cref="Enumerable.Any{TSource}(IEnumerable{TSource})"/>.
        /// </summary>
        public bool HasNone()
        {
            return !source.Any();
        }

        /// <summary>
        /// Wraps the enumerable in a <see cref="CachedCountEnumerable{T}"/> to
        /// cache the count during enumeration and provide seamless access to
        /// the lower bound and the possibly-known count.
        /// </summary>
        public CachedCountEnumerable<T> WithCountCaching()
        {
            return new(source);
        }

        /// <summary>
        /// Attempts to get the count of the enumerable without enumerating it.
        /// The implementation uses <see cref="GetNonEnumeratedCount"/>.
        /// </summary>
        /// <returns>
        /// The count of the enumerable without enumerating it, if it is known,
        /// otherwise 0.
        /// </returns>
        public int GetNonEnumeratedCountOrDefault()
        {
            return source.GetNonEnumeratedCount() ?? 0;
        }

#if HAS_TRY_GET_NON_ENUMERATED_COUNT
        /// <summary>
        /// Attempts to get the count of the enumerable without enumerating it.
        /// The implementation relies on <see cref="Enumerable.TryGetNonEnumeratedCount"/>.
        /// </summary>
        /// <returns>
        /// The count of the enumerable without enumerating it, if it is known,
        /// otherwise <see langword="null"/>.
        /// </returns>
#else
        /// <summary>
        /// Attempts to get the count of the enumerable without enumerating it.
        /// The method supports <see cref="ICollection"/> and
        /// <see cref="ICollection{T}"/>.
        /// </summary>
        /// <returns>
        /// The count of the enumerable without enumerating it, if it is known,
        /// otherwise <see langword="null"/>.
        /// </returns>
#endif
        public int? GetNonEnumeratedCount()
        {
#if HAS_TRY_GET_NON_ENUMERATED_COUNT
            bool hasQuickCount = source.TryGetNonEnumeratedCount(out var sourceCount);
            return hasQuickCount ? sourceCount : null;
#else
            return source switch
            {
                ICollection<T> collection => collection.Count,
                ICollection nonGenericCollection => nonGenericCollection.Count,
                _ => null,
            };
#endif
        }

        /// <summary>
        /// Determines whether the two enumerables have equal counts.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        ///     <item>
        ///     <see langword="true"/> when the counts are equal
        ///     </item>
        ///     <item>
        ///     <see langword="false"/> when the counts are not equal
        ///     </item>
        ///     <item>
        ///     <see langword="null"/> when at least one of the enumerables
        ///     cannot provide a non-enumerated count
        ///     </item>
        /// </list>
        /// </returns>
        public bool? EqualsNonEnumeratedCount(IEnumerable<T> other)
        {
            return source.GetNonEnumeratedCount() is int sourceCount
                && other.GetNonEnumeratedCount() is int otherCount
                && sourceCount == otherCount
                ;
        }
    }

    extension(IEnumerable source)
    {
        /// <summary>
        /// Wraps the enumerable in a <see cref="CachedCountEnumerable"/> to
        /// cache the count during enumeration and provide seamless access to
        /// the lower bound and the possibly-known count.
        /// </summary>
        public CachedCountEnumerable WithCountCaching()
        {
            return new(source);
        }

        /// <summary>
        /// Determines whether the enumerable is empty or not by initiating the
        /// enumerator and invoking <see cref="IEnumerator.MoveNext"/> once.
        /// </summary>
        /// <remarks>
        /// For <see cref="IReadOnlyCollection{T}"/>, use
        /// <see cref="IReadOnlyCollectionExtensions.get_IsEmpty{T}(IReadOnlyCollection{T})"/>.
        /// </remarks>
        public bool HasNone()
        {
            var hasAny = source.GetEnumerator().MoveNext();
            return !hasAny;
        }

        /// <summary>
        /// Gets the count of the enumerable, attempting to avoid enumeration
        /// via <see cref="GetNonEnumeratedCount(IEnumerable)"/>, otherwise
        /// enumerating it via <see cref="WithCountCaching(IEnumerable)"/>.
        /// </summary>
        public int Count()
        {
            var nonEnumeratedCount = source.GetNonEnumeratedCount();
            if (nonEnumeratedCount is int nonEnumeratedCountValue)
            {
                return nonEnumeratedCountValue;
            }

            return source.WithCountCaching().ForceCount();
        }

        /// <summary>
        /// Attempts to get the count of the enumerable without enumerating it.
        /// The method supports <see cref="ICollection"/> and
        /// <see cref="ICollection{T}"/>.
        /// </summary>
        /// <returns>
        /// The count of the enumerable without enumerating it, if it is known,
        /// otherwise <see langword="null"/>.
        /// </returns>
        public int? GetNonEnumeratedCount()
        {
            return source switch
            {
                ICollection nonGenericCollection => nonGenericCollection.Count,
                _ => GetGenericEnumerableCount(),
            };

            int? GetGenericEnumerableCount()
            {
                // Require that the source implements exactly one IEnumerable<T> interface
                // so that we can dynamically invoke GetNonEnumeratedCount
                var singleEnumerableType = source.GetSingleEnumerableTypeOrDefault();
                if (singleEnumerableType is null)
                {
                    return null;
                }

#if HAS_DYNAMIC_INVOCATION
                // Use dynamic binding to avoid further reflection usage
                return GetNonEnumeratedCount<dynamic>((dynamic)source);
#else
                return null;
#endif
            }
        }

        /// <summary>
        /// Determines whether the two enumerables have equal counts.
        /// </summary>
        public bool EqualsNonEnumeratedCount(IEnumerable other)
        {
            return source.GetNonEnumeratedCount() is int sourceCount
                && other.GetNonEnumeratedCount() is int otherCount
                && sourceCount == otherCount
                ;
        }

        private IReadOnlyList<Type> GetEnumerableTypes()
        {
            var type = source.GetType();
            return type.GetInterfaces()
                .Where(s => s.IsGenericVariantOf(typeof(IEnumerable<>)))
                .ToList();
        }

        private Type? GetSingleEnumerableTypeOrDefault()
        {
            var types = source.GetEnumerableTypes();
            if (types.Count is 1)
            {
                return types[0];
            }

            return null;
        }
    }
}
