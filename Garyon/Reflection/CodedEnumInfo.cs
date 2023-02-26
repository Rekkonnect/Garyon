using Garyon.Extensions;
using System;
using System.Collections.Generic;
#if HAS_IMMUTABLE
using System.Collections.Immutable;
#endif
using System.Linq;
using System.Reflection;

namespace Garyon.Reflection;

/// <summary>
/// Contains information for enums with members denoted with
/// <seealso cref="CodeAttribute"/>.
/// </summary>
public static class CodedEnumInfo
{
    /// <summary>
    /// Parses the given code for a given enum type, and returns
    /// the enum field that bears the given code.
    /// </summary>
    /// <param name="enumType">The type of the enum to look into.</param>
    /// <param name="code">The code of the enum field.</param>
    /// <returns>
    /// The value of the enum type that contains the given code.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown when the code is not found in the enum.
    /// </exception>
    public static Enum ParseCode(Type enumType, string code)
    {
        var method = GetCacheType(enumType).GetMethod(nameof(Cache<TypeCode>.Parse))!;
        return method.Invoke(null, new[] { code })! as Enum;
    }
    /// <summary>
    /// Gets the code for the respective enum value.
    /// </summary>
    /// <param name="enumValue">
    /// The enum field whose code to get. Combinations of flag values
    /// are not supported.
    /// </param>
    /// <returns>
    /// The the code that the enum value is mapped to.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown when the enum value does not have a code.
    /// </exception>
    public static string GetCode(Enum enumValue)
    {
        var enumType = enumValue.GetType();
        var method = GetCacheType(enumType).GetMethod(nameof(Cache<TypeCode>.GetCode))!;
        var result = method.Invoke(null, new[] { enumValue }) as string;
        return result!;
    }
    /// <summary>
    /// Parses the given code for a given enum type, and returns
    /// the enum field that bears the given code. If the code is
    /// not found on the enum, the default value of the enum is
    /// returned.
    /// </summary>
    /// <param name="enumType">The type of the enum to look into.</param>
    /// <param name="code">The code of the enum field.</param>
    /// <returns>
    /// The value of the enum type that contains the given code,
    /// or the default value of the enum if the code is not found.
    /// </returns>
    public static Enum ParseCodeOrDefault(Type enumType, string code)
    {
        var defaultValue = enumType.GetDefaultValue();
        var method = GetCacheType(enumType).GetMethod(nameof(Cache<TypeCode>.ParseOrDefault))!;
        return method.Invoke(null, new object[] { code, defaultValue })! as Enum;
    }
    /// <summary>
    /// Parses the given code for a given enum type, and returns
    /// the enum field that bears the given code. If the code is
    /// not found on the enum, the specified default value is returned.
    /// The specified default value implies the type of the enum
    /// whose code to look for.
    /// </summary>
    /// <param name="code">The code of the enum field.</param>
    /// <param name="defaultValue">
    /// The default value to return if the code is not found in the enum.
    /// </param>
    /// <returns>
    /// The value of the enum type that contains the given code,
    /// or the specified default value if the code is not found.
    /// </returns>
    public static Enum ParseCodeOrDefault(string code, Enum defaultValue)
    {
        var enumType = defaultValue.GetType();
        var method = GetCacheType(enumType).GetMethod(nameof(Cache<TypeCode>.ParseOrDefault))!;
        return method.Invoke(null, new object[] { code, defaultValue })! as Enum;
    }
    /// <summary>
    /// Gets the code for the respective enum value. If the value
    /// does not have a code associated with it, the specified
    /// default value is returned.
    /// </summary>
    /// <param name="enumValue">
    /// The enum field whose code to get. Combinations of flag values
    /// are not supported.
    /// </param>
    /// <param name="defaultCode">
    /// The default value to return if the value does not have a
    /// code.
    /// </param>
    /// <returns>
    /// The the code that the enum value is mapped to.
    /// </returns>
    public static string GetCodeOrDefault(Enum enumValue, string defaultCode = null)
    {
        var enumType = enumValue.GetType();
        var method = GetCacheType(enumType).GetMethod(nameof(Cache<TypeCode>.GetCodeOrDefault))!;
        var result = method.Invoke(null, new object[] { enumValue, defaultCode }) as string;
        return result!;
    }

