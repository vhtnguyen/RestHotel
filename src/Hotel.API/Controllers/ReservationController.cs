using Hotel.BusinessLogic.DTO.HotelReservation;
using Hotel.BusinessLogic.DTO.Rooms;
using Hotel.BusinessLogic.Services;
using Hotel.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;
    private readonly IRoomService _roomService;
    private readonly IReservationCancellationService _reservationCancellationService;

    public ReservationController(IReservationService reservationService,
            IReservationCancellationService reservationCancellationService,
            IRoomService roomService)
    {
        _reservationService = reservationService;
        _reservationCancellationService = reservationCancellationService;
        _roomService = roomService;
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

        if (Invoice == null)
        {
            return BadRequest("Some room has been booked before!");
        }
        return Ok(Invoice);
    }


    [HttpPost("confirm")]
    public async Task<ActionResult> ConfirmReservation(ReservationConfirmedDTO reservation)
    {
        //Console.WriteLine("Page: " + page + ", entries: " + entries);
        if (!reservation.ParseDate())
        {
            return BadRequest("Wrong input date format");
        }
        var Invoice = await _reservationService.ConfirmReservation(reservation);
        if (Invoice == null)
        {
            return BadRequest("Reservation doesn't exist");
        }
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


    [Authorize(Roles = "staff,manager")]
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


    [Authorize(Roles = "staff,manager")]
    [HttpPost("edit")]
    public async Task<ActionResult> EditReservationCard(ReservationCardEditDTO reservationCardEditDTO)
    {
        var card = await _reservationService.EditReservationCard(reservationCardEditDTO);
        if (card == null)
        {
            return BadRequest("Wrong reservation card id");
        }

        return Ok(card);
    }


    [Authorize(Roles = "staff,manager")]
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

    [HttpGet("free-rooms-list")]
    public async Task<ActionResult> GetFreeRoomList(RoomToFindFreeDTO roomToFindFreeDTO)
    {
        if (!roomToFindFreeDTO.ParseDate())
        {
            return BadRequest("Wrong input date format");
        }

        List<RoomFreeToReturnDTO>? rooms = await _roomService.FindFreeByDateAsync(roomToFindFreeDTO);

        return Ok(rooms);
    }

    [HttpGet("total-page")]
    public async Task<ActionResult> GetTotalPage(int page, int entries)
    {
        Console.WriteLine(page);
        return Ok(await _reservationService.GetTotalPage(page, entries));
    }
}