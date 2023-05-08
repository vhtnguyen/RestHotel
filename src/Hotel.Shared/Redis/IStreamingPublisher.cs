using Hotel.Shared.Handlers;

namespace Hotel.Shared.Redis;

public interface IStreamingPublisher
{
    Task PublishAsync<TCommand>(string topic, TCommand command) where TCommand : ICommand;
}
