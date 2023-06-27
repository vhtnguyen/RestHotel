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
    public async Task<ActionResult> GetReservationCardsByTime([FromQuery] PeriodTimeDTO periodTimeDTO)
    {
        if (!periodTimeDTO.ParseDate())
        {
            return BadRequest("Wrong input date format");
        }
        return Ok(await _reservationService.GetReservationCardsByTime(periodTimeDTO));
    }

    [HttpGet("by-card-id")]
    public async Task<ActionResult> GetReservationCardsById(int id)
    {
        ReservationCardReturnDTO? card = await _reservationService.GetReservationCardByID(id);
        if (card == null)
        {
            return BadRequest("Card doesn't exist");
        }
        return Ok(card);
    }

    [HttpGet("by-card-invoice-id")]
    public async Task<ActionResult> GetReservationCardsByInvoiceId(int id)
    {
        List<ReservationCardReturnDTO>? cards = await _reservationService.GetReservationCardByInvoiceID(id);
        if (cards.Count() == 0)
        {
            return BadRequest("Invoice doesn't exist");
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
    [HttpDelete("{cardId}")]
    public async Task<ActionResult> RemoveReservationCard(int cardId)
    {
        IdDTO idDTO = new IdDTO();
        idDTO.Id = cardId;
        ReservationCardReturnDTO? card = await _reservationService.RemoveReservationCard(idDTO);
        if (card == null)
        {
            return BadRequest("Wrong reservation card id");
        }

        return Ok("Delete successful");
    }

    [HttpGet("free-rooms-list")]
    public async Task<ActionResult> GetFreeRoomList([FromQuery] RoomToFindFreeDTO roomToFindFreeDTO)
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