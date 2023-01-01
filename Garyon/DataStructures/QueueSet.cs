using System.Collections.Generic;

namespace Garyon.DataStructures;

/// <summary>Represents a set whose elements are stored in a queue. This data structure encapsulates a <seealso cref="Queue{T}"/> and a <seealso cref="HashSet{T}"/>. Through the usage of the set, the data structure ensures that no elements are contained more than once.</summary>
/// <typeparam name="T">The type of the stored elements.</typeparam>
public sealed class QueueSet<T> : BaseSetLinearCollection<T>
{
    private readonly Queue<T> queue;

    /// <summary>Initializes a new empty instance of the <seealso cref="QueueSet{T}"/> class.</summary>
    public QueueSet()
        : base()
    {
        queue = new();
    }
#if HAS_HASHSET_CAPACITY_CTOR
    /// <summary>Initializes a new empty instance of the <seealso cref="QueueSet{T}"/> class with a given initial capacity.</summary>
    /// <param name="capacity">The capacity of both the contained <seealso cref="Queue{T}"/> and the <seealso cref="HashSet{T}"/>.</param>
    public QueueSet(int capacity)
        : base(capacity)
    {
        queue = new(capacity);
    }
#endif
    /// <summary>Initializes a new empty instance of the <seealso cref="QueueSet{T}"/> class with a given comparer for the <seealso cref="HashSet{T}"/>.</summary>
    /// <param name="comparer">The comparer that <seealso cref="HashSet{T}"/> will use for the elements.</param>
    public QueueSet(IEqualityComparer<T> comparer)
        : base(comparer)
    {
        queue = new();
    }
#if HAS_HASHSET_CAPACITY_CTOR
    /// <summary>Initializes a new empty instance of the <seealso cref="QueueSet{T}"/> class with a given initial capacity and a given comparer for the <seealso cref="HashSet{T}"/>.</summary>
    /// <param name="capacity">The capacity of both the contained <seealso cref="Queue{T}"/> and the <seealso cref="HashSet{T}"/>.</param>
    /// <param name="comparer">The comparer that <seealso cref="HashSet{T}"/> will use for the elements.</param>
    public QueueSet(int capacity, IEqualityComparer<T> comparer)
        : base(capacity, comparer)
    {
        queue = new(capacity);
    }
#endif

    /// <summary>Enqueues an item in the queue set, if it is not already stored.</summary>
    /// <param name="item">The item to enqueue.</param>
    /// <returns><see langword="true"/> if the item was not contained anywhere in the queue set and was successfully enqueued, otherwise <see langword="false"/>.</returns>
    public bool Enqueue(T item)
    {
        return Add(item);
    }
    /// <summary>Dequeues the item at the start of the queue, and also removes it from the set.</summary>
    /// <returns>The dequeued item.</returns>
    public T Dequeue()
    {
        return Remove();
    }
    /// <inheritdoc/>
    public override T Peek()
    {
        return queue.Peek();
    }

    /// <inheritdoc/>
    protected override void AddToLinearCollection(T item) => queue.Enqueue(item);
    /// <inheritdoc/>
    protected override T RemoveFromLinearCollection() => queue.Dequeue();
    /// <inheritdoc/>
    protected override void ClearLinearCollection() => queue.Clear();

    /// <summary>Enqueues a collection of items to the queue set. The order at which each item is enqueued is determined by the enumeration order of the provided collection.</summary>
    /// <param name="items">The collection of items to enqueue to the queue set.</param>
    public void EnqueueRange(IEnumerable<T> items)
    {
        AddRange(items);
    }
    /// <summary>Dequeues a number of items from the queue set, and returns them in the order they were dequeued.</summary>
    /// <param name="count">The number of items to dequeue from the queue set. If the queue set contains less items than the provided count, it will be capped to the current item count.</param>
    /// <returns>The collection of items that were dequeued from the queue set.</returns>
    public IEnumerable<T> DequeueRange(int count)
    {
        return RemoveRange(count);
    }

    /// <inheritdoc/>
    public override void CopyTo(T[] array, int arrayIndex) => queue.CopyTo(array, arrayIndex);

    /// <summary>Gets the enumerator for this queue set, which is the enumerator for the queue.</summary>
    /// <returns>The queue's enumerator.</returns>
    public override IEnumerator<T> GetEnumerator() => queue.GetEnumerator();
}
