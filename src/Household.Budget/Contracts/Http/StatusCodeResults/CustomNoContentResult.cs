using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Household.Budget.Contracts.Http.StatusCodeResults
{
    [DefaultStatusCode(204)]
    public class CustomNoContentResult : ObjectResult
    {
        public CustomNoContentResult(object? value) : base(value)
        {}
    }
}