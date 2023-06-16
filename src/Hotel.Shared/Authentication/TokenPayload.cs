namespace Hotel.Shared.Authentication;

public class TokenPayload
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public ICollection<string> Roles { get; set; } = new List<string>();
}