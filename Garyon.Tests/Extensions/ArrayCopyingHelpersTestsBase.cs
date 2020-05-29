namespace Garyon.Tests.Extensions
{
    public abstract class ArrayCopyingHelpersTestsBase : ArrayManipulationHelpersTestsBase
    {
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
    }
}
