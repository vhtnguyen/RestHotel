using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

[ApiController]
[Route("reservation-cards")]
public class ReservationController : Controller
{
    [HttpGet("page")]
    public ActionResult<string> Get(int page, int entries)
    {
        //Console.WriteLine("Page: " + page + ", entries: " + entries);
        return Ok("Hotel API");
    }
}