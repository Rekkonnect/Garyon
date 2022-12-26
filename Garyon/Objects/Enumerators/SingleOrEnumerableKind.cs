using System.Collections.Generic;

namespace Garyon.Objects.Enumerators;

/// <summary>
/// Represents the kind of values stored in a
/// <seealso cref="SingleOrEnumerable{T}"/>.
/// </summary>
public enum SingleOrEnumerableKind
{
    /// <summary>
    /// No value is stored, and thus nothing is enumerated.
    /// </summary>
    None,
    /// <summary>
    /// A single value is stored.
    /// </summary>
    Single,
    /// <summary>
    /// An enumerable of values is stored. Note that the enumerable
    /// could still only contain a single value, however it implements
    /// the <seealso cref="IEnumerable{T}"/> interface.
    /// </summary>
    Enumerable,
}
