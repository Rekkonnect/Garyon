using System.Collections.Generic;

namespace Garyon.DataStructures
{
    /// <summary>Represents a set whose elements are stored in a stack. This data structure encapsulates a <seealso cref="Stack{T}"/> and a <seealso cref="HashSet{T}"/>. Through the usage of the set, the data structure ensures that no elements are contained more than once.</summary>
    /// <typeparam name="T">The type of the stored elements.</typeparam>
    public sealed class StackSet<T> : BaseSetLinearCollection<T>
    {
        private readonly Stack<T> stack;

        /// <summary>Initializes a new empty instance of the <seealso cref="StackSet{T}"/> class.</summary>
        public StackSet()
            : base()
        {
            stack = new();
        }
        /// <summary>Initializes a new empty instance of the <seealso cref="StackSet{T}"/> class with a given initial capacity.</summary>
        /// <param name="capacity">The capacity of both the contained <seealso cref="Stack{T}"/> and the <seealso cref="HashSet{T}"/>.</param>
        public StackSet(int capacity)
            : base(capacity)
        {
            stack = new(capacity);
        }
        /// <summary>Initializes a new empty instance of the <seealso cref="StackSet{T}"/> class with a given comparer for the <seealso cref="HashSet{T}"/>.</summary>
        /// <param name="comparer">The comparer that <seealso cref="HashSet{T}"/> will use for the elements.</param>
        public StackSet(IEqualityComparer<T> comparer)
            : base(comparer)
        {
            stack = new();
        }
        /// <summary>Initializes a new empty instance of the <seealso cref="StackSet{T}"/> class with a given initial capacity and a given comparer for the <seealso cref="HashSet{T}"/>.</summary>
        /// <param name="capacity">The capacity of both the contained <seealso cref="Stack{T}"/> and the <seealso cref="HashSet{T}"/>.</param>
        /// <param name="comparer">The comparer that <seealso cref="HashSet{T}"/> will use for the elements.</param>
        public StackSet(int capacity, IEqualityComparer<T> comparer)
            : base(capacity, comparer)
        {
            stack = new(capacity);
        }

        /// <summary>Pushes an item in the stack set, if it is not already stored.</summary>
        /// <param name="item">The item to push.</param>
        /// <returns><see langword="true"/> if the item was not contained anywhere in the stack set and was successfully pushed, otherwise <see langword="false"/>.</returns>
        public bool Push(T item)
        {
            return Add(item);
        }
        /// <summary>Pops the item at the top of the stack, and also removes it from the set.</summary>
        /// <returns>The popped item.</returns>
        public T Pop()
        {
            return Remove();
        }
        /// <inheritdoc/>
        public override T Peek()
        {
            return stack.Peek();
        }

        /// <inheritdoc/>
        protected override void AddToLinearCollection(T item) => stack.Push(item);
        /// <inheritdoc/>
        protected override T RemoveFromLinearCollection() => stack.Pop();
        /// <inheritdoc/>
        protected override void ClearLinearCollection() => stack.Clear();

        /// <summary>Pushes a collection of items to the stack set. The order at which each item is pushed is determined by the enumeration order of the provided collection.</summary>
        /// <param name="items">The collection of items to push to the stack set.</param>
        public void PushRange(IEnumerable<T> items)
        {
            AddRange(items);
        }
        /// <summary>Pops a number of items from the stack set, and returns them in the order they were popped.</summary>
        /// <param name="count">The number of items to pop from the stack set. If the stack set contains less items than the provided count, it will be capped to the current item count.</param>
        /// <returns>The collection of items that were popped from the stack set.</returns>
        public IEnumerable<T> PopRange(int count)
        {
            return RemoveRange(count);
        }

        /// <inheritdoc/>
        public override void CopyTo(T[] array, int arrayIndex) => stack.CopyTo(array, arrayIndex);

        /// <summary>Gets the enumerator for this stack set, which is the enumerator for the stack.</summary>
        /// <returns>The stack's enumerator.</returns>
        public override IEnumerator<T> GetEnumerator() => stack.GetEnumerator();
    }
}
