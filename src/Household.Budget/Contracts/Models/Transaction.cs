using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.ViewModels;
using Household.Budget.UseCases.Transactions.UpdateTransaction;

namespace Household.Budget.Contracts.Models;

public class Transaction : Model
{
    private readonly Dictionary<string, object> _metaData = [];
    public string Description { get; set; } = "";
    public CategoryViewModel Category { get; set; } = new();
    public PaymentViewModel Payment { get; set; } = new();
    public DateTime TransactionDate { get; set; }
    public List<string> Tags { get; set; } = [];
    public CategoryType Type { get; set; }

    public void Merge(UpdateTransactionRequest? request, Category category, Subcategory subcategory)
    {
        if (request is { } && request.Status.HasValue)
        {
            Status = request.Status.Value;
            if (Status == ModelStatus.EXCLUDED)
            {
                return;
            }
        }

        if (string.IsNullOrWhiteSpace(request?.Description) is false)
        {
            Description = request.Description;
        }

        if (request is { } && request.TransactionDate.HasValue)
        {
            _metaData.Add(nameof(TransactionDate), TransactionDate);
            TransactionDate = request.TransactionDate.Value;
        }

        Type = category.Type;
        Merge(request?.Category, category, subcategory);
        Merge(request?.Tags);
        Merge(request?.Payment);
    }

    public Dictionary<string, object> GetMetaData() => _metaData;

    public Transaction CloneWith(Dictionary<string, object> metaData)
    {
        var clone = (Transaction)MemberwiseClone();
        if (metaData.TryGetValue(nameof(TransactionDate), out var transactionDate))
        {
            clone.TransactionDate = DateTime.Parse($"{transactionDate}");
        }
        return clone;
    }

    public (int, Month) GetYearMonth()
    {
        var year = TransactionDate.Year;
        int? month = TransactionDate.Month;

        if (Payment.Type == PaymentType.CREDIT_CARD)
        {
            month = Payment?.CreditCard?.Installment?.NextPayments?.FirstOrDefault()?.DueDate.Month;
            year = TransactionDate.Year;
        }
        return (year, (Month)month);
    }

    public bool IsCreditCard() => Payment.Type == PaymentType.CREDIT_CARD;

    public bool HasNextPayments()
    {
        var installment = Payment?.CreditCard?.Installment;
        return IsCreditCard() && installment?.Number > 1;
    }

    public List<NextPaymentViewModel> GetNextPayments(bool removeFirst = false)
    {
        List<NextPaymentViewModel> nextPayments = [];
        if (HasNextPayments())
        {
            nextPayments = [.. Payment?.CreditCard?.Installment?.NextPayments];
            if (removeFirst)
            {
                nextPayments.RemoveAt(0);
            }
        }
        return nextPayments;
    }

    public NextPaymentViewModel GetFirstNextPayment() => 
        Payment?.CreditCard?.Installment?.NextPayments?.FirstOrDefault();

    private void Merge(CategoryViewModel? categoryRequest, Category category, Subcategory subcategory)
    {
        if (categoryRequest is { })
        {
            Category.Merge(category, subcategory);
        }
    }

    private void Merge(PaymentViewModel? payment)
    {
        if (payment is { })
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