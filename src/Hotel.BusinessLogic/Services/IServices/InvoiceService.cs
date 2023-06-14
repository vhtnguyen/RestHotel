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

    public async Task<List<InvoiceToGetAllDTO>> GetAllInvoiceAsync()
    {
        var result = await _invoiceRepository.GetAllInvoice();
        return _mapper.Map<List<InvoiceToGetAllDTO>>(result);
    }

    public async Task<InvoiceToDetailDTO> GetDetailDTO(int orderId)
    {
        //var conditionQuery = _mapper.Map<Invoice>(invoiceDetailDTO);
        //var result = await _invoiceRepository.GetInvoiceDetail(conditionQuery.Id);
        var result = await _invoiceRepository.GetInvoiceDetail(orderId);
        return _mapper.Map<InvoiceToDetailDTO>(result);
    }

    public Task<Invoice> GetInvoiceBrowser(InvoiceBrowserDTO query)
    {
        //var req_query = _mapper.Map<Invoice>(query);
        //var result = _invoiceRepository.
        throw new NotImplementedException();
    }

    public Task UpdateInvocie()
    {
        throw new NotImplementedException();
    }
}


