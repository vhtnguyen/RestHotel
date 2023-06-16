using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Hotel.Shared.Authentication;

public static class Extensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        using var provider = services.BuildServiceProvider();
        var configuration = provider.GetService<IConfiguration>()!;

        var section = configuration.GetSection("jwt");
        var options = new JwtOptions();
        section.Bind(options);
        services.Configure<JwtOptions>(section);

        services.AddScoped<IStringHasher, StringHasher>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = options.Issuer,
                    ValidAudience = options.Audience
                };
            });

        return services;
    }

    public static IApplicationBuilder UseJwtAuthentication(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        return app;
    }
}