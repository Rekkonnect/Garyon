using System;

namespace Garyon.Reflection
{
    /// <summary>Contains information about the definition of a <seealso cref="Type"/>.</summary>
    public class TypeDefinitionInfo
    {
        /// <summary>The kind of the type.</summary>
        public TypeKind Kind;
        /// <summary>The modifiers of the type.</summary>
        public TypeModifiers Modifiers;

        /// <summary>Determines whether the type is either a class or an array. For the non-array variant, use <see cref="IsTrueClass"/>.</summary>
        public bool IsClass => IsTrueClass || IsArray;
        /// <summary>Determines whether the type is a class.</summary>
        public bool IsTrueClass => Kind.HasFlag(TypeKind.Class);
        /// <summary>Determines whether the type is a struct.</summary>
        public bool IsStruct => Kind == TypeKind.Struct;
        /// <summary>Determines whether the type is an interface.</summary>
        public bool IsInterface => Kind == TypeKind.Interface;
        /// <summary>Determines whether the type is a delegate.</summary>
        public bool IsDelegate => Kind == TypeKind.Delegate;
        /// <summary>Determines whether the type is an exception.</summary>
        public bool IsException => Kind == TypeKind.Exception;
        /// <summary>Determines whether the type is an attribute.</summary>
        public bool IsAttribute => Kind == TypeKind.Attribute;
        /// <summary>Determines whether the type is an enum.</summary>
        public bool IsEnum => Kind == TypeKind.Enum;
        /// <summary>Determines whether the type is by ref.</summary>
        public bool IsByRef => Kind == TypeKind.ByRef;
        /// <summary>Determines whether the type is an array.</summary>
        public bool IsArray => Kind == TypeKind.Array;
        /// <summary>Determines whether the type is a pointer.</summary>
        public bool IsPointer => Kind == TypeKind.Pointer;
        /// <summary>Determines whether the type is <see langword="void"/>.</summary>
        public bool IsVoid => Kind == TypeKind.Void;
        /// <summary>Determines whether the type is a tuple.</summary>
        public bool IsTuple => Kind.HasFlag(TypeKind.Tuple);
        /// <summary>Determines whether the type is a nullable struct.</summary>
        public bool IsNullableStruct => Kind.HasFlag(TypeKind.NullableStruct);
        /// <summary>Determines whether the type is a nullable tuple.</summary>
        public bool IsNullableTuple => Kind.HasFlag(TypeKind.NullableTuple);

        /// <summary>Determines whether the type is a reference type; that is, any class, interface, delegate or by reference type. Pointers do not count as reference types.</summary>
        public bool IsReferenceType => IsClass || IsInterface || IsDelegate || IsByRef;
        /// <summary>Determines whether the type is a reference type; that is, any struct, enum, tuple or pointer type.</summary>
        public bool IsValueType => IsStruct || IsEnum || IsTuple || IsPointer;
        /// <summary>Determines whether the type is a nullable value type; that is, any nullable struct or pointer type.</summary>
        public bool IsNullableValueType => IsNullableStruct || IsPointer;
        /// <summary>Determines whether the type is a nullable type; that is, any reference, nullable struct or pointer type.</summary>
        public bool IsNullableType => IsReferenceType || IsNullableValueType;

        /// <summary>Determines whether the type is <see langword="public"/>.</summary>
        public bool IsPublic => Modifiers.HasFlag(TypeModifiers.Public);
        /// <summary>Determines whether the type is <see langword="internal"/>.</summary>
        public bool IsInternal => Modifiers.HasFlag(TypeModifiers.Internal);
        /// <summary>Determines whether the type is <see langword="protected internal"/>.</summary>
        public bool IsProtectedInternal => Modifiers.HasFlag(TypeModifiers.ProtectedInternal);
        /// <summary>Determines whether the type is <see langword="protected"/>.</summary>
        public bool IsProtected => Modifiers.HasFlag(TypeModifiers.Protected);
        /// <summary>Determines whether the type is <see langword="private protected"/>.</summary>
        public bool IsPrivateProtected => Modifiers.HasFlag(TypeModifiers.PrivateProtected);
        /// <summary>Determines whether the type is <see langword="private"/>.</summary>
        public bool IsPrivate => Modifiers.HasFlag(TypeModifiers.Private);

