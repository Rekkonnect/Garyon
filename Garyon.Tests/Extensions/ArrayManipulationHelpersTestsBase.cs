using Garyon.Functions.UnmanagedHelpers;
using Garyon.QualityControl.Extensions;
using Garyon.Tests.Resources;
using NUnit.Framework;
using System;
using static Garyon.Tests.Resources.AssertionHelpers;

namespace Garyon.Tests.Extensions
{
    public abstract class ArrayManipulationHelpersTestsBase : ArrayManipulationExtensionsQualityControlAsset
    {
        protected const byte Mask = 11;

        protected unsafe void PerformManipulationArray<TOrigin>(ArrayManipulationOperation<TOrigin, TOrigin> operation)
            where TOrigin : unmanaged
        {
            PerformManipulation(new TOrigin[ArrayLength], new TOrigin[ArrayLength], operation);
        }
        protected unsafe void PerformManipulationArray<TOrigin>(MaskableArrayManipulationOperation<TOrigin> operation)
            where TOrigin : unmanaged
        {
            PerformManipulation(new TOrigin[ArrayLength], new TOrigin[ArrayLength], operation);
        }

        protected unsafe void PerformManipulation<TOrigin, TTarget>(TOrigin[] origin, TTarget[] target, ArrayManipulationOperation<TOrigin, TTarget> operation)
            where TOrigin : unmanaged
            where TTarget : unmanaged
        {
            DebugAssertionHelpers.TestArrays(origin, target);

            fixed (TOrigin* o = origin)
            fixed (TTarget* t = target)
            {
                if (!operation(o, t, ArrayLength))
                    UnsupportedInstructionSet();

                for (int i = 0; i < ArrayLength; i++)
                    Assert.AreEqual(GetExpectedResult<TOrigin, TTarget>(o, i), target[i], $"{i}/{ArrayLength}");
            }

            // Clear for debugging purposes
            Array.Clear(target, 0, ArrayLength);
        }
        protected virtual unsafe void PerformManipulation<TOrigin>(TOrigin[] origin, TOrigin[] target, MaskableArrayManipulationOperation<TOrigin> operation)
            where TOrigin : unmanaged
        {
            var finalMask = ValueManipulation.Rescale<byte, TOrigin>(Mask);

            PerformManipulation(origin, target, Operation);

            bool Operation(TOrigin* origin, TOrigin* target, uint length) => operation(origin, target, finalMask, length);
        }

        protected virtual unsafe object GetExpectedResult<TOrigin, TTarget>(TOrigin* origin, int index)
            where TOrigin : unmanaged
            where TTarget : unmanaged
        {
            return origin[index];
        }

        #region Delegates
        /// <summary>Represents an array manipulation operation, storing the results of the operation in a different array.</summary>
        /// <typeparam name="TOrigin">The type of the elements stored in the origin sequence.</typeparam>
        /// <typeparam name="TTarget">The type of the elements stored in the target sequence.</typeparam>
        /// <param name="origin">The pointer to the start of the origin sequence.</param>
        /// <param name="target">The pointer to the start of the target sequence.</param>
        /// <param name="length">The number of elements to perform the operation on.</param>
        /// <returns><see langword="true"/> if the operation was successfully performed, otherwise <see langword="false"/>.</returns>
        protected unsafe delegate bool ArrayManipulationOperation<TOrigin, TTarget>(TOrigin* origin, TTarget* target, uint length)
            where TOrigin : unmanaged
            where TTarget : unmanaged;

        /// <summary>Represents an array manipulation operation that applies a mask to all elements, storing the results of the operation in a different array.</summary>
        /// <typeparam name="TOrigin">The type of the elements stored in the origin sequence.</typeparam>
        /// <param name="origin">The pointer to the start of the origin sequence.</param>
        /// <param name="target">The pointer to the start of the target sequence.</param>
        /// <param name="mask">The mask to apply to all elements of the origin sequence.</param>
        /// <param name="length">The number of elements to perform the operation on.</param>
        /// <returns><see langword="true"/> if the operation was successfully performed, otherwise <see langword="false"/>.</returns>
        protected unsafe delegate bool MaskableArrayManipulationOperation<TOrigin>(TOrigin* origin, TOrigin* target, TOrigin mask, uint length)
            where TOrigin : unmanaged;
        #endregion
    }
}
