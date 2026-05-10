using Garyon.Functions;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Garyon.Reflection;

/// <summary>
/// Contains extension methods for the <seealso cref="FieldInfo"/> class.
/// </summary>
public static class FieldInfoExtensions
{
    /// <summary>
    /// Determines the kind of the invokable type of a field.
    /// </summary>
    /// <param name="field">
    /// The field whose type to evaluate.
    /// </param>
    /// <returns>
    /// The respective kind of invokable type that the field's type is.
    /// <seealso cref="InvokableTypeKind.Invalid"/> will be returned if the
    /// field type is not an invokable type.
    /// </returns>
    public static InvokableTypeKind GetFieldInvokableTypeKind(this FieldInfo field)
    {
        var type = field.FieldType;

        // Subject (hopefully) to change in future versions of .NET
        if (type == typeof(IntPtr))
            return InvokableTypeKind.FunctionPointer;

        if (type.IsDelegate())
            return InvokableTypeKind.Delegate;

        return InvokableTypeKind.Invalid;
    }

    public static AccessibilityModifiers GetAccessibilityModifiers(this FieldInfo field)
    {
        return Misc.ValueIf(AccessibilityModifiers.Public, field.IsPublic)
            | Misc.ValueIf(AccessibilityModifiers.Internal, field.IsAssembly)
            | Misc.ValueIf(AccessibilityModifiers.ProtectedInternal, field.IsFamilyOrAssembly)
            | Misc.ValueIf(AccessibilityModifiers.Protected, field.IsFamily)
            | Misc.ValueIf(AccessibilityModifiers.PrivateProtected, field.IsFamilyAndAssembly)
            | Misc.ValueIf(AccessibilityModifiers.Private, field.IsPrivate)
            ;
    }
}

/// <summary>
/// Contains extension methods for the <seealso cref="PropertyInfo"/> class.
/// </summary>
public static class PropertyInfoExtensions
{
    public static AccessibilityModifiers GetAccessibilityModifiers(this PropertyInfo property)
    {
        var getterAccessibility = property.GetMethod?.GetAccessibilityModifiers() ?? AccessibilityModifiers.None;
        var setterAccessibility = property.SetMethod?.GetAccessibilityModifiers() ?? AccessibilityModifiers.None;
        return (AccessibilityModifiers)Math.Max((int)getterAccessibility, (int)setterAccessibility);
    }
}

/// <summary>
/// Contains extension methods for the <seealso cref="EventInfo"/> class.
/// </summary>
public static class EventInfoExtensions
{
    public static AccessibilityModifiers GetAccessibilityModifiers(this EventInfo @event)
    {
        var addMethodAccessibility = @event.AddMethod?.GetAccessibilityModifiers() ?? AccessibilityModifiers.None;
        var removeMethodAccessibility = @event.RemoveMethod?.GetAccessibilityModifiers() ?? AccessibilityModifiers.None;
        return (AccessibilityModifiers)Math.Max((int)addMethodAccessibility, (int)removeMethodAccessibility);
    }
}
