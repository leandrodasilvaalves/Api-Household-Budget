using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Household.Budget.Contracts.Http.StatusCodeResults
{
    [DefaultStatusCode(201)]
    public class CustomCreatedResult : ObjectResult
    {
        public CustomCreatedResult(object? value) : base(value)
        {}
    }

}