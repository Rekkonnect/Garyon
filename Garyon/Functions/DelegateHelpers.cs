using System;

namespace Garyon.Functions;

public static class DelegateHelpers
{
    /// <summary>
    /// Tries the execution of a delegate and ignores any exceptions that may occur.
    /// Returns <see langword="true"/> if the delegate was executed successfully,
    /// otherwise <see langword="false"/> if an exception was thrown.
    /// </summary>
    public static bool Try(Action action)
    {
        try
        {
            action();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Invokes the specified function and returns its result,
    /// or a default value if an exception is thrown.
    /// </summary>
    /// <remarks>This method catches all exceptions thrown by the function and returns the default value
    /// instead. Use with caution, as it may suppress unexpected errors.</remarks>
    /// <typeparam name="T">The type of the value returned by the function.</typeparam>
    /// <param name="func">The function to invoke. Cannot be null.</param>
    /// <param name="defaultValue">The value to return if the function throws an exception.
    /// The default value for type T is used if not specified.</param>
    /// <returns>
    /// The result of the function if it completes successfully; otherwise,
    /// the specified default value.
    /// </returns>
    public static T? Try<T>(Func<T> func, T? defaultValue = default)
    {
        try
        {
            return func();
        }
        catch
        {
            return defaultValue;
        }
    }
}
