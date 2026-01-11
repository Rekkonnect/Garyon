using Garyon.Extensions;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.DataStructures;

/// <summary>
/// Provides a thread-safe set implementation based on
/// <see cref="ConcurrentDictionary{TKey, TValue}"/>.
/// </summary>
/// <typeparam name="T">The type of values stored in the set.</typeparam>
/// <remarks>
/// This encapsulates <see cref="ConcurrentDictionary{TKey, TValue}"/> as a concurrent set,
/// using <see cref="byte"/> as the value type that is mapped from the key of <typeparamref name="T"/>.
/// The idea of using byte is inspired by dotnet/roslyn.
/// </remarks>
public sealed class ConcurrentSet<T> : ISet<T>, IReadOnlyCollection<T>
    where T : notnull
{
    private const byte _addedValue = 1;
    private const byte _updatedValue = 2;

    private readonly ConcurrentDictionary<T, byte> _dictionary;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConcurrentSet{T}"/> class.
    /// </summary>
    public ConcurrentSet()
    {
        _dictionary = new();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConcurrentSet{T}"/> class
    /// with the specified equality comparer.
    /// </summary>
    public ConcurrentSet(IEqualityComparer<T> comparer)
    {
        _dictionary = new(comparer);
    }

    /// <summary>
    /// Gets the number of elements contained in the set.
    /// </summary>
    public int Count => _dictionary.Count;

    /// <summary>
    /// Gets a value indicating whether the set is empty.
    /// </summary>
    public bool IsEmpty => _dictionary.IsEmpty;

    bool ICollection<T>.IsReadOnly => false;

    /// <summary>
    /// Adds the specified value to the set.
    /// </summary>
    public bool Add(T value)
    {
        var resultValue = _dictionary.AddOrUpdate(value, _addedValue, UpdateValueFactory);
        var added = resultValue == _addedValue;
        return added;
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
        return _dictionary.TryRemove(value, out _);
    }

    /// <summary>
    /// Removes all elements from the set.
    /// </summary>
    public void Clear()
    {
        _dictionary.Clear();
    }

    private static byte UpdateValueFactory(T key, byte value)
    {
        return _updatedValue;
    }

    /// <inheritdoc/>
    public void ExceptWith(IEnumerable<T> other)
    {
        RemoveRange(other);
    }

    /// <inheritdoc/>
    public void IntersectWith(IEnumerable<T> other)
    {
        var removed = _dictionary.Keys.Except(other).ToList();
        RemoveRange(removed);
    }

    /// <inheritdoc/>
    public bool IsProperSubsetOf(IEnumerable<T> other)
    {
        var otherSet = other as ISet<T> ?? other.ToHashSet();
        return _dictionary.Keys.All(otherSet.Contains)
            && _dictionary.Count < otherSet.Count;
    }

    /// <inheritdoc/>
    public bool IsProperSupersetOf(IEnumerable<T> other)
    {
        var otherList = other.ToList();
        var distinctCounted = otherList.Distinct().WithCountCaching();
        return distinctCounted.All(Contains)
            && _dictionary.Count > distinctCounted.ForceCount();
    }

    /// <inheritdoc/>
    public bool IsSubsetOf(IEnumerable<T> other)
    {
        var otherSet = other as ISet<T> ?? other.ToHashSet();
        return _dictionary.Keys.All(otherSet.Contains);
    }

    /// <inheritdoc/>
    public bool IsSupersetOf(IEnumerable<T> other)
    {
        return other.All(Contains);
    }

    /// <inheritdoc/>
    public bool Overlaps(IEnumerable<T> other)
    {
        return other.Any(Contains);
    }

    /// <inheritdoc/>
    public bool SetEquals(IEnumerable<T> other)
    {
        var otherSet = other as ISet<T> ?? other.ToHashSet();
        return _dictionary.Count == otherSet.Count && _dictionary.Keys.All(otherSet.Contains);
    }

    /// <inheritdoc/>
    public void SymmetricExceptWith(IEnumerable<T> other)
    {
        foreach (var item in other)
        {
            if (!TryRemove(item))
            {
                Add(item);
            }
        }
    }

    /// <inheritdoc/>
    public void UnionWith(IEnumerable<T> other)
    {
        this.AddRange(other);
    }

    /// <inheritdoc/>
    public bool Contains(T item)
    {
        return _dictionary.ContainsKey(item);
    }

    void ICollection<T>.CopyTo(T[] array, int arrayIndex)
    {
        _dictionary.Keys.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc/>
    public bool Remove(T item)
    {
        return _dictionary.TryRemove(item, out _);
    }

    private void RemoveRange(IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            Remove(value);
        }
    }

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
    {
        return _dictionary.Keys.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    void ICollection<T>.Add(T item)
    {
        Add(item);
    }
}
