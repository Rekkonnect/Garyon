using System;

namespace Garyon.Objects.Enumerators;

/// <summary>Represents an indexed enumerator result.</summary>
/// <param name="Index">The index of the enumerator result.</param>
/// <param name="Current">The current enumerator result.</param>
/// <typeparam name="T">The type of the enumerator result.</typeparam>
public record IndexedEnumeratorResult<T>(int Index, T Current)
{
    public override int GetHashCode() => HashCode.Combine(Index, Current);
    public override string ToString() => $"{Index} - {Current}";
}
