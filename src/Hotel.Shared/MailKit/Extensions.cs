using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Shared.MailKit;

public static class Extensions
{
    public static IServiceCollection AddMailKit(this IServiceCollection services)
    {
        // get configuration
        using var provider = services.BuildServiceProvider();
        var configuration = provider.GetService<IConfiguration>()!;

        // bind options
        services.Configure<MailKitOptions>(configuration.GetSection("mailKit"));
        services.AddSingleton<IMailSender, MailSender>();

        return services;
    }
}
