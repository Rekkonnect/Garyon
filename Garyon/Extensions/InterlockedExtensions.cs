using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions to the <see cref="Interlocked"/> type.
/// </summary>
[ExcludeFromCodeCoverage]
public static class InterlockedExtensions
{
    extension(Interlocked)
    {
        /// <summary>
        /// Atomically reads the value of the specified location.
        /// </summary>
        /// <remarks>
        /// The implementation uses
        /// <see cref="Interlocked.CompareExchange{T}(ref T, T, T)"/> as
        /// similarly done in other overloads of Read.
        /// </remarks>
        public static T? Read<T>(ref readonly T? location)
            where T : class
        {
            return Interlocked.CompareExchange(ref Unsafe.AsRef(in location), default, default);
        }
    }
}
