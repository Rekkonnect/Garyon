using Garyon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for the <see cref="ICloneable"/> and
/// <see cref="ICloneable{T}"/> interfaces.
/// </summary>
public static class CloningExtensions
{
    /// <summary>
    /// Clones a range of <see cref="ICloneable{T}"/> values into a new enumerable.
    /// </summary>  
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="range">The values to clone.</param>
    /// <returns>
    /// An enumerable with the cloned instances. The values are lazily returned via
    /// <see cref="Enumerable.Select{TSource, TResult}(IEnumerable{TSource}, Func{TSource, TResult})"/>.
    /// </returns>
    public static IEnumerable<TElement> CloneRange<TElement>(this IEnumerable<TElement> range)
        where TElement : notnull, ICloneable<TElement>
    {
        return range.Select(static s => s.Clone());
    }

    /// <summary>
    /// Clones a range of <see cref="ICloneable{T}"/> values into a new list.
    /// </summary>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="range">The values to clone.</param>
    /// <returns>
    /// A list with the cloned instances.
    /// </returns>
    public static IList<TElement> CloneList<TElement>(this IEnumerable<TElement> range)
        where TElement : notnull, ICloneable<TElement>
    {
        return range.CloneRange().ToList();
    }

    /// <summary>
    /// Clones a range of <see cref="ICloneable"/> values into a new enumerable.
    /// </summary>  
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="range">The values to clone.</param>
    /// <returns>
    /// An enumerable with the cloned instances. The values are lazily returned via
    /// <see cref="Enumerable.Select{TSource, TResult}(IEnumerable{TSource}, Func{TSource, TResult})"/>.
    /// </returns>
    public static IEnumerable<TElement> CloneRangeNonGeneric<TElement>(this IEnumerable<TElement> range)
        where TElement : notnull, ICloneable
    {
        return range
            .Select(static s => s.Clone())
            .Cast<TElement>();
    }

    /// <summary>
    /// Clones a range of <see cref="ICloneable"/> values into a new list.
    /// </summary>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="range">The values to clone.</param>
    /// <returns>
    /// A list with the cloned instances.
    /// </returns>
    public static IList<TElement> CloneListNonGeneric<TElement>(this IEnumerable<TElement> range)
        where TElement : notnull, ICloneable
    {
        return range.CloneRangeNonGeneric().ToList();
    }
}
