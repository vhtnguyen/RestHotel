using Hotel.Shared.Handlers;

namespace Hotel.BusinessLogic.Commands;

public class SendNotificationCommand : ICommand
{
    public string Email { get; set; } = null!;
    public string CusName { get; set; } = null!;
    public int InvoiceId { get; set; }
    public ICollection<InvoiceDetail> Details { get; set; } = null!;
}

public class InvoiceDetail
{
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public double Price { get; set; }
}
