namespace Garyon.Objects;

/// <summary>Represents the result of a comparison.</summary>
public enum ComparisonResult
{
    /// <summary>Indicates that the first value of the comparison is less than the second.</summary>
    Less = -1,
    /// <summary>Indicates that the first value of the comparison is equal to the second.</summary>
    Equal = 0,
    /// <summary>Indicates that the first value of the comparison is greater than the second.</summary>
    Greater = 1,
}
