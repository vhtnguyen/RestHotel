using Microsoft.AspNetCore.Mvc;
using Hotel.BusinessLogic.DTO.RoomRegulation;
using Hotel.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _roomRegulationServices.getRoomByID(id));
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {

            return Ok(await _roomRegulationServices.getAllRoomRegulation());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, RoomRegulationToCreateDTO roomRegulation)
        {
            await _roomRegulationServices.UpdateRoomRegulation(id, roomRegulation);
            return NoContent();

        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await _roomRegulationServices.RemoveRoomRegulation(id);
            return NoContent();
        }


        [Authorize(Roles = "manager")]
        [HttpPost]
        public async Task<ActionResult> Create(RoomRegulationToCreateDTO roomRegulation)
        {

            await _roomRegulationServices.AddRoomRegulation(roomRegulation);
            return NoContent();
        }
    }
}
