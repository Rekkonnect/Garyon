using Garyon.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.Objects;

public class MessageRequestChannelTests
{
    [Test]
    public async Task EmptyRequest()
    {
        var channel = MessageRequestChannel.Create(
            new()
            {
                SingleWriter = true,
            });

        await Assert.That(channel.ConsumeAllRequests()).IsFalse();
    }

    [Test]
    public async Task SingleItemConsumption()
    {
        var channel = MessageRequestChannel.Create(
            new()
            {
                SingleWriter = true,
            });

        await Assert.That(channel.ConsumeAllRequests()).IsFalse();

        await channel.WriteOne();
        await Assert.That(channel.ConsumeAllRequests()).IsTrue();

        await Assert.That(channel.ConsumeAllRequests()).IsFalse();
    }

    [Test]
    public async Task MultiThreadItemsConsumption()
    {
        var channel = MessageRequestChannel.Create(
            new()
            {
                SingleWriter = false,
            });

        var tasks = new List<Task>();
        for (int i = 0; i < 10; i++)
        {
            var task = Task.Run(() => channel.WriteOne());
            tasks.Add(task);
        }

        await Task.WhenAll(tasks);
        await Assert.That(channel.ConsumeAllRequests()).IsTrue();

        await Assert.That(channel.ConsumeAllRequests()).IsFalse();
    }
}