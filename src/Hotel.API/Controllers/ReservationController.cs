using Hotel.BusinessLogic.DTO.HotelReservation;
using Hotel.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

[ApiController]
[Route("reservation-cards")]
public class ReservationController : Controller
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet("page")]
    public ActionResult<string> Get(int page, int entries)
    {
        //Console.WriteLine("Page: " + page + ", entries: " + entries);
        return Ok("Hotel API");
    }

    [HttpPost("")]
    public ActionResult<string> Post(ReservationCreateDTO reservation)
    {
        //Console.WriteLine("Page: " + page + ", entries: " + entries);
        return Ok(_reservationService.CreateReservation(reservation));
    }
}