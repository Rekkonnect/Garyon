using System;

namespace Garyon.Reflection;

/// <summary>
/// Contains values representing the accessibility modifiers of a member.
/// </summary>
[Flags]
public enum AccessibilityModifiers : byte
{
    /// <summary>
    /// The default value, representing no accessibility modifiers.
    /// </summary>
    None = 0,

    /// <summary>
    /// The <see langword="public"/> modifier.
    /// </summary>
    Public = 1 << 0,
    /// <summary>
    /// The <see langword="internal"/> modifier.
    /// </summary>
    Internal = 1 << 1,
    /// <summary>
    /// The <see langword="protected internal"/> modifier.
    /// </summary>
    ProtectedInternal = 1 << 2,
    /// <summary>
    /// The <see langword="protected"/> modifier.
    /// </summary>
    Protected = 1 << 3,
    /// <summary>
    /// The <see langword="private protected"/> modifier.
    /// </summary>
    PrivateProtected = 1 << 4,
    /// <summary>
    /// The <see langword="private"/> modifier.
    /// </summary>
    Private = 1 << 5,
    /// <summary>
    /// The <see langword="file"/> modifier.
    /// </summary>
    File = 1 << 6,

    /// <summary>
    /// All accessibility modifiers.
    /// </summary>
    AllAccessibilities = Public | Internal | ProtectedInternal | Protected | PrivateProtected | Private | File,
}
