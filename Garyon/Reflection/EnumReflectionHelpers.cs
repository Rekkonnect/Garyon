using Garyon.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Garyon.Reflection;

/// <summary>Provides reflection helpers for enum types.</summary>
public static class EnumReflectionHelpers
{
    /// <summary>Constructs a <seealso cref="Dictionary{TKey, TValue}"/> mapping the provided enum's fields to their descriptions, as provided through their <seealso cref="DescriptionAttribute"/>s.</summary>
    /// <typeparam name="T">The type of the enum whose fields to construct the dictionary of.</typeparam>
    /// <returns>The resulting constructed <seealso cref="Dictionary{TKey, TValue}"/> containing the enum fields mapped to their descriptions.</returns>
    public static Dictionary<T, string> GetEnumFieldDescriptionDictionary<T>()
        where T : struct, Enum
    {
        return GetEnumFieldDictionary<T, DescriptionAttribute, string>(d => d?.Description);
    }
    /// <summary>Constructs a <seealso cref="Dictionary{TKey, TValue}"/> mapping the provided enum's fields to a sought attribute.</summary>
    /// <typeparam name="TEnum">The type of the enum whose fields to construct the dictionary of.</typeparam>
    /// <typeparam name="TAttribute">The type of the attribute to seek for in each field.</typeparam>
    /// <returns>The resulting constructed <seealso cref="Dictionary{TKey, TValue}"/> containing the enum fields mapped to their chosen sought attributes.</returns>
    public static Dictionary<TEnum, TAttribute> GetEnumFieldDictionary<TEnum, TAttribute>()
        where TEnum : struct, Enum
        where TAttribute : Attribute
    {
        return GetEnumFieldDictionary<TEnum, TAttribute, TAttribute>(Selectors.SelfObjectReturner);
    }
    /// <summary>Constructs a <seealso cref="Dictionary{TKey, TValue}"/> mapping the provided enum's fields to a value provided from a sought attribute.</summary>
    /// <typeparam name="TEnum">The type of the enum whose fields to construct the dictionary of.</typeparam>
    /// <typeparam name="TAttribute">The type of the attribute to seek for in each field.</typeparam>
    /// <typeparam name="TMappedValue">The type of the value to map the fields to.</typeparam>
    /// <param name="selector">The function that selects the value to map the respective field to, given its attribute instance, if found. It should expect nulls.</param>
    /// <returns>The resulting constructed <seealso cref="Dictionary{TKey, TValue}"/> containing the enum fields mapped to their chosen values.</returns>
    public static Dictionary<TEnum, TMappedValue> GetEnumFieldDictionary<TEnum, TAttribute, TMappedValue>(Func<TAttribute?, TMappedValue> selector)
        where TEnum : struct, Enum
        where TAttribute : Attribute
    {
        var dictionary = new Dictionary<TEnum, TMappedValue>();
        var fields = GetEnumFields<TEnum>();
        foreach (var f in fields)
        {
            var v = f.GetRawConstantValue();
            var d = selector(f.GetCustomAttribute<TAttribute>());
            dictionary.Add((TEnum)v, d);
        }
        return dictionary;
    }

    /// <summary>Gets all the publicly available constant enum fields.</summary>
    /// <typeparam name="T">The type of the enum whose enum fields to get.</typeparam>
    /// <returns>An array containing the enum fields of the provided type.</returns>
    public static FieldInfo[] GetEnumFields<T>()
        where T : struct, Enum
    {
        return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
    }
}
