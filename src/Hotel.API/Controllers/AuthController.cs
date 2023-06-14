using Hotel.BusinessLogic.DTO.Users;
using Hotel.BusinessLogic.Services;
using Hotel.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    //private readonly IUserService _service;
    //public AuthController(IUserService service)
    //{
    //    _service = service;
    //}

    //[HttpGet]
    //public Task<ActionResult<UserReadDto>> Get()
    //{
    //    _service.find();
    //    throw new NotImplementedException(nameof(Index));
    //}

    [HttpPost]
    public Task<ActionResult<UserReadDto>> Get(UserCreateDto user)
    {
        throw new NotImplementedException(nameof(Index));
    }
}
