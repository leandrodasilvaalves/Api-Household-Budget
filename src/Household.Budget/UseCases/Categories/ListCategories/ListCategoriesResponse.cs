using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Categories.ListCategories;

public class ListCategoriesResponse : Response<PagedListResult<Category>>
{
    public ListCategoriesResponse(PagedListResult<Category> data) : base(data) { }
}