        /// <summary>Determines whether the type is <see langword="static"/>.</summary>
        public bool IsStatic => Modifiers.HasFlag(TypeModifiers.Static);
        /// <summary>Determines whether the type is <see langword="abstract"/>.</summary>
        public bool IsAbstract => Modifiers.HasFlag(TypeModifiers.Abstract);
        /// <summary>Determines whether the type is <see langword="sealed"/>.</summary>
        public bool IsSealed => Modifiers.HasFlag(TypeModifiers.Sealed);
        /// <summary>Determines whether the type is <see langword="ref"/>. NOTE: This property will always return <see langword="false"/> for ref struct type definitions, since the reflection API does not have any mechanism to determine that. However, by ref types (types with the ref modifier in argument type declarations) will be detected as such.</summary>
        public bool IsRef => Modifiers.HasFlag(TypeModifiers.Ref);
        /// <summary>Determines whether the type is <see langword="readonly"/>. NOTE: This property will always return <see langword="false"/> since the reflection API does not have any mechanism to determine that.</summary>
        public bool IsReadonly => Modifiers.HasFlag(TypeModifiers.Readonly);
        /// <summary>Determines whether the type is <see langword="readonly ref"/>. NOTE: This property will always return <see langword="false"/> since the reflection API does not have any mechanism to determine that.</summary>
        public bool IsReadonlyRef => Modifiers.HasFlag(TypeModifiers.ReadonlyRef);

        #region Inheritance
        /// <summary>Determines whether this type can inherit any type, either a class or an interface.</summary>
        /// <returns><see langword="true"/> if the type can inherit any type; that is, it is not a pointer or a by reference type, otherwise <see langword="false"/>.</returns>
        public bool CanInherit => !IsPointer && !IsByRef;
        /// <summary>Determines whether this type can inherit any type, either a class or an interface.</summary>
        /// <returns><see langword="true"/> if the type can inherit any type; that is, it is not a pointer, a by reference, a static class, a delegate, an enum or an array type, otherwise <see langword="false"/>.</returns>
        public bool CanInheritCustomTypes => CanInherit && !IsStatic && !IsDelegate && !IsEnum && !IsArray;
        /// <summary>Determines whether this type can inherit any interface.</summary>
        /// <returns><see langword="true"/> if the type can inherit any interface; that is, it is not a pointer, a by reference, a static class, a delegate, an enum or an array type, otherwise <see langword="false"/>.</returns>
        public bool CanInheritInterfaces => CanInheritCustomTypes;
        /// <summary>Determines whether this type can inherit any class.</summary>
        /// <returns><see langword="true"/> if the type can inherit any interface; that is, it is not a pointer, a by reference, or a delegate type, otherwise <see langword="false"/>.</returns>
        public bool CanInheritClasses => CanInherit && !IsInterface;
        /// <summary>Determines whether this type can inherit any custom class.</summary>
        /// <returns><see langword="true"/> if the type can inherit any interface; that is, it is not a pointer, a by reference, a static class, or a delegate type, otherwise <see langword="false"/>.</returns>
        public bool CanInheritCustomClasses => IsTrueClass && !IsStatic;
        /// <summary>Determines whether this type can be inherited by any type.</summary>
        /// <returns><see langword="true"/> if the type can be inherited by any type; that is it is not sealed, is a true class or an interface, otherwise <see langword="false"/>.</returns>
        public bool CanBeInherited => !IsSealed && (IsTrueClass || IsInterface);
        #endregion

