using Household.Budget.Contracts.Events;
using Household.Budget.UseCases.Categories.EventHandlers;

using MassTransit;

namespace Household.Budget.Infra.Consumers;

public class SubcategoryWasCreatedConsumer : IConsumer<SubcategoryWasCreated>
{
    private readonly IAttachSubCategoryEventHandler _handler;

    public SubcategoryWasCreatedConsumer(IAttachSubCategoryEventHandler handler) =>
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));

    public Task Consume(ConsumeContext<SubcategoryWasCreated> context) =>
        _handler.Handle(context.Message, CancellationToken.None);
}