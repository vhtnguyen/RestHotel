using Hotel.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
        [Authorize(Roles = "manager")]
        [HttpGet]
        public async Task<IActionResult> Get()

        {
            var res = await _roomOccupancyService.getAll();

            return Ok(res);
        }
        [Authorize(Roles = "manager")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByFilter(int id)

        {
            var res = await _roomOccupancyService.getByRoomDetailId(id);

            return Ok(res);
        }
        [Authorize(Roles = "manager")]
        [HttpGet("{id}/{month}/{year}")]
        public async Task<IActionResult> GetByFilter(int id, int month, int year)

        {
            var res = await _roomOccupancyService.getByTypeAndMonth(id, month, year);

            return Ok(res);
        }

        [Authorize(Roles = "manager")]
        [HttpGet("{month}/{year}")]
        public async Task<IActionResult> GetByFilter(int month, int year)

        {
            var res = await _roomOccupancyService.getByMonth(month, year);

            return Ok(res);
        }
    }
}
