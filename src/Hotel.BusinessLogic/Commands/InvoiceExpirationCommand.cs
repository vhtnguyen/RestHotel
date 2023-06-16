using Hotel.Shared.Handlers;

namespace Hotel.BusinessLogic.Commands;


public class InvoiceExpirationCommand : ICommand
{
    public string payment { get; set; } = null!;
}