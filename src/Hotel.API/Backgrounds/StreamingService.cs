using Hotel.BussinessLogic.Commands;
using Hotel.Shared.Redis;

namespace Hotel.API.Backgrounds
{
    public class StreamingService : BackgroundService
    {
        private readonly IStreamingPublisher streamingPublisher;
        private readonly IStreamingSubscriber streamingSubscriber;

        public StreamingService(
            IStreamingPublisher streamingPublisher,
            IStreamingSubscriber streamingSubscriber)
        {
            this.streamingPublisher = streamingPublisher;
            this.streamingSubscriber = streamingSubscriber;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(true)
            {
                var command = new SendNotificationCommand();
                await streamingPublisher.PublishAsync("email", command);
                await streamingPublisher.PublishAsync("email", command);
                await streamingPublisher.PublishAsync("email", command);
                await Task.Delay(1000);
            }
        }
    }
}
