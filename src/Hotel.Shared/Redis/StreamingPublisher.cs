using Hotel.Shared.Handlers;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Hotel.Shared.Redis;

internal class StreamingPublisher : IStreamingPublisher
{
    private readonly ISubscriber _subscriber;
    public StreamingPublisher(
        IConnectionMultiplexer connection)
    {
        _subscriber = connection.GetSubscriber();
    }

    public async Task PublishAsync<TCommand>(string topic, TCommand command) where TCommand : ICommand
    {
        var data = JsonConvert.SerializeObject(command);
        await _subscriber.PublishAsync(topic, data);
    }
}
