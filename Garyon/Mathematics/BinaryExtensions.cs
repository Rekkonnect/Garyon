#if HAS_INUMBER
using System.Numerics;

namespace Garyon.Mathematics;

public static class BinaryExtensions
{
    extension<T>(T value)
        where T : IBitwiseOperators<T, T, T>, IEqualityOperators<T, T, bool>
    {
        public bool HasFlag(T flag)
        {
            return (value & flag) == flag;
        }

        public T RemoveFlag(T flag)
        {
            return value & ~flag;
        }
    }
}
#endif
