using Garyon.DataStructures;
using Garyon.QualityControl.Types;
using Garyon.Reflection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using GaryonTypeExtensions = Garyon.Reflection.TypeExtensions;

namespace Garyon.Tests.Reflection
{
    [Parallelizable(ParallelScope.Children)]
    public class TypeExtensionsTests : ExampleTypes
    {
        private static readonly Type intByRefType;
        private static readonly Type intPointerByRefType;
        private static readonly Dictionary<Type, TypeDefinitionInfo> predicateTestTypes;

        static TypeExtensionsTests()
        {
            var parameters = typeof(TypeExtensionsTests).GetMethod(nameof(DummyByRefFunction), BindingFlags.NonPublic | BindingFlags.Static).GetParameters();
            intByRefType = parameters[0].ParameterType;
            intPointerByRefType = parameters[1].ParameterType;

            predicateTestTypes = new Dictionary<Type, TypeDefinitionInfo>
            {
                [typeof(void)] = new TypeDefinitionInfo(TypeKind.Void, TypeModifiers.PublicSealed),
                [typeof(IA)] = new TypeDefinitionInfo(TypeKind.Interface, TypeModifiers.ProtectedAbstract),
                [typeof(CA)] = new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.Protected),
                [typeof(SA)] = new TypeDefinitionInfo(TypeKind.Struct, TypeModifiers.ProtectedSealed),
                [typeof(DA)] = new TypeDefinitionInfo(TypeKind.Delegate, TypeModifiers.ProtectedSealed),
                [typeof(EA)] = new TypeDefinitionInfo(TypeKind.Enum, TypeModifiers.ProtectedSealed),
                [typeof(SA[])] = new TypeDefinitionInfo(TypeKind.Array, TypeModifiers.PublicSealed),
                [typeof(SA*)] = new TypeDefinitionInfo(TypeKind.Pointer, TypeModifiers.Internal),
                [typeof(SA*[])] = new TypeDefinitionInfo(TypeKind.Array, TypeModifiers.PublicSealed),
                [typeof((int, int))] = new TypeDefinitionInfo(TypeKind.Tuple, TypeModifiers.PublicSealed),
                [typeof((int, int)?)] = new TypeDefinitionInfo(TypeKind.NullableTuple, TypeModifiers.PublicSealed),
                [typeof(StaticClass)] = new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.ProtectedStatic),
                [typeof(GenericClass<>)] = new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.Protected),
                [typeof(GenericClass<,>)] = new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.Protected),
                [typeof(GenericClass<,,>)] = new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.Protected),
                [typeof(GenericStaticClass<>)] = new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.ProtectedStatic),
                [typeof(GenericStaticClass<,>)] = new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.ProtectedStatic),
                [typeof(GenericStaticClass<,,>)] = new TypeDefinitionInfo(TypeKind.Class, TypeModifiers.ProtectedStatic),
                [typeof(ExceptionA)] = new TypeDefinitionInfo(TypeKind.Exception, TypeModifiers.Protected),
                [typeof(ExceptionA<>)] = new TypeDefinitionInfo(TypeKind.Exception, TypeModifiers.Protected),
                [intByRefType] = new TypeDefinitionInfo(TypeKind.ByRef, TypeModifiers.InternalRef),
                [intPointerByRefType] = new TypeDefinitionInfo(TypeKind.ByRef, TypeModifiers.InternalRef),
            };
        }

        // Underscore, optionally followed by any digit(s) is considered a dummy variable and does not emit IDE0060
        private static unsafe void DummyByRefFunction(ref int _0, ref int* _1) { }

        private void AssertTypeDefinitionInfo<T>(Func<TypeDefinitionInfo, T> expected, Func<Type, T> actual)
        {
            foreach (var kvp in predicateTestTypes)
                Assert.AreEqual(expected(kvp.Value), actual(kvp.Key));
        }

        #region Inheritance

        [Test]
        public void TypeDefinitionInfoTest()
        {
            foreach (var kvp in predicateTestTypes)
                Assert.AreEqual(kvp.Value, new TypeDefinitionInfo(kvp.Key));
        }

        [Test]
        public void CanInheritTest()
        {
            AssertTypeDefinitionInfo(t => t.CanInherit, GaryonTypeExtensions.CanInherit);
        }
        [Test]
        public void CanInheritCustomTypesTest()
        {
            AssertTypeDefinitionInfo(t => t.CanInheritCustomTypes, GaryonTypeExtensions.CanInheritCustomTypes);
        }
        [Test]
        public void CanInheritInterfacesTest()
        {
            AssertTypeDefinitionInfo(t => t.CanInheritInterfaces, GaryonTypeExtensions.CanInheritInterfaces);
        }
        [Test]
        public void CanInheritClassesTest()
        {
            AssertTypeDefinitionInfo(t => t.CanInheritClasses, GaryonTypeExtensions.CanInheritClasses);
        }
        [Test]
        public void CanInheritCustomClassesTest()
        {
            AssertTypeDefinitionInfo(t => t.CanInheritCustomClasses, GaryonTypeExtensions.CanInheritCustomClasses);
        }
        
        public void CanBeInheritedTest()
        {
            AssertTypeDefinitionInfo(t => t.CanBeInherited, GaryonTypeExtensions.CanBeInherited);
        }

        [Test]
        public void InheritsTest()
        {
            Assert.IsFalse(typeof(object).Inherits<object>());
            Assert.IsTrue(typeof(ID).Inherits<IA>());
            Assert.IsTrue(typeof(ID).Inherits<IB>());
            Assert.IsFalse(typeof(ID).Inherits<ID>());
            Assert.IsTrue(typeof(CA).Inherits<object>());
            Assert.IsTrue(typeof(EA).Inherits<Enum>());
            Assert.IsTrue(typeof(EA).Inherits<ValueType>());
            Assert.IsFalse(typeof(SA).Inherits<Enum>());
            Assert.IsTrue(typeof(DA).Inherits<Delegate>());
        }
        
        [Test]
        public void GetInheritanceLevelTest()
        {
            int level = typeof(CD).GetInheritanceLevel();
            Assert.AreEqual(3, level);
        }
        [Test]
        public void GetInheritanceTreeTest()
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

            var expectedTreeView =
