using System.Net;

using Household.Budget.Contracts.Http.Controllers;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;
using Household.Budget.UseCases.MonthlyBudgets.GetMonthlyBudget;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Api.Controllers;

[ApiController]
[Route("api/v1/budget")]
public class MonthlyBudgetController : CustomControllerBase
{
    [HttpGet("{year}/{month}")]
    public async Task<IActionResult> GetAsync([FromRoute] GetMonthlyBudgetsRequest request,
                                              [FromServices] IGetMonthlyBudgetsHandler handler)
    {
        var response = await handler.HandleAsync(request, HttpContext.RequestAborted);
        return response.ToActionResult(HttpStatusCode.OK, HttpStatusCode.NotFound);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateMonthlyBudgetRequest request,
                                                 [FromServices] ICreateMonthlyBudgetHandler handler)
    {
        var response = await handler.HandleAsync(request, HttpContext.RequestAborted);
        return response.ToActionResult(HttpStatusCode.Created);
    }
}