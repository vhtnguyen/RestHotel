using Microsoft.AspNetCore.Mvc;
using Hotel.BusinessLogic.Services;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories;
namespace Hotel.API.Controllers
{
    [ApiController]
    [Route("/reg")]
    public class RegulationController : Controller
    {

        IRoomRegulationService _roomRegulationServices { get; set; }
        public RegulationController(IRoomRegulationService roomRegulationServices)
        {
            this._roomRegulationServices = roomRegulationServices;
        }

        [HttpGet]
        public Task<IEnumerable<RoomRegulation>> Get()
        {

           return _roomRegulationServices.getAllRoomRegulation();
        }
        [HttpDelete]
        public async Task Delete(int id)
        {
          await  _roomRegulationServices.RemoveRoomRegulation(id); 
        }

    }
}
