using Hotel.BusinessLogic.Commands;
using Hotel.Shared.Exceptions;
using Hotel.Shared.Handlers;

namespace Hotel.BussinessLogic.Handlers;

public class SendNotificationCommandHandler : ICommandHandler<SendNotificationCommand>
{
    public Task HandleAsync(SendNotificationCommand command)
    {
        throw new DomainBadRequestException("test", "error");
    }
}
