using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Domain.Entities;

namespace Household.Budget.UseCases.Subcategories.ListSubcategories;

public class ListSubcategoriesResponse : AbstractResponse<PagedListResult<Subcategory>>
{
    public ListSubcategoriesResponse(PagedListResult<Subcategory> data) : base(data) { }
}
