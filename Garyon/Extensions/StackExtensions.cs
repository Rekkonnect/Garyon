using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions
{
    /// <summary>Provides extensions for the <seealso cref="Stack{T}"/> class.</summary>
    public static class StackExtensions
    {
        /// <summary>Pushes a range of elements to the stack.</summary>
        /// <typeparam name="T">The type of the elements contained in the stack.</typeparam>
        /// <param name="stack">The stack.</param>
        /// <param name="range">The elements to push.</param>
        public static void PushRange<T>(this Stack<T> stack, IEnumerable<T> range)
        {
            foreach (var e in range)
                stack.Push(e);
        }
        /// <summary>Pops a range of elements from the stack.</summary>
        /// <typeparam name="T">The type of the elements contained in the stack.</typeparam>
        /// <param name="stack">The stack.</param>
        /// <param name="count">The number of elements to pop.</param>
        /// <returns>The popped elements in the order they were popped.</returns>
        public static IEnumerable<T> PopRange<T>(this Stack<T> stack, int count)
        {
            for (int i = 0; i < count; i++)
                yield return stack.Pop();
        }
        /// <summary>Pops all elements from the stack.</summary>
        /// <typeparam name="T">The type of the elements contained in the stack.</typeparam>
        /// <param name="stack">The stack.</param>
        /// <returns>The popped elements in the order they were popped.</returns>
        public static IEnumerable<T> PopAll<T>(this Stack<T> stack) => stack.PopRange(stack.Count);

        /// <summary>Inserts an element in the stack at the specified index.</summary>
        /// <typeparam name="T">The type of the values stored in the stack.</typeparam>
        /// <param name="stack">The stack.</param>
        /// <param name="item">The item to insert into the stack.</param>
        /// <param name="index">The index at which the item will be inserted.</param>
        /// <remarks>This is an expensive operation, due to the lack of the necessary public APIs for speeding up the insertion.</remarks>
        public static void Insert<T>(this Stack<T> stack, T item, int index)
        {
            if (index >= stack.Count)
            {
                stack.Push(item);
                return;
            }

            var popped = stack.PopRange(stack.Count - index - 1).ToArray();
            stack.Push(item);
            stack.PushRange(popped);
        }
    }
}
