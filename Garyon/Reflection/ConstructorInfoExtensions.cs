using System;
using System.Reflection;

namespace Garyon.Reflection;

/// <summary>Provides extension methods for the <seealso cref="ConstructorInfo"/> class.</summary>
public static class ConstructorInfoExtensions
{
    /// <summary>Initializes a new instance of a type, given its parameterless <seealso cref="ConstructorInfo"/>.</summary>
    /// <typeparam name="T">The type of the resulting instance.</typeparam>
    /// <param name="ctor">The parameterless constructor of the type. Must not be <see langword="null"/>.</param>
    /// <returns>The initialized instance as an instance of type <typeparamref name="T"/>.</returns>
    /// <exception cref="NullReferenceException">The provided parameterless constructor was <see langword="null"/>.</exception>
    public static T InitializeInstance<T>(this ConstructorInfo ctor)
    {
        return ctor.InitializeInstance<T>(null);
    }
    /// <summary>Initializes a new instance of a type, given its <seealso cref="ConstructorInfo"/>.</summary>
    /// <typeparam name="T">The type of the resulting instance.</typeparam>
    /// <param name="ctor">The constructor of the type. Must not be <see langword="null"/>.</param>
    /// <param name="parameters">The parameters to pass to the constructor</param>
    /// <returns>The initialized instance as an instance of type <typeparamref name="T"/>.</returns>
    /// <exception cref="NullReferenceException">The provided constructor was <see langword="null"/>.</exception>
    public static T InitializeInstance<T>(this ConstructorInfo ctor, params object?[]? parameters)
    {
        return (T)ctor.Invoke(parameters);
    }
}
