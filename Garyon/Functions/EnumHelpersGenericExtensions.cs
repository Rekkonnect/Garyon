using System;

namespace Garyon.Functions;

/// <summary>
/// Contains extensions available to all defined enum types, using the
/// underlying helpers from <see cref="EnumHelpers"/>.
/// </summary>
public static class EnumHelpersGenericExtensions
{
    extension<T>(T)
        where T : unmanaged, Enum
    {
        /// <summary>
        /// Gets the number of entries defined in the specified enum type.
        /// </summary>
        public static int GetEntryCount()
        {
            return EnumHelpers.GetEntryCount<T>();
        }

        /// <summary>
        /// Gets all the values defined in the enum.
        /// </summary>
        public static T[] GetValues()
        {
            return Enum.GetValues<T>();
        }

        /// <summary>
        /// Parses the given name as an enum value.
        /// </summary>
        public static T Parse(string name)
        {
#if HAS_GENERIC_ENUM_PARSE
            return Enum.Parse<T>(name);
#else
            return EnumHelpers.Parse<T>(name);
#endif
        }

        /// <summary>
        /// Parses the given name as an enum value, optionally ignoring the
        /// case.
        /// </summary>
        public static T Parse(string name, bool ignoreCase)
        {
#if HAS_GENERIC_ENUM_PARSE
            return Enum.Parse<T>(name, ignoreCase);
#else
            return EnumHelpers.Parse<T>(name, ignoreCase);
#endif
        }

#if HAS_ENUM_PARSE_SPANSTRING
        /// <summary>Parses the given name as an enum value.</summary>
        public static T Parse(ReadOnlySpan<char> name)
        {
            return Enum.Parse<T>(name);
        }

        /// <summary>Parses the given name as an enum value, optionally ignoring the case.</summary>
        public static T Parse(ReadOnlySpan<char> name, bool ignoreCase)
        {
            return Enum.Parse<T>(name, ignoreCase);
        }
#endif

        /// <summary>
        /// Determines whether the enum type has the specified underlying type.
        /// </summary>
        public static bool HasUnderlyingType<TUnderlying>()
            where TUnderlying : unmanaged
        {
            return EnumHelpers.IsEnumOfType<T, TUnderlying>();
        }
    }
}
