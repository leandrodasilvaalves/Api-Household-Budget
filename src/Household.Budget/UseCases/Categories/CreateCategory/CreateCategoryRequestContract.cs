using Flunt.Validations;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;

namespace Household.Budget.UseCases.Categories.CreateCategories;

public class CreateCategoryRequestContract : Contract<CreateCategoryRequest>
{
    public CreateCategoryRequestContract(CreateCategoryRequest request)
    {
        Requires()
            .IsNotNullOrEmpty(request.Name, CategoryKnownErrors.CATEGORY_NAME_IS_REQUIRED)
            .IsGreaterOrEqualsThan(request.Name, 3, CategoryKnownErrors.CATEGORY_NAME_MIN_LENGTH)
            .IsLowerOrEqualsThan(request.Name, 25, CategoryKnownErrors.CATEGORY_NAME_MAX_LENGTH)
            .IsValidModelTypeForUser(request.Owner, request.UserClaims ?? [], CategoryKnownErrors.CATEGORY_OWNER_FORBIDDEN_FOR_USER);
    }
}
