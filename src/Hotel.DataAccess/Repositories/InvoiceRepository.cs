using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
        => await _genericRepository.FindAsync(predicate);

    public async Task<IEnumerable<Invoice>> GetAllInvoice()
    {
        //var result = _context.Invoice
        //     .Include(i => i.ReservationCards).ThenInclude(i => i.Guests)
        //     .ToList();
        //if (result == null)  
        //{
        //    throw new Exception();
        //}
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
                        .Include(i => i.HotelServices)
                        .ThenInclude(i => i.HotelService)
                        .FirstOrDefaultAsync(i => i.Id == id);
                        
        return result;
    }

    public Task<Invoice?> GetInvoiceQuery(Invoice query)
    {
        throw new NotImplementedException();
    }
}
