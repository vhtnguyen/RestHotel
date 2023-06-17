using Hotel.BusinessLogic.DTO.RoomRevenue;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.DataAccess.ObjectValues;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomRevenueController : Controller
    {
        IRoomRevenueService _roomRevenueService;

        public RoomRevenueController(IRoomRevenueService roomRevenueService
            )
        {
            _roomRevenueService = roomRevenueService;
        }
        [Authorize(Roles = "manager")]
        [HttpGet]
        public async Task<IEnumerable<RoomRevenueToReturnDTO>> Get()

        {
            return await _roomRevenueService.getAll();
        }
    }
}
