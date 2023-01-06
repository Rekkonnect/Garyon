using Garyon.Internal;
using System;
using System.Collections.Generic;
#if HAS_IMMUTABLE
using System.Collections.Immutable;
#endif

namespace Garyon.Objects;

/// <summary>
/// Provides mechanisms for convenient yielding of a
/// factory's values.
/// </summary>
/// <typeparam name="T">The type of the yielded values.</typeparam>
/// <param name="Factory">
/// The factory that will be used to yield values. You may
/// change this during the lifetime of this instance.
/// </param>
public record struct Yielder<T>(Func<T> Factory)
{
    /// <summary>
    /// Yields values out of the factory without storing
    /// them anywhere.
    /// </summary>
    /// <param name="count">
    /// The number of values to yield from the factory.
    /// </param>
    /// <returns>
    /// A <seealso cref="IEnumerable{T}"/> with the yielded
    /// values from the current <seealso cref="Factory"/>.
    /// </returns>
    /// <remarks>
    /// As <seealso cref="Factory"/> is mutable, the yielded
    /// results from the resulting <seealso cref="IEnumerable{T}"/>
    /// could use multiple factories, if adjusted from another
    /// external source.
    /// </remarks>
    public IEnumerable<T> Yield(int count)
    {
        for (int i = 0; i < count; i++)
            yield return Factory();
    }

#if HAS_IMMUTABLE
    /// <summary>
    /// Yields a number of values into a new instance of
    /// <seealso cref="ImmutableArray{T}"/>.
    /// </summary>
    /// <param name="count">The number of values to yield.</param>
    /// <returns>
    /// The created <seealso cref="ImmutableArray{T}"/> instance
    /// containing the yielded values in the order they were yielded.
    /// </returns>
    public ImmutableArray<T> YieldImmutableArray(int count)
    {
        var result = ImmutableArray.CreateBuilder<T>(count);
        for (int i = 0; i < count; i++)
            result.Add(Factory());
        return result.ToImmutable();
    }
#endif
    /// <summary>
    /// Yields a number of values into a new array.
    /// </summary>
    /// <param name="count">The number of values to yield.</param>
    /// <returns>
    /// The created array containing the yielded values
    /// in the order they were yielded.
    /// </returns>
    public T[] YieldArray(int count)
    {
        var result = new T[count];
        for (int i = 0; i < count; i++)
            result[i] = Factory();
        return result;
    }
    /// <summary>
    /// Yields a number of values into a new instance of
    /// <seealso cref="List{T}"/>.
    /// </summary>
    /// <param name="count">The number of values to yield.</param>
    /// <returns>
    /// The created <seealso cref="List{T}"/> instance containing
    /// the yielded values in the order they were yielded.
    /// </returns>
    public List<T> YieldList(int count)
    {
        var list = new List<T>(count);
        return YieldInto(count, list);
    }
    /// <summary>
    /// Yields a number of values into a new instance of
    /// <seealso cref="HashSet{T}"/>.
    /// </summary>
    /// <param name="count">The number of values to yield.</param>
    /// <returns>
    /// The created <seealso cref="HashSet{T}"/> instance containing
    /// the yielded values.
    /// </returns>
    /// <remarks>
    /// Duplicate yielded values are ignored. If the provided
    /// factory method can yield duplicate values, the count
    /// may be less than the provided.
    /// </remarks>
    public HashSet<T> YieldSet(int count)
    {
        var set = HashSet.New<T>(count);
        return YieldInto(count, set);
    }
    /// <summary>
    /// Yields a number of values into a new instance of
    /// <seealso cref="SortedSet{T}"/>.
    /// </summary>
    /// <param name="count">The number of values to yield.</param>
    /// <returns>
    /// The created <seealso cref="SortedSet{T}"/> instance containing
    /// the yielded values.
    /// </returns>
    /// <remarks>
    /// This method first creates a new <seealso cref="List{T}"/> using
    /// <seealso cref="YieldList(int)"/>, and then constructs the
    /// <seealso cref="SortedSet{T}"/> out of the created list.
    /// This approach is considered to be more optimal.
    /// </remarks>
    public SortedSet<T> YieldSortedSet(int count)
    {
        return new(YieldList(count));
    }

