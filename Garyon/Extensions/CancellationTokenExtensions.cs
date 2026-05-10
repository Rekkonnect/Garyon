using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for <see cref="CancellationToken"/>.
/// </summary>
public static class CancellationTokenExtensions
{
    extension(CancellationToken cancellationToken)
    {
        /// <summary>
        /// Creates a <see cref="CancellationTokenSource"/> that links this
        /// <see cref="CancellationToken"/> with another.
        /// </summary>
        public CancellationTokenSource CreateLinked(CancellationToken other)
        {
            return CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, other);
        }

        /// <summary>
        /// Creates a <see cref="CancellationTokenSource"/> that links this
        /// <see cref="CancellationToken"/> with others.
        /// </summary>
        public CancellationTokenSource CreateLinked(params IEnumerable<CancellationToken> other)
        {
            return CancellationTokenSource.CreateLinkedTokenSource([cancellationToken, .. other]);
        }

        /// <summary>
        /// Yields execution with <see cref="Task.Yield"/> and checks for
        /// cancellation.
        /// </summary>
        public async ValueTask YieldCancellable()
        {
            await Task.Yield();
            cancellationToken.ThrowIfCancellationRequested();
        }

        /// <summary>
        /// Creates a new <see cref="CancellationToken"/> that is linked to this token
        /// and will be cancelled after the specified timeout duration.
        /// </summary>
        public CancellationToken WithTimeout(TimeSpan timeout)
        {
            return cancellationToken.WithTimeout((int)timeout.TotalMilliseconds);
        }

        /// <summary>
        /// Creates a new <see cref="CancellationToken"/> that is linked to this token
        /// and will be cancelled after the specified timeout duration.
        /// </summary>
        public CancellationToken WithTimeout(int milliseconds)
        {
            var timeoutSource = new CancellationTokenSource(milliseconds);
            var linkedSource = cancellationToken.CreateLinked(
                cancellationToken, timeoutSource.Token);
            return linkedSource.Token;
        }
    }
}
