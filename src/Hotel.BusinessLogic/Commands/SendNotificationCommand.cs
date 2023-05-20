using Hotel.Shared.Handlers;

namespace Hotel.BusinessLogic.Commands;

public class SendNotificationCommand : ICommand
{
    public string Email { get; set; }
}
