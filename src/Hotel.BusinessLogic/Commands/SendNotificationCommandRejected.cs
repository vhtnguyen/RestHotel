using Hotel.Shared.Handlers;

namespace Hotel.BusinessLogic.Commands;

public class SendNotificationCommandRejected : IRejectedCommand
{
    public string Message { get; set; }
    public string Code { get; set; }
    public string Email { get; set; }
}
