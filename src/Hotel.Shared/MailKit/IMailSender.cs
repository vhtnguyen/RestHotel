using MimeKit;

namespace Hotel.Shared.MailKit;

public interface IMailSender
{
    Task SendAsync(MimeMessage message);
}
