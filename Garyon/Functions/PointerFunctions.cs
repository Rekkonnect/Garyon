using System;

namespace Garyon.Functions
{
    public static class PointerFunctions
    {
        public static unsafe void Fill<T>(T* ptr, int length, T value)
            where T : unmanaged
        {
            var span = new Span<T>(ptr, length);
            span.Fill(value);
        }
    }
}