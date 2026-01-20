using System;

namespace Garyon.Functions;

/// <summary>
/// Contains extensions available to the <see cref="Enum"/> type, using the
/// underlying helpers from <see cref="EnumHelpers"/>.
/// </summary>
public static class EnumHelpersExtensions
{
    extension(Enum)
    {
#if !HAS_GENERIC_ENUM_GETVALUES
        /// <summary>
        /// Gets all the values defined in the enum.
        /// </summary>
        public static T[] GetValues<T>()
            where T : unmanaged, Enum
        {
            return EnumHelpers.GetValues<T>();
        }
#endif
    }
}
