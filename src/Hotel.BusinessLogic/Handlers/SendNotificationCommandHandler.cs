using Hotel.BussinessLogic.Commands;
using Hotel.Shared.Handlers;

namespace Hotel.BussinessLogic.Handlers;

internal class SendNotificationCommandHandler : ICommandHandler<SendNotificationCommand>
{
    public Task HandleAsync(SendNotificationCommand command)
    {
        throw new NotImplementedException();
    }
}
