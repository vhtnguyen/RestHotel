namespace Hotel.Shared.Authentication;

public class JwtOptions
{
    public string Key { get; set; } = null!;
    public int ExpirationAt { get; set; }
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
}