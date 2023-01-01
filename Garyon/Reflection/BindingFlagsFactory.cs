using System.Reflection;

namespace Garyon.Reflection;

/// <summary>Contains constants and generators that help generating <seealso cref="BindingFlags"/> values without creating large expressions.</summary>
public static class BindingFlagsFactory
{
    /// <summary>Represents the combination <seealso cref="BindingFlags.Public"/> | <seealso cref="BindingFlags.NonPublic"/>.</summary>
    public const BindingFlags AnyAccessibility = BindingFlags.Public | BindingFlags.NonPublic;
    /// <summary>Represents the combination <seealso cref="BindingFlags.Static"/> | <seealso cref="AnyAccessibility"/>.</summary>
    public const BindingFlags AnyAccessibilityStatic = BindingFlags.Static | AnyAccessibility;
    /// <summary>Represents the combination <seealso cref="BindingFlags.Instance"/> | <seealso cref="AnyAccessibility"/>.</summary>
    public const BindingFlags AnyAccessibilityInstance = BindingFlags.Instance | AnyAccessibility;
    /// <summary>Represents the combination <seealso cref="BindingFlags.Static"/> | <seealso cref="BindingFlags.Instance"/> | <seealso cref="AnyAccessibility"/>.</summary>
    public const BindingFlags AnyAccessibilityStaticOrInstance = BindingFlags.Static | BindingFlags.Instance | AnyAccessibility;
}
