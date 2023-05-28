using Hotel.API.Filters;
using Hotel.BusinessLogic.Commands;
using Hotel.Shared.Redis;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

[ApiController]
[Route("")]
public class HomeController : Controller
{
    private readonly IStreamingPublisher streamingPublisher;

    public HomeController(
        IStreamingPublisher streamingPublisher)
    {
        this.streamingPublisher = streamingPublisher;
    }

    [HttpGet]
    [SampleActionFilter]
    public ActionResult<string> Get()
    {
        return Ok("Hotel API");
    }

    [HttpGet("test")]
    public async Task<IActionResult> Post()
    {
        await streamingPublisher.PublishAsync("email", new SendNotificationCommand());
        return Ok();
    }
}
