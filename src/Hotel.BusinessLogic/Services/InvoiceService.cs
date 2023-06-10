using AutoMapper;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Services;

internal class InvoiceService : IInvoiceService
{
    private readonly IMapper _mapper;
    private readonly IInvoiceRepository _invoiceRepository;
    public InvoiceService(IMapper mapper, IInvoiceRepository invoiceRepository)
    {
        _mapper = mapper;
        _invoiceRepository = invoiceRepository;
    }

    public async Task<List<Invoice>> GetAllInvoiceAsync()
    {
        var result = await _invoiceRepository.GetAllInvoice();
        return _mapper.Map<List<Invoice>>(result);
    }

    public Task<Invoice> GetInoviceByRoomIdAsync(int roomId)
    {
        throw new NotImplementedException();
    }

    public Task<Invoice> GetInvoiceByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Invoice> GetInvoiceByIdAsync(DateOnly dateOfInvoice)
    {
        throw new NotImplementedException();
    }
}
