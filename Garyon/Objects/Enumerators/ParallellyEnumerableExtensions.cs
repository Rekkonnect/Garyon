using System.Collections.Generic;

namespace Garyon.Objects.Enumerators;

/// <summary>Provides extensions that revolve around parallelly enumerable collections.</summary>
public static class ParallellyEnumerableExtensions
{
    /// <summary>Creates an instance of <seealso cref="ParallellyEnumerable{T1, T2}"/> out of a tuple of 2 collections.</summary>
    /// <typeparam name="T1">The type of the elements stored in the first collection.</typeparam>
    /// <typeparam name="T2">The type of the elements stored in the second collection.</typeparam>
    /// <param name="enumerables">The tuple of 2 enumerables that will be parallelly enumerated.</param>
    /// <returns>The instance of <seealso cref="ParallellyEnumerable{T1, T2}"/> that will parallelly enumerate the 2 collections.</returns>
    public static ParallellyEnumerable<T1, T2> AsParallellyEnumerable<T1, T2>(this (IEnumerable<T1>, IEnumerable<T2>) enumerables)
    {
        return new(enumerables.Item1, enumerables.Item2);
    }
    /// <summary>Creates an instance of <seealso cref="ParallellyEnumerable{T1, T2, T3}"/> out of a tuple of 3 collections.</summary>
    /// <typeparam name="T1">The type of the elements stored in the first collection.</typeparam>
    /// <typeparam name="T2">The type of the elements stored in the second collection.</typeparam>
    /// <typeparam name="T3">The type of the elements stored in the third collection.</typeparam>
    /// <param name="enumerables">The tuple of 3 enumerables that will be parallelly enumerated.</param>
    /// <returns>The instance of <seealso cref="ParallellyEnumerable{T1, T2, T3}"/> that will parallelly enumerate the 3 collections.</returns>
    public static ParallellyEnumerable<T1, T2, T3> AsParallellyEnumerable<T1, T2, T3>(this (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>) enumerables)
    {
        return new(enumerables.Item1, enumerables.Item2, enumerables.Item3);
    }
    /// <summary>Creates an instance of <seealso cref="ParallellyEnumerable{T1, T2, T3, T4}"/> out of a tuple of 4 collections.</summary>
    /// <typeparam name="T1">The type of the elements stored in the first collection.</typeparam>
    /// <typeparam name="T2">The type of the elements stored in the second collection.</typeparam>
    /// <typeparam name="T3">The type of the elements stored in the third collection.</typeparam>
    /// <typeparam name="T4">The type of the elements stored in the fourth collection.</typeparam>
    /// <param name="enumerables">The tuple of 4 enumerables that will be parallelly enumerated.</param>
    /// <returns>The instance of <seealso cref="ParallellyEnumerable{T1, T2, T3, T4}"/> that will parallelly enumerate the 4 collections.</returns>
    public static ParallellyEnumerable<T1, T2, T3, T4> AsParallellyEnumerable<T1, T2, T3, T4>(this (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>) enumerables)
    {
        return new(enumerables.Item1, enumerables.Item2, enumerables.Item3, enumerables.Item4);
    }
}
