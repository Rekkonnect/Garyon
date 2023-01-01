#if HAS_SPAN
using System;
#endif

namespace Garyon.Functions;

public static class PointerFunctions
{
    public static unsafe void Fill<T>(T* ptr, int length, T value)
        where T : unmanaged
    {
#if HAS_SPAN
        var span = new Span<T>(ptr, length);
        span.Fill(value);
#else
        for (int i = 0; i < length; i++)
        {
            ptr[i] = value;
        }
#endif
    }
    public static unsafe void Clear(void* pointer, int bytes)
    {
#if HAS_SPAN
        var span = new Span<byte>(pointer, bytes);
        span.Clear();
#else
        byte* ptr = (byte*)pointer;
        for (int i = 0; i < bytes; i++)
        {
            // Could be optimized further
            // or you could not use pointers in .NET Standard 2.0
            ptr[i] = 0;
        }
#endif
    }
}