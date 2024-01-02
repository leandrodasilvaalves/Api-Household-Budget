using Household.Budget.Contracts.Exceptions;
using Household.Budget.UseCases.Categories.ImportCategorySeed;

using MassTransit;

namespace Household.Budget.Infra.Consumers.Category;

public class ImportCategorySeedRequestConsumer : IConsumer<ImportCategorySeedRequest>
{
    private readonly IImportCategorySeedHandler _handler;

    public ImportCategorySeedRequestConsumer(IImportCategorySeedHandler handler) =>
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));

    public async Task Consume(ConsumeContext<ImportCategorySeedRequest> context)
    {
        var response = await _handler.Handle(context.Message, context.CancellationToken);
        if (response.IsSuccess is false)
            throw new ImportCategorySeedException(response);
    }
}