using Hotel.BusinessLogic.Commands;
using Hotel.Shared.Exceptions;
using Hotel.Shared.Handlers;
using Hotel.Shared.MailKit;
using Hotel.Shared.Payments;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Hotel.BussinessLogic.Handlers;

public class SendNotificationCommandHandler : ICommandHandler<SendNotificationCommand>
{
    private readonly IMailSender _mailSender;
    private readonly MailKitOptions _options;
    private readonly ILogger<SendNotificationCommandHandler> _logger;
    private readonly PaymentOptions _payment;
    public SendNotificationCommandHandler(
        IOptions<MailKitOptions> options,
        IMailSender mailSender,
        IOptions<PaymentOptions> payment,
        ILogger<SendNotificationCommandHandler> logger)
    {
        _mailSender = mailSender;
        _options = options.Value;
        _logger = logger;
        _payment = payment.Value;
    }
    public async Task HandleAsync(SendNotificationCommand command)
    {
        _logger.LogInformation($"handling send notification mail of invoice {command.InvoiceId} to {command.CusName}");

        //     string dir = System.IO.Path.GetDirectoryName(
        //   System.Reflection.Assembly.GetExecutingAssembly().Location);
        string path = "wwwroot/InvoiceCreated.html";
        string mailText;
        using (var streamReader = new StreamReader(path))
        {
            mailText = await streamReader.ReadToEndAsync();
        }

        string table = "";
        double total = 0;
        foreach (var product in command.Details)
        {
            table += "<tr>";
            table += $"<td style='padding: 5px 15px 5px 0; '>{product.Name}</td>";
            table += $"<td style='padding: 0 15px; '>{product.Quantity}</td>";
            table += $"<td style='padding: 0 0 0 15px; ' align='right'>{product.Price}</td>";
            table += "</tr>";

            total += product.Quantity * product.Price * _payment.DepositRatio;
        }


        mailText = mailText.Replace("[CusName]", command.CusName)
           .Replace("[invoiceId]", command.InvoiceId.ToString())
           .Replace("[Total]", total.ToString())
           .Replace("[Table]", table);

        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_options.Email);
        email.To.Add(MailboxAddress.Parse(command.Email));
        email.Subject = "Payment Succeeded";

        var builder = new BodyBuilder();
        builder.HtmlBody = mailText;
        email.Body = builder.ToMessageBody();

        await _mailSender.SendAsync(email);

        _logger.LogInformation($"handled send notification mail of invoice {command.InvoiceId} to {command.CusName}");
    }
}
