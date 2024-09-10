using Garyon.Exceptions;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Garyon.Objects;

/// <summary>Represents comparison kinds.</summary>
[Flags]
public enum ComparisonKinds
{
    /// <summary>Represents the default value of the enum. It should only be used in argument validation.</summary>
    None = 0,

    // The flags are implemented as such:
    // LEG
    // L: Less than
    // E: Equal to
    // G: Greater than

    /// <summary>A value is less than (&lt;) another.</summary>
    Less = 0b100,
    /// <summary>A value is equal to (=) another.</summary>
    Equal = 0b010,
    /// <summary>A value is greater than (&gt;) another.</summary>
    Greater = 0b001,

    /// <summary>A value is less than or equal to (&lt;=) another.</summary>
    LessOrEqual = Less | Equal,
    /// <summary>A value is greater than or equal to (&gt;=) another.</summary>
    GreaterOrEqual = Greater | Equal,
    /// <summary>A value is different than (!=) another.</summary>
    /// <remarks>The value is identical to <seealso cref="NotEqual"/>.</remarks>
    Different = Less | Greater,
    /// <summary>A value is not equal to (!=) another.</summary>
    /// <remarks>The value is identical to <seealso cref="Different"/>.</remarks>
    NotEqual = Different,

    /// <summary>Represents all comparison kinds.</summary>
    All = NotEqual | Equal,
}

/// <summary>Contains extensions for the <seealso cref="ComparisonKinds"/> enum.</summary>
public static class ComparisonTypeExtensions
{
    /// <summary>Gets the comparison kind from a <seealso cref="ComparisonResult"/>.</summary>
    /// <param name="result">The <seealso cref="ComparisonResult"/> to map to its respective <seealso cref="ComparisonKinds"/> value.</param>
    /// <returns>A <seealso cref="ComparisonKinds"/> value with a single kind representing the given <seealso cref="ComparisonResult"/>.</returns>
    public static ComparisonKinds GetComparisonKind(this ComparisonResult result) => result switch
    {
        ComparisonResult.Less => ComparisonKinds.Less,
        ComparisonResult.Equal => ComparisonKinds.Equal,
        ComparisonResult.Greater => ComparisonKinds.Greater,
    };

    /// <summary>Determines whether a <seealso cref="ComparisonKinds"/> contains a comparison kind that matches the given <seealso cref="ComparisonResult"/>.</summary>
    /// <param name="kinds">The comparison kinds to determine whether they are satisfied by the result.</param>
    /// <param name="result">The </param>
    /// <returns><see langword="true"/> if there is at least one comparison kind in <paramref name="kinds"/> that was matched by the <paramref name="result"/>, otherwise <see langword="false"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Matches(this ComparisonKinds kinds, ComparisonResult result) => HasKinds(kinds, result.GetComparisonKind());
    /// <summary>Determines whether a <seealso cref="ComparisonKinds"/> value contains the given kinds.</summary>
    /// <param name="kinds">The kinds value to determine if it contains the requested kinds.</param>
    /// <param name="other">The requested comparison kinds to check if they are contained in the given <seealso cref="ComparisonKinds"/> value.</param>
    /// <returns><see langword="true"/> if <paramref name="kinds"/> contains at least all of the requested kinds in <paramref name="other"/>, otherwise <see langword="false"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasKinds(this ComparisonKinds kinds, ComparisonKinds other) => (kinds & other) == other;

    /// <summary>Converts the given comparison kind into a <seealso cref="ComparisonResult"/> value.</summary>
    /// <param name="kind">The single comparison kind that should be </param>
    /// <returns>The respective <seealso cref="ComparisonResult"/> value.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="kind"/> does not represent exactly a single comparison kind.</exception>
    public static ComparisonResult AsResult(this ComparisonKinds kind) => kind switch
    {
        ComparisonKinds.Less => ComparisonResult.Less,
        ComparisonKinds.Equal => ComparisonResult.Equal,
        ComparisonKinds.Greater => ComparisonResult.Greater,
        _ => ThrowNotSingleComparisonKind(),
    };

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static ComparisonResult ThrowNotSingleComparisonKind()
    {
        throw ThrowHelper.Throw<ArgumentException>("There must only be given one comparison kind to convert into a comparison result.");
    }
}
