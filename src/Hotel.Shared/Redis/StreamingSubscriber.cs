using Hotel.Shared.Dispatchers;
using Hotel.Shared.Exceptions;
using Hotel.Shared.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using StackExchange.Redis;

namespace Hotel.Shared.Redis;

internal class StreamingSubscriber : IStreamingSubscriber
{
    private readonly ILogger<StreamingSubscriber> _logger;
    private readonly ISubscriber _subscriber;
    private readonly IStreamingPublisher _publisher;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly RedisOptions _options;
    public StreamingSubscriber(
        WebApplication application)
    {
        // get redis option include retry policy and something related to polly
        _logger = application.Services.GetService<ILogger<StreamingSubscriber>>()!;  
        _subscriber = application.Services.GetService<IConnectionMultiplexer>()!.GetSubscriber();
        _publisher = application.Services.GetService<IStreamingPublisher>()!;
        _commandDispatcher = application.Services.GetService<ICommandDispatcher>()!;
        _options = application.Services.GetService<IOptions<RedisOptions>>()!.Value;
    }

    public IStreamingSubscriber SubscribeAsync<TCommand>(
        string topic, Func<TCommand, DomainException, IRejectedCommand>? onError) 
        where TCommand : ICommand 
    {
        // write handle async function with polly fault handling
        _subscriber.SubscribeAsync(topic, async (channel, data) =>
        {
            var command = JsonConvert.DeserializeObject<TCommand>(data!);
            if (command == null)
            {
                return;
            }
            //await _commandDispatcher.DispatchAsync(command);
            await HandleAsync(topic, command, () => _commandDispatcher.DispatchAsync(command), onError);
        });

        return this;
    }


    // pass handler and on error here
    private async Task HandleAsync<TCommand>(
        string topic, 
        TCommand command,
        Func<Task> handler, 
        Func<TCommand, DomainException, IRejectedCommand>? onError = null)
        where TCommand : ICommand
    {
        // design policy
        var policy = Policy
            .Handle<Exception>()
            .RetryAsync(_options.RetryPolicy);

        var messageName = command.GetType().Name;
        var currentPollyExecution = 0;
        await policy.ExecuteAsync(async () =>
        {
            try
            {
                _logger.LogInformation($"handling message {messageName} on topic {topic}");

                await handler();

                _logger.LogInformation($"handled message {messageName} on topic {topic}");
                return Task.CompletedTask;
            } catch(Exception ex)
            {
                currentPollyExecution++;
                _logger.LogInformation($"retry handle message {messageName} on topic {topic} at {currentPollyExecution} time");
                if (ex is DomainException domainException && onError != null)
                {
                    var rejectedCommand = onError(command, domainException)!;
                    await _publisher.PublishAsync(topic, rejectedCommand);
                    return Task.CompletedTask;
                }

                throw new Exception($"unable to handle message {messageName} on topic {topic}");
            }
        });
    }
}
