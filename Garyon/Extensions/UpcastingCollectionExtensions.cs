using System.Collections.Generic;

namespace Garyon.Extensions;

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
    /// <typeparam name="TBase">The base type of the list's elements.</typeparam>
    /// <typeparam name="TUp">The type to upcast all of the list's elements.</typeparam>
    /// <param name="source">The source list to upcast.</param>
    /// <returns>
    /// An instance that returns the source list's elements cast into
    /// <typeparamref name="TUp"/>.
    /// </returns>
    /// <remarks>
    /// No validation is performed on whether all the elements of the list
    /// are <typeparamref name="TUp"/>. This method is unsafe and should be
    /// used on lists where it is known. Upcast elements that are not
    /// <typeparamref name="TUp"/> will be returned as <see langword="null"/>.
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
    /// <typeparam name="TBase">The base type of the list's elements.</typeparam>
    /// <typeparam name="TUp">The type to upcast all of the list's elements.</typeparam>
    /// <param name="source">The source list to upcast.</param>
    /// <returns>
    /// An instance that returns the source list's elements cast into
    /// <typeparamref name="TUp"/>.
    /// </returns>
    /// <remarks>
    /// No validation is performed on whether all the elements of the list
    /// are <typeparamref name="TUp"/>. This method is unsafe and should be
    /// used on lists where it is known. Upcast elements that are not
    /// <typeparamref name="TUp"/> will be returned as <see langword="null"/>.
    /// </remarks>
    public static IReadOnlyList<TUp> UpcastReadOnlyList<TBase, TUp>(
        this IReadOnlyList<TBase> source)
        where TUp : class, TBase
    {
        return new UpcastReadOnlyList<TBase, TUp>(source);
    }

    /*
     * TODO: Also support
     *  - ICollection<T>
     *  - IReadOnlyCollection<T>
     *  - IDictionary<TKey, TValue>
     *  - IReadOnlyDictionary<TKey, TValue>
     *  - ISet<T>
     *  - IReadOnlySetSet<T>
     */
}
