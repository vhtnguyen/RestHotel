using AutoMapper;
using Hotel.BusinessLogic.DTO.RoomDetail;
using Hotel.BusinessLogic.DTO.RoomRegulation;
using Hotel.BusinessLogic.Services;
using Hotel.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RoomDetailController : Controller
    {
        IRoomDetailService _roomDetailService;
        public RoomDetailController(IRoomDetailService roomDetailService)
        {
            this._roomDetailService = roomDetailService;
        }

        [HttpGet("by-id")]
        public async Task<ActionResult> Get(int id)
        {
            var room = await _roomDetailService.getRoomDetailByID(id);
            return Ok(room);
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {

            var rooms = await _roomDetailService.getAllRoomDetail();
            return Ok(rooms);
        }


        [Authorize(Roles = "manager")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await _roomDetailService.RemoveRoomDetail(id);
            return NoContent();
        }


        [Authorize(Roles = "manager")]
        [HttpPost]
        public async Task<ActionResult> Create(RoomDetailToCreateDTO roomDetail)
        {
            var room = await _roomDetailService.CreateRoomDetail(roomDetail);
            return Ok(room);
        }
        [HttpPut]
        public async Task<ActionResult> update(RoomDetailToUpdateDTO roomDetail)
        {

            await _roomDetailService.UpdateRoomDetail(roomDetail);
            return NoContent();
        }


        [HttpGet("all-id")]

        public async Task<ActionResult> getAllId()
        {
            var result = await _roomDetailService.getAllId();
            return Ok(result);
        }

    }
}
