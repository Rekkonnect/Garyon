#if HAS_SPAN

using System;

namespace Garyon.Objects;

/// <summary>
/// Provides helper methods for yielding values from a factory
/// into a <seealso cref="Span{T}"/>.
/// </summary>
public static class SpanYielding
{
    /// <inheritdoc cref="SpanYielder{T}.SpanYielder(Span{T}, Func{T})"/>
    /// <typeparam name="T">The type of the yielded values.</typeparam>
    /// <returns>
    /// The new <seealso cref="SpanYielder{T}"/> instance with the
    /// provided underlying factory method.
    /// </returns>
    /// <remarks>
    /// <seealso cref="SpanYielder{T}.Factory"/> is mutable, and the
    /// underlying factory method may be changed from another
    /// external source.
    /// </remarks>
    public static SpanYielder<T> For<T>(Span<T> span, Func<T> factory)
    {
        return new(span, factory);
    }
    /// <inheritdoc cref="SpanYielder{T}.SpanYielder(Span{T}, Func{T}, int)"/>
    /// <inheritdoc cref="For{T}(Span{T}, Func{T})"/>
    public static SpanYielder<T> For<T>(Span<T> span, Func<T> factory, int startingSpanIndex)
    {
        return new(span, factory, startingSpanIndex);
    }

    // You absolutely have to know how to handle ref structs,
    // or you expose yourself into big trouble
    /// <summary>
    /// Yields a number of values into an existing instance
    /// of <seealso cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the yielded values.</typeparam>
    /// <param name="span">
    /// The <seealso cref="Span{T}"/> instance to yield the values into.
    /// </param>
    /// <param name="count">The number of values to yield.</param>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    /// <returns>
    /// A constructed <seealso cref="SpanYielder{T}"/> instance for yielding
    /// further into the specified <seealso cref="Span{T}"/> with the given
    /// factory method.
    /// </returns>
    public static SpanYielder<T> YieldOntoSpan<T>(Span<T> span, int count, Func<T> factory)
    {
        var yielder = For(span, factory);
        yielder.Yield(count);
        return yielder;
    }
    /// <summary>
    /// Yields a number of values into an existing instance
    /// of <seealso cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the yielded values.</typeparam>
    /// <param name="span">
    /// The <seealso cref="Span{T}"/> instance to yield the values into.
    /// </param>
    /// <param name="count">The number of values to yield.</param>
    /// <param name="factory">
    /// The factory method whose values will be yielded.
    /// </param>
    /// <param name="startingIndex">
    /// The first index in the span that the values will be yielded on.
    /// </param>
    /// <returns>
    /// A constructed <seealso cref="SpanYielder{T}"/> instance for yielding
    /// further into the specified <seealso cref="Span{T}"/> with the given
    /// factory method.
    /// </returns>
    public static SpanYielder<T> YieldOntoSpan<T>(Span<T> span, int count, Func<T> factory, int startingIndex)
    {
        var yielder = For(span, factory, startingIndex);
        yielder.Yield(count);
        return yielder;
    }

    public static ref SpanYielder<T> Yield<T>(this ref SpanYielder<T> yielder, int count)
    {
        yielder.YieldInternal(count);
        return ref yielder;
    }
    public static ref SpanYielder<T> Yield<T>(this ref SpanYielder<T> yielder, int count, Func<T> factory)
    {
        yielder.Factory = factory;
        return ref yielder.Yield(count);
    }

    /// <summary>
    /// Fills the rest of the <seealso cref="Span{T}"/> from the
    /// given instance of <seealso cref="SpanYielder{T}"/> using the
    /// underlying factory method.
    /// </summary>
    /// <typeparam name="T">The type of the yielded values.</typeparam>
    /// <param name="yielder">
    /// The <seealso cref="SpanYielder{T}"/> that yields values into
    /// the <seealso cref="Span{T}"/> using its current factory method.
    /// </param>
    /// <returns>
    /// A reference to the provided <seealso cref="SpanYielder{T}"/>.
    /// </returns>
    public static ref SpanYielder<T> FillRest<T>(this ref SpanYielder<T> yielder)
    {
        yielder.FillRestInternal();
        return ref yielder;
    }
    /// <summary>
    /// Fills the rest of the <seealso cref="Span{T}"/> from the
    /// given instance of <seealso cref="SpanYielder{T}"/> using a
    /// provided factory method.
    /// </summary>
    /// <typeparam name="T">The type of the yielded values.</typeparam>
    /// <param name="yielder">
    /// The <seealso cref="SpanYielder{T}"/> that yields values into
    /// the <seealso cref="Span{T}"/> using the provided factory method.
    /// </param>
    /// <param name="factory">
    /// The factory method to use for yielding the remaining values.
    /// </param>
    /// <returns>
    /// A reference to the provided <seealso cref="SpanYielder{T}"/>.
    /// </returns>
    public static ref SpanYielder<T> FillRest<T>(this ref SpanYielder<T> yielder, Func<T> factory)
    {
        yielder.Factory = factory;
        return ref yielder.FillRest();
    }
}

#endif
