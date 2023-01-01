using System;

namespace Garyon.Reflection;

/// <summary>Contains values representing the modifiers of a <seealso cref="Type"/>.</summary>
[Flags]
public enum TypeModifiers : ushort
{
    /// <summary>The default value, representing no type modifiers.</summary>
    None = 0,

    // Accessibility
    /// <summary>The <see langword="public"/> modifier.</summary>
    Public = AccessibilityModifiers.Public,
    /// <summary>The <see langword="internal"/> modifier.</summary>
    Internal = AccessibilityModifiers.Internal,
    /// <summary>The <see langword="protected internal"/> modifier.</summary>
    ProtectedInternal = AccessibilityModifiers.ProtectedInternal,
    /// <summary>The <see langword="protected"/> modifier.</summary>
    Protected = AccessibilityModifiers.Protected,
    /// <summary>The <see langword="private protected"/> modifier.</summary>
    PrivateProtected = AccessibilityModifiers.PrivateProtected,
    /// <summary>The <see langword="private"/> modifier.</summary>
    Private = AccessibilityModifiers.Private,

    /// <summary>All accesibility modifiers.</summary>
    AllAccessibilities = AccessibilityModifiers.AllAccessibilities,

    // Definition
    /// <summary>The <see langword="static"/> modifier.</summary>
    Static = 0x40,
    /// <summary>The <see langword="abstract"/> modifier.</summary>
    Abstract = 0x80,
    /// <summary>The <see langword="sealed"/> modifier.</summary>
    Sealed = 0x100,
    /// <summary>The <see langword="readonly"/> modifier.</summary>
    Readonly = 0x200,
    /// <summary>The <see langword="ref"/> modifier.</summary>
    Ref = 0x400,
    /// <summary>The <see langword="readonly ref"/> modifier.</summary>
    ReadonlyRef = Readonly | Ref,

    /// <summary>All definition modifiers.</summary>
    AllDefinitions = Static | Abstract | Sealed | ReadonlyRef,

    /// <summary>All modifiers.</summary>
    AllModifiers = AllAccessibilities | AllDefinitions,

    // Combinations
    // The combinations were added to make it easier for the consumer to use a combination of these modifiers
    // In fact, there aren't many combinations that can be used anyway, and it mainly consists of up to 2 different values being combined

    // Static
    /// <summary>The <see langword="public static"/> modifier combination.</summary>
    PublicStatic = Public | Static,
    /// <summary>The <see langword="internal static"/> modifier combination.</summary>
    InternalStatic = Internal | Static,
    /// <summary>The <see langword="protected internal static"/> modifier combination.</summary>
    ProtectedInternalStatic = ProtectedInternal | Static,
    /// <summary>The <see langword="protected static"/> modifier combination.</summary>
    ProtectedStatic = Protected | Static,
    /// <summary>The <see langword="private protected static"/> modifier combination.</summary>
    PrivateProtectedStatic = PrivateProtected | Static,
    /// <summary>The <see langword="private static"/> modifier combination.</summary>
    PrivateStatic = Private | Static,

    // Abstract
    /// <summary>The <see langword="public abstract"/> modifier combination.</summary>
    PublicAbstract = Public | Abstract,
    /// <summary>The <see langword="internal abstract"/> modifier combination.</summary>
    InternalAbstract = Internal | Abstract,
    /// <summary>The <see langword="protected internal abstract"/> modifier combination.</summary>
    ProtectedInternalAbstract = ProtectedInternal | Abstract,
    /// <summary>The <see langword="protected abstract"/> modifier combination.</summary>
    ProtectedAbstract = Protected | Abstract,
    /// <summary>The <see langword="private protected abstract"/> modifier combination.</summary>
    PrivateProtectedAbstract = PrivateProtected | Abstract,
    /// <summary>The <see langword="private abstract"/> modifier combination.</summary>
    PrivateAbstract = Private | Abstract,

    // Sealed
    /// <summary>The <see langword="public sealed"/> modifier combination.</summary>
    PublicSealed = Public | Sealed,
    /// <summary>The <see langword="internal sealed"/> modifier combination.</summary>
    InternalSealed = Internal | Sealed,
    /// <summary>The <see langword="protected internal sealed"/> modifier combination.</summary>
    ProtectedInternalSealed = ProtectedInternal | Sealed,
    /// <summary>The <see langword="protected sealed"/> modifier combination.</summary>
    ProtectedSealed = Protected | Sealed,
    /// <summary>The <see langword="private protected sealed"/> modifier combination.</summary>
    PrivateProtectedSealed = PrivateProtected | Sealed,
    /// <summary>The <see langword="private sealed"/> modifier combination.</summary>
    PrivateSealed = Private | Sealed,

    // Readonly
    /// <summary>The <see langword="public readonly"/> modifier combination.</summary>
    PublicReadonly = Public | Readonly,
    /// <summary>The <see langword="internal readonly"/> modifier combination.</summary>
    InternalReadonly = Internal | Readonly,
    /// <summary>The <see langword="protected internal readonly"/> modifier combination.</summary>
    ProtectedInternalReadonly = ProtectedInternal | Readonly,
    /// <summary>The <see langword="protected readonly"/> modifier combination.</summary>
    ProtectedReadonly = Protected | Readonly,
    /// <summary>The <see langword="private protected readonly"/> modifier combination.</summary>
    PrivateProtectedReadonly = PrivateProtected | Readonly,
    /// <summary>The <see langword="private readonly"/> modifier combination.</summary>
    PrivateReadonly = Private | Readonly,

    // Ref
    /// <summary>The <see langword="public ref"/> modifier combination.</summary>
    PublicRef = Public | Ref,
    /// <summary>The <see langword="internal ref"/> modifier combination.</summary>
    InternalRef = Internal | Ref,
    /// <summary>The <see langword="protected internal ref"/> modifier combination.</summary>
    ProtectedInternalRef = ProtectedInternal | Ref,
    /// <summary>The <see langword="protected ref"/> modifier combination.</summary>
    ProtectedRef = Protected | Ref,
    /// <summary>The <see langword="private protected ref"/> modifier combination.</summary>
    PrivateProtectedRef = PrivateProtected | Ref,
    /// <summary>The <see langword="private ref"/> modifier combination.</summary>
    PrivateRef = Private | Ref,

    // ReadonlyRef
    /// <summary>The <see langword="public readonly ref"/> modifier combination.</summary>
    PublicReadonlyRef = Public | ReadonlyRef,
    /// <summary>The <see langword="internal readonly ref"/> modifier combination.</summary>
    InternalReadonlyRef = Internal | ReadonlyRef,
    /// <summary>The <see langword="protected internal readonly ref"/> modifier combination.</summary>
    ProtectedInternalReadonlyRef = ProtectedInternal | ReadonlyRef,
    /// <summary>The <see langword="protected readonly ref"/> modifier combination.</summary>
    ProtectedReadonlyRef = Protected | ReadonlyRef,
    /// <summary>The <see langword="private protected readonly ref"/> modifier combination.</summary>
    PrivateProtectedReadonlyRef = PrivateProtected | ReadonlyRef,
    /// <summary>The <see langword="private readonly ref"/> modifier combination.</summary>
    PrivateReadonlyRef = Private | ReadonlyRef,
}
