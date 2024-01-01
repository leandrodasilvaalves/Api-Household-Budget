using System.Net;

using Household.Budget.UseCases.Identity.LoginUser;
using Household.Budget.UseCases.Identity.RegisterUser;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Api.Controllers;

[ApiController]
[Route("api/v1/identity")]
public class IdentityController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest request,
                                                   [FromServices] IRegisterUserHandler handler)
    {
        var result = await handler.Handle(request, HttpContext.RequestAborted);
        return result.ToActionResult(HttpStatusCode.Created);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest request,
                                                [FromServices] ILoginUserRequestHandler handler)
    {
        var result = await handler.Handle(request, HttpContext.RequestAborted);
        return result.ToActionResult(HttpStatusCode.OK);
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePasswordAysnc([FromBody] ChangeUserPasswordRequest request,
                                                         [FromServices] IChangeUserPasswordHandler handler)
    {
        var result = await handler.Handle(request, HttpContext.RequestAborted);
        return result.ToActionResult(HttpStatusCode.OK);
    }
}