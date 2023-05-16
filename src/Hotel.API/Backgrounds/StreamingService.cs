
public StreamingService(
    IStreamingPublisher streamingPublisher)
{
    this.streamingPublisher = streamingPublisher;
}
protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    while (true)
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