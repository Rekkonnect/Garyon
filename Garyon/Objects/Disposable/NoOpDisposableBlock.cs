using System;

namespace Garyon.Objects.Disposable;

/// <summary>
/// Provides a disposable block that does nothing upon disposal.
/// </summary>
/// <remarks>
/// Consider using this struct in scenarios where a disposable object is required,
/// but no actual disposal logic is necessary.
/// <br/>
/// Another emerging pattern can be decorating a block of operations to avoid
/// having stranded braces enclosing the block.
/// </remarks>
public readonly struct NoOpDisposableBlock : IDisposable
{
    // Do nothing
    void IDisposable.Dispose() { }
}
