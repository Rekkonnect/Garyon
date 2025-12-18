using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using UsableValueTask =
#if HAS_VALUE_TASK
    System.Threading.Tasks.ValueTask
#else
    System.Threading.Tasks.Task
#endif
    ;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for <see cref="CancellationToken"/>.
/// </summary>
public static class CancellationTokenExtensions
{
    extension(CancellationToken cancellationToken)
    {
        /// <summary>
        /// Creates a <see cref="CancellationTokenSource"/> that links
        /// this <see cref="CancellationToken"/> with another.
        /// </summary>
        public CancellationTokenSource CreateLinked(CancellationToken other)
        {
            return CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, other);
        }

        /// <summary>
        /// Creates a <see cref="CancellationTokenSource"/> that links
        /// this <see cref="CancellationToken"/> with others.
        /// </summary>
        public CancellationTokenSource CreateLinked(params IEnumerable<CancellationToken> other)
        {
            return CancellationTokenSource.CreateLinkedTokenSource([cancellationToken, .. other]);
        }

        /// <summary>
        /// Yields execution with <see cref="Task.Yield"/>
        /// and checks for cancellation.
        /// </summary>
        public async UsableValueTask YieldCancellable()
        {
            await Task.Yield();
            cancellationToken.ThrowIfCancellationRequested();
        }
    }
}
