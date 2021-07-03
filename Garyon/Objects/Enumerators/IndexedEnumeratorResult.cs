using System;

namespace Garyon.Objects.Enumerators
{
    /// <summary>Represents an indexed enumerator result.</summary>
    /// <typeparam name="T">The type of the enumerator result.</typeparam>
    public class IndexedEnumeratorResult<T> : IEquatable<IndexedEnumeratorResult<T>>
    {
        /// <summary>The index of the enumerator result.</summary>
        public int Index { get; init; }
        /// <summary>The current enumerator result.</summary>
        public T Current { get; init; }

        /// <summary>Initializes a new empty instance of the <seealso cref="IndexedEnumeratorResult{T}"/> class.</summary>
        public IndexedEnumeratorResult() { }
        /// <summary>Initializes a new instance of the <seealso cref="IndexedEnumeratorResult{T}"/> class.</summary>
        /// <param name="index">The index of the enumerator result.</param>
        /// <param name="current">The current enumerator result.</param>
        public IndexedEnumeratorResult(int index, T current)
        {
            Index = index;
            Current = current;
        }

        /// <summary>Deconstructs this <seealso cref="IndexedEnumeratorResult{T}"/> into the index and the current enumerator result.</summary>
        /// <param name="index">The index of the enumerator result.</param>
        /// <param name="current">The current enumerator result.</param>
        public void Deconstruct(out int index, out T current) => (index, current) = (Index, Current);

        public bool Equals(IndexedEnumeratorResult<T> other) => Index == other.Index && Current.Equals(other.Current);
        public override bool Equals(object? obj) => obj is IndexedEnumeratorResult<T> i && Equals(i);
        public override int GetHashCode() => HashCode.Combine(Index, Current);
        public override string ToString() => $"{Index} - {Current}";
    }
}
