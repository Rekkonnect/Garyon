using System.Threading;

using System;
using Garyon.Extensions;

namespace Garyon.Objects;

/// <summary>
/// Provides a <see cref="CancellationTokenSource"/> that will be
/// re-instantiated after every cancellation.
/// </summary>
public sealed class CancellationTokenFactory : IDisposable
{
    private CancellationTokenSource? _currentSource;

    /// <summary>
    /// The current <see cref="CancellationTokenSource"/> that provides the
    /// current non-cancelled <see cref="CancellationToken"/>. If the last
    /// stored <see cref="CancellationTokenSource"/> had a requested
    /// cancellation, a new one will be created.
    /// </summary>
    public CancellationTokenSource CurrentSource
    {
        get
        {
            if (_currentSource is null or { IsCancellationRequested: true })
            {
                return CreateSource();
            }

            return _currentSource;
        }
    }

    /// <summary>
    /// The current <see cref="CancellationToken"/> that is not yet cancelled.
    /// </summary>
    public CancellationToken CurrentToken => CurrentSource.Token;

    /// <summary>
    /// Forces the creation of a new <see cref="CancellationTokenSource"/>.
    /// </summary>
    /// <remarks>
    /// This disposes the currently stored <see cref="CancellationTokenSource"/>
    /// (without cancelling it) and replaces it with a new instance.
    /// </remarks>
    /// <returns>
    /// The newly created <see cref="CancellationTokenSource"/>.
    /// </returns>
    public CancellationTokenSource CreateSource()
    {
        _currentSource?.Dispose();
        _currentSource = new CancellationTokenSource();
        return _currentSource;
    }

    /// <summary>
    /// Cancels the current <see cref="CancellationToken"/>. This does not
    /// automatically trigger the instantiation of the new
    /// <see cref="CancellationTokenSource"/>.
    /// </summary>
    /// <remarks>
    /// This cancels and disposes the currently stored
    /// <see cref="CancellationTokenSource"/>.
    /// </remarks>
    public void Cancel()
    {
        _currentSource?.CancelDispose();
        _currentSource = null;
    }

    /// <summary>
    /// Disposes the currently stored <see cref="CancellationTokenSource"/> without
    /// cancelling it.
    /// </summary>
    public void Dispose()
    {
        _currentSource?.Dispose();
        _currentSource = null;
        GC.SuppressFinalize(this);
    }
}
