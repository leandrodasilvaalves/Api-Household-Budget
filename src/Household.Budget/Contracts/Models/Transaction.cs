using Household.Budget.Contracts.ViewModels;
using Household.Budget.UseCases.Transactions.UpdateTransaction;

namespace Household.Budget.Contracts.Models;

public class Transaction : Model
{
    public string Description { get; set; } = "";
    public CategoryViewModel Category { get; set; } = new();
    public PaymentViewModel Payment { get; set; } = new();
    public DateTime TransactionDate { get; set; }
    public List<string> Tags { get; set; } = [];

    public void Merge(UpdateTransactionRequest? request, Category category, Subcategory subcategory)
    {
        if (string.IsNullOrWhiteSpace(request?.Description) is false)
        {
            Description = request.Description;
        }
        Merge(request?.Category, category, subcategory);
        Merge(request?.Tags);
        Merge(request?.Payment);
    }

    private void Merge(CategoryViewModel? categoryRequest, Category category, Subcategory subcategory)
    {
        if (categoryRequest is {})
        {
            Category.Merge(category, subcategory);
        }
    }

    private void Merge(PaymentViewModel? payment)
    {
        if(payment is {})
        {
            Payment.Merge(payment);
        }
    }

    private void Merge(List<string>? tags)
    {
        if (tags is not null && tags.Any())
        {
            var tagsRemove = Tags.Where(t => !tags.Contains(t));
            Tags.RemoveAll(t => tagsRemove.Contains(t));

            var tagsInclude = tags.Where(t => !Tags.Contains(t));
            Tags.AddRange(tagsInclude);
        }
    }
}