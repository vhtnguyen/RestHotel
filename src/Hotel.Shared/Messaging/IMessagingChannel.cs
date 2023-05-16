using System.Threading.Channels;

namespace Hotel.Shared.Messaging;

public interface IMessagingChannel<T>
{
    public ChannelReader<T> Reader { get; }
    public ChannelWriter<T> Writer { get; }
}
