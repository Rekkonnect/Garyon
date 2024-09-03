using Garyon.DataStructures;
using Garyon.Exceptions;
using Garyon.Extensions;
using Garyon.Objects;
using Garyon.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Garyon.Functions;

/// <summary>Provides helper functions for enum types.</summary>
public static class EnumHelpers
{
    #region Missing Framework Methods
#if !HAS_GENERIC_ENUM_GETVALUES
    /// <summary>Gets the values of an enum type.</summary>
    /// <typeparam name="T">The type of the enum.</typeparam>
    /// <returns>An array of type <typeparamref name="T"/>[] which contains all the values contained in the <typeparamref name="T"/> enum.</returns>
    public static T[] GetValues<T>()
        where T : struct, Enum
    {
        var values = Enum.GetValues(typeof(T));
        return values as T[]
            ?? values.Cast<T>().ToArray();
    }
#endif

#if !HAS_GENERIC_ENUM_PARSE
    /// <summary>Parses the value of an enum type.</summary>
    /// <typeparam name="T">The type of the enum.</typeparam>
    /// <param name="name">The name of the field in the enum.</param>
    /// <returns>The parsed value cast to the enum type that was specified.</returns>
    public static T Parse<T>(string name)
        where T : struct, Enum
    {
        return (T)Enum.Parse(typeof(T), name);
    }
    /// <summary>Parses the value of an enum type.</summary>
    /// <typeparam name="T">The type of the enum.</typeparam>
    /// <param name="name">The name of the field in the enum.</param>
    /// <param name="ignoreCase">Determines whether to ignore the case of the name to parse.</param>
    /// <returns>The parsed value cast to the enum type that was specified.</returns>
    public static T Parse<T>(string name, bool ignoreCase)
        where T : struct, Enum
    {
        return (T)Enum.Parse(typeof(T), name, ignoreCase);
    }
#endif
    #endregion

    #region Enum Entry Count
    private static readonly TypeValueCounterDictionary enumTypeEntryCounts = new();

    /// <summary>Gets the number of entries defined in the specified enum type.</summary>
    /// <typeparam name="T">The enum type whose entry count to get.</typeparam>
    /// <returns>The number of entries defined in the enum type.</returns>
    /// <remarks>This function has a O(1) time complexity, contrast to <seealso cref="GetEntryCount(Type)"/>, since the count is stored as a runtime constant. Thus, prefer calling this function wherever possible.</remarks>
    public static int GetEntryCount<T>()
        where T : struct, Enum
    {
        return EnumCountRetriever<T>.Count;
    }
    /// <summary>Gets the number of entries defined in the specified enum type.</summary>
    /// <param name="type">The enum type whose entry count to get.</param>
    /// <exception cref="ArgumentException">The provided type is <see langword="null"/>, or not an enum type.</exception>
    /// <returns>The number of entries defined in the enum type.</returns>
    /// <remarks>This function has a O(n) time complexity if the provided type has not been evaluated with either this, or the <seealso cref="GetEntryCount{T}"/> function, otherwise it has a O(1) complexity. Thus, prefer calling the <seealso cref="GetEntryCount{T}"/> function wherever possible.</remarks>
    public static int GetEntryCount(Type type)
    {
        if (!type.IsEnum)
            ThrowHelper.Throw<ArgumentException>("The provided type must be an enum type.");

        bool contained = enumTypeEntryCounts.TryGetValue(type, out int count);
        if (!contained)
        {
            count = Enum.GetValues(type).Length;
            enumTypeEntryCounts[type] = count;
        }

        return count;
    }

    /// <summary>Discovers all enum types from all the loaded assemblies, using <seealso cref="AppDomainCache.Current"/>, and statically registers their entry counts.</summary>
    /// <remarks>Ideally, this should only be called once per execution.</remarks>
    public static void RegisterEntryCountsGlobally()
    {
        RegisterEntryCounts(AppDomainCache.Current.AllTypes);
    }
    /// <summary>Discovers all enum types from the specified assembly, and statically registers their entry counts.</summary>
    /// <param name="assembly">The assembly whose enum types to discover.</param>
    /// <remarks>Ideally, this should only be called once per assembly.</remarks>
    public static void RegisterEntryCounts(Assembly assembly)
    {
        RegisterEntryCounts(assembly.GetTypes());
    }

