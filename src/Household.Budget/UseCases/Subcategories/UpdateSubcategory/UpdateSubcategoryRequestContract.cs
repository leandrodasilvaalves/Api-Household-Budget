using Flunt.Validations;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;

namespace Household.Budget.UseCases.Categories.UpdateSubcategory;

public class UpdateSubcategoryRequestContract : Contract<UpdateSubcategoryRequest>
{
    public const int NAME_MAX_LENGTH = 25;
    public const int NAME_MIN_LENGTH = 3;

    public UpdateSubcategoryRequestContract(UpdateSubcategoryRequest request)
    {
        Requires()
            .IsNotNullOrEmpty(request.Name, SubcategoryErrors.SUBCATEGORY_NAME_IS_REQUIRED)
            .IsGreaterOrEqualsThan(request.Name, NAME_MIN_LENGTH, SubcategoryErrors.SUBCATEGORY_NAME_MIN_LENGTH)
            .IsLowerOrEqualsThan(request.Name, NAME_MAX_LENGTH, SubcategoryErrors.SUBCATEGORY_NAME_MAX_LENGTH)
            .IsValidModelTypeForUser(request.Owner, request.UserClaims, SubcategoryErrors.SUBCATEGORY_OWNER_FORBIDDEN_FOR_USER);
    }
}