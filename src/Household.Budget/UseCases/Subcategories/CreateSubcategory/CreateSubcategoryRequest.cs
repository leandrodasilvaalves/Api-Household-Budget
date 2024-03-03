namespace Household.Budget.UseCases.Subcategories.CreateSubcategory;
public class CreateSubcategoryRequest(string name, Guid categoryId) : Request
{
    public string Name { get; } = name;

    public Guid CategoryId { get; } = categoryId;

    public override void Validate()
        => AddNotifications(new CreateSubcategoryRequestContract(this));
}
