using System.Collections.Generic;
using System.Threading;

namespace Garyon.Objects;

/// <summary>
/// Provides a <see cref="CancellationTokenSource"/> that will be re-instantiated
/// after every cancellation.
/// </summary>
public sealed class CancellationTokenFactory
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
            if (_currentSource is null)
            {
                return CreateSource();
            }

            if (_currentSource.IsCancellationRequested)
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
    /// <returns>The newly created <see cref="CancellationTokenSource"/>.</returns>
    public CancellationTokenSource CreateSource()
    {
        _currentSource = new CancellationTokenSource();
        return _currentSource;
    }

    /// <summary>
    /// Cancels the current <see cref="CancellationToken"/>. This does not
    /// automatically trigger the instantiation of the new
    /// <see cref="CancellationTokenSource"/>.
    /// </summary>
    public void Cancel()
    {
        _currentSource?.Cancel();
    }
}
