using System.Net;

using Household.Budget.Contracts.Http.Controllers;
using Household.Budget.UseCases.Transactions.CreateTransaction;
using Household.Budget.UseCases.Transactions.GetTransactionById;
using Household.Budget.UseCases.Transactions.ListTransactions;
using Household.Budget.UseCases.Transactions.UpdateTransaction;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Api.Controllers
{
    [ApiController]
    [Route("api/v1/transactions")]
    public class TransactionsController : CustomControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] ListTransactionRequest request,
                                                     [FromServices] IListTransactionHandler handler)
        {
            var response = await handler.HandleAsync(request, HttpContext.RequestAborted);
            return response.ToActionResult(HttpStatusCode.OK, HttpStatusCode.NoContent);
        }

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

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] UpdateTransactionRequest request,
                                                     [FromServices] IUpdateTransactionHandler handler)
        {
            request.Id = id;
            var result = await handler.HandleAsync(request, HttpContext.RequestAborted);
            return result.ToActionResult(HttpStatusCode.OK);
        }
    }
}