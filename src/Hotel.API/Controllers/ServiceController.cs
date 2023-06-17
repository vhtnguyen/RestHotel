
using Hotel.BusinessLogic.DTO.HotelServices;
using Hotel.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Hotel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceController : ControllerBase
{
    private readonly IHotelServicesService _hotelServicesService;

    public ServiceController(IHotelServicesService hotelServicesService)
    {
        _hotelServicesService = hotelServicesService;
    }

    [HttpGet("")]

    public async Task<ActionResult> Get()
    {
        return Ok(await _hotelServicesService.GetServicesAsync());
    }

    [HttpGet("search")]
    public async Task<ActionResult<ServiceToReturnDTO>> Search([FromQuery] string? value, string option = "all", int category = 0)
    {

        if (value.IsNullOrEmpty() && option != "all") return BadRequest();

        return Ok(await _hotelServicesService.SearchSeviceAsync(value, option, category));
    }

    [Authorize(Roles = "manager")]
    [HttpPost("")]
    public async Task<ActionResult> CreateService(ServiceToCreateDTO serviceDTO)
    {
        return Ok(await _hotelServicesService.CreateServiceAsync(serviceDTO));
    }

    [Authorize(Roles = "manager")]
    [HttpDelete("")]
    public async Task<ActionResult> RemoveService(int serviceId)
    {
        await _hotelServicesService.RemoveServiceAsync(serviceId);
        return Ok($"Removed service #{serviceId}.");
    }


}
