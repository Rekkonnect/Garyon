using System.Collections.Generic;

namespace Garyon.Extensions;

/// <summary>
/// Provides general collection extensions
/// </summary>
public static class GeneralCollectionExtensions
{
    /// <summary>Clones a list.</summary>
    /// <typeparam name="T">The type of the list elements.</typeparam>
    /// <param name="list">The list to clone.</param>
    public static List<T> Clone<T>(this List<T> list) => new(list);
    /// <summary>Clones a set.</summary>
    /// <typeparam name="T">The type of the set elements.</typeparam>
    /// <param name="set">The set to clone.</param>
    public static HashSet<T> Clone<T>(this HashSet<T> set) => new(set);
    /// <summary>Clones a set.</summary>
    /// <typeparam name="T">The type of the set elements.</typeparam>
    /// <param name="set">The set to clone.</param>
    public static SortedSet<T> Clone<T>(this SortedSet<T> set) => new(set);
}
