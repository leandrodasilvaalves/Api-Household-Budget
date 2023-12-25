using Flunt.Validations;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;

namespace Household.Budget.UseCases.Categories.ListCategories;

public class ListCategoriesRequestContract : Contract<ListCategoriesRequest>
{
    public ListCategoriesRequestContract(ListCategoriesRequest request)
    {
        Requires()
            .IsLowerOrEqualsThan(50, request.PageSize, CategoryErrors.CATEGORY_PAGE_SIZE_MAX);
    }
}