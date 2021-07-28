// For the time being this is a custom symbol to prevent compilation of this file
// since the runtime will prevent loading such types

/************************************ NOTE ************************************
 *                         Notes taken from discussions                       *
 *                                                                            *
 *                                                                            *
 * The constraint of generic structs with explicit layout is because there    *
 * can be GC corruptions if reference and value types overlap and a value is  *
 * adjusted in a catastrophic way. This would mean that there would have to   *
 * be a per-combination evaluation during type load of this situation.        *
 ******************************************************************************/

/*
 * CATASTROPHIC EXAMPLE
 * 
 * +-------------------+
 * |*******************| (struct - 19 bytes)
 * |   ********        | (reference 0 - 8 bytes)
 * |           ********| (reference 1 - 8 bytes)
 * |********           | (reference 2 - 8 bytes) overlaps with reference 0 without being aligned
 * |           ********| (reference 3 - 8 bytes) overlaps with reference 1 but is aligned, so no problem
 * |**                 | (value 0 - 2 bytes)
 * |****               | (value 1 - 4 bytes) overlaps with reference 0
 * +-------------------+
 */

#if true
using System.Runtime.InteropServices;

namespace Garyon.Objects
{
    /// <summary>Represents a value-typed union containing a value that can be represented as 2 distinct types.</summary>
    /// <typeparam name="T1">The first type that the value may be represented as.</typeparam>
    /// <typeparam name="T2">The second type that the value may be represented as.</typeparam>
    /// <remarks>The union is always zero-initialized before assigning any values to it, in the case of varying type argument sizes.</remarks>
    [StructLayout(LayoutKind.Explicit)]
    public struct ValueUnion<T1, T2> : IUnion<T1, T2>
    {
        /// <summary>Represents the stored value in the union as one of the first specified type.</summary>
        [FieldOffset(0)]
        public T1 Value1;
        /// <summary>Represents the stored value in the union as one of the second specified type.</summary>
        [FieldOffset(0)]
        public T2 Value2;

        T1 IUnion<T1, T2>.Value1 => Value1;
        T2 IUnion<T1, T2>.Value2 => Value2;

        /// <summary>Initializes a new instance of the <seealso cref="ValueUnion{T1, T2}"/> struct, from a value of type <typeparamref name="T1"/>.</summary>
        /// <param name="value">The value of <typeparamref name="T1"/> to initialize the union from.</param>
        public ValueUnion(T1 value)
            : this()
        {
            Value1 = value;
        }
        /// <summary>Initializes a new instance of the <seealso cref="ValueUnion{T1, T2}"/> struct, from a value of type <typeparamref name="T2"/>.</summary>
        /// <param name="value">The value of <typeparamref name="T2"/> to initialize the union from.</param>
        public ValueUnion(T2 value)
            : this()
        {
            Value2 = value;
        }

        /// <inheritdoc cref="IUnion.TryGet(int, out object?)"/>
        public bool TryGet(int index, out object? value) => (this as IUnion).TryGet(index, out value);

        // TODO: Add setter?
        /// <inheritdoc cref="IUnion.this[int]"/>
        public object? this[int index]
        {
            get => (this as IUnion)[index];
        }
    }
}
#endif