using Garyon.Exceptions;
using Garyon.Functions;
using Garyon.Objects;
using Garyon.Objects.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Extensions
{
    /// <summary>Contains extension functions for the <seealso cref="IEnumerable{T}"/> interface.</summary>
    public static class IEnumerableExtensions
    {
        #region Multiple Enumerables
        /// <summary>Concatenates multiple <seealso cref="IEnumerable{T}"/>s and returns the concatenated result.</summary>
        /// <typeparam name="T">The type of the elements the <seealso cref="IEnumerable{T}"/>s hold.</typeparam>
        /// <param name="source">The source <seealso cref="IEnumerable{T}"/> to concatenate with the others.</param>
        /// <param name="others">The other <seealso cref="IEnumerable{T}"/>s to concatenate.</param>
        public static IEnumerable<T> ConcatMultiple<T>(this IEnumerable<T> source, params IEnumerable<T>[] others)
        {
            return ConcatMultiple(source, (IEnumerable<IEnumerable<T>>)others);
        }
        /// <summary>Concatenates multiple <seealso cref="IEnumerable{T}"/>s and returns the concatenated result.</summary>
        /// <typeparam name="T">The type of the elements the <seealso cref="IEnumerable{T}"/>s hold.</typeparam>
        /// <param name="source">The source <seealso cref="IEnumerable{T}"/> to concatenate with the others.</param>
        /// <param name="others">The other <seealso cref="IEnumerable{T}"/>s to concatenate.</param>
        public static IEnumerable<T> ConcatMultiple<T>(this IEnumerable<T> source, IEnumerable<IEnumerable<T>> others)
        {
            var concatenated = source;
            foreach (var e in others)
                concatenated = concatenated.Concat(e);
            return concatenated;
        }

        /// <summary>Flattens a collection of collections into a single collection. The resulting elements are contained in the order they are enumerated.</summary>
        /// <typeparam name="T">The type of elements contained in the collections.</typeparam>
        /// <param name="source">The collection of collections.</param>
        /// <returns>The flattened collection.</returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
        {
            foreach (var e in source)
                foreach (var v in e)
                    yield return v;
        }
        #endregion

        #region Null
        /// <summary>Gets the count of elements that are not <see langword="null"/>.</summary>
        /// <typeparam name="T">The type of the elements stored in the <seealso cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="source">The <seealso cref="IEnumerable{T}"/> whose elements to iterate.</param>
        /// <returns>The total number of elements that are not <see langword="null"/>.</returns>
        /// <remarks>For nullable structs, consider using <see cref="ValuedNullableElementsCount{T}(IEnumerable{T?})"/>.</remarks>
        public static int NotNullCount<T>(this IEnumerable<T> source)
        {
            return source.Count(Predicates.NotNull);
        }
        /// <summary>Gets the count of elements that are not <see langword="null"/>.</summary>
        /// <typeparam name="T">The type of the elements stored in the <seealso cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="source">The <seealso cref="IEnumerable{T}"/> whose elements to iterate.</param>
        /// <returns>The total number of elements that are not <see langword="null"/>.</returns>
        /// <remarks>For nullable structs, consider using <see cref="NonValuedNullableElementsCount{T}(IEnumerable{T?})"/>.</remarks>
        public static int NullCount<T>(this IEnumerable<T> source)
        {
            return source.Count(Predicates.Null);
        }
        /// <summary>Gets the count of the non-<see langword="null"/> elements in the provided collection of nullable struct elements.</summary>
        /// <typeparam name="T">The type of the struct.</typeparam>
        /// <param name="source">The collection of nullable struct elements.</param>
        /// <returns>The values of the non-<see langword="null"/> elements in the provided collection.</returns>
        public static int ValuedNullableElementsCount<T>(this IEnumerable<T?> source)
            where T : struct
        {
            return source.Count(Predicates.HasValue);
        }
        /// <summary>Gets the count of the <see langword="null"/> elements in the provided collection of nullable struct elements.</summary>
        /// <typeparam name="T">The type of the struct.</typeparam>
        /// <param name="source">The collection of nullable struct elements.</param>
        /// <returns>The values of the <see langword="null"/> elements in the provided collection.</returns>
        public static int NonValuedNullableElementsCount<T>(this IEnumerable<T?> source)
            where T : struct
        {
            return source.Count(Predicates.DoesNotHaveValue);
        }
        /// <summary>Gets the values of the non-<see langword="null"/> elements in the provided collection of nullable struct elements.</summary>
        /// <typeparam name="T">The type of the struct.</typeparam>
        /// <param name="source">The collection of nullable struct elements.</param>
        /// <returns>The values of the non-<see langword="null"/> elements in the provided collection.</returns>
        public static IEnumerable<T> GetValuedElements<T>(this IEnumerable<T?> source)
            where T : struct
        {
            return source.Where(Predicates.HasValue).Select(Selectors.ValueReturner);
        }
        #endregion

        #region MinMax
        // Behold the true copy-paste hell
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<byte> MinMax(this IEnumerable<byte> source)
        {
            VerifyNonEmptyCollection(source);

            byte min = byte.MaxValue;
            byte max = byte.MinValue;

            foreach (var v in source)
            {
                if (v < min)
                    min = v;
                if (v > max)
                    max = v;
            }

            return new(min, max);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<sbyte> MinMax(this IEnumerable<sbyte> source)
        {
            VerifyNonEmptyCollection(source);

            sbyte min = sbyte.MaxValue;
            sbyte max = sbyte.MinValue;

            foreach (var v in source)
            {
                if (v < min)
                    min = v;
                if (v > max)
                    max = v;
            }

            return new(min, max);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<short> MinMax(this IEnumerable<short> source)
        {
            VerifyNonEmptyCollection(source);

            short min = short.MaxValue;
            short max = short.MinValue;

            foreach (var v in source)
            {
                if (v < min)
                    min = v;
                if (v > max)
                    max = v;
            }

            return new(min, max);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<ushort> MinMax(this IEnumerable<ushort> source)
        {
            VerifyNonEmptyCollection(source);

            ushort min = ushort.MaxValue;
            ushort max = ushort.MinValue;

            foreach (var v in source)
            {
                if (v < min)
                    min = v;
                if (v > max)
                    max = v;
            }

            return new(min, max);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<int> MinMax(this IEnumerable<int> source)
        {
            VerifyNonEmptyCollection(source);

            int min = int.MaxValue;
            int max = int.MinValue;

            foreach (var v in source)
            {
                if (v < min)
                    min = v;
                if (v > max)
                    max = v;
            }

            return new(min, max);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<uint> MinMax(this IEnumerable<uint> source)
        {
            VerifyNonEmptyCollection(source);

            uint min = uint.MaxValue;
            uint max = uint.MinValue;

            foreach (var v in source)
            {
                if (v < min)
                    min = v;
                if (v > max)
                    max = v;
            }

            return new(min, max);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<long> MinMax(this IEnumerable<long> source)
        {
            VerifyNonEmptyCollection(source);

            long min = long.MaxValue;
            long max = long.MinValue;

            foreach (var v in source)
            {
                if (v < min)
                    min = v;
                if (v > max)
                    max = v;
            }

            return new(min, max);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<ulong> MinMax(this IEnumerable<ulong> source)
        {
            VerifyNonEmptyCollection(source);

            ulong min = ulong.MaxValue;
            ulong max = ulong.MinValue;

            foreach (var v in source)
            {
                if (v < min)
                    min = v;
                if (v > max)
                    max = v;
            }

            return new(min, max);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<float> MinMax(this IEnumerable<float> source)
        {
            VerifyNonEmptyCollection(source);

            float min = float.MaxValue;
            float max = float.MinValue;

            foreach (var v in source)
            {
                if (v < min)
                    min = v;
                if (v > max)
                    max = v;
            }

            return new(min, max);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<double> MinMax(this IEnumerable<double> source)
        {
            VerifyNonEmptyCollection(source);

            double min = double.MaxValue;
            double max = double.MinValue;

            foreach (var v in source)
            {
                if (v < min)
                    min = v;
                if (v > max)
                    max = v;
            }

            return new(min, max);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<decimal> MinMax(this IEnumerable<decimal> source)
        {
            VerifyNonEmptyCollection(source);

            decimal min = decimal.MaxValue;
            decimal max = decimal.MinValue;

            foreach (var v in source)
            {
                if (v < min)
                    min = v;
                if (v > max)
                    max = v;
            }

            return new(min, max);
        }

        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
        public static MinMaxResult<byte?> MinMax(this IEnumerable<byte?> source)
        {
            var filtered = source.Where(e => e.HasValue);
            if (!filtered.Any())
                return MinMaxResult<byte?>.Default;
            return MinMax(filtered);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
        public static MinMaxResult<sbyte?> MinMax(this IEnumerable<sbyte?> source)
        {
            var filtered = source.Where(e => e.HasValue);
            if (!filtered.Any())
                return MinMaxResult<sbyte?>.Default;
            return MinMax(filtered);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
        public static MinMaxResult<short?> MinMax(this IEnumerable<short?> source)
        {
            var filtered = source.Where(e => e.HasValue);
            if (!filtered.Any())
                return MinMaxResult<short?>.Default;
            return MinMax(filtered);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
        public static MinMaxResult<ushort?> MinMax(this IEnumerable<ushort?> source)
        {
            var filtered = source.Where(e => e.HasValue);
            if (!filtered.Any())
                return MinMaxResult<ushort?>.Default;
            return MinMax(filtered);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
        public static MinMaxResult<int?> MinMax(this IEnumerable<int?> source)
        {
            var filtered = source.Where(e => e.HasValue);
            if (!filtered.Any())
                return MinMaxResult<int?>.Default;
            return MinMax(filtered);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
        public static MinMaxResult<uint?> MinMax(this IEnumerable<uint?> source)
        {
            var filtered = source.Where(e => e.HasValue);
            if (!filtered.Any())
                return MinMaxResult<uint?>.Default;
            return MinMax(filtered);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
        public static MinMaxResult<long?> MinMax(this IEnumerable<long?> source)
        {
            var filtered = source.Where(e => e.HasValue);
            if (!filtered.Any())
                return MinMaxResult<long?>.Default;
            return MinMax(filtered);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
        public static MinMaxResult<ulong?> MinMax(this IEnumerable<ulong?> source)
        {
            var filtered = source.Where(e => e.HasValue);
            if (!filtered.Any())
                return MinMaxResult<ulong?>.Default;
            return MinMax(filtered);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
        public static MinMaxResult<float?> MinMax(this IEnumerable<float?> source)
        {
            var filtered = source.Where(e => e.HasValue);
            if (!filtered.Any())
                return MinMaxResult<float?>.Default;
            return MinMax(filtered);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
        public static MinMaxResult<double?> MinMax(this IEnumerable<double?> source)
        {
            var filtered = source.Where(e => e.HasValue);
            if (!filtered.Any())
                return MinMaxResult<double?>.Default;
            return MinMax(filtered);
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <returns>The minimum and maximum values of the objects in the sequence that are non-<see langword="null"/>, otherwise <seealso cref="MinMaxResult{T}.Default"/>.</returns>
        public static MinMaxResult<decimal?> MinMax(this IEnumerable<decimal?> source)
        {
            var filtered = source.Where(e => e.HasValue);
            if (!filtered.Any())
                return MinMaxResult<decimal?>.Default;
            return MinMax(filtered);
        }

        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<byte> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, byte> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<sbyte> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, sbyte> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<short> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, short> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<ushort> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, ushort> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<int> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<uint> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, uint> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<long> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<ulong> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<float> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<double> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<decimal> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            return MinMax(source.Select(selector));
        }

        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<byte?> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, byte?> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<sbyte?> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, sbyte?> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<short?> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, short?> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<ushort?> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, ushort?> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<int?> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<uint?> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, uint?> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<long?> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<ulong?> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong?> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<float?> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<double?> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
        {
            return MinMax(source.Select(selector));
        }
        /// <summary>Gets the minimum and maximum values within the collection.</summary>
        /// <param name="source">The collection. It must be non-<see langword="null"/>, and contain at least one element.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The minimum and maximum values.</returns>
        public static MinMaxResult<decimal?> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        {
            return MinMax(source.Select(selector));
        }
        #endregion

        #region For Each
        /// <summary>Performs an action on each of the elements contained in the collection.</summary>
        /// <typeparam name="T">The type of the elements that are contained in the collection.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="action">The action to perform on each of the elements.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T e in source)
                action(e);
        }
        /// <summary>Performs an action on each of the elements contained in the collection.</summary>
        /// <typeparam name="T">The type of the elements that are contained in the collection.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="action">The action to perform on each of the elements.</param>
        public static void ForEach<T>(this IEnumerable<T> source, IndexedEnumeratedElementAction<T> action)
        {
            foreach (var (index, e) in source.WithIndex())
                action(index, e);
        }
        #endregion

        #region LINQ-Like Operations
        /// <summary>Filters out the provided collection's elements based on the given predicate, and separates the matched from the unmatched ones, storing them in the respective collections.</summary>
        /// <typeparam name="T">The type of the elements that are contained in the <seealso cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="collection">The collection whose elements to filter out.</param>
        /// <param name="predicate">The predicate based on which to filter the elements out.</param>
        /// <param name="matched">The new collection that will contain the elements that matched the predicate.</param>
        /// <param name="unmatched">The new collection that will contain the elements that did not match the predicate.</param>
        public static void Dissect<T>(this IEnumerable<T> collection, Predicate<T> predicate, out IEnumerable<T> matched, out IEnumerable<T> unmatched)
        {
            if (!collection.Any())
            {
                matched = Enumerable.Empty<T>();
                unmatched = Enumerable.Empty<T>();
                return;
            }

            var matchedList = new List<T>();
            var unmatchedList = new List<T>();

            matched = matchedList;
            unmatched = unmatchedList;

            foreach (var e in collection)
            {
                if (predicate(e))
                    matchedList.Add(e);
                else
                    unmatchedList.Add(e);
            }
        }
        #endregion

        #region Enumeration
        /// <summary>Wraps the <seealso cref="IEnumerable{T}"/> into an <seealso cref="IndexedEnumerable{T}"/> for enumeration with index.</summary>
        /// <typeparam name="T">The type of the elements that are contained in the collection.</typeparam>
        /// <param name="enumerable">The <seealso cref="IEnumerable{T}"/> to enumerate with index.</param>
        /// <returns>The <seealso cref="IndexedEnumerable{T}"/> that wraps the <paramref name="enumerable"/> for indexed enumeration.</returns>
        public static IndexedEnumerable<T> WithIndex<T>(this IEnumerable<T> enumerable)
        {
            return new IndexedEnumerable<T>(enumerable);
        }
        #endregion

        #region Equality
        /// <summary>Determines whether all the elements of a collection are equal to all the respective elements of another collection in any order.</summary>
        /// <param name="source">The first collection.</param>
        /// <param name="other">The second collection.</param>
        /// <returns><see langword="true"/> if all the elements of any of the collections match exactly one unique element in the other collection, otherwise <see langword="false"/>. This is determined by whether both collections are subsets of each other.</returns>
        public static bool EqualsUnordered<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            var s = new HashSet<T>(source);
            return s.SetEquals(other);
        }
        #endregion

        #region Key/Value Pair
        /// <summary>Projects each <seealso cref="KeyValuePair{TKey, TValue}"/> in the sequence into a new <seealso cref="KeyValuePair{TKey, TValue}"/> with keys selected from the provided selector function.</summary>
        /// <typeparam name="TKey">The type of the key in the initial <seealso cref="KeyValuePair{TKey, TValue}"/> sequence.</typeparam>
        /// <typeparam name="TValue">The type of the value in the initial <seealso cref="KeyValuePair{TKey, TValue}"/> sequence.</typeparam>
        /// <typeparam name="TNewKey">The type of the key in the new <seealso cref="KeyValuePair{TKey, TValue}"/> sequence.</typeparam>
        /// <param name="kvps">The initial <seealso cref="KeyValuePair{TKey, TValue}"/> sequence.</param>
        /// <param name="keySelector">The function that transforms the initial <typeparamref name="TKey"/> into a <typeparamref name="TNewKey"/>.</param>
        /// <returns>The projected sequence.</returns>
        public static IEnumerable<KeyValuePair<TNewKey, TValue>> SelectKeys<TKey, TValue, TNewKey>(this IEnumerable<KeyValuePair<TKey, TValue>> kvps, Func<TKey, TNewKey> keySelector)
        {
            return kvps.Select(kvp => new KeyValuePair<TNewKey, TValue>(keySelector(kvp.Key), kvp.Value));
        }
        /// <summary>Projects each <seealso cref="KeyValuePair{TKey, TValue}"/> in the sequence into a new <seealso cref="KeyValuePair{TKey, TValue}"/> with values selected from the provided selector function.</summary>
        /// <typeparam name="TKey">The type of the key in the initial <seealso cref="KeyValuePair{TKey, TValue}"/> sequence.</typeparam>
        /// <typeparam name="TValue">The type of the value in the initial <seealso cref="KeyValuePair{TKey, TValue}"/> sequence.</typeparam>
        /// <typeparam name="TNewValue">The type of the value in the new <seealso cref="KeyValuePair{TKey, TValue}"/> sequence.</typeparam>
        /// <param name="kvps">The initial <seealso cref="KeyValuePair{TKey, TValue}"/> sequence.</param>
        /// <param name="valueSelector">The function that transforms the initial <typeparamref name="TValue"/> into a <typeparamref name="TNewValue"/>.</param>
        /// <returns>The projected sequence.</returns>
        public static IEnumerable<KeyValuePair<TKey, TNewValue>> SelectValues<TKey, TValue, TNewValue>(this IEnumerable<KeyValuePair<TKey, TValue>> kvps, Func<TValue, TNewValue> valueSelector)
        {
            return kvps.Select(kvp => new KeyValuePair<TKey, TNewValue>(kvp.Key, valueSelector(kvp.Value)));
        }
        #endregion

        private static void VerifyNonEmptyCollection<T>(IEnumerable<T> source)
        {
            if (source?.Any() != true)
                ThrowHelper.Throw<ArgumentException>("The collection must be non-null and contain at least one element.");
        }
    }
}
