using System;
using System.Collections;
using System.Collections.Generic;

namespace Garyon.DataStructures
{
    /// <summary>Represents a setted linear collection, which is a linear collection accompanied by a set. This data structure encapsulates an ordered linear collection data type that has a specific insertion and removal order (e.g. <seealso cref="Queue{T}"/>, <seealso cref="Stack{T}"/>) and a <seealso cref="HashSet{T}"/>. Through the usage of the set, the data structure ensures that no elements are contained more than once.</summary>
    /// <typeparam name="T">The type of the stored elements.</typeparam>
    public abstract class BaseSetLinearCollection<T> : IReadOnlyCollection<T>
    {
        /// <summary>The set that contains the elements that are contained in the linear collection.</summary>
        private readonly HashSet<T> set;

        /// <summary>The count of elements in the setted linear collection.</summary>
        public int Count => set.Count;
        /// <summary>Determines whether the setted linear collection is empty or not.</summary>
        public bool IsEmpty => Count == 0;

        /// <summary>Initializes a new empty instance of the <seealso cref="BaseSetLinearCollection{T}"/> class.</summary>
        protected BaseSetLinearCollection()
        {
            set = new();
        }
        /// <summary>Initializes a new empty instance of the <seealso cref="BaseSetLinearCollection{T}"/> class with a given initial capacity.</summary>
        /// <param name="capacity">The capacity of both the contained linear collection and the <seealso cref="HashSet{T}"/>.</param>
        protected BaseSetLinearCollection(int capacity)
        {
            set = new(capacity);
        }
        /// <summary>Initializes a new empty instance of the <seealso cref="BaseSetLinearCollection{T}"/> class with a given comparer for the <seealso cref="HashSet{T}"/>.</summary>
        /// <param name="comparer">The comparer that <seealso cref="HashSet{T}"/> will use for the elements.</param>
        protected BaseSetLinearCollection(IEqualityComparer<T> comparer)
        {
            set = new(comparer);
        }
        /// <summary>Initializes a new empty instance of the <seealso cref="BaseSetLinearCollection{T}"/> class with a given initial capacity and a given comparer for the <seealso cref="HashSet{T}"/>.</summary>
        /// <param name="capacity">The capacity of both the contained linear collection and the <seealso cref="HashSet{T}"/>.</param>
        /// <param name="comparer">The comparer that <seealso cref="HashSet{T}"/> will use for the elements.</param>
        protected BaseSetLinearCollection(int capacity, IEqualityComparer<T> comparer)
        {
            set = new(capacity, comparer);
        }

        /// <summary>Determines whether an item is contained in the setted linear collection.</summary>
        /// <param name="item">The item to find in the setted linear collection.</param>
        /// <returns><see langword="true"/> if the item is contained in the set, otherwise <see langword="false"/>.</returns>
        public bool Contains(T item) => set.Contains(item);

        /// <summary>Adds an item in the setted linear collection, if it is not already stored.</summary>
        /// <param name="item">The item to add.</param>
        /// <returns><see langword="true"/> if the item was not contained anywhere in the setted linear collection and was successfully added, otherwise <see langword="false"/>.</returns>
        public bool Add(T item)
        {
            if (!set.Add(item))
                return false;

            AddToLinearCollection(item);
            return true;
        }
        /// <summary>Removes the item from the linear collection, and also removes it from the set.</summary>
        /// <returns>The removed item.</returns>
        public T Remove()
        {
            var item = RemoveFromLinearCollection();
            set.Remove(item);
            return item;
        }
        /// <summary>Peeks the linear collection. This does not remove any items from the setted linear collection.</summary>
        /// <returns>The item that was peeked from the linear collection. It is the item that will be removed next.</returns>
        public abstract T Peek();

        /// <summary>Removes the next item to be removed from the linear collection.</summary>
        /// <remarks>It should only perform the necessary operations to the linear collection.</remarks>
        /// <returns>The item that was removed from the linear collection.</returns>
        protected abstract T RemoveFromLinearCollection();
        /// <summary>Adds an item to the linear collection.</summary>
        /// <param name="item">The item to add to the linear collection.</param>
        /// <remarks>It should only perform the necessary operations to the linear collection.</remarks>
        protected abstract void AddToLinearCollection(T item);

        /// <summary>Adds a collection of items to the setted linear collection. The order at which each item is added is determined by the enumeration order of the provided collection.</summary>
        /// <param name="items">The collection of items to add to the setted linear collection.</param>
        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                Add(item);
        }
        /// <summary>Removes a number of items from the setted linear collection, and returns them in the order they were removed.</summary>
        /// <param name="count">The number of items to remove from the setted linear collection. If the setted linear collection contains less items than the provided count, it will be capped to the current item count.</param>
        /// <returns>The collection of items that were removed from the setted linear collection.</returns>
        public IEnumerable<T> RemoveRange(int count)
        {
            count = Math.Min(count, Count);
            for (int i = 0; i < count; i++)
                yield return Remove();
        }

        /// <summary>Clears the setted linear collection, emptying the linear collection and the set individually.</summary>
        public void Clear()
        {
            set.Clear();
            ClearLinearCollection();
        }

        /// <summary>Clears the linear collection. It should call the respective Clear method for the linear collection instance that backs the data structure.</summary>
        protected abstract void ClearLinearCollection();

        /// <summary>Copies the elements of the linear collection in the provided <typeparamref name="T"/>[] starting from the given array index.</summary>
        /// <param name="array">The array in which to copy the elements of the linear collection in the order they would be removed.</param>
        /// <param name="arrayIndex">The index of the first element that will be copied from the linear collection into.</param>
        public abstract void CopyTo(T[] array, int arrayIndex);

        public abstract IEnumerator<T> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
