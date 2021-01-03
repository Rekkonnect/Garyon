using System.Collections.Generic;

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
        /// <summary>Destacks a range of elements from the stack.</summary>
        /// <typeparam name="T">The type of the elements contained in the stack.</typeparam>
        /// <param name="stack">The stack.</param>
        /// <param name="count">The number of elements to pop.</param>
        /// <returns>The popped elements in the order they were popped.</returns>
        public static IEnumerable<T> PopRange<T>(this Stack<T> stack, int count)
        {
            for (int i = 0; i < count; i++)
                yield return stack.Pop();
        }
    }
}
