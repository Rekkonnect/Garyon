using Garyon.Objects;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for type filtering on <see cref="IEnumerable{T}"/>.
/// </summary>
public static class OfTypeEnumerableExtensions
{
    extension<TSource>(IEnumerable<TSource> source)
    {
        /// <summary>
        /// Filters the elements of the source collection, returning only
        /// those that are of any of the specified types.
        /// </summary>
        public IEnumerable<TSource> OfAnyType<TA, TB>()
            where TA : TSource
            where TB : TSource
        {
            return source.Where(t => t is TA or TB);
        }

        /// <summary>
        /// Filters the elements of the source collection, returning only
        /// those that are of any of the specified types.
        /// </summary>
        public IEnumerable<TSource> OfAnyType<TA, TB, TC>()
            where TA : TSource
            where TB : TSource
            where TC : TSource
        {
            return source.Where(t => t is TA or TB or TC);
        }

        /// <summary>
        /// Filters the elements of the source collection, returning only
        /// those that are of any of the specified types.
        /// </summary>
        public IEnumerable<TSource> OfAnyType<TA, TB, TC, TD>()
            where TA : TSource
            where TB : TSource
            where TC : TSource
            where TD : TSource
        {
            return source.Where(t => t is TA or TB or TC or TD);
        }
    }
}
