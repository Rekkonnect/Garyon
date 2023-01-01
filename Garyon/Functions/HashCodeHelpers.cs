#if HAS_HASH_CODE

using System;
using System.Collections.Generic;

namespace Garyon.Functions;

/// <summary>Provides helper functions for the <seealso cref="HashCode"/> struct.</summary>
public static class HashCodeHelpers
{
    /// <summary>Generates a hash code produced by the combination of the hash code of each individual element in the provided collection.</summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="elements">The elements whose hash codes to combine into the resulting hash code.</param>
    /// <returns>The <seealso cref="HashCode"/> that contains the elements that were hashed and had their hash codes combined.</returns>
    public static HashCode Combine<T>(IEnumerable<T> elements)
    {
        var hashCode = new HashCode();
        foreach (var e in elements)
            hashCode.Add(e);
        return hashCode;
    }
}

#endif
