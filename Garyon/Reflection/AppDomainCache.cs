using Garyon.Objects.Advanced;
using System;
using System.Collections.Generic;

namespace Garyon.Reflection;

/// <summary>
/// Contains cached information about <seealso cref="AppDomain"/> instances.
/// </summary>
public class AppDomainCache
{
    /// <summary>
    /// Gets the instance for <seealso cref="AppDomain.CurrentDomain"/>.
    /// </summary>
    public static AppDomainCache Current { get; } = new(AppDomain.CurrentDomain);

    private readonly AdvancedLazy<TypeListCache> allTypes;

    /// <summary>
    /// Gets all the types that are defined in this
    /// <seealso cref="AppDomain"/>'s containing assemblies. If the types are
    /// not cached; a full scan will be performed.
    /// </summary>
    /// <remarks>
    /// WARNING: This is a highly expensive operation, costing both
    /// computationally and space-wise.
    /// </remarks>
    public TypeListCache GetAllTypes() => allTypes.GetValue();
    /// <summary>
    /// Gets all the classes that are defined in this
    /// <seealso cref="AppDomain"/>'s containing assemblies. If the types are
    /// not cached; a full scan will be performed.
    /// </summary>
    /// <remarks>
    /// This filters the cached types from <seealso cref="GetAllTypes"/>.
    /// </remarks>
    public IEnumerable<Type> GetAllClasses() => GetAllTypes().GetClasses();
    /// <summary>
    /// Gets all abstract classes that are defined in this
    /// <seealso cref="AppDomain"/>'s containing assemblies. If the types are
    /// not cached; a full scan will be performed.
    /// </summary>
    /// <remarks>
    /// This filters the cached types from <seealso cref="GetAllTypes"/>.
    /// </remarks>
    public IEnumerable<Type> GetAllAbstractClasses() => GetAllTypes().GetAbstractClasses();
    /// <summary>
    /// Gets all non-abstract classes that are defined in this
    /// <seealso cref="AppDomain"/>'s containing assemblies. If the types are
    /// not cached; a full scan will be performed.
    /// </summary>
    /// <remarks>
    /// This filters the cached types from <seealso cref="GetAllTypes"/>.
    /// </remarks>
    public IEnumerable<Type> GetAllNonAbstractClasses() => GetAllTypes().GetNonAbstractClasses();
    /// <summary>
    /// Gets all the structs that are defined in this
    /// <seealso cref="AppDomain"/>'s containing assemblies. If the types are
    /// not cached; a full scan will be performed.
    /// </summary>
    /// <remarks>
    /// This filters the cached types from <seealso cref="GetAllTypes"/>.
    /// </remarks>
    public IEnumerable<Type> GetAllStructs() => GetAllTypes().GetStructs();
    /// <summary>
    /// Gets all the interfaces that are defined in this
    /// <seealso cref="AppDomain"/>'s containing assemblies. If the types are
    /// not cached; a full scan will be performed.
    /// </summary>
    /// <remarks>
    /// This filters the cached types from <seealso cref="GetAllTypes"/>.
    /// </remarks>
    public IEnumerable<Type> GetAllInterfaces() => GetAllTypes().GetInterfaces();
    /// <summary>
    /// Gets all the static classes that are defined in this
    /// <seealso cref="AppDomain"/>'s containing assemblies. If the types are
    /// not cached; a full scan will be performed.
    /// </summary>
    /// <remarks>
    /// This filters the cached types from <seealso cref="GetAllTypes"/>.
    /// </remarks>
    public IEnumerable<Type> GetAllStaticClasses() => GetAllTypes().GetStaticClasses();

    /// <summary>
    /// Gets the <seealso cref="AppDomain"/> instance for which this instance
    /// holds cache.
    /// </summary>
    public AppDomain Domain { get; }

    /// <summary>
    /// Initializes a new instance of the <seealso cref="AppDomainCache"/> class
    /// from an <seealso cref="AppDomain"/>.
    /// </summary>
    /// <param name="domain">
    /// The <seealso cref="AppDomain"/> instance for which this instance will
    /// hold cache.
    /// </param>
    public AppDomainCache(AppDomain domain)
    {
        Domain = domain;

        allTypes = new(() => new(Domain.GetAllTypes()));
    }

    /// <summary>
    /// Empties the cached types in <seealso cref="GetAllTypes"/>.
    /// </summary>
    public void EmptyAllTypesCache() => allTypes.ClearValue();
}
