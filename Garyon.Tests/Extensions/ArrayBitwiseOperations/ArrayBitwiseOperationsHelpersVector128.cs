using Garyon.Functions.UnmanagedHelpers;

namespace Garyon.Tests.Extensions.ArrayBitwiseOperations
{
    public abstract class ArrayBitwiseOperationsHelpersVector128 : ArrayManipulationHelpersTestsBase
    {
        protected const byte Mask = 11;

        protected virtual unsafe MaskableArrayManipulationOperation<TOrigin> GetMaskableArrayManipulationOperationDelegate<TOrigin>()
            where TOrigin : unmanaged
        {
            return null;
        }

        protected unsafe void PerformManipulation<TOrigin>(TOrigin[] origin, TOrigin[] target)
            where TOrigin : unmanaged
        {
            PerformManipulation(origin, target, GetMaskableArrayManipulationOperationDelegate<TOrigin>());
        }

        // This looks like a mess
        protected virtual unsafe void PerformManipulation<TOrigin>(TOrigin[] origin, TOrigin[] target, ArrayManipulationOperation<TOrigin, TOrigin> operation)
            where TOrigin : unmanaged
        {
            PerformManipulation<TOrigin, TOrigin>(origin, target, operation);
        }
        protected virtual unsafe void PerformManipulation<TOrigin>(TOrigin[] origin, TOrigin[] target, MaskableArrayManipulationOperation<TOrigin> operation)
            where TOrigin : unmanaged
        {
            var finalMask = ValueManipulation.Rescale<byte, TOrigin>(Mask);

            PerformManipulation(origin, target, Operation);

            bool Operation(TOrigin* origin, TOrigin* target, uint length) => operation(origin, target, finalMask, length);
        }
        protected unsafe delegate bool MaskableArrayManipulationOperation<TOrigin>(TOrigin* origin, TOrigin* target, TOrigin mask, uint length)
            where TOrigin : unmanaged;
    }
}
