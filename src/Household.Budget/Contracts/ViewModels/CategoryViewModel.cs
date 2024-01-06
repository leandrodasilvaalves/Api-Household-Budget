using Household.Budget.Contracts.Models;

namespace Household.Budget.Contracts.ViewModels;

public class CategoryViewModel
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public CategoryViewModel? Subcategory { get; set; }

    public static CategoryViewModel CreateFrom(Category category, Subcategory subcategory)
    {
        var viewModel = new CategoryViewModel
        {
            Id = category?.Id ?? "",
            Name = category?.Name ?? "",
            Subcategory = new()
            {
                Id = subcategory?.Id ?? "",
                Name = subcategory?.Name ?? "",
            }
        };
        return viewModel;
    }
}