using System.Net;

using Household.Budget.Contracts.Http.Controllers;
using Household.Budget.UseCases.Transactions.CreateTransaction;
using Household.Budget.UseCases.Transactions.GetTransactionById;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Api.Controllers
{
    [ApiController]
    [Route("api/v1/transactions")]
    public class TransactionsController : CustomControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] GetTransacationByIdRequest request,
                                                 [FromServices] IGetTransacationByIdHandler handler)
        {
            var response = await handler.HandleAsync(request, HttpContext.RequestAborted);
            return response.ToActionResult(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTransactionRequest request,
                                                     [FromServices] ICreateTransactionHandler handler)
        {
            var result = await handler.HandleAsync(request, HttpContext.RequestAborted);
            return result.ToActionResult(HttpStatusCode.Created);
        }
    }
}