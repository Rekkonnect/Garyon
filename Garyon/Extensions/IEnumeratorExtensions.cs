using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Garyon.Extensions;

/// <summary>
/// Contains extension methods for the <see cref="IEnumerator{T}"/> interface.
/// </summary>
public static class IEnumeratorExtensions
{
    /// <inheritdoc cref="GetEnumerator{T}(IEnumerator{T})"/>
    public static IEnumerator GetEnumerator(this IEnumerator enumerator)
    {
        return enumerator;
    }
    /// <inheritdoc cref="GetEnumerator{T}(IEnumerator{T})"/>
    public static TEnumerator GetEnumerator<TEnumerator>(this TEnumerator enumerator)
        where TEnumerator : IEnumerator
    {
        return enumerator;
    }

    /// <summary>Returns the given enumerator instance.</summary>
    /// <typeparam name="T">The type of the enumerated values.</typeparam>
    /// <param name="enumerator">The enumerator instance to return.</param>
    /// <returns>The given enumerator instance.</returns>
    /// <remarks>
    /// This method is only present to enable direct enumeration
    /// of the enumeartor in a <see langword="foreach"/> statement.
    /// <br/>
    /// Avoid using it directly.
    /// </remarks>
    public static IEnumerator<T> GetEnumerator<T>(this IEnumerator<T> enumerator)
    {
        return enumerator;
    }
    /// <inheritdoc cref="GetEnumerator{T}(IEnumerator{T})"/>
    /// <typeparam name="TValue">The type of the enumerated values.</typeparam>
    /// <typeparam name="TEnumerator">The type of the enumerator.</typeparam>
    public static TEnumerator GetEnumerator<TEnumerator, TValue>(this TEnumerator enumerator)
        where TEnumerator : IEnumerator<TValue>
    {
        return enumerator;
    }

    public static IEnumerator<T> WithResetState<T>(this IEnumerator<T> enumerator)
    {
        enumerator.Reset();
        return enumerator;
    }

    public static T[] ToArray<T>(this IEnumerator<T> enumerator, bool resetEnumerator = false)
    {
        var builder = ImmutableArray.CreateBuilder<T>();
        LoadIntoList(enumerator, builder, resetEnumerator);
        return builder.ToArray();
    }
    public static List<T> ToList<T>(this IEnumerator<T> enumerator, bool resetEnumerator = false)
    {
        var list = new List<T>();
        LoadIntoList(enumerator, list, resetEnumerator);
        return list;
    }
    public static ImmutableArray<T> ToImmutableArray<T>(this IEnumerator<T> enumerator, bool resetEnumerator = false)
    {
        var builder = ImmutableArray.CreateBuilder<T>();
        LoadIntoList(enumerator, builder, resetEnumerator);
        return builder.ToImmutable();
    }

    private static void LoadIntoList<T>(this IEnumerator<T> enumerator, IList<T> list, bool resetEnumerator)
    {
        if (resetEnumerator)
            enumerator.Reset();

        while (true)
        {
            bool hasNext = enumerator.MoveNext();
            if (!hasNext)
                break;

            list.Add(enumerator.Current);
        }
    }
}
