using Flunt.Validations;

using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Models;
using Household.Budget.Contracts.Extensions;

using MediatR;

namespace Household.Budget.UseCases.Categories.UpdateCategory;

public class UpdateCategoryRequest : Request, IRequest<UpdateCategoryResponse>
{
    public Guid Id { get; private set; }
    public string Name { get; set; } = "";
    public ModelStatus Status { get; set; }
    public ModelOwner Owner { get; set; }
    public CategoryType Type { get; set; }

    public Category ToModel() => new()
    {
        Id = $"{Id}",
        Name = Name,
        Owner = Owner,
        Status = Status,
        UserId = $"{UserId}",
        Type = Type,
        UpdatedAt = DateTime.UtcNow,
    };

    public override void Validate() =>
        AddNotifications(new UpdateCategoryRequestContract(this));

    public UpdateCategoryRequest WithId(Guid id)
    {
        Id = id;
        return this;
    }
}

public class UpdateCategoryRequestContract : Contract<UpdateCategoryRequest>
{
    public UpdateCategoryRequestContract(UpdateCategoryRequest request)
    {
        Requires()
            .IsNotNullOrEmpty(request.Name, CategoryKnownErrors.CATEGORY_NAME_IS_REQUIRED)
            .IsGreaterOrEqualsThan(request.Name, 3, CategoryKnownErrors.CATEGORY_NAME_MIN_LENGTH)
            .IsLowerOrEqualsThan(request.Name, 25, CategoryKnownErrors.CATEGORY_NAME_MAX_LENGTH)
            .IsValidModelTypeForUser(request.Owner, request.UserClaims ?? [], CategoryKnownErrors.CATEGORY_OWNER_FORBIDDEN_FOR_USER);
    }
}