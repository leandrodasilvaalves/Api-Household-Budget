using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget;

public class UpdateCategoryResponse : Response<Category>
{
    public UpdateCategoryResponse(Category data) : base(data) { }
}
