using Household.Budget.UseCases.Categories.ImportCategorySeed;

using MassTransit;

using MediatR;

namespace Household.Budget.Infra.Consumers.Category;

public class ImportCategorySeedRequestConsumer : IConsumer<ImportCategorySeedRequest>
{
    private readonly IRequestHandler<ImportCategorySeedRequest> _handler;

    public ImportCategorySeedRequestConsumer(IRequestHandler<ImportCategorySeedRequest> handler) =>
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));

    public Task Consume(ConsumeContext<ImportCategorySeedRequest> context) =>
        _handler.Handle(context.Message, CancellationToken.None);
}
