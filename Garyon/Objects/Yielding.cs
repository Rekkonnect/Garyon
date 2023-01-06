using System;
using System.Collections.Generic;
#if HAS_IMMUTABLE
using System.Collections.Immutable;
#endif

namespace Garyon.Objects;

/// <summary>
/// Provides helper methods for handling yielded
/// values from factory methods. 
/// </summary>
public static class Yielding
{
    /// <summary>
    /// Creates a new <seealso cref="Yielder{T}"/> from a
    /// factory method.
    /// </summary>
    /// <typeparam name="T">The type of the yielded values.</typeparam>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    /// <returns>
    /// The new <seealso cref="Yielder{T}"/> instance with the
    /// provided underlying factory method.
    /// </returns>
    /// <remarks>
    /// <seealso cref="Yielder{T}.Factory"/> is mutable, and the
    /// underlying factory method may be changed from another
    /// external source.
    /// </remarks>
    public static Yielder<T> For<T>(Func<T> factory)
    {
        return new(factory);
    }

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
    /// <returns>
    /// A <seealso cref="IEnumerable{T}"/> with the yielded
    /// values from the provided factory.
    /// </returns>
    /// <remarks/>
    /// <inheritdoc cref="Yielder{T}.Yield(int)"/>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    public static IEnumerable<T> Yield<T>(int count, Func<T> factory)
    {
        return For(factory).Yield(count);
    }

#if HAS_IMMUTABLE
    /// <inheritdoc cref="Yielder{T}.YieldImmutableArray(int)"/>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    public static ImmutableArray<T> YieldImmutableArray<T>(int count, Func<T> factory)
    {
        return For(factory).YieldImmutableArray(count);
    }
#endif
    /// <inheritdoc cref="Yielder{T}.YieldArray(int)"/>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    public static T[] YieldArray<T>(int count, Func<T> factory)
    {
        return For(factory).YieldArray(count);
    }
    /// <inheritdoc cref="Yielder{T}.YieldList(int)"/>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    public static List<T> YieldList<T>(int count, Func<T> factory)
    {
        return For(factory).YieldList(count);
    }
    /// <inheritdoc cref="Yielder{T}.YieldSet(int)"/>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    public static HashSet<T> YieldSet<T>(int count, Func<T> factory)
    {
        return For(factory).YieldSet(count);
    }
    /// <inheritdoc cref="Yielder{T}.YieldSortedSet(int)"/>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    public static SortedSet<T> YieldSortedSet<T>(int count, Func<T> factory)
    {
        return For(factory).YieldSortedSet(count);
    }

    /// <inheritdoc cref="Yielder{T}.YieldInto(int, List{T})"/>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    public static List<T> YieldInto<T>(int count, Func<T> factory, List<T> list)
    {
        return For(factory).YieldInto(count, list);
    }
    /// <inheritdoc cref="Yielder{T}.YieldInto(int, HashSet{T})"/>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    public static HashSet<T> YieldInto<T>(int count, Func<T> factory, HashSet<T> set)
    {
        return For(factory).YieldInto(count, set);
    }
    /// <inheritdoc cref="Yielder{T}.YieldInto(int, SortedSet{T})"/>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    public static SortedSet<T> YieldInto<T>(int count, Func<T> factory, SortedSet<T> set)
    {
        return For(factory).YieldInto(count, set);
    }

    /// <inheritdoc cref="Yielder{T}.YieldInto(int, List{T})"/>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    public static List<T> YieldOnto<T>(this List<T> list, int count, Func<T> factory)
    {
        return YieldInto(count, factory, list);
    }
    /// <inheritdoc cref="Yielder{T}.YieldInto(int, HashSet{T})"/>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    public static HashSet<T> YieldOnto<T>(this HashSet<T> set, int count, Func<T> factory)
    {
        return YieldInto(count, factory, set);
    }
    /// <inheritdoc cref="Yielder{T}.YieldInto(int, SortedSet{T})"/>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    public static SortedSet<T> YieldOnto<T>(this SortedSet<T> set, int count, Func<T> factory)
    {
        return YieldInto(count, factory, set);
    }

    /// <inheritdoc cref="Yielder{T}.Fill(T[])"/>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    public static T[] Fill<T>(this T[] span, Func<T> factory)
    {
        return For(factory).Fill(span);
    }
#if HAS_SPAN
    /// <inheritdoc cref="Yielder{T}.Fill(Span{T})"/>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    public static Span<T> Fill<T>(this Span<T> span, Func<T> factory)
    {
        return For(factory).Fill(span);
    }
    /// <inheritdoc cref="Yielder{T}.Fill(Memory{T})"/>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    public static Memory<T> Fill<T>(this Memory<T> span, Func<T> factory)
    {
        return For(factory).Fill(span);
    }
#endif
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
}
