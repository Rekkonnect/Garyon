using System.Collections;
using System.Collections.Generic;

namespace Garyon.Objects.Enumerators
{
    /// <summary>Provides the base mechanism for a parallel enumerator for two <seealso cref="IEnumerator{T}"/> objects.</summary>
    public abstract class BaseParallelEnumerator
    {
        /// <summary>Gets the current value of the <seealso cref="IEnumerator{T}"/> at the specified index.</summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="index">The zero-based index of the <seealso cref="IEnumerator{T}"/>.</param>
        /// <returns>The current value of the <seealso cref="IEnumerator{T}"/>, if the given <seealso cref="IEnumerator{T}"/> is alive, otherwise <see langword="default"/>.</returns>
        protected abstract T GetCurrentValue<T>(int index);

        /// <summary>Gets the current value of the <seealso cref="IEnumerator{T}"/>.</summary>
        /// <typeparam name="T">The type of the elements the <seealso cref="IEnumerator{T}"/> enumerates.</typeparam>
        /// <param name="enumerator">The enumerator.</param>
        /// <param name="alive">Determines whether the enumerator's current position is before the end of the collection, allowing it to retrieve the <see cref="IEnumerator{T}.Current"/> value.</param>
        /// <returns>The current value of the <seealso cref="IEnumerator{T}"/>, if <paramref name="alive"/> is <see langword="true"/>, otherwise <see langword="default"/>.</returns>
        protected static T GetCurrentValue<T>(IEnumerator<T> enumerator, bool alive)
        {
            return alive ? enumerator.Current : default;
        }
        /// <summary>Conditionally calls the <see cref="IEnumerator.MoveNext()"/> method on the provided enumerator, if <paramref name="alive"/> is <see langword="true"/>.</summary>
        /// <typeparam name="T">The type of the elements the <seealso cref="IEnumerator{T}"/> enumerates.</typeparam>
        /// <param name="enumerator">The enumerator.</param>
        /// <param name="alive">Determines whether the enumerator should move to the next element. The result of the <see cref="IEnumerator.MoveNext()"/> method is stored back to this variable.</param>
        protected static void ConditionallyMoveNext<T>(IEnumerator<T> enumerator, ref bool alive)
        {
            if (alive)
                alive = enumerator.MoveNext();
        }
    }
}
