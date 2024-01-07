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

    public void Merge(Category category, Subcategory subcategory)
    {
        if (category.Id is not null)
        {
            Id = category.Id;
            Name = category.Name ?? "";
            Merge(subcategory);
        }
    }

    private void Merge(Subcategory subcategory)
    {
        if (subcategory.Id is not null)
        {
            Subcategory ??= new();
            Subcategory.Id = subcategory.Id;
            Subcategory.Name = subcategory.Name ?? "";
        }
    }
}