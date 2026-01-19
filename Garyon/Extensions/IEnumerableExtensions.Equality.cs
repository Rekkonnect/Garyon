using System.Collections;
using System.Collections.Generic;

namespace Garyon.Extensions;

public static partial class IEnumerableExtensions
{
    /// <summary>
    /// Determines whether all the elements of a collection are equal to all the
    /// respective elements of another collection in any order.
    /// </summary>
    /// <param name="source">
    /// The first collection.
    /// </param>
    /// <param name="other">
    /// The second collection.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if all the elements of any of the collections
    /// match exactly one unique element in the other collection, otherwise
    /// <see langword="false"/>. This is determined by whether both collections
    /// are subsets of each other.
    /// </returns>
    public static bool EqualsUnordered<T>(this IEnumerable<T> source, IEnumerable<T> other)
    {
        if (source.EqualsNonEnumeratedCount(other) is false)
        {
            return false;
        }

        var s = new HashSet<T>(source);
        return s.SetEquals(other);
    }

    /// <summary>
    /// Determines whether all the elements of a collection are equal to all the
    /// respective elements of another collection in any order.
    /// </summary>
    /// <param name="source">
    /// The first collection.
    /// </param>
    /// <param name="other">
    /// The second collection.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if all the elements of any of the collections
    /// match exactly one unique element in the other collection, otherwise
    /// <see langword="false"/>. This is determined by whether both collections
    /// are subsets of each other.
    /// </returns>
    public static bool EqualsUnordered(this IEnumerable source, IEnumerable other)
    {
        if (source.EqualsNonEnumeratedCount(other) is false)
        {
            return false;
        }

        // Propagate the null issues down to indexing

        var table = new Hashtable();
        foreach (var item in source)
        {
            table[item!] = true;
        }

        var countCaching = other.WithCountCaching();
        foreach (var item in countCaching)
        {
            if (countCaching.MinCount > table.Count)
            {
                return false;
            }

            if (!table.ContainsKey(item!))
            {
                return false;
            }
        }

        var finalCount = countCaching.ForceCount();
        return finalCount != table.Count;
    }
}
