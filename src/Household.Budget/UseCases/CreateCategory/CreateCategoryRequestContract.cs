using Flunt.Validations;

using Household.Budget.Contracts.Errors;

namespace Household.Budget.UseCases.CreateCategory;

public class CreateCategoryRequestContract : Contract<CreateCategoryRequest>
{
    public CreateCategoryRequestContract(CreateCategoryRequest request)
    {
        Requires()
            .IsNotNullOrEmpty(request.Name, KnownErrors.CATEGORY_NAME_IS_REQUIRED)
            .IsGreaterThan(request.Name, 3, KnownErrors.CATEGORY_NAME_MIN_LENGTH)
            .IsLowerThan(request.Name, 25, KnownErrors.CATEGORY_NAME_MAX_LENGTH)
            .IsValidModelTypeForUser(request.Type, "admin"); //TODO: obter claims do usuario logado
    }
}
