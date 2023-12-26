using Flunt.Validations;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;

namespace Household.Budget.UseCases.Subcategories.ListSubcategories;

public class ListSubcategoriesRequestContract : Contract<ListSubcategoriesRequest>
{
    public ListSubcategoriesRequestContract(ListSubcategoriesRequest request)
    {
        Requires()
            .IsLowerOrEqualsThan(50, request.PageSize, CommonErrors.MAX_PAGE_SIZE);
    }
}