@"
CD
├---CC
|   ├---CB
|   |   ├---CA
|   |   |   ├---System.Object
|   |   |   ├---ID
|   |   |   |   ├---IA
|   |   |   |   └---IB
|   |   |   └---IJ
|   |   └---IK
|   |       ├---ID
|   |       |   ├---IA
|   |       |   └---IB
|   |       ├---IJ
|   |       └---II
|   |           └---IH
|   └---IC
└---IE
    ├---IA
    ├---IB
    └---IC
";

            // Verify the expected tree's tree view is the intended
            Assert.AreEqual(expectedTreeView.Trim(), expectedTree.GetTreeView().Replace($"{typeof(ExampleTypes)}+", ""));
            Assert.AreEqual(expectedTree, tree);
        }
        #endregion

        #region Generic
        [Test]
        public void IsGenericVariantOrDefinitionTest()
        {
            Assert.IsTrue(typeof(Action<int, string>).IsGenericVariantOrDefinition(typeof(Action<,>)));
            Assert.IsFalse(typeof(Action<int, string>).IsGenericVariantOrDefinition(typeof(Action<>)));
            Assert.IsFalse(typeof(Action<int, string>).IsGenericVariantOrDefinition(typeof(Func<,>)));
            Assert.IsFalse(typeof(Action<int, string>).IsGenericVariantOrDefinition(typeof(Func<>)));

            Assert.IsFalse(typeof(Action).IsGenericVariantOrDefinition(typeof(Action<,>)));
            Assert.IsTrue(typeof(Action<,>).IsGenericVariantOrDefinition(typeof(Action<,>)));

            Assert.Throws<InvalidOperationException>(() => typeof(Action).IsGenericVariantOrDefinition(typeof(Action)));
            Assert.Throws<InvalidOperationException>(() => typeof(Action<,>).IsGenericVariantOrDefinition(typeof(Action)));
        }
        [Test]
        public void IsGenericVariantOfTest()
        {
            Assert.IsTrue(typeof(Action<int, string>).IsGenericVariantOf(typeof(Action<,>)));
            Assert.IsFalse(typeof(Action<int, string>).IsGenericVariantOf(typeof(Action<>)));
            Assert.IsFalse(typeof(Action<int, string>).IsGenericVariantOf(typeof(Func<,>)));
            Assert.IsFalse(typeof(Action<int, string>).IsGenericVariantOf(typeof(Func<>)));

            Assert.IsFalse(typeof(Action).IsGenericVariantOf(typeof(Action<,>)));
            Assert.IsFalse(typeof(Action<,>).IsGenericVariantOf(typeof(Action<,>)));

            Assert.Throws<InvalidOperationException>(() => typeof(Action).IsGenericVariantOf(typeof(Action)));
            Assert.Throws<InvalidOperationException>(() => typeof(Action<,>).IsGenericVariantOf(typeof(Action)));
        }
        [Test]
        public void CanConstructTest()
        {
            Assert.IsTrue(typeof(Action<,>).CanConstruct<Action<int, string>>());
            Assert.IsFalse(typeof(Action<>).CanConstruct<Action<int, string>>());
            Assert.IsFalse(typeof(Func<,>).CanConstruct<Action<int, string>>());
            Assert.IsFalse(typeof(Func<>).CanConstruct<Action<int, string>>());

            Assert.IsFalse(typeof(Action<,>).CanConstruct(typeof(Action)));
            Assert.IsFalse(typeof(Action<,>).CanConstruct(typeof(Action<,>)));

            Assert.Throws<InvalidOperationException>(() => typeof(Action).CanConstruct(typeof(Action<,>)));
            Assert.Throws<InvalidOperationException>(() => typeof(Action).CanConstruct(typeof(Action)));
        }
        #endregion

        #region Type Categories
        [Test]
        public void IsVoidTest()
        {
            AssertTypeDefinitionInfo(t => t.IsVoid, GaryonTypeExtensions.IsVoid);
        }
        [Test]
        public void IsTrueClassTest()
        {
            AssertTypeDefinitionInfo(t => t.IsTrueClass, GaryonTypeExtensions.IsTrueClass);
        }
        [Test]
        public void IsStaticClassTest()
        {
            AssertTypeDefinitionInfo(t => t.IsStatic, GaryonTypeExtensions.IsStaticClass);
        }
        [Test]
        public void IsDelegateTest()
        {
            AssertTypeDefinitionInfo(t => t.IsDelegate, GaryonTypeExtensions.IsDelegate);
        }
        [Test]
        public void IsExceptionTest()
        {
            AssertTypeDefinitionInfo(t => t.IsException, GaryonTypeExtensions.IsException);
        }
        [Test]
        public void IsAttributeTest()
        {
            AssertTypeDefinitionInfo(t => t.IsAttribute, GaryonTypeExtensions.IsAttribute);
        }
        [Test]
        public void IsTupleTest()
        {
            AssertTypeDefinitionInfo(t => t.IsTuple, GaryonTypeExtensions.IsTuple);
        }
        [Test]
        public void IsNullableValueTypeTest()
        {
            AssertTypeDefinitionInfo(t => t.IsNullableValueType, GaryonTypeExtensions.IsNullableValueType);
        }
        [Test]
        public void IsNullableTypeTest()
        {
            AssertTypeDefinitionInfo(t => t.IsNullableType, GaryonTypeExtensions.IsNullableType);
        }
        [Test]
        public void IsReferenceTypeTest()
        {
            AssertTypeDefinitionInfo(t => t.IsReferenceType, GaryonTypeExtensions.IsReferenceType);
        }
        #endregion

        [Test]
        public void GetArrayJaggingLevelTest()
        {
            Assert.AreEqual(1, typeof(int[]).GetArrayJaggingLevel());
            Assert.AreEqual(2, typeof(int[][]).GetArrayJaggingLevel());
            Assert.AreEqual(3, typeof(int[][][]).GetArrayJaggingLevel());
            Assert.AreEqual(4, typeof(int[][][][]).GetArrayJaggingLevel());
            Assert.AreEqual(5, typeof(int[][][][][]).GetArrayJaggingLevel());
            Assert.AreEqual(6, typeof(int[][][][][][]).GetArrayJaggingLevel());

            Assert.AreEqual(4, typeof(int[,,,][,,][,][]).GetArrayJaggingLevel());
            Assert.AreEqual(4, typeof(int**[,][,][,][,]).GetArrayJaggingLevel());
        }
        [Test]
        public void GetMultiplePointerLevelTest()
        {
            Assert.AreEqual(1, typeof(int*).GetMultiplePointerLevel());
            Assert.AreEqual(2, typeof(int**).GetMultiplePointerLevel());
            Assert.AreEqual(3, typeof(int***).GetMultiplePointerLevel());
            Assert.AreEqual(4, typeof(int****).GetMultiplePointerLevel());
            Assert.AreEqual(5, typeof(int*****).GetMultiplePointerLevel());
            Assert.AreEqual(6, typeof(int******).GetMultiplePointerLevel());
        }

        [Test]
        public void GetDeepestElementTypeTest()
        {
            Assert.AreEqual(typeof(int), typeof(int*).GetDeepestElementType());
            Assert.AreEqual(typeof(int), typeof(int****).GetDeepestElementType());
            Assert.AreEqual(typeof(int), typeof(int[]).GetDeepestElementType());
            Assert.AreEqual(typeof(int), typeof(int[,,][,,,]).GetDeepestElementType());
            Assert.AreEqual(typeof(int), typeof(int***[,][,,][,]).GetDeepestElementType());
            Assert.AreEqual(typeof(int), intByRefType.GetDeepestElementType());
            Assert.AreEqual(typeof(int), intPointerByRefType.GetDeepestElementType());
        }
        [Test]
        public void ContainsElementsOfType()
        {
            Assert.IsTrue(typeof(int*).ContainsElementsOfType<int>());
            Assert.IsTrue(typeof(int****).ContainsElementsOfType<int>());
            Assert.IsTrue(typeof(int[]).ContainsElementsOfType<int>());
            Assert.IsTrue(typeof(int[,,][,,,]).ContainsElementsOfType<int>());
            Assert.IsTrue(typeof(int***[,][,,][,]).ContainsElementsOfType<int>());
            Assert.IsTrue(intByRefType.ContainsElementsOfType<int>());
            Assert.IsTrue(intPointerByRefType.ContainsElementsOfType<int>());

            // To be completely honest, I don't fucking know why. It just works like that. Just don't ever use those types. Please.
            Assert.IsTrue(typeof(int[,,][,,,]).ContainsElementsOfType<int[,,,]>());
            Assert.IsFalse(typeof(int[,,][,,,]).ContainsElementsOfType<int[]>());

            Assert.IsTrue(typeof(int**[,,][,,,]).ContainsElementsOfType<int**[,,,]>());

            // A typeof generic type argument constraint would be much appreciated:
            /*
                void Function<T>()
                    where T : typeof
                {
                    var t = typeof(T);
                    T element = default; // Illegal; T could be void
                }
             */
            Assert.IsTrue(typeof(int**[,,][,,,]).ContainsElementsOfType(typeof(int**)));
            Assert.IsTrue(typeof(int**[,,][,,,]).ContainsElementsOfType(typeof(int*)));
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
        public void GetParameterlessConstructorTest()
        {
            Assert.IsNotNull(typeof(PublicConstructorContainer).GetParameterlessConstructor());
            Assert.IsNull(typeof(InternalConstructorContainer).GetParameterlessConstructor());
            Assert.IsNull(typeof(ProtectedInternalConstructorContainer).GetParameterlessConstructor());
            Assert.IsNull(typeof(ProtectedConstructorContainer).GetParameterlessConstructor());
            Assert.IsNull(typeof(PrivateProtectedConstructorContainer).GetParameterlessConstructor());
            Assert.IsNull(typeof(PrivateConstructorContainer).GetParameterlessConstructor());
        }
        [Test]
        public void GetAnyAccessibilityParameterlessConstructorTest()
        {
            // Holy shit those are some long names
            Assert.IsNotNull(typeof(PublicConstructorContainer).GetAnyAccessibilityParameterlessConstructor());
            Assert.IsNotNull(typeof(InternalConstructorContainer).GetAnyAccessibilityParameterlessConstructor());
            Assert.IsNotNull(typeof(ProtectedInternalConstructorContainer).GetAnyAccessibilityParameterlessConstructor());
            Assert.IsNotNull(typeof(ProtectedConstructorContainer).GetAnyAccessibilityParameterlessConstructor());
            Assert.IsNotNull(typeof(PrivateProtectedConstructorContainer).GetAnyAccessibilityParameterlessConstructor());
            Assert.IsNotNull(typeof(PrivateConstructorContainer).GetAnyAccessibilityParameterlessConstructor());
        }

        [Test]
        public void InitializeInstanceTest()
        {
            AssertInstanceInitialization<PublicConstructorContainer>();
            AssertInvalidInstanceInitialization<InternalConstructorContainer>();
            AssertInvalidInstanceInitialization<ProtectedInternalConstructorContainer>();
            AssertInvalidInstanceInitialization<ProtectedConstructorContainer>();
            AssertInvalidInstanceInitialization<PrivateProtectedConstructorContainer>();
            AssertInvalidInstanceInitialization<PrivateConstructorContainer>();

            static void AssertInstanceInitialization<T>()
                where T : class
            {
                Assert.IsInstanceOf<T>(typeof(T).InitializeInstance<T>());
            }
            static void AssertInvalidInstanceInitialization<T>()
                where T : class
            {
                Assert.IsNull(typeof(T).InitializeInstance<T>());
            }
        }

        private class ArgumentConstructorContainer
        {
            public ArgumentConstructorContainer(string a, string b) { }
            public ArgumentConstructorContainer(int k) { }
        }

        [Test]
        public void InitializeInstanceWithArgumentsTest()
        {
            AssertInstanceInitialization<ArgumentConstructorContainer>("a", "b");
            AssertInstanceInitialization<ArgumentConstructorContainer>(0);
            AssertInvalidInstanceInitialization<ArgumentConstructorContainer>();
            AssertInvalidInstanceInitialization<ArgumentConstructorContainer>("a");
            AssertInvalidInstanceInitialization<ArgumentConstructorContainer>(0, 0);

            static void AssertInstanceInitialization<T>(params object?[]? parameters)
                where T : class
            {
                Assert.IsInstanceOf<T>(typeof(T).InitializeInstance<T>(parameters));
            }
            static void AssertInvalidInstanceInitialization<T>(params object?[]? parameters)
                where T : class
            {
                Assert.IsNull(typeof(T).InitializeInstance<T>(parameters));
            }
        }
        #endregion
    }
}
