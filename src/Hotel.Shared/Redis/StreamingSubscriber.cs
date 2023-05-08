using Hotel.Shared.Dispatchers;
using Hotel.Shared.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Hotel.Shared.Redis;

internal class StreamingSubscriber : IStreamingSubscriber
{
    private readonly ISubscriber _subscriber;
    private readonly ICommandDispatcher _commandDispatcher;
    public StreamingSubscriber(
        WebApplication application)
    {
        _subscriber = application.Services.GetService<IConnectionMultiplexer>()!.GetSubscriber();
        _commandDispatcher = application.Services.GetService<ICommandDispatcher>()!;
    }

    public IStreamingSubscriber SubscribeAsync<TCommand>(string topic) where TCommand : ICommand
    {
        Task.Run(() => _subscriber.SubscribeAsync(topic, async (_, data) =>
        {
            var command = JsonConvert.DeserializeObject<TCommand>(data!);
            if (command == null)
            {
                return;
            }
            await _commandDispatcher.DispatchAsync(command);
        }));

        return this;
    }
}
