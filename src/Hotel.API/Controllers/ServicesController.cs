
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
    
}
