using System;

namespace Garyon.Reflection
{
    /// <summary>Contains values representing the accessibility modifiers of a member.</summary>
    [Flags]
    public enum AccessibilityModifiers : byte
    {
        /// <summary>The default value, representing no accesibility modifiers.</summary>
        None = 0,

        /// <summary>The <see langword="public"/> modifier.</summary>
        Public = 0x1,
        /// <summary>The <see langword="internal"/> modifier.</summary>
        Internal = 0x2,
        /// <summary>The <see langword="protected internal"/> modifier.</summary>
        ProtectedInternal = 0x4,
        /// <summary>The <see langword="protected"/> modifier.</summary>
        Protected = 0x8,
        /// <summary>The <see langword="private protected"/> modifier.</summary>
        PrivateProtected = 0x10,
        /// <summary>The <see langword="private"/> modifier.</summary>
        Private = 0x20,
    }
}
