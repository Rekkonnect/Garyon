using System.Collections;
using System.Collections.Generic;

namespace Garyon.Objects.Enumerators
{
    /// <summary>An enumerable that uses the <seealso cref="IndexedEnumerator{T}"/> to enumerate elements of an <seealso cref="IEnumerable{T}"/>.</summary>
    /// <typeparam name="T">The type of elements stored in the <seealso cref="IEnumerable{T}"/>.</typeparam>
    public class IndexedEnumerable<T> : IEnumerable<IndexedEnumeratorResult<T>>
    {
        private readonly IEnumerable<T> enumerable;

        /// <summary>Initializes a new instance of the <seealso cref="IndexedEnumerable{T}"/> class from an <seealso cref="IEnumerable{T}"/>.</summary>
        /// <param name="originalEnumerable">The <seealso cref="IEnumerable{T}"/> to enumerate with index.</param>
        public IndexedEnumerable(IEnumerable<T> originalEnumerable)
        {
            enumerable = originalEnumerable;
        }

        /// <summary>Gets the <seealso cref="IndexedEnumerator{T}"/> that enumerates this <seealso cref="IEnumerable{T}"/>.</summary>
        /// <returns>The <seealso cref="IndexedEnumerator{T}"/>.</returns>
        public IEnumerator<IndexedEnumeratorResult<T>> GetEnumerator() => new IndexedEnumerator<T>(enumerable);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
