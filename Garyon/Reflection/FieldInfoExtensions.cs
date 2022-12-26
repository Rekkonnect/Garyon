using System;
using System.Reflection;

namespace Garyon.Reflection;

/// <summary>Contains extension methods for the <seealso cref="FieldInfo"/> class.</summary>
public static class FieldInfoExtensions
{
    /// <summary>Determines the kind of the invokable type of a field.</summary>
    /// <param name="field">The field whose type to evaluate.</param>
    /// <returns>
    /// The respective kind of invokable type that the field's type is.
    /// <seealso cref="InvokableTypeKind.Invalid"/> will be returned if
    /// the field type is not an invokable type.
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
}
