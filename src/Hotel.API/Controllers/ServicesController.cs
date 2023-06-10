
using Hotel.BusinessLogic.DTO.HotelServices;
using Hotel.BusinessLogic.DTO.Users;
using Hotel.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : Controller
{
    private readonly IHotelServicesService _hotelServicesService;

    public ServicesController(IHotelServicesService hotelServicesService)
    {
        _hotelServicesService = hotelServicesService;
    }

    [HttpGet("")]

    public async Task<ActionResult> Get()
    {
        return Ok( await _hotelServicesService.GetServicesAsync());
    }

    [HttpPost("")]
    public async Task<ActionResult> CreateService(ServiceToCreateDTO serviceDTO)
    {
        return Ok(await _hotelServicesService.CreateServiceAsync(serviceDTO));
    }

    [HttpDelete("")]
    public async Task<ActionResult> RemoveService(int serviceId)
    {
        await _hotelServicesService.RemoveServiceAsync(serviceId);
        return Ok($"Removed service #{serviceId}.");
    }


}
