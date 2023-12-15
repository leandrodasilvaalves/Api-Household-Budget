using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget;

public class ListCategoriesResponse : Response<List<Category>>
{
    public ListCategoriesResponse(List<Category> data) : base(data) { }
}
