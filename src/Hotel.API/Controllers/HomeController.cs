using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok("Hotel API");
    }

    [HttpGet("ping")]
    public ActionResult Ping()
    {
        return Ok("pong");
    }
}
