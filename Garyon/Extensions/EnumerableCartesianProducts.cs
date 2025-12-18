using Garyon.Mathematics;
using System.Collections.Generic;
#if HAS_IMMUTABLE
using System.Collections.Immutable;
#endif

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for generating Cartesian products from collections.
/// </summary>
public static class EnumerableCartesianProducts
{
    public static IEnumerable<(T, T)> CartesianProduct<T>(this IEnumerable<T> source)
    {
        foreach (var a in source)
            foreach (var b in source)
                yield return (a, b);
    }

    public static IReadOnlyList<(T, T)> MaterializedCartesianProduct<T>(this IReadOnlyList<T> source)
    {
        int count = source.Count;
        var result = new (T, T)[count * count];
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                result[i * count + j] = (source[i], source[j]);
            }
        }
        return result;
    }

    public static IReadOnlyList<(T, T)> MaterializedCartesianProductExcludingSame<T>(this IReadOnlyList<T> source)
    {
        int count = source.Count;
        int links = count - 1;
        var result = new (T, T)[count * links];
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                if (i == j)
                    continue;

                int index = i * links + j;
                if (j > i)
                    index--;

                result[index] = (source[i], source[j]);
            }
        }
        return result;
    }

    public static IReadOnlyList<(T, T)> MaterializedHomogenousCartesianProduct<T>(this IReadOnlyList<T> source)
    {
        int sourceCount = source.Count;
        int resultCount = SequencesMath.Sum(sourceCount);
        var builder =
#if HAS_IMMUTABLE
            ImmutableArray.CreateBuilder<(T, T)>(resultCount);
#else
            new List<(T, T)>(resultCount);
#endif
            ;
        for (int i = 0; i < sourceCount; i++)
        {
            for (int j = i + 1; j < sourceCount; j++)
            {
                builder.Add((source[i], source[j]));
            }
        }
        return builder;
    }
}
