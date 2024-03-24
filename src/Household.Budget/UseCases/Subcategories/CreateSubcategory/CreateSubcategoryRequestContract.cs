using Flunt.Validations;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;

namespace Household.Budget.UseCases.Subcategories.CreateSubcategory;

public class CreateSubcategoryRequestContract : Contract<CreateSubcategoryRequest>
{
    public const int NAME_MAX_LENGTH = 25;
    public const int NAME_MIN_LENGTH = 3;

    public CreateSubcategoryRequestContract(CreateSubcategoryRequest request)
    {
        Requires()
            .IsNotNullOrEmpty(request.Name, SubcategoryErrors.SUBCATEGORY_NAME_IS_REQUIRED)
            .IsGreaterOrEqualsThan(request.Name, NAME_MIN_LENGTH, SubcategoryErrors.SUBCATEGORY_NAME_MIN_LENGTH)
            .IsLowerOrEqualsThan(request.Name, NAME_MAX_LENGTH, SubcategoryErrors.SUBCATEGORY_NAME_MAX_LENGTH);
    }
}
