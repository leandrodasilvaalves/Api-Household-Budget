using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Subcategories.ListSubcategories;

public class ListSubcategoriesResponse : Response<PagedListResult<Subcategory>>
{
    public ListSubcategoriesResponse(PagedListResult<Subcategory> data) : base(data) { }
}
