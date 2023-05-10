namespace Hotel.Shared.Redis;

internal class RedisOptions
{
    public string ConnectionString { get; set; } = null!;
    public bool PollyEnable { get; set; }
    public int RetryPolicy { get; set; }
}
