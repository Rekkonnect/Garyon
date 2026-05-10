using Garyon.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Extensions;

public class CancellationTokenSourceExtensionsTests
{
    [Test]
    public async Task CancelDisposeCancelsAndDisposesTest()
    {
        var source = new CancellationTokenSource();

        source.CancelDispose();

        await Assert.That(source.IsCancellationRequested).IsTrue();
        _ = Assert.Throws<ObjectDisposedException>(() => _ = source.Token);
    }

    [Test]
    public void CancelDisposeOnDisposedSourceDoesNotThrowTest()
    {
        var source = new CancellationTokenSource();
        source.Dispose();

        source.CancelDispose();
    }
}

