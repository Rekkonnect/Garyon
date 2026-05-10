using System;
using System.Threading;

namespace Garyon.Extensions;

/// <summary>
/// Provides extensions for <see cref="CancellationTokenSource"/>.
/// </summary>
public static class CancellationTokenSourceExtensions
{
    extension(CancellationTokenSource cancellationTokenSource)
    {
        /// <summary>
        /// Cancels this <see cref="CancellationTokenSource"/> and then disposes it.
        /// </summary>
        public void CancelDispose()
        {
            try
            {
                cancellationTokenSource.Cancel();
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            cancellationTokenSource.Dispose();
        }
    }
}
