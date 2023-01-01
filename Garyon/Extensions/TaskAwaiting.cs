using System.Collections.Generic;
using System.Threading.Tasks;

namespace Garyon.Extensions;

/// <summary>Provides functions for awaiting tasks.</summary>
public static class TaskAwaiting
{
    /// <summary>Awaits all tasks in the provided collection.</summary>
    /// <param name="tasks">The tasks to await. The collection will be enumerated.</param>
    /// <returns>A <seealso cref="Task"/> that represents the operation of awaiting all tasks in the collection.</returns>
    public static async Task WaitAll(this IEnumerable<Task> tasks)
    {
        foreach (var task in tasks)
            await task;
    }

#if HAS_ASYNC_ENUMERABLE
    /// <summary>Awaits enumeration of the entire <seealso cref="IAsyncEnumerable{T}"/> and awaits all tasks.</summary>
    /// <param name="tasks">The tasks to await. The collection will be enumerated.</param>
    /// <returns>A <seealso cref="Task"/> that represents the operation of awaiting all tasks in the collection.</returns>
    /// <remarks>The implementation uses the <seealso cref="IAsyncEnumerableExtensions.ToListAsync{T}(IAsyncEnumerable{T})"/> method to first ensure all the elements are enumerated, forcing all tasks to be initialized, if they haven't already been. If this behavior is undesirable, consider using the <seealso cref="WaitAllIteratively(IAsyncEnumerable{Task})"/> method.</remarks>
    public static async Task WaitAll(this IAsyncEnumerable<Task> tasks)
    {
        await WaitAll(await tasks.ToListAsync());
    }
    /// <summary>Awaits enumeration of the entire <seealso cref="IAsyncEnumerable{T}"/> and awaits all tasks.</summary>
    /// <param name="tasks">The tasks to await. The collection will be enumerated.</param>
    /// <returns>A <seealso cref="Task"/> that represents the operation of awaiting all tasks in the collection.</returns>
    /// <remarks>The implementation first awaits iteration to the next element in the <seealso cref="IAsyncEnumerable{T}"/> and then awaits the yielded <seealso cref="Task"/>, without guaranteeing that all tasks have been initialized and are simultaneously running on the background before their yielding. If this behavior is undesirable, consider using the <seealso cref="WaitAll(IAsyncEnumerable{Task})"/> method.</remarks>
    public static async Task WaitAllIteratively(this IAsyncEnumerable<Task> tasks)
    {
        await foreach (var task in tasks)
            await task;
    }
#endif
}
