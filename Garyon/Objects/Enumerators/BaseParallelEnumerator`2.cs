using System;
using System.Collections.Generic;

namespace Garyon.Objects.Enumerators
{
    /// <summary>Provides the base mechanism for a parallel enumerator for 2 <seealso cref="IEnumerator{T}"/> objects.</summary>
    /// <typeparam name="T1">The type of the elements the first <seealso cref="IEnumerator{T}"/> enumerates.</typeparam>
    /// <typeparam name="T2">The type of the elements the second <seealso cref="IEnumerator{T}"/> enumerates.</typeparam>
    public abstract class BaseParallelEnumerator<T1, T2> : BaseParallelEnumerator
    {
        private bool enumerator1Alive = true;
        private bool enumerator2Alive = true;

        /// <summary>Gets the first enumerator.</summary>
        protected IEnumerator<T1> Enumerator1 { get; private set; }
        /// <summary>Gets the second enumerator.</summary>
        protected IEnumerator<T2> Enumerator2 { get; private set; }

        /// <summary>Determines whether the first enumerator has not reached the end of the collection.</summary>
        public bool Enumerator1Alive => enumerator1Alive;
        /// <summary>Determines whether the second enumerator has not reached the end of the collection.</summary>
        public bool Enumerator2Alive => enumerator2Alive;

        /// <summary>Initializes a new instance of the <seealso cref="BaseParallelEnumerator{T1, T2}"/> class.</summary>
        /// <param name="enumerator1">The first enumerator.</param>
        /// <param name="enumerator2">The second enumerator.</param>
        protected BaseParallelEnumerator(IEnumerable<T1> enumerator1, IEnumerable<T2> enumerator2)
        {
            Enumerator1 = enumerator1.GetEnumerator();
            Enumerator2 = enumerator2.GetEnumerator();
        }

        /// <inheritdoc/>
        protected override T GetCurrentValue<T>(int index)
        {
            return index switch
            {
                0 => (T)(object)GetCurrentValue(Enumerator1, enumerator1Alive),
                1 => (T)(object)GetCurrentValue(Enumerator2, enumerator2Alive),
                _ => default,
            };
        }

        /// <summary>Advances both enumerators to the next element of each collection.</summary>
        /// <returns><see langword="true"/> if any of the two enumerators was successfully advanced to the next element; <see langword="false"/> if both enumerators have passed the end of the collection.</returns>
        public virtual bool MoveNext()
        {
            ConditionallyMoveNext(Enumerator1, ref enumerator1Alive);
            ConditionallyMoveNext(Enumerator2, ref enumerator2Alive);
            return enumerator1Alive || enumerator2Alive;
        }
        /// <summary>Sets both enumerators to their initial position, which is before the first element in each collection.</summary>
        public virtual void Reset()
        {
            Enumerator1.Reset();
            Enumerator2.Reset();
            enumerator1Alive = enumerator2Alive = true;
        }

        /// <summary>Calls the <seealso cref="IDisposable.Dispose()"/> method on both enumerators.</summary>
        public virtual void Dispose()
        {
            Enumerator1.Dispose();
            Enumerator2.Dispose();
        }
    }
}
