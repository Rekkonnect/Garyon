using System.Numerics;

namespace Garyon.Objects;

/// <summary>
/// Contains the minimum and maximum values that were discovered from a
/// collection.
/// </summary>
/// <typeparam name="T">
/// The type of the elements.
/// </typeparam>
/// <param name="Min">
/// The minimum value.
/// </param>
/// <param name="Max">
/// The maximum value.
/// </param>
public record ValueBounds<T>(T? Min, T? Max)
{
    /// <summary>
    /// Gets a default instance of the <seealso cref="ValueBounds{T}"/> record
    /// with both values being <see langword="default"/>.
    /// </summary>
    public static readonly ValueBounds<T> Default = new(default, default);
}

public static class ValueBoundsExtensions
{
#if HAS_INUMBER
    extension<T>(ValueBounds<T>)
        where T : INumber<T>, IMinMaxValue<T>
    {
        /// <summary>
        /// Gets a <see cref="ValueBounds{T}"/> with the
        /// <see cref="IMinMaxValue{TSelf}.MinValue"/>
        /// and <see cref="IMinMaxValue{TSelf}.MaxValue"/>
        /// as the corresponding values.
        /// </summary>
        public static ValueBounds<T> MinMaxValues => NumericProperties<T>.MinMaxValues;
    }

    private static class NumericProperties<T>
        where T : IMinMaxValue<T>
    {
        public static readonly ValueBounds<T> MinMaxValues = new(T.MinValue, T.MaxValue);
    }
#endif
}
