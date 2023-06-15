
using Hotel.BusinessLogic.DTO.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Hotel.BusinessLogic.Services.IServices;

namespace Hotel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StaffController : ControllerBase
{
    private readonly IUserService _userService;

    public StaffController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet("")]

    public async Task<ActionResult> Get()
    {
        return Ok(await _userService.GetUsersAsync());
    }

    [HttpGet("search")]
    public async Task<ActionResult<UserToReturnDTO>> Search([FromQuery] string value, string option = "id")
    {

        if (option.IsNullOrEmpty() || value.IsNullOrEmpty()) return BadRequest();

        return Ok(await _userService.SearchUserAsync(option, value));
    }

    [HttpPost("")]
    public async Task<ActionResult> CreateUser([FromBody] UserToCreateDTO userToCreateDTO)
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


