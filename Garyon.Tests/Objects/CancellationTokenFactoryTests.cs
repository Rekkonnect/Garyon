using Garyon.Objects;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Objects;

public class CancellationTokenFactoryTests
{
    [Test]
    public async Task CancelDisposesCurrentSourceAndNextTokenIsFreshTest()
    {
        using var factory = new CancellationTokenFactory();

        var token1 = factory.CurrentToken;
        var source1 = factory.CurrentSource;

        factory.Cancel();

        _ = Assert.Throws<ObjectDisposedException>(() => _ = source1.Token);
        await Assert.That(token1.IsCancellationRequested).IsTrue();

        var token2 = factory.CurrentToken;
        await Assert.That(token2.IsCancellationRequested).IsFalse();
        await Assert.That(token1.Equals(token2)).IsFalse();
    }

    [Test]
    public async Task DisposeDoesNotCancelCurrentTokenTest()
    {
        var factory = new CancellationTokenFactory();
        var token = factory.CurrentToken;

        factory.Dispose();

        await Assert.That(token.IsCancellationRequested).IsFalse();
    }
}

