using Household.Budget.UseCases.Identity.RegisterUser;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class IdentityController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Created(default(Uri), result) : BadRequest(result);
    }
}
