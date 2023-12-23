﻿using Household.Budget.Contracts.Constants;
using Household.Budget.Contracts.Http.Responses;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Household.Budget.Api.Controllers.Filters;

public class AddUserClaimsFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("request", out var request) && request is Request requestObject)
        {
            var userId = context.HttpContext.User.Claims
              .FirstOrDefault(c => c.Type == IdentityClaims.USER_ID)?.Value ?? "";

            var userClaims = context.HttpContext.User.Claims
                .Where(c => c.Type != IdentityClaims.USER_ID)
                .Select(c => c.Value) ?? [];

            requestObject.UserId = userId;
            requestObject.UserClaims = userClaims;
            requestObject.Validate();
            if(requestObject.IsValid is false)
            {
                context.Result = new BadRequestObjectResult(new Response<Request>(requestObject.Notifications));
                return;
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
