
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