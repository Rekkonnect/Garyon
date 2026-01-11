using System.Runtime.CompilerServices;
using System.Threading;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions to the <see cref="Interlocked"/> type.
/// </summary>
public static class InterlockedExtensions
{
    extension(Interlocked)
    {
        /// <summary>
        /// Atomically reads the value of the specified location.
        /// </summary>
        /// <remarks>
        /// The implementation uses <see cref="Interlocked.CompareExchange{T}(ref T, T, T)"/>
        /// as similarly done in other overloads of Read.
        /// </remarks>
#if HAS_UNSAFE
        public static T? Read<T>(ref readonly T? location)
#if !HAS_INTERLOCKED_COMPARE_EXCHANGE_UNCONSTRAINED
            where T : class
#endif
        {
            return Interlocked.CompareExchange(ref Unsafe.AsRef(in location), default, default);
        }
#else
        public static T? Read<T>(ref T? location)
#if !HAS_INTERLOCKED_COMPARE_EXCHANGE_UNCONSTRAINED
            where T : class
#endif
        {
            return Interlocked.CompareExchange(ref location, default, default);
        }
#endif
    }
}
