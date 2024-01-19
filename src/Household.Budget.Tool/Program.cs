using CsvHelper;
using CsvHelper.Configuration;

using Household.Budget.Api.Extensions;
using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Infra.Extensions;
using Household.Budget.UseCases.Transactions.CreateTransaction;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Globalization;

var envName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables()
    .Build();

var services = new ServiceCollection();
services.AddInfra(config);
services.AddUseCases();
var provider = services.BuildServiceProvider();

var categoryRepository = provider.GetRequiredService<ICategoryRepository>();
var handler = provider.GetRequiredService<ICreateTransactionHandler>();

string csvFile = Environment.GetEnvironmentVariable("CSV_FILE") ?? "";
var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    HasHeaderRecord = true,
    Delimiter = ";"
};

using (var reader = new StreamReader(csvFile))
using (var csv = new CsvReader(reader, csvConfig))
{
    var data = csv.GetRecords<Data>().ToList();
    if (data is { })
    {
        var root = config["Identity:RootUser:Request:UserId"] ?? "";
        var ownerUserId = Environment.GetEnvironmentVariable("OWNER_USER_ID") ?? "";
        int transactionYear = int.Parse(Environment.GetEnvironmentVariable("TRANSACTION_YEAR") ?? DateTime.UtcNow.Year.ToString());
        var transactionTags = Environment.GetEnvironmentVariable("TRANSACTION_TAGS")?.Split(",") ?? [];
        var defaultTransacationDescription = Environment.GetEnvironmentVariable("DEFAULT_TRANSACATION_DESCRIPTION") ?? "";
        var categories = (await categoryRepository.GetAllAsync(50, 1, root)).Items;

        foreach (var item in data)
        {
            var category = categories?.FirstOrDefault(c => c.Name == item.Category);
            var subcategory = category?.Subcategories?.FirstOrDefault(c => c.Name == item.Subcategory);
            if (category is null || subcategory is null) { continue; }

            for (var month = 1; month <= 12; month++)
            {
                var total = item.GetTotal(month);
                if (total == 0) { continue; }

                var request = new CreateTransactionRequest
                {
                    UserId = ownerUserId,
                    Description = defaultTransacationDescription,
                    Payment = new()
                    {
                        Type = PaymentType.MONEY,
                        Total = total
                    },
                    Tags = [.. transactionTags],
                    TransactionDate = new DateTime(transactionYear, month, 1),
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
                await handler.HandleAsync(request, CancellationToken.None);
            }
        }
    }
}

