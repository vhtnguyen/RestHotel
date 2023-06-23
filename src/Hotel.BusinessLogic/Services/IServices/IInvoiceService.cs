using Hotel.BusinessLogic.DTO.Invoices;

namespace Hotel.BusinessLogic.Services.IServices;

public interface IInvoiceService
{
    Task<IEnumerable<InvoiceToGetAllDTO>> GetAllInvoiceAsync(InvoiceQueryDto query);
    Task<InvoiceToDetailDTO> GetDetailDTO(int orderId);
    Task<InvoiceToDetailDTO> AddService(int invoiceId, int serviceId);
    Task<InvoiceToDetailDTO> RemoveService(int invoiceId, int serviceId);
    Task<InvoiceToDetailDTO> AddReservationCard(int invoiceId, int cardId);
    Task<InvoiceToDetailDTO> RemoveReservationCard(int invoiceId, int cardId);
    Task<InvoiceToDetailDTO> CheckoutInvoice(int invoiceId);
    Task Delete(int invoiceId);
    Task<(double, List<string>)> CalculateInvoice(int id);
}
