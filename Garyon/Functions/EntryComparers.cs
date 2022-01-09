using System;
using System.Collections.Generic;

namespace Garyon.Functions
{
    /// <summary>Represents a <seealso cref="KeyValuePair{TKey, TValue}"/> comparer.</summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface IKeyValuePairComparer<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>> { }

    /// <summary>Represents a <seealso cref="KeyValuePair{TKey, TValue}"/> comparer where both the key and the value are compared.</summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface IFullKeyValuePairComparer<TKey, TValue> : IKeyValuePairComparer<TKey, TValue>
        where TKey : IComparable<TKey>
        where TValue : IComparable<TValue>
    {
    }

    /// <summary>Represents a <seealso cref="KeyValuePair{TKey, TValue}"/> comparer that compares both the key and the value.</summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public sealed class EntryKeyOverValueComparer<TKey, TValue> : IFullKeyValuePairComparer<TKey, TValue>
        where TKey : IComparable<TKey>
        where TValue : IComparable<TValue>
    {
        // How nice would it be if we had specific singleton declaration syntax
        /// <summary>The default instance of the comparer.</summary>
        public static readonly EntryKeyOverValueComparer<TKey, TValue> Default = new();

        private EntryKeyOverValueComparer() { }

        /// <summary>Compares two <seealso cref="KeyValuePair{TKey, TValue}"/> instances.</summary>
        /// <param name="left">The left <seealso cref="KeyValuePair{TKey, TValue}"/> in the comparison.</param>
        /// <param name="right">The right <seealso cref="KeyValuePair{TKey, TValue}"/> in the comparison.</param>
        /// <returns>
        /// The comparison value of <paramref name="left"/> with <paramref name="right"/>.
        /// This is the comparison value for their respective <seealso cref="KeyValuePair{TKey, TValue}.Key"/> properties, if non-zero,
        /// otherwise the comparison value for their respective <seealso cref="KeyValuePair{TKey, TValue}.Value"/> properties.
        /// </returns>
        public int Compare(KeyValuePair<TKey, TValue> left, KeyValuePair<TKey, TValue> right)
        {
            int comparison = left.Key.CompareTo(right.Key);
            if (comparison != 0)
                return comparison;

            return left.Value.CompareTo(right.Value);
        }
    }

    /// <summary>Represents a <seealso cref="KeyValuePair{TKey, TValue}"/> comparer that compares both the key and the value.</summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public sealed class EntryValueOverKeyComparer<TKey, TValue> : IFullKeyValuePairComparer<TKey, TValue>
        where TKey : IComparable<TKey>
        where TValue : IComparable<TValue>
    {
        /// <summary>The default instance of the comparer.</summary>
        public static readonly EntryValueOverKeyComparer<TKey, TValue> Default = new();

        /// <summary>Compares two <seealso cref="KeyValuePair{TKey, TValue}"/> instances.</summary>
        /// <param name="left">The left <seealso cref="KeyValuePair{TKey, TValue}"/> in the comparison.</param>
        /// <param name="right">The right <seealso cref="KeyValuePair{TKey, TValue}"/> in the comparison.</param>
        /// <returns>
        /// The comparison value of <paramref name="left"/> with <paramref name="right"/>.
        /// This is the comparison value for their respective <seealso cref="KeyValuePair{TKey, TValue}.Value"/> properties, if non-zero,
        /// otherwise the comparison value for their respective <seealso cref="KeyValuePair{TKey, TValue}.Key"/> properties.
        /// </returns>
        public int Compare(KeyValuePair<TKey, TValue> left, KeyValuePair<TKey, TValue> right)
        {
            int comparison = left.Value.CompareTo(right.Value);
            if (comparison != 0)
                return comparison;

            return left.Key.CompareTo(right.Key);
        }
    }

    /// <summary>Represents a <seealso cref="KeyValuePair{TKey, TValue}"/> comparer that only compares the key.</summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public sealed class EntryKeyComparer<TKey, TValue> : IKeyValuePairComparer<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        /// <summary>The default instance of the comparer.</summary>
        public static readonly EntryKeyComparer<TKey, TValue> Default = new();

        /// <summary>Compares two <seealso cref="KeyValuePair{TKey, TValue}"/> instances.</summary>
        /// <param name="left">The left <seealso cref="KeyValuePair{TKey, TValue}"/> in the comparison.</param>
        /// <param name="right">The right <seealso cref="KeyValuePair{TKey, TValue}"/> in the comparison.</param>
        /// <returns>The comparison value of <paramref name="left"/> with <paramref name="right"/>, for their respective <seealso cref="KeyValuePair{TKey, TValue}.Key"/> properties.</returns>
        public int Compare(KeyValuePair<TKey, TValue> left, KeyValuePair<TKey, TValue> right)
        {
            return left.Key.CompareTo(right.Key);
        }
    }

    /// <summary>Represents a <seealso cref="KeyValuePair{TKey, TValue}"/> comparer that only compares the value.</summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public sealed class EntryValueComparer<TKey, TValue> : IKeyValuePairComparer<TKey, TValue>
        where TValue : IComparable<TValue>
    {
        /// <summary>The default instance of the comparer.</summary>
        public static readonly EntryValueComparer<TKey, TValue> Default = new();

        /// <summary>Compares two <seealso cref="KeyValuePair{TKey, TValue}"/> instances.</summary>
        /// <param name="left">The left <seealso cref="KeyValuePair{TKey, TValue}"/> in the comparison.</param>
        /// <param name="right">The right <seealso cref="KeyValuePair{TKey, TValue}"/> in the comparison.</param>
        /// <returns>The comparison value of <paramref name="left"/> with <paramref name="right"/>, for their respective <seealso cref="KeyValuePair{TKey, TValue}.Value"/> properties.</returns>
        public int Compare(KeyValuePair<TKey, TValue> left, KeyValuePair<TKey, TValue> right)
        {
            return left.Value.CompareTo(right.Value);
        }
    }
}