using Hotel.BusinessLogic.DTO.Invoices;
using Hotel.BusinessLogic.Services;
using Hotel.Shared.Exceptions;
using Hotel.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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


    [Authorize(Roles = "staff,manager")]
    [HttpGet("")]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await _invoiceService.GetAllInvoiceAsync());
    }


    [Authorize(Roles = "staff,manager")]
    [HttpGet("{invoiceId}")]
    public async Task<ActionResult> Get(int invoiceId)
    {
        return Ok(await _invoiceService.GetDetailDTO(invoiceId));
    }


    [Authorize(Roles = "staff,manager")]
    [HttpPost("{invoiceId}/service/{serviceId}")]
    public async Task<ActionResult> AddService(int invoiceId, int serviceId)
    {
        return Ok(await _invoiceService.AddService(invoiceId, serviceId));
    }


    [Authorize(Roles = "staff,manager")]
    [HttpPut("{invoiceId}/service/{serviceId}")]
    public async Task<ActionResult> RemoveService(int invoiceId, int serviceId)
    {
        return Ok(await _invoiceService.RemoveService(invoiceId, serviceId));
    }


    [Authorize(Roles = "staff,manager")]
    [HttpPost("{invoiceId}/card/{cardId}")]
    public async Task<ActionResult> AddReservationCard(int invoiceId, int cardId)
    {
        return Ok(await _invoiceService.AddReservationCard(invoiceId, cardId));
    }


    [Authorize(Roles = "staff,manager")]
    [HttpPut("{invoiceId}/card/{cardId}")]
    public async Task<ActionResult> RemoveReservationCard(int invoiceId, int cardId)
    {
        return Ok(await _invoiceService.RemoveReservationCard(invoiceId, cardId));
    }


    [Authorize(Roles = "staff,manager")]
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

    [HttpPut("{invoiceId}")]
    public async Task<ActionResult> Checkout(int invoiceId)
    {
        return Ok(await _invoiceService.CheckoutInvoice(invoiceId));
    }
}
