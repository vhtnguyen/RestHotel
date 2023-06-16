namespace Hotel.Shared.Authentication;

public interface ITokenGenerator
{
    Task<string> GenerateToken(TokenPayload payload);
}