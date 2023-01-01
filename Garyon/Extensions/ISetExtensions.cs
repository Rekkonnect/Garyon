using System.Collections.Generic;

namespace Garyon.Extensions;

/// <summary>Contains extension functions for the <seealso cref="ISet{T}"/> interface.</summary>
public static class ISetExtensions
{
    /// <summary>Adds a range of elements to the provided set.</summary>
    /// <typeparam name="T">The type of the elements that are contained in the collections.</typeparam>
    /// <param name="set">The set.</param>
    /// <param name="range">The range of values to add to the set.</param>
    public static void AddRange<T>(this ISet<T> set, IEnumerable<T> range)
    {
        foreach (var element in range)
            set.Add(element);
    }
}
