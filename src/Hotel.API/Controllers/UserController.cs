
using Hotel.BusinessLogic.DTO.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Hotel.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Hotel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(
        ILogger<UserController> logger,
        IUserService userService)
    {
        _userService = userService;
        _logger = logger;
    }

    [Authorize(Roles = "manager")]
    [HttpGet("")]

    public async Task<ActionResult> Get()
    {
        return Ok(await _userService.GetUserListAsync());
    }

    [Authorize(Roles = "manager")]
    [HttpGet("search")]
    public async Task<ActionResult<UserToReturnDTO>> Search([FromQuery] string value, string option = "id")
    {

        if (option.IsNullOrEmpty() || value.IsNullOrEmpty()) return BadRequest();

        return Ok(await _userService.SearchUserAsync(option, value));
    }


    [Authorize(Roles = "manager")]
    [HttpPost("")]
    public async Task<ActionResult> CreateUser([FromBody] UserToCreateDTO userToCreateDTO)
    {
        return Ok(await _userService.CreateUserAsync(userToCreateDTO));
    }

    [Authorize(Roles = "staff,manager")]
    [HttpGet("profile")]
    public async Task<ActionResult> GetUserDetailByID()
    {
        var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _userService.GetUserByIDAsync(userId);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(UserCreateDto user)
    {
        (var token, var userDto) = await _userService.Login(user);
        return Ok(new { Token = token, user = userDto });
    }


    // admin change password for user
    [Authorize(Roles = "manager")]
    [HttpPut("admin")]
    public async Task<ActionResult> ChangeUserPassword([FromQuery] int id, [FromBody] string newPassWord)
    {
        await _userService.ChangeUserPassWordAsync(id, newPassWord);
        return Ok("ok");
    }

    [Authorize(Roles = "staff,manager")]
    [HttpPut("")]
    public async Task<ActionResult> ChangeUserPassword([FromBody] string currentPassWord, string newPassWord)
    {
        var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _userService.ChangeUserPassWordAsync(userId, currentPassWord, newPassWord);
        return Ok("ok");
    }

    [Authorize(Roles = "manager")]
    [HttpDelete("")]
    public async Task<ActionResult> RemoveUser([FromQuery] int id)
    {
        await _userService.RemoveUserAsync(id);
        return Ok($"Removed user #'{id}'.");
    }
}


