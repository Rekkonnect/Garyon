#if HAS_SLICES

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Garyon.Extensions;

/// <summary>Provides extension functions for the <seealso cref="Index"/> type.</summary>
public static class IndexExtensions
{
    /// <summary>Gets the offset for the provided dimension in an array.</summary>
    /// <param name="index">The index in the dimension.</param>
    /// <param name="array">The array to get the offset from.</param>
    /// <param name="dimension">The dimension within the array.</param>
    /// <returns>The offset.</returns>
    [ExcludeFromCodeCoverage]
    public static int GetOffset(this Index index, Array array, int dimension) => index.GetOffset(array.GetLength(dimension));
    /// <summary>Gets the offset for the provided dimension in a single-dimensional array.</summary>
    /// <param name="index">The index in the array.</param>
    /// <param name="array">The array to get the offset from.</param>
    /// <returns>The offset.</returns>
    [ExcludeFromCodeCoverage]
    public static int GetOffset<T>(this Index index, T[] array) => index.GetOffset(array.Length);
    /// <summary>Gets the offset for the provided dimension in a collection.</summary>
    /// <param name="index">The index in the collection.</param>
    /// <param name="collection">The collection to get the offset from.</param>
    /// <returns>The offset.</returns>
    [ExcludeFromCodeCoverage]
    public static int GetOffset<T>(this Index index, IEnumerable<T> collection) => index.GetOffset(collection.Count());
    /// <summary>Gets the offset for the provided dimension in a collection.</summary>
    /// <param name="index">The index in the collection.</param>
    /// <param name="collection">The collection to get the offset from.</param>
    /// <returns>The offset.</returns>
    [ExcludeFromCodeCoverage]
    public static int GetOffset<T>(this Index index, ICollection<T> collection) => index.GetOffset(collection.Count);

    /// <summary>Inverts the index to reflect the respective index symmetric to the middle of the collection.</summary>
    /// <param name="index">The index to invert.</param>
    /// <returns>The inverted index.</returns>
    public static Index Invert(this Index index)
    {
        // Imagine this as a nasty ternary operator
        return index.IsFromEnd switch
        {
            true => index.Value - 1,
            false => ^(index.Value + 1),
        };
    }
}

#endif
