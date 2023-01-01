using System.Collections.Generic;

namespace Garyon.Objects;

/// <summary>Represents a value selector function, which transforms a provided key into a value, within a <seealso cref="KeyValuePair{TKey, TValue}"/>.</summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TValue">The type of the value.</typeparam>
/// <param name="key">The key to transform into a value.</param>
/// <returns>The transformed value.</returns>
public delegate TValue ValueSelector<TKey, TValue>(TKey key);
/// <summary>Represents a key selector function, which transforms a provided value into a key, within a <seealso cref="KeyValuePair{TKey, TValue}"/>.</summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TValue">The type of the value.</typeparam>
/// <param name="value">The value to transform into a key.</param>
/// <returns>The transformed key.</returns>
public delegate TKey KeySelector<TKey, TValue>(TValue value);
