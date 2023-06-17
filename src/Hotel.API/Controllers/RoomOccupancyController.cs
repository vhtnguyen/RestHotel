using Hotel.BusinessLogic.DTO.RoomOccupancy;
using Hotel.BusinessLogic.DTO.RoomRevenue;
using Hotel.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomOccupancyController : Controller
    {
        IRoomOccupancyService _roomOccupancyService;

        public RoomOccupancyController(IRoomOccupancyService roomOccupancyService)
        {
            _roomOccupancyService = roomOccupancyService;
        }

        [HttpGet]
        public async Task<IEnumerable<RoomOccupancyToReturnDTO>> Get()

        {
            return await _roomOccupancyService.getAll();
        }
    }
}
