using Garyon.Extensions;
using Garyon.Objects.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.Reflection
{
    /// <summary>Contains cached information about <seealso cref="AppDomain"/> instances.</summary>
    public class AppDomainCache
    {
        /// <summary>Gets the instance for <seealso cref="AppDomain.CurrentDomain"/>.</summary>
        public static AppDomainCache Current { get; } = new(AppDomain.CurrentDomain);

        private readonly AdvancedLazy<Type[]>
            allTypes,
            allClasses,
            allAbstractClasses,
            allNonAbstractClasses,
            allStructs,
            allInterfaces,
            allStaticClasses;

        /// <summary>Gets all the types that are defined in this <seealso cref="AppDomain"/>'s containing assemblies. If the types are not cached; a full scan will be performed.</summary>
        /// <remarks>WARNING: This is a highly expensive operation, costing both computationally and space-wise.</remarks>
        public IEnumerable<Type> AllTypes => allTypes.Value;
        /// <summary>Gets all the classes that are defined in this <seealso cref="AppDomain"/>'s containing assemblies. If the types are not cached; a full scan will be performed.</summary>
        /// <remarks>If <seealso cref="AllTypes"/> has been cached, this only filters the cached types.</remarks>
        public IEnumerable<Type> AllClasses => allClasses.Value;
        /// <summary>Gets all abstract classes that are defined in this <seealso cref="AppDomain"/>'s containing assemblies. If the types are not cached; a full scan will be performed.</summary>
        /// <remarks>If <seealso cref="AllTypes"/> has been cached, this only filters the cached types.</remarks>
        public IEnumerable<Type> AllAbstractClasses => allAbstractClasses.Value;
        /// <summary>Gets all non-abstract classes that are defined in this <seealso cref="AppDomain"/>'s containing assemblies. If the types are not cached; a full scan will be performed.</summary>
        /// <remarks>If <seealso cref="AllTypes"/> has been cached, this only filters the cached types.</remarks>
        public IEnumerable<Type> AllNonAbstractClasses => allNonAbstractClasses.Value;
        /// <summary>Gets all the structs that are defined in this <seealso cref="AppDomain"/>'s containing assemblies. If the types are not cached; a full scan will be performed.</summary>
        /// <remarks>If <seealso cref="AllTypes"/> has been cached, this only filters the cached types.</remarks>
        public IEnumerable<Type> AllStructs => allStructs.Value;
        /// <summary>Gets all the interfaces that are defined in this <seealso cref="AppDomain"/>'s containing assemblies. If the types are not cached; a full scan will be performed.</summary>
        /// <remarks>If <seealso cref="AllTypes"/> has been cached, this only filters the cached types.</remarks>
        public IEnumerable<Type> AllInterfaces => allInterfaces.Value;
        /// <summary>Gets all the static classes that are defined in this <seealso cref="AppDomain"/>'s containing assemblies. If the types are not cached; a full scan will be performed.</summary>
        /// <remarks>If <seealso cref="AllTypes"/> has been cached, this only filters the cached types.</remarks>
        public IEnumerable<Type> AllStaticClasses => allStaticClasses.Value;

        /// <summary>Gets the <seealso cref="AppDomain"/> instance for which this instance holds cache.</summary>
        public AppDomain Domain { get; }

        /// <summary>Initializes a new instance of the <seealso cref="AppDomainCache"/> class from an <seealso cref="AppDomain"/>.</summary>
        /// <param name="domain">The <seealso cref="AppDomain"/> instance for which this instance will hold cache.</param>
        public AppDomainCache(AppDomain domain)
        {
            Domain = domain;

            allTypes = new(Domain.GetAllTypes().ToArray);
            allClasses = NewLazyAllTypeRetriever(TypePredicates.IsClass);
            allAbstractClasses = NewLazyAllTypeRetriever(TypePredicates.IsAbstractClass);
            allNonAbstractClasses = NewLazyAllTypeRetriever(TypePredicates.IsNonAbstractClass);
            allStructs = NewLazyAllTypeRetriever(TypePredicates.IsValueType);
            allInterfaces = NewLazyAllTypeRetriever(TypePredicates.IsInterface);
            allStaticClasses = NewLazyAllTypeRetriever(TypePredicates.IsStatic);
        }

        /// <summary>Empties the cached types in <seealso cref="AllTypes"/>.</summary>
        public void EmptyAllTypesCache() => allTypes.DestroyValue();
        /// <summary>Empties the cached types in <seealso cref="AllClasses"/>.</summary>
        public void EmptyAllClassesCache() => allClasses.DestroyValue();
        /// <summary>Empties the cached types in <seealso cref="AllAbstractClasses"/>.</summary>
        public void EmptyAllAbstractClassesCache() => allAbstractClasses.DestroyValue();
        /// <summary>Empties the cached types in <seealso cref="AllNonAbstractClasses"/>.</summary>
        public void EmptyAllNonAbstractClassesCache() => allNonAbstractClasses.DestroyValue();
        /// <summary>Empties the cached types in <seealso cref="AllStructs"/>.</summary>
        public void EmptyAllStructsCache() => allStructs.DestroyValue();
        /// <summary>Empties the cached types in <seealso cref="AllInterfaces"/>.</summary>
        public void EmptyAllInterfacesCache() => allInterfaces.DestroyValue();
        /// <summary>Empties the cached types in <seealso cref="AllStaticClasses"/>.</summary>
        public void EmptyAllStaticClassesCache() => allStaticClasses.DestroyValue();

        private AdvancedLazy<Type[]> NewLazyAllTypeRetriever(Predicate<Type> predicate) => new(AllTypeRetriever(predicate));
        private Func<Type[]> AllTypeRetriever(Predicate<Type> predicate)
        {
            if (allTypes.IsValueCreated)
                return allTypes.Value.WherePredicate(predicate).ToArray;

            return Domain.GetAllTypes(predicate).ToArray;
        }
    }
}
