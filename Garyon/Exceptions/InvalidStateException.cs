using System;

namespace Garyon.Exceptions;

/// <summary>
/// An exception that is thrown when the state of an object
/// or operation is corrupted. Usually an assertion failure
/// may lead to throwing this exception.
/// </summary>
public sealed class InvalidStateException(
    string message = "The operation's state is invalid.")
    : Exception(message)
{
}
