using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Garyon.Functions;

/// <summary>Provides helper functions and extensions for the <seealso cref="AppDomain"/> class.</summary>
public static class AppDomainHelpers
{
    /// <summary>Forces loading all assemblies that are referenced in the given <seealso cref="AppDomain"/>.</summary>
    /// <param name="domain">The <seealso cref="AppDomain"/> whose referenced assemblies to force loading.</param>
    public static void ForceLoadAllAssemblies(this AppDomain domain)
    {
        // Graciously copied from some helpful guy on the internet
        // https://stackoverflow.com/a/2384679/11438007
        // This function should exist in the BCL
        var staticLoadedAssemblies = domain.GetStaticAssemblies();
        var staticLoadedPaths = staticLoadedAssemblies.Select(a => a.Location).ToArray();

        var referencedPaths = Directory.GetFiles(domain.BaseDirectory, "*.dll");
        var toLoad = referencedPaths.Except(staticLoadedPaths, StringComparer.InvariantCultureIgnoreCase);
        var names = toLoad.Select(AssemblyName.GetAssemblyName);

        foreach (var assemblyName in names)
            domain.Load(assemblyName);
    }
    /// <summary>Forces loading all assemblies that are referenced in <seealso cref="AppDomain.CurrentDomain"/>.</summary>
    public static void ForceLoadAllAssembliesCurrent()
    {
        ForceLoadAllAssemblies(AppDomain.CurrentDomain);
    }

    /// <summary>Gets all the static assemblies that are loaded.</summary>
    /// <param name="domain">The <seealso cref="AppDomain"/> whose static assemblies to get.</param>
    /// <returns>A collection of <seealso cref="Assembly"/> instances representing the static assemblies that the given <seealso cref="AppDomain"/> contains.</returns>
    /// <remarks>Only the loaded assemblies are included. Consider using <seealso cref="ForceLoadAllAssemblies(AppDomain)"/>.</remarks>
    [ExcludeFromCodeCoverage]
    public static IEnumerable<Assembly> GetStaticAssemblies(this AppDomain domain)
    {
        return domain.GetAssemblies().Where(assembly => !assembly.IsDynamic);
    }
    /// <summary>Gets all the dynamic assemblies that are loaded.</summary>
    /// <param name="domain">The <seealso cref="AppDomain"/> whose dynamic assemblies to get.</param>
    /// <returns>A collection of <seealso cref="Assembly"/> instances representing the dynamic assemblies that the given <seealso cref="AppDomain"/> contains.</returns>
    /// <remarks>Only the loaded assemblies are included..</remarks>
    [ExcludeFromCodeCoverage]
    public static IEnumerable<Assembly> GetDynamicAssemblies(this AppDomain domain)
    {
        return domain.GetAssemblies().Where(assembly => assembly.IsDynamic);
    }
}
