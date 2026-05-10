using Garyon.Reflection;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Reflection;

public class ReflectionHelperAdditionalTests
{
    [Test]
    public async Task ConstructorAndMethodInfoExtensionsInvokeTypedMembersTest()
    {
        var defaultCtor = typeof(ReflectionTarget).GetConstructor(Type.EmptyTypes)!;
        var valueCtor = typeof(ReflectionTarget).GetConstructor([typeof(int)])!;
        var staticMethod = typeof(ReflectionTarget).GetMethod(nameof(ReflectionTarget.Add), BindingFlags.Public | BindingFlags.Static)!;
        var instanceMethod = typeof(ReflectionTarget).GetMethod(nameof(ReflectionTarget.Scale), BindingFlags.Public | BindingFlags.Instance)!;

        var defaultInstance = defaultCtor.InitializeInstance<ReflectionTarget>();
        var valueInstance = valueCtor.InitializeInstance<ReflectionTarget>(5);
        var add = staticMethod.CreateDelegate<Func<int, int, int>>();
        var scale = instanceMethod.CreateDelegate<Func<int, int>>(valueInstance);

        await Assert.That(defaultInstance.Value).IsEqualTo(1);
        await Assert.That(valueInstance.Value).IsEqualTo(5);
        await Assert.That(add(2, 3)).IsEqualTo(5);
        await Assert.That(scale(3)).IsEqualTo(15);
        await Assert.That(staticMethod.Invoke<int>(null, [4, 6])).IsEqualTo(10);
    }

    [Test]
    public async Task FieldAndMemberInfoExtensionsClassifyAndReadMembersTest()
    {
        var intPtrField = typeof(ReflectionTarget).GetField(nameof(ReflectionTarget.PointerField))!;
        var delegateField = typeof(ReflectionTarget).GetField(nameof(ReflectionTarget.ActionField))!;
        var valueField = typeof(ReflectionTarget).GetField(nameof(ReflectionTarget.Value))!;
        var property = typeof(ReflectionTarget).GetProperty(nameof(ReflectionTarget.DoubleValue))!;
        var @event = typeof(ReflectionTarget).GetEvent(nameof(ReflectionTarget.Changed))!;
        var genericMethod = typeof(ReflectionTarget).GetMethod(nameof(ReflectionTarget.GenericMethod))!;
        var instance = new ReflectionTarget(9);

        await Assert.That(intPtrField.GetFieldInvokableTypeKind()).IsEqualTo(InvokableTypeKind.FunctionPointer);
        await Assert.That(delegateField.GetFieldInvokableTypeKind()).IsEqualTo(InvokableTypeKind.Delegate);
        await Assert.That(valueField.GetFieldInvokableTypeKind()).IsEqualTo(InvokableTypeKind.Invalid);
        await Assert.That(genericMethod.Arity).IsEqualTo(1);
        await Assert.That(typeof(GenericTarget<>).GetTypeInfo().Arity).IsEqualTo(1);
        await Assert.That(Garyon.Reflection.MemberInfoExtensions.get_MemberType(valueField)).IsEqualTo(typeof(int));
        await Assert.That(Garyon.Reflection.MemberInfoExtensions.get_MemberType(property)).IsEqualTo(typeof(int));
        await Assert.That(Garyon.Reflection.MemberInfoExtensions.get_MemberType(@event)).IsEqualTo(typeof(EventHandler));
        await Assert.That(Garyon.Reflection.MemberInfoExtensions.get_MemberType(typeof(ReflectionTarget))).IsEqualTo(typeof(ReflectionTarget));
        await Assert.That(valueField.GetFieldOrPropertyValue(instance)).IsEqualTo(9);
        await Assert.That(property.GetFieldOrPropertyValue(instance)).IsEqualTo(18);
        await Assert.That(genericMethod.GetDeclaringMember()).IsEqualTo(typeof(ReflectionTarget));
        Assert.Throws<InvalidOperationException>(() => typeof(ReflectionTarget).GetFieldOrPropertyValue(instance));
    }

