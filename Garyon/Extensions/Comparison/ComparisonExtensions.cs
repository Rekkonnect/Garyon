namespace Garyon.Extensions.Comparison;

/// <summary>
/// Provides extension methods related to comparisons.
/// </summary>
public static class ComparisonExtensions
{
    /// <summary>
    /// Provides access to an ergonomic declarative approach to comparing by
    /// multiple properties.
    /// </summary>
    public static ComparisonSource<T> BeginCompare<T>(this T left, T right)
    {
        return new(left, right);
    }
}
