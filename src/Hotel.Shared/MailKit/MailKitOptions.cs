namespace Hotel.Shared.MailKit;

public class MailKitOptions
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string Password { get; set; } = null!;
}
