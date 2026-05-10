using Garyon.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Reflection;

/// <summary>
/// Contains a cached list of types and exposes filtered views for them.
/// </summary>
public sealed class TypeListCache : IReadOnlyList<Type>
{
    private readonly Type[] types;

    /// <summary>
    /// Initializes a new instance of the <seealso cref="TypeListCache"/> class.
    /// </summary>
    /// <param name="types">
    /// The types to cache.
    /// </param>
    public TypeListCache(IEnumerable<Type> types)
    {
        this.types = types.ToArray();
    }

    /// <summary>
    /// Gets the cached types.
    /// </summary>
    public IReadOnlyList<Type> Types => types;

    /// <summary>
    /// Gets the number of cached types.
    /// </summary>
    public int Count => types.Length;

    /// <summary>
    /// Gets the type at the given index.
    /// </summary>
    public Type this[int index] => types[index];

    /// <summary>
    /// Gets all the classes in the cached types.
    /// </summary>
    public IEnumerable<Type> GetClasses() => GetFilteredTypes(TypePredicates.IsClass);
    /// <summary>
    /// Gets all the abstract classes in the cached types.
    /// </summary>
    public IEnumerable<Type> GetAbstractClasses() => GetFilteredTypes(TypePredicates.IsAbstractClass);
    /// <summary>
    /// Gets all the non-abstract classes in the cached types.
    /// </summary>
    public IEnumerable<Type> GetNonAbstractClasses() => GetFilteredTypes(TypePredicates.IsNonAbstractClass);
    /// <summary>
    /// Gets all the structs in the cached types.
    /// </summary>
    public IEnumerable<Type> GetStructs() => GetFilteredTypes(TypePredicates.IsValueType);
    /// <summary>
    /// Gets all the interfaces in the cached types.
    /// </summary>
    public IEnumerable<Type> GetInterfaces() => GetFilteredTypes(TypePredicates.IsInterface);
    /// <summary>
    /// Gets all the static classes in the cached types.
    /// </summary>
    public IEnumerable<Type> GetStaticClasses() => GetFilteredTypes(TypePredicates.IsStatic);

    /// <summary>
    /// Gets a filtered view of the cached types.
    /// </summary>
    /// <param name="predicate">
    /// The predicate to filter the types with.
    /// </param>
    public IEnumerable<Type> GetFilteredTypes(Predicate<Type> predicate) => types.WherePredicate(predicate);

    /// <inheritdoc/>
    public IEnumerator<Type> GetEnumerator() => ((IEnumerable<Type>)types).GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
