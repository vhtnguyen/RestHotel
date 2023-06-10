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
    public async Task<IActionResult> GetAllInvoice()
    {
        return Ok(await _invoiceService.GetAllInvoiceAsync());
    }

    //[HttpPost("allinvoice")]
    //public Task<IActionResult> FilterById(invoiceDTO query)
    //{

    //}

}
