using System.Globalization;

using CsvHelper;
using CsvHelper.Configuration;

using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.UseCases.Transactions.CreateTransaction;

using MassTransit;

namespace Household.Budget.UseCases.Transactions.ImportTransactions;

public class ImportTransactionHandler : IImportTransactionHandler
{
    private readonly ICategoryData _categoryData;
    private readonly IBus _bus;

    public ImportTransactionHandler(ICategoryData categoryData, IBus bus)
    {
        _categoryData = categoryData ?? throw new ArgumentNullException(nameof(categoryData));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task<ImportTransactionResponse> HandleAsync(ImportTransactionRequest request, CancellationToken cancellationToken)
    {
        request.ValidateCsvFile();
        if (request.IsValid is false)
        {
            return new ImportTransactionResponse(request.Notifications);
        }

        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ";"
        };

        using var reader = new StreamReader(request.Stream);
        using var csv = new CsvReader(reader, csvConfig);

        var data = csv.GetRecords<TransactionImportCsvData>().ToList();
        if (data is { })
        {
            var categories = await GetAllCategoriesAsync(request.UserId, cancellationToken);
            var sendEndpoint = await _bus.GetPublishSendEndpoint<CreateTransactionRequest>();

            foreach (var item in data)
            {
                var category = categories?.FirstOrDefault(c => c.Name == item.Category);
                var subcategory = category?.Subcategories?.FirstOrDefault(c => c.Name == item.Subcategory);
                if (category is null || subcategory is null) { continue; }

                for (var month = 1; month <= 12; month++)
                {
                    var total = item.GetTotal(month);
                    if (total == 0) { continue; }

                    var createTransactionRequest = new CreateTransactionRequest
                    {
                        UserId = request.UserId,
                        Description = request.DefaultDescription,
                        Payment = new()
                        {
                            Type = request.PaymentType,
                            Total = total
                        },
                        Tags = request.SplitTags(),
                        TransactionDate = new DateTime(request.Year, month, 1),
                        Category = new()
                        {
                            Id = category?.Id ?? "",
                            Name = category?.Name ?? "",
                            Subcategory = new()
                            {
                                Id = subcategory?.Id ?? "",
                                Name = subcategory?.Name ?? "",
                            }
                        },
                    };
                    await sendEndpoint.Send(createTransactionRequest, CancellationToken.None);
                }
            }
        }
        return ImportTransactionResponse.Default;
    }

    private async Task<List<Category>> GetAllCategoriesAsync(string userId, CancellationToken cancellationToken)
    {
        bool hasMorePage;
        var (pageNumber, pageSize) = (1, 50);
        List<Category> categories = [];
        do
        {
            var result = await _categoryData.GetAllAsync(pageSize, pageNumber, userId, cancellationToken);
            hasMorePage = result.HasMorePages;
            pageNumber++;
            categories.AddRange(result.Items ?? []);
        } while (hasMorePage);
        return categories;
    }
}
