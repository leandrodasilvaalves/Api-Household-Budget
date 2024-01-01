using Household.Budget.UseCases.Categories.ImportCategorySeed;

using MassTransit;

namespace Household.Budget.Infra.Consumers.Category;

public class ImportCategorySeedRequestConsumer : IConsumer<ImportCategorySeedRequest>
{
    private readonly IImportCategorySeedHandler _handler;

    public ImportCategorySeedRequestConsumer(IImportCategorySeedHandler handler) =>
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));

    public Task Consume(ConsumeContext<ImportCategorySeedRequest> context) =>
        _handler.Handle(context.Message, CancellationToken.None);
}
