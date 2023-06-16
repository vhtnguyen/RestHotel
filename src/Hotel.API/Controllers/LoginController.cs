
using Hotel.BusinessLogic.DTO.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Hotel.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Hotel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<StaffController> _logger;

    public LoginController(
        ILogger<StaffController> logger,
        IUserService userService)
    {
        _userService = userService;
        _logger = logger;
    }
    [HttpPost]
    public async Task<ActionResult> Login(UserCreateDto user)
    {
        (var token, var userDto) = await _userService.Login(user);
        return Ok(new { Token = token, user = userDto });
    }
}