    private static Type GetCacheType(Type enumType)
    {
        if (!enumType.IsEnum)
            throw new InvalidOperationException("The given type must be an enum type.");

        return typeof(Cache<>).MakeGenericType(enumType);
    }
    
    /// <inheritdoc cref="ParseCode(Type, string)"/>
    /// <typeparam name="TEnum">
    /// The type of the enum to look into.
    /// </typeparam>
    public static TEnum ParseCode<TEnum>(string code)
        where TEnum : struct, Enum
    {
        return Cache<TEnum>.Parse(code);
    }
    /// <inheritdoc cref="GetCode(Enum)"/>
    /// <typeparam name="TEnum">
    /// The type of the enum to look into.
    /// </typeparam>
    public static string GetCode<TEnum>(TEnum code)
        where TEnum : struct, Enum
    {
        return Cache<TEnum>.GetCode(code);
    }

    /// <inheritdoc cref="ParseCode(Type, string)"/>
    /// <typeparam name="TEnum">
    /// The type of the enum to look into.
    /// </typeparam>
    public static TEnum ParseCodeOrDefault<TEnum>(string code, TEnum defaultValue)
        where TEnum : struct, Enum
    {
        return Cache<TEnum>.ParseOrDefault(code, defaultValue);
    }
    /// <inheritdoc cref="GetCode(Enum)"/>
    /// <typeparam name="TEnum">
    /// The type of the enum to look into.
    /// </typeparam>
    public static string GetCodeOrDefault<TEnum>(TEnum code, string defaultValue)
        where TEnum : struct, Enum
    {
        return Cache<TEnum>.GetCodeOrDefault(code, defaultValue);
    }

    private static class Cache<TEnum>
        where TEnum : struct, Enum
    {
#if HAS_IMMUTABLE
        private static readonly ImmutableDictionary<TEnum, string> enumToString;
        private static readonly ImmutableDictionary<string, TEnum> stringToEnum;
#else
        private static readonly Dictionary<TEnum, string> enumToString;
        private static readonly Dictionary<string, TEnum> stringToEnum;
#endif

        static Cache()
        {
            var fields = typeof(TEnum).GetFields();
            var enumFields = fields
                .Where(f => f.IsStatic && f.FieldType == typeof(TEnum))
                .ToArray();

#if HAS_IMMUTABLE
            var enumToStringBuilder = ImmutableDictionary.CreateBuilder<TEnum, string>();
            var stringToEnumBuilder = ImmutableDictionary.CreateBuilder<string, TEnum>();
#else
            enumToString = new();
            stringToEnum = new();
#endif
            foreach (var enumField in enumFields)
            {
                var attribute = enumField.GetCustomAttribute<CodeAttribute>();
                if (attribute is null)
                    continue;

                TEnum value = (TEnum)enumField.GetValue(null)!;
                string code = attribute.Code;
#if HAS_IMMUTABLE
                enumToStringBuilder.Add(value, code);
                stringToEnumBuilder.Add(code, value);
#else
                enumToString.Add(value, code);
                stringToEnum.Add(code, value);
#endif
            }

#if HAS_IMMUTABLE
            enumToString = enumToStringBuilder.ToImmutable();
            stringToEnum = stringToEnumBuilder.ToImmutable();
#endif
        }

        public static TEnum Parse(string code)
        {
            return stringToEnum[code];
        }
        public static string GetCode(TEnum value)
        {
            return enumToString[value];
        }

        public static TEnum ParseOrDefault(string code, TEnum defaultValue)
        {
            return stringToEnum.ValueOrDefault(code, defaultValue);
        }
        public static string GetCodeOrDefault(TEnum value, string defaultValue)
        {
            return enumToString.ValueOrDefault(value, defaultValue);
        }
    }
}
