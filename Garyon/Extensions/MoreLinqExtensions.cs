using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions
{
    /// <summary>Provides more LINQ-like extensions that are not that likely to be considered candidates for being imported into System.Linq.</summary>
    public static class MoreLinqExtensions
    {
        /// <inheritdoc cref="Enumerable.Where{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>
        public static IEnumerable<T> WherePredicate<T>(this IEnumerable<T> source, Predicate<T> predicate) => source.Where(new Func<T, bool>(predicate));
    }
}