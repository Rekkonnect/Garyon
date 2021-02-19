using System.Collections;
using System.Collections.Generic;

namespace Garyon.Objects.Enumerators
{
    /// <summary>An enumerator that enumerates elements of an <seealso cref="IEnumerable{T}"/> while also preserving the enumeration index.</summary>
    /// <typeparam name="T">The type of elements that are being enumerated.</typeparam>
    public class IndexedEnumerator<T> : IEnumerator<IndexedEnumeratorResult<T>>
    {
        private IEnumerator<T> enumerator;

        /// <summary>The current index in the enumerator.</summary>
        public int Index { get; private set; }

        /// <summary>Gets the current <seealso cref="IndexedEnumeratorResult{T}"/> in the enumerator.</summary>
        public IndexedEnumeratorResult<T> Current => new(Index, enumerator.Current);
        object IEnumerator.Current => Current;

        /// <summary>Initializes a new instance of the <seealso cref="IndexedEnumerator{T}"/> class from an <seealso cref="IEnumerable{T}"/>, using its <seealso cref="IEnumerable{T}.GetEnumerator()"/> function.</summary>
        /// <param name="originalEnumerable">The original <seealso cref="IEnumerable{T}"/> whose <seealso cref="IEnumerator{T}"/> to wrap with this <seealso cref="IndexedEnumerator{T}"/>.</param>
        public IndexedEnumerator(IEnumerable<T> originalEnumerable)
            : this(originalEnumerable.GetEnumerator()) { }
        /// <summary>Initializes a new instance of the <seealso cref="IndexedEnumerator{T}"/> class from an <seealso cref="IEnumerator{T}"/>.</summary>
        /// <param name="originalEnumerator">The original <seealso cref="IEnumerator{T}"/> to wrap with this <seealso cref="IndexedEnumerator{T}"/>.</param>
        public IndexedEnumerator(IEnumerator<T> originalEnumerator)
        {
            enumerator = originalEnumerator;
            Reset();
        }

        /// <inheritdoc/>
        public void Dispose() => enumerator.Dispose();
        /// <inheritdoc/>
        public bool MoveNext()
        {
            Index++;
            return enumerator.MoveNext();
        }
        /// <inheritdoc/>
        public void Reset()
        {
            Index = -1;
            enumerator.Reset();
        }
    }
}
