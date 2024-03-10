using Flunt.Validations;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;
using Household.Budget.Domain.Entities;

namespace Household.Budget.UseCases.Categories.CreateCategories;

public class CreateCategoryRequestContract : Contract<CreateCategoryRequest>
{ 
    
    public CreateCategoryRequestContract(CreateCategoryRequest request)
    {
        Requires()
            .IsNotNullOrEmpty(request.Name, CategoryErrors.CATEGORY_NAME_IS_REQUIRED)
            .IsGreaterOrEqualsThan(request.Name, Category.MinLengthName, CategoryErrors.CATEGORY_NAME_MIN_LENGTH)
            .IsLowerOrEqualsThan(request.Name, Category.MaxLengthName, CategoryErrors.CATEGORY_NAME_MAX_LENGTH)
            .IsValidModelTypeForUser(request.Owner, request.UserClaims, CategoryErrors.CATEGORY_OWNER_FORBIDDEN_FOR_USER);
    }
}
