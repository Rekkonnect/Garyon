using System;
using System.Collections.Generic;

namespace Garyon.Extensions
{
    /// <summary>Provides extensions for the <seealso cref="HashCode"/> struct.</summary>
    public static class HashCodeExtensions
    {
        /// <summary>Adds the hash codes of a range of elements to the given <seealso cref="HashCode"/> instance.</summary>
        /// <typeparam name="T">The type of the elemnts in the collection.</typeparam>
        /// <param name="hashCode">The hash code instance that will also combine the hash codes of the given element collection.</param>
        /// <param name="elements">The elements whose hash codes to combine into the given hash code instance.</param>
        /// <returns>The resulting <seealso cref="HashCode"/> that also contains the combined hash codes of the elements of the collection.</returns>
        public static HashCode AddRange<T>(this HashCode hashCode, IEnumerable<T> elements)
        {
            foreach (var e in elements)
                hashCode.Add(e);
            return hashCode;
        }
    }
}
