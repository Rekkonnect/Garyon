using System.Collections.Generic;
using System.Threading;

namespace Garyon.Objects.Enumerators
{
    /// <summary>An enumerable that uses the <seealso cref="IndexedEnumerator{T}"/> to enumerate elements of an <seealso cref="IAsyncEnumerable{T}"/>.</summary>
    /// <typeparam name="T">The type of elements stored in the <seealso cref="IAsyncEnumerable{T}"/>.</typeparam>
    public class IndexedAsyncEnumerable<T> : IAsyncEnumerable<IndexedEnumeratorResult<T>>
    {
        private IAsyncEnumerable<T> enumerable;

        /// <summary>Initializes a new instance of the <seealso cref="IndexedAsyncEnumerable{T}"/> class from an <seealso cref="IAsyncEnumerable{T}"/>.</summary>
        /// <param name="originalEnumerable">The <seealso cref="IAsyncEnumerable{T}"/> to enumerate with index.</param>
        public IndexedAsyncEnumerable(IAsyncEnumerable<T> originalEnumerable)
        {
            enumerable = originalEnumerable;
        }

        /// <summary>Gets the <seealso cref="IndexedAsyncEnumerator{T}"/> that enumerates this <seealso cref="IAsyncEnumerable{T}"/>.</summary>
        /// <returns>The <seealso cref="IndexedAsyncEnumerator{T}"/>.</returns>
        public IAsyncEnumerator<IndexedEnumeratorResult<T>> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new IndexedAsyncEnumerator<T>(enumerable);
    }
}
