using Garyon.DataStructures.Trees;
using Garyon.QualityControl.Types;
using Garyon.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;
using GaryonTypeExtensions = Garyon.Reflection.TypeExtensions;

namespace Garyon.Tests.Reflection;

public class TypeExtensionsTests : ExampleTypes
{
    private static readonly Type intByRefType;
    private static readonly Type intPointerByRefType;

    static TypeExtensionsTests()
    {
        var parameters = typeof(TypeExtensionsTests)
            .GetMethod(nameof(DummyByRefFunction), BindingFlags.NonPublic | BindingFlags.Static)
            .GetParameters();
        intByRefType = parameters[0].ParameterType;
        intPointerByRefType = parameters[1].ParameterType;
    }

    private static unsafe void DummyByRefFunction(ref int _0, ref int* _1) { }
    private static void GenericMethod<T>() { }

    private static async Task AssertTypeDefinitionInfo<T>(
        TypeDefinitionInfoMapping mapping,
        Func<TypeDefinitionInfo, T> expected,
        Func<Type, T> actual)
    {
        await Assert.That(actual(mapping.Type))
            .IsEqualTo(expected(mapping.Info));
    }

    private sealed class HasConstructors
    {
        public HasConstructors() { }
        public HasConstructors(int _, string __) { }
    }

    private sealed class HasPrivateParameterlessConstructor
    {
        private HasPrivateParameterlessConstructor() { }
    }

    private sealed class GenericContainer<T>
    {
        public void Transform<TValue>(TValue value)
        {
            _ = value;
        }
    }

