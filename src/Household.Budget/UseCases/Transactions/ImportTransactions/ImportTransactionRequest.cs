
using Household.Budget.Contracts;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;

namespace Household.Budget.UseCases.Transactions.ImportTransactions;

public class ImportTransactionRequest : Request
{
    public Stream? Stream => File?.OpenReadStream();
    public string DefaultDescription { get; set; } = "Importing process";
    public PaymentType PaymentType { get; set; } = PaymentType.MONEY;
    public int Year { get; set; } = DateTime.Now.Year;
    public string Tags { get; set; } = "";
    public IFormFile File { get; set; }

    public override void Validate()
    {
        if (CurrentYear.IsValid(Year) is false)
        {
            AddNotification(CommonErrors.INVALID_YEAR);
        }
    }

    public void ValidateCsvFile()
    {
        if (Stream is null)
        {
            AddNotification(TransactionErrors.CSV_FILE_IS_REQUIRED);
        }
    }

    public List<string> SplitTags() => [.. Tags?.Split(";")];
}


public class TransactionImportCsvData
{
    public string Category { get; set; } = "";
    public string Subcategory { get; set; } = "";
    public decimal Jan { get; set; }
    public decimal Fev { get; set; }
    public decimal Mar { get; set; }
    public decimal Apr { get; set; }
    public decimal Mai { get; set; }
    public decimal Jun { get; set; }
    public decimal Jul { get; set; }
    public decimal Aug { get; set; }
    public decimal Sep { get; set; }
    public decimal Oct { get; set; }
    public decimal Nov { get; set; }
    public decimal Dec { get; set; }

    public decimal GetTotal(int month) => month switch
    {
        1 => Jan,
        2 => Fev,
        3 => Mar,
        4 => Apr,
        5 => Mai,
        6 => Jun,
        7 => Jul,
        8 => Aug,
        9 => Sep,
        10 => Oct,
        11 => Nov,
        12 => Dec,
        _ => throw new ArgumentOutOfRangeException(),
    };
}