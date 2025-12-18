using System;

namespace Garyon.Exceptions;

/// <summary>
/// An exception that is thrown when a service, external or not,
/// provides a bad response (error or bad data).
/// </summary>
public sealed class BadServiceResponseException(
    string message = "The service's response was not desired.")
    : Exception(message)
{
}
