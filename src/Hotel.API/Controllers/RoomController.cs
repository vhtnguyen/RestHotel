
using Hotel.BusinessLogic.DTO.Rooms;
using Microsoft.AspNetCore.Mvc;
using Hotel.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using org.apache.zookeeper.data;

namespace Hotel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomController : ControllerBase
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


    [Authorize(Roles = "manager")]
    [HttpPost("")]
    public async Task<ActionResult<RoomToReturnDetailDTO>> CreateRoom([FromBody] RoomToCreateDTO roomToCreateDTO)
    {
        return Ok(await _roomService.CreateRoomAsync(roomToCreateDTO));
    }


    [Authorize(Roles = "manager")]
    [HttpDelete("")]
    public async Task<ActionResult> RemoveRoom([FromQuery] int id)
    {
        await _roomService.RemoveRoomByIDAsync(id);
        return Ok($"Removed room #{id}");
    }
  
    [HttpGet("detail")]
    public async Task<ActionResult<RoomToReturnDetailDTO>> GetDetail ([FromQuery]int id)
    {
        return Ok(await _roomService.GetRoomByIDAsync(id));
    }

    [Authorize(Roles = "manager")]
    [HttpPut("")]
    public async Task<ActionResult<RoomToReturnDetailDTO>> UpdateRoom(RoomToCreateDTO room)
    {
        return Ok(await _roomService.UpdateRoomAsync(room));
    }

    [HttpGet("search")]
    public async Task <ActionResult<List<RoomToReturnDetailDTO>>?> Search([FromQuery]bool byRoomID,string roomType, string status,string? searchContent)
    {
        
        if (byRoomID==true)
        {
            if (searchContent != null)
            {
                int id = Int32.Parse(searchContent);
                var room = await _roomService.GetRoomByIDAsync(id);
                List<RoomToReturnDetailDTO> result = new List<RoomToReturnDetailDTO>();
                if (room != null)
                {
                    result.Add(room);
                }
                return result;

            }
            else
                throw new Exception("Query invalid");

        }
        else
        {
            return await _roomService.FindRooms(roomType, status);
        }
        
    }
   
}

