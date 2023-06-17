using Hotel.BusinessLogic.DTO.Invoices;

namespace Hotel.BusinessLogic.Services.IServices;

public interface IInvoiceService
{
    Task<IEnumerable<InvoiceToGetAllDTO>> GetAllInvoiceAsync();
    Task<InvoiceToDetailDTO> GetDetailDTO(int orderId);
    Task AddService(int invoiceId, int serviceId);
    Task RemoveService(int invoiceId, int serviceId);
    Task AddReservationCard(int invoiceId, int cardId);
    Task RemoveReservationCard(int invoiceId, int cardId);
    Task Delete(int invoiceId);
    Task<(double, List<string>)> CalculateInvoice(int id);
}
