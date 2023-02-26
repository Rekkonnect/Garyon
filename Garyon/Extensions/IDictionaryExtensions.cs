#nullable enable

using Garyon.Functions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

/// <summary>
/// Provides useful extensions for the
/// <seealso cref="IDictionary{TKey, TValue}"/>
/// and <seealso cref="IDictionary"/> types.
/// </summary>
public static class IDictionaryExtensions
{
    #region IDictionary
    /// <summary>Converts the keys of the dictionary into a collection of strongly-typed elements.</summary>
    /// <typeparam name="T">The type to cast all the keys of the dictionary into.</typeparam>
    /// <param name="dictionary">The dictionary whose keys to convert into a strongly-typed element collection.</param>
    /// <returns>An <seealso cref="IEnumerable{T}"/> containing the keys of the provided dictionary.</returns>
    public static IEnumerable<T> Keys<T>(this IDictionary dictionary)
    {
        return dictionary.Keys.Cast<T>();
    }
    /// <summary>Converts the values of the dictionary into a collection of strongly-typed elements.</summary>
    /// <typeparam name="T">The type to cast all the values of the dictionary into.</typeparam>
    /// <param name="dictionary">The dictionary whose values to convert into a strongly-typed element collection.</param>
    /// <returns>An <seealso cref="IEnumerable{T}"/> containing the values of the provided dictionary.</returns>
    public static IEnumerable<T> Values<T>(this IDictionary dictionary)
    {
        return dictionary.Values.Cast<T>();
    }
    #endregion

    #region IDictionary<T>
    // Leeches
    /// <summary>Increments the value of a key if it exists, otherwise creates a new key with the default value 1.</summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <param name="d">The dictionary whose value to increment for the specified key.</param>
    /// <param name="key">The key in the dictionary whose value to increment.</param>
    public static void IncrementOrAddKeyValue<TKey>(this IDictionary<TKey, int> d, TKey key)
    {
        if (d.ContainsKey(key))
            d[key]++;
        else
            d.Add(key, 1);
    }
    /// <summary>Calculates the sum of all the values in the dictionary, excluding specific keys.</summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <param name="d">The dictionary whose value sum to calculate.</param>
    /// <param name="exclusions">The keys that will be excluded from the sum.</param>
    public static int Sum<TKey>(this IDictionary<TKey, int> d, params TKey[] exclusions)
    {
        int sum = 0;
        foreach (var v in d.Values)
            sum += v;
        foreach (var e in exclusions)
            if (d.ContainsKey(e))
                sum -= d[e];
        return sum;
    }

