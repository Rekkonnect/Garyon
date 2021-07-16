using System;
using System.Collections;
using System.Collections.Generic;

namespace Garyon.Objects.Enumerators
{
    /// <summary>Represents an immutable collection of enumerables, which is flattened, through enumeration, into a single <seealso cref="IEnumerable{T}"/>.</summary>
    /// <typeparam name="T">The type of the elements that are held</typeparam>
    public class FlattenedEnumerables3D<T> : IEnumerable<T>
    {
        private readonly IEnumerable<IEnumerable<IEnumerable<T>>> enumerables;

        /// <summary>Initializes a new instance of the <seealso cref="FlattenedEnumerables3D{T}"/> class out of a collection of collections of enumerables.</summary>
        /// <param name="enumerables">The enumerables to initialize the instance from and transform into a flattened collection.</param>
        public FlattenedEnumerables3D(IEnumerable<IEnumerable<IEnumerable<T>>> enumerables)
        {
            this.enumerables = enumerables;
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Only intended for benchmarking purposes.</summary>
        /// <returns>The backed enumerator for the 3D enumeration process.</returns>
        public IEnumerator<T> GetBackedEnumerator() => new BackedEnumerator(this);

        /// <summary>Represents an enumerator for the <seealso cref="FlattenedEnumerables3D{T}"/> class.</summary>
        public class Enumerator : IEnumerator<T>
        {
            private readonly IEnumerator<IEnumerable<IEnumerable<T>>> outerEnumerator0;
            private IEnumerator<IEnumerable<T>>? outerEnumerator1;
            private IEnumerator<T>? innerEnumerator;

            private T current;

            /// <summary>Gets the currently enumerated element in the flattened enumerable.</summary>
            public T Current => current;
            object IEnumerator.Current => Current;

            /// <summary>Initializes a new instance of the <seealso cref="BackedEnumerator"/> class out of a <seealso cref="FlattenedEnumerables3D{T}"/> instance.</summary>
            /// <param name="instance">The <seealso cref="FlattenedEnumerables3D{T}"/> whose enumerables will be flattened.</param>
            public Enumerator(FlattenedEnumerables3D<T> instance)
            {
                outerEnumerator0 = instance.enumerables.GetEnumerator();
            }

            /// <summary>Advances to the next element in the flattened enumerable. Enumerables that contain no elements are automatically skipped.</summary>
            /// <returns><see langword="true"/> if the enumerator advanced to the next element, otherwise <see langword="false"/> if there were no remaining elements to enumerate.</returns>
            public bool MoveNext()
            {
                while (innerEnumerator?.MoveNext() != true)
                {
                    while (outerEnumerator1?.MoveNext() != true)
                    {
                        if (!outerEnumerator0.MoveNext())
                            return false;

                        outerEnumerator1 = outerEnumerator0.Current.GetEnumerator();
                    }

                    innerEnumerator = outerEnumerator1.Current.GetEnumerator();
                }

                current = innerEnumerator.Current;
                return true;
            }
            /// <inheritdoc/>
            public void Reset()
            {
                outerEnumerator0.Reset();
                outerEnumerator1 = null;
                innerEnumerator = null;
            }

            /// <summary>Disposes the currently available enumerators backing the enumeration process.</summary>
            /// <remarks>The enumerators that are wrapped should not be reused outside the flattening context in any case, otherwise problems might occur. The enumerators that are generated for this purpose should be exclusively available to the flattener, and nowhere else.</remarks>
            public void Dispose()
            {
                outerEnumerator0.Dispose();
                outerEnumerator1?.Dispose();
                innerEnumerator?.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>Represents an enumerator for the <seealso cref="FlattenedEnumerables3D{T}"/> class.</summary>
        private class BackedEnumerator : IEnumerator<T>
        {
            private readonly IEnumerator<IEnumerable<IEnumerable<T>>> outerEnumerator;
            private FlattenedEnumerables2D<T>.Enumerator? innerFlattenedEnumerator;

            private T current;

            /// <summary>Gets the currently enumerated element in the flattened enumerable.</summary>
            public T Current => current;
            object IEnumerator.Current => Current;

            /// <summary>Initializes a new instance of the <seealso cref="BackedEnumerator"/> class out of a <seealso cref="FlattenedEnumerables3D{T}"/> instance.</summary>
            /// <param name="instance">The <seealso cref="FlattenedEnumerables3D{T}"/> whose enumerables will be flattened.</param>
            public BackedEnumerator(FlattenedEnumerables3D<T> instance)
            {
                outerEnumerator = instance.enumerables.GetEnumerator();
            }

            /// <summary>Advances to the next element in the flattened enumerable. Enumerables that contain no elements are automatically skipped.</summary>
            /// <returns><see langword="true"/> if the enumerator advanced to the next element, otherwise <see langword="false"/> if there were no remaining elements to enumerate.</returns>
            public bool MoveNext()
            {
                while (innerFlattenedEnumerator?.MoveNext() != true)
                {
                    if (!outerEnumerator.MoveNext())
                        return false;

                    innerFlattenedEnumerator = new(outerEnumerator.Current.GetEnumerator());
                }

                current = innerFlattenedEnumerator.Current;
                return true;
            }
            /// <inheritdoc/>
            public void Reset()
            {
                outerEnumerator.Reset();
                innerFlattenedEnumerator = null;
            }

            /// <summary>Disposes the currently available enumerators backing the enumeration process.</summary>
            /// <remarks>The enumerators that are wrapped should not be reused outside the flattening context in any case, otherwise problems might occur. The enumerators that are generated for this purpose should be exclusively available to the flattener, and nowhere else.</remarks>
            public void Dispose()
            {
                outerEnumerator.Dispose();
                innerFlattenedEnumerator?.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
