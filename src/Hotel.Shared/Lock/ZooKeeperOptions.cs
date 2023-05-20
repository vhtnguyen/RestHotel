
namespace Hotel.Shared.Lock;

public class ZooKeeperOptions
{
    public string Host { get; set; } = null!;
    public int ConnectionTimeout { get; set; }
}
