
using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Transactions.CreateTransaction;

public class CreateTransactionResponse : Response<Transaction>
{
    public CreateTransactionResponse(Transaction data) : base(data){}
}