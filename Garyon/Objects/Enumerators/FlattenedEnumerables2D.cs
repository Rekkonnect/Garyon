using System;
using System.Collections;
using System.Collections.Generic;

namespace Garyon.Objects.Enumerators
{
    /// <summary>Represents an immutable collection of enumerables, which is flattened, through enumeration, into a single <seealso cref="IEnumerable{T}"/>.</summary>
    /// <typeparam name="T">The type of the elements that are held</typeparam>
    public class FlattenedEnumerables2D<T> : IEnumerable<T>
    {
        private readonly IEnumerable<IEnumerable<T>> enumerables;

        /// <summary>Initializes a new instance of the <seealso cref="FlattenedEnumerables2D{T}"/> class out of a collection of enumerables.</summary>
        /// <param name="enumerables">The enumerables to initialize the instance from and transform into a flattened collection.</param>
        public FlattenedEnumerables2D(IEnumerable<IEnumerable<T>> enumerables)
        {
            this.enumerables = enumerables;
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Represents an enumerator for the <seealso cref="FlattenedEnumerables2D{T}"/> class.</summary>
        public class Enumerator : IEnumerator<T>
        {
            private readonly IEnumerator<IEnumerable<T>> outerEnumerator;
            private IEnumerator<T>? innerEnumerator;

            private T current;

            /// <summary>Gets the currently enumerated element in the flattened enumerable.</summary>
            public T Current => current;
            object IEnumerator.Current => Current;

            /// <summary>Initializes a new instance of the <seealso cref="Enumerator"/> class out of a <seealso cref="FlattenedEnumerables2D{T}"/> instance.</summary>
            /// <param name="instance">The <seealso cref="FlattenedEnumerables2D{T}"/> whose enumerables will be flattened.</param>
            public Enumerator(FlattenedEnumerables2D<T> instance)
            {
                outerEnumerator = instance.enumerables.GetEnumerator();
            }
            /// <summary>Initializes a new instance of the <seealso cref="Enumerator"/> class out of an enumerator of collections.</summary>
            /// <param name="enumerator">The enumerator that will enumerate the collections of the elements that will be flattened. It should not have any other dependencies. See <seealso cref="Dispose"/> for more info.</param>
            public Enumerator(IEnumerator<IEnumerable<T>> enumerator)
            {
                outerEnumerator = enumerator;
            }

            /// <summary>Advances to the next element in the flattened enumerable. Enumerables that contain no elements are automatically skipped.</summary>
            /// <returns><see langword="true"/> if the enumerator advanced to the next element, otherwise <see langword="false"/> if there were no remaining elements to enumerate.</returns>
            public bool MoveNext()
            {
                while (innerEnumerator?.MoveNext() != true)
                {
                    if (!outerEnumerator.MoveNext())
                        return false;

                    innerEnumerator = outerEnumerator.Current.GetEnumerator();
                }

                current = innerEnumerator.Current;
                return true;
            }
            /// <inheritdoc/>
            public void Reset()
            {
                outerEnumerator.Reset();
                innerEnumerator = null;
            }

            /// <summary>Disposes the currently available enumerators backing the enumeration process.</summary>
            /// <remarks>The enumerators that are wrapped should not be reused outside the flattening context in any case, otherwise problems might occur. The enumerators that are generated for this purpose should be exclusively available to the flattener, and nowhere else.</remarks>
            public void Dispose()
            {
                outerEnumerator.Dispose();
                innerEnumerator?.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
