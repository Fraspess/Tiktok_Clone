using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class NullActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var parameters = context.ActionDescriptor.Parameters;

        foreach (var parameter in parameters)
        {
            if (!context.ActionArguments.TryGetValue(parameter.Name, out var value) || value is null)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    isSuccess = false,
                    errors = new[] { "Тіло запиту не має бути порожнім!" }
                });
                return;
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}