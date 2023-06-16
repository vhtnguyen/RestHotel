using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories.IRepositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Hotel.DataAccess.Repositories;

internal class InvoiceRepository : IInvoiceRepository
{
    private readonly IGenericRepository<Invoice> _genericRepository;
    private readonly AppDbContext _context;

    public InvoiceRepository(IGenericRepository<Invoice> genericRepository, AppDbContext context)
    {
        _genericRepository = genericRepository;
        _context = context;
    }
    public async Task<Invoice?> FindAsync(Expression<Func<Invoice, bool>> predicate)
        => await _context.Invoice
            .Include(i => i.ReservationCards)
            .ThenInclude(i => i.Room)
            .ThenInclude(i => i.RoomDetail)
            .Include(i => i.HotelServices)
            .ThenInclude(i => i.HotelService)
            .FirstOrDefaultAsync(predicate);

    public async Task<IEnumerable<Invoice>> GetAllInvoice()
    {
        var result = await _genericRepository.GetListAsync();
        return result;
    }

    public async Task<Invoice?> CreateAsync(Invoice invoice)
    {
        var result = await _genericRepository.CreateAsync(invoice);
        return result;
    }
    public async Task<Invoice?> GetInvoiceDetail(int id)
    {
        var result = await _context.Invoice
                        .Include(i => i.ReservationCards)
                        .ThenInclude(i => i.Room)
                        .ThenInclude(i => i.RoomDetail)
                        .Include(i => i.HotelServices)
                        .ThenInclude(i => i.HotelService)
                        .FirstOrDefaultAsync(i => i.Id == id);

        return result;
    }

    public async Task SaveChangesAsync()
    {
        await _genericRepository.SaveChangesAsync();
    }

    public async Task RemoveInvoice(Invoice invoice)
    {
        await _genericRepository.DeleteAsync(invoice);
    }

    public async Task UpdateInvoice(Invoice invoice)
    {
        await _genericRepository.UpdateAsync(invoice);
    }
}
