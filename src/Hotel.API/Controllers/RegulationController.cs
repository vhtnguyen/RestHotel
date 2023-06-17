using Microsoft.AspNetCore.Mvc;
using Hotel.BusinessLogic.DTO.RoomRegulation;
using Hotel.BusinessLogic.Services.IServices;

namespace Hotel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegulationController : ControllerBase
    {

        IRoomRegulationService _roomRegulationServices { get; set; }
        public RegulationController(IRoomRegulationService roomRegulationServices)
        {
            this._roomRegulationServices = roomRegulationServices;
        }
        [HttpGet("id")]
        public async Task<RoomRegulationToReturnDTO> Get(int id)
        {
            return await _roomRegulationServices.getRoomByID(id);
        }
        [HttpGet]
        public Task<IEnumerable<RoomRegulationToReturnDTO>> Get()
        {

            return _roomRegulationServices.getAllRoomRegulation();
        }
        //[HttpPut("{id}")]
        //public Task<ActionResult> update(int id,RoomRegulation )
        //{

        //}
        [HttpDelete]
        public async Task Delete(int id)
        {
            await _roomRegulationServices.RemoveRoomRegulation(id);
        }
        [HttpPost]
        public async Task Create(RoomRegulationToCreateDTO roomRegulation)
        {

            await _roomRegulationServices.AddRoomRegulation(roomRegulation);
        }
        //[HttpPost("update")]
        //public async Task update(RoomRegulationToCreateDTO roomRegulation)
        //{
        //    await _roomRegulationServices.UpdateRoomRegulation( roomRegulation);
        //}
    }
}
