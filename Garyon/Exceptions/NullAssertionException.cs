using System;

namespace Garyon.Exceptions;

/// <summary>
/// An exception that is thrown when a specific path in the code is expected to
/// be unreachable.
/// </summary>
/// <remarks>
/// This exception should not be documented as throwable, despite its presence
/// in the method's body.
/// </remarks>
public sealed class NullAssertionException(string message = "Expected the value to not be null")
    : Exception(message);
