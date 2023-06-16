
using Hotel.BusinessLogic.DTO.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Hotel.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Hotel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StaffController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<StaffController> _logger;

    public StaffController(
        ILogger<StaffController> logger,
        IUserService userService)
    {
        _userService = userService;
        _logger = logger;
    }
    [HttpGet("")]

    public async Task<ActionResult> Get()
    {
        return Ok(await _userService.GetUserListAsync());
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

    [Authorize(Roles = "staff")]
    [HttpGet("profile")]
    public async Task<ActionResult> GetUserDetailByID([FromQuery] int id)
    {
        // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _userService.GetUserByIDAsync(id);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(UserCreateDto user)
    {
        (var token, var userDto) = await _userService.Login(user);
        return Ok(new { Token = token, user = userDto });
    }

    [HttpPut("")]
    public async Task<ActionResult> ChangeUserPassword([FromQuery] int id, [FromBody] string newPassWord)
    {
        await _userService.ChangeUserPassWordAsync(id, newPassWord);
        return Ok("ok");
    }

    [HttpDelete("")]
    public async Task<ActionResult> RemoveUser([FromQuery] int id)
    {
        await _userService.RemoveUserAsync(id);
        return Ok($"Removed user #'{id}'.");
    }
}


