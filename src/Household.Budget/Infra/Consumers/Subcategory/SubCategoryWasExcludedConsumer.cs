using Household.Budget.Contracts.Events;
using Household.Budget.UseCases.Categories.EventHandlers.DetachSubcategory;

using MassTransit;

namespace Household.Budget.Infra.Consumers;

public class SubCategoryWasExcludedConsumer : IConsumer<SubcategoryWasExcluded>
{
    private readonly IDetachSubcategoryEventHandler _handler;

    public SubCategoryWasExcludedConsumer(IDetachSubcategoryEventHandler handler) =>
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));

    public Task Consume(ConsumeContext<SubcategoryWasExcluded> context) =>
        _handler.HandleAsync(context.Message, context.CancellationToken);
}