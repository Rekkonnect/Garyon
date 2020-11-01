using System.Collections;
using System.Collections.Generic;

namespace Garyon.Objects.Enumerators
{
    /// <summary>Provides a mechanism for parallelly enumerating 2 <seealso cref="IEnumerable{T}"/> objects.</summary>
    public sealed class ParallelEnumerator<T1, T2> : BaseParallelEnumerator<T1, T2>, IEnumerator<(T1, T2)>
    {
        /// <summary>Gets a tuple containing the current values of both enumerators. If any enumerator is beyond the end of the collection, the respective value is equal to <see langword="default"/>.</summary>
        public (T1, T2) Current => (GetCurrentValue<T1>(0), GetCurrentValue<T2>(1));
        object? IEnumerator.Current => Current;

        /// <summary>Initializes a new instance of the <seealso cref="ParallelEnumerator{T1, T2}"/> class from 2 <seealso cref="IEnumerable{T}"/> objects.</summary>
        /// <param name="enumerable1">The first enumerable.</param>
        /// <param name="enumerable2">The second enumerable.</param>
        public ParallelEnumerator(IEnumerable<T1> enumerable1, IEnumerable<T2> enumerable2)
            : base(enumerable1, enumerable2) { }
    }

    /// <summary>Provides a mechanism for parallelly enumerating 3 <seealso cref="IEnumerable{T}"/> objects.</summary>
    public sealed class ParallelEnumerator<T1, T2, T3> : BaseParallelEnumerator<T1, T2, T3>, IEnumerator<(T1, T2, T3)>
    {
        /// <summary>Gets a tuple containing the current values of all enumerators. If any enumerator is beyond the end of the collection, the respective value is equal to <see langword="default"/>.</summary>
        public (T1, T2, T3) Current => (GetCurrentValue<T1>(0), GetCurrentValue<T2>(1), GetCurrentValue<T3>(2));
        object? IEnumerator.Current => Current;

        /// <summary>Initializes a new instance of the <seealso cref="ParallelEnumerator{T1, T2, T3}"/> class from 3 <seealso cref="IEnumerable{T}"/> objects.</summary>
        /// <param name="enumerable1">The first enumerable.</param>
        /// <param name="enumerable2">The second enumerable.</param>
        /// <param name="enumerable3">The third enumerable.</param>
        public ParallelEnumerator(IEnumerable<T1> enumerable1, IEnumerable<T2> enumerable2, IEnumerable<T3> enumerable3)
            : base(enumerable1, enumerable2, enumerable3) { }
    }

    /// <summary>Provides a mechanism for parallelly enumerating 4 <seealso cref="IEnumerable{T}"/> objects.</summary>
    public sealed class ParallelEnumerator<T1, T2, T3, T4> : BaseParallelEnumerator<T1, T2, T3, T4>, IEnumerator<(T1, T2, T3, T4)>
    {
        /// <summary>Gets a tuple containing the current values of all enumerators. If any enumerator is beyond the end of the collection, the respective value is equal to <see langword="default"/>.</summary>
        public (T1, T2, T3, T4) Current => (GetCurrentValue<T1>(0), GetCurrentValue<T2>(1), GetCurrentValue<T3>(2), GetCurrentValue<T4>(3));
        object? IEnumerator.Current => Current;

        /// <summary>Initializes a new instance of the <seealso cref="ParallelEnumerator{T1, T2, T3}"/> class from 4 <seealso cref="IEnumerable{T}"/> objects.</summary>
        /// <param name="enumerable1">The first enumerable.</param>
        /// <param name="enumerable2">The second enumerable.</param>
        /// <param name="enumerable3">The third enumerable.</param>
        /// <param name="enumerable3">The fourth enumerable.</param>
        public ParallelEnumerator(IEnumerable<T1> enumerable1, IEnumerable<T2> enumerable2, IEnumerable<T3> enumerable3, IEnumerable<T4> enumerable4)
            : base(enumerable1, enumerable2, enumerable3, enumerable4) { }
    }
}
