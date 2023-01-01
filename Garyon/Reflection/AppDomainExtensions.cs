using Garyon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Reflection;

/// <summary>Contains extensions for the <seealso cref="AppDomain"/> type.</summary>
public static class AppDomainExtensions
{
    /// <summary>Gets all the types defined in all assemblies for the given <seealso cref="AppDomain"/>.</summary>
    /// <param name="domain">The <seealso cref="AppDomain"/> whose types to get.</param>
    public static IEnumerable<Type> GetAllTypes(this AppDomain domain) => domain.GetAssemblies().SelectMany(domain => domain.GetTypes());

    /// <summary>Gets and filters all the types defined in all assemblies for the given <seealso cref="AppDomain"/>.</summary>
    /// <param name="domain">The <seealso cref="AppDomain"/> whose types to get.</param>
    /// <param name="predicate">The filter to determine which types are included in the result.</param>
    /// <returns>The filtered types contained in all assemblies in <paramref name="domain"/>.</returns>
    public static IEnumerable<Type> GetAllTypes(this AppDomain domain, Predicate<Type> predicate) => domain.GetAllTypes().WherePredicate(predicate);
}
