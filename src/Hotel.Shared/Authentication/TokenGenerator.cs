using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Hotel.Shared.Authentication;

public class TokenGenerator : ITokenGenerator
{
    private readonly JwtOptions _options;
    public TokenGenerator(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }
    public Task<string> GenerateToken(TokenPayload payload)
    {
        var key = Encoding.ASCII.GetBytes(_options.Key);

        var claims = new ClaimsIdentity();

        foreach (var role in payload.Roles)
        {
            claims.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, payload.Id.ToString()));
        claims.AddClaim(new Claim(ClaimTypes.Name, payload.Username));

        var token = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddMinutes(_options.ExpirationAt),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.CreateToken(token);
        var response = tokenHandler.WriteToken(jwtToken);

        return Task.FromResult(response);
    }
}