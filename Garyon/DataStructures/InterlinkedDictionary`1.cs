using System;
using System.Collections.Generic;

namespace Garyon.DataStructures;

/// <summary>
/// Represents a collection of interlinked pairs of values of the same type,
/// where the first and second components are mapped to each other.
/// </summary>
/// <typeparam name="T">
/// The type of both components of the pairs.
/// </typeparam>
public sealed class InterlinkedDictionary<T>
    where T : notnull
{
    private readonly Dictionary<T, T> t1Dictionary;
    private readonly Dictionary<T, T> t2Dictionary;

    /// <summary>
    /// Gets the count of pairs that are present in the dictionary.
    /// </summary>
    public int Count => t1Dictionary.Count;

    /// <summary>
    /// Gets all the currently stored values of the first component.
    /// </summary>
    public IEnumerable<T> Values1 => t1Dictionary.Keys;
    /// <summary>
    /// Gets all the currently stored values of the second component.
    /// </summary>
    public IEnumerable<T> Values2 => t2Dictionary.Keys;
    /// <summary>
    /// Gets all the currently stored pairs of values.
    /// </summary>
    public IEnumerable<(T, T)> ValuePairs
    {
        get
        {
            foreach (var pair in t1Dictionary)
                yield return (pair.Key, pair.Value);
        }
    }

    /// <summary>
    /// Initializes a new empty <seealso cref="InterlinkedDictionary{T}"/>.
    /// </summary>
    public InterlinkedDictionary()
    {
        t1Dictionary = new();
        t2Dictionary = new();
    }
    /// <summary>
    /// Initializes a new <seealso cref="InterlinkedDictionary{T}"/> out of
    /// another, copying the pairs to this new instance.
    /// </summary>
    /// <param name="other">
    /// The other <see cref="InterlinkedDictionary{T}"/> from which to copy the
    /// pairs. That instance remains unaffected upon performing operations on
    /// this new one.
    /// </param>
    public InterlinkedDictionary(InterlinkedDictionary<T> other)
    {
        t1Dictionary = new(other.t1Dictionary);
        t2Dictionary = new(other.t2Dictionary);
    }

    /// <summary>
    /// Determines whether a value exists as the first component of a stored
    /// pair.
    /// </summary>
    public bool Contains1(T value) => t1Dictionary.ContainsKey(value);
    /// <summary>
    /// Determines whether a value exists as the second component of a stored
    /// pair.
    /// </summary>
    public bool Contains2(T value) => t2Dictionary.ContainsKey(value);

    /// <summary>
    /// Adds a pair of values to the dictionary.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Thrown if one of the values already exists in the dictionary.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown if any of the values is <see langword="null"/>.
    /// </exception>
    public void Add(T t1Value, T t2Value)
    {
        if (t1Value is null || t2Value is null)
            throw new ArgumentNullException($"One of the provided values was null (passed values: {t1Value}, {t2Value})");

        AssertAlreadyExisting(t1Dictionary, t1Value);
        AssertAlreadyExisting(t2Dictionary, t2Value);

        t1Dictionary.Add(t1Value, t2Value);
        t2Dictionary.Add(t2Value, t1Value);

        static void AssertAlreadyExisting(Dictionary<T, T> dictionary, T key)
        {
            if (dictionary.ContainsKey(key))
                throw new ArgumentException($"The value {key} already exists.");
        }
    }

    /// <summary>
    /// Removes a stored pair by its first component, if it exists.
    /// </summary>
    public bool Remove1(T t1Value)
    {
        if (!t1Dictionary.TryGetValue(t1Value, out var t2Value))
            return false;

        return Remove(t1Value, t2Value);
    }
    /// <summary>
    /// Removes a stored pair by its second component, if it exists.
    /// </summary>
    public bool Remove2(T t2Value)
    {
        if (!t2Dictionary.TryGetValue(t2Value, out var t1Value))
            return false;

        return Remove(t1Value, t2Value);
    }

    private bool Remove(T t1Value, T t2Value)
    {
        t1Dictionary.Remove(t1Value);
        t2Dictionary.Remove(t2Value);
        return true;
    }

    /// <summary>
    /// Clears the dictionary, removing all stored pairs.
    /// </summary>
    public void Clear()
    {
        t1Dictionary.Clear();
        t2Dictionary.Clear();
    }

    /// <summary>
    /// Attempts to get the paired second component for a stored first
    /// component.
    /// </summary>
    public bool TryGetValue1(T t1Key, out T? t2Value)
    {
        return t1Dictionary.TryGetValue(t1Key, out t2Value);
    }
    /// <summary>
    /// Attempts to get the paired first component for a stored second
    /// component.
    /// </summary>
    public bool TryGetValue2(T t2Key, out T? t1Value)
    {
        return t2Dictionary.TryGetValue(t2Key, out t1Value);
    }

    /// <summary>
    /// Gets the paired second component for a stored first component, or
    /// <see langword="default"/> if it is not present.
    /// </summary>
    public T? ValueOrDefault1(T key)
    {
        TryGetValue1(key, out var value);
        return value;
    }
    /// <summary>
    /// Gets the paired first component for a stored second component, or
    /// <see langword="default"/> if it is not present.
    /// </summary>
    public T? ValueOrDefault2(T key)
    {
        TryGetValue2(key, out var value);
        return value;
    }

    /// <summary>
    /// Gets the paired second component for a stored first component.
    /// </summary>
    public T GetValue1(T t1Key)
    {
        return t1Dictionary[t1Key];
    }
    /// <summary>
    /// Gets the paired first component for a stored second component.
    /// </summary>
    public T GetValue2(T t2Key)
    {
        return t2Dictionary[t2Key];
    }

    /// <summary>
    /// Changes the paired second component for a stored first component.
    /// </summary>
    public void SetValue1(T t1Key, T t2Value)
    {
        ChangeValue(t1Dictionary, t2Dictionary, t1Key, t2Value);
    }
    /// <summary>
    /// Changes the paired first component for a stored second component.
    /// </summary>
    public void SetValue2(T t2Key, T t1Value)
    {
        ChangeValue(t2Dictionary, t1Dictionary, t2Key, t1Value);
    }

    private static void ChangeValue(
        Dictionary<T, T> focusedDictionary,
        Dictionary<T, T> otherDictionary,
        T key,
        T value)
    {
        var oldValue = focusedDictionary[key];
        focusedDictionary[key] = value;
        otherDictionary.Remove(oldValue);
        otherDictionary.Add(value, key);
    }
}
