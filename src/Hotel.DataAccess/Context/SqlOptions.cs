namespace Hotel.DataAccess.Context;

// build connection string based on sql options properties
internal class SqlOptions
{
    public string Server { get; set; } = null!;
    public string Database { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool Encrypt { get; set; }
    public bool TrustServerCertificate { get; set; }
}
