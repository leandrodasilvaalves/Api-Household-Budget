namespace Household.Budget.UseCases.Subcategories.ListSubcategories;

public class ListSubcategoriesRequest : Request
{
    public int PageSize { get; set; } = 20;
    public int PageNumber { get; set; } = 1;

    public override void Validate()
        => AddNotifications(new ListSubcategoriesRequestContract(this));
}
