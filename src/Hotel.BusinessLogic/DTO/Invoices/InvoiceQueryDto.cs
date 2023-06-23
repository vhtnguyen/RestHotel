namespace Hotel.BusinessLogic.DTO.Invoices;

public class InvoiceQueryDto
{
    public int Take { get; set; }
    public int Page { get; set; }
    public string? Status { get; set; }
}