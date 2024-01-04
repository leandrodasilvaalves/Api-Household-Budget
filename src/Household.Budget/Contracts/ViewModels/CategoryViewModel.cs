namespace Household.Budget.Contracts.ViewModels;

public class CategoryViewModel
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public CategoryViewModel? Subcategory { get; set; } = new();
}