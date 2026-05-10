using System;

namespace Garyon.Reflection;

/// <summary>
/// Contains a collection of predicates to apply on <seealso cref="Type"/>
/// instances. Also contains several aliases for other scattered functions.
/// </summary>
public static class TypePredicates
{
    /// <summary>
    /// Determines whether the given type is a class.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when <paramref name="type"/> represents a class; otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsClass(Type type) => type.IsClass;

    /// <summary>
    /// Determines whether the given type is an abstract class.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when <paramref name="type"/> represents an abstract class; otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsAbstractClass(Type type) => type.IsClass && type.IsAbstract;

    /// <summary>
    /// Determines whether the given type is a non-abstract class.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when <paramref name="type"/> represents a class that is not abstract; otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsNonAbstractClass(Type type) => type.IsClass && !type.IsAbstract;

    /// <summary>
    /// Determines whether the given type is a value type.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when <paramref name="type"/> represents a value type; otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsValueType(Type type) => type.IsValueType;

    /// <summary>
    /// Determines whether the given type is an interface.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when <paramref name="type"/> represents an interface; otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsInterface(Type type) => type.IsInterface;

    /// <summary>
    /// Determines whether the given type is a static class.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when <paramref name="type"/> represents a static class; otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsStatic(Type type) => type.IsStaticClass();
}
