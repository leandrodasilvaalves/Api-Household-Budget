using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Domain.Entities;

namespace Household.Budget.UseCases.Categories.ListCategories;

public class ListCategoriesResponse : AbstractResponse<PagedListResult<Category>>
{
    public ListCategoriesResponse(PagedListResult<Category> data) : base(data) { }
}
