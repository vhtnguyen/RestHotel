using AutoMapper;
using Hotel.BusinessLogic.DTO.Invoices;
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

    public Task CreateInvoice()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Invoice>> GetAllInvoiceAsync()
    {
        var result = await _invoiceRepository.GetAllInvoice();
        return _mapper.Map<List<Invoice>>(result);
    }

    public Task<Invoice> GetInvoiceBrowser(invoiceBrowserDTO query)
    {
        //var req_query = _mapper.Cr;
        throw new NotImplementedException();
    }

    public Task<Invoice> GetInvoiceByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateInvocie()
    {
        throw new NotImplementedException();
    }
}


