using System.Collections.Generic;

namespace Garyon.Extensions;

/// <summary>Provides extensions for the <seealso cref="KeyValuePair{TKey, TValue}"/> struct.</summary>
public static class KeyValuePairExtensions
{
    /// <summary>Creates a new <seealso cref="KeyValuePair{TKey, TValue}"/> from the given existing one, but with a different key.</summary>
    /// <typeparam name="TKey">The type of the key stored in the <seealso cref="KeyValuePair{TKey, TValue}"/>.</typeparam>
    /// <typeparam name="TValue">The type of the value stored in the <seealso cref="KeyValuePair{TKey, TValue}"/>.</typeparam>
    /// <param name="kvp">The given <seealso cref="KeyValuePair{TKey, TValue}"/>.</param>
    /// <param name="key">The new key to use in the new <seealso cref="KeyValuePair{TKey, TValue}"/>.</param>
    /// <returns>A new <seealso cref="KeyValuePair{TKey, TValue}"/> instance with <paramref name="key"/> as the key, and the value of <paramref name="kvp"/>.</returns>
    public static KeyValuePair<TKey, TValue> WithKey<TKey, TValue>(this KeyValuePair<TKey, TValue> kvp, TKey key) => new(key, kvp.Value);

    /// <summary>Creates a new <seealso cref="KeyValuePair{TKey, TValue}"/> from the given existing one, but with a different value.</summary>
    /// <typeparam name="TKey">The type of the key stored in the <seealso cref="KeyValuePair{TKey, TValue}"/>.</typeparam>
    /// <typeparam name="TValue">The type of the value stored in the <seealso cref="KeyValuePair{TKey, TValue}"/>.</typeparam>
    /// <param name="kvp">The given <seealso cref="KeyValuePair{TKey, TValue}"/>.</param>
    /// <param name="value">The new value to use in the new <seealso cref="KeyValuePair{TKey, TValue}"/>.</param>
    /// <returns>A new <seealso cref="KeyValuePair{TKey, TValue}"/> instance with the key of <paramref name="kvp"/>, and <paramref name="value"/> as the value.</returns>
    public static KeyValuePair<TKey, TValue> WithValue<TKey, TValue>(this KeyValuePair<TKey, TValue> kvp, TValue value) => new(kvp.Key, value);

    /// <summary>Creates a new <seealso cref="KeyValuePair{TKey, TValue}"/> from the given existing one, but with a different key.</summary>
    /// <typeparam name="TKey">The type of the key stored in the original <seealso cref="KeyValuePair{TKey, TValue}"/>.</typeparam>
    /// <typeparam name="TValue">The type of the value stored in the <seealso cref="KeyValuePair{TKey, TValue}"/>.</typeparam>
    /// <typeparam name="TNewKey">The type of the key stored in the new <seealso cref="KeyValuePair{TKey, TValue}"/>.</typeparam>
    /// <param name="kvp">The given <seealso cref="KeyValuePair{TKey, TValue}"/>.</param>
    /// <param name="key">The new key to use in the new <seealso cref="KeyValuePair{TKey, TValue}"/>.</param>
    /// <returns>A new <seealso cref="KeyValuePair{TKey, TValue}"/> instance with <paramref name="key"/> as the key, and the value of <paramref name="kvp"/>.</returns>
    public static KeyValuePair<TNewKey, TValue> WithKey<TKey, TValue, TNewKey>(this KeyValuePair<TKey, TValue> kvp, TNewKey key) => new(key, kvp.Value);

    /// <summary>Creates a new <seealso cref="KeyValuePair{TKey, TValue}"/> from the given existing one, but with a different value.</summary>
    /// <typeparam name="TKey">The type of the key stored in the <seealso cref="KeyValuePair{TKey, TValue}"/>.</typeparam>
    /// <typeparam name="TValue">The type of the value stored in the original <seealso cref="KeyValuePair{TKey, TValue}"/>.</typeparam>
    /// <typeparam name="TNewValue">The type of the value stored in the new <seealso cref="KeyValuePair{TKey, TValue}"/>.</typeparam>
    /// <param name="kvp">The given <seealso cref="KeyValuePair{TKey, TValue}"/>.</param>
    /// <param name="value">The new value to use in the new <seealso cref="KeyValuePair{TKey, TValue}"/>.</param>
    /// <returns>A new <seealso cref="KeyValuePair{TKey, TValue}"/> instance with the key of <paramref name="kvp"/>, and <paramref name="value"/> as the value.</returns>
    public static KeyValuePair<TKey, TNewValue> WithValue<TKey, TValue, TNewValue>(this KeyValuePair<TKey, TValue> kvp, TNewValue value) => new(kvp.Key, value);
}