using System.Collections.Generic;

namespace Garyon.Objects.Enumerators;

/// <summary>A container object allowing for 2 <seealso cref="IEnumerable{T}"/> objects to be parallelly enumerated.</summary>
/// <typeparam name="T1">The type of the elements stored in the first <seealso cref="IEnumerable{T}"/>.</typeparam>
/// <typeparam name="T2">The type of the elements stored in the second <seealso cref="IEnumerable{T}"/>.</typeparam>
public sealed class ParallellyEnumerable<T1, T2> : BaseParallellyEnumerable<(T1, T2)>
{
    /// <summary>The first <seealso cref="IEnumerable{T}"/>.</summary>
    public IEnumerable<T1> Enumerable1;
    /// <summary>The second <seealso cref="IEnumerable{T}"/>.</summary>
    public IEnumerable<T2> Enumerable2;

    /// <summary>Initializes a new instance of the <seealso cref="ParallellyEnumerable{T1, T2}"/> class from 2 <seealso cref="IEnumerable{T}"/> objects.</summary>
    /// <param name="enumerable1">The first <seealso cref="IEnumerable{T}"/>.</param>
    /// <param name="enumerable2">The second <seealso cref="IEnumerable{T}"/>.</param>
    public ParallellyEnumerable(IEnumerable<T1> enumerable1, IEnumerable<T2> enumerable2)
    {
        Enumerable1 = enumerable1;
        Enumerable2 = enumerable2;
    }

    /// <summary>Gets the <seealso cref="ParallelEnumerator{T1, T2}"/> object that enumerates through the 2 collections.</summary>
    /// <returns>The <seealso cref="ParallelEnumerator{T1, T2}"/> object that enumerates through the 2 collections.</returns>
    public override IEnumerator<(T1, T2)> GetEnumerator() => new ParallelEnumerator<T1, T2>(Enumerable1, Enumerable2);
}

/// <summary>A container object allowing for 3 <seealso cref="IEnumerable{T}"/> objects to be parallelly enumerated.</summary>
/// <typeparam name="T1">The type of the elements stored in the first <seealso cref="IEnumerable{T}"/>.</typeparam>
/// <typeparam name="T2">The type of the elements stored in the second <seealso cref="IEnumerable{T}"/>.</typeparam>
/// <typeparam name="T3">The type of the elements stored in the third <seealso cref="IEnumerable{T}"/>.</typeparam>
public sealed class ParallellyEnumerable<T1, T2, T3> : BaseParallellyEnumerable<(T1, T2, T3)>
{
    /// <summary>The first <seealso cref="IEnumerable{T}"/>.</summary>
    public IEnumerable<T1> Enumerable1;
    /// <summary>The second <seealso cref="IEnumerable{T}"/>.</summary>
    public IEnumerable<T2> Enumerable2;
    /// <summary>The third <seealso cref="IEnumerable{T}"/>.</summary>
    public IEnumerable<T3> Enumerable3;

    /// <summary>Initializes a new instance of the <seealso cref="ParallellyEnumerable{T1, T2, T3}"/> class from 3 <seealso cref="IEnumerable{T}"/> objects.</summary>
    /// <param name="enumerable1">The first <seealso cref="IEnumerable{T}"/>.</param>
    /// <param name="enumerable2">The second <seealso cref="IEnumerable{T}"/>.</param>
    /// <param name="enumerable3">The third <seealso cref="IEnumerable{T}"/>.</param>
    public ParallellyEnumerable(IEnumerable<T1> enumerable1, IEnumerable<T2> enumerable2, IEnumerable<T3> enumerable3)
    {
        Enumerable1 = enumerable1;
        Enumerable2 = enumerable2;
        Enumerable3 = enumerable3;
    }

    /// <summary>Gets the <seealso cref="ParallelEnumerator{T1, T2, T3}"/> object that enumerates through the 3 collections.</summary>
    /// <returns>The <seealso cref="ParallelEnumerator{T1, T2, T3}"/> object that enumerates through the 3 collections.</returns>
    public override IEnumerator<(T1, T2, T3)> GetEnumerator() => new ParallelEnumerator<T1, T2, T3>(Enumerable1, Enumerable2, Enumerable3);
}

/// <summary>A container object allowing for 4 <seealso cref="IEnumerable{T}"/> objects to be parallelly enumerated.</summary>
/// <typeparam name="T1">The type of the elements stored in the first <seealso cref="IEnumerable{T}"/>.</typeparam>
/// <typeparam name="T2">The type of the elements stored in the second <seealso cref="IEnumerable{T}"/>.</typeparam>
/// <typeparam name="T3">The type of the elements stored in the third <seealso cref="IEnumerable{T}"/>.</typeparam>
/// <typeparam name="T4">The type of the elements stored in the fourth <seealso cref="IEnumerable{T}"/>.</typeparam>
public sealed class ParallellyEnumerable<T1, T2, T3, T4> : BaseParallellyEnumerable<(T1, T2, T3, T4)>
{
    /// <summary>The first <seealso cref="IEnumerable{T}"/>.</summary>
    public IEnumerable<T1> Enumerable1;
    /// <summary>The second <seealso cref="IEnumerable{T}"/>.</summary>
    public IEnumerable<T2> Enumerable2;
    /// <summary>The third <seealso cref="IEnumerable{T}"/>.</summary>
    public IEnumerable<T3> Enumerable3;
    /// <summary>The fourth <seealso cref="IEnumerable{T}"/>.</summary>
    public IEnumerable<T4> Enumerable4;

    /// <summary>Initializes a new instance of the <seealso cref="ParallellyEnumerable{T1, T2, T3, T4}"/> class from 4 <seealso cref="IEnumerable{T}"/> objects.</summary>
    /// <param name="enumerable1">The first <seealso cref="IEnumerable{T}"/>.</param>
    /// <param name="enumerable2">The second <seealso cref="IEnumerable{T}"/>.</param>
    /// <param name="enumerable3">The third <seealso cref="IEnumerable{T}"/>.</param>
    /// <param name="enumerable4">The fourth <seealso cref="IEnumerable{T}"/>.</param>
    public ParallellyEnumerable(IEnumerable<T1> enumerable1, IEnumerable<T2> enumerable2, IEnumerable<T3> enumerable3, IEnumerable<T4> enumerable4)
    {
        Enumerable1 = enumerable1;
        Enumerable2 = enumerable2;
        Enumerable3 = enumerable3;
        Enumerable4 = enumerable4;
    }

    /// <summary>Gets the <seealso cref="ParallelEnumerator{T1, T2, T3, T4}"/> object that enumerates through the 4 collections.</summary>
    /// <returns>The <seealso cref="ParallelEnumerator{T1, T2, T3, T4}"/> object that enumerates through the 4 collections.</returns>
    public override IEnumerator<(T1, T2, T3, T4)> GetEnumerator() => new ParallelEnumerator<T1, T2, T3, T4>(Enumerable1, Enumerable2, Enumerable3, Enumerable4);
}