    /// <summary>Gets the value mapped to the given key within the dictionary, if the key is present.</summary>
    /// <typeparam name="TKey">The type of the keys stored in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values stored in the dictionary.</typeparam>
    /// <param name="dictionary">The dictionary whose mapped value to get.</param>
    /// <param name="key">The key whose mapped value to get.</param>
    /// <returns>The associated value to <paramref name="key"/>, if it exists, otherwise <see langword="default"/>.</returns>
    public static TValue? ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey? key)
    {
        return ValueOrDefault(dictionary, key, default);
    }

    /// <summary>Gets the value mapped to the given key within the dictionary, if the key is present.</summary>
    /// <typeparam name="TKey">The type of the keys stored in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values stored in the dictionary.</typeparam>
    /// <param name="dictionary">The dictionary whose mapped value to get.</param>
    /// <param name="key">The key whose mapped value to get.</param>
    /// <param name="defaultValue">The default value to return if the key is <see langword="null"/> or is not found in the dictionary.</param>
    /// <returns>The associated value to <paramref name="key"/>, if it exists, otherwise the specified default value.</returns>
    public static TValue? ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey? key, TValue? defaultValue)
    {
        if (key is null)
            return defaultValue;

        if (!dictionary.TryGetValue(key, out var value))
            return defaultValue;

        return value;
    }

    /// <summary>Adds a new entry to the dictionary. If the given key already exists, its value is overwritten in the source dictionary.</summary>
    /// <typeparam name="TKey">The type of the keys stored in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values stored in the dictionary.</typeparam>
    /// <param name="source">The source dictionary.</param>
    /// <param name="key">The key of the entry to add or overwrite.</param>
    /// <param name="value">The value of the entry to set.</param>
    /// <returns><see langword="true"/> if the entry already existed with a different value and was overwritten, otherwise <see langword="false"/>.</returns>
    public static bool AddOrSet<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value)
    {
        var contained = source.TryGetValue(key, out var oldValue);

        if (!contained)
            source.Add(key, value);
        else
        {
            if (Checks.SafeEquals(oldValue, value))
                return false;

            source[key] = value;
        }

        return contained;
    }
    /// <inheritdoc cref="TryAddPreserve{TKey, TValue}(IDictionary{TKey, TValue}, KeyValuePair{TKey, TValue}, out TValue)"/>
    public static bool TryAddPreserve<TKey, TValue>(this IDictionary<TKey, TValue> source, KeyValuePair<TKey, TValue> kvp)
    {
        return source.TryAddPreserve(kvp.Key, kvp.Value);
    }
    /// <inheritdoc cref="TryAddPreserve{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue, out TValue)"/>
    /// <param name="kvp">The <seealso cref="KeyValuePair{TKey, TValue}"/> to add to the dictionary.</param>
    public static bool TryAddPreserve<TKey, TValue>(this IDictionary<TKey, TValue> source, KeyValuePair<TKey, TValue> kvp, out TValue? existingValue)
    {
        return source.TryAddPreserve(kvp.Key, kvp.Value, out existingValue);
    }
    /// <inheritdoc cref="TryAddPreserve{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue, out TValue)"/>
    public static bool TryAddPreserve<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value)
    {
        return source.TryAddPreserve(key, value, out _);
    }
    /// <summary>Adds a new entry to the dictionary. If the given key already exists, its value is preserved in the source dictionary.</summary>
    /// <typeparam name="TKey">The type of the keys stored in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values stored in the dictionary.</typeparam>
    /// <param name="source">The source dictionary.</param>
    /// <param name="key">The key of the entry to add or overwrite.</param>
    /// <param name="value">The value of the entry to set.</param>
    /// <param name="existingValue">The value that existed in the dictionary, if the key was already present, otherwise <see langword="default"/>.</param>
    /// <returns><see langword="true"/> if the entry did not exist, or existed with the same value, otherwise <see langword="false"/>.</returns>
    public static bool TryAddPreserve<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value, out TValue? existingValue)
    {
        var available = !source.TryGetValue(key, out existingValue);

        if (available)
            source.Add(key, value);
        else
        {
            if (Checks.SafeEquals(existingValue, value))
                return true;
        }

        return available;
    }

    /// <summary>Adds a range of entries to a source dictionary.</summary>
    /// <typeparam name="TKey">The type of the keys stored in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values stored in the dictionary.</typeparam>
    /// <param name="source">The source dictionary.</param>
    /// <param name="entries">The entries to add to the <paramref name="source"/> dictionary.</param>
    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> entries)
    {
        foreach (var entry in entries)
            source.Add(entry.Key, entry.Value);
    }
    /// <summary>Adds a collection of new entries to the dictionary. For each of the given entries, if its key already exists, its value is overwritten in the source dictionary.</summary>
    /// <typeparam name="TKey">The type of the keys stored in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values stored in the dictionary.</typeparam>
    /// <param name="source">The source dictionary.</param>
    /// <param name="entries">The entries to add or overwrite.</param>
    /// <returns><see langword="true"/> if at least one of the given entries already existed with a different value and was overwritten, otherwise <see langword="false"/>.</returns>
    public static bool AddOrSetRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> entries)
    {
        bool overwritten = false;
        foreach (var entry in entries)
            overwritten |= source.AddOrSet(entry.Key, entry.Value);
        return overwritten;
    }
    /// <summary>Adds a collection of new entries to the dictionary. For each of the given entries, if its key already exists, its value is preserved in the source dictionary.</summary>
    /// <typeparam name="TKey">The type of the keys stored in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values stored in the dictionary.</typeparam>
    /// <param name="source">The source dictionary.</param>
    /// <param name="entries">The entries to add or overwrite.</param>
    /// <returns><see langword="true"/> if none of the given entries already existed with a different value, otherwise <see langword="false"/>.</returns>
    public static bool TryAddPreserveRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> entries)
    {
        bool preserved = true;
        foreach (var entry in entries)
            preserved &= source.TryAddPreserve(entry.Key, entry.Value);
        return preserved;
    }

    /// <summary>Gets a <seealso cref="KeyValuePair{TKey, TValue}"/> instance containing the specified key and its associated value.</summary>
    /// <typeparam name="TKey">The type of the keys stored in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values stored in the dictionary.</typeparam>
    /// <param name="source">The source dictionary.</param>
    /// <param name="key">The key of the entry whose <seealso cref="KeyValuePair{TKey, TValue}"/> to get.</param>
    /// <returns>The resulting <seealso cref="KeyValuePair{TKey, TValue}"/> containing the entry that was requested.</returns>
    public static KeyValuePair<TKey, TValue> GetKeyValuePair<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
    {
        return new(key, source[key]);
    }

    /// <summary>Adds an entry to the provided dictionary, if the key does not exist.</summary>
    /// <typeparam name="TKey">The type of the keys stored in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values stored in the dictionary.</typeparam>
    /// <param name="source">The source dictionary.</param>
    /// <param name="kvp">The entry to add to the dictionary.</param>
    public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> source, KeyValuePair<TKey, TValue> kvp)
    {
        source.Add(kvp);
    }
#if !HAS_DICTIONARY_TRYADD
    /// <summary>Attempts to add an entry to the provided dictionary, if the key does not already exist.</summary>
    /// <typeparam name="TKey">The type of the keys stored in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values stored in the dictionary.</typeparam>
    /// <param name="source">The source dictionary.</param>
    /// <param name="key">The key of the entry to add to the dictionary.</param>
    /// <param name="value">The value of the entry to add to the dictionary.</param>
    /// <returns><see langword="true"/> if the entry was successsfully added, otherwise <see langword="false"/>.</returns>
    public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value)
    {
        return source.TryAddPreserve(key, value);
    }
#endif
    /// <summary>Attempts to add an entry to the provided dictionary, if the key does not already exist.</summary>
    /// <typeparam name="TKey">The type of the keys stored in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values stored in the dictionary.</typeparam>
    /// <param name="source">The source dictionary.</param>
    /// <param name="kvp">The entry to add to the dictionary.</param>
    /// <returns><see langword="true"/> if the entry was successsfully added, otherwise <see langword="false"/>.</returns>
    public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> source, KeyValuePair<TKey, TValue> kvp)
    {
        return source.TryAdd(kvp.Key, kvp.Value);
    }
    #endregion
}
