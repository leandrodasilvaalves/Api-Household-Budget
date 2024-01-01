using Household.Budget.Contracts.Events;
using Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;

using MassTransit;

namespace Household.Budget.Infra.Consumers;

public class SubcategoryWasCreatedConsumer : IConsumer<SubcategoryWasCreated>
{
    private readonly IAttachSubcategoryEventHandler _handler;

    public SubcategoryWasCreatedConsumer(IAttachSubcategoryEventHandler handler) =>
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));

    public Task Consume(ConsumeContext<SubcategoryWasCreated> context) =>
        _handler.Handle(context.Message, context.CancellationToken);
}