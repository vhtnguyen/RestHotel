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
    Task<Invoice> GetInvoiceByIdAsync(int id);
    Task<Invoice> GetInoviceByRoomIdAsync(int roomId);
    Task<Invoice> GetInvoiceByIdAsync(DateOnly dateOfInvoice);
}
