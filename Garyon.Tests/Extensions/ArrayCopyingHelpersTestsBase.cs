namespace Garyon.Tests.Extensions;

public abstract class ArrayCopyingHelpersTestsBase : ArrayManipulationHelpersTestsBase
{
    protected unsafe void CopyArray<TOrigin, TTarget>(TOrigin[] origin, TTarget[] target)
        where TOrigin : unmanaged
        where TTarget : unmanaged
    {
        PerformManipulation(origin, target, GetArrayManipulationOperationDelegate<TOrigin, TTarget>());
    }

    protected override unsafe object GetExpectedResult<TOrigin, TTarget>(TOrigin* origin, int index)
    {
        // That escalated quickly

        if (typeof(TOrigin) == typeof(TTarget))
            return *(TTarget*)&origin[index];

        if (typeof(TOrigin) == typeof(double))
        {
            long result = (long)*(double*)&origin[index];
            return GetExpectedResult<long, TTarget>(&result, 0);
        }
        if (typeof(TOrigin) == typeof(float))
        {
            long result = (long)*(float*)&origin[index];
            return GetExpectedResult<long, TTarget>(&result, 0);
        }

        if (sizeof(TOrigin) > sizeof(TTarget))
        {
            var result = *(TTarget*)&origin[index];
            return result;
        }

        return base.GetExpectedResult<TOrigin, TTarget>(origin, index);
    }

    protected unsafe virtual ArrayManipulationOperation<TOrigin, TTarget> GetArrayManipulationOperationDelegate<TOrigin, TTarget>()
        where TOrigin : unmanaged
        where TTarget : unmanaged
    {
        return null;
    }
}
