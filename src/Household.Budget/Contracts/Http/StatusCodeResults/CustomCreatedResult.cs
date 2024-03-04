using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Contracts.Http.StatusCodeResults
{
    public class CustomCreatedResult : CreatedResult
    {
        public CustomCreatedResult(object value)
            : base(default(string), value) { }
    }

}