    [Test]
    public async Task MemberInfoAttributeHelpersReturnTypedAndUntypedAttributesTest()
    {
        var type = typeof(AttributedTarget);
        var field = type.GetField(nameof(AttributedTarget.Value))!;

        await Assert.That(type.HasCustomAttribute<DescriptionAttribute>()).IsTrue();
        await Assert.That(type.HasCustomAttribute(typeof(DescriptionAttribute))).IsTrue();
        await Assert.That(type.HasCustomAttribute<DescriptionAttribute>(out var typed)).IsTrue();
        await Assert.That(typed!.Description).IsEqualTo("target");
        await Assert.That(type.HasCustomAttribute(typeof(DescriptionAttribute), out var untyped)).IsTrue();
        await Assert.That(((DescriptionAttribute)untyped!).Description).IsEqualTo("target");
        await Assert.That(field.HasCustomAttributes<DescriptionAttribute>(out var typedAttributes)).IsTrue();
        await Assert.That(typedAttributes.Single().Description).IsEqualTo("field");
        await Assert.That(field.HasCustomAttributes(typeof(DescriptionAttribute), out var untypedAttributes)).IsTrue();
        await Assert.That(untypedAttributes.Cast<DescriptionAttribute>().Single().Description).IsEqualTo("field");
        await Assert.That(field.HasCustomAttribute<ObsoleteAttribute>()).IsFalse();
    }

    [Test]
    public async Task DelegateAndEnumReflectionHelpersExposeMetadataTest()
    {
        var parameterTypes = typeof(Func<int, string, bool>).GetDelegateParameterTypes();
        var genericParameterTypes = DelegateTypeExtensions.GetDelegateParameterTypes<Action<int, string>>();
        var descriptions = EnumReflectionHelpers.GetEnumFieldDescriptionDictionary<DescribedEnum>();
        var attributes = EnumReflectionHelpers.GetEnumFieldDictionary<DescribedEnum, DescriptionAttribute>();
        var selected = EnumReflectionHelpers.GetEnumFieldDictionary<DescribedEnum, DescriptionAttribute, string>(d => d?.Description.ToUpperInvariant());
        var fields = EnumReflectionHelpers.GetEnumFields<DescribedEnum>();

        await Assert.That(parameterTypes.SequenceEqual([typeof(int), typeof(string)])).IsTrue();
        await Assert.That(genericParameterTypes.SequenceEqual([typeof(int), typeof(string)])).IsTrue();
        await Assert.That(descriptions[DescribedEnum.First]).IsEqualTo("first");
        await Assert.That(attributes[DescribedEnum.Second].Description).IsEqualTo("second");
        await Assert.That(selected[DescribedEnum.First]).IsEqualTo("FIRST");
        await Assert.That(fields.Select(f => f.Name).SequenceEqual([nameof(DescribedEnum.First), nameof(DescribedEnum.Second)])).IsTrue();
        Assert.Throws<ArgumentException>(() => typeof(string).GetDelegateParameterTypes());
    }

    [Test]
    public async Task AttributeMappingFluentBuilderSupportsAttributeMappingsTest()
    {
        var attributeMapping = AttributeMapping
            .ForEnum<ConfiguredEnum>()
            .WithAttribute<DisplayMetadataAttribute>()
            .Build();

        await Assert.That(attributeMapping[ConfiguredEnum.First].Name).IsEqualTo("first");
    }

    [Test]
    public async Task AttributeMappingFluentBuilderSupportsSelectedKeyMappingsTest()
    {
        var selectedKeyMapping = AttributeMapping
            .ForEnum<ConfiguredEnum>()
            .WithAttributeKey<DisplayMetadataAttribute>(static a => a.Name)
            .Build();

        await Assert.That((string)selectedKeyMapping[ConfiguredEnum.Second]).IsEqualTo("second");
    }

    [Test]
    public async Task AttributeMappingFluentBuilderSupportsFilteredMappingsTest()
    {
        var filteredMapping = AttributeMapping
            .ForEnum<ConfiguredEnum>()
            .Where(static s => s is not ConfiguredEnum.None)
            .WithAttributeKey<DisplayMetadataAttribute>(static a => a.Name)
            .Build();

        await Assert.That(filteredMapping.ContainsKey(ConfiguredEnum.None)).IsFalse();
    }

