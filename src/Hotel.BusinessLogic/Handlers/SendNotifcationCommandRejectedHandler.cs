using Hotel.BusinessLogic.Commands;
using Hotel.Shared.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Handlers;

internal class SendNotifcationCommandRejectedHandler : ICommandHandler<SendNotificationCommandRejected>
{
    public Task HandleAsync(SendNotificationCommandRejected command)
    {
        return Task.CompletedTask;
    }
}
