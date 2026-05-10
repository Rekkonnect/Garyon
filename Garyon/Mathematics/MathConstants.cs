using System;
using System.Diagnostics.CodeAnalysis;

namespace Garyon.Mathematics;

/// <summary>
/// Provides common math constants.
/// </summary>
[ExcludeFromCodeCoverage]
public static class MathConstants
{
    /// <summary>
    /// The value of 2 * <see cref="Math.PI"/>.
    /// </summary>
    public const double TwoPi = Math.PI * 2;

    /// <summary>
    /// The value of <see cref="Math.Sqrt(double)"/> of 2.
    /// </summary>
    public static readonly double Sqrt2 = Math.Sqrt(2);
    /// <summary>
    /// The value of <see cref="Math.Sqrt(double)"/> of 3.
    /// </summary>
    public static readonly double Sqrt3 = Math.Sqrt(3);

    /// <summary>
    /// The value of sin(30 deg).
    /// </summary>
    public const double Sin30 = 0.5;
    /// <summary>
    /// The value of sin(45 deg).
    /// </summary>
    public static readonly double Sin45 = Sqrt2 / 2;
    /// <summary>
    /// The value of sin(60 deg).
    /// </summary>
    public static readonly double Sin60 = Sqrt3 / 2;

    /// <summary>
    /// The value of tan(30 deg).
    /// </summary>
    public static readonly double Tan30 = 1 / Sqrt3;
    /// <summary>
    /// The value of tan(45 deg).
    /// </summary>
    public const double Tan45 = 1;
    /// <summary>
    /// The value of tan(60 deg).
    /// </summary>
    public static readonly double Tan60 = Sqrt3;
}
