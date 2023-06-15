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

internal class InvoiceHotelServiceRepository : IInvoiceHotelServiceRepository
{
    private readonly IGenericRepository<InvoiceHotelService> _genericRepository;
    private readonly AppDbContext _context;

    public InvoiceHotelServiceRepository(IGenericRepository<InvoiceHotelService> genericRepository, AppDbContext context)
    {
        _genericRepository = genericRepository;
        _context = context;
    }
    public async Task<InvoiceHotelService?> FindAsync(Expression<Func<InvoiceHotelService, bool>> predicate) 
        => await _genericRepository.FindAsync(predicate);

    public async Task RemoveInvoiceHotelService(InvoiceHotelService invoiceHotelService)
    {
        await _genericRepository.DeleteAsync(invoiceHotelService);
    }
}
