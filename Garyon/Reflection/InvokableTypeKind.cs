namespace Garyon.Reflection;

/// <summary>
/// Determines the kind of an invokable type. It could be a
/// delegate, a function pointer, or not a valid invokable type.
/// </summary>
public enum InvokableTypeKind
{
    /// <summary>
    /// Represents an invalid invokable type. It could be a type
    /// that is not invokable, or an unsupported invokable type.
    /// </summary>
    Invalid,
    /// <summary>Represents a delegate type.</summary>
    Delegate,
    /// <summary>Represents a function pointer type.</summary>
    FunctionPointer
}
