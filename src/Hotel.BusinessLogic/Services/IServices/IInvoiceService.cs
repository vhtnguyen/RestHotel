using Hotel.BusinessLogic.DTO.Invoices;
using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Services;

public interface IInvoiceService 
{
    Task<List<InvoiceToGetAllDTO>> GetAllInvoiceAsync();
    Task<Invoice> GetInvoiceBrowser(InvoiceBrowserDTO query);
    Task<InvoiceToDetailDTO> GetDetailDTO(int orderId);
    Task UpdateInvocie();
    
}
