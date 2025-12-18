#if HAS_CHANNELS

using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Garyon.Objects;

/// <summary>
/// Represents a message request channel that allows writing requests
/// for a single operation.
/// </summary>
public sealed class MessageRequestChannel
{
    private readonly Channel<bool> _channel;

    private MessageRequestChannel(Channel<bool> channel)
    {
        _channel = channel;
    }

    /// <summary>
    /// Creates a new <see cref="MessageRequestChannel"/> with the specified channel options.
    /// </summary>
    /// <param name="channelOptions">
    /// An <see cref="UnboundedChannelOptions"/> instance to configure the channel.
    /// In reality, its options are used to create a bounded channel with a capacity of 1.
    /// </param>
    public static MessageRequestChannel Create(
        UnboundedChannelOptions channelOptions)
    {
        var boundedChannelOptions = new BoundedChannelOptions(1)
        {
            FullMode = BoundedChannelFullMode.DropWrite,

            AllowSynchronousContinuations = channelOptions.AllowSynchronousContinuations,
            SingleReader = channelOptions.SingleReader,
            SingleWriter = channelOptions.SingleWriter,
        };

        var backingChannel = Channel.CreateBounded<bool>(boundedChannelOptions);
        var channel = new MessageRequestChannel(backingChannel);
        return channel;
    }

    /// <summary>
    /// Writes one request into the channel.
    /// </summary>
    public async Task WriteOne(CancellationToken cancellationToken = default)
    {
        await _channel.Writer.WriteAsync(true, cancellationToken);
    }

    /// <summary>
    /// Consumes all requests and returns whether any was found in the channel.
    /// </summary>
    public bool ConsumeAllRequests()
    {
        return _channel.Reader.TryRead(out _);
    }
}

#endif
