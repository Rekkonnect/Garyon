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
    public UnreachableException()
        : this("This path in the code is expected to be unreachable.") { }
    public UnreachableException(string message)
        : base(message) { }
}
