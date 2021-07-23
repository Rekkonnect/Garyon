using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Garyon.Objects.Enumerators
{
    /// <summary>An enumerator that enumerates elements of an <seealso cref="IEnumerable{T}"/> while also preserving the enumeration index.</summary>
    /// <typeparam name="T">The type of elements that are being enumerated.</typeparam>
    public class IndexedAsyncEnumerator<T> : IAsyncEnumerator<IndexedEnumeratorResult<T>>
    {
        private readonly IAsyncEnumerator<T> enumerator;

        /// <summary>The current index in the enumerator.</summary>
        public int Index { get; private set; } = -1;

        /// <summary>Gets the current <seealso cref="IndexedEnumeratorResult{T}"/> in the enumerator.</summary>
        public IndexedEnumeratorResult<T> Current => new(Index, enumerator.Current);

        /// <summary>Initializes a new instance of the <seealso cref="IndexedAsyncEnumerator{T}"/> class from an <seealso cref="IAsyncEnumerable{T}"/>, using its <seealso cref="IAsyncEnumerable{T}.GetAsyncEnumerator(CancellationToken)"/> function.</summary>
        /// <param name="originalEnumerable">The original <seealso cref="IAsyncEnumerable{T}"/> whose <seealso cref="IAsyncEnumerator{T}"/> to wrap with this <seealso cref="IndexedAsyncEnumerator{T}"/>.</param>
        public IndexedAsyncEnumerator(IAsyncEnumerable<T> originalEnumerable)
            : this(originalEnumerable.GetAsyncEnumerator()) { }
        /// <summary>Initializes a new instance of the <seealso cref="IndexedAsyncEnumerator{T}"/> class from an <seealso cref="IAsyncEnumerator{T}"/>.</summary>
        /// <param name="originalEnumerator">The original <seealso cref="IAsyncEnumerator{T}"/> to wrap with this <seealso cref="IndexedAsyncEnumerator{T}"/>.</param>
        public IndexedAsyncEnumerator(IAsyncEnumerator<T> originalEnumerator)
        {
            enumerator = originalEnumerator;
        }

        /// <summary>Disposes the async enumerator by calling its <seealso cref="IAsyncDisposable.DisposeAsync()"/> implementation.</summary>
        /// <returns>A task that represents the asynchronous dispose operation.</returns>
        public async ValueTask DisposeAsync() => await enumerator.DisposeAsync();
        /// <inheritdoc/>
        public async ValueTask<bool> MoveNextAsync()
        {
            Index++;
            return await enumerator.MoveNextAsync();
        }
    }
}
