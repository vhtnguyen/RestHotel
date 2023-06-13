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
    Task<List<Invoice>> GetAllInvoiceAsync();
    Task<Invoice> GetInvoiceBrowser(invoiceBrowserDTO query);

    Task<Invoice> GetInvoiceByIdAsync(Guid id);
    Task UpdateInvocie();
    Task CreateInvoice();
    
}
