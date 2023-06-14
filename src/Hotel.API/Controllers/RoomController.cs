using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
