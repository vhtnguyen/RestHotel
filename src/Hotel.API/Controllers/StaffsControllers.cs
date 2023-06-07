
using Hotel.BusinessLogic.DTO.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Hotel.BusinessLogic.Services;

namespace Hotel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
    public class StaffsController : Controller
    {
    private readonly IUserService _userService;

    public StaffsController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet("")]

    public async Task<ActionResult> Get() {
        try
        {
            return Ok(await _userService.GetUsersAsync());
        }
        catch (Exception)
        {
            return BadRequest("Something went wrong");
        }

    }
}


