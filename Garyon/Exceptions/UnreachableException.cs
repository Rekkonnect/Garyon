#if !HAS_UNREACHABLE_EXCEPTION

using System;

namespace Garyon.Exceptions;

/// <summary>
/// An exception that is thrown when a specific path in
/// the code is expected to be unreachable.
/// </summary>
/// <remarks>
/// This exception should not be documented as
/// throwable, despite its presence in the method's body.
/// </remarks>
public sealed class UnreachableException : Exception
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public UnreachableException()
        : this("This path in the code is expected to be unreachable.") { }
    public UnreachableException(string message)
        : base(message) { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

#endif
