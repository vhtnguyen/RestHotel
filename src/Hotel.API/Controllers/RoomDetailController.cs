using AutoMapper;
using Hotel.BusinessLogic.DTO.RoomDetail;
using Hotel.BusinessLogic.DTO.RoomRegulation;
using Hotel.BusinessLogic.Services;
using Hotel.BusinessLogic.Services.IServices;
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
        [HttpDelete]
        public async Task Delete(int id)
        {
            await _roomDetailService.RemoveRoomDetail(id);
        }
        [HttpPost]
        public async Task<RoomDetailToReturnDTO> Create(RoomDetailToCreateDTO roomDetail)
        {
            return   await _roomDetailService.CreateRoomDetail(roomDetail);
        }
        [HttpPut]
        public async Task update(RoomDetailToReturnDTO roomDetail)
        {
             await   _roomDetailService.UpdateRoomDetail(roomDetail);
        }

    }
}
