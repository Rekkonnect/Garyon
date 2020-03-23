using System;

namespace Garyon.Exceptions
{
    public static class ThrowHelper
    {
        public static void Throw<T>()
            where T : Exception, new()
        {
            throw new T();
        }
    }
}
