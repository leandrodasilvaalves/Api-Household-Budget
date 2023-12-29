using Household.Budget.Contracts.Events;

using MassTransit;

using MediatR;

namespace Household.Budget.Infra.Consumers;

public class SubcategoryWasCreatedConsumer : IConsumer<SubcategoryWasCreated>
{
    private readonly INotificationHandler<SubcategoryWasCreated> _handler;

    public SubcategoryWasCreatedConsumer(INotificationHandler<SubcategoryWasCreated> handler) =>
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));

    public Task Consume(ConsumeContext<SubcategoryWasCreated> context) =>
        _handler.Handle(context.Message, CancellationToken.None);
}