using Garyon.Functions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Garyon.Reflection;

/// <summary>
/// Provides fluent builders for constructing enum-to-attribute mappings.
/// </summary>
public static class AttributeMapping
{
    /// <summary>
    /// Creates a fluent builder for the provided enum type.
    /// </summary>
    /// <typeparam name="TEnum">
    /// The type of the enum whose fields to map.
    /// </typeparam>
    public static EnumAttributeMappingBuilder<TEnum> ForEnum<TEnum>()
        where TEnum : struct, Enum
    {
        return new(Predicates.True);
    }
}

/// <summary>
/// Represents a fluent builder for configuring enum attribute mappings.
/// </summary>
/// <typeparam name="TEnum">
/// The type of the enum whose fields to map.
/// </typeparam>
public sealed class EnumAttributeMappingBuilder<TEnum>
    where TEnum : struct, Enum
{
    private readonly Func<TEnum, bool> predicate;

    internal EnumAttributeMappingBuilder(Func<TEnum, bool> predicate)
    {
        this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
    }

    /// <summary>
    /// Restricts the enum values that participate in the mapping.
    /// </summary>
    /// <param name="predicate">
    /// The predicate that determines whether a given enum value should be
    /// included.
    /// </param>
    public EnumAttributeMappingBuilder<TEnum> Where(Func<TEnum, bool> predicate)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return new(value => this.predicate(value) && predicate(value));
    }

    /// <summary>
    /// Configures the mapping to use the entire attribute instance as the
    /// mapped value.
    /// </summary>
    /// <typeparam name="TAttribute">
    /// The type of the attribute to retrieve from each enum field.
    /// </typeparam>
    public EnumAttributeMappingBuilder<TEnum, TAttribute, TAttribute> WithAttribute<TAttribute>()
        where TAttribute : Attribute
    {
        return new(predicate, static attribute => attribute);
    }

    /// <summary>
    /// Configures the mapping to use a selected value from the attribute as the
    /// mapped value.
    /// </summary>
    /// <typeparam name="TAttribute">
    /// The type of the attribute to retrieve from each enum field.
    /// </typeparam>
    /// <param name="selector">
    /// The selector that chooses the mapped value from the attribute.
    /// </param>
    public EnumAttributeMappingBuilder<TEnum, TAttribute, object> WithAttributeKey<TAttribute>(Func<TAttribute, object?> selector)
        where TAttribute : Attribute
    {
        return new(predicate, selector ?? throw new ArgumentNullException(nameof(selector)));
    }

    /// <summary>
    /// Configures the mapping to use a selected value from the attribute as the
    /// mapped value.
    /// </summary>
    /// <typeparam name="TAttribute">
    /// The type of the attribute to retrieve from each enum field.
    /// </typeparam>
    /// <typeparam name="TMappedValue">
    /// The type of the mapped value.
    /// </typeparam>
    /// <param name="selector">
    /// The selector that chooses the mapped value from the attribute.
    /// </param>
    public EnumAttributeMappingBuilder<TEnum, TAttribute, TMappedValue> WithAttributeKey<TAttribute, TMappedValue>(Func<TAttribute, TMappedValue?> selector)
        where TAttribute : Attribute
    {
        return new(predicate, selector ?? throw new ArgumentNullException(nameof(selector)));
    }
}

/// <summary>
/// Represents a fluent builder for constructing enum attribute mappings.
/// </summary>
/// <typeparam name="TEnum">
/// The type of the enum whose fields to map.
/// </typeparam>
/// <typeparam name="TAttribute">
/// The type of the attribute to retrieve from each enum field.
/// </typeparam>
/// <typeparam name="TMappedValue">
/// The type of the mapped value.
/// </typeparam>
public sealed class EnumAttributeMappingBuilder<TEnum, TAttribute, TMappedValue>
    where TEnum : struct, Enum
    where TAttribute : Attribute
{
    private readonly Func<TEnum, bool> predicate;
    private readonly Func<TAttribute, TMappedValue?> selector;

    internal EnumAttributeMappingBuilder(Func<TEnum, bool> predicate, Func<TAttribute, TMappedValue?> selector)
    {
        this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        this.selector = selector ?? throw new ArgumentNullException(nameof(selector));
    }

    /// <summary>
    /// Restricts the enum values that participate in the mapping.
    /// </summary>
    /// <param name="predicate">
    /// The predicate that determines whether a given enum value should be
    /// included.
    /// </param>
    public EnumAttributeMappingBuilder<TEnum, TAttribute, TMappedValue> Where(Func<TEnum, bool> predicate)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        return new(value => this.predicate(value) && predicate(value), selector);
    }

    /// <summary>
    /// Constructs the configured mapping.
    /// </summary>
    public Dictionary<TEnum, TMappedValue> Build()
    {
        var dictionary = new Dictionary<TEnum, TMappedValue>();
        var fields = EnumReflectionHelpers.GetEnumFields<TEnum>();
        foreach (var field in fields)
        {
            var value = (TEnum)field.GetRawConstantValue()!;
            if (!predicate(value))
            {
                continue;
            }

            var attribute = field.GetCustomAttribute<TAttribute>();
            if (attribute is null)
            {
                continue;
            }

            var mappedValue = selector(attribute);
            if (mappedValue is null)
            {
                continue;
            }

            dictionary.Add(value, mappedValue);
        }
        return dictionary;
    }
}
