namespace Hotel.Shared.Logging;

public class SeriLogOptions
{
    public string? MinimumLevel { get; set; }
    public bool ElkEnable { get; set; }
    public bool SeqEnable { get; set; }
    public bool ConsoleEnable { get; set; }
}
