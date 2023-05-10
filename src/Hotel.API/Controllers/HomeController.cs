using Hotel.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

[ApiController]
[Route("")]
public class HomeController : Controller
{
    [HttpGet]
    [SampleActionFilter]
    public IActionResult Get()
    {
        return Ok("Hotel API");
    }
}