    #region Inheritance

    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task TypeDefinitionInfoTest(
        TypeDefinitionInfoMapping mapping)
    {
        var info = new TypeDefinitionInfo(mapping.Type);
        await Assert.That(info).IsEqualTo(mapping.Info);
    }

    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task CanInheritTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(
            mapping,
            t => t.CanInherit,
            GaryonTypeExtensions.CanInherit);
    }
    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task CanInheritCustomTypesTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(
            mapping,
            t => t.CanInheritCustomTypes,
            GaryonTypeExtensions.CanInheritCustomTypes);
    }
    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task CanInheritInterfacesTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(
            mapping,
            t => t.CanInheritInterfaces,
            GaryonTypeExtensions.CanInheritInterfaces);
    }
    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task CanInheritClassesTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(
            mapping,
            t => t.CanInheritClasses,
            GaryonTypeExtensions.CanInheritClasses);
    }
    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task CanInheritCustomClassesTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(
            mapping,
            t => t.CanInheritCustomClasses,
            GaryonTypeExtensions.CanInheritCustomClasses);
    }

    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task CanBeInheritedTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(
            mapping,
            t => t.CanBeInherited,
            GaryonTypeExtensions.CanBeInherited);
    }

    [Test]
    public async Task InheritsTest()
    {
        await Assert.That(typeof(object).Inherits<object>()).IsFalse();
        await Assert.That(typeof(ID).Inherits<IA>()).IsTrue();
        await Assert.That(typeof(ID).Inherits<IB>()).IsTrue();
        await Assert.That(typeof(ID).Inherits<ID>()).IsFalse();
        await Assert.That(typeof(CA).Inherits<object>()).IsTrue();
        await Assert.That(typeof(EA).Inherits<Enum>()).IsTrue();
        await Assert.That(typeof(EA).Inherits<ValueType>()).IsTrue();
        await Assert.That(typeof(SA).Inherits<Enum>()).IsFalse();
        await Assert.That(typeof(DA).Inherits<Delegate>()).IsTrue();
    }
    
    [Test]
    public async Task GetInheritanceLevelTest()
    {
        int level = typeof(CD).GetInheritanceLevel();
        await Assert.That(level).IsEqualTo(3);
    }

    [Test]
    public async Task GeneralTypeHelpersCoverageTest()
    {
        var genericPrefix = typeof(Dictionary<int, string>).GenericFullNamePrefixOrSame();
        var typeCode = typeof(int).GetTypeCode();
        var implements = typeof(List<int>).Implements(typeof(IEnumerable<int>));
        var isOrImplements = typeof(IEnumerable<int>).IsOrImplements(typeof(IEnumerable<int>));
        var baseTypes = typeof(CD).EnumerateBaseTypes().ToArray();
        var interfaceTree = typeof(IK).GetInterfaceInheritanceTree();
        var genericVariantInherited = Garyon.Reflection.TypeExtensions.InheritsGenericVariantOf(typeof(List<int>), typeof(IEnumerable<>), out var genericVariantType);
        var classGenericVariantInherited = Garyon.Reflection.TypeExtensions.InheritsGenericVariantOf(typeof(List<int>), typeof(List<>), out var classGenericVariantType);
        var genericDefinitionOrSame = typeof(List<int>).GetGenericTypeDefinitionOrSame();
        var nonGenericDefinitionOrSame = typeof(string).GetGenericTypeDefinitionOrSame();
        var parameterlessConstructor = typeof(HasConstructors).GetParameterlessConstructor();
        var anyAccessibilityParameterlessConstructor = typeof(HasPrivateParameterlessConstructor).GetAnyAccessibilityParameterlessConstructor();
        var intStringConstructor = typeof(HasConstructors).GetConstructor<int, string>();
        var anyAccessibilityConstructor = typeof(HasPrivateParameterlessConstructor).GetAnyAccessibilityConstructor();

        await Assert.That(genericPrefix).IsEqualTo("System.Collections.Generic.Dictionary");
        await Assert.That(typeCode).IsEqualTo(TypeCode.Int32);
        await Assert.That(implements).IsTrue();
        await Assert.That(isOrImplements).IsTrue();
        await Assert.That(baseTypes.SequenceEqual([typeof(CC), typeof(CB), typeof(CA), typeof(object)])).IsTrue();
        await Assert.That(interfaceTree.Root.Children.Select(static node => node.Value).SequenceEqual([typeof(ID), typeof(IJ), typeof(II)])).IsTrue();
        await Assert.That(genericVariantInherited).IsTrue();
        await Assert.That(genericVariantType).IsEqualTo(typeof(IEnumerable<int>));
        await Assert.That(classGenericVariantInherited).IsFalse();
        await Assert.That(classGenericVariantType).IsNull();
        await Assert.That(genericDefinitionOrSame).IsEqualTo(typeof(List<>));
        await Assert.That(nonGenericDefinitionOrSame).IsEqualTo(typeof(string));
        await Assert.That(parameterlessConstructor).IsNotNull();
        await Assert.That(anyAccessibilityParameterlessConstructor).IsNotNull();
        await Assert.That(intStringConstructor).IsNotNull();
        await Assert.That(anyAccessibilityConstructor).IsNotNull();
    }

    [Test]
    public async Task TypeHelperExceptionalCasesAndGenericMethodMetadataTest()
    {
        var declaringMethodSafe = typeof(TypeExtensionsTests)
            .GetMethod(nameof(GenericMethod), BindingFlags.NonPublic | BindingFlags.Static)!
            .GetGenericArguments()[0]
            .GetDeclaringMethodSafe();

        var originalDeclaringMember = typeof(GenericContainer<>).GetGenericArguments()[0]
            .GetOriginalDeclaringGenericMember();

        var invalidImplements = Assert.Throws<ArgumentException>(() => typeof(int).Implements(typeof(int)));
        var invalidIsOrImplements = Assert.Throws<ArgumentException>(() => typeof(List<int>).IsOrImplements(typeof(IEnumerable<int>)));
        var invalidGenericDefinition = Assert.Throws<ArgumentException>(() => Garyon.Reflection.TypeExtensions.InheritsGenericVariantOf(typeof(List<int>), typeof(List<int>), out _));

        await Assert.That(declaringMethodSafe!.Name).IsEqualTo(nameof(GenericMethod));
        await Assert.That(originalDeclaringMember.Name).IsEqualTo("GenericContainer`1");
        await Assert.That(invalidImplements.Message.Contains("not an interface type")).IsTrue();
        await Assert.That(invalidIsOrImplements.Message.Contains("source interface type is not an interface type")).IsTrue();
        await Assert.That(invalidGenericDefinition.ParamName).IsEqualTo("genericDefinition");
    }
    [Test]
    public async Task GetInheritanceTreeTest()
    {
        var tree = typeof(CD).GetInheritanceTree();

        var expectedTree = new Tree<Type>(typeof(CD));
        // Massive manual tree construction
        // A manually hand-drawn tree was used for reference, this process is as tedious as washing dishes
        var cd = expectedTree.Root;
        var cc = cd.AddChild(typeof(CC));
        var ie0 = cd.AddChild(typeof(IE));

        var cb = cc.AddChild(typeof(CB));
        cc.AddChild(typeof(IC));

        ie0.AddChildren(typeof(IA), typeof(IB), typeof(IC));

        var ca = cb.AddChild(typeof(CA));
        var ik = cb.AddChild(typeof(IK));

        ca.AddChild(typeof(object));
        var id0 = ca.AddChild(typeof(ID));
        ca.AddChild(typeof(IJ));

        var id1 = ik.AddChild(typeof(ID));
        ik.AddChild(typeof(IJ));
        var ii = ik.AddChild(typeof(II));

        id0.AddChildren(typeof(IA), typeof(IB));
        id1.AddChildren(typeof(IA), typeof(IB));
        ii.AddChild(typeof(IH));

        var typeNamePrefix = $"{typeof(ExampleTypes)}+";
        var expectedTreeView =
            $"""
            {typeNamePrefix}CD
            ├---{typeNamePrefix}CC
            |   ├---{typeNamePrefix}CB
            |   |   ├---{typeNamePrefix}CA
            |   |   |   ├---System.Object
            |   |   |   ├---{typeNamePrefix}ID
            |   |   |   |   ├---{typeNamePrefix}IA
            |   |   |   |   └---{typeNamePrefix}IB
            |   |   |   └---{typeNamePrefix}IJ
            |   |   └---{typeNamePrefix}IK
            |   |       ├---{typeNamePrefix}ID
            |   |       |   ├---{typeNamePrefix}IA
            |   |       |   └---{typeNamePrefix}IB
            |   |       ├---{typeNamePrefix}IJ
            |   |       └---{typeNamePrefix}II
            |   |           └---{typeNamePrefix}IH
            |   └---{typeNamePrefix}IC
            └---{typeNamePrefix}IE
                ├---{typeNamePrefix}IA
                ├---{typeNamePrefix}IB
                └---{typeNamePrefix}IC
            """;

        await Assert.That(expectedTree.GetTreeView()).IsEqualTo(expectedTreeView.Trim());
        await Assert.That(tree).IsEqualTo(expectedTree);
    }

    #endregion

    #region Generic
    [Test]
    public async Task IsGenericVariantOrDefinitionTest()
    {
        await Assert.That(typeof(Action<int, string>).IsGenericVariantOrDefinition(typeof(Action<,>))).IsTrue();
        await Assert.That(typeof(Action<int, string>).IsGenericVariantOrDefinition(typeof(Action<>))).IsFalse();
        await Assert.That(typeof(Action<int, string>).IsGenericVariantOrDefinition(typeof(Func<,>))).IsFalse();
        await Assert.That(typeof(Action<int, string>).IsGenericVariantOrDefinition(typeof(Func<>))).IsFalse();

        await Assert.That(typeof(Action).IsGenericVariantOrDefinition(typeof(Action<,>))).IsFalse();
        await Assert.That(typeof(Action<,>).IsGenericVariantOrDefinition(typeof(Action<,>))).IsTrue();

        Assert.Throws<InvalidOperationException>(() => typeof(Action).IsGenericVariantOrDefinition(typeof(Action)));
        Assert.Throws<InvalidOperationException>(() => typeof(Action<,>).IsGenericVariantOrDefinition(typeof(Action)));
    }
    [Test]
    public async Task IsGenericVariantOfTest()
    {
        await Assert.That(typeof(Action<int, string>).IsGenericVariantOf(typeof(Action<,>))).IsTrue();
        await Assert.That(typeof(Action<int, string>).IsGenericVariantOf(typeof(Action<>))).IsFalse();
        await Assert.That(typeof(Action<int, string>).IsGenericVariantOf(typeof(Func<,>))).IsFalse();
        await Assert.That(typeof(Action<int, string>).IsGenericVariantOf(typeof(Func<>))).IsFalse();

        await Assert.That(typeof(Action).IsGenericVariantOf(typeof(Action<,>))).IsFalse();
        await Assert.That(typeof(Action<,>).IsGenericVariantOf(typeof(Action<,>))).IsFalse();

        Assert.Throws<InvalidOperationException>(() => typeof(Action).IsGenericVariantOf(typeof(Action)));
        Assert.Throws<InvalidOperationException>(() => typeof(Action<,>).IsGenericVariantOf(typeof(Action)));
    }
    [Test]
    public async Task CanConstructTest()
    {
        await Assert.That(typeof(Action<,>).CanConstruct<Action<int, string>>()).IsTrue();
        await Assert.That(typeof(Action<>).CanConstruct<Action<int, string>>()).IsFalse();
        await Assert.That(typeof(Func<,>).CanConstruct<Action<int, string>>()).IsFalse();
        await Assert.That(typeof(Func<>).CanConstruct<Action<int, string>>()).IsFalse();

        await Assert.That(typeof(Action<,>).CanConstruct(typeof(Action))).IsFalse();
        await Assert.That(typeof(Action<,>).CanConstruct(typeof(Action<,>))).IsFalse();

        Assert.Throws<InvalidOperationException>(() => typeof(Action).CanConstruct(typeof(Action<,>)));
        Assert.Throws<InvalidOperationException>(() => typeof(Action).CanConstruct(typeof(Action)));
    }
    #endregion

    #region Type Categories
    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task IsVoidTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(mapping, t => t.IsVoid, GaryonTypeExtensions.IsVoid);
    }
    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task IsTrueClassTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(mapping, t => t.IsTrueClass, GaryonTypeExtensions.IsTrueClass);
    }
    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task IsStaticClassTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(mapping, t => t.IsStatic, GaryonTypeExtensions.IsStaticClass);
    }
    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task IsDelegateTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(mapping, t => t.IsDelegate, GaryonTypeExtensions.IsDelegate);
    }
    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task IsExceptionTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(mapping, t => t.IsException, GaryonTypeExtensions.IsException);
    }
    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task IsAttributeTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(mapping, t => t.IsAttribute, GaryonTypeExtensions.IsAttribute);
    }
    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task IsTupleTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(mapping, t => t.IsTuple, GaryonTypeExtensions.IsTuple);
    }
    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task IsNullableValueTypeTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(mapping, t => t.IsNullableValueType, GaryonTypeExtensions.IsNullableValueType);
    }
    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task IsNullableTypeTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(mapping, t => t.IsNullableType, GaryonTypeExtensions.IsNullableType);
    }
    [Test]
    [MethodDataSource(nameof(GetTestTypeMappings))]
    public async Task IsReferenceTypeTest(TypeDefinitionInfoMapping mapping)
    {
        await AssertTypeDefinitionInfo(mapping, t => t.IsReferenceType, GaryonTypeExtensions.IsReferenceType);
    }
    #endregion

    [Test]
    public async Task GetArrayJaggingLevelTest()
    {
        await AssertJaggingLevel(typeof(int[]), 1);
        await AssertJaggingLevel(typeof(int[][]), 2);
        await AssertJaggingLevel(typeof(int[][][]), 3);
        await AssertJaggingLevel(typeof(int[][][][]), 4);
        await AssertJaggingLevel(typeof(int[][][][][]), 5);
        await AssertJaggingLevel(typeof(int[][][][][][]), 6);

        await AssertJaggingLevel(typeof(int[,,,][,,][,][]), 4);
        await AssertJaggingLevel(typeof(int**[,][,][,][,]), 4);

        static async Task AssertJaggingLevel(Type type, int level)
        {
            await Assert.That(type.GetArrayJaggingLevel()).IsEqualTo(level);
        }
    }
    [Test]
    public async Task GetMultiplePointerLevelTest()
    {
        await AssertMultiplePointerLevel(typeof(int*), 1);
        await AssertMultiplePointerLevel(typeof(int**), 2);
        await AssertMultiplePointerLevel(typeof(int***), 3);
        await AssertMultiplePointerLevel(typeof(int****), 4);
        await AssertMultiplePointerLevel(typeof(int*****), 5);
        await AssertMultiplePointerLevel(typeof(int******), 6);

        static async Task AssertMultiplePointerLevel(Type type, int level)
        {
            await Assert.That(type.GetMultiplePointerLevel()).IsEqualTo(level);
        }
    }

    [Test]
    public async Task GetDeepestElementTypeTest()
    {
        await AssertDeepestElementType(typeof(int*), typeof(int));
        await AssertDeepestElementType(typeof(int****), typeof(int));
        await AssertDeepestElementType(typeof(int[]), typeof(int));
        await AssertDeepestElementType(typeof(int[,,][,,,]), typeof(int));
        await AssertDeepestElementType(typeof(int***[,][,,][,]), typeof(int));
        await AssertDeepestElementType(intByRefType, typeof(int));
        await AssertDeepestElementType(intPointerByRefType, typeof(int));

        static async Task AssertDeepestElementType(Type type, Type expectedElementType)
        {
            await Assert.That(type.GetDeepestElementType()).IsEqualTo(expectedElementType);
        }
    }
    [Test]
    public async Task ContainsElementsOfType()
    {
        await Assert.That(typeof(int*).ContainsElementsOfType<int>()).IsTrue();
        await Assert.That(typeof(int****).ContainsElementsOfType<int>()).IsTrue();
        await Assert.That(typeof(int[]).ContainsElementsOfType<int>()).IsTrue();
        await Assert.That(typeof(int[,,][,,,]).ContainsElementsOfType<int>()).IsTrue();
        await Assert.That(typeof(int***[,][,,][,]).ContainsElementsOfType<int>()).IsTrue();
        await Assert.That(intByRefType.ContainsElementsOfType<int>()).IsTrue();
        await Assert.That(intPointerByRefType.ContainsElementsOfType<int>()).IsTrue();

        // To be completely honest, I don't fucking know why. It just works like that. Just don't ever use those types. Please.
        await Assert.That(typeof(int[,,][,,,]).ContainsElementsOfType<int[,,,]>()).IsTrue();
        await Assert.That(typeof(int[,,][,,,]).ContainsElementsOfType<int[]>()).IsFalse();

        bool pointerContains;
        unsafe
        {
            pointerContains = typeof(int**[,,][,,,]).ContainsElementsOfType<int**[,,,]>();
        }
        await Assert.That(pointerContains).IsTrue();

        await Assert.That(typeof(int**[,,][,,,]).ContainsElementsOfType(typeof(int**))).IsTrue();
        await Assert.That(typeof(int**[,,][,,,]).ContainsElementsOfType(typeof(int*))).IsTrue();
    }

    #region Instances
    private class PrivateConstructorContainer
    {
        private PrivateConstructorContainer() { }
    }
    private class PrivateProtectedConstructorContainer
    {
        private protected PrivateProtectedConstructorContainer() { }
    }
    private class ProtectedConstructorContainer
    {
        protected ProtectedConstructorContainer() { }
    }
    private class ProtectedInternalConstructorContainer
    {
        protected internal ProtectedInternalConstructorContainer() { }
    }
    private class InternalConstructorContainer
    {
        internal InternalConstructorContainer() { }
    }
    private class PublicConstructorContainer
    {
        public PublicConstructorContainer() { }
    }

    [Test]
    public async Task GetParameterlessConstructorTest()
    {
        await Assert.That(typeof(PublicConstructorContainer).GetParameterlessConstructor()).IsNotNull();
        await Assert.That(typeof(InternalConstructorContainer).GetParameterlessConstructor()).IsNull();
        await Assert.That(typeof(ProtectedInternalConstructorContainer).GetParameterlessConstructor()).IsNull();
        await Assert.That(typeof(ProtectedConstructorContainer).GetParameterlessConstructor()).IsNull();
        await Assert.That(typeof(PrivateProtectedConstructorContainer).GetParameterlessConstructor()).IsNull();
        await Assert.That(typeof(PrivateConstructorContainer).GetParameterlessConstructor()).IsNull();
    }
    [Test]
    public async Task GetAnyAccessibilityParameterlessConstructorTest()
    {
        // Holy shit those are some long names
        await Assert.That(typeof(PublicConstructorContainer).GetAnyAccessibilityParameterlessConstructor()).IsNotNull();
        await Assert.That(typeof(InternalConstructorContainer).GetAnyAccessibilityParameterlessConstructor()).IsNotNull();
        await Assert.That(typeof(ProtectedInternalConstructorContainer).GetAnyAccessibilityParameterlessConstructor()).IsNotNull();
        await Assert.That(typeof(ProtectedConstructorContainer).GetAnyAccessibilityParameterlessConstructor()).IsNotNull();
        await Assert.That(typeof(PrivateProtectedConstructorContainer).GetAnyAccessibilityParameterlessConstructor()).IsNotNull();
        await Assert.That(typeof(PrivateConstructorContainer).GetAnyAccessibilityParameterlessConstructor()).IsNotNull();
    }

    [Test]
    public async Task InitializeInstanceTest()
    {
        await AssertInstanceInitialization<PublicConstructorContainer>();
        await AssertInvalidInstanceInitialization<InternalConstructorContainer>();
        await AssertInvalidInstanceInitialization<ProtectedInternalConstructorContainer>();
        await AssertInvalidInstanceInitialization<ProtectedConstructorContainer>();
        await AssertInvalidInstanceInitialization<PrivateProtectedConstructorContainer>();
        await AssertInvalidInstanceInitialization<PrivateConstructorContainer>();

        static async Task AssertInstanceInitialization<T>()
            where T : class
        {
            await Assert.That(typeof(T).InitializeInstance<T>()).IsTypeOf<T>();
        }
        static async Task AssertInvalidInstanceInitialization<T>()
            where T : class
        {
            await Assert.That(typeof(T).InitializeInstance<T>()).IsNull();
        }
    }

    private class ArgumentConstructorContainer
    {
        public ArgumentConstructorContainer(string a, string b) { }
        public ArgumentConstructorContainer(int k) { }
    }

    [Test]
    public async Task InitializeInstanceWithArgumentsTest()
    {
        await AssertInstanceInitialization<ArgumentConstructorContainer>("a", "b");
        await AssertInstanceInitialization<ArgumentConstructorContainer>(0);
        await AssertInvalidInstanceInitialization<ArgumentConstructorContainer>();
        await AssertInvalidInstanceInitialization<ArgumentConstructorContainer>("a");
        await AssertInvalidInstanceInitialization<ArgumentConstructorContainer>(0, 0);

#nullable enable
        static async Task AssertInstanceInitialization<T>(params object?[]? parameters)
            where T : class
        {
            await Assert.That(typeof(T).InitializeInstance<T>(parameters)).IsTypeOf<T>();
        }
        static async Task AssertInvalidInstanceInitialization<T>(params object?[]? parameters)
            where T : class
        {
            await Assert.That(typeof(T).InitializeInstance<T>(parameters)).IsNull();
        }
#nullable disable
    }
    #endregion

    [Test]
    public async Task GetOriginalDeclaringGenericMemberTest()
    {
        // Nice identifier
        var testingMethod = GenericWithNestedGeneric<int, int>.Nested<int, int, int>.Nested2<int, int, int>.Type;
        var genericParameters = testingMethod.GetGenericArguments();
        var outer = genericParameters[0..2];
        var inner1 = genericParameters[2..5];
        var inner2 = genericParameters[5..8];
        await AssertDeclaringTypes(outer, typeof(GenericWithNestedGeneric<,>));
        await AssertDeclaringTypes(inner1, typeof(GenericWithNestedGeneric<,>.Nested<,,>));
        await AssertDeclaringTypes(inner2, typeof(GenericWithNestedGeneric<,>.Nested<,,>.Nested2<,,>));

        var nonGenericNestedType = GenericWithNonGenericNested<int, int>.Nested.Nested2<int>.Type;
        var nonGenericNestedParameters = nonGenericNestedType.GetGenericArguments();
        var nonGenericOuter = nonGenericNestedParameters[0..2];
        var nonGenericInner = nonGenericNestedParameters[2..3];
        await AssertDeclaringTypes(nonGenericOuter, typeof(GenericWithNonGenericNested<,>));
        await AssertDeclaringTypes(nonGenericInner, typeof(GenericWithNonGenericNested<,>.Nested.Nested2<>));

        var manyOuterType = GenericWithManyOuterParameters<int, int, int, int>.Nested<int>.Nested2<int>.Type;
        var manyOuterParameters = manyOuterType.GetGenericArguments();
        var manyOuter = manyOuterParameters[0..4];
        var manyInner1 = manyOuterParameters[4..5];
        var manyInner2 = manyOuterParameters[5..6];
        await AssertDeclaringTypes(manyOuter, typeof(GenericWithManyOuterParameters<,,,>));
        await AssertDeclaringTypes(manyInner1, typeof(GenericWithManyOuterParameters<,,,>.Nested<>));
        await AssertDeclaringTypes(manyInner2, typeof(GenericWithManyOuterParameters<,,,>.Nested<>.Nested2<>));

        async Task AssertDeclaringTypes(IEnumerable<Type> genericParameters, MemberInfo expectedDeclaringMember)
        {
            await Assert.That(genericParameters
                .Select(parameter => parameter.GetOriginalDeclaringGenericMember())
                .All(declaringMember => declaringMember == expectedDeclaringMember)).IsTrue();
        }
    }

    public static IReadOnlyList<TypeDefinitionInfoMapping> GetTestTypeMappings()
    {
        return
        [
            new(typeof(void), new TypeDefinitionInfo(TypeKind.Void, TypeModifiers.PublicSealed)),
            new(typeof(IA), new TypeDefinitionInfo(TypeKind.Interface, TypeModifiers.ProtectedAbstract)),
            new(typeof(CA), new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.Protected)),
            new(typeof(SA), new TypeDefinitionInfo(TypeKind.Struct, TypeModifiers.ProtectedSealed)),
            new(typeof(DA), new TypeDefinitionInfo(TypeKind.Delegate, TypeModifiers.ProtectedSealed)),
            new(typeof(EA), new TypeDefinitionInfo(TypeKind.Enum, TypeModifiers.ProtectedSealed)),
            new(typeof(SA[]), new TypeDefinitionInfo(TypeKind.Array, TypeModifiers.PublicSealed)),
            new(typeof(SA*), new TypeDefinitionInfo(TypeKind.Pointer, TypeModifiers.Public)),
            new(typeof(SA*[]), new TypeDefinitionInfo(TypeKind.Array, TypeModifiers.PublicSealed)),
            new(typeof((int, int)), new TypeDefinitionInfo(TypeKind.Tuple, TypeModifiers.PublicSealed)),
            new(typeof((int, int)?), new TypeDefinitionInfo(TypeKind.NullableTuple, TypeModifiers.PublicSealed)),
            new(typeof(StaticClass), new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.ProtectedStatic)),
            new(typeof(GenericClass<>), new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.Protected)),
            new(typeof(GenericClass<,>), new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.Protected)),
            new(typeof(GenericClass<,,>), new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.Protected)),
            new(typeof(GenericStaticClass<>), new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.ProtectedStatic)),
            new(typeof(GenericStaticClass<,>), new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.ProtectedStatic)),
            new(typeof(GenericStaticClass<,,>), new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.ProtectedStatic)),
            new(typeof(ExceptionA), new TypeDefinitionInfo(TypeKind.Exception, TypeModifiers.Protected)),
            new(typeof(ExceptionA<>), new TypeDefinitionInfo(TypeKind.Exception, TypeModifiers.Protected)),
            new(intByRefType, new TypeDefinitionInfo(TypeKind.ByRef, TypeModifiers.PublicRef)),
            new(intPointerByRefType, new TypeDefinitionInfo(TypeKind.ByRef, TypeModifiers.PublicRef)),
            new(typeof(FileType), new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.InternalSealed)),
        ];
    }

    public sealed record TypeDefinitionInfoMapping(Type Type, TypeDefinitionInfo Info);
}

file sealed class FileType;
