
using Hotel.BusinessLogic.DTO.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Hotel.BusinessLogic.Services;
using Hotel.DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;
using Hotel.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Hotel.BusinessLogic.DTO.Shared;

namespace Hotel.API.Controllers;
[Authorize(Roles = "Manager,Staff")]
[Route("api/[controller]")]
[ApiController]
public class MyProfileController : Controller
{
    private readonly IUserService _userService;

    public MyProfileController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet("")]
    public async Task<ActionResult> GetMyProfile()
    {
        var userID = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _userService.GetUserByIDAsync(userID);
        return Ok(result);
    }
    [HttpPut("")]
    public async Task<ActionResult> ChangeUserPassword([FromBody]PasswordToChangeDTO pwd)
    {
        var currentUserID = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _userService.ChangeUserPassWordAsync(currentUserID, pwd.CurrentPassword, pwd.NewPassword);
        return Ok($"Changed password: User#{currentUserID}");
    }



}


