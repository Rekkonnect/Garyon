using Garyon.Functions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Garyon.Objects
{
    /// <summary>Represents a collection that consists of the same repeated value the specified number of times.</summary>
    /// <typeparam name="T">The type of the value contained in the repeated value collection.</typeparam>
    public class RepeatedValueCollection<T> : IReadOnlyCollection<T>
    {
        /// <summary>The value that is contained multiple times.</summary>
        public T Value { get; }
        /// <summary>Gets or sets the number of times the specified value is to be repeated.</summary>
        public int Count { get; set; }

        /// <summary>Creates a new instance of the <seealso cref="RepeatedValueCollection{T}"/> class from the specified value and the count.</summary>
        /// <param name="value">The repeated value.</param>
        /// <param name="count">The number of times the value is to be repeated.</param>
        public RepeatedValueCollection(T value, int count)
        {
            Value = value;
            Count = count;
        }
        
        /// <summary>Gets the underlying enumerator instance that enumerates this collection by continuously returning the repeated element without exceeding the count.</summary>
        /// <returns>The <seealso cref="Enumerator"/> instance that enumerates this collection.</returns>
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Creates a new array and fills it with the same repeated value as many times as it's specified.</summary>
        /// <returns>The new array that contains the repeated element.</returns>
        public T[] ToArray()
        {
            return ArrayFactory.CreateFilled(Value, Count);
        }

        /// <summary>Provides an enumerator for a <seealso cref="RepeatedValueCollection{T}"/> instance.</summary>
        public struct Enumerator : IEnumerator<T>
        {
            /// <summary>Gets the collection that is being enumerated.</summary>
            public RepeatedValueCollection<T> Collection { get; }

            private int index;

            /// <inheritdoc/>
            public T Current => Collection.Value;
            object IEnumerator.Current => Current;

            /// <summary>Initializes a new instance of the <seealso cref="Enumerator"/> struct from a given <seealso cref="RepeatedValueCollection{T}"/> instance.</summary>
            /// <param name="collection">The collection that is to be enumerated.</param>
            public Enumerator(RepeatedValueCollection<T> collection)
            {
                Collection = collection;
                index = -1;
            }

            /// <inheritdoc/>
            public bool MoveNext()
            {
                index++;
                return index < Collection.Count;
            }
            /// <inheritdoc/>
            public void Reset()
            {
                index = -1;
            }
            void IDisposable.Dispose() { }
        }
    }
}