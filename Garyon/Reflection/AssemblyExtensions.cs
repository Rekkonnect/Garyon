using System;
using System.Reflection;

namespace Garyon.Reflection;

/// <summary>
/// Contains extensions for the <seealso cref="Assembly"/> type.
/// </summary>
public static class AssemblyExtensions
{
    /// <summary>
    /// Gets the types of the assembly, through the
    /// <seealso cref="Assembly.GetTypes"/> method, or
    /// <seealso cref="Type.EmptyTypes"/> if the method fails.
    /// </summary>
    /// <param name="assembly">The assembly whose types to get.</param>
    /// <returns>
    /// The result of the <seealso cref="Assembly.GetTypes"/> method,
    /// or <seealso cref="Type.EmptyTypes"/> if it fails.
    /// </returns>
    public static Type[] GetTypesOrEmpty(this Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch
        {
            return Type.EmptyTypes;
        }
    }
}
