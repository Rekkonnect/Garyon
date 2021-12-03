using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Garyon.Reflection
{
    /// <summary>Contains extension methods for the <seealso cref="MemberInfo"/> class.</summary>
    public static class MemberInfoExtensions
    {
        #region MemberInfo
        /// <inheritdoc cref="HasCustomAttribute{T}(MemberInfo, out T)"/>
        public static bool HasCustomAttribute<T>(this MemberInfo member)
            where T : Attribute
        {
            return member.HasCustomAttribute<T>(out _);
        }
        /// <summary>Determines whether a member has a custom attribute of the specified type.</summary>
        /// <typeparam name="T">The type of the attribute to find.</typeparam>
        /// <param name="member">The member whose attribute to find.</param>
        /// <param name="firstInstance">The first instance of the attribute that is found, if any, otherwise <see langword="null"/>.</param>
        /// <returns>A value indicating whether the requested attribute was found.</returns>
        public static bool HasCustomAttribute<T>(this MemberInfo member, out T firstInstance)
            where T : Attribute
        {
            firstInstance = member.GetCustomAttribute<T>();
            return firstInstance != null;
        }
        /// <summary>Determines whether a member has custom attributes of the specified type.</summary>
        /// <typeparam name="T">The type of the attributes to find.</typeparam>
        /// <param name="member">The member whose attributes to find.</param>
        /// <param name="instances">A collection of instances of the attributes that were found. If no attributes were found, the collection is empty.</param>
        /// <returns>A value indicating whether the requested attribute was found at least once.</returns>
        public static bool HasCustomAttributes<T>(this MemberInfo member, out IEnumerable<T> instances)
            where T : Attribute
        {
            instances = member.GetCustomAttributes<T>();
            return instances.Any();
        }
        #endregion

        #region IEnumerable<MemberInfo>
        /// <summary>Maps the provided members' custom attributes to the members that have them.</summary>
        /// <typeparam name="T">The type of the attributes to map.</typeparam>
        /// <param name="members">The members whose attributes to map.</param>
        /// <returns>A dictionary mapping the found instances of the specified attribute to their respective members.</returns>
        public static Dictionary<T, MemberInfo> MapCustomAttributesToMembers<T>(this IEnumerable<MemberInfo> members)
            where T : Attribute
        {
            return MapCustomAttributesToMembers<T, MemberInfo>(members);
        }
        /// <summary>Maps the provided members' custom attributes to the members that have them.</summary>
        /// <typeparam name="TAttribute">The type of the attributes to map.</typeparam>
        /// <typeparam name="TMemberInfo">The type of the members.</typeparam>
        /// <param name="members">The members whose attributes to map.</param>
        /// <returns>A dictionary mapping the found instances of the specified attribute to their respective members.</returns>
        public static Dictionary<TAttribute, TMemberInfo> MapCustomAttributesToMembers<TAttribute, TMemberInfo>(this IEnumerable<TMemberInfo> members)
            where TAttribute : Attribute
            where TMemberInfo : MemberInfo
        {
            var result = new Dictionary<TAttribute, TMemberInfo>();
            foreach (var m in members)
                if (m.HasCustomAttributes<TAttribute>(out var attributes))
                    foreach (var a in attributes)
                        result.Add(a, m);
            return result;
        }
        /// <summary>Maps the provided members' custom attributes to the members that have them, with the keys being the keys of the attributes based on the provided selector.</summary>
        /// <typeparam name="TAttribute">The type of the attributes to map.</typeparam>
        /// <typeparam name="TResult">The resulting type of the keys in the dictionary.</typeparam>
        /// <param name="members">The members whose attributes to map.</param>
        /// <param name="selector">The selector that returns an object of the resulting key type from the specified attribute.</param>
        /// <returns>A dictionary mapping the keys of the found instances of the specified attribute to their respective members.</returns>
        public static Dictionary<TResult, MemberInfo> MapCustomAttributeKeysToMembers<TAttribute, TResult>(this IEnumerable<MemberInfo> members, Func<TAttribute, TResult> selector)
            where TAttribute : Attribute
        {
            return MapCustomAttributeKeysToMembers<TAttribute, TResult, MemberInfo>(members, selector);
        }
        /// <summary>Maps the provided members' custom attributes to the members that have them, with the keys being the keys of the attributes based on the provided selector.</summary>
        /// <typeparam name="TAttribute">The type of the attributes to map.</typeparam>
        /// <typeparam name="TResult">The resulting type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TMemberInfo">The type of the members.</typeparam>
        /// <param name="members">The members whose attributes to map.</param>
        /// <param name="selector">The selector that returns an object of the resulting key type from the specified attribute.</param>
        /// <returns>A dictionary mapping the keys of the found instances of the specified attribute to their respective members.</returns>
        public static Dictionary<TResult, TMemberInfo> MapCustomAttributeKeysToMembers<TAttribute, TResult, TMemberInfo>(this IEnumerable<TMemberInfo> members, Func<TAttribute, TResult> selector)
            where TAttribute : Attribute
            where TMemberInfo : MemberInfo
        {
            var result = new Dictionary<TResult, TMemberInfo>();
            foreach (var m in members)
                if (m.HasCustomAttributes<TAttribute>(out var attributes))
                    foreach (var a in attributes)
                        result.Add(selector(a), m);
            return result;
        }
        #endregion
    }
}
