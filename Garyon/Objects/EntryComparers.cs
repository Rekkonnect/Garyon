using System;
using System.Collections.Generic;

#nullable enable

namespace Garyon.Objects;

/// <summary>Represents a comparer for <seealso cref="KeyValuePair{TKey, TValue}"/>.</summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TValue">The type of the value.</typeparam>
public interface IKeyValuePairComparer<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>> { }

/// <summary>Represents a comparer for <seealso cref="KeyValuePair{TKey, TValue}"/> that demands that both the key and the value implement <seealso cref="IComparable{T}"/>.</summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TValue">The type of the value.</typeparam>
public interface IFullKeyValuePairComparer<TKey, TValue> : IKeyValuePairComparer<TKey, TValue>
    where TKey : IComparable<TKey>
    where TValue : IComparable<TValue>
{
}

/// <summary>Represents a <seealso cref="KeyValuePair{TKey, TValue}"/> comparer, where the result relies on the comparison result between the keys, and then the values, if the keys are equal.</summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TValue">The type of the value.</typeparam>
public sealed class EntryKeyOverValueComparer<TKey, TValue> : IFullKeyValuePairComparer<TKey, TValue>
    where TKey : IComparable<TKey>
    where TValue : IComparable<TValue>
{
    /// <summary>The default instance of the <seealso cref="EntryKeyOverValueComparer{TKey, TValue}"/> class.</summary>
    public static readonly EntryKeyOverValueComparer<TKey, TValue> Default = new();

    /// <summary>Compares two <seealso cref="KeyValuePair{TKey, TValue}"/> instances given this comparer's rules.</summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <returns>The comparison result between the keys, if not 0, otherwise the comparison result between the values. For all comparisons, order is matched.</returns>
    public int Compare(KeyValuePair<TKey, TValue> left, KeyValuePair<TKey, TValue> right)
    {
        int comparison = left.Key.CompareTo(right.Key);
        if (comparison != 0)
            return comparison;

        return left.Value.CompareTo(right.Value);
    }
}

/// <summary>Represents a <seealso cref="KeyValuePair{TKey, TValue}"/> comparer, where the result relies on the comparison result between the values, and then the keys, if the values are equal.</summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TValue">The type of the value.</typeparam>
public sealed class EntryValueOverKeyComparer<TKey, TValue> : IFullKeyValuePairComparer<TKey, TValue>
    where TKey : IComparable<TKey>
    where TValue : IComparable<TValue>
{
    /// <summary>The default instance of the <seealso cref="EntryValueOverKeyComparer{TKey, TValue}"/> class.</summary>
    public static readonly EntryValueOverKeyComparer<TKey, TValue> Default = new();

    /// <summary>Compares two <seealso cref="KeyValuePair{TKey, TValue}"/> instances given this comparer's rules.</summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <returns>The comparison result between the values, if not 0, otherwise the comparison result between the keys. For all comparisons, order is matched.</returns>
    public int Compare(KeyValuePair<TKey, TValue> left, KeyValuePair<TKey, TValue> right)
    {
        int comparison = left.Value.CompareTo(right.Value);
        if (comparison != 0)
            return comparison;

        return left.Key.CompareTo(right.Key);
    }
}

/// <summary>Represents a <seealso cref="KeyValuePair{TKey, TValue}"/> comparer, where the result only relies on the comparison result between the keys.</summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TValue">The type of the value.</typeparam>
public sealed class EntryKeyComparer<TKey, TValue> : IKeyValuePairComparer<TKey, TValue>
    where TKey : IComparable<TKey>
{
    /// <summary>The default instance of the <seealso cref="EntryKeyComparer{TKey, TValue}"/> class.</summary>
    public static readonly EntryKeyComparer<TKey, TValue> Default = new();

    /// <summary>Compares two <seealso cref="KeyValuePair{TKey, TValue}"/> instances given this comparer's rules.</summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <returns>The comparison result between the keys. For the comparison, order is matched.</returns>
    public int Compare(KeyValuePair<TKey, TValue> left, KeyValuePair<TKey, TValue> right)
    {
        return left.Key.CompareTo(right.Key);
    }
}

/// <summary>Represents a <seealso cref="KeyValuePair{TKey, TValue}"/> comparer, where the result only relies on the comparison result between the values.</summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TValue">The type of the value.</typeparam>
public sealed class EntryValueComparer<TKey, TValue> : IKeyValuePairComparer<TKey, TValue>
    where TValue : IComparable<TValue>
{
    /// <summary>The default instance of the <seealso cref="EntryValueComparer{TKey, TValue}"/> class.</summary>
    public static readonly EntryValueComparer<TKey, TValue> Default = new();

    /// <summary>Compares two <seealso cref="KeyValuePair{TKey, TValue}"/> instances given this comparer's rules.</summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <returns>The comparison result between the values. For the comparison, order is matched.</returns>
    public int Compare(KeyValuePair<TKey, TValue> left, KeyValuePair<TKey, TValue> right)
    {
        return left.Value.CompareTo(right.Value);
    }
}
