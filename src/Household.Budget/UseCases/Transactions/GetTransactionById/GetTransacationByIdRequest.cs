using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.UseCases.Transactions.GetTransactionById;

public class GetTransacationByIdRequest : Request
{
    [FromRoute]
    public string Id { get; set; }


    public override void Validate()
    {
        if(Id == default)
        {
            AddNotification("TRANSACTION_ID_IS_REQUIRED", "Transaction Id is required");
        }
    }
}

