using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget;

public class GetCategoryByIdResponse : Response<Category>
{
    public GetCategoryByIdResponse(Category data) : base(data) { }
}