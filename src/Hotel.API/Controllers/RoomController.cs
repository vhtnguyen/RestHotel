
using Hotel.BusinessLogic.DTO.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Hotel.BusinessLogic.Services;
using Hotel.DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;
using Hotel.BusinessLogic.DTO.Users;

namespace Hotel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomController : Controller
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }
    [HttpGet("")]

    public async Task<ActionResult<List<RoomToReturnListDTO>>> Get()
    {
        return Ok(await _roomService.GetRoomListAsync());
    }

    [HttpPost("")]
    public async Task<ActionResult<RoomToReturnDetailDTO>> CreateRoom([FromBody] RoomToCreateDTO roomToCreateDTO)
    {
        return Ok(await _roomService.CreateRoomAsync(roomToCreateDTO));
    }
    [HttpDelete("")]

    public async Task<ActionResult<List<RoomToReturnListDTO>>> RemoveRoom([FromQuery] int id)
    {
        await _roomService.RemoveRoomByIDAsync(id);
        return Ok($"Removed room #{id}");
    }

    [HttpGet("detail")]
    public async Task<ActionResult<RoomToReturnDetailDTO>> Get([FromQuery]int id)
    {
        return Ok(await _roomService.GetRoomByIDAsync(id));
    }
}


