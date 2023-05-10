using Microsoft.AspNetCore.Mvc.Filters;

namespace Hotel.API.Filters;

public class SampleActionFilter : ActionFilterAttribute
{
    // cannot inject to this action filter attribute    
    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var logger = context.HttpContext.RequestServices.GetService<ILogger<SampleActionFilter>>()!;
        logger.LogInformation("running sample action filter attribute");
        return base.OnActionExecutionAsync(context, next);
    }
}
