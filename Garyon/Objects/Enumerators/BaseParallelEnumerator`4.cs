using System;
using System.Collections.Generic;

namespace Garyon.Objects.Enumerators
{
    /// <summary>Provides the base mechanism for a parallel enumerator for 4 <seealso cref="IEnumerator{T}"/> objects.</summary>
    /// <typeparam name="T1">The type of the elements the first <seealso cref="IEnumerator{T}"/> enumerates.</typeparam>
    /// <typeparam name="T2">The type of the elements the second <seealso cref="IEnumerator{T}"/> enumerates.</typeparam>
    /// <typeparam name="T3">The type of the elements the third <seealso cref="IEnumerator{T}"/> enumerates.</typeparam>
    /// <typeparam name="T4">The type of the elements the fourth <seealso cref="IEnumerator{T}"/> enumerates.</typeparam>
    public abstract class BaseParallelEnumerator<T1, T2, T3, T4> : BaseParallelEnumerator<T1, T2, T3>
    {
        private bool enumerator4Alive = true;

        /// <summary>Gets the fourth enumerator.</summary>
        protected IEnumerator<T4> Enumerator4 { get; private set; }

        /// <summary>Determines whether the fourth enumerator has not reached the end of the collection.</summary>
        public bool Enumerator4Alive => enumerator4Alive;

        /// <summary>Initializes a new instance of the <seealso cref="BaseParallelEnumerator{T1, T2, T3, T4}"/> class.</summary>
        /// <param name="enumerator1">The first enumerator.</param>
        /// <param name="enumerator2">The second enumerator.</param>
        /// <param name="enumerator3">The third enumerator.</param>
        /// <param name="enumerator4">The fourth enumerator.</param>
        protected BaseParallelEnumerator(IEnumerable<T1> enumerator1, IEnumerable<T2> enumerator2, IEnumerable<T3> enumerator3, IEnumerable<T4> enumerator4)
            : base(enumerator1, enumerator2, enumerator3)
        {
            Enumerator4 = enumerator4.GetEnumerator();
        }

        protected override T GetCurrentValue<T>(int index)
        {
            return index switch
            {
                3 => (T)(object)GetCurrentValue(Enumerator4, enumerator4Alive),
                _ => base.GetCurrentValue<T>(index),
            };
        }

        /// <summary>Advances all enumerators to the next element of each collection.</summary>
        /// <returns><see langword="true"/> if any of the 4 enumerators was successfully advanced to the next element; <see langword="false"/> if all enumerators have passed the end of the collection.</returns>
        public override bool MoveNext()
        {
            ConditionallyMoveNext(Enumerator4, ref enumerator4Alive);
            return base.MoveNext() || enumerator4Alive;
        }
        /// <summary>Sets all enumerators to their initial position, which is before the first element in each collection.</summary>
        public override void Reset()
        {
            Enumerator4.Reset();
            enumerator4Alive = true;
        }

        /// <summary>Calls the <seealso cref="IDisposable.Dispose()"/> method on all enumerators.</summary>
        public override void Dispose()
        {
            Enumerator4.Dispose();
        }
    }
}
