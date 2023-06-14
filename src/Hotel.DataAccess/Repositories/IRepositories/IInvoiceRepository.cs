using Hotel.DataAccess.Entities;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories.IRepositories;

public interface IInvoiceRepository
{
    Task<IEnumerable<Invoice>> GetAllInvoice();
    Task<Invoice?> FindAsync(Expression<Func<Invoice, bool>> predicate);
    Task<Invoice?> GetInvoiceDetail(int id);
    Task SaveChangesAsync();
}
