using System.Collections.Generic;

namespace Garyon.Objects.Enumerators;

/// <summary>
/// Provides extensions for quickly affixing enumerables.
/// </summary>
public static class AffixedEnumerableExtensions
{
    /// <summary>
    /// Returns an <seealso cref="AffixedEnumerable{T}"/> from the main
    /// enumerable with the specified single value as the prefix.
    /// </summary>
    /// <typeparam name="T">The type of values contained in the enumerable.</typeparam>
    /// <param name="enumerable">The main enumerable that is being prefixed.</param>
    /// <param name="prefix">The single value to prepend before the main enumerable.</param>
    /// <returns>
    /// An <seealso cref="AffixedEnumerable{T}"/> that contains the main enumerable
    /// prefixed by the given single value.
    /// </returns>
    public static AffixedEnumerable<T> WithPrefix<T>(this IEnumerable<T> enumerable, T prefix)
    {
        return new AffixedEnumerable<T>(enumerable).WithPrefix(prefix);
    }
    /// <summary>
    /// Returns an <seealso cref="AffixedEnumerable{T}"/> from the main
    /// enumerable with the specified enumerable as the prefix.
    /// </summary>
    /// <typeparam name="T">The type of values contained in the enumerable.</typeparam>
    /// <param name="enumerable">The main enumerable that is being prefixed.</param>
    /// <param name="prefix">The enumerable to prepend before the main enumerable.</param>
    /// <returns>
    /// An <seealso cref="AffixedEnumerable{T}"/> that contains the main enumerable
    /// prefixed by the given enumerable.
    /// </returns>
    public static AffixedEnumerable<T> WithPrefix<T>(this IEnumerable<T> enumerable, IEnumerable<T> prefix)
    {
        return new AffixedEnumerable<T>(enumerable).WithPrefix(prefix);
    }
    /// <summary>
    /// Returns an <seealso cref="AffixedEnumerable{T}"/> from the main
    /// enumerable with the specified enumerable as the prefix.
    /// </summary>
    /// <typeparam name="T">The type of values contained in the enumerable.</typeparam>
    /// <param name="enumerable">The main enumerable that is being prefixed.</param>
    /// <param name="prefix">The enumerable to prepend before the main enumerable.</param>
    /// <returns>
    /// An <seealso cref="AffixedEnumerable{T}"/> that contains the main enumerable
    /// prefixed by the given enumerable.
    /// </returns>
    public static AffixedEnumerable<T> WithPrefix<T>(this IEnumerable<T> enumerable, params T[] prefix)
    {
        return new AffixedEnumerable<T>(enumerable).WithPrefix(prefix);
    }

    /// <summary>
    /// Returns an <seealso cref="AffixedEnumerable{T}"/> from the main
    /// enumerable with the specified single value as the suffix.
    /// </summary>
    /// <typeparam name="T">The type of values contained in the enumerable.</typeparam>
    /// <param name="enumerable">The main enumerable that is being suffixed.</param>
    /// <param name="suffix">The single value to prepend before the main enumerable.</param>
    /// <returns>
    /// An <seealso cref="AffixedEnumerable{T}"/> that contains the main enumerable
    /// suffixed by the given single value.
    /// </returns>
    public static AffixedEnumerable<T> WithSuffix<T>(this IEnumerable<T> enumerable, T suffix)
    {
        return new AffixedEnumerable<T>(enumerable).WithSuffix(suffix);
    }
    /// <summary>
    /// Returns an <seealso cref="AffixedEnumerable{T}"/> from the main
    /// enumerable with the specified enumerable as the suffix.
    /// </summary>
    /// <typeparam name="T">The type of values contained in the enumerable.</typeparam>
    /// <param name="enumerable">The main enumerable that is being suffixed.</param>
    /// <param name="suffix">The enumerable to prepend before the main enumerable.</param>
    /// <returns>
    /// An <seealso cref="AffixedEnumerable{T}"/> that contains the main enumerable
    /// suffixed by the given enumerable.
    /// </returns>
    public static AffixedEnumerable<T> WithSuffix<T>(this IEnumerable<T> enumerable, IEnumerable<T> suffix)
    {
        return new AffixedEnumerable<T>(enumerable).WithSuffix(suffix);
    }
    /// <summary>
    /// Returns an <seealso cref="AffixedEnumerable{T}"/> from the main
    /// enumerable with the specified enumerable as the suffix.
    /// </summary>
    /// <typeparam name="T">The type of values contained in the enumerable.</typeparam>
    /// <param name="enumerable">The main enumerable that is being suffixed.</param>
    /// <param name="suffix">The enumerable to prepend before the main enumerable.</param>
    /// <returns>
    /// An <seealso cref="AffixedEnumerable{T}"/> that contains the main enumerable
    /// suffixed by the given enumerable.
    /// </returns>
    public static AffixedEnumerable<T> WithSuffix<T>(this IEnumerable<T> enumerable, params T[] suffix)
    {
        return new AffixedEnumerable<T>(enumerable).WithSuffix(suffix);
    }
}
