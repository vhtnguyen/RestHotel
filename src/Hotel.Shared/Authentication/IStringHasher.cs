namespace Hotel.Shared.Authentication;

public interface IStringHasher
{
    string Hash(string password);
    bool Verify(string hashPassword, string inputPassword);
}