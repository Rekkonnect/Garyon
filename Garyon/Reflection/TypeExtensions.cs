using Garyon.DataStructures;
using Garyon.Exceptions;
using Garyon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Garyon.Reflection
{
    /// <summary>Provides extension methods for the <seealso cref="Type"/> class.</summary>
    public static class TypeExtensions
    {
        #region Static Function Wrappers
        /// <inheritdoc cref="Type.GetTypeCode(Type?)"/>
        public static TypeCode GetTypeCode(this Type type) => Type.GetTypeCode(type);
        #endregion

        #region Inheritance
        /// <summary>Determines whether the provided type can inherit any type, either a class or an interface.</summary>
        /// <param name="type">The type to determine whether it can inherit any type.</param>
        /// <returns><see langword="true"/> if the type can inherit any type; that is, it is not a pointer or a by reference type, otherwise <see langword="false"/>.</returns>
        public static bool CanInherit(this Type type) => !type.IsPointer && !type.IsByRef;
        /// <summary>Determines whether the provided type can inherit any type, either a class or an interface.</summary>
        /// <param name="type">The type to determine whether it can inherit any type.</param>
        /// <returns><see langword="true"/> if the type can inherit any type; that is, it is not a pointer, a by reference, a static class, a delegate, an enum or an array type, otherwise <see langword="false"/>.</returns>
        public static bool CanInheritCustomTypes(this Type type) => type.CanInherit() && !type.IsStaticClass() && !type.IsDelegate() && !type.IsEnum && !type.IsArray;
        /// <summary>Determines whether the provided type can inherit any interface.</summary>
        /// <param name="type">The type to determine whether it can inherit any interface.</param>
        /// <returns><see langword="true"/> if the type can inherit any interface; that is, it is not a pointer, a by reference, a static class, a delegate, an enum or an array type, otherwise <see langword="false"/>.</returns>
        public static bool CanInheritInterfaces(this Type type) => type.CanInheritCustomTypes();
        /// <summary>Determines whether the provided type can inherit any class.</summary>
        /// <param name="type">The type to determine whether it can inherit any class.</param>
        /// <returns><see langword="true"/> if the type can inherit any interface; that is, it is not a pointer, a by reference, or a delegate type, otherwise <see langword="false"/>.</returns>
        public static bool CanInheritClasses(this Type type) => type.CanInherit() && !type.IsInterface;
        /// <summary>Determines whether the provided type can inherit any custom class.</summary>
        /// <param name="type">The type to determine whether it can inherit any custom class.</param>
        /// <returns><see langword="true"/> if the type can inherit any interface; that is, it is not a pointer, a by reference, a static class, or a delegate type, otherwise <see langword="false"/>.</returns>
        public static bool CanInheritCustomClasses(this Type type) => type.IsTrueClass() && !type.IsStaticClass();

        /// <summary>Determines whether the provided type can be inherited.</summary>
        /// <param name="type">The type to determine whether it can be inherited.</param>
        /// <returns>A value determining whether <paramref name="type"/> can be inherited; that is, a non-sealed, thus non-static, class or an interface type.</returns>
        public static bool CanBeInherited(this Type type) => !type.IsSealed && (type.IsTrueClass() || type.IsInterface);

        /// <summary>Determines whether a type inherits another type, or is equal to it.</summary>
        /// <typeparam name="T">The type to check whether it is being inherited by or equal to the provided <paramref name="type"/>.</typeparam>
        /// <param name="type">The type to determine whether it inherits or is equal to the type <typeparamref name="T"/>.</param>
        /// <returns>A value determining whether <paramref name="type"/> inherits or equals <typeparamref name="T"/>.</returns>
        public static bool InheritsOrEquals<T>(this Type type) => type.InheritsOrEquals(typeof(T));
        /// <summary>Determines whether a type inherits or equals another type.</summary>
        /// <param name="type">The type to determine whether it inherits or equals the type <paramref name="inherited"/>.</param>
        /// <param name="inherited">The type to check whether it is being inherited by or equal to the provided <paramref name="type"/>.</param>
        /// <returns>A value determining whether <paramref name="type"/> inherits or equals <paramref name="inherited"/>.</returns>
        public static bool InheritsOrEquals(this Type type, Type inherited) => type == inherited || type.Inherits(inherited);
        /// <summary>Determines whether a type inherits another type.</summary>
        /// <typeparam name="T">The type to check whether it is being inherited by the provided <paramref name="type"/>.</typeparam>
        /// <param name="type">The type to determine whether it inherits the type <typeparamref name="T"/>.</param>
        /// <returns>A value determining whether <paramref name="type"/> inherits <typeparamref name="T"/>.</returns>
        public static bool Inherits<T>(this Type type) => type.Inherits(typeof(T));
        /// <summary>Determines whether a type inherits another type.</summary>
        /// <param name="type">The type to determine whether it inherits the type <paramref name="inherited"/>.</param>
        /// <param name="inherited">The type to check whether it is being inherited by the provided <paramref name="type"/>.</param>
        /// <returns>A value determining whether <paramref name="type"/> inherits <paramref name="inherited"/>.</returns>
        public static bool Inherits(this Type type, Type inherited)
        {
            if (type == inherited)
                return false;

            if (!type.CanInherit())
                return false;

            if (type.IsEnum)
                if (inherited == typeof(Enum)) // Allow fallbacking to the next check for ValueType
                    return true;

            if (type.IsValueType)
                return inherited == typeof(ValueType) || inherited == typeof(object);

            return inherited.IsAssignableFrom(type);
        }

        /// <summary>Determines whether a type implements an interface type.</summary>
        /// <typeparam name="T">The type of the interface.</typeparam>
        /// <param name="type">The type to check if it implements the provided interface type. Must not be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the type implements the provided interface type, otherwise <see langword="false"/>.</returns>
        /// <exception cref="ArgumentException"><paramref name="type"/> is a type that cannot implement interfaces, or <typeparamref name="T"/> is not an interface.</exception>
        public static bool Implements<T>(this Type type)
            where T : class
        {
            return Implements(type, typeof(T));
        }
        /// <summary>Determines whether a type implements an interface type.</summary>
        /// <param name="type">The type to check if it implements the provided interface type. Must not be <see langword="null"/>.</param>
        /// <param name="interfaceType">The type of the interface. Must not be <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the type implements the provided interface type, otherwise <see langword="false"/>.</returns>
        /// <exception cref="ArgumentException"><paramref name="type"/> is a type that cannot implement interfaces, or <paramref name="interfaceType"/> is not an interface.</exception>
        public static bool Implements(this Type type, Type interfaceType)
        {
            if (!interfaceType.IsInterface)
                ThrowHelper.Throw<ArgumentException>("The requested interface type is not an interface type.");

            if (!type.CanInheritInterfaces())
                ThrowHelper.Throw<ArgumentException>("The provided type cannot implement interfaces.");

            return type.GetInterfaces().Contains(interfaceType);
        }

        /// <summary>Determines the number of base types that the provided type directly inherits, excluding the special classes <seealso cref="ValueType"/>, <seealso cref="Array"/>, <seealso cref="Delegate"/>, <seealso cref="Enum"/> and <seealso cref="Object"/>.</summary>
        /// <param name="type">The type whose inheritance level to determine.</param>
        /// <returns>The number of base types that the provided type inherits. For value types, pointers, arrays, and delegates this is equal to 0, and for reference types it is equal to the number of classes that are recursively directly inherited, excluding the <seealso cref="Object"/> class.</returns>
        public static int GetInheritanceLevel(this Type type)
        {
            if (type.IsValueType || !type.CanInheritClasses())
                return 0;

            var currentType = type;
            int level = 0;
            while ((currentType = currentType.BaseType) != null)
                level++;
            return level - 1;
        }
        /// <summary>Gets a <seealso cref="Tree{T}"/> representing the provided type's inheritance tree.</summary>
        /// <param name="type">The type whose inheritance tree to get.</param>
        /// <returns>A <seealso cref="Tree{T}"/> representing the provided type's inheritance tree.</returns>
        /// <remarks>The inheritance tree's root is the provided type, and the base classes are ancestors, which is the opposite than the proposed inheritance tree views, that have the deepest base object as the root, with each child being the inheritor.</remarks>
        public static Tree<Type> GetInheritanceTree(this Type type)
        {
            var tree = new Tree<Type>(type);

            if (!type.CanInherit())
                return tree;
            
            var leaves = new Queue<TreeNode<Type>>();
            leaves.Enqueue(tree.Root);

            while (leaves.Any())
            {
                var node = leaves.Dequeue();
                var nodeType = node.Value;

                // Add the class
                if (nodeType.IsClass)
                {
                    var baseType = nodeType.BaseType;
                    if (baseType != null)
                    {
                        var baseTypeNode = node.AddChild(baseType);
                        leaves.Enqueue(baseTypeNode);
                    }
                }

                // Add the interfaces
                var interfaces = nodeType.GetInterfaces();
                foreach (var i in interfaces)
                {
                    var interfaceNode = node.AddChild(i);
                    leaves.Enqueue(interfaceNode);

                    // Remove indirectly inherited interfaces
                    var currentParent = node;
                    while ((currentParent = currentParent.Parent) != null)
                        currentParent.RemoveChild(i);
                }
            }

            return tree;
        }
        /// <summary>Gets a <seealso cref="Tree{T}"/> representing the provided type's interface inheritance tree.</summary>
        /// <param name="type">The type whose interface inheritance tree to get.</param>
        /// <returns>A <seealso cref="Tree{T}"/> representing the provided type's interface inheritance tree. Each level only contains direct interface implementations, meaning that no node has nodes of indirectly inherited interfaces as direct children, they are indirect descendants though.</returns>
        public static Tree<Type> GetInterfaceInheritanceTree(this Type type)
        {
            var tree = new Tree<Type>(type);

            var leaves = new Queue<TreeNode<Type>>();
            leaves.Enqueue(tree.Root);

            if (!type.CanInheritInterfaces())
                return tree;

            while (leaves.Any())
            {
                var node = leaves.Dequeue();
                var nodeType = node.Value;

                // If the interface is an indirectly inherited interface, it has been already removed from the tree
                if (node.IsRoot)
                    continue;

                // Add the interfaces
                var interfaces = nodeType.GetInterfaces();
                foreach (var i in interfaces)
                {
                    var interfaceNode = node.AddChild(i);
                    leaves.Enqueue(interfaceNode);

                    // Remove indirectly inherited interfaces
                    var currentParent = node;
                    while ((currentParent = currentParent.Parent) != null)
                        currentParent.RemoveChild(i);
                }
            }

            return tree;
        }
        /// <summary>Gets a the deepest interface inheritance level from the interface inheritance tree of the specified type.</summary>
        /// <param name="type">The type whose deepest interface inheritance level to get.</param>
        /// <returns>The deepest interface inheritance level. For pointers, by reference, delegate and enum types, this is equal to 0. For all the other types it is equal to the height of the interface inheritance tree.</returns>
        public static int GetDeepestInterfaceInheritanceLevel(this Type type)
        {
            if (type.CanInheritInterfaces())
                return 0;

            return type.GetInterfaceInheritanceTree().Height;
        }
        #endregion

        #region Generic
        /// <summary>Determines whether the provided type is a variant of the specified base generic type, or a definition that is equal to the provided generic type definition.</summary>
        /// <param name="type">The type to determine whether it is a generic variant.</param>
        /// <param name="genericType">The base generic type.</param>
        /// <returns><see langword="true"/> if <paramref name="type"/> is a generic variant of or equal to <paramref name="genericType"/>, otherwise <see langword="false"/>.</returns>
        /// <exception cref="InvalidOperationException">The <paramref name="genericType"/> argument is not an unbound generic type.</exception>
        public static bool IsGenericVariantOrDefinition(this Type type, Type genericType)
        {
            if (!genericType.IsGenericTypeDefinition)
                ThrowHelper.Throw<InvalidOperationException>("The genericType argument must be an unbound generic type.");
            if (!type.IsGenericType)
                return false;

            return type.GetGenericTypeDefinition() == genericType;
        }
        /// <summary>Determines whether the provided type is a variant of the specified base generic type.</summary>
        /// <param name="type">The type to determine whether it is a generic variant.</param>
        /// <param name="genericType">The base generic type.</param>
        /// <returns><see langword="true"/> if <paramref name="type"/> is a generic variant of <paramref name="genericType"/>, otherwise <see langword="false"/>.</returns>
        /// <exception cref="InvalidOperationException">The <paramref name="genericType"/> argument is not an unbound generic type.</exception>
        public static bool IsGenericVariantOf(this Type type, Type genericType)
        {
            // We want to throw the exception so the check is performed again before checking any other non-exceptional condition
            if (!genericType.IsGenericTypeDefinition)
                ThrowHelper.Throw<InvalidOperationException>("The genericType argument must be an unbound generic type.");
            if (type.IsGenericTypeDefinition)
                return false;

            return type.IsGenericVariantOrDefinition(genericType);
        }
        /// <summary>Determines whether the specified base generic type can be constructed into a type.</summary>
        /// <typeparam name="T">The type to determine whether it can be a construction of the base generic type.</typeparam>
        /// <param name="genericType">The base generic type.</param>
        /// <returns><see langword="true"/> if <paramref name="genericType"/> can be constructed into <typeparamref name="T"/>, otherwise <see langword="false"/>.</returns>
        /// <exception cref="InvalidOperationException">The <paramref name="genericType"/> argument is not an unbound generic type or the <typeparamref name="T"/> argument is not a bound generic type.</exception>
        public static bool CanConstruct<T>(this Type genericType) => genericType.CanConstruct(typeof(T));
        /// <summary>Determines whether the specified base generic type can be constructed into a type.</summary>
        /// <param name="genericType">The base generic type.</param>
        /// <param name="constructedType">The constructed type to determine whether it is constructed from the provided generic type.</param>
        /// <returns><see langword="true"/> if <paramref name="genericType"/> can be constructed into <paramref name="constructedType"/>, otherwise <see langword="false"/>.</returns>
        /// <exception cref="InvalidOperationException">The <paramref name="genericType"/> argument is not an unbound generic type or the <paramref name="constructedType"/> argument is not a bound generic type.</exception>
        public static bool CanConstruct(this Type genericType, Type constructedType) => constructedType.IsGenericVariantOf(genericType);
        #endregion

        #region Type Categories
        // The checks below are based on the estimated commonness of the attributes

        // Local static readonly variables please
        private static readonly string tupleTypeNameStart = typeof(ValueTuple<>).FullName.RemoveLastNumber();

        /// <summary>Determines whether the type is <see langword="void"/>.</summary>
        /// <param name="type">The type to determine whether it is <see langword="void"/>.</param>
        /// <returns>A value determining whether the type is <see langword="void"/>.</returns>
        public static bool IsVoid(this Type type) => type == typeof(void);
        /// <summary>Determines whehter the type is an actual class, and not an array, a delegate, a by reference, or a pointer type.</summary>
        /// <param name="type">The type to determine whether it is a class.</param>
        /// <returns>A value determining whether the type is a class.</returns>
        public static bool IsTrueClass(this Type type) => type.IsClass && !type.IsArray && !type.IsDelegate() && !type.IsPointer && !type.IsByRef;

        /// <summary>Determines whehter the type is a static class.</summary>
        /// <param name="type">The type to determine whether it is a static class.</param>
        /// <returns>A value determining whether the type is a static class.</returns>
        public static bool IsStaticClass(this Type type) => type.IsSealed && type.IsAbstract;

        /// <summary>Determines whether the type is a delegate.</summary>
        /// <param name="type">The type to determine whether it is a delegate.</param>
        /// <returns>A value determining whether the type is a delegate.</returns>
        public static bool IsDelegate(this Type type) => type.InheritsOrEquals<Delegate>();
        /// <summary>Determines whether the type is an exception.</summary>
        /// <param name="type">The type to determine whether it is an exception.</param>
        /// <returns>A value determining whether the type is an exception.</returns>
        public static bool IsException(this Type type) => type.InheritsOrEquals<Exception>();
        /// <summary>Determines whether the type is an attribute.</summary>
        /// <param name="type">The type to determine whether it is an attribute.</param>
        /// <returns>A value determining whether the type is an attribute.</returns>
        // TODO: Remove the generic type check when generic attributes are implemented in the language
        public static bool IsAttribute(this Type type) => !type.IsGenericType && type.InheritsOrEquals<Attribute>();
        /// <summary>Determines whether the type is a tuple; that is, any generic variant of the <seealso cref="ValueTuple"/> struct, nullable or not.</summary>
        /// <param name="type">The type to determine whether it is a tuple.</param>
        /// <returns>A value determining whether the type is a tuple.</returns>
        public static bool IsTuple(this Type type)
        {
            if (type.IsGenericVariantOf(typeof(Nullable<>)))
                return IsTuple(type.GenericTypeArguments[0]);

            // The full name of the type should be System.ValueTuple`N, where N is the number of generic type arguments
            var fullName = type.FullName;

            if (fullName.StartsWith(tupleTypeNameStart))
                return true;

            return false;
        }
        /// <summary>Determines whether the type is a nullable value type; that is, it is a <see cref="Nullable{T}"/> or a pointer type.</summary>
        /// <param name="type">The type to determine whether it is a nullable value type.</param>
        /// <returns><see langword="true"/> if the type is a nullable value type, otherwise <see langword="false"/>.</returns>
        public static bool IsNullableValueType(this Type type) => type.IsGenericVariantOrDefinition(typeof(Nullable<>)) || type.IsPointer;
        /// <summary>Determines whether the type is a nullable type; that is, it is a <see cref="Nullable{T}"/>, a pointer type or a reference type.</summary>
        /// <param name="type">The type to determine whether it is a nullable type.</param>
        /// <returns><see langword="true"/> if the type is a nullable type, either reference or value type, otherwise <see langword="false"/>.</returns>
        public static bool IsNullableType(this Type type) => type.IsReferenceType() || type.IsNullableValueType() || type.IsPointer;
        /// <summary>Determines whether the type is a reference type; that is, not a value type, or a pointer type.</summary>
        /// <param name="type">The type to determine whether it is a reference type.</param>
        /// <returns><see langword="true"/> if the type is a class or an interface, otherwise <see langword="false"/>.</returns>
        public static bool IsReferenceType(this Type type) => (type.IsClass || type.IsInterface) && !type.IsPointer;

        /// <summary>Determines whether the given type is a valid type argument.</summary>
        /// <param name="type">The type to determine whether it is a valid type argument.</param>
        /// <returns><see langword="true"/> if the type is a valid type argument, that is, it is not a by ref, a by ref-like, a pointer, or <see langword="void"/>, otherwise <see langword="false"/>.</returns>
        public static bool IsValidTypeArgument(this Type type) => !type.IsByRef && !type.IsByRefLike && !type.IsPointer && !type.IsVoid();
        #endregion

        #region Arrays
        /// <summary>Creates a jagged array type object with the specified jagging level.</summary>
        /// <param name="type">The type of the elements within the array.</param>
        /// <param name="jaggingLevel">The jagging level of the resulting jagged array type.</param>
        /// <returns>The resulting <seealso cref="Type"/> object representing a jagged array type.</returns>
        public static Type MakeJaggedArrayType(this Type type, int jaggingLevel)
        {
            var currentType = type;
            for (int i = 0; i < jaggingLevel; i++)
                currentType = type.MakeArrayType();
            return currentType;
        }
        /// <summary>Creates a jagged array type object with the specified ranks for each jag.</summary>
        /// <param name="type">The type of the elements within the array.</param>
        /// <param name="ranks">The ranks for each jagging level of the array type. The order is preserved like this: { 1, 2, 3 } => T[][,][,,].</param>
        /// <returns>The resulting <seealso cref="Type"/> object representing a jagged array type.</returns>
        public static Type MakeJaggedArrayType(this Type type, params int[] ranks)
        {
            var currentType = type;
            foreach (var r in ranks)
                currentType = currentType.MakeArrayType(r);
            return currentType;
        }
        /// <summary>Gets the array jagging level of the provided array type.</summary>
        /// <param name="type">The jagged array type.</param>
        /// <returns>The jagging level of the jagged array, if the provided type is an array, otherwise 0.</returns>
        public static int GetArrayJaggingLevel(this Type type)
        {
            var currentType = type;
            int jaggingLevel = 0;
            while (currentType.IsArray)
            {
                currentType = currentType.GetElementType();
                jaggingLevel++;
            }
            return jaggingLevel;
        }
        #endregion

        #region Pointers
        /// <summary>Creates a multiple pointer type object with the specified pointer depth level.</summary>
        /// <param name="type">The type of the elements within the pointer.</param>
        /// <param name="pointerDepthLevel">The depth level of the resulting pointer type.</param>
        /// <returns>The resulting <seealso cref="Type"/> object representing a pointer type.</returns>
        public static Type MakeMultiplePointerType(this Type type, int pointerDepthLevel)
        {
            var currentType = type;
            for (int i = 0; i < pointerDepthLevel; i++)
                currentType = type.MakePointerType();
            return currentType;
        }
        /// <summary>Gets the multiple pointer depth level of the provided pointer type.</summary>
        /// <param name="type">The pointer type.</param>
        /// <returns>The depth level of the multiple pointer, if the provided type is a pointer, otherwise 0.</returns>
        public static int GetMultiplePointerLevel(this Type type)
        {
            var currentType = type;
            int depthLevel = 0;
            while (currentType.IsPointer)
            {
                currentType = currentType.GetElementType();
                depthLevel++;
            }
            return depthLevel;
        }
        #endregion

        #region Element Types
        /// <summary>Gets the deepest element type of the provided type.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The deepest element type according to <seealso cref="Type.GetElementType()"/>.</returns>
        public static Type GetDeepestElementType(this Type type)
        {
            var currentType = type;
            while (currentType.HasElementType)
                currentType = currentType.GetElementType();
            return currentType;
        }
        /// <summary>Determines whether the provided type contains elements of type <paramref name="elementType"/>.</summary>
        /// <param name="type">The type.</param>
        /// <param name="elementType">The type of the elements that the provided type should contain.</param>
        /// <returns>A value determining whether the provided type contains elements of type <paramref name="elementType"/>.</returns>
        public static bool ContainsElementsOfType(this Type type, Type elementType)
        {
            var currentType = type;
            while (currentType.HasElementType)
            {
                currentType = currentType.GetElementType();
                if (currentType == elementType)
                    return true;
            }
            return false;
        }
        /// <summary>Determines whether the provided type contains elements of type <typeparamref name="T"/>.</summary>
        /// <typeparam name="T">The type of the elements that the provided type should contain.</typeparam>
        /// <param name="type">The type.</param>
        /// <returns>A value determining whether the provided type contains elements of type <typeparamref name="T"/>.</returns>
        public static bool ContainsElementsOfType<T>(this Type type)
        {
            return type.ContainsElementsOfType(typeof(T));
        }
        #endregion

        #region Instances
        /// <summary>Gets the public parameterless constructor of the <see cref="Type"/>.</summary>
        /// <param name="type">The type whose public parameterless constructor to get.</param>
        /// <returns>The public parameterless constructor of the type, or <see langword="null"/> if the type does not have one, or if the parameterless constructor is not public.</returns>
        public static ConstructorInfo GetParameterlessConstructor(this Type type)
        {
            return type.GetConstructor(Type.EmptyTypes);
        }
        /// <summary>Gets the parameterless constructor of the <see cref="Type"/>. The constructor may have any accessibility.</summary>
        /// <param name="type">The type whose parameterless constructor to get.</param>
        /// <returns>The parameterless constructor of the type, or <see langword="null"/> if the type does not have one.</returns>
        public static ConstructorInfo GetAnyAccessibilityParameterlessConstructor(this Type type)
        {
            return type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
        }

        /// <summary>Initializes a new instance of a <seealso cref="Type"/> by calling its public parameterless constructor.</summary>
        /// <typeparam name="T">The type of the resulting instance.</typeparam>
        /// <param name="type">The type of the resulting instance.</param>
        /// <returns>The initialized instance, or <see langword="null"/> if no public parameterless constructor was not found.</returns>
        public static T InitializeInstance<T>(this Type type)
        {
            return (T)InitializeInstance(type);
        }
        /// <summary>Initializes a new instance of a <seealso cref="Type"/> with the given parameters in the constructor.</summary>
        /// <typeparam name="T">The type of the resulting instance.</typeparam>
        /// <param name="type">The type of the resulting instance.</param>
        /// <param name="parameters">The parameters of the constructor.</param>
        /// <returns>The initialized instance, or <see langword="null"/> if such a constructor was not found.</returns>
        public static T InitializeInstance<T>(this Type type, params object?[]? parameters)
        {
            return (T)InitializeInstance(type, parameters);
        }

        /// <summary>Initializes a new instance of a <seealso cref="Type"/> by calling its public parameterless constructor.</summary>
        /// <param name="type">The type of the resulting instance.</param>
        /// <returns>The initialized instance, or <see langword="null"/> if no public parameterless constructor was not found.</returns>
        public static object InitializeInstance(this Type type)
        {
            return type.GetParameterlessConstructor()?.Invoke(null);
        }
        /// <summary>Initializes a new instance of a <seealso cref="Type"/> with the given parameters in the constructor.</summary>
        /// <param name="type">The type of the resulting instance.</param>
        /// <param name="parameters">The parameters of the constructor.</param>
        /// <returns>The initialized instance, or <see langword="null"/> if such a constructor was not found.</returns>
        public static object InitializeInstance(this Type type, params object?[]? parameters)
        {
            if (parameters == null)
                return InitializeInstance(type);

            return type.GetConstructor(parameters.Select(p => p?.GetType()).ToArray())?.Invoke(parameters);
        }

        /// <summary>Gets the default value of the given type.</summary>
        /// <param name="type">The type whose default value to get.</param>
        /// <returns>Returns the default value of the type, if it is a value type, by calling its parameterless constructor is called, otherwise <see langword="null"/>.</returns>
        public static object GetDefaultValue(this Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);
            return null;
        }
        #endregion
    }
}
