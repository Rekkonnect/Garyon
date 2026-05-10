using Garyon.Objects.Advanced;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Objects;

public class AsyncLazyTests
{
    [Test]
    public async Task AsyncLazyCachesClearsAndRecomputesValuesTest()
    {
        int calls = 0;
        var lazy = new AsyncLazy<int>(async () =>
        {
            await Task.Yield();
            return ++calls;
        });

        await Assert.That(lazy.IsValueCreated).IsFalse();
        await Assert.That(lazy.ValueTaskOrDefault is null).IsTrue();

        await Assert.That(await lazy.GetValueAsync()).IsEqualTo(1);
        await Assert.That(await lazy.GetValueAsync()).IsEqualTo(1);
        await Assert.That(calls).IsEqualTo(1);
        await Assert.That(lazy.IsValueCreated).IsTrue();
        await Assert.That(lazy.ValueTaskOrDefault is not null).IsTrue();

        lazy.ClearValue();

        await Assert.That(lazy.IsValueCreated).IsFalse();
        await Assert.That(await lazy.GetValueAsync()).IsEqualTo(2);
        await Assert.That(calls).IsEqualTo(2);
    }

    [Test]
    public async Task AsyncLazyValueConstructorStartsCreatedTest()
    {
        var lazy = new AsyncLazy<string>("ready");

        await Assert.That(lazy.IsValueCreated).IsTrue();
        await Assert.That(await lazy.GetValueAsync()).IsEqualTo("ready");
        await Assert.That(lazy.ValueTaskOrDefault is not null).IsTrue();
    }
}