    /// <summary>Registers the entry counts for the enum types from the given types.</summary>
    /// <param name="types">The collection of types, from which the enum types' entry counts will be registered.</param>
    /// <remarks>Ideally, this should only be called once per type in the collection.</remarks>
    public static void RegisterEntryCounts(IEnumerable<Type> types)
    {
        types.Where(t => t.IsEnum).ForEach(t => GetEntryCount(t));
    }

    private static class EnumCountRetriever<T>
        where T : struct, Enum
    {
        public static readonly int Count = Enum.GetValues(typeof(T)).Length;

        static EnumCountRetriever()
        {
            enumTypeEntryCounts.SetValue<T>(Count);
        }
    }
    #endregion

    #region Contained Values
    private static readonly FlexibleDictionary<Type, Type> enumUnderlyingTypeCodeDictionary = new();

    // Partial documentation is not supported
#warning Tracking issue: https://github.com/dotnet/csharplang/discussions/295
#if HAS_GENERIC_ENUM_ISDEFINED
    /// <summary>Determines whether a value is defined in the enum <typeparamref name="TEnum"/>.</summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <typeparam name="TUnderlying">The underlying type of the enum, which is the type of the value instances.</typeparam>
    /// <param name="value">The integral value to determine whether it's defined in an enum.</param>
    /// <returns><see langword="true"/> if <typeparamref name="TEnum"/> has a definition assigned to the specified integral value, otherwise <see langword="false"/>.</returns>
    /// <remarks>The function is based on <seealso cref="Enum.IsDefined{TEnum}(TEnum)"/>.</remarks>
#else
    /// <summary>Determines whether a value is defined in the enum <typeparamref name="TEnum"/>.</summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <typeparam name="TUnderlying">The underlying type of the enum, which is the type of the value instances.</typeparam>
    /// <param name="value">The integral value to determine whether it's defined in an enum.</param>
    /// <returns><see langword="true"/> if <typeparamref name="TEnum"/> has a definition assigned to the specified integral value, otherwise <see langword="false"/>.</returns>
    /// <remarks>The function is based on <seealso cref="Enum.IsDefined(Type, object)"/>.</remarks>
#endif
    public static unsafe bool IsDefined<TEnum, TUnderlying>(TUnderlying value)
        where TEnum : unmanaged, Enum
        where TUnderlying : unmanaged
    {
        if (!IsEnumOfType<TEnum, TUnderlying>())
            return false;

#if HAS_GENERIC_ENUM_ISDEFINED
        return Enum.IsDefined(*(TEnum*)&value);
#else
        return Enum.IsDefined(typeof(TEnum), value);
#endif
    }
    /// <summary>Determines whether <typeparamref name="TEnum"/>'s underlying value type is <typeparamref name="TUnderlying"/>.</summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <typeparam name="TUnderlying">The underlying type of the enum, which is the type of the value instances.</typeparam>
    /// <returns><see langword="true"/> if <typeparamref name="TEnum"/> stores values of type <typeparamref name="TUnderlying"/>, otherwise <see langword="false"/>.</returns>
    public static bool IsEnumOfType<TEnum, TUnderlying>()
        where TEnum : unmanaged, Enum
        where TUnderlying : unmanaged
    {
        return EnumUnderlyingTypeRetriever<TEnum>.UnderlyingType == typeof(TUnderlying);
    }

    private static class EnumUnderlyingTypeRetriever<T>
        where T : struct, Enum
    {
        public static readonly Type UnderlyingType = Enum.GetUnderlyingType(typeof(T));

        static EnumUnderlyingTypeRetriever()
        {
            enumUnderlyingTypeCodeDictionary[typeof(T)] = UnderlyingType;
        }
    }
#endregion
}