        /// <summary>Initializes a new instance of the <seealso cref="TypeDefinitionInfo"/> class.</summary>
        public TypeDefinitionInfo() { }
        /// <summary>Initializes a new instance of the <seealso cref="TypeDefinitionInfo"/> class from a type.</summary>
        /// <param name="type">The type whose type definition info to get.</param>
        public TypeDefinitionInfo(Type type)
        {
            InitializeFromType(type);
        }
        /// <summary>Initializes a new instance of the <seealso cref="TypeDefinitionInfo"/> class from a type kind and the type modifiers.</summary>
        /// <param name="kind">The kind of the type.</param>
        /// <param name="modifiers">The modifiers of the type.</param>
        public TypeDefinitionInfo(TypeKind kind, TypeModifiers modifiers)
        {
            Kind = kind;
            Modifiers = modifiers;
        }

        private void InitializeFromType(Type type)
        {
            Kind = GetTypeKind(type);
            Modifiers = GetTypeModifiers(type);
        }

        // TODO: Move to some other class that handles that API
        /// <summary>Gets the type kind of the type.</summary>
        /// <param name="type">The type whose type kind to get.</param>
        /// <returns>A <seealso cref="TypeKind"/> which determines the type's type kind.</returns>
        public static TypeKind GetTypeKind(Type? type)
        {
            if (type == null)
                return default;

            if (type.IsPointer)
                return TypeKind.Pointer;
            if (type.IsEnum)
                return TypeKind.Enum;
            if (type.IsArray)
                return TypeKind.Array;
            if (type.IsByRef)
                return TypeKind.ByRef;
            if (type.IsInterface)
                return TypeKind.Interface;
            if (type.IsDelegate())
                return TypeKind.Delegate;
            if (type.IsException())
                return TypeKind.Exception;
            if (type.IsAttribute())
                return TypeKind.Attribute;
            if (type.IsVoid())
                return TypeKind.Void;
            if (type.IsTuple())
            {
                if (type.IsNullableValueType())
                    return TypeKind.NullableTuple;
                return TypeKind.Tuple;
            }
            if (type.IsNullableValueType())
                return TypeKind.NullableStruct;
            if (type.IsValueType)
                return TypeKind.Struct;
            if (type.IsClass)
                return TypeKind.Class;

            return default;
        }
        /// <summary>Gets the type modifiers of the type.</summary>
        /// <param name="type">The type whose type modifiers to get.</param>
        /// <returns>A <seealso cref="TypeModifiers"/> value that contains the type modifiers.</returns>
        public static TypeModifiers GetTypeModifiers(Type? type)
        {
            if (type == null)
                return default;

            var result = TypeModifiers.None;

            if (type.IsStaticClass())
                result |= TypeModifiers.Static;
            else
            {
                // The API is constructed in a weird way
                if (type.IsSealed)
                    result |= TypeModifiers.Sealed;
                if (type.IsAbstract)
                    result |= TypeModifiers.Abstract;
            }

            if (type.IsByRef || type.IsByRefLike)
                result |= TypeModifiers.Ref;

            // There is no way to determine anything else
            // TODO: Consider removing the type modifiers that are undetectable (readonly, readonly ref)

            // TODO: Move accessibility identification logic into a more generalized environment to support other member types
            // Top-level types can only be public or internal
            if (!type.IsNested)
            {
                if (type.IsPublic)
                    result |= TypeModifiers.Public;
                else
                    result |= TypeModifiers.Internal;
            }
            // Nested types require looking through the specific properties for nested types
            else
            {
                if (type.IsNestedPublic)
                    result |= TypeModifiers.Public;
                else if (type.IsNestedAssembly)
                    result |= TypeModifiers.Internal;
                else if (type.IsNestedFamORAssem)
                    result |= TypeModifiers.ProtectedInternal;
                else if (type.IsNestedFamily)
                    result |= TypeModifiers.Protected;
                else if (type.IsNestedFamANDAssem)
                    result |= TypeModifiers.PrivateProtected;
                else if (type.IsNestedPrivate)
                    result |= TypeModifiers.Private;
            }

            return result;
        }

        public bool Equals(TypeDefinitionInfo other) => Kind == other.Kind && Modifiers == other.Modifiers;
        public override bool Equals(object? obj) => Equals((TypeDefinitionInfo)obj);
        public override string ToString() => $"{Kind} - {Modifiers}";
        public override int GetHashCode() => HashCode.Combine(Kind, Modifiers);
    }
}
