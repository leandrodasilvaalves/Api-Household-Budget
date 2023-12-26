using Flunt.Validations;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;

namespace Household.Budget.UseCases.Categories.UpdateSubcategory;

public class UpdateSubcategoryRequestContract : Contract<UpdateSubcategoryRequest>
{
    public UpdateSubcategoryRequestContract(UpdateSubcategoryRequest request)
    {
        Requires()
            .IsNotNullOrEmpty(request.Name, CategoryErrors.CATEGORY_NAME_IS_REQUIRED)
            .IsGreaterOrEqualsThan(request.Name, 3, CategoryErrors.CATEGORY_NAME_MIN_LENGTH)
            .IsLowerOrEqualsThan(request.Name, 25, CategoryErrors.CATEGORY_NAME_MAX_LENGTH)
            .IsValidModelTypeForUser(request.Owner, request.UserClaims ?? [], CategoryErrors.CATEGORY_OWNER_FORBIDDEN_FOR_USER);
    }
}