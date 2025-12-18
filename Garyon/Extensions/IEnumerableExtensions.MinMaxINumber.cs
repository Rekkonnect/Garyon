#if HAS_INUMBER

using Garyon.Functions;
using Garyon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Garyon.Extensions;

#nullable enable

public static partial class IEnumerableExtensions
{
    extension<T>(IEnumerable<T> source)
        where T : IMinMaxValue<T>, IComparisonOperators<T, T, bool>
    {
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        public MinMaxResult<T> MinMax()
        {
            VerifyNonEmptyCollection(source);

            T min = T.MaxValue;
            T max = T.MinValue;

            foreach (var v in source)
            {
                if (v < min)
                    min = v;
                if (v > max)
                    max = v;
            }

            return new(min, max);
        }

    }

    extension<T>(IEnumerable<T?> source)
        where T : notnull, IMinMaxValue<T>, IComparisonOperators<T, T, bool>
    {
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
        public MinMaxResult<T> MinMaxNullable()
        {
            var filtered = source.WhereNotNull();
            if (filtered.HasNone())
                return MinMaxResult<T>.Default;
            return MinMax(filtered);
        }
    }

    /// <summary>Gets the minimum and maximum values within the collection.</summary>
    /// <returns>The minimum and maximum values.</returns>
    public static MinMaxResult<TComparable> MinMax<TSource, TComparable>(
        IEnumerable<TSource> source,
        Func<TSource, TComparable> selector)
        where TComparable : IMinMaxValue<TComparable>, IComparisonOperators<TComparable, TComparable, bool>
    {
        return MinMax(source.Select(selector));
    }
}

#endif
