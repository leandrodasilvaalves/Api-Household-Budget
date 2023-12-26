using Flunt.Validations;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;

namespace Household.Budget.UseCases.Subcategories.CreateSubcategory;

public class CreateSubcategoryRequestContract : Contract<CreateSubcategoryRequest>
{
    public CreateSubcategoryRequestContract(CreateSubcategoryRequest request)
    {
        Requires()
            .IsNotNullOrEmpty(request.Name, SubcategoryErrors.SUBCATEGORY_NAME_IS_REQUIRED)
            .IsGreaterOrEqualsThan(request.Name, 3, SubcategoryErrors.SUBCATEGORY_NAME_MIN_LENGTH)
            .IsLowerOrEqualsThan(request.Name, 25, SubcategoryErrors.SUBCATEGORY_NAME_MAX_LENGTH)
            .IsNotDefault(request.CategoryId, SubcategoryErrors.SUBCATEGORY_CATEGORY_ID_IS_REQUIRED);
    }
}
