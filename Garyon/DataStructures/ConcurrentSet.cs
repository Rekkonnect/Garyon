using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Garyon.DataStructures;

/// <summary>
/// Provides a thread-safe set implementation based on
/// <see cref="ConcurrentDictionary{TKey, TValue}"/>.
/// </summary>
/// <typeparam name="T">The type of values stored in the set.</typeparam>
/// <remarks>
/// This extends <see cref="ConcurrentDictionary{TKey, TValue}"/> as a quick
/// implementation of a concurrent set, using <see cref="byte"/> as the value
/// type that is mapped from the key of <typeparamref name="T"/>.
/// The idea of using byte is inspired by dotnet/roslyn.
/// </remarks>
public sealed class ConcurrentSet<T> : ConcurrentDictionary<T, byte>
    where T : notnull
{
    /// <summary>
    /// Shadows the <see cref="ConcurrentDictionary{TKey, TValue}.Values"/>
    /// property 
    /// </summary>
    /// <returns>
    /// The value of <see cref="ConcurrentDictionary{TKey, TValue}.Keys"/>.
    /// </returns>
    public new ICollection<T> Values => Keys;

    /// <summary>
    /// Adds the specified value to the set.
    /// </summary>
    public void Add(T value)
    {
        AddOrUpdate(value, (byte)default, UpdateValueFactory);
    }

    /// <summary>
    /// Tries to remove the value from the set.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the value was successfully removed;
    /// otherwise <see langword="false"/>.
    /// </returns>
    public bool TryRemove(T value)
    {
        return TryRemove(value, out _);
    }

    private static byte UpdateValueFactory(T key, byte value)
    {
        return default;
    }
}
