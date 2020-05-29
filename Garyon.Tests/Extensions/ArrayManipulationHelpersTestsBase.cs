using Garyon.QualityControl.Extensions;
using Garyon.Tests.Resources;
using NUnit.Framework;
using System;
using static Garyon.Tests.Resources.AssertionHelpers;

namespace Garyon.Tests.Extensions
{
    public abstract class ArrayManipulationHelpersTestsBase : ArrayManipulationExtensionsQualityControlAsset
    {
        protected virtual unsafe object GetExpectedResult<TOrigin, TTarget>(TOrigin* origin, int index)
            where TOrigin : unmanaged
            where TTarget : unmanaged
        {
            return origin[index];
        }

        protected virtual unsafe ArrayManipulationOperation<TOrigin, TTarget> GetArrayManipulationOperationDelegate<TOrigin, TTarget>()
            where TOrigin : unmanaged
            where TTarget : unmanaged
        {
            return null;
        }
        protected virtual unsafe void PerformManipulation<TOrigin, TTarget>(TOrigin[] origin, TTarget[] target, ArrayManipulationOperation<TOrigin, TTarget> operation)
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
        protected unsafe void PerformManipulation<TOrigin, TTarget>(TOrigin[] origin, TTarget[] target)
            where TOrigin : unmanaged
            where TTarget : unmanaged
        {
            PerformManipulation(origin, target, GetArrayManipulationOperationDelegate<TOrigin, TTarget>());
        }

        protected unsafe delegate bool ArrayManipulationOperation<TOrigin, TTarget>(TOrigin* origin, TTarget* target, uint length)
            where TOrigin : unmanaged
            where TTarget : unmanaged;
    }
}
