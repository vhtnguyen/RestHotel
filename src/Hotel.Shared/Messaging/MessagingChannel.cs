
using System.Threading.Channels;

namespace Hotel.Shared.Messaging;

public class MessagingChannel<T> : IMessagingChannel<T>
{
    public MessagingChannel()
    {
        var channel = Channel.CreateUnbounded<T>();
        Reader = channel.Reader;
        Writer = channel.Writer;
    }

    public ChannelReader<T> Reader { get; }
    public ChannelWriter<T> Writer { get; }
}
