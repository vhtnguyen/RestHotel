using Hotel.BusinessLogic.DTO.Invoices;
using Hotel.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : Controller
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet("allinvoice")]
    public async Task<ActionResult> GetAllInvoice()
    {
        return Ok(await _invoiceService.GetAllInvoiceAsync());
    }

    [HttpGet("allinvoice")]
    public async Task<ActionResult> GetInvoiceBrowser([FromQuery]invoiceBrowserDTO query)
    {

        return Ok( await _invoiceService.GetInvoiceBrowser(query));
    }

}
