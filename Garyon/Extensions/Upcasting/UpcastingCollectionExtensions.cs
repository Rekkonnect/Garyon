using System.Collections.Generic;

namespace Garyon.Extensions.Upcasting;

/// <summary>
/// Provides extensions for upcasting known collection types like
/// <see cref="IList{T}"/>, <see cref="IReadOnlyList{T}"/>, etc.
/// </summary>
public static class UpcastingCollectionExtensions
{
    /// <summary>
    /// Upcasts the given <see cref="IList{T}"/> into a list of
    /// <typeparamref name="TUp"/> instances.
    /// </summary>
    /// <typeparam name="TBase">
    /// The base type of the list's elements.
    /// </typeparam>
    /// <typeparam name="TUp">
    /// The type to upcast all of the list's elements.
    /// </typeparam>
    /// <param name="source">
    /// The source list to upcast.
    /// </param>
    /// <returns>
    /// An instance that returns the source list's elements cast into
    /// <typeparamref name="TUp"/>.
    /// </returns>
    /// <remarks>
    /// No validation is performed on whether all the elements of the list are
    /// <typeparamref name="TUp"/>. This method is unsafe and should be used on
    /// lists where it is known. If an element is not actually
    /// <typeparamref name="TUp"/>, accessing it (for example by indexing or
    /// enumeration) will throw an exception at runtime.
    /// </remarks>
    public static IList<TUp> UpcastList<TBase, TUp>(this IList<TBase> source)
        where TUp : class, TBase
    {
        return new UpcastList<TBase, TUp>(source);
    }
    /// <summary>
    /// Upcasts the given <see cref="IReadOnlyList{T}"/> into a list of
    /// <typeparamref name="TUp"/> instances.
    /// </summary>
    /// <typeparam name="TBase">
    /// The base type of the list's elements.
    /// </typeparam>
    /// <typeparam name="TUp">
    /// The type to upcast all of the list's elements.
    /// </typeparam>
    /// <param name="source">
    /// The source list to upcast.
    /// </param>
    /// <returns>
    /// An instance that returns the source list's elements cast into
    /// <typeparamref name="TUp"/>.
    /// </returns>
    /// <remarks>
    /// No validation is performed on whether all the elements of the list are
    /// <typeparamref name="TUp"/>. This method is unsafe and should be used on
    /// lists where it is known. If an element is not actually
    /// <typeparamref name="TUp"/>, accessing it (for example by indexing or
    /// enumeration) will throw an exception at runtime.
    /// </remarks>
    public static IReadOnlyList<TUp> UpcastReadOnlyList<TBase, TUp>(
        this IReadOnlyList<TBase> source)
        where TUp : class, TBase
    {
        return new UpcastReadOnlyList<TBase, TUp>(source);
    }

    /// <summary>
    /// Upcasts the given <see cref="ICollection{T}"/> into a collection of
    /// <typeparamref name="TUp"/> instances.
    /// </summary>
    /// <remarks>
    /// No validation is performed on whether all the elements are
    /// <typeparamref name="TUp"/>. This method is unsafe and should be used on
    /// collections where it is known.
    /// </remarks>
    public static ICollection<TUp> UpcastCollection<TBase, TUp>(this ICollection<TBase> source)
        where TUp : class, TBase
    {
        return new UpcastCollection<TBase, TUp>(source);
    }

    /// <summary>
    /// Upcasts the given <see cref="IReadOnlyCollection{T}"/> into a read-only
    /// collection of <typeparamref name="TUp"/> instances.
    /// </summary>
    /// <remarks>
    /// No validation is performed on whether all the elements are
    /// <typeparamref name="TUp"/>. This method is unsafe and should be used on
    /// collections where it is known.
    /// </remarks>
    public static IReadOnlyCollection<TUp> UpcastReadOnlyCollection<TBase, TUp>(
        this IReadOnlyCollection<TBase> source)
        where TUp : class, TBase
    {
        return source is IReadOnlyList<TBase> list
            ? new UpcastReadOnlyList<TBase, TUp>(list)
            : new UpcastReadOnlyCollection<TBase, TUp>(source);
    }

    /// <summary>
    /// Upcasts the given <see cref="IDictionary{TKey, TValue}"/> into a
    /// dictionary whose values are <typeparamref name="TUp"/>.
    /// </summary>
    /// <remarks>
    /// No validation is performed on whether all values are
    /// <typeparamref name="TUp"/>. This method is unsafe and should be used on
    /// dictionaries where it is known.
    /// </remarks>
    public static IDictionary<TKey, TUp> UpcastDictionary<TKey, TBase, TUp>(
        this IDictionary<TKey, TBase> source)
        where TKey : notnull
        where TUp : class, TBase
    {
        return new UpcastDictionary<TKey, TBase, TUp>(source);
    }

    /// <summary>
    /// Upcasts the given <see cref="IReadOnlyDictionary{TKey, TValue}"/> into a
    /// read-only dictionary whose values are <typeparamref name="TUp"/>.
    /// </summary>
    /// <remarks>
    /// No validation is performed on whether all values are
    /// <typeparamref name="TUp"/>. This method is unsafe and should be used on
    /// dictionaries where it is known.
    /// </remarks>
    public static IReadOnlyDictionary<TKey, TUp> UpcastReadOnlyDictionary<TKey, TBase, TUp>(
        this IReadOnlyDictionary<TKey, TBase> source)
        where TKey : notnull
        where TUp : class, TBase
    {
        return new UpcastReadOnlyDictionary<TKey, TBase, TUp>(source);
    }

    /// <summary>
    /// Upcasts the given <see cref="ISet{T}"/> into a set of
    /// <typeparamref name="TUp"/> instances.
    /// </summary>
    /// <remarks>
    /// No validation is performed on whether all elements are
    /// <typeparamref name="TUp"/>. This method is unsafe and should be used on
    /// sets where it is known.
    /// </remarks>
    public static ISet<TUp> UpcastSet<TBase, TUp>(this ISet<TBase> source)
        where TUp : class, TBase
    {
        return new UpcastSet<TBase, TUp>(source);
    }

#if HAS_READONLY_SET
    /// <summary>
    /// Upcasts the given <see cref="IReadOnlySet{T}"/> into a read-only set of
    /// <typeparamref name="TUp"/> instances.
    /// </summary>
    /// <remarks>
    /// No validation is performed on whether all elements are
    /// <typeparamref name="TUp"/>. This method is unsafe and should be used on
    /// sets where it is known.
    /// </remarks>
    public static IReadOnlySet<TUp> UpcastReadOnlySet<TBase, TUp>(this IReadOnlySet<TBase> source)
        where TUp : class, TBase
    {
        return new UpcastReadOnlySet<TBase, TUp>(source);
    }
#endif
}
