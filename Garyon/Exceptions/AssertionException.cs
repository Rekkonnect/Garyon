using System;

namespace Garyon.Exceptions;

/// <summary>
/// An exception that is thrown when an assertion fails.
/// </summary>
public class AssertionException(string message)
    : Exception(message);
