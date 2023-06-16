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
        [HttpDelete]
        public async Task Delete(int id)
        {
            await _roomRegulationServices.RemoveRoomRegulation(id);
        }
        [HttpPost]
        public async Task Post(RoomRegulationToCreateDTO roomRegulation)
        {
            await _roomRegulationServices.AddRoomRegulation(roomRegulation);
        }

    }
}
