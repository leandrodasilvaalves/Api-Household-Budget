using Household.Budget.Contracts.Events;

using MassTransit;

using MediatR;

namespace Household.Budget.Infra.Consumers;

public class SubcategoryChangedCategoryConsumer : IConsumer<SubcategoryChangedCategory>
{
    private readonly INotificationHandler<SubcategoryChangedCategory> _handler;

    public SubcategoryChangedCategoryConsumer(INotificationHandler<SubcategoryChangedCategory> handler) => 
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));

    public Task Consume(ConsumeContext<SubcategoryChangedCategory> context) => 
        _handler.Handle(context.Message, CancellationToken.None);
}