using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions;

/// <summary>Provides extensions for the <seealso cref="Queue{T}"/> class.</summary>
public static class QueueExtensions
{
    /// <summary>Enqueues a range of elements to the queue.</summary>
    /// <typeparam name="T">The type of the elements contained in the queue.</typeparam>
    /// <param name="queue">The queue.</param>
    /// <param name="range">The elements to enqueue.</param>
    public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> range)
    {
        foreach (var e in range)
            queue.Enqueue(e);
    }
    /// <summary>Dequeues a range of elements from the queue.</summary>
    /// <typeparam name="T">The type of the elements contained in the queue.</typeparam>
    /// <param name="queue">The queue.</param>
    /// <param name="count">The number of elements to dequeue.</param>
    /// <returns>The dequeued elements in the order they were dequeued.</returns>
    public static IEnumerable<T> DequeueRange<T>(this Queue<T> queue, int count)
    {
        for (int i = 0; i < count; i++)
            yield return queue.Dequeue();
    }
    /// <summary>Dequeues all elements from the queue.</summary>
    /// <typeparam name="T">The type of the elements contained in the queue.</typeparam>
    /// <param name="queue">The queue.</param>
    /// <returns>The dequeued elements in the order they were dequeued.</returns>
    public static IEnumerable<T> DequeueAll<T>(this Queue<T> queue) => queue.DequeueRange(queue.Count);

    /// <summary>Inserts an element in the queue at the specified index.</summary>
    /// <typeparam name="T">The type of the values stored in the queue.</typeparam>
    /// <param name="queue">The queue.</param>
    /// <param name="item">The item to insert into the queue.</param>
    /// <param name="index">The index at which the item will be inserted.</param>
    /// <remarks>This is an expensive operation, due to the lack of the necessary public APIs for speeding up the insertion.</remarks>
    public static void Insert<T>(this Queue<T> queue, T item, int index)
    {
        if (index >= queue.Count)
        {
            queue.Enqueue(item);
            return;
        }

        var dequeued = queue.DequeueRange(queue.Count - index - 1).ToArray();
        queue.Enqueue(item);
        queue.EnqueueRange(dequeued);
    }
}
