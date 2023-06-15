namespace Hotel.Shared.Authentication;

public class JwtOptions
{
    public string Key { get; set; } = null!;
    public int ExpirationAt { get; set; }
}