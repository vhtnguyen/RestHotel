using Hotel.Shared.Handlers;
using Hotel.Shared.Messaging;

namespace Hotel.API.Backgrounds
{
    public class MessagingService : BackgroundService
    {
        private readonly IMessagingChannel<ICommand> messagingChannel;

        public MessagingService(IMessagingChannel<ICommand> messagingChannel)
        {
            this.messagingChannel = messagingChannel;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // 2 worker
            return Task.Run(() => Task.WaitAll(consumeAsync(1), consumeAsync(2), consumeAsync(3)));
        }

        private async Task consumeAsync(int worker)
        {
            while (true)
            {
                if (await messagingChannel.Reader.WaitToReadAsync())
                {
                    var value = await messagingChannel.Reader.ReadAsync();
                    Console.WriteLine($"worker {worker}");
                }
            }
        }
    }
}