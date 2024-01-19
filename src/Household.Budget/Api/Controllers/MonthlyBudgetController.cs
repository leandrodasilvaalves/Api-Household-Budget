using System.Net;

using Household.Budget.Contracts.Http.Controllers;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Api.Controllers;

[ApiController]
[Route("api/v1/budget")]
public class MonthlyBudgetController : CustomControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateMonthlyBudgetRequest request,
                                                 [FromServices] ICreateMonthlyBudgetHandler handler)
    {
        var response = await handler.HandleAsync(request, HttpContext.RequestAborted);
        return response.ToActionResult(HttpStatusCode.Created);
    }
}