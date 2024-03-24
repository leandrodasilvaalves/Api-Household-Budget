namespace Household.Budget.UseCases.Subcategories.CreateSubcategory;
public class CreateSubcategoryRequest(string name, string categoryId) : Request
{
    public string Name { get; set; } = name;

    public string CategoryId { get; set; } = categoryId;

    public override void Validate()
        => AddNotifications(new CreateSubcategoryRequestContract(this));
}
