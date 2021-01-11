using System;

namespace Garyon.Reflection
{
    /// <summary>Contains values representing the kind of a <seealso cref="Type"/>.</summary>
    [Flags]
    public enum TypeKind : ushort
    {
        /// <summary>The default value that represents no type kind.</summary>
        None = 0,

        /// <summary>The <see langword="void"/> type.</summary>
        Void = 0x1,

        /// <summary>A class type.</summary>
        Class = 0x2,
        /// <summary>A struct type.</summary>
        Struct = 0x4,
        /// <summary>An interface type.</summary>
        Interface = 0x8,
        /// <summary>An enum type.</summary>
        Enum = 0x10,
        /// <summary>A delegate type.</summary>
        Delegate = 0x20,

        /// <summary>A pointer type.</summary>
        Pointer = 0x40,
        /// <summary>An array type.</summary>
        Array = 0x80,

        /// <summary>A by reference type.</summary>
        ByRef = 0x100,

        /// <summary>An exception type.</summary>
        Exception = 0x200 | Class,
        /// <summary>An attribute type.</summary>
        Attribute = 0x400 | Class,

        /// <summary>A tuple type.</summary>
        Tuple = 0x800,

        /// <summary>A nullable struct type.</summary>
        NullableStruct = Struct | 0x1000,
        /// <summary>A nullable tuple type.</summary>
        NullableTuple = NullableStruct | Tuple,
    }
}
