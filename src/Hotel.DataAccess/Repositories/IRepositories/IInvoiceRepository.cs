using Hotel.DataAccess.Entities;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories.IRepositories;

public interface IInvoiceRepository
{
    Task<IEnumerable<Invoice>> GetAllInvoice(int take, int page, string? status);
    Task<Invoice?> FindAsync(Expression<Func<Invoice, bool>> predicate);
    Task<Invoice?> CreateAsync(Invoice invoice);
    Task<Invoice?> GetInvoiceDetail(int id);
    Task RemoveInvoice(Invoice invoice);
    Task UpdateInvoice(Invoice invoice);
    Task SaveChangesAsync();
}
