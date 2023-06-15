using Hotel.BusinessLogic.DTO.HotelReservation;
using Hotel.BusinessLogic.Services;
using Hotel.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
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
    public async Task<ActionResult> CreateReservation(ReservationCreateDTO reservation)
    {
        //Console.WriteLine("Page: " + page + ", entries: " + entries);
        if (!reservation.ParseDate())
        {
            return BadRequest("Wrong input date format");
        }
        PendingInvoiceReturnDTO Invoice = await _reservationService.CreatePendingReservation(reservation);

        try
        {
            return Ok(Invoice);
        }
        finally
        {
            // Response.OnCompleted(async () => 
            // {
            //     if (Invoice != null)
            //     {
            //         await _reservationCancellationService.CheckConfirmedReservation(Invoice.InvoiceId);
            //     }
            // });

        }
    }


    [HttpPost("confirm")]
    public async Task<ActionResult> ConfirmReservation(ReservationConfirmedDTO reservation)
    {
        //Console.WriteLine("Page: " + page + ", entries: " + entries);
        if (!reservation.ParseDate())
        {
            return BadRequest("Wrong input date format");
        }
        InvoiceReturnDTO Invoice = await _reservationService.ConfirmReservation(reservation);
        return Ok(Invoice);
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

    [HttpGet("by-card-id")]
    public async Task<ActionResult> GetReservationCardsById(IdDTO idDTO)
    {
        ReservationCardReturnDTO? card = await _reservationService.GetReservationCardByID(idDTO.Id);
        if (card == null)
        {
            return Ok("Card doesn't exist");
        }
        return Ok(card);
    }

    [HttpGet("by-card-invoice-id")]
    public async Task<ActionResult> GetReservationCardsByInvoiceId(IdDTO idDTO)
    {
        List<ReservationCardReturnDTO>? cards = await _reservationService.GetReservationCardByInvoiceID(idDTO.Id);
        if (cards.Count() == 0)
        {
            return Ok("Invoice doesn't exist");
        }
        return Ok(cards);
    }

    [HttpPost("change-room")]
    public async Task<ActionResult> ChaneRoom(ChangeRoomDTO changeRoomDTO)
    {
        if (!changeRoomDTO.ParseDate())
        {
            return BadRequest("Wrong input date format");
        }

        List<ReservationCardReturnDTO>? handledCards = await _reservationService.ChangeRoom(changeRoomDTO);
        if (handledCards.Count() == 0)
        {
            return BadRequest("Wrong room id");
        }

        return Ok(handledCards);
    }

    [HttpPost("edit")]
    public async Task<ActionResult> EditReservationCard(ReservationCardEditDTO reservationCardEditDTO)
    {
        ReservationCardReturnDTO card = await _reservationService.EditReservationCard(reservationCardEditDTO);
        if (card == null)
        {
            return BadRequest("Wrong reservation card id");
        }

        return Ok(card);
    }

    [HttpDelete("")]
    public async Task<ActionResult> RemoveReservationCard(IdDTO idDTO)
    {
        ReservationCardReturnDTO? card = await _reservationService.RemoveReservationCard(idDTO);
        if (card == null)
        {
            return BadRequest("Wrong reservation card id");
        }

        return Ok("Delete successful");
    }
}