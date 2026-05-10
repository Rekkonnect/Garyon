using System;

namespace Garyon.Functions;

/// <summary>
/// Contains functions that can be used as predicates in functions that filter
/// collections. Using these functions is advised over lambdas, as lambdas
/// create a new delegate instance, effectively reducing performance.
/// </summary>
public static class Predicates
{
    /// <summary>
    /// Always returns <see langword="true"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the element.
    /// </typeparam>
    /// <param name="_">
    /// The element.
    /// </param>
    /// <returns>
    /// Always <see langword="true"/>.
    /// </returns>
    public static bool True<T>(T _) => true;

    /// <summary>
    /// Always returns <see langword="false"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the element.
    /// </typeparam>
    /// <param name="_">
    /// The element.
    /// </param>
    /// <returns>
    /// Always <see langword="false"/>.
    /// </returns>
    public static bool False<T>(T _) => false;

    /// <summary>
    /// Determines whether the provided element is not <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the element.
    /// </typeparam>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the element is not <see langword="null"/>,
    /// otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// For nullable structs, consider using <see cref="HasValue{T}(T?)"/>.
    /// </remarks>
    public static bool NotNull<T>(T element) => element is not null;

    /// <summary>
    /// Determines whether the provided element is <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the element.
    /// </typeparam>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the element is <see langword="null"/>,
    /// otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// For nullable structs, consider using
    /// <see cref="DoesNotHaveValue{T}(T?)"/>.
    /// </remarks>
    public static bool Null<T>(T element) => element is null;

    /// <summary>
    /// Determines whether the provided nullable struct object has a value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the struct.
    /// </typeparam>
    /// <param name="element">
    /// The nullable struct object.
    /// </param>
    /// <returns>
    /// The value equal to <seealso cref="Nullable{T}.HasValue"/> for the
    /// provided object.
    /// </returns>
    public static bool HasValue<T>(T? element)
        where T : struct
    {
        return element.HasValue;
    }

    /// <summary>
    /// Determines whether the provided nullable struct object does not have a
    /// value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the struct.
    /// </typeparam>
    /// <param name="element">
    /// The nullable struct object.
    /// </param>
    /// <returns>
    /// The value opposite to <seealso cref="Nullable{T}.HasValue"/> for the
    /// provided object.
    /// </returns>
    public static bool DoesNotHaveValue<T>(T? element)
        where T : struct
    {
        return !element.HasValue;
    }

    /// <summary>
    /// Determines whether the provided string is neither <see langword="null"/> nor empty.
    /// </summary>
    /// <param name="s">
    /// The string.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when <paramref name="s"/> is not <see langword="null"/> and has a non-zero length;
    /// otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Equivalent to <c>!string.IsNullOrEmpty(s)</c>.
    /// </remarks>
    public static bool NotEmpty(string? s) => !string.IsNullOrEmpty(s);

    /// <summary>
    /// Determines whether the provided string is neither <see langword="null"/>, empty, nor consists exclusively of whitespace.
    /// </summary>
    /// <param name="s">
    /// The string.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when <paramref name="s"/> is not <see langword="null"/>, not empty, and contains at least one
    /// non-whitespace character; otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Equivalent to <c>!string.IsNullOrWhiteSpace(s)</c>.
    /// </remarks>
    public static bool NotEmptyOrWhitespace(string? s) => !string.IsNullOrWhiteSpace(s);
}
