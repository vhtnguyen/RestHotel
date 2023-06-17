using Hotel.BusinessLogic.DTO.Invoices;
using Hotel.BusinessLogic.Services;
using Hotel.Shared.Exceptions;
using Hotel.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet("")]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await _invoiceService.GetAllInvoiceAsync());
    }

    [HttpGet("{invoiceId}")]
    public async Task<ActionResult> Get(int invoiceId)
    {
        return Ok(await _invoiceService.GetDetailDTO(invoiceId));
    }

    [HttpPost("{invoiceId}/service/{serviceId}")]
    public async Task<ActionResult> AddService(int invoiceId, int serviceId)
    {
        await _invoiceService.AddService(invoiceId, serviceId);
        return NoContent();
    }

    [HttpPut("{invoiceId}/service/{serviceId}")]
    public async Task<ActionResult> RemoveService(int invoiceId, int serviceId)
    {
        await _invoiceService.RemoveService(invoiceId, serviceId);
        return NoContent();
    }

    [HttpPost("{invoiceId}/card/{cardId}")]
    public async Task<ActionResult> AddReservationCard(int invoiceId, int cardId)
    {
        await _invoiceService.AddReservationCard(invoiceId, cardId);
        return NoContent();
    }

    [HttpPut("{invoiceId}/card/{cardId}")]
    public async Task<ActionResult> RemoveReservationCard(int invoiceId, int cardId)
    {
        await _invoiceService.RemoveReservationCard(invoiceId, cardId);
        return NoContent();
    }
    [HttpDelete("{invoiceId}")]
    public async Task<ActionResult> Delete(int invoiceId)
    {
        await _invoiceService.Delete(invoiceId);
        return NoContent();
    }

    [HttpGet("calculate/{invoiceId}")]
    public async Task<ActionResult> CalculateInvoice(int invoiceId)
    {
        (double total, List<string> detailInvoice) = await _invoiceService.CalculateInvoice(invoiceId);

        return Ok(new { Total = total, Detail = detailInvoice });
    }
}
