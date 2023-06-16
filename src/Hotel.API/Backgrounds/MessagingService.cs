using Hotel.BusinessLogic.Commands;
using Hotel.Shared.Dispatchers;
using Hotel.Shared.Exceptions;
using Hotel.Shared.Handlers;
using Hotel.Shared.Lock;
using Hotel.Shared.Messaging;
using Hotel.Shared.Redis;
using Microsoft.Extensions.Options;
using Polly;

namespace Hotel.API.Backgrounds;

public class MessagingService : BackgroundService
{
    private readonly IMessagingChannel<ICommand> _messagingChannel;
    private readonly RedisOptions _options;
    private readonly ILogger<MessagingService> _logger;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IDistributedLockFactory _distributedLockFactory;

    //public MessagingService(
    //    IMessagingChannel<ICommand> messagingChannel,
    //    IOptions<RedisOptions> options,
    //    ILogger<MessagingService> logger,
    //    ICommandDispatcher commandDispatcher)
    //{
    //    _messagingChannel = messagingChannel;
    //    _options = options.Value;
    //    _logger = logger;
    //    _commandDispatcher = commandDispatcher;
    //}

    public MessagingService(IServiceProvider serviceProvider)
    {
        _messagingChannel = serviceProvider.GetService<IMessagingChannel<ICommand>>()!;
        _options = serviceProvider.GetService<IOptions<RedisOptions>>()!.Value;
        _logger = serviceProvider.GetService<ILogger<MessagingService>>()!;
        _commandDispatcher = serviceProvider.GetService<ICommandDispatcher>()!;
        _distributedLockFactory = serviceProvider.GetService<IDistributedLockFactory>()!;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var command in _messagingChannel.Reader.ReadAllAsync())
        {
            switch (command)
            {
                // case SendNotificationCommand c:
                //     await HandleAsync(
                //         command,
                //         () => _commandDispatcher.DispatchAsync(c),
                //         (_, e) => new SendNotificationCommandRejected()
                //         {
                //             Email = c.Email,
                //             Code = e.Code,
                //             Message = e.Message
                //         });
                //     break;
                // case SendNotificationCommandRejected c:
                //     await _distributedLockFactory.Create("email").GetLocker().LockAsync(() => HandleAsync(
                //         command, 
                //         () => _commandDispatcher.DispatchAsync(c)));
                //     break;
                // default:
                //     await HandleAsync(
                //         command,
                //         () => _commandDispatcher.DispatchAsync(command));
                //     break;
            };
        }
    }

    private async Task HandleAsync<TCommand>(
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
                _logger.LogInformation($"handling message {messageName}");

                await handler();

                _logger.LogInformation($"handled message {messageName}");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                if (currentPollyExecution != 0)
                {
                    _logger.LogInformation($"retry handle message {messageName} at {currentPollyExecution} time");
                }

                currentPollyExecution++;
                if (ex is DomainException domainException && onError != null)
                {
                    var rejectedCommand = onError(command, domainException)!;
                    await _messagingChannel.Writer.WriteAsync(rejectedCommand);
                    return Task.CompletedTask;
                }

                throw new Exception($"unable to handle message {messageName}");
            }
        });
    }
}
