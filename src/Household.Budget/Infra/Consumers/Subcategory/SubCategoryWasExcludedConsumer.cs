using Household.Budget.Contracts.Events;

using MassTransit;

using MediatR;

namespace Household.Budget.Infra.Consumers;

public class SubCategoryWasExcludedConsumer : IConsumer<SubCategoryWasExcluded>
{
    private readonly INotificationHandler<SubCategoryWasExcluded> _handler;

    public SubCategoryWasExcludedConsumer(INotificationHandler<SubCategoryWasExcluded> handler) =>
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));

    public Task Consume(ConsumeContext<SubCategoryWasExcluded> context) =>
        _handler.Handle(context.Message, CancellationToken.None);
}