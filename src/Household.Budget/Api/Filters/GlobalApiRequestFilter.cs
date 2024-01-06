using Household.Budget.Contracts.Constants;
using Household.Budget.Contracts.Http.Responses;

using Microsoft.AspNetCore.Mvc.Filters;

namespace Household.Budget.Api.Controllers.Filters;

public class GlobalApiRequestFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("request", out var outValue) && outValue is Request request)
        {
            var userId = context.HttpContext.User.Claims
              .FirstOrDefault(c => c.Type == IdentityClaims.USER_ID)?.Value ?? "";

            var userClaims = context.HttpContext.User.Claims
                .Where(c => c.Type != IdentityClaims.USER_ID)
                .Select(c => c.Value) ?? [];

            request.UserId = userId;
            request.UserClaims = userClaims;
            request.Validate();
            if(request.IsValid is false)
            {
                context.Result = new Response(request.Notifications).ToActionResult();
                return;
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}

