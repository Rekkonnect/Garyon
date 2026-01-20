using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Garyon.Objects;

/// <summary>
/// Encapsulates a memoization dictionary for caching outputs of an async
/// function.
/// </summary>
/// <param name="func">
/// The function that computes the outputs.
/// </param>
/// <param name="concurrencyLevel">
/// The concurrency level to use for the cache dictionary, which should match
/// the expected average concurrent readers of the cache. This matches the
/// parameter in the constructor of
/// <see cref="ConcurrentDictionary{TKey, TValue}"/>.
/// </param>
/// <param name="capacity">
/// The initial capacity of the dictionary.
/// </param>
/// <remarks>
/// This uses a <see cref="ConcurrentDictionary{TKey, TValue}"/> under the hood,
/// which is initialized with a given concurrency level and capacity in its
/// constructor.
/// </remarks>
public sealed class AsyncMemoizationDictionary<TInput, TOutput>(
    Func<TInput, ValueTask<TOutput>> func,
    int concurrencyLevel,
    int capacity = 64)
    where TInput : notnull
{
    private readonly ConcurrentDictionary<TInput, TOutput> _outputs = new(concurrencyLevel, capacity);
    private readonly Func<TInput, ValueTask<TOutput>> _func = func;

    /// <summary>
    /// Gets the value of the given input. If the value has not been computed
    /// before, it is computed and stored into the dictionary for future
    /// retrievals. Otherwise, it's fetched directly from the dictionary.
    /// </summary>
    /// <param name="input">
    /// The input to the function.
    /// </param>
    /// <returns>
    /// The output of the function that was initialized, either cached or
    /// computed.
    /// </returns>
    public async ValueTask<TOutput> Get(TInput input)
    {
        bool found = _outputs.TryGetValue(input, out var value);
        if (!found)
        {
            value = await _func(input);
            _outputs[input] = value;
        }

        // Whether null or not, we have always fetched the value
        // either from the dictionary or by computing it
        return value!;
    }

    /// <summary>
    /// Clears the cache.
    /// </summary>
    public void Clear()
    {
        _outputs.Clear();
    }
}
