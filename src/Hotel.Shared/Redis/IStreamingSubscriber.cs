
using Hotel.Shared.Exceptions;
using Hotel.Shared.Handlers;

namespace Hotel.Shared.Redis;

public interface IStreamingSubscriber
{
    IStreamingSubscriber SubscribeAsync<TCommand>(
        string topic, 
        Func<TCommand, DomainException, IRejectedCommand>? onError) where TCommand : ICommand;
}
