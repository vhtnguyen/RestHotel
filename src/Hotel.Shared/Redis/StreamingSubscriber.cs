using Hotel.Shared.Dispatchers;
using Hotel.Shared.Exceptions;
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
        // get redis option include retry policy and something related to polly
        _subscriber = application.Services.GetService<IConnectionMultiplexer>()!.GetSubscriber();
        _commandDispatcher = application.Services.GetService<ICommandDispatcher>()!;
    }

    public IStreamingSubscriber SubscribeAsync<TCommand>(
        string topic, Func<TCommand, DomainException, TCommand>? onError) where TCommand : ICommand
    {
        // write handle async function with polly fault handling
        Task.Run(() => _subscriber.SubscribeAsync(topic, async (_, data) =>
        {
            var command = JsonConvert.DeserializeObject<TCommand>(data!);
            if (command == null)
            {
                return;
            }
            await _commandDispatcher.DispatchAsync(command);
            await HandleAsync(() => _commandDispatcher.DispatchAsync(command), onError);
        }));

        return this;
    }


    // pass handler and on error here
    private Task HandleAsync<TCommand>(Func<Task> handler, Func<TCommand, DomainException, TCommand>? onError = null)
        where TCommand : ICommand
    {
        // await handler(); -> handle by polly
        // var errorCommand = onError(command, exception); -> publish error message
        return Task.CompletedTask;
    }
}
