#if HAS_SPAN

using System;

namespace Garyon.Objects;

/// <summary>
/// Provides mechanisms for convenient yielding of a
/// factory's values into a <seealso cref="Span{T}"/>,
/// with multiple yield operations applying after the previously
/// iterated indices.
/// </summary>
/// <typeparam name="T">The type of the yielded values.</typeparam>
/// <remarks>
/// Operations for this object are supported through extensions
/// defined in <seealso cref="Yielding"/> to support fluent syntax.
/// </remarks>
public ref struct SpanYielder<T>
{
    /// <summary>
    /// The <seealso cref="Span{T}"/> that the yielded values will
    /// be inserted into.
    /// </summary>
    public Span<T> Span { get; }
    /// <summary>
    /// The factory that will be used to yield values. You may
    /// change this during the lifetime of this instance.
    /// </summary>
    public Func<T> Factory { get; set; }

    /// <summary>
    /// Gets the index in the <seealso cref="Span{T}"/> that will
    /// be yielded.
    /// </summary>
    public int NextSpanIndex { get; private set; }

    /// <summary>
    /// Creates a new <seealso cref="SpanYielder{T}"/> from the
    /// affected <seealso cref="Span{T}"/> and the factory method.
    /// </summary>
    /// <param name="span">
    /// The <seealso cref="Span{T}"/> that the yielded values will
    /// be inserted into.
    /// </param>
    /// <param name="factory">
    /// The factory that will be used to yield values.
    /// </param>
    public SpanYielder(Span<T> span, Func<T> factory)
        : this(span, factory, 0) { }

    /// <summary>
    /// Creates a new <seealso cref="SpanYielder{T}"/> from the
    /// affected <seealso cref="Span{T}"/> and the factory method
    /// with a speicifed starting index in the span.
    /// </summary>
    /// <param name="span">
    /// The <seealso cref="Span{T}"/> that the yielded values will
    /// be inserted into.
    /// </param>
    /// <param name="factory">
    /// The factory that will be used to yield values.
    /// </param>
    /// <param name="startingSpanIndex">
    /// The first index in the span that the values will be yielded on.
    /// </param>
    public SpanYielder(Span<T> span, Func<T> factory, int startingSpanIndex)
    {
        Span = span;
        Factory = factory;
        NextSpanIndex = startingSpanIndex;
    }

    internal void YieldInternal(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var yielded = Factory();
            Span[NextSpanIndex] = yielded;
            NextSpanIndex++;
        }
    }
    internal void FillRestInternal()
    {
        for (int i = NextSpanIndex; i < Span.Length; i++)
        {
            var yielded = Factory();
            Span[i] = yielded;
        }
        NextSpanIndex = Span.Length;
    }

    /// <summary>
    /// Resets <see cref="NextSpanIndex"/> to 0.
    /// </summary>
    public void ResetNextIndex()
    {
        NextSpanIndex = 0;
    }
    /// <summary>
    /// Resets <see cref="NextSpanIndex"/> to the specified value.
    /// </summary>
    /// <param name="index">The index to reset to.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the index exceeds the span's length.
    /// </exception>
    public void ResetNextIndex(int index)
    {
        if (index >= Span.Length)
            throw new ArgumentOutOfRangeException(nameof(index), "The index is out of the span's bounds.");

        NextSpanIndex = index;
    }
}

#endif
