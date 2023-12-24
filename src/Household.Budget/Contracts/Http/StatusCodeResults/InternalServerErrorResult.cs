using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Household.Budget.Contracts.Http.StatusCodeResults
{
    [DefaultStatusCode(500)]
    public class InternalServerErrorResult : ObjectResult
    {
        public InternalServerErrorResult(object? value) : base(value)
        {}
    }
}