
using Hotel.Shared.Handlers;

namespace Hotel.Shared.Redis;

public interface IStreamingSubscriber
{
    IStreamingSubscriber SubscribeAsync<TCommand>(string topic) where TCommand : ICommand;
}
