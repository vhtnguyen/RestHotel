
using Hotel.BusinessLogic.DTO.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Hotel.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Hotel.API.Controllers;

[Authorize(Roles = "Manager")]
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
        var result = await _userService.GetUserListAsync();
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<UserToReturnDTO>> Search([FromQuery] string value, string option = "id")
    {

        if (option.IsNullOrEmpty() || value.IsNullOrEmpty()) return BadRequest("Invalid request.");
        var result = await _userService.SearchUserAsync(option, value);
        return Ok(result);
    }

    [HttpPost("")]
    public async Task<ActionResult> CreateUser([FromBody] UserToCreateDTO userToCreateDTO)
    {
        var result = await _userService.CreateUserAsync(userToCreateDTO);
        return Ok(result);
    }

   
    [HttpGet("{username}")]
    public async Task<ActionResult> GetUserDetailByUsername(string username)
    {
        var result = await _userService.GetUserByUsernameAsync(username);
        return Ok(result);
    }

    [HttpPut("")]
    public async Task<ActionResult> ChangeUserPassword([FromQuery] int id, [FromBody] string newPassWord)
    {
        await _userService.ChangeUserPassWordAsync(id, newPassWord);
        return Ok($"Changed password: User #'{id}'.");
    }

    [HttpDelete("")]
    public async Task<ActionResult> RemoveUser([FromQuery] int id)
    {
        var currentUserID = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (id== currentUserID)
        {
            return BadRequest("Cannot remove self.");
        }
        await _userService.RemoveUserAsync(id);
        return Ok($"Removed: User #'{id}'.");
    }
}


