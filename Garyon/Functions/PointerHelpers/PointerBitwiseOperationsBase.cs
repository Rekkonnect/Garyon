namespace Garyon.Functions.PointerHelpers;

/// <summary>The base class for pointer bitwise operations classes.</summary>
public abstract unsafe class PointerBitwiseOperationsBase
{
    /// <summary>Represents a function that performs a bitwise operation on an array and stores the results on another array.</summary>
    /// <typeparam name="T">The type of elements that are stored in the arrays.</typeparam>
    /// <param name="origin">The origin array passed as a <typeparamref name="T"/>*.</param>
    /// <param name="target">The target array passed as a <typeparamref name="T"/>*.</param>
    /// <param name="mask">The mask of the bitwise operation to be performed.</param>
    /// <param name="length">The number of elements to perform the operation on.</param>
    /// <returns>A value determining whether the operation was successfully performed or not.</returns>
    public delegate bool ArrayBitwiseOperation<T>(T* origin, T* target, T mask, uint length)
        where T : unmanaged;

    /// <summary>Returns an <seealso cref="ArrayBitwiseOperation{T}"/> that suits the provided bitwise operation that is desired to be performed.</summary>
    /// <typeparam name="T">The type of the elements to perform the bitwise operation on.</typeparam>
    /// <param name="operation">The bitwise operation to perform.</param>
    /// <returns>The appropriate <seealso cref="ArrayBitwiseOperation{T}"/> delegate for the provided bitwise operation.</returns>
    public delegate ArrayBitwiseOperation<T> ArrayBitwiseOperationDelegateReturner<T>(BitwiseOperation operation)
        where T : unmanaged;

    /// <summary>Contains all the supported bitwise operations.</summary>
    public enum BitwiseOperation
    {
        NOT,
        AND,
        OR,
        XOR,
        NAND,
        NOR,
        XNOR
    }
}