using Hotel.BusinessLogic.DTO.HotelReservation;
using Hotel.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

[ApiController]
[Route("reservation-cards")]
public class ReservationController : Controller
{
    private readonly IReservationService _reservationService;
    private readonly IReservationCancellationService _reservationCancellationService;

    public ReservationController(IReservationService reservationService, IReservationCancellationService reservationCancellationService)
    {
        _reservationService = reservationService;
        _reservationCancellationService = reservationCancellationService;
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
        InvoiceReturnDTO result = await _reservationService.CreateReservation(reservation);
        
        try
        {
            return Ok(result);
        }
        finally
        {
            Response.OnCompleted(async () => 
            {
                //await _reservationCancellationService.CheckConfirmedReservation(0);
                if (result != null)
                {
                    await _reservationCancellationService.CheckConfirmedReservation(result.Id);
                }
            });
        }
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