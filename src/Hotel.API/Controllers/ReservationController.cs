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
    public async Task<ActionResult> Get(int page, int entries)
    {
        //Console.WriteLine("Page: " + page + ", entries: " + entries);
        return Ok(await _reservationService.GetAll(page, entries));
    }

    [HttpPost("")]
    public async Task<ActionResult> Post(ReservationCreateDTO reservation)
    {
        //Console.WriteLine("Page: " + page + ", entries: " + entries);
        if (!reservation.ParseDate())
        {
            return BadRequest("Wrong input date format");
        }
        return Ok(await _reservationService.CreateReservation(reservation));
    }

    [HttpGet("by-period-time")]
    public async Task<ActionResult> GetReservationCardsByTime(PeriodTimeDTO periodTimeDTO)
    {
        if (!periodTimeDTO.ParseDate())
        {
            return BadRequest("Wrong input date format");
        }
        return Ok(await _reservationService.GetReservationCardsByTime(periodTimeDTO));
    }
}