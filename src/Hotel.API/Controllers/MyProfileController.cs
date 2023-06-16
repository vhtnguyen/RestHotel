
using Hotel.BusinessLogic.DTO.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Hotel.BusinessLogic.Services;
using Hotel.DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;
using Hotel.BusinessLogic.Services.IServices;

namespace Hotel.API.Controllers;

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
    public async Task<ActionResult> GetUserDetailByID([FromHeader] int userID)
    {
        var result = await _userService.GetUserByIDAsync(userID);
        return Ok(result);
    }
    [HttpPut("")]
    public async Task<ActionResult> ChangeUserPassword([FromQuery] int id, [FromBody] string currentPassWord, string newPassWord)
    {
        await _userService.ChangeUserPassWordAsync(id, currentPassWord, newPassWord);
        return Ok("ok");
    }



}


