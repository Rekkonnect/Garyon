using System.Collections;
using System.Collections.Generic;

namespace Garyon.Objects.Enumerators;

/// <summary>A container object allowing for 2 <seealso cref="IEnumerable{T}"/> objects to be parallelly enumerated.</summary>
public abstract class BaseParallellyEnumerable<TEnumeratorTuple> : IEnumerable<TEnumeratorTuple>
{
    /// <inheritdoc/>
    public abstract IEnumerator<TEnumeratorTuple> GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
