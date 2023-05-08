using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Hotel.Shared.MailKit;

internal class MailSender : IMailSender
{
    private readonly MailKitOptions _options;
    public MailSender(
        IOptions<MailKitOptions> options)
    {
        _options = options.Value;
    }
    public async Task SendAsync(MimeMessage message)
    {
        using var client = new SmtpClient();
        await client.ConnectAsync(_options.Host, _options.Port, false);
        await client.AuthenticateAsync(_options.Email, _options.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
