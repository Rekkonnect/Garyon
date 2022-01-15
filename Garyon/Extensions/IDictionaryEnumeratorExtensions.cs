using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions
{
    /// <summary>Provides extensions for the <seealso cref="IDictionary"/> interface.</summary>
    public static class IDictionaryEnumeratorExtensions
    {
        /// <summary>Converts the values of the dictionary into a collection of strongly-typed elements.</summary>
        /// <typeparam name="T">The type to cast all the values of the dictionary into.</typeparam>
        /// <param name="dictionary">The dictionary whose values to convert into a strongly-typed element collection.</param>
        /// <returns>An <seealso cref="IEnumerable{T}"/> containing the values of the provided dictionary.</returns>
        public static IEnumerable<T> Values<T>(this IDictionary dictionary)
        {
            return dictionary.Values.Cast<T>();
        }
    }
}