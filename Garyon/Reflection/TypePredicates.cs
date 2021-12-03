using System;

namespace Garyon.Reflection
{
    /// <summary>Contains a collection of predicates to apply on <seealso cref="Type"/> instances. Also contains several aliases for other scattered functions.</summary>
    public static class TypePredicates
    {
        public static bool IsClass(Type type) => type.IsClass;
        public static bool IsAbstractClass(Type type) => type.IsClass && type.IsAbstract;
        public static bool IsNonAbstractClass(Type type) => type.IsClass && !type.IsAbstract;
        public static bool IsValueType(Type type) => type.IsValueType;
        public static bool IsInterface(Type type) => type.IsInterface;
        public static bool IsStatic(Type type) => type.IsStaticClass();
    }
}
