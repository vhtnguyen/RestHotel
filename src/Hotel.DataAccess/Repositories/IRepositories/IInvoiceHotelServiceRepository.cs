using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Repositories.IRepositories;

public interface IInvoiceHotelServiceRepository
{
    Task<InvoiceHotelService?> FindAsync(Expression<Func<InvoiceHotelService, bool>> predicate);
    Task RemoveInvoiceHotelService(InvoiceHotelService invoiceHotelService);
}
