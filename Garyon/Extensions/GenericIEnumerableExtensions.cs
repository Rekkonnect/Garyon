using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions
{
    /// <summary>Contains extension functions for the <seealso cref="IEnumerable{T}"/> interface.</summary>
    public static class GenericIEnumerableExtensions
    {
        /// <summary>Concatenates multiple <seealso cref="IEnumerable{T}"/>s and returns the concatenated result.</summary>
        /// <typeparam name="T">The type of the elements the <seealso cref="IEnumerable{T}"/>s hold.</typeparam>
        /// <param name="e">The base <seealso cref="IEnumerable{T}"/> to concatenate with the others.</param>
        /// <param name="others">The other <seealso cref="IEnumerable{T}"/>s to concatenate.</param>
        public static IEnumerable<T> ConcatMultiple<T>(this IEnumerable<T> e, params IEnumerable<T>[] others)
        {
            var concated = e;
            for (int i = 0; i < others.Length; i++)
                concated = concated.Concat(others[i]);
            return concated;
        }
    }
}
