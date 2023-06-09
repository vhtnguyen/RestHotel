
using Hotel.BusinessLogic.DTO.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Hotel.BusinessLogic.Services;
using Hotel.DataAccess.Entities;

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
        return Ok(await _userService.GetUsersAsync());
    }
    [HttpPost("")]
    public async Task<ActionResult> CreateUser(UserToCreateDTO userToCreateDTO)
    {
        return Ok(await _userService.CreateUserAsync(userToCreateDTO));
    }

    [HttpDelete("")]
    public async Task<ActionResult> RemoveUser(int userId)
    {
        await _userService.RemoveUserAsync(userId);
            return Ok($"Removed user #'{userId}'.");
    }
}


