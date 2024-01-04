using System.Net;

using Household.Budget.Contracts.Http.Controllers;
using Household.Budget.UseCases.Transactions.CreateTransaction;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Api.Controllers
{
    [ApiController]
    [Route("api/v1/transactions")]
    public class TransactionsController : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTransactionRequest request,
                                                     [FromServices] ICreateTransactionHandler handler)
        {
            var result = await handler.HandleAsync(request, HttpContext.RequestAborted);
            return result.ToActionResult(HttpStatusCode.Created);
        }
    }
}