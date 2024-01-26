using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;

namespace Household.Budget.UseCases.Transactions.ImportTransactions;

public class ImportTransactionResponse : AbstractResponse<ImportTransactionRequest>
{
    private ImportTransactionResponse() : base(data: new()) { }
    public ImportTransactionResponse(ImportTransactionRequest data) : base(data) { }
    public ImportTransactionResponse(IEnumerable<Notification> errors) : base(errors) { }
    public ImportTransactionResponse(Notification notification) : base(notification) { }

    public static ImportTransactionResponse Default => new();
}