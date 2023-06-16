using Hotel.BusinessLogic.DTO.RoomRevenue;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.DataAccess.ObjectValues;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    [ApiController]
    [Route("/room-revenue")]
    public class RoomRevenueController : Controller
    {
        IRoomRevenueService _roomRevenueService;

        public RoomRevenueController(IRoomRevenueService roomRevenueService
            )
        {
            _roomRevenueService = roomRevenueService;
        }
        [HttpGet]
        public  async Task<IEnumerable<RoomRevenueToReturnDTO>> Get()

        {
            return await _roomRevenueService.getAll();
        }
    }
}
