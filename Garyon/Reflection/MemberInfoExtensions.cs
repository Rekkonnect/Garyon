using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Garyon.Reflection;

/// <summary>Contains extension methods for the <seealso cref="MemberInfo"/> class.</summary>
public static class MemberInfoExtensions
{
    extension(MemberInfo member)
    {
        /// <summary>Gets the arity of the member represented by the given <seealso cref="MemberInfo"/> instance.</summary>
        /// <returns>The arity of the member, if it is either a <seealso cref="MethodInfo"/> or a <seealso cref="Type"/>. Otherwise, returns 0.</returns>
        public int Arity => member switch
        {
            MethodInfo method => method.GetGenericArguments().Length,
            TypeInfo type => type.GetGenericArguments().Length,
            _ => 0,
        };

        /// <summary>
        /// Gets the member type of the given <seealso cref="MemberInfo"/>
        /// instance.
        /// </summary>
        /// <returns>
        /// If the given <seealso cref="MemberInfo"/> is a <seealso cref="Type"/>,
        /// the type itself is returned. Otherwise, depending on the type of
        /// the <seealso cref="MemberInfo"/> instance, the following are returned:
        /// <list type="bullet">
        /// <item><seealso cref="FieldInfo.FieldType"/></item>
        /// <item><seealso cref="PropertyInfo.PropertyType"/></item>
        /// <item><seealso cref="EventInfo.EventHandlerType"/></item>
        /// <item><seealso cref="MethodInfo.ReturnType"/></item>
        /// </list>
        /// </returns>
        public Type? MemberType => member switch
        {
            FieldInfo @field => @field.FieldType,
            PropertyInfo property => property.PropertyType,
            EventInfo @event => @event.EventHandlerType,
            MethodInfo method => method.ReturnType,
            Type type => type,
            _ => null,
        };

        /// <inheritdoc cref="HasCustomAttribute(MemberInfo, Type, out Attribute)"/>
        public bool HasCustomAttribute(Type attributeType)
        {
            return member.HasCustomAttribute(attributeType, out _);
        }

        /// <summary>Determines whether a member has a custom attribute of the specified type.</summary>
        /// <param name="attributeType">The type of the attribute to find.</param>
        /// <param name="firstInstance">The first instance of the attribute that is found, if any, otherwise <see langword="null"/>.</param>
        /// <returns>A value indicating whether the requested attribute was found.</returns>
        public bool HasCustomAttribute(
            Type attributeType,
            out Attribute? firstInstance)
        {
            firstInstance = member.GetCustomAttribute(attributeType);
            return firstInstance is not null;
        }

        /// <summary>Determines whether a member has custom attributes of the specified type.</summary>
        /// <param name="attributeType">The type of the attribute to find.</param>
        /// <param name="instances">A collection of instances of the attributes that were found. If no attributes were found, the collection is empty.</param>
        /// <returns>A value indicating whether the requested attribute was found at least once.</returns>
        public bool HasCustomAttributes(
            Type attributeType,
            out IEnumerable<Attribute> instances)
        {
            instances = member.GetCustomAttributes(attributeType);
            return instances.Any();
        }

        /// <inheritdoc cref="HasCustomAttribute{T}(MemberInfo, out T)"/>
        public bool HasCustomAttribute<T>()
            where T : Attribute
        {
            return member.HasCustomAttribute<T>(out _);
        }

        /// <summary>Determines whether a member has a custom attribute of the specified type.</summary>
        /// <typeparam name="T">The type of the attribute to find.</typeparam>
        /// <param name="firstInstance">The first instance of the attribute that is found, if any, otherwise <see langword="null"/>.</param>
        /// <returns>A value indicating whether the requested attribute was found.</returns>
        public bool HasCustomAttribute<T>(out T? firstInstance)
            where T : Attribute
        {
            firstInstance = member.GetCustomAttribute<T>();
            return firstInstance is not null;
        }
        /// <summary>Determines whether a member has custom attributes of the specified type.</summary>
        /// <typeparam name="T">The type of the attributes to find.</typeparam>
        /// <param name="instances">A collection of instances of the attributes that were found. If no attributes were found, the collection is empty.</param>
        /// <returns>A value indicating whether the requested attribute was found at least once.</returns>
        public bool HasCustomAttributes<T>(out IEnumerable<T> instances)
            where T : Attribute
        {
            instances = member.GetCustomAttributes<T>();
            return instances.Any();
        }

        public MemberInfo? GetDeclaringMember()
        {
            return member switch
            {
                Type type => DetermineDirectDeclaringMember(type.GetDeclaringMethodSafe(), type.DeclaringType),
                // Currently MethodBase does not contain information about being contained in a local method
                MethodBase method => method.DeclaringType,

                _ => member.DeclaringType,
            };
        }

        /// <summary>Gets the value of a field or property <seealso cref="MemberInfo"/>.</summary>
        /// <param name="instance">The instance on which to get the value of the field or property, or <see langword="null"/> if it's a <see langword="static"/> one.</param>
        /// <returns>The value returned by the field or property on the given instance.</returns>
        /// <exception cref="InvalidOperationException">The given <seealso cref="MemberInfo"/> is not a <seealso cref="FieldInfo"/> or a <seealso cref="PropertyInfo"/>.</exception>
        public object? GetFieldOrPropertyValue(object? instance)
        {
            return member switch
            {
                FieldInfo field => field.GetValue(instance),
                PropertyInfo property => property.GetValue(instance),
                _ => throw new InvalidOperationException("The member is not a field or a property."),
            };
        }
    }

    // This logic does not seem right, reiterate
    private static MemberInfo? DetermineDirectDeclaringMember(MethodBase? declaringMethod, Type? declaringType)
    {
        if (declaringMethod is null && declaringType is null)
            return null;

        // Should never be the case
        if (declaringType is null)
            return declaringMethod;

        if (declaringMethod is null)
            return declaringType;

        if (declaringMethod.DeclaringType == declaringType)
            return declaringMethod;

        return declaringType;
    }
}
