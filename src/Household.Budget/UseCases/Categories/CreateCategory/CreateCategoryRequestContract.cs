using Flunt.Validations;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;

namespace Household.Budget.UseCases.Categories.CreateCategories;

public class CreateCategoryRequestContract : Contract<CreateCategoryRequest>
{
    public const int MinLengthName = 3;
    public const int MaxLengthName = 25;
    
    public CreateCategoryRequestContract(CreateCategoryRequest request)
    {
        Requires()
            .IsNotNullOrEmpty(request.Name, CategoryErrors.CATEGORY_NAME_IS_REQUIRED)
            .IsGreaterOrEqualsThan(request.Name, MinLengthName, CategoryErrors.CATEGORY_NAME_MIN_LENGTH)
            .IsLowerOrEqualsThan(request.Name, MaxLengthName, CategoryErrors.CATEGORY_NAME_MAX_LENGTH)
            .IsValidModelTypeForUser(request.Owner, request.UserClaims, CategoryErrors.CATEGORY_OWNER_FORBIDDEN_FOR_USER);
    }
}
