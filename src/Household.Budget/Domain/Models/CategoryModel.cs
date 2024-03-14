using Household.Budget.Domain.Entities;

namespace Household.Budget.Domain.Models;

public class CategoryModel
{
    public CategoryModel() { }

    public CategoryModel(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public CategoryModel Subcategory { get; set; }

    public static CategoryModel CreateFrom(Category category, Subcategory subcategory)
    {
        var viewModel = new CategoryModel
        {
            Id = category?.Id,
            Name = category?.Name,
            Subcategory = new()
            {
                Id = subcategory?.Id,
                Name = subcategory?.Name,
            }
        };
        return viewModel;
    }

    public void Merge(Category category, Subcategory subcategory)
    {
        if (category.Id is not null)
        {
            Id = category.Id;
            Name = category.Name;
            Merge(subcategory);
        }
    }

    private void Merge(Subcategory subcategory)
    {
        if (subcategory.Id is not null)
        {
            Subcategory ??= new();
            Subcategory.Id = subcategory.Id;
            Subcategory.Name = subcategory.Name;
        }
    }
}