    /// <summary>
    /// Yields a number of values into an existing instance
    /// of <seealso cref="List{T}"/>.
    /// </summary>
    /// <param name="count">The number of values to yield.</param>
    /// <param name="list">
    /// The <seealso cref="List{T}"/> instance to yield the values into.
    /// </param>
    /// <returns>
    /// The provided <seealso cref="List{T}"/> instance.
    /// </returns>
    public List<T> YieldInto(int count, List<T> list)
    {
        for (int i = 0; i < count; i++)
            list.Add(Factory());
        return list;
    }
    /// <summary>
    /// Yields a number of values into an existing instance
    /// of <seealso cref="HashSet{T}"/>.
    /// </summary>
    /// <param name="count">The number of values to yield.</param>
    /// <param name="set">
    /// The <seealso cref="HashSet{T}"/> instance to yield the values into.
    /// </param>
    /// <returns>
    /// The provided <seealso cref="HashSet{T}"/> instance.
    /// </returns>
    public HashSet<T> YieldInto(int count, HashSet<T> set)
    {
        for (int i = 0; i < count; i++)
            set.Add(Factory());
        return set;
    }
    /// <summary>
    /// Yields a number of values into an existing instance
    /// of <seealso cref="SortedSet{T}"/>.
    /// </summary>
    /// <param name="count">The number of values to yield.</param>
    /// <param name="set">
    /// The <seealso cref="SortedSet{T}"/> instance to yield the values into.
    /// </param>
    /// <returns>
    /// The provided <seealso cref="SortedSet{T}"/> instance.
    /// </returns>
    /// <remarks>
    /// This method first creates a new <seealso cref="List{T}"/> using
    /// <seealso cref="YieldList(int)"/>, and then performs a union on
    /// <seealso cref="SortedSet{T}"/> with the created list.
    /// This approach is considered to be more optimal.
    /// </remarks>
    public SortedSet<T> YieldInto(int count, SortedSet<T> set)
    {
        var yielded = YieldList(count);
        set.UnionWith(yielded);
        return set;
    }
#if HAS_SPAN
    /// <summary>
    /// Yields a number of values into an existing instance
    /// of <seealso cref="Span{T}"/>.
    /// </summary>
    /// <param name="count">The number of values to yield.</param>
    /// <param name="span">
    /// The <seealso cref="Span{T}"/> instance to yield the values into.
    /// </param>
    /// <returns>
    /// The provided <seealso cref="Span{T}"/> instance.
    /// </returns>
    public Span<T> YieldInto(int count, Span<T> span)
    {
        if (span.Length < count)
            throw new ArgumentOutOfRangeException(nameof(count), "The yield count is larger than the provided span's length.");

        for (int i = 0; i < count; i++)
            span[i] = Factory();
        return span;
    }
    /// <summary>
    /// Yields a number of values into an existing instance
    /// of <seealso cref="Memory{T}"/>.
    /// </summary>
    /// <param name="count">The number of values to yield.</param>
    /// <param name="memory">
    /// The <seealso cref="Memory{T}"/> instance to yield the values into.
    /// </param>
    /// <returns>
    /// The provided <seealso cref="Memory{T}"/> instance.
    /// </returns>
    public Memory<T> YieldInto(int count, Memory<T> memory)
    {
        YieldInto(count, memory.Span);
        return memory;
    }
#endif

    /// <summary>
    /// Fills the contents of an existing array with
    /// yielded values from the currently provided factory.
    /// </summary>
    /// <param name="array">
    /// The array to yield the values into.
    /// </param>
    /// <returns>The provided array.</returns>
    public T[] Fill(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
            array[i] = Factory();
        return array;
    }
#if HAS_SPAN
    /// <summary>
    /// Fills the contents of an existing instance of
    /// <seealso cref="Span{T}"/> with yielded values
    /// from the currently provided factory.
    /// </summary>
    /// <param name="span">
    /// The <seealso cref="Span{T}"/> instance to yield the values into.
    /// </param>
    /// <returns>
    /// The provided <seealso cref="Span{T}"/> instance.
    /// </returns>
    public Span<T> Fill(Span<T> span)
    {
        return YieldInto(span.Length, span);
    }
    /// <summary>
    /// Fills the contents of an existing instance of
    /// <seealso cref="Memory{T}"/> with yielded values
    /// from the currently provided factory.
    /// </summary>
    /// <param name="memory">
    /// The <seealso cref="Memory{T}"/> instance to yield the values into.
    /// </param>
    /// <returns>
    /// The provided <seealso cref="Memory{T}"/> instance.
    /// </returns>
    public Memory<T> Fill(Memory<T> memory)
    {
        return YieldInto(memory.Length, memory);
    }
#endif
}