    [Test]
    public async Task AttributeMappingFluentBuilderSupportsCompositeKeyMappingsTest()
    {
        var compositeKeyMapping = AttributeMapping
            .ForEnum<ConfiguredEnum>()
            .WithAttributeKey<DisplayMetadataAttribute, (string Name, int Order)>(static a => (a.Name, a.Order))
            .Build();

        await Assert.That(compositeKeyMapping[ConfiguredEnum.Second].Name).IsEqualTo("second");
        await Assert.That(compositeKeyMapping[ConfiguredEnum.Second].Order).IsEqualTo(2);
    }

    [Test]
    public async Task TypePredicatesClassifyCommonTypeKindsTest()
    {
        await Assert.That(TypePredicates.IsClass(typeof(ReflectionTarget))).IsTrue();
        await Assert.That(TypePredicates.IsAbstractClass(typeof(AbstractTarget))).IsTrue();
        await Assert.That(TypePredicates.IsNonAbstractClass(typeof(ReflectionTarget))).IsTrue();
        await Assert.That(TypePredicates.IsValueType(typeof(int))).IsTrue();
        await Assert.That(TypePredicates.IsInterface(typeof(IDisposable))).IsTrue();
        await Assert.That(TypePredicates.IsStatic(typeof(StaticTarget))).IsTrue();
    }

    [Test]
    public async Task TypeListCacheExposesFilteredViewsTest()
    {
        Type[] cachedTypes = [typeof(ReflectionTarget), typeof(AbstractTarget), typeof(StaticTarget), typeof(IDisposable), typeof(int)];
        var cache = new TypeListCache(cachedTypes);

        await Assert.That(cache.SequenceEqual(cachedTypes)).IsTrue();
        await Assert.That(cache.Types.SequenceEqual(cachedTypes)).IsTrue();
        await Assert.That(cache.GetClasses().SequenceEqual([typeof(ReflectionTarget), typeof(AbstractTarget), typeof(StaticTarget)])).IsTrue();
        await Assert.That(cache.GetAbstractClasses().SequenceEqual([typeof(AbstractTarget), typeof(StaticTarget)])).IsTrue();
        await Assert.That(cache.GetNonAbstractClasses().SequenceEqual([typeof(ReflectionTarget)])).IsTrue();
        await Assert.That(cache.GetStructs().SequenceEqual([typeof(int)])).IsTrue();
        await Assert.That(cache.GetInterfaces().SequenceEqual([typeof(IDisposable)])).IsTrue();
        await Assert.That(cache.GetStaticClasses().SequenceEqual([typeof(StaticTarget)])).IsTrue();
        await Assert.That(cache.GetFilteredTypes(TypePredicates.IsClass).SequenceEqual(cache.GetClasses())).IsTrue();
    }

    private sealed class ReflectionTarget
    {
        public IntPtr PointerField;
        public Action? ActionField;
        public int Value;
        public int DoubleValue => Value * 2;
        public event EventHandler? Changed;

        public ReflectionTarget()
        {
            Value = 1;
        }

        public ReflectionTarget(int value)
        {
            Value = value;
        }

        public static int Add(int left, int right) => left + right;
        public int Scale(int multiplier) => Value * multiplier;
        public T GenericMethod<T>(T value) => value;
    }

    [Description("target")]
    private sealed class AttributedTarget
    {
        [Description("field")]
        public int Value;
    }

    private sealed class GenericTarget<T>;
    private abstract class AbstractTarget;
    private static class StaticTarget;

    private enum DescribedEnum
    {
        [Description("first")]
        First,
        [Description("second")]
        Second,
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    private sealed class DisplayMetadataAttribute : Attribute
    {
        public string Name { get; }
        public int Order { get; }

        public DisplayMetadataAttribute(string name, int order)
        {
            Name = name;
            Order = order;
        }
    }

    private enum ConfiguredEnum
    {
        [DisplayMetadata("none", 0)]
        None,

        [DisplayMetadata("first", 1)]
        First,

        [DisplayMetadata("second", 2)]
        Second,
    }
}
