using Garyon.Extensions;
using System;
using System.Collections.Generic;

namespace Garyon.Functions
{
    /// <summary>Contains methods to create hash codes.</summary>
    public static class HashCodeFactory
    {
        /// <summary>Creates a new hash code out of an array of values.</summary>
        /// <typeparam name="T">The type of values stored in the array.</typeparam>
        /// <param name="values">The values to generate the hash code from.</param>
        /// <returns>The hash code from the combined values.</returns>
        public static int Combine<T>(params T[] values) => Combine((IEnumerable<T>)values);
        /// <summary>Creates a new hash code out of a collection of values.</summary>
        /// <typeparam name="T">The type of values stored in the collection.</typeparam>
        /// <param name="values">The values to generate the hash code from.</param>
        /// <returns>The hash code from the combined values.</returns>
        public static int Combine<T>(IEnumerable<T> values)
        {
            return new HashCode().AddRange(values).ToHashCode();
        }
    }
}
