namespace Garyon.Objects;

/// <summary>Contains the minimum and maximum values that were discovered from a collection.</summary>
/// <typeparam name="T">The type of the elements.</typeparam>
/// <param name="Min">The minimum value.</param>
/// <param name="Max">The maximum value.</param>
public record MinMaxResult<T>(T Min, T Max)
{
    /// <summary>Gets a default instance of the <seealso cref="MinMaxResult{T}"/> record with both values being <see langword="default"/>.</summary>
    public static readonly MinMaxResult<T> Default = new(default, default);

    /// <summary>Deconstructs this instance into separate minimum and maximum value variables.</summary>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    public void Deconstruct(out T min, out T max)
    {
        min = Min;
        max = Max;
    }
}
