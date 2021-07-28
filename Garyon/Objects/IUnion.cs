using Garyon.Exceptions;
using System;

namespace Garyon.Objects
{
    /// <summary>Represents a union containing a value that can be represented as any of a number of distinct types.</summary>
    public interface IUnion
    {
        /// <summary>Gets the numer of distinct types this union's value can be represented as.</summary>
        public abstract int DistinctTypeCount { get; }

        /// <summary>Gets the value of the union represented as an object of the type at the specified index.</summary>
        /// <param name="index">The index of the type to represent the value as. The index is 1-based, meaning the first type is at index 1. Index 0 is invalid.</param>
        /// <returns>The value of the union, if the index of the type is valid, and the type at that index is allowed to represent the stored value, otherwise the respective exception is thrown.</returns>
        /// <remarks>Consider using <seealso cref="TryGet(int, out object?)"/> if exceptions are not desired.</remarks>
        /// <exception cref="IndexOutOfRangeException">The index of the value is invalid.</exception>
        /// <exception cref="InvalidOperationException">The stored value in the union is not of the type specified by the index.</exception>
        public object? this[int index]
        {
            get
            {
                var error = ErrorForTypeIndex(index);
                switch (error)
                {
                    case UnionTypeIndexError.OutOfBounds:
                        return ThrowHelper.Throw<IndexOutOfRangeException>("The provided index is invalid.");
                    case UnionTypeIndexError.InvalidType:
                        return ThrowHelper.Throw<InvalidOperationException>("The type at the provided index is not a valid type of the union's value.");

                    case UnionTypeIndexError.None:
                        return GetValueAtIndex(index);

                    default:
                        return null;
                }
            }
        }

        /// <summary>Gets the value of the union represented as an object of the type at the specified index.</summary>
        /// <param name="index">The index of the type to represent the value as. The index is 1-based, meaning the first type is at index 1. Index 0 is invalid.</param>
        /// <param name="value">The value that the union stores, if the index of the type is valid and the type at that index is allowed to represent the stored value, otherwise <see langword="null"/>.</param>
        /// <returns>A value determining whether the operation succeeded.</returns>
        public sealed bool TryGet(int index, out object? value)
        {
            value = GetValueAtIndex(index);
            return ErrorForTypeIndex(index) is UnionTypeIndexError.None;
        }

        /// <summary>Gets an error regarding the type of the index for this union.</summary>
        /// <param name="index">The index of the type to represent the value as.</param>
        /// <returns>An error code representing the error that would be presented upon attempting to get the value of the union as the type at the specified index.</returns>
        private UnionTypeIndexError ErrorForTypeIndex(int index)
        {
            if (index < 1 || index > DistinctTypeCount)
                return UnionTypeIndexError.OutOfBounds;

            if (!IsValidTypeIndex(index))
                return UnionTypeIndexError.InvalidType;

            return UnionTypeIndexError.None;
        }

        /// <summary>Determines whether the type at the specified index is a valid type to represent the union's value as.</summary>
        /// <param name="index">The index of the desired type to represent the union's value as.</param>
        /// <returns><see langword="true"/> if the index of the desired type is valid, otherwise <see langword="false"/>.</returns>
        protected internal virtual bool IsValidTypeIndex(int index) => true;

        /// <summary>Gets an error regarding the type of the index for this </summary>
        /// <param name="index">The index of the type to represent the value as.</param>
        /// <returns>The value of the union represented as an object of the type at the specified index.</returns>
        protected internal abstract object? GetValueAtIndex(int index);

        /// <summary>Represents an error related to getting the value of the union as the type at the specified index.</summary>
        private enum UnionTypeIndexError
        {
            /// <summary>The operation was successful, producing no errors.</summary>
            None,
            /// <summary>The provided index was out of bounds.</summary>
            OutOfBounds,
            /// <summary>The type at the specified index is not the valid type of the value in the union.</summary>
            InvalidType,
        }
    }
}
