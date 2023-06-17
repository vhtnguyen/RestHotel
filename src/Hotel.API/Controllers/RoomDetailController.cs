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

        [HttpGet("id")]
        public async Task<RoomDetailToReturnDTO> Get(int id)
        {
            return await _roomDetailService.getRoomDetailByID(id);
        }
        [HttpGet]
        public Task<IEnumerable<RoomDetailToReturnDTO>> Get()
        {

            return _roomDetailService.getAllRoomDetail();
        }


        [Authorize(Roles = "manager")]
        [HttpDelete]
        public async Task Delete(int id)
        {
            await _roomDetailService.RemoveRoomDetail(id);
        }


        [Authorize(Roles = "manager")]
        [HttpPost]
        public async Task Post(RoomDetailToCreateDTO roomDetail)
        {
            await _roomDetailService.AddRoomDetail(roomDetail);
        }

    }
}
