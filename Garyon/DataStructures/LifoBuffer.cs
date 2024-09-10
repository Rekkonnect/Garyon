#if HAS_SPAN

using System;

namespace Garyon.DataStructures;

/// <summary>
/// Provides a buffer storing elements in a LIFO (last-in-first-out) order.
/// The buffer cycles through its fixed capacity, automatically overwriting
/// the oldest elements upon appending an element.
/// </summary>
/// <typeparam name="T">The type of the stored elements.</typeparam>
/// <param name="capacity">
/// The fixed capacity of the buffer.
/// </param>
public sealed class LifoBuffer<T>(int capacity)
{
    private readonly T[] _buffer = new T[capacity];
    private int _appends = 0;

    /// <summary>
    /// The maximum number of elements that can be stored in the buffer.
    /// </summary>
    public int Capacity => _buffer.Length;
    /// <summary>
    /// The number of appends that have been made to the buffer.
    /// </summary>
    public int Appends => _appends;

    private int GetCurrentIndex()
    {
        return _appends % _buffer.Length;
    }

    /// <summary>
    /// Appends a value to the buffer. If the buffer is full, the oldest
    /// element will be automatically overwritten.
    /// </summary>
    /// <param name="value">The value to append to the buffer.</param>
    public void Append(T value)
    {
        _buffer[GetCurrentIndex()] = value;
        _appends++;
    }

    /// <summary>
    /// Gets the buffer as a <see cref="ReadOnlySpan{T}"/>. The order of the
    /// elements does not indicate the order of the appends.
    /// </summary>
    /// <returns>The buffer as a <see cref="ReadOnlySpan{T}"/>.</returns>
    public ReadOnlySpan<T> GetBuffer()
    {
        int length = Math.Min(_appends, _buffer.Length);
        return _buffer.AsSpan()[..length];
    }

    /// <summary>
    /// Gets the buffer split into two <see cref="ReadOnlySpan{T}"/> instances,
    /// showing the slice representing the newest and the oldest elements. The
    /// buffer is split to preserve its memory contiguity property.
    /// </summary>
    /// <param name="newest">
    /// The <see cref="ReadOnlySpan{T}"/> representing the newest elements that
    /// were appended to the buffer. The last element of this span is the most
    /// recent. The span may be empty, indicating that either the buffer is empty,
    /// or the last appended item overwrote the last item in the buffer, meaning
    /// that the next append will overwrite the first item in the buffer.
    /// </param>
    /// <param name="oldest">
    /// The <see cref="ReadOnlySpan{T}"/> representing the oldest elements that
    /// were appended to the buffer. The first element of this span is the oldest.
    /// The span may be empty, indicating that either the buffer is empty. If at
    /// least one item has been added to the buffer, this span will never be empty.
    /// </param>
    public void GetOrderedBuffer(out ReadOnlySpan<T> newest, out ReadOnlySpan<T> oldest)
    {
        var buffer = GetBuffer();
        var startIndex = GetCurrentIndex();
        newest = buffer[..startIndex];
        oldest = [];
        if (startIndex < buffer.Length)
        {
            oldest = buffer[startIndex..];
        }
    }
}

#endif
