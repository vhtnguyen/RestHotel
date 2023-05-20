namespace Hotel.Shared.Redis;

public class RedisOptions
{
    public string ConnectionString { get; set; } = null!;
    public int RetryPolicy { get; set; }
    public bool MultiWorkerEnable { get; set; }
}
