using System;

namespace Garyon.Exceptions;

/// <summary>
/// An exception that is thrown when a contract in the code is not met.
/// </summary>
public class ContractException(string message)
    : Exception(message);
