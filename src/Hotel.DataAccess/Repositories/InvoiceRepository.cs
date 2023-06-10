using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

    public async Task<Invoice> GetAllInvoice()
    {
        List<Invoice> result = _context.Invoices.Include(i => i.ReservationCards).ToList();
        //if (result == null)
        //{
        //    throw new Exception();
        //}
        return result;
    }
}
