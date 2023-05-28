using Hotel.BusinessLogic.Commands;
using Hotel.Shared.Redis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Hotel.API.Backgrounds
{
    public class StreamingService : BackgroundService
    {
        private readonly IStreamingPublisher streamingPublisher;

        public StreamingService(
            IStreamingPublisher streamingPublisher)
        {
            this.streamingPublisher = streamingPublisher;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var command = new SendNotificationCommand();
            //await streamingPublisher.PublishAsync("email", command);
            while (true)
            {
                var command = new SendNotificationCommand();
                await streamingPublisher.PublishAsync("email", command);
                await streamingPublisher.PublishAsync("email", command);
                await Task.Delay(1000);
            }
        }
    }